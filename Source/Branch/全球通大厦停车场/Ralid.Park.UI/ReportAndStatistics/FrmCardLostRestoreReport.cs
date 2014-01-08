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
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardLostRestoreReport :FrmReportBase 
    {
        public FrmCardLostRestoreReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }


        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            RecordSearchCondition con = new RecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
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
            List<CardLostRestoreRecord> items = bll.GetCardLostRestoreRecords(con).QueryObjects;
            ShowReportsOnGrid(items);
        }

        private void FrmCardLostRestoreReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.operatorCombobox1.Init();
            this.workStationCombobox1.Init();
        }

        private void ShowReportsOnGrid(List<CardLostRestoreRecord> items)
        {
            this.GridView.Rows.Clear();
            items = (from CardLostRestoreRecord cr in items
                     orderby cr.LostDateTime  descending
                     select cr).ToList();
            foreach (CardLostRestoreRecord record in items)
            {
                int index = GridView.Rows.Add();
                DataGridViewRow row = GridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colLostDateTime"].Value = record.LostDateTime;
                row.Cells["colLostOperator"].Value = record.LostOperator;
                row.Cells["colLostMemo"].Value = record.LostMemo;
                row.Cells["colLostCardCost"].Value = record.LostCardCost;
                row.Cells["colPaymentMode"].Value = record.PaymentMode == null ? string.Empty : PaymentModeDescription.GetDescription(record.PaymentMode.Value);
                row.Cells["colSettled"].Value = record.SettleDateTime != null;
                row.Cells["colRestoreDateTime"].Value = record.RestoreDateTime;
                row.Cells["colRestoreOperator"].Value = record.RestoreOperator;
                row.Cells["colRestoreMemo"].Value = record.RestoreMemo;
            }
        }
    }
}
