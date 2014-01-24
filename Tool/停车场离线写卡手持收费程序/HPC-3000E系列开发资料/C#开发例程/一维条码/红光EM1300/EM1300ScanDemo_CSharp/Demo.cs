using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EM1300Space;

namespace EM1300ScanDemo_CSharp
{
    public partial class Demo : Form
    {
        Trigger_State[] pattern = { 
                Trigger_State.TS_INTERVAL, 
                Trigger_State.TS_PERCEIVE, 
                Trigger_State.TS_CONTINUE, 
                Trigger_State.TS_LAZYPERCEIVE, 
                Trigger_State.TS_SINGLE
        };
        string[] pattername = { 
                "间隔扫描",
                "感应扫描", 
                "连续扫描", 
                "延迟感应扫描", 
                "单次扫描" 
        };
        int exitThreadEvent;
        Thread readThread;
        string strDisp;
        public Demo()
        {
            InitializeComponent();
            foreach (string s in pattername)
            {
                cbPattern.Items.Add(s);
            }
            cbPattern.SelectedIndex = 4;
            if (Ret_Value.INIT_OK != EM1300DLL.EM1300SerialInit(Seriao_Port.COM2))
            {
                MessageBox.Show("启动通信失败");
                Application.Exit();
            }
            exitThreadEvent = EM1300DLL.CreateEvent(0,1,0,0);
            readThread = new Thread(new ThreadStart(ReadBarcode));
            readThread.Start();
            KeyPreview = true;
        }

        delegate void somedle();
        private void ReadBarcode()
        {
            byte[] ReceiveBuf;
            byte[] CountBuf;
            ReceiveBuf = new byte[128];
            CountBuf = new byte[1];
            while (true)
            {
                if (0/*WAIT_OBJECT_0*/==EM1300DLL.WaitForSingleObject(exitThreadEvent,0))
                {
                    break;
                }
                Ret_Value uiState = EM1300DLL.EM1300GetDecodeData(ReceiveBuf, CountBuf);
                if (uiState == Ret_Value.RECEIVE_SUCCESS)
                {
                    strDisp = "";
                    //Buzzer.BeepOK();
                    int count = CountBuf[0];
                    for (int i = 0; i < count; i++)
                    {
                        strDisp += (char)ReceiveBuf[i];
                    }
                    somedle sd = new somedle(ShowBarcode);
                    this.Invoke(sd);
                }
                Thread.Sleep(2);
            }
        }
        int num = 0;
        private void ShowBarcode()
        {
            if (num == 100)
            {
                bnClear_Click(null, null);
                num = 0;
            }
            int start = txtShow.Text.Length;
            txtShow.Text += "条形码为:" + strDisp + "\r\n";
            int end = txtShow.Text.Length;
            txtShow.Select(start, end);
            txtShow.ScrollToCaret();
            strDisp = "";
            num++;
        }

        private void bnSetPattern_Click(object sender, EventArgs e)
        {
            if (Ret_Value.SUCCESS_SETTING != EM1300DLL.EM1300TriggerState(pattern[cbPattern.SelectedIndex]))
            {
                MessageBox.Show("设置触发方式失败");
                return;
            }
            bnStart.Enabled = cbPattern.SelectedIndex == 4;
            bnStop.Enabled = cbPattern.SelectedIndex == 4;
        }

        private void bnStart_Click(object sender, EventArgs e)
        {
            if (Ret_Value.SUCCESS_SETTING != EM1300DLL.EM1300DecodeState(true))
            {
                MessageBox.Show("开始扫描失败");
            }
        }

        private void bnStop_Click(object sender, EventArgs e)
        {
            if (Ret_Value.SUCCESS_SETTING != EM1300DLL.EM1300DecodeState(false))
            {
                MessageBox.Show("停止扫描失败");
            }
        }

        private void bnVersion_Click(object sender, EventArgs e)
        {
	        uint ulState = EM1300DLL.EM1300GetVersion();
	        string s = String.Format("软件版本:V{0:F2}!",ulState*1.0/100);
            MessageBox.Show(s);
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            txtShow.Text = "";
        }

        private void Demo_Closing(object sender, CancelEventArgs e)
        {
            EM1300DLL.SetEvent(exitThreadEvent);
            readThread.Join();
            EM1300DLL.CloseHandle(exitThreadEvent);
        }

        int pre = 0;
        private void Demo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.F20)
            {
                int now = System.Environment.TickCount;
                if(now-pre>350)
                {
                    bnStart_Click(null,null);
                    pre = now;
                }
            }
        }
    }
}