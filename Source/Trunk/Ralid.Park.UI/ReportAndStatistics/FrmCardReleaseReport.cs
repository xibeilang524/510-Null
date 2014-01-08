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
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardReleaseReport : FrmReportBase
    {
        public FrmCardReleaseReport()
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
            con.OwnerName = this.txtOwnerName.Text;
            con.CarPlate = this.txtCarPlate.Text;
            con.CardCertificate = this.txtCertificate.Text;
            if (this.comCardType.SelectedIndex > 0) con.CardType = this.comCardType.SelectedCardType;
            con.Operator = this.operatorCombobox1.SelectecOperator;
            con.StationID = this.workStationCombobox1.Text;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            List<CardReleaseRecord> items = bll.GetCardReleaseRecords(con).QueryObjects;
            ShowReportsOnGrid(items);
            this.txtCount.Text = items.Count.ToString();
            this.txtRecieveMoney.Text = items.Sum(c => c.ReleaseMoney).ToString();
        }

        private void FrmCardReleaseReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.ShowTime = false;
            this.ucDateTimeInterval1.Init();
            this.workStationCombobox1.Init();
            this.operatorCombobox1.Init();
            this.comCardType.Init();
        }

        private void ShowReportsOnGrid(List<CardReleaseRecord> items)
        {
            this.GridView.Rows.Clear();
            items = (from CardReleaseRecord cr in items
                     orderby cr.ReleaseDateTime descending
                     select cr).ToList();
            foreach (CardReleaseRecord record in items)
            {
                int index = GridView.Rows.Add();
                DataGridViewRow row = GridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colCardType"].Value = record.CardType.ToString();
                row.Cells["colActivationDate"].Value = record.ActivationDate;
                row.Cells["colValidDate"].Value = record.ValidDate;
                row.Cells["colDeposit"].Value = record.Deposit;
                row.Cells["colPaymentMode"].Value = Ralid.Park.BusinessModel.Resouce.PaymentModeDescription.GetDescription(record.PaymentMode);
                row.Cells["colReleaseMoney"].Value = record.ReleaseMoney;
                row.Cells["colSettled"].Value = record.SettleDateTime != null;
                row.Cells["colReleaseDateTime"].Value = record.ReleaseDateTime;
                row.Cells["colOperatorID"].Value = record.OperatorID;
                row.Cells["colStation"].Value = record.StationID;
                row.Cells["colMemo"].Value = record.Memo;
            }
        }
    }
}
