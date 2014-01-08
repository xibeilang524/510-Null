using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardChargeReport :FrmReportBase 
    {
        public FrmCardChargeReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }


        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            RecordSearchCondition con = new RecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CardID = this.txtCardID.Text.Trim();
            con.Operator = this.operatorCombobox1.SelectecOperator;
            con.StationID = this.workStationCombobox1.Text;
            con.OwnerName = this.txtOwnerName.Text;
            con.CarPlate = this.txtCarPlate.Text;
            con.CardCertificate = this.txtCertificate.Text;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect); 
            List<CardChargeRecord> items = bll.GetCardChargeRecords(con).QueryObjects;
            ShowReportsOnGrid(items);
            this.txtCount.Text = items.Count.ToString();
            this.txtRecieveMoney.Text = items.Sum(c => c.Payment).ToString();
            this.txtChargeAmount.Text = items.Sum(c => c.ChargeAmount).ToString();
        }

        private void FrmCardChargeReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.workStationCombobox1.Init();
            this.operatorCombobox1.Init();
        }

        private void ShowReportsOnGrid(List<CardChargeRecord> items)
        {
            this.GridView.Rows.Clear();
            items = (from CardChargeRecord cr in items
                     orderby cr.ChargeDateTime descending
                     select cr).ToList();
            foreach (CardChargeRecord record in items)
            {
                int index = this.GridView.Rows.Add();
                DataGridViewRow row = this.GridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colChargeDateTime"].Value = record.ChargeDateTime;
                row.Cells["colChargeAmount"].Value = record.ChargeAmount;
                row.Cells["colPayment"].Value = record.Payment ;
                row.Cells["colPaymentMode"].Value = Ralid.Park.BusinessModel.Resouce.PaymentModeDescription.GetDescription(record.PaymentMode);
                row.Cells["colBalance"].Value = record.Balance;
                row.Cells["colExpiredDate"].Value = record.ValidDate.ToString("yyyy-MM-dd");
                row.Cells["colSettled"].Value = record.SettleDateTime != null;
                row.Cells["colOperatorID"].Value = record.OperatorID;
                row.Cells["colStation"].Value = record.StationID;
                row.Cells["colMemo"].Value = record.Memo;
            }
        }
    }
}
