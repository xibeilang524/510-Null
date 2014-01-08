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
    public partial class FrmCardDeferStatistics : FrmReportBase
    {
        public FrmCardDeferStatistics()
        {
            InitializeComponent();
        }

        #region 私有方法
        private void GroupBy(List<CardDeferRecord > items, CarFlowStatisticsType statisticsType)
        {
            IEnumerable<IGrouping<string, CardDeferRecord >> groups = null;
            if (items.Count > 0)
            {
                switch (statisticsType)
                {
                    case CarFlowStatisticsType.perHour:
                        groups = from c in items
                                 orderby c.DeferDateTime ascending
                                 group c by c.DeferDateTime.ToString("yyyy-MM-dd HH:00:00");
                        break;
                    case CarFlowStatisticsType.perDay:
                        groups = from c in items
                                 orderby c.DeferDateTime ascending
                                 group c by c.DeferDateTime.ToString("yyyy-MM-dd");
                        break;
                    case CarFlowStatisticsType.perMonth:
                        groups = from c in items
                                 orderby c.DeferDateTime ascending
                                 group c by c.DeferDateTime.ToString("yyyy-MM");
                        break;
                    default:
                        break;
                }
                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        int row = this.customDataGridView1.Rows.Add();
                        ShowGroupOnGridviewRow(this.customDataGridView1.Rows[row], group);
                    }
                }
            }
        }

        private void ShowGroupOnGridviewRow(DataGridViewRow row, IGrouping<string, CardDeferRecord> group)
        {
            row.Cells["colChargeDateTime"].Value = group.Key;
            row.Cells["colCount"].Value = group.Count();
            row.Cells["colAccounts"].Value = group.Sum(item => item.DeferMoney);
            row.Cells["colDiscount"].Value = 0;
            row.Cells["colPaid"].Value = group.Sum(item => item.DeferMoney);
        }
        #endregion

        #region 重写基类方法
        protected override void OnLoad(EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.ucDateTimeInterval1.SelectThisMonth();
            this.operatorCombobox1.Init();
            this.workStationCombobox1.Init();
            base.OnLoad(e);
        }

        protected override void OnItemSearching(EventArgs e)
        {
            this.customDataGridView1.Rows.Clear();
            RecordSearchCondition con = new RecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CardID = this.txtCardID.Text.Trim();
            con.Operator = this.operatorCombobox1.SelectecOperator;
            con.StationID = this.workStationCombobox1.Text;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            List<CardDeferRecord> items = bll.GetCardDeferRecords(con).QueryObjects;
            if (items != null && items.Count > 0)
            {
                CarFlowStatisticsType group;
                if (this.rdPerHour.Checked)
                {
                    group = CarFlowStatisticsType.perHour;
                }
                else if (this.rdPerDay.Checked)
                {
                    group = CarFlowStatisticsType.perDay;
                }
                else
                {
                    group = CarFlowStatisticsType.perMonth;
                }
                GroupBy(items, group);
            }
        }
        #endregion
    }
}
