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
        public virtual bool SnapShotTo(string path)
        {
            bool ret = false;
            return ret;
        }

        /// <summary>
        /// 抓拍图片到文件
        /// </summary>
        /// <param name="path">文件保存路径(包括文件名)</param>
        public virtual bool SnapShotTo(string path, int timeout)
        {
            bool ret = false;
            return ret;
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
                    this.Close();
                    this.VideoSource = o as VideoSourceInfo;
                    this.Play(true);
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
            string dir = System.IO.Path.Combine(Application.StartupPath, "Capture") ;
            if (AppSettings.CurrentSetting != null && !string.IsNullOrEmpty(AppSettings.CurrentSetting.SnapShotSavePath))
            {
                dir = AppSettings.CurrentSetting.SnapShotSavePath;
            }
            string file = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            this.SnapShotTo(System.IO.Path.Combine(dir, file));
        }
        #endregion
    }
}
