using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RFID_CSharp;
using IR_CSharp;

namespace IR_CSharp
{

    public partial class IR : Form
    {
        string ns;
        string sString;
        string strSingleByte;
        string sOutput;
        int i = 0;
        
        public IR()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("coredll.dll")]
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private void Clear_Mem()
        {
            PostMessage(this.Handle, 0x03FF, IntPtr.Zero, IntPtr.Zero);
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            txtShow.Text = "";
        }

        private void bnRead_Click(object sender, EventArgs e)
        {
            byte[] data = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
            byte[] Addr = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
            byte[] DI1 = {0x00,0x00,0x00,0x00};
	        byte[] DI2 = {0x00,0x00,0x01,0x00};
	        byte[] DI3 = {0x00,0x00,0x02,0x00};
            byte[] ReadBuff = new byte[255];

            //读取2007电表地址
            if (DLT645_DLL.DLT645_2007_ReadAddr(Addr))
	        {
                //字符转换
                for (i = 0; i < 6; i++)
                {
                    strSingleByte = String.Format("{0:X2}", Addr[i]);

                    // 如果不是最后一个数据，则添加一个填充字符，用来隔开两个字节数据
                    if (i < (6 - 1))
                    {
                        sString += strSingleByte;
                        sString += " ";
                    }
                    else
                    {
                        sString += strSingleByte;
                    }
                }

		        ns = ("电表的通信地址：\r\n");
		        ns += sString;
		        ns += ("\r\n");
                txtShow.Text += ns;
                Buzzer.BeepOK();
	        }
	        else
	        {
		        ns = ("读通信地址失败！\r\n");
                txtShow.Text += ns;
                Buzzer.BeepError();
                return;
	        }

	        sString = "";
	        if (DLT645_DLL.DLT645_2007_ReadData1(Addr, DI1, ReadBuff))
	        {
		        //StringUtil::ByteToString(&ReadBuff[6], &sString, ReadBuff[1]-4);
		        for (i = 0; i < ReadBuff[1]-4; i++)
                {
                    strSingleByte = String.Format("{0:X2}", ReadBuff[6+i]);

                    // 如果不是最后一个数据，则添加一个填充字符，用来隔开两个字节数据
                    if (i < (ReadBuff[1]-4 - 1))
                    {
                        sString += strSingleByte;
                        sString += " ";
                    }
                    else
                    {
                        sString += strSingleByte;
                    }
                }
                
                //StringUtil::BCDToDecimal(&sString,&sOutput);
                double value=0;
	            int bei=1;
	            int num=0;
	            for(i = 0;i < sString.Length; i++)
	            {
	                char c=(char)sString[i];
	                if(c == ' ')
	                {
		                value+=num*bei;
		                num=0;
		                bei*=100;
	                }
	                else
	                {
		                num*=10;
		                num+=c-'0';
	                }
	            }
	            value+=num*bei;
	            value/=100;
	            sOutput = String.Format("%.2lf", value);
                
                ns = ("组合有功总电能： ");
		        ns += sOutput;
		        ns += (" KWh\r\n");
		        txtShow.Text += ns;
                Buzzer.BeepOK();
	        }
	        else
	        {
		        ns = ("读取数据失败！\r\n");
		        txtShow.Text += ns;		        		     
		        Buzzer.BeepError();
                return;

	        }

	        sString = "";
	        if (DLT645_DLL.DLT645_2007_ReadData1(Addr, DI2, ReadBuff))
	        {
		        //StringUtil::ByteToString(&ReadBuff[6], &sString, ReadBuff[1]-4);
		        for (i = 0; i < ReadBuff[1]-4; i++)
                {
                    strSingleByte = String.Format("{0:X2}", ReadBuff[6+i]);

                    // 如果不是最后一个数据，则添加一个填充字符，用来隔开两个字节数据
                    if (i < (ReadBuff[1]-4 - 1))
                    {
                        sString += strSingleByte;
                        sString += " ";
                    }
                    else
                    {
                        sString += strSingleByte;
                    }
                }
                
                double value=0;
	            int bei=1;
	            int num=0;
	            for(i = 0;i < sString.Length; i++)
	            {
	                char c=(char)sString[i];
	                if(c == ' ')
	                {
		                value+=num*bei;
		                num=0;
		                bei*=100;
	                }
	                else
	                {
		                num*=10;
		                num+=c-'0';
	                }
	            }
	            value+=num*bei;
	            value/=100;
	            sOutput = String.Format("%.2lf", value);
                
		        ns = ("正向有功总电能： ");
		        ns += sOutput;
		        ns += (" KWh\r\n");
		        txtShow.Text += ns;
		       
		        Buzzer.BeepOK();
	        }
	        else
	        {
                ns = ("读取数据失败！\r\n");
                txtShow.Text += ns;
                Buzzer.BeepError();
                return;
	        }


	        sString = "";
	        if (DLT645_DLL.DLT645_2007_ReadData1(Addr, DI3, ReadBuff))
	        {
		        for (i = 0; i < ReadBuff[1]-4; i++)
                {
                    strSingleByte = String.Format("{0:X2}", ReadBuff[6+i]);

                    // 如果不是最后一个数据，则添加一个填充字符，用来隔开两个字节数据
                    if (i < (ReadBuff[1]-4 - 1))
                    {
                        sString += strSingleByte;
                        sString += " ";
                    }
                    else
                    {
                        sString += strSingleByte;
                    }
                }
                
                //StringUtil::BCDToDecimal(&sString,&sOutput);
                double value=0;
	            int bei=1;
	            int num=0;
	            for(i = 0;i < sString.Length; i++)
	            {
	                char c=(char)sString[i];
	                if(c == ' ')
	                {
		                value+=num*bei;
		                num=0;
		                bei*=100;
	                }
	                else
	                {
		                num*=10;
		                num+=c-'0';
	                }
	            }
	            value+=num*bei;
	            value/=100;
	            sOutput = String.Format("%.2lf", value);

		        ns = ("反向有功总电能： ");
		        ns += sOutput;
		        ns += (" KWh\r\n\r\n");
                txtShow.Text += ns;
		       
		        Buzzer.BeepOK();
	        }
	        else
	        {
                ns = ("读取数据失败！\r\n");
                txtShow.Text += ns;
                Buzzer.BeepError();
                return;
	        }
            txtShow.Text += ("读取数据完成！\r\n");
    
        }

        private void IR_Load(object sender, EventArgs e)
        {
            //参数1对应COM2
            if (DLT645_DLL.DLT645_OpenPort(1) != true)
            {
                MessageBox.Show("ERROR打开端口失败！\r\n");
            }

        }

        private void IR_Closed(object sender, EventArgs e)
        {
	        //DLT645_DLL.IRModulePowerOff();	
            //参数1对应COM2
            if (DLT645_DLL.DLT645_ClosePort(1) != true)
            {
                MessageBox.Show("ERROR关闭端口失败！\r\n");
            }
// // 	        Buzzer.BeepClose();
        }

        private void bnVerify_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void IR_Closing(object sender, CancelEventArgs e)
        {
            //DLT645_DLL.IRModulePowerOff();	
//             if (DLT645_DLL.IRModuleClosePort() != 0/*IR_STATUS_OK*/)
// 	        {
// 		        MessageBox.Show("ERROR!关闭串口失败！\r\n");
// 	        }
//	        Buzzer.BeepClose();
        }
    }
}