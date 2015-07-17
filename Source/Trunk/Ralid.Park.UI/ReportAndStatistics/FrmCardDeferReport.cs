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
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardDeferReport : FrmReportBase
    {
        public FrmCardDeferReport()
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
            con.CardType = this.comCardType.SelectedCardType;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect); 
            List < CardDeferRecord > items= bll.GetCardDeferRecords(con).QueryObjects;
            ShowReportsOnGrid(items);
            this.txtCount.Text = items.Count.ToString ();
            this.txtRecieveMoney.Text = items.Sum(c => c.DeferMoney).ToString();
        }

        private void FrmCardDeferReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.operatorCombobox1.Init();
            this.workStationCombobox1.Init();
            this.comCardType.Init();
        }

        private void ShowReportsOnGrid(List<CardDeferRecord> items)
        {
            this.GridView.Rows.Clear();
            items = (from CardDeferRecord cr in items
                     orderby cr.DeferDateTime  descending
                     select cr).ToList();
            foreach (CardDeferRecord record in items)
            {
                int index = GridView.Rows.Add();
                DataGridViewRow row = GridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardType"].Value = record.CardType != null ? record.CardType.Name : string.Empty;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colDeferDateTime"].Value = record.DeferDateTime;
                row.Cells["colCurValidDate"].Value = record.CurrentDate;
                row.Cells["colOriginalValidDate"].Value = record.OriginalDate;
                row.Cells["colPaymentMode"].Value = Ralid.Park.BusinessModel.Resouce.PaymentModeDescription.GetDescription(record.PaymentMode);
                row.Cells["colRecieveMoney"].Value = record.DeferMoney;
                row.Cells["colSettled"].Value = record.SettleDateTime != null;
                row.Cells["colOperator"].Value = record.OperatorID;
                row.Cells["colStation"].Value = record.StationID;
                row.Cells["colMemo"].Value = record.Memo;
            }
        }
    }
}
