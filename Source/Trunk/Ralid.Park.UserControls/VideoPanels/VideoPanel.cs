using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls.VideoPanels
{
    public partial class VideoPanel :UserControl
    {
        public VideoPanel()
        {
            InitializeComponent();
            Init();
        }

        #region 变量
        protected VideoStatus _Status;
        protected object _StatusLock = new object();
        #endregion

        #region 公共事件
        /// <summary>
        /// 视频源拖动事件处理
        /// </summary>
        public VideoDragDropHandler VideoDragDropHandling;
        #endregion

        #region 私有方法
        private void Init()
        {
            this.AllowDrop = true;
            this.DragEnter += DragEnter_Handler;
            this.DragDrop += DragDrop_Handler;
        }

        private int count;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int penWidth = 2;

            if (count >= 0)
            {
                Graphics g = this.CreateGraphics();
                Brush b = (count % 2 == 0) ? new SolidBrush(this.BackColor) : Brushes.Red;
                Rectangle rec = new Rectangle(this.ClientRectangle.Left + penWidth,
                    this.ClientRectangle.Top + penWidth,
                    this.ClientRectangle.Width - penWidth * 2,
                    this.ClientRectangle.Height - penWidth * 2);
                g.DrawRectangle(new Pen(b, penWidth), rec);
                count--;
            }
            else
            {
                timer1.Enabled = false;
            }
        }
        #endregion

        #region 只读方法
        /// <summary>
        /// 视频服务类型
        /// </summary>
        public int VideoType
        {
            get
            {
                if (this is XingLuTongVideoPanel)
                {
                    return (int)VideoServerType.XinLuTong;
                }
                else if (this is JingYangVideoPanel)
                {
                    return (int)VideoServerType.JingYang;
                }
                else if (this is DaHuaVideoPanel)
                {
                    return (int)VideoServerType.DaHua;
                }
                //默认返回ACTi类型
                return (int)VideoServerType.ACTi;
            }
        }
        #endregion

        #region 公共方法和属性
        /// <summary>
        /// 获取或设置控件的标题
        /// </summary>
        public string Caption
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
        /// <summary>
        /// 获取或设置控件的标题颜色
        /// </summary>
        public Color CaptionColor
        {
            get
            {
                return this.label1.ForeColor;
            }
            set
            {
                this.label1.ForeColor = value;
            }
        }
        /// <summary>
        /// 视频源
        /// </summary>
        public VideoSourceInfo VideoSource { get; set; }

        /// <summary>
        /// 获取视频的状态
        /// </summary>
        public VideoStatus Status
        {
            get
            {
                return _Status;
            }
        }

        /// <summary>
        /// 是否显示标题栏
        /// </summary>
        public bool ShowTitle{get;set;}

        /// <summary>
        /// 是否拉伸画面以适应控件大小
        /// </summary>
        public bool StretchToFit { get; set; }

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            Play(false);
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="isAsync">true 表示异步播放</param>
        public virtual void Play(bool isAsync)
        {
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public virtual void Pause()
        {
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            
        }

        /// <summary>
        /// 抓拍图片到文件
        /// </summary>
        /// <param name="path">文件保存路径(包括文件名)</param>
        public virtual bool SnapShotTo(ref string path)
        {
            bool ret = false;
            return ret;
        }

        /// <summary>
        /// 抓拍图片到文件
        /// </summary>
        /// <param name="path">文件保存路径(包括文件名)</param>
        /// <param name="timeout">超时时间(ms)</param>
        /// <param name="force">是否强制重新抓拍，不管之前有没有抓拍到图片</param>
        public virtual bool SnapShotTo(ref string path, int timeout, bool force)
        {
            bool ret = false;
            return ret;
        }

        /// <summary>
        /// 清除最近保存的抓拍图片信息
        /// </summary>
        public virtual void ClearSnapShot()
        {
 
        }

        /// <summary>
        /// 突出显示
        /// </summary>
        /// <param name="count"></param>
        public virtual void HightLight(int count)
        {
            //this.count = count;
            //this.timer1.Enabled = true;
        }
        /// <summary>
        /// 打开视频用于抓拍图片,(由于某些类型的视频服务器不需要播放视频流也能抓拍，所以增加这一个方法用于与Play区分作用)
        /// </summary>
        /// <param name="async"></param>
        public virtual void OpenForSnapshot(bool _async)
        {
            Play(_async);
        }
        /// <summary>
        /// 获取视频是否已经可以用于抓拍
        /// </summary>
        public virtual bool IsReadyForSnapshot
        {
            get
            {
                return this.Status == VideoStatus.Playing;
            }
        }
        #endregion

        #region 事件处理程序
        private void DragDrop_Handler(object sender, DragEventArgs e)
        {
            string[] s = e.Data.GetFormats();
            if (s.Length > 0)
            {
                object o = e.Data.GetData(s[0]);
                if (o is VideoSourceInfo)
                {
                    VideoSourceInfo video = o as VideoSourceInfo;

                    if (video.VideoSourceType == this.VideoType)
                    {
                        //只有视频类型相同的才处理
                        this.Close();
                        this.VideoSource = video;
                        this.Play(true);
                    }
                    else if (this.VideoDragDropHandling != null)
                    {
                        this.VideoDragDropHandling(this, video);
                    }
                }
            }
        }

        private void DragEnter_Handler(object sender, DragEventArgs e)
        {
            string[] s = e.Data.GetFormats();
            if (s.Length > 0)
            {
                object o = e.Data.GetData(s[0]);
                if (o is VideoSourceInfo)
                {
                    if (e.AllowedEffect != DragDropEffects.None)
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Capture_Click(object sender, EventArgs e)
        {
            string dir = System.IO.Path.Combine(Application.StartupPath, "Capture");
            if (AppSettings.CurrentSetting != null && !string.IsNullOrEmpty(AppSettings.CurrentSetting.SnapShotSavePath))
            {
                dir = AppSettings.CurrentSetting.SnapShotSavePath;
            }
            if (!System.IO.Directory.Exists(dir))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                catch
                {
                }
            }
            string file = string.Format("{0}_{1}.jpg", this.VideoSource.VideoID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            string path = System.IO.Path.Combine(dir, file);
            this.SnapShotTo(ref path);
        }
        #endregion
    }

    /// <summary>
    /// 视频源拖动事件处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="video"></param>
    /// <returns></returns>
    public delegate void VideoDragDropHandler(object sender,VideoSourceInfo video);
}
