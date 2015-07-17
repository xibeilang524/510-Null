using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.UserControls;
using Ralid.Park.UI.ReportAndStatistics;

namespace Ralid.Park.UI
{
    public partial class FrmSpeedingProcess : Form
    {
        #region 构造函数
        public FrmSpeedingProcess()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private int _EventIndex;
        #endregion

        #region 公共属性
        public CustomDataGridView GridView
        {
            get
            {
                return this.dgvSpeedingRecord;
            }
        }
        #endregion

        #region 私有方法
        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            this.dgvSpeedingRecord.Rows.Clear();

            RecordSearchCondition con = new RecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CarPlate = this.txtCarPlate.Text.Trim();

            SpeedingRecordSearching(con);

            this.btnProcess.Enabled = this.dgvSpeedingRecord.Rows.Count > 0;
            this.searchInfo.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, this.dgvSpeedingRecord.Rows.Count);
        }

        private bool SpeedingRecordSearching(RecordSearchCondition con)
        {
            SpeedingRecordBll bll = new SpeedingRecordBll(AppSettings.CurrentSetting.ParkConnect);
            QueryResultList<SpeedingRecord> result = bll.GetRecords(con);
            if (result.Result == ResultCode.Successful)
            {
                List<SpeedingRecord> items = result.QueryObjects;
                foreach (SpeedingRecord item in items)
                {
                    int row = this.dgvSpeedingRecord.Rows.Add();
                    ShowSpeedingRecordOnRow(this.dgvSpeedingRecord.Rows[row], item);
                }                
                return true;
            }
            else
            {
                MessageBox.Show(result.Message);
            }
            return false;
        }

        private void ShowSpeedingRecordOnRow(DataGridViewRow row, SpeedingRecord info)
        {
            row.Tag = info;
            row.Cells["colSpeedingDateTime"].Value = info.SpeedingDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colPlateNumber"].Value = info.PlateNumber;
            row.Cells["colPlace"].Value = info.Place;
            row.Cells["colSpeed"].Value = info.Speed;
            row.Cells["colMemo"].Value = info.Memo;
        }

        private int SpeedingProcess()
        {
            int processCount = 0;
            CommandResult result = null;
            OperatorInfo processOperator = OperatorInfo.CurrentOperator;
            string processMemo = this.txtDealMemo.Text.Trim();

            SpeedingRecordBll bll = new SpeedingRecordBll(AppSettings.CurrentSetting.ParkConnect);
            DataGridViewRow[] rows = new DataGridViewRow[this.dgvSpeedingRecord.Rows.Count];
            this.dgvSpeedingRecord.Rows.CopyTo(rows, 0);
            foreach (DataGridViewRow row in rows)
            {
                SpeedingRecord record = row.Tag as SpeedingRecord;
                if (record != null)
                {
                    result = bll.SpeedingProcessing(record, processOperator, DateTime.Now, processMemo);
                    if (result.Result == ResultCode.Successful)
                    {
                        processCount++;
                        this.dgvSpeedingRecord.Rows.Remove(row);
                    }
                }
            }
            return processCount;
        }
        #endregion

        #region 私有事件
        private void frm_NextRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex < this.GridView.Rows.Count - 1)
            {
                this.GridView.Rows[_EventIndex].Selected = false;
                _EventIndex++;
                this.GridView.Rows[_EventIndex].Selected = true;
                if (_EventIndex > this.GridView.FirstDisplayedScrollingRowIndex + this.GridView.DisplayedRowCount(false) - 1)
                {
                    this.GridView.FirstDisplayedScrollingRowIndex += this.GridView.DisplayedRowCount(false);
                }
                string path = string.Empty;
                if (this.GridView.Rows[_EventIndex].Tag is SpeedingRecord)
                {
                    SpeedingRecord info = this.GridView.Rows[_EventIndex].Tag as SpeedingRecord;
                    path = info.Photo;
                }
                else if (this.GridView.Rows[_EventIndex].Tag is SpeedingLog)
                {
                    SpeedingLog info = this.GridView.Rows[_EventIndex].Tag as SpeedingLog;
                    path = info.Photo;
                }
                FrmPhotoViewer frm = sender as FrmPhotoViewer;
                frm.ShowImage(path);
            }
            e.IsButtonRecord = (_EventIndex == GridView.Rows.Count - 1);
        }

        private void frm_PreRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex > 0)
            {
                this.GridView.Rows[_EventIndex].Selected = false;
                _EventIndex--;
                this.GridView.Rows[_EventIndex].Selected = true;
                if (_EventIndex < this.GridView.FirstDisplayedScrollingRowIndex)
                {
                    if (this.GridView.FirstDisplayedScrollingRowIndex >= this.GridView.DisplayedRowCount(false))
                    {
                        this.GridView.FirstDisplayedScrollingRowIndex -= this.GridView.DisplayedRowCount(false);
                    }
                    else
                    {
                        this.GridView.FirstDisplayedScrollingRowIndex = 0;
                    }
                }
                string path = string.Empty;
                if (this.GridView.Rows[_EventIndex].Tag is SpeedingRecord)
                {
                    SpeedingRecord info = this.GridView.Rows[_EventIndex].Tag as SpeedingRecord;
                    path = info.Photo;
                }
                else if (this.GridView.Rows[_EventIndex].Tag is SpeedingLog)
                {
                    SpeedingLog info = this.GridView.Rows[_EventIndex].Tag as SpeedingLog;
                    path = info.Photo;
                }
                FrmPhotoViewer frm = sender as FrmPhotoViewer;
                frm.ShowImage(path);
            }
            e.IsTopRecord = (_EventIndex == 0);
        }
        #endregion

        #region 窗体事件
        private void FrmSpeedingProcess_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.btnProcess.Enabled = false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ItemSearching_Handler(sender, e);
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (this.dgvSpeedingRecord.Rows.Count > 0)
            {
                int processCount = SpeedingProcess();
                this.btnProcess.Enabled = this.dgvSpeedingRecord.Rows.Count > 0;
                if (!this.btnProcess.Enabled)
                {
                    this.txtDealMemo.Text = string.Empty;
                }
                MessageBox.Show(string.Format(Resources.Resource1.FrmSpeedingProcess_ProcessedCount, processCount));
            }
            else
            {
                MessageBox.Show(Resources.Resource1.FrmSpeedingProcess_NotRecords);
            }
        }
        private void dgvSpeedingRecord_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex >= 0)
            {
                _EventIndex = e.RowIndex;
                string path = string.Empty;
                if (this.GridView.Rows[e.RowIndex].Tag is SpeedingRecord)
                {
                    SpeedingRecord info = this.GridView.Rows[e.RowIndex].Tag as SpeedingRecord;
                    path = info.Photo;
                }
                else if (this.GridView.Rows[e.RowIndex].Tag is SpeedingLog)
                {
                    SpeedingLog info = this.GridView.Rows[e.RowIndex].Tag as SpeedingLog;
                    path = info.Photo;
                }

                FrmPhotoViewer frm = new FrmPhotoViewer();
                frm.PreRecord += new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord += new RecordPositionEventHandler(frm_NextRecord);
                frm.ShowImage(path);
                frm.ShowDialog();
                frm.PreRecord -= new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord -= new RecordPositionEventHandler(frm_NextRecord);
            }
        }
        #endregion

        

    }
}
