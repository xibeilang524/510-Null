using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.GeneralLibrary;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmSnapShotViewer : Form
    {
        public event RecordPositionEventHandler PreRecord;
        public event RecordPositionEventHandler NextRecord;
        private string folder = string.Empty;

        public FrmSnapShotViewer()
        {
            InitializeComponent();
        }

        private void FrmCardEventImage_Load(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowImage(DateTime snapshotDatetime,string cardID)
        {
            plEvent.Clear();
            plLastEvent.Clear();
            this.Text = string.Format("{0}", snapshotDatetime.ToString("yyyy-MM-dd HH:mm:ss"));
            List<SnapShot> shots = (new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).GetSnapShots(snapshotDatetime,cardID);
            if (shots != null && shots.Count > 0)
            {
                plEvent.ShowSnapShots(shots);
            }
        }

        public void ShowImage(DateTime eventDateTime, DateTime lastEventDateTime, string cardID)
        {
            plEvent.Clear();
            plLastEvent.Clear();
            this.Text = string.Format("{0}", eventDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            List<SnapShot> items = (new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).GetSnapShots(eventDateTime, cardID);
            if (items != null && items.Count > 0)
            {
                plEvent.ShowSnapShots(items);
            }

            List<SnapShot> items1 = (new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).GetSnapShots(lastEventDateTime, cardID);
            if (items1 != null && items1.Count > 0)
            {
                plLastEvent.ShowSnapShots(items1);
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(folder))
                {
                    folderBrowserDialog1.SelectedPath = folder;
                }
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    folder = folderBrowserDialog1.SelectedPath;
                    int count = 0;
                    count += plEvent.ExportImagesTo(folder);
                    count += plLastEvent.ExportImagesTo(folder);
                    if (count > 0)
                    {
                        if (MessageBox.Show(Resources.Resource1.FrmSnapshotViewer_ToFolderQuery, Resources.Resource1.Form_Query, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(folder);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class RecordPositionEventArgs : EventArgs
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置当前记录是否是最顶上一条记录
        /// </summary>
        public bool IsTopRecord { get; set; }
        /// <summary>
        /// 获取或设置当前记录是否是最底下一条记录
        /// </summary>
        public bool IsButtonRecord { get; set; }
        #endregion
    }

    public delegate void RecordPositionEventHandler(object sender, RecordPositionEventArgs e);
}
