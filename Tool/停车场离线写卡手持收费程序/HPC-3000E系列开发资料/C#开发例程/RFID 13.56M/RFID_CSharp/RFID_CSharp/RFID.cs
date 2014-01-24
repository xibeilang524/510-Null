using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RFID_CSharp
{

    public partial class RFID : Form
    {
        public RFID()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("coredll.dll")]
        // [System.Runtime.InteropServices.DllImport("coredll.dll")]
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
            byte[] data = new byte[256];
            byte ret = HPC_RFID_DLL.ISO14443A_ReadCardSn(data);
            if( ret== 0/*RFID_STATUS_OK*/)
            {
                string msg = "读到15693卡号：",ss;
                for(int i=0;i<8;i++)
                {
                    ss = String.Format("{0:X2} ", data[i]);
                    msg += ss;
                }
                txtShow.Text += msg + "\r\n";
                Buzzer.BeepOK();
            }
            else
            {
                Buzzer.BeepError();
            }
        }

        private void RFID_Load(object sender, EventArgs e)
        {
            byte ret = HPC_RFID_DLL.RfidModuleOpenPort(0);
	        if (ret != 0/*RFID_STATUS_OK*/)
	        {
		        MessageBox.Show("ERROR!打开串口失败！\r\n");
	        }
            //HPC_RFID_DLL.RfidModulePowerOn();
        }

        private void RFID_Closed(object sender, EventArgs e)
        {
	        //HPC_RFID_DLL.RfidModulePowerOff();	
            if (HPC_RFID_DLL.RfidModuleClosePort() != 0/*RFID_STATUS_OK*/)
	        {
		        MessageBox.Show("ERROR!关闭串口失败！\r\n");
	        }
// 	        Buzzer.BeepClose();
        }

        private void bnVerify_Click(object sender, EventArgs e)
        {
            byte Block = 8;
            byte[] key = { 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1 };
            byte KeyModel = 0x60;
            if (HPC_RFID_DLL.ISO14443A_MF1AuthKey(Block, key, KeyModel) == 0)
            {
                MessageBox.Show("验证密钥成功！");
            }
            else
            {
                MessageBox.Show("验证密钥失败！");
            }
        }

        private void RFID_Closing(object sender, CancelEventArgs e)
        {
            //HPC_RFID_DLL.RfidModulePowerOff();	
//             if (HPC_RFID_DLL.RfidModuleClosePort() != 0/*RFID_STATUS_OK*/)
// 	        {
// 		        MessageBox.Show("ERROR!关闭串口失败！\r\n");
// 	        }
//	        Buzzer.BeepClose();
        }
    }
}