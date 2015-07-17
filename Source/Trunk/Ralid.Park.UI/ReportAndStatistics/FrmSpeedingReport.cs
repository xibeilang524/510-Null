using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmSpeedingReport : Ralid.Park.UI.ReportAndStatistics.FrmReportBase
    {
        #region 构造函数
        public FrmSpeedingReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }
        #endregion

        #region 私有变量
        private int _EventIndex;
        #endregion

        #region 公共属性
        public override CustomDataGridView GridView
        {
            get
            {
                if (this.tabControl1.SelectedTab == this.tpSpeedingLog)
                {
                    return this.dgvSpeedingLog;
                }
                else
                {
                    return this.dgvSpeedingRecord;
                }

            }
        }
        public override List<CustomDataGridView> GridViews
        {
            get
            {
                List<CustomDataGridView> views = new List<CustomDataGridView>();
                views.Add(this.dgvSpeedingLog);
                views.Add(this.dgvSpeedingRecord);
                return views;
            }
        }
        #endregion

        #region 私有方法
        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            this.dgvSpeedingRecord.Rows.Clear();
            this.dgvSpeedingLog.Rows.Clear();

            RecordSearchCondition con = new RecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CarPlate = this.txtCarPlate.Text.Trim();

            SpeedingRecordSearching(con);

            SpeedingLogSearching(con);
            
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

        private bool SpeedingLogSearching(RecordSearchCondition con)
        {
            SpeedingLogBll bll = new SpeedingLogBll(AppSettings.CurrentSetting.ParkConnect);
            QueryResultList<SpeedingLog> result = bll.GetRecords(con);
            if (result.Result == ResultCode.Successful)
            {
                List<SpeedingLog> items = result.QueryObjects;
                foreach (SpeedingLog item in items)
                {
                    int row = this.dgvSpeedingLog.Rows.Add();
                    ShowSpeedingLogOnRow(this.dgvSpeedingLog.Rows[row], item);
                }
                return true;
            }
            else
            {
                MessageBox.Show(result.Message);
            }
            return false;
        }

        private void ShowSpeedingLogOnRow(DataGridViewRow row, SpeedingLog info)
        {
            row.Tag = info;
            row.Cells["colLSpeedingDateTime"].Value = info.SpeedingDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colLPlateNumber"].Value = info.PlateNumber;
            row.Cells["colLPlace"].Value = info.Place;
            row.Cells["colLSpeed"].Value = info.Speed;
            row.Cells["colLMemo"].Value = info.Memo;
            row.Cells["colLDealDateTime"].Value = info.DealDateTime.HasValue ? info.DealDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            row.Cells["colLDealOperator"].Value = info.DealOperator != null ? info.DealOperator.OperatorName : info.DealOperatorID;
            row.Cells["colLDealMemo"].Value = info.DealMemo;
        }
        #endregion

        #region 私有事件
        private void FrmSpeedingReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
        }
        private void customDataGridview1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                else if(this.GridView.Rows[e.RowIndex].Tag is SpeedingLog)
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


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            this.searchInfo.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, this.GridView.Rows.Count);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            SpeedingRecord info = new SpeedingRecord();
            info.SpeedingID = Guid.NewGuid();
            info.SpeedingDateTime = DateTime.Now;
            info.PlateNumber = "粤A123ab";
            info.Place = "A区监测点";
            info.Speed = "100";
            info.Photo = @"http://wenwen.soso.com/p/20101016/20101016200301-1077351978.jpg";
            info.Memo = "逆行";


            SpeedingRecord info1 = new SpeedingRecord();
            info1.SpeedingID = Guid.NewGuid();
            info1.SpeedingDateTime = DateTime.Now;
            info1.PlateNumber = "粤A456ab";
            info1.Place = "B区测速点";
            info1.Speed = "150";
            info1.Photo = @"http://img2.duitang.com/uploads/item/201209/20/20120920165508_EuenZ.jpeg";
            info1.Memo = "超速";

            SpeedingRecordBll bll = new SpeedingRecordBll(AppSettings.CurrentSetting.ParkConnect);
            CommandResult result = bll.Insert(info);
            if (result.Result != ResultCode.Successful)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                MessageBox.Show(result.Result.ToString()); 
            }
            result = bll.Insert(info1);
            if (result.Result != ResultCode.Successful)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                MessageBox.Show(result.Result.ToString());
            }
        }

    }
}
