using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ZSTLib;
using System.Threading;

namespace zhongshantong
{
    public partial class Form1 : Form
    {
        
        

        IR50 zstdll;
        public string IPString;
        public Boolean AutoFlag;
        System.Threading.Thread AutoOpreate;
       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Thread.CurrentThread.IsBackground = true;
            zstdll = new R50();
            IPString = "";
            AutoFlag = false;
        }

        private string InputBox(string Caption, string Hint, string Default)
        {

            Form InputForm = new Form();

            InputForm.MinimizeBox = false;

            InputForm.MaximizeBox = false;

            InputForm.StartPosition = FormStartPosition.CenterScreen;

            InputForm.Width = 220;

            InputForm.Height = 150;

            //InputForm.Font.Name = "宋体";

            //InputForm.Font.Size = 10;



            InputForm.Text = Caption;

            Label lbl = new Label();

            lbl.Text = Hint;

            lbl.Left = 10;

            lbl.Top = 20;

            lbl.Parent = InputForm;

            lbl.AutoSize = true;

            TextBox tb = new TextBox();

            tb.Left = 30;

            tb.Top = 45;

            tb.Width = 160;

            tb.Parent = InputForm;

            tb.Text = Default;

            tb.SelectAll();

            Button btnok = new Button();

            btnok.Left = 30;

            btnok.Top = 80;

            btnok.Parent = InputForm;

            btnok.Text = "确定";

            InputForm.AcceptButton = btnok;//回车响应



            btnok.DialogResult = DialogResult.OK;

            Button btncancal = new Button();

            btncancal.Left = 120;

            btncancal.Top = 80;

            btncancal.Parent = InputForm;

            btncancal.Text = "取消";

            btncancal.DialogResult = DialogResult.Cancel;

            try
            {

                if (InputForm.ShowDialog() == DialogResult.OK)
                {

                    return tb.Text;

                }

                else
                {

                    return null;

                }

            }

            finally
            {

                InputForm.Dispose();

            }



        }
        public void UpdateCur()
        {
            // this.textBox1.Focus();//获取焦点
            this.textBox1.Select(this.textBox1.TextLength, 0);//光标定位到文本最后
            this.textBox1.ScrollToCaret();//滚动到光标处            
        }



        delegate void SetTextCallback(string text);//后加的，好好想一想,参数是SetText带的参数。

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.AppendText(text);
                this.textBox1.Refresh();
            }
        }




        public void AppendText(string Text)
        {
            
            
            textBox1.Text += Text;
            textBox1.Text += "\r\n";
            textBox1.ScrollToCaret();
            UpdateCur();
        }

        public void AppendText(object sender, string Text)
        {

            textBox1.Text += ((Button)sender).Text;
            textBox1.Text += ":";
            textBox1.Text += Text;
            textBox1.Text += "\r\n";
            textBox1.ScrollToCaret();
            UpdateCur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int InitValue = zstdll.init();
            AppendText(sender, InitValue.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tIPS;
            tIPS = zstdll.getServerInfo();
            // IPString = tIPS.Substring(0, 3);
            if (string.Equals("ip :", tIPS.Substring(0,4)))
            {
                IPString = tIPS.Substring(5, tIPS.Length - 7);
            }
            AppendText(sender, zstdll.getServerInfo());
        }

        private void ClearText_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tString = "";
            tString = zstdll.getMessage();
            AppendText(sender, tString);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string TimerField = "";
            TimerField = InputBox("设置上传时间", "上传时间段, 最多8组", "{1:0800,2:0900}");
            if (TimerField.Length != 0)
            {
                AppendText(sender,  zstdll.setUpdateTimer(IPString, TimerField));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string tFiles = "";
            tFiles =zstdll.upLoadFile(IPString);
            AppendText(sender, tFiles);
        }





        private void button5_Click(object sender, EventArgs e)
        {
            string TimerField = "";
            TimerField = InputBox("设置下载时间", "下载时间段, 最多8组", "{1:0800,2:0900}");
            if (TimerField.Length != 0)
            {
                AppendText(sender, zstdll.setDownloadTimer(IPString, TimerField));
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string tFiles = "";
            tFiles = zstdll.downLoadFile(IPString);
            AppendText(sender, tFiles);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string tString = "";
            tString = zstdll.messageConfirm(IPString);
            AppendText(sender, tString);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string TimerField = "";
            TimerField = InputBox("消费", "设置消费金额,单位(分)", "01");
            if (TimerField != null)
            {
                AppendText(sender, zstdll.consumption(IPString, TimerField));
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string TimerField = "";
            TimerField = InputBox("消费统计", "开始统计时间", "00");
            if (TimerField != null)
            {
                AppendText(sender, zstdll.comsumptionReport(IPString, TimerField));
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AutoFlag = !AutoFlag;
            if (AutoFlag)
            {
                ((Button)sender).Text = "停止操作";
                AutoOpreate = new System.Threading.Thread(new System.Threading.ThreadStart(AutoOpreatePro));
                AutoOpreate.Start();
            }
            else
            {
                ((Button)sender).Text = "自动操作";
                
            }
            
        }
        void AutoOpreatePro()
        {
            string tString = "";
            string tSubString = "";
            string OprIP = "";
            string OprCardID = "";
            string OprBalance = "";
            string OprTime = "";
            string OprTerminal = "";
            Boolean InOutType  = true;
             //   "type:1\r\nip:192.168.16.153\r\n卡号:0000000000000000000\r\n余额: 0.00元\r\n交易时间:2013-01-31 16:16:37\r\n终端号:32345678\r\n卡类型代码:00\r\n"
            while (AutoFlag)
            {
                tString = zstdll.getMessage();
                if( tString != "-1")
                // if( !string.Equals( "-1", tString.Substring(0,2) ) )
                {                    
                    SetText("\r\n取得设备消息:" + tString);
                    tSubString = tString.Substring(5, 1);
                    switch (tSubString)
                    {
                        case "1":
                            if (InOutType)
                            {
                                // 入口
                                zstdll.messageConfirm(IPString);
                                InOutType = !InOutType;
                            }
                            else
                            {
                                // 出口扣费0.1元
                                SetText(zstdll.consumption(IPString, "0010"));
                                InOutType = !InOutType;
                            }                            
                            break;
                        case "2":
                            SetText("扣费成功\r\n");
                            break;
                        case "3":
                            SetText("扣费失败\r\n");
                            break;
                        default:
                            break;
                        
                    }
                }
                
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
