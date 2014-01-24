using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;

namespace ReadBarcode_CSharp
{
    public partial class Scan : Form
    {
        private Thread readThread;
        volatile bool paused;
        ManualResetEvent resumeEvent;   //F20摁下的触发事件

        string ExePath,strDisp;
        int num = 0;
        Bitmap bmp_up = null;
        Bitmap bmp_down = null;
        int widgetnum = 2;
        enum Widget
        {
            bnClear = 0,
            bnExit  = 1,
            none = -1
        }
        Rectangle[] widgetrect ={
                             new Rectangle(15,265,100,40),
                             new Rectangle(125,265,100,40),
                         };
        bool[] pushdown ={
                            false,
                            false
                        };
        bool[] cursorin ={
                            false,
                            false
                        };

        private Widget WhichWidget(Point point)
        {
            for (int i = 0; i < widgetnum; i++)
            {
                if (widgetrect[i].Contains(point))
                {
                    return (Widget)i;
                }
            }
            return Widget.none;
        }
        public Scan()
        {
            InitializeComponent();

            if (Se955.Se955SerialInit(4/*com2*/, 0x06/*B_RATE9600*/, 0x01/*STOP_BITONE*/, 0x04/*DAT_NONE*/) != 50/*INIT_OK*/)
            {
                MessageBox.Show("启动通信失败");
                //bnExit_Click(null, null);
            }

            resumeEvent = new ManualResetEvent(false);
            resumeEvent.Reset();
            paused = true;
            readThread = new Thread(new ThreadStart(ReadBarcode));
            readThread.Start();

            //Pause();

            ExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            bmp_up = new Bitmap(ExePath + @"\up-ReadBarcode.bmp");
            bmp_down = new Bitmap(ExePath + @"\down-ReadBarcode.bmp");
            KeyPreview = true;
        }

        delegate void somedle();
        private void Pause()
        {
            resumeEvent.Reset();
            paused = true;
            somedle sd = new somedle(ShowBarcode);
            this.Invoke(sd);
        }
        private void ShowBarcode()
        {
            if (num == 100)
            {
                bnClear_Click(null,null);
            }
            int start = txtShow.Text.Length;
            txtShow.Text += "条形码为:" + strDisp + "\r\n";
            int end = txtShow.Text.Length;
            txtShow.Select(start, end);
            txtShow.ScrollToCaret();
            strDisp = "";
            num++;
        }

        private void ReadBarcode()
        {
            byte[] ReceiveBuf;
            byte[] CountBuf;
            ReceiveBuf = new byte[128];
            CountBuf = new byte[1];
            while (true)
            {
                if (paused)
                {
                    resumeEvent.WaitOne();
                    paused = false;
                }
                int uiState = Se955.Se955GetDecodeData(ReceiveBuf, CountBuf);
                if (uiState == 52/*RECEIVE_SUCCESS*/)
                {
                    strDisp = "";
                    Buzzer.BeepOK();
                    int count = CountBuf[0];
                    for (int i = 0; i < count; i++)
                    {
                        strDisp += (char)ReceiveBuf[i];
                    }
                    Pause();
                }
                Thread.Sleep(2);
            }
        }

        int pre = 0;
        bool State = false;
        private void Scan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F20)
            {
                if (State == false)
                {
                    int ret = Se955.Se955DecodeState(true);
                    if (ret != 47/*SUCCESS_SETTING*/ && ret != 55/*ACK_FAILED*/)
                    {
                        string str = "";
                        if (ret == 53/*DATA_ERR_SELECT*/)
                            str = "发送数据错误";
                        MessageBox.Show(str);
                        bnExit_Click(null, null);
                    }
                    resumeEvent.Set();
                    State = true;
                }
            }
        }

        private void Scan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F20)
            {
                int ret = Se955.Se955DecodeState(false);
                if (ret != 47/*SUCCESS_SETTING*/ && ret != 55/*ACK_FAILED*/)
                {
                    string str = "";
                    if (ret == 53/*DATA_ERR_SELECT*/)
                        str = "发送数据错误";
                     MessageBox.Show(str);
                     bnExit_Click(null, null);
                }
                resumeEvent.Set();
                State = false;
            }
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            txtShow.Text = "";
            num = 0;
        }

        private void bnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.DrawImage(bmp_up, new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                new Rectangle(0, 0, bmp_up.Width, bmp_up.Height), GraphicsUnit.Pixel);
        }


        private void PaintUp(Widget widget)
        {
            Graphics g = CreateGraphics();
            g.DrawImage(bmp_up, widgetrect[(int)widget], widgetrect[(int)widget], GraphicsUnit.Pixel);
        }
        private void PaintDown(Widget widget)
        {
            Graphics g = CreateGraphics();
            g.DrawImage(bmp_down, widgetrect[(int)widget], widgetrect[(int)widget], GraphicsUnit.Pixel);
        }
        private void Scan_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            Widget which = WhichWidget(point);
            if (which != Widget.none)
            {
                pushdown[(int)which] = true;
                cursorin[(int)which] = true;
                PaintDown(which);
            }
        }

        private void Scan_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < widgetnum; i++)
            {
                if (pushdown[i])
                {
                    Point point = new Point(e.X, e.Y);
                    if (widgetrect[i].Contains(point) && cursorin[i] == false)
                    {
                        cursorin[i] = true;
                        PaintDown((Widget)i);
                    }
                    else if (widgetrect[i].Contains(point) == false && cursorin[i])
                    {
                        cursorin[i] = false;
                        PaintUp((Widget)i);
                    }
                    break;
                }
            }
        }

        private void Scan_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < widgetnum; i++)
            {
                if (pushdown[i])
                {
                    Point point = new Point(e.X, e.Y);
                    if (widgetrect[i].Contains(point))
                    {
                        PaintUp((Widget)i);
                        switch ((Widget)i)
                        {
                            case Widget.bnClear: bnClear_Click(null, null); break;
                            case Widget.bnExit: bnExit_Click(null, null); break;
                        }
                        cursorin[i] = false;
                    }
                    pushdown[i] = false;
                }
            }
        }

        private void Scan_Closed(object sender, EventArgs e)
        {
            Se955.Se955SerialTerminate();
            if(readThread != null)
                readThread.Abort();
        }
    }
}