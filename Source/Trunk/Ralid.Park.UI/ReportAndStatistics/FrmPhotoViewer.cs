using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmPhotoViewer : Form
    {
        #region 构造函数
        public FrmPhotoViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region 公共事件
        public event RecordPositionEventHandler PreRecord;
        public event RecordPositionEventHandler NextRecord;
        #endregion

        #region 窗体事件
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowImage(string path)
        {
            this.pbPhoto.ImageLocation = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                //this.pbPhoto.ImageLocation = path;
                this.pbPhoto.LoadAsync(path);
            }
            else
            {
                this.pbPhoto.Image = Properties.Resources.NoImage;
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (this.PreRecord != null)
            {
                RecordPositionEventArgs args = new RecordPositionEventArgs();
                PreRecord(this, args);
                btnPre.Enabled = !args.IsTopRecord;
                btnNext.Enabled = !args.IsButtonRecord;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.NextRecord != null)
            {
                RecordPositionEventArgs args = new RecordPositionEventArgs();
                NextRecord(this, args);
                btnPre.Enabled = !args.IsTopRecord;
                btnNext.Enabled = !args.IsButtonRecord;
            }
        }

        //重写处理命令键方法，获取方向键
        protected override bool ProcessCmdKey(ref Message msg, Keys charCode)
        {
            if (charCode == Keys.Left || charCode == Keys.Up)
            {
                btnPre_Click(this.btnPre, EventArgs.Empty);
            }
            else if (charCode == Keys.Right || charCode == Keys.Down)
            {
                btnNext_Click(btnNext, EventArgs.Empty);
            }
            return base.ProcessCmdKey(ref msg, charCode);
        }
        #endregion
    }
}
