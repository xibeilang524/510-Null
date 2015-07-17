using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmAPMRefundReport : Ralid.Park.UI.ReportAndStatistics.FrmReportBase
    {
        public FrmAPMRefundReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }

        #region 重写基类方法
        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            APMLogSearchCondition con = new APMLogSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange(ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            con.SerialNum = txtSerialNum.Text;
            con.CardID = txtCardID.Text;
            con.MID = comAPM.Text;
            con.CardCertificate = this.txtCertificate.Text; 
            con.Operator = this.operatorCombobox1.SelectecOperator;
            con.StationID = this.workStationCombobox1.Text;

            GridView.Rows.Clear();
            List<APMRefundRecord> items = (new APMRefundRecordBll(AppSettings.CurrentSetting.ParkConnect)).GetAPMRefundRecords(con).QueryObjects;
            foreach (APMRefundRecord item in items)
            {
                int row = this.GridView.Rows.Add();
                ShowAPMRefundRecordOnRow(GridView.Rows[row], item);
            }
            this.txtCount.Text = items.Count.ToString();
            this.txtTurnBackMoney.Text = items.Sum(c => c.RefundMoney).ToString();
        }

        private void ShowAPMRefundRecordOnRow(DataGridViewRow row, APMRefundRecord record)
        {
            row.Tag = record;
            row.Cells["colCardID"].Value = record.CardID;
            row.Cells["colCardCertificate"].Value = record.CardCertificate;
            row.Cells["colMID"].Value = record.MID;
            row.Cells["colPaymentSerialNumber"].Value = record.PaymentSerialNumber;
            row.Cells["colRefundDateTime"].Value = record.RefundDateTime;
            row.Cells["colParkFee"].Value = record.ParkFee;
            row.Cells["colTotalPaidFee"].Value = record.TotalPaidFee;
            row.Cells["colRefundMoney"].Value = record.RefundMoney;
            row.Cells["colEnterDateTime"].Value = record.EnterDateTime;
            row.Cells["colPaidDateTime"].Value = record.PaidDateTime;
            row.Cells["colSettled"].Value = record.SettleDateTime != null;
            row.Cells["colSettleDateTime"].Value = record.SettleDateTime;
            row.Cells["colOperatorID"].Value = record.OperatorID;
            row.Cells["colStation"].Value = record.StationID;
            row.Cells["colMemo"].Value = record.Memo;
            if (record.TotalPaidFee != record.RefundMoney)
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
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

        private void FrmAPMRefundReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            InitComAPM();
            this.operatorCombobox1.Init();
            this.workStationCombobox1.Init();
        }

    }
}
