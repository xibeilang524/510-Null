using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net ;
using System.Net .NetworkInformation ;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls.VideoPanels
{
    public partial class ACTIVideoControl : VideoPanel
    {
        public ACTIVideoControl()
        {
            InitializeComponent();
            this.media.OnConnect += media_OnConnect;
            this.media.OnNetworkLoss += media_OnNetworkLoss;
        }

        #region 私有变量
        private readonly int CONNECT_SUCESS = 1;       //连接不成功
        private readonly int CONNECT_ASYNC = 1;      //异步连接
        private AutoResetEvent connectEvent = new AutoResetEvent(false);
        private bool _SnapSuccess = false;
        private Ping _Ping;
        #endregion

        #region 私有方法
        private void ConnectVideoSource()
        {
            media.ID = VideoSource.VideoID;
            media.StreamType = 0;
            media.MediaType = 0;
            media.DeviceType = 0;	//single=0 , quad =1
            media.Caption = VideoSource.VideoName;
            media.MediaSource = VideoSource.MediaSource;
            media.TCPVideoStreamID = (uint)VideoSource.Channel;
            media.MediaUsername = VideoSource.UserName;
            media.MediaPassword = VideoSource.Password;
            media.ControlPort = VideoSource.ControlPort;
            media.StreamingPort = VideoSource.StreamPort;
            media.AutoReconnect = VideoSource.AutoReconnect ? 1 : 0;
            media.Connect(CONNECT_ASYNC);
            media.Visible = true;
        }

        private void media_OnConnect(object sender, AxnvUnifiedControlLib._DnvUnifiedControlEvents_OnConnectEvent e)
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

        private void media_OnNetworkLoss(object sender, AxnvUnifiedControlLib._DnvUnifiedControlEvents_OnNetworkLossEvent e)
        {
            this._Status = VideoStatus.Disconnected;
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
            //media.Visible = true;
            //media.Refresh();
            media.StartStream();
            media.Play();
        }

        private void ACTIVideoPanel_Resize(object sender, EventArgs e)
        {
            this.media.Top = 0;
            this.media.Left = 0;
            this.media.Width = this.panel1.Width;
            this.media.Height = this.panel1.Height;
        }

        /// <summary>
        /// 查看设置是否能ping通
        /// </summary>
        /// <returns></returns>
        private bool Pingable()
        {
            try
            {
                _Ping = new Ping();
                IPAddress ip;
                if (IPAddress.TryParse(VideoSource.MediaSource, out ip))
                {
                    PingReply reply = _Ping.Send(ip, 200);  //超时间设置成200毫秒
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
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
                        if (Pingable()) //连接之前先ping，如果ping不通，则不再尝试连接硬件
                        {
                            ConnectVideoSource();
                            if (!isAsync)  //同步打开，等待状态变化,连接超时固定为3S
                            {
                                connectEvent.WaitOne(VideoSource.ConnectTimeOut * 1000, false);
                            }
                            this._Status = VideoStatus.Playing;
                        }
                    }
                }
            }
        }

        public override void Pause()
        {
            lock (_StatusLock)
            {
                this._Status = VideoStatus.Paused;
                media.Stop();
            }
        }

        public override void Close()
        {
            lock (_StatusLock)
            {
                this._Status = VideoStatus.Disconnected;
                media.Stop();
                media.Disconnect();
                media.Visible = false;
            }
            if (!this.TitlePanel.InvokeRequired)
            {
                this.TitlePanel.Visible = false;
            }
        }

        public override bool SnapShotTo(ref string path)
        {
            return SnapShotTo(ref path, 1000, false);
        }
        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="timeout">超时时间(ms)</param>
        /// <returns></returns>
        public override bool SnapShotTo(ref string path, int timeout, bool force)
        {
            bool success = false;
            try
            {
                lock (_StatusLock)
                {
                        media.SnapShot(1, path, 1, 100, 100, 100);
                        DateTime dtBegin = DateTime.Now;

                        Thread.Sleep(200);
                        while ((new TimeSpan(DateTime.Now.Ticks - dtBegin.Ticks).TotalMilliseconds < timeout))
                        {
                            if (File.Exists(path))
                            {
                                success = true;
                                Thread.Sleep(50);
                                break;
                            }
                            Thread.Sleep(200);
                        }
                    }
                
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
