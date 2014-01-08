using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading ;
using System.Runtime.InteropServices;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls.VideoPanels
{
    public partial class ACTIVideoPanel : VideoPanel
    {
        #region interop
        [DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, //目的上下文设备的句柄
                    int nXDest, //目的图形的左上角的x坐标
                    int nYDest, //目的图形的左上角的y坐标
                    int nWidth, //目的图形的矩形宽度   
                    int nHeight, //目的图形的矩形高度
                    IntPtr hdcSrc, //源上下文设备的句柄
                    int nXSrc, //源图形的左上角的x坐标
                    int nYSrc, //源图形的左上角的x坐标 
                    System.Int32 dwRop //光栅操作代码
                    );
        #endregion

        #region 私有变量
        private readonly int CONNECT_SUCESS = 1;       //连接不成功
        private readonly int CONNECT_ASYNC = 1;      //异步连接
        private AutoResetEvent connectEvent = new AutoResetEvent(false);
        #endregion

        #region 构造函数
        public ACTIVideoPanel()
        {
            InitializeComponent();
            this.media.OnConnect += media_OnConnect;
        }
        #endregion

        #region 私有方法
        private void ConnectVideoSource()
        {
            media.MediaType = 0;
            media.ID = VideoSource.VideoID;
            media.Caption = VideoSource.VideoName;
            media.MediaURL = VideoSource.MediaSource;
            media.Channel = VideoSource.Channel;
            if (VideoSource.Channel > 0)
            {
                media.TCPVideoStreamID = (uint)(VideoSource.Channel - 1);
            }
            else
            {
                media.TCPVideoStreamID = 0;
            }
            media.MediaUsername = VideoSource.UserName;
            media.MediaPassword = VideoSource.Password;
            media.ControlPort = VideoSource.ControlPort;
            media.StreamingPort = VideoSource.StreamPort;
            media.AutoReconnect = VideoSource.AutoReconnect ? 1 : 0;
            media.Connect(CONNECT_ASYNC);
        }

        void media_OnConnect(object sender, AxnvEPLMediaLib._DnvEPLMediaEvents_OnConnectEvent e)
        {
            if (e.connectSuccessful == CONNECT_SUCESS)
            {
                RenderVideo();
                this._Status = VideoStatus.Playing;
            }
            else
            {
                this._Status = VideoStatus.Disconnected;
            }
            connectEvent.Set();
        }

        private void RenderVideo()
        {
            if (StretchToFit)
            {
                media.StretchToFit = 1;
            }
            else
            {
                media.StretchToFit = 0;
            }
            media.Play();
        }

        private void ACTIVideoPanel_Resize(object sender, EventArgs e)
        {
            this.media.Top = 0;
            this.media.Left = 0;
            this.media.Width = this.panel1.Width;
            this.media.Height = this.panel1.Height;
        }
        #endregion

        #region 重写基类方法
        public override void Play(bool isAsync)
        {
            lock (_StatusLock)
            {
                if (this._Status != VideoStatus.Playing)
                {
                    if (VideoSource != null)
                    {
                        if (!this.TitlePanel.InvokeRequired)
                        {
                            this.Caption = VideoSource.VideoName;
                            this.TitlePanel.Visible = ShowTitle;
                        }

                        ConnectVideoSource();
                        if (!isAsync)  //同步打开
                        {
                            connectEvent.WaitOne(VideoSource.ConnectTimeOut * 1000, false);
                        }
                    }
                }
            }
        }

        public override void Pause()
        {
            lock (_StatusLock)
            {
                if (this._Status == VideoStatus.Playing)
                {
                    media.Stop();
                    this._Status = VideoStatus.Paused;
                }
            }
        }

        public override void Close()
        {
            lock (_StatusLock)
            {
                if (this._Status == VideoStatus.Playing || this._Status == VideoStatus.Connected)
                {
                    media.Disconnect();
                }
                this._Status = VideoStatus.Disconnected;
                if (!this.TitlePanel.InvokeRequired)
                {
                    this.TitlePanel.Visible = false;
                }
            }
        }

        public override bool SnapShotTo(string path)
        {

            bool success = false;
            try
            {
                media.SnapShot(1, path, 1, 100, 100, 100);
                success = File.Exists(path);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return success;
        }
        #endregion

    }
}
