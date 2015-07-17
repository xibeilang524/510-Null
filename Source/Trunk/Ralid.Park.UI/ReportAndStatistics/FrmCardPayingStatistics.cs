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
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardPayingStatistics : FrmReportBase
    {
        public FrmCardPayingStatistics()
        {
            InitializeComponent();
        }

        #region 私有方法
        private void GroupBy(List<CardPaymentInfo> items, CarFlowStatisticsType statisticsType)
        {
            decimal paid = 0;
            decimal discount = 0;
            IEnumerable<IGrouping<string, CardPaymentInfo>> groups = null;
            if (items.Count > 0)
            {
                switch (statisticsType)
                {
                    case CarFlowStatisticsType.perHour:
                        groups = from c in items
                                 orderby c.ChargeDateTime ascending
                                 group c by c.ChargeDateTime.ToString("yyyy-MM-dd HH:00:00");
                        break;
                    case CarFlowStatisticsType.perDay:
                        groups = from c in items
                                 orderby c.ChargeDateTime ascending
                                 group c by c.ChargeDateTime.ToString("yyyy-MM-dd");
                        break;
                    case CarFlowStatisticsType.perMonth:
                        groups = from c in items
                                 orderby c.ChargeDateTime ascending
                                 group c by c.ChargeDateTime.ToString("yyyy-MM");
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
                        paid += group.Sum(item => item.Paid);
                        discount += group.Sum(item => item.Discount);
                    }
                    this.txtPaid.Text = paid.ToString();
                    this.txtAccounts.Text = discount.ToString();
                }
            }
        }

        private void ShowGroupOnGridviewRow(DataGridViewRow row, IGrouping<string, CardPaymentInfo> group)
        {
            decimal discount = group.Sum(item => item.Discount);
            decimal paid = group.Sum(item => item.Paid);

            row.Cells["colChargeDateTime"].Value = group.Key;
            row.Cells["colCount"].Value = group.Count();
            //这里不使用group.Sum(item => item.Accounts)进行应收款统计，是因为缴费机的缴费记录可能分多条记录缴费，如果使用Accounts统计，会多统计应收款
            //row.Cells["colAccounts"].Value = group.Sum(item => item.Accounts);
            row.Cells["colAccounts"].Value = paid + discount;
            row.Cells["colDiscount"].Value = discount;
            row.Cells["colPaid"].Value = paid;
        }
        #endregion

        #region 重写基类方法
        protected override void OnItemSearching(EventArgs e)
        {
            this.customDataGridView1.Rows.Clear();
            CardPaymentRecordSearchCondition con = new CardPaymentRecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CardType = this.comCardType.SelectedCardType;
            con.CardID = this.txtCardID.Text.Trim();
            con.OperatorCardID = this.txtOperatorCardID.Text.Trim();
            con.PaymentCode = this.comPaymentCode.SelectedPaymentCode;

            if (chkPaymentMode.Checked)
            {
                con.PaymentMode = comPaymentMode.SelectedPaymentMode;
            }

            if (this.comOperator.SelectecOperator != null)
            {
                con.Operator = this.comOperator.SelectecOperator;
            }
            if (carTypeComboBox1.SelectedIndex > 0) con.CarType = carTypeComboBox1.SelectedCarType;
            con.StationID = this.workStationCombobox1.Text;

            CardPaymentRecordBll bll = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
            QueryResultList<CardPaymentInfo> result = bll.GetItems(con);
            if (result.Result == ResultCode.Successful)
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
                GroupBy(result.QueryObjects, group);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.ucDateTimeInterval1.SelectThisMonth();
            this.comCardType.Init();
            this.comOperator.Init();
            this.comPaymentMode.Init();
            this.workStationCombobox1.Init();
            this.carTypeComboBox1.Init();
            this.comPaymentCode.Init();
            base.OnLoad(e);
        }
        #endregion
    }
}

