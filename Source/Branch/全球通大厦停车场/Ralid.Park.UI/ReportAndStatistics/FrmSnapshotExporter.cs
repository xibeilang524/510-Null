using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model ;
using Ralid.Park .BLL ;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmSnapshotExporter : Form
    {
        public FrmSnapshotExporter()
        {
            InitializeComponent();
        }

        #region 私有变量
        private Thread _ExportThread;
        #endregion

        #region 私有方法
        private void ExportSnaphost_Thread()
        {
            try
            {
                SnapShotBll ssb = new SnapShotBll(Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.ParkConnect);
                foreach (CardEventRecord record in CardEvents)
                {
                    List<SnapShot> shots = ssb.GetSnapShots(record.EventDateTime,null);
                    if (shots != null && shots.Count > 0)
                    {
                        for (int i = 0; i < shots.Count; i++)
                        {
                            string f = string.Format("{0}_{1}_{2}.jpg", record.EntranceName, record.EventDateTime.ToString("yyyyMMddHHmmss"), shots[i].VideoSourceID.ToString("D2"));
                            shots[i].Image.Save(System.IO.Path.Combine(ExportFolder, f));
                        }
                    }
                    Action action = delegate()
                    {
                        this.progressBar1.Value++;
                        this.label1.Text = string.Format(Resources.Resource1.FrmSnapshotExport_Processing, progressBar1.Value, progressBar1.Maximum);
                        if (this.progressBar1.Value == this.progressBar1.Maximum)
                        {
                            this.Close();
                        }
                    };
                    if (this.InvokeRequired)
                    {
                        this.Invoke(action);
                    }
                    else
                    {
                        action();
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }
        #endregion

        #region 公共属性
        public List<CardEventRecord> CardEvents { get; set; }
        public string ExportFolder { get; set; }
        #endregion

        #region 事件处理程序
        private void FrmSnapshotExporter_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ExportFolder))
            {
                MessageBox.Show(Resources.Resource1.FrmSnapshotExport_NoFolder);
                return;
            }
            if (CardEvents != null && CardEvents.Count > 0)
            {
                progressBar1.Maximum = CardEvents.Count;
                _ExportThread = new Thread(ExportSnaphost_Thread);
                _ExportThread.IsBackground = true;
                _ExportThread.Start();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_ExportThread != null)
            {
                _ExportThread.Abort();
                this.Close();
            }
        }
        #endregion
    }
}
