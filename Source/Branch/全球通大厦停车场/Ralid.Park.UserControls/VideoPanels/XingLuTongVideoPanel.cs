using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ralid.Park.UserControls.VideoPanels
{
    public partial class XingLuTongVideoPanel : VideoPanel
    {
        public XingLuTongVideoPanel()
        {
            InitializeComponent();
        }

        #region 私有方法
        private void axHV_OnReceiveVideo(object sender, int channel, byte[] data)
        {
            if (this.VideoSource != null && this.VideoSource.Channel == channel)
            {
                MemoryStream stream = new MemoryStream(data);
                video.Image = Image.FromStream(stream);
            }
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
                            Console.WriteLine("线程ＩＤ{0}", Thread.CurrentThread.ManagedThreadId);
                            this.Caption = VideoSource.VideoName;
                            this.TitlePanel.Visible = ShowTitle;
                        }
                        if (StretchToFit)
                        {
                            this.video.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else
                        {
                            this.video.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        FrmXinlutongContainer.GetInstance().RequestVideo(VideoSource.MediaSource,axHV_OnReceiveVideo);
                        this._Status = VideoStatus.Playing;
                    }
                }
            }
        }

        public override void Pause()
        {
            lock (_StatusLock)
            {
                this._Status = VideoStatus.Paused;
            }
        }

        public override void Close()
        {
            lock (_StatusLock)
            {
                if (VideoSource != null)
                {
                    FrmXinlutongContainer.GetInstance().CancelVideo(VideoSource.MediaSource, axHV_OnReceiveVideo);
                }
                this._Status = VideoStatus.Disconnected;
            }
            if (!this.TitlePanel.InvokeRequired)
            {
                this.TitlePanel.Visible = false;
                this.video.Image = null;
            }
        }

        public override bool SnapShotTo(string path)
        {
            return SnapShotTo(path, 1000);
        }
        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="timeout">超时时间(ms)</param>
        /// <returns></returns>
        public override bool SnapShotTo(string path, int timeout)
        {
            bool success = false;
            try
            {
                lock (_StatusLock)
                {
                    if (video.Image != null)
                    {
                        Ralid.GeneralLibrary.ImageHelper.SaveImage(video.Image, path);
                        success = true;
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