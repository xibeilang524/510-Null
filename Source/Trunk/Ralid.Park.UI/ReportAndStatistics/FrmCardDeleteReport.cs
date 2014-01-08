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
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardDeleteReport : FrmReportBase 
    {
        public FrmCardDeleteReport()
        {
            InitializeComponent();
        }

        #region 私有方法
        private void ShowReportsOnGrid(List<CardDeleteRecord> items)
        {
            GridView.Rows.Clear();
            items = (from CardDeleteRecord cr in items
                     orderby cr.DeleteDateTime descending
                     select cr).ToList();
            foreach (CardDeleteRecord record in items)
            {
                int index = GridView.Rows.Add();
                DataGridViewRow row = GridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colRecycleDateTime"].Value = record.DeleteDateTime;
                row.Cells["colCardType"].Value = record.CardType.ToString();
                row.Cells["colOperatorID"].Value = record.OperatorID;
                row.Cells["colStation"].Value = record.StationID;
            }
        }
        #endregion

        #region 重写基类方法
        protected override void OnLoad(EventArgs e)
        {
            this.ucDateTimeInterval1.ShowTime = false;
            this.ucDateTimeInterval1.Init();
            this.workStationCombobox1.Init();
            this.operatorCombobox1.Init();
            this.comCardType.Init();
        }

        protected override void OnItemSearching(EventArgs e)
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
            List<CardDeleteRecord> items = bll.GetCardDeleteRecords(con).QueryObjects;
            ShowReportsOnGrid(items);
        }
        #endregion
    }
}
