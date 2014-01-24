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
using EM1300Space;

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

            if (Ret_Value.INIT_OK != EM1300DLL.EM1300SerialInit(Seriao_Port.COM5))
            {
                MessageBox.Show("启动通信失败");
                Application.Exit();
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
                Ret_Value uiState = EM1300DLL.EM1300GetDecodeData(ReceiveBuf, CountBuf);
                if (uiState == Ret_Value.RECEIVE_SUCCESS/*RECEIVE_SUCCESS*/)
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
                    if (Ret_Value.SUCCESS_SETTING != EM1300DLL.EM1300DecodeState(true))
                    {
                        MessageBox.Show("开始扫描失败");
                    }
                    State = true;
                    resumeEvent.Set();
                }
            }
        }

        private void Scan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F20)
            {
                if (Ret_Value.SUCCESS_SETTING != EM1300DLL.EM1300DecodeState(false))
                {
                    MessageBox.Show("关闭扫描失败");
                    //return;
                }
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
            EM1300DLL.EM1300SerialTerminate();
            if(readThread != null)
                readThread.Abort();
        }
    }
}