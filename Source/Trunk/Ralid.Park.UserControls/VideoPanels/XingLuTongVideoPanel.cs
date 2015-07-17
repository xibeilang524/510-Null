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
using Ralid.Park.VideoCapture;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls.VideoPanels
{
    public partial class XingLuTongVideoPanel : VideoPanel
    {
        public XingLuTongVideoPanel()
        {
            InitializeComponent();
        }

        #region 私有变量
        /// <summary>
        /// 是否不打开视频时抓拍
        /// </summary>
        private bool _IsReadyForSnapshot;
        #endregion

        #region 私有方法
        private void axHV_OnReceiveVideo(object sender, int channel, byte[] data)
        {
            try
            {
                if (this.VideoSource != null && this.VideoSource.Channel == channel)
                {
                    using (MemoryStream stream = new MemoryStream(data))
                    {
                        video.Image = Image.FromStream(stream);
                    }
                }
            }
            catch
            { 
            }
        }
        #endregion

        #region 重写基类方法
        public override bool IsReadyForSnapshot
        {
            get
            {
                return _IsReadyForSnapshot;
            }
        }

        public override void OpenForSnapshot(bool _async)
        {
            _IsReadyForSnapshot = true;
        }

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
                //lock (_StatusLock)
                //{
                //    if (video.Image != null)
                //    {
                //        Ralid.GeneralLibrary.ImageHelper.SaveImage(video.Image, path);
                //        success = true;
                //    }
                //}

                if (_IsReadyForSnapshot)
                {
                    //不打开视频时抓拍
                    IVideoCapture capture = VideoCaptureManager.Instance[(int)VideoServerType.XinLuTong];
                    if (capture != null)
                    {
                        path = capture.CapturePicture(this.VideoSource,force);
                        success = !string.IsNullOrEmpty(path);
                    }
                }
                else
                {
                    //打开视频时抓拍
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

        public override void ClearSnapShot()
        {
            try
            {
                IVideoCapture capture = VideoCaptureManager.Instance[(int)VideoServerType.XinLuTong];
                if (capture != null)
                {
                    capture.ClearCapture(this.VideoSource);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion
    }
}