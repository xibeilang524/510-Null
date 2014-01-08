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
    public partial class FrmCardRecycleReport : FrmReportBase
    {
        public FrmCardRecycleReport()
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
            List<CardRecycleRecord> items = bll.GetCardRecycleRecords(con).QueryObjects;
            ShowReportsOnGrid(items);
            this.txtCount.Text = items.Count.ToString();
            this.txtTurnBackMoney.Text = items.Sum(c => c.RecycleMoney).ToString();
        }

        private void FrmCardRecycleReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.ShowTime = false;
            this.ucDateTimeInterval1.Init();
            this.workStationCombobox1.Init();
            this.operatorCombobox1.Init();
            this.comCardType.Init();
        }

        private void ShowReportsOnGrid(List<CardRecycleRecord> items)
        {
            GridView.Rows.Clear();
            items = (from CardRecycleRecord cr in items
                     orderby cr.RecycleDateTime descending
                     select cr).ToList();
            foreach (CardRecycleRecord record in items)
            {
                int index = GridView.Rows.Add();
                DataGridViewRow row = GridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colRecycleDateTime"].Value = record.RecycleDateTime;
                row.Cells["colCardType"].Value = record.CardType.ToString();
                row.Cells["colBalance"].Value = record.Balance;
                row.Cells["colValidDate"].Value = record.ValidDate.ToString("yyyy-MM-dd");
                row.Cells["colDeposit"].Value = record.Deposit;
                row.Cells["colRecycleMoney"].Value = record.RecycleMoney;
                row.Cells["colSettled"].Value = record.SettleDateTime != null;
                row.Cells["colOperatorID"].Value = record.OperatorID;
                row.Cells["colStation"].Value = record.StationID;
                row.Cells["colMemo"].Value = record.Memo;
            }
        }

    }
}
