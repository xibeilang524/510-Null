using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmAlarmReport : FrmReportBase
    {
        #region 构造函数
        public FrmAlarmReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }

        private void FrmAlarmReport_Load(object sender, EventArgs e)
        {
            this.entranceComboBox1.Init();
            this.ucDateTimeInterval1.Init();
            this.alarmTypeComboBox1.Init();
            this.operatorCombobox1.Init();
        }
        #endregion

        #region 私有变量
        private int _EventIndex;
        #endregion

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            this.customDataGridview1.Rows.Clear();
            AlarmSearchCondition con = new AlarmSearchCondition();
            con.AlarmSource = this.entranceComboBox1.SelectedEntranceName;
            if (Enum.IsDefined(typeof(AlarmType), this.alarmTypeComboBox1.SelectedAlarmType))
            {
                con.AlarmType = this.alarmTypeComboBox1.SelectedAlarmType;
            }
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.Operator = this.operatorCombobox1.SelectecOperator;
            AlarmBll bll = new AlarmBll(Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.ParkConnect);
            QueryResultList<AlarmInfo> result = bll.GetAlarms(con);
            if (result.Result == ResultCode.Successful)
            {
                List<AlarmInfo> items = (from alarm in result.QueryObjects
                                         orderby alarm.AlarmDateTime descending
                                         select alarm).ToList();
                foreach (AlarmInfo alarm in items)
                {
                    int row = this.customDataGridview1.Rows.Add();
                    ShowAlarmOnRow(this.customDataGridview1.Rows[row], alarm);
                }
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void customDataGridview1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex >= 0)
            {
                _EventIndex = e.RowIndex;
                AlarmInfo info = this.GridView.Rows[e.RowIndex].Tag as AlarmInfo;
                FrmSnapShotViewer frm = new FrmSnapShotViewer();
                frm.PreRecord += new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord += new RecordPositionEventHandler(frm_NextRecord);
                frm.ShowImage(info.AlarmDateTime, string.Empty);
                frm.ShowDialog();
                frm.PreRecord -= new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord -= new RecordPositionEventHandler(frm_NextRecord);
            }
        }

        private void frm_NextRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex < customDataGridview1.Rows.Count - 1)
            {
                this.customDataGridview1.Rows[_EventIndex].Selected = false;
                _EventIndex++;
                this.customDataGridview1.Rows[_EventIndex].Selected = true;
                if (_EventIndex > this.customDataGridview1.FirstDisplayedScrollingRowIndex + this.customDataGridview1.DisplayedRowCount(false) - 1)
                {
                    this.customDataGridview1.FirstDisplayedScrollingRowIndex += this.customDataGridview1.DisplayedRowCount(false);
                }
                AlarmInfo info = this.GridView.Rows[_EventIndex].Tag as AlarmInfo;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                frm.ShowImage(info.AlarmDateTime, string.Empty);
            }
            e.IsButtonRecord = (_EventIndex == GridView.Rows.Count - 1);
        }

        private void frm_PreRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex > 0)
            {
                this.customDataGridview1.Rows[_EventIndex].Selected = false;
                _EventIndex--;
                this.customDataGridview1.Rows[_EventIndex].Selected = true;
                if (_EventIndex < this.customDataGridview1.FirstDisplayedScrollingRowIndex)
                {
                    if (this.customDataGridview1.FirstDisplayedScrollingRowIndex >= this.customDataGridview1.DisplayedRowCount(false))
                    {
                        this.customDataGridview1.FirstDisplayedScrollingRowIndex -= this.customDataGridview1.DisplayedRowCount(false);
                    }
                    else
                    {
                        this.customDataGridview1.FirstDisplayedScrollingRowIndex = 0;
                    }
                }
                AlarmInfo info = this.GridView.Rows[_EventIndex].Tag as AlarmInfo;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                frm.ShowImage(info.AlarmDateTime, string.Empty);
            }
            e.IsTopRecord = (_EventIndex == 0);
        }

        private void ShowAlarmOnRow(DataGridViewRow row, AlarmInfo alarm)
        {
            row.Tag = alarm;
            row.Cells["colAlarmDateTime"].Value = alarm.AlarmDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colAlarmSource"].Value = alarm.AlarmSource;
            row.Cells["colAlarmType"].Value = Ralid.Park.BusinessModel.Resouce.AlarmTypeDescription.GetDescription(alarm.AlarmType);
            row.Cells["colAlarmDescr"].Value = alarm.AlarmDescr;
            row.Cells["colOperatorID"].Value = alarm.OperatorID;
        }
    }
}
