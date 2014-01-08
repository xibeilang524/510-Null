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
    public partial class FrmCardDisableEnableReport :FrmReportBase 
    {
        public FrmCardDisableEnableReport()
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
            List<CardDisableEnableRecord> items=bll.GetCardDisableEnableRecords(con).QueryObjects ;
            ShowReportsOnGrid(items);
        }

        private void FrmCardDisableEnableReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.operatorCombobox1.Init();
            this.workStationCombobox1.Init();
        }

        private void ShowReportsOnGrid(List<CardDisableEnableRecord> items)
        {
            this.gridView.Rows.Clear();
            items = (from CardDisableEnableRecord cr in items
                     orderby cr.DisableDateTime  descending
                     select cr).ToList();
            foreach (CardDisableEnableRecord  record in items)
            {
                int index = gridView.Rows.Add();
                DataGridViewRow row = gridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colDisableDateTime"].Value = record.DisableDateTime ;
                row.Cells["colDisableOperator"].Value = record.DisableOperator ;
                row.Cells["colDisableMemo"].Value = record.DisableMemo ;
                row.Cells["colEnableDateTime"].Value = record.EnableDateTime ;
                row.Cells["colEnableOperator"].Value = record.EnableOperator;
                row.Cells["colEnableMemo"].Value = record.EnableMemo ;
            }
        }
    }
}
