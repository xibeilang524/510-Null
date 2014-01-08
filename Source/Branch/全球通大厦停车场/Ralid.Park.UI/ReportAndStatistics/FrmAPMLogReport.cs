using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmAPMLogReport : FrmReportBase
    {
        public FrmAPMLogReport()
        {
            InitializeComponent();
            this.ItemSearching += new EventHandler(FrmPayOperationLogReport_ItemSearching);
        }

        #region 重写基类方法
        private void FrmPayOperationLogReport_ItemSearching(object sender, EventArgs e)
        {
            APMLogSearchCondition con = new APMLogSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange(ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            con.SerialNum = txtSerialNum.Text;
            con.CardID = txtCardID.Text;
            con.MID = comAPM.Text;
            con.Description = txtDescription.Text;
            con.Types = new List<BusinessModel.Enum.APMLogType>();
            if (chkMessage.Checked) con.Types.Add(BusinessModel.Enum.APMLogType.Message);
            if (chkAlarm.Checked) con.Types.Add(BusinessModel.Enum.APMLogType.Alarm);
            if (chkError.Checked) con.Types.Add(BusinessModel.Enum.APMLogType.Error);

            GridView.Rows.Clear();
            List<APMLog> items = (new APMLogBll (AppSettings.CurrentSetting.ParkConnect)).GetItems(con).QueryObjects ;
            foreach (APMLog item in items)
            {
                int row = this.GridView.Rows.Add();
                ShowPayOperationLogOnRow(GridView.Rows[row], item);
            }
        }

        private void ShowPayOperationLogOnRow(DataGridViewRow row,APMLog item )
        {
            row.Tag = item;
            row.Cells["colSerialNumber"].Value = item.SerialNumber;
            row.Cells["colLogDateTime"].Value = item.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colLogType"].Value = Ralid.Park.BusinessModel.Resouce.APMLogTypeDescription.GetDescritption(item.LogType);
            row.Cells["colCardID"].Value = item.CardID;
            row.Cells["colDescription"].Value = item.Description;
            row.Cells["colMID"].Value = item.MID;
            row.Cells["colOperatorID"].Value = item.OperatorID;
            if (item.LogType == Ralid.Park.BusinessModel.Enum.APMLogType.Alarm)
            {
                row.DefaultCellStyle.ForeColor = Color.Orange;
            }
            else if (item.LogType == Ralid.Park.BusinessModel.Enum.APMLogType.Error)
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
            }
            else
            {
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void InitComAPM()
        {
            this.comAPM.Items.Clear();
            this.comAPM.Items.Add(string.Empty);
            List<APM> items = (new APMBll(AppSettings.CurrentSetting.ParkConnect)).GetAllItems().QueryObjects;
            foreach (APM item in items)
            {
                this.comAPM.Items.Add(item.SerialNum);
            }
        }
        #endregion

        private void FrmPayOperationLogReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            InitComAPM();
        }
    }
}
