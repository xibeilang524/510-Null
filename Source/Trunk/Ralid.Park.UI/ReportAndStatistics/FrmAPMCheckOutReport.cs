using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmAPMCheckOutReport : Ralid.Park.UI.ReportAndStatistics.FrmReportBase
    {
        public FrmAPMCheckOutReport()
        {
            InitializeComponent();
            this.ItemSearching += new EventHandler(FrmAPMCheckOutReport_ItemSearching);
        }

        #region 重写基类方法
        private void FrmAPMCheckOutReport_ItemSearching(object sender, EventArgs e)
        {
            APMCheckOutRecordSearchCondition con = new APMCheckOutRecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange(ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            con.MID = comAPM.Text;
            con.APMOperator = this.txtAPMOperator.Text.Trim();

            GridView.Rows.Clear();
            List<APMCheckOutRecord> items = (new APMChectOutRecordBll(AppSettings.CurrentSetting.ParkConnect)).GetItems(con).QueryObjects;
            foreach (APMCheckOutRecord item in items)
            {
                int row = this.GridView.Rows.Add();
                ShowAPMCheckOutRecordOnRow(GridView.Rows[row], item);
            }
        }

        private void ShowAPMCheckOutRecordOnRow(DataGridViewRow row, APMCheckOutRecord info)
        {
            row.Tag = info;
            row.Cells["colMID"].Value = info.MID;
            row.Cells["colCheckOutDateTime"].Value = info.CheckOutDateTime;
            row.Cells["colAmount"].Value = info.Amount;
            row.Cells["colActualAmount"].Value = info.ActualAmount;
            row.Cells["colDifference"].Value = info.Difference;
            row.Cells["colTheBalance"].Value = info.TheBalance;
            row.Cells["colTotalAmount"].Value = info.TotalAmount;
            row.Cells["colLastDateTime"].Value = info.LastDateTime;
            row.Cells["colLastBalance"].Value = info.LastBalance;
            row.Cells["colPayMoney"].Value = info.PayMoney;
            row.Cells["colIncomeMoneny"].Value = info.IncomeMoneny;
            row.Cells["colBalance"].Value = info.Balance;
            row.Cells["colHundred"].Value = info.Hundred;
            row.Cells["colFifty"].Value = info.Fifty;
            row.Cells["colTwenty"].Value = info.Twenty;
            row.Cells["colTen"].Value = info.Ten;
            row.Cells["colCash"].Value = info.Cash;
            row.Cells["colCashAmount"].Value = info.CashAmount;
            row.Cells["colCoin"].Value = info.Coin;
            row.Cells["colAPMOperator"].Value = info.APMOperator;
            row.Cells["colMemo"].Value = info.Memo;

            if (info.Difference != 0) row.DefaultCellStyle.ForeColor = Color.Red;
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

        private void FrmAPMCheckOutReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            InitComAPM();
        }
    }
}
