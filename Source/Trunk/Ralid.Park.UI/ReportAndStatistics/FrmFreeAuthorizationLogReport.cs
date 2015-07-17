using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmFreeAuthorizationLogReport : Ralid.Park.UI.ReportAndStatistics.FrmReportBase
    {
        #region 构造函数
        public FrmFreeAuthorizationLogReport()
        {
            InitializeComponent();
            this.ItemSearching += this.ItemSearching_Handler;
        }
        #endregion

        #region 私有方法
        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            this.customDataGridview1.Rows.Clear();
            RecordSearchCondition con = new RecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CardID = this.txtCardID.Text.Trim();
            con.Operator = this.comOperator.SelectecOperator;
            con.StationID = this.comWorkStation.Text.Trim();
            FreeAuthorizationLogBll bll = new FreeAuthorizationLogBll(Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.ParkConnect);
            QueryResultList<FreeAuthorizationLog> result = bll.GetFreeAuthorizationLogs(con);
            if (result.Result == ResultCode.Successful)
            {
                List<FreeAuthorizationLog> items = (from log in result.QueryObjects
                                         orderby log.LogDateTime descending
                                         select log).ToList();
                foreach (FreeAuthorizationLog alarm in items)
                {
                    int row = this.customDataGridview1.Rows.Add();
                    ShowFreeAuthorizationLogOnRow(this.customDataGridview1.Rows[row], alarm);
                }
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void ShowFreeAuthorizationLogOnRow(DataGridViewRow row, FreeAuthorizationLog log)
        {
            row.Tag = log;
            row.Cells["colLogDateTime"].Value = log.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colOperatorID"].Value = log.OperatorID;
            row.Cells["colCardID"].Value = log.CardID;
            row.Cells["colBeginDateTime"].Value = log.BeginDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colEndDateTime"].Value = log.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colStationID"].Value = log.StationID;
            row.Cells["colInPark"].Value = log.InPark;
            row.Cells["colNotCheckOut"].Value = !log.CheckOut;
            row.Cells["colMemo"].Value = log.Memo;
        }
        #endregion

        #region 事件处理程序
        private void FrmFreeAuthorizationLogReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.comOperator.Init();
            this.comWorkStation.Init();
        }
        #endregion
    }
}
