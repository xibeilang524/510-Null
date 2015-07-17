using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmServerSwitchReport : Ralid.Park.UI.ReportAndStatistics.FrmReportBase
    {
        #region 构造函数
        public FrmServerSwitchReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }
        #endregion

        #region 私有事件
        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            ServerSwitchRecordSearchCondition con = new ServerSwitchRecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange(ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            con.ParkID = this.comPark.SelectedParkID;

            GridView.Rows.Clear();
            List<ServerSwitchRecord> items = (new ServerSwitchRecordBll(AppSettings.CurrentSetting.ParkConnect)).GetServerSwitchRecords(con).QueryObjects;
            foreach (ServerSwitchRecord item in items)
            {
                int row = this.GridView.Rows.Add();
                ShowServerSwitchRecordOnRow(GridView.Rows[row], item);
            }
        }

        private void ShowServerSwitchRecordOnRow(DataGridViewRow row, ServerSwitchRecord item)
        {
            row.Tag = item;
            ParkInfo park = ParkBuffer.Current.GetPark(item.ParkID);
            row.Cells["colPark"].Value = park == null ? string.Empty : park.ParkName;
            row.Cells["colSwitchDateTime"].Value = item.SwitchDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colSwitchServerIP"].Value = item.SwitchServerIP;
            row.Cells["colSwitchStatus"].Value = HostStandbyStatusDescription.GetDescription(item.SwitchStatus);
            row.Cells["colOperator"].Value = item.Operator;
            row.Cells["colLastDateTime"].Value = item.LastDateTime == null ? string.Empty : item.LastDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colLastIP"].Value = item.LastIP;
            row.Cells["colLastStatus"].Value = HostStandbyStatusDescription.GetDescription(item.LastStatus);
            row.Cells["colSMSStatus"].Value = SMSSendStatusDescription.GetDescription(item.SMSStatus);
            row.Cells["colMemo"].Value = item.Memo;
        }
        #endregion

        #region 窗体事件
        private void FrmServerSwitchReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.comPark.Init();
        }
        #endregion
    }
}
