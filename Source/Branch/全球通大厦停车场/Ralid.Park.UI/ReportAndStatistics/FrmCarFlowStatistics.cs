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
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCarFlowStatistics : FrmReportBase
    {
        public FrmCarFlowStatistics()
        {
            InitializeComponent();
            InitGridHeader();
        }

        #region 私有变量
        private string strIn = Resources.Resource1.FrmCarFlow_In;
        private string strOut = Resources.Resource1.FrmCarFlow_Out;
        private string strTotal = Resources.Resource1.FrmCarFlow_Total;
        private string strDateTime = Resources.Resource1.FrmCarFlow_DateTime;
        #endregion

        #region 私有方法
        private void InitGridHeader()
        {
            int colWidth = 100;

            DataGridViewTextBoxColumn colDateTime = new DataGridViewTextBoxColumn();
            colDateTime.Name = "colDateTime";
            colDateTime.HeaderText = strDateTime;
            colDateTime.ReadOnly = true;
            this.GridView.Columns.Add(colDateTime);

            DataGridViewTextBoxColumn colIn = new DataGridViewTextBoxColumn();
            colIn.HeaderText = CardType.MonthRentCard.Name + strIn;
            colIn.Name = colIn.HeaderText;
            colIn.ReadOnly = true;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colIn);

            colIn = new DataGridViewTextBoxColumn();
            colIn.HeaderText = CardType.PrePayCard.Name + strIn;
            colIn.Name = colIn.HeaderText;
            colIn.ReadOnly = true;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colIn);

            colIn = new DataGridViewTextBoxColumn();
            colIn.HeaderText = CardType.TempCard.Name + strIn;
            colIn.Name = colIn.HeaderText;
            colIn.ReadOnly = true;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colIn);

            if (CustomCardTypeSetting.Current != null && CustomCardTypeSetting.Current.CardTypes != null)
            {
                CardType[] cardTypes = CustomCardTypeSetting.Current.CardTypes;
                foreach (CardType ct in cardTypes)
                {
                    colIn = new DataGridViewTextBoxColumn();
                    colIn.HeaderText = ct.Name + strIn;
                    colIn.Name = colIn.HeaderText;
                    colIn.ReadOnly = true;
                    colIn.Width = colWidth;
                    this.GridView.Columns.Add(colIn);
                }
            }
            DataGridViewTextBoxColumn colTotalIn = new DataGridViewTextBoxColumn();
            colTotalIn.ReadOnly = true;
            colTotalIn.HeaderText = strIn + strTotal;
            colTotalIn.Name = colTotalIn.HeaderText;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colTotalIn);

            DataGridViewTextBoxColumn colOut = new DataGridViewTextBoxColumn();
            colOut.HeaderText = CardType.MonthRentCard.Name + strOut;
            colOut.Name = colOut.HeaderText;
            colOut.ReadOnly = true;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colOut);

            colOut = new DataGridViewTextBoxColumn();
            colOut.HeaderText = CardType.PrePayCard.Name + strOut;
            colOut.Name = colOut.HeaderText;
            colOut.ReadOnly = true;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colOut);

            colOut = new DataGridViewTextBoxColumn();
            colOut.HeaderText = CardType.TempCard.Name + strOut;
            colOut.Name = colOut.HeaderText;
            colOut.ReadOnly = true;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colOut);

            if (CustomCardTypeSetting.Current != null && CustomCardTypeSetting.Current.CardTypes != null)
            {
                CardType[] cardTypes = CustomCardTypeSetting.Current.CardTypes;
                foreach (CardType ct in cardTypes)
                {
                    colOut = new DataGridViewTextBoxColumn();
                    colOut.HeaderText = ct.Name + strOut;
                    colOut.Name = colOut.HeaderText;
                    colOut.ReadOnly = true;
                    colIn.Width = colWidth;
                    this.GridView.Columns.Add(colOut);
                }
            }
            DataGridViewTextBoxColumn colTotalOut = new DataGridViewTextBoxColumn();
            colTotalOut.HeaderText = strOut + strTotal;
            colTotalOut.Name = colTotalOut.HeaderText;
            colTotalOut.ReadOnly = true;
            colIn.Width = colWidth;
            this.GridView.Columns.Add(colTotalOut);
        }

        private void CarFlowStaticstics(RecordSearchCondition search, CarFlowStatisticsType statisticsType)
        {
            List<CardEventRecord> items = (new CardEventBll(AppSettings.CurrentSetting.ParkConnect)).GetCardEvents(search).QueryObjects;
            IEnumerable<IGrouping<string, CardEventRecord>> groups = null;
            if (items.Count > 0)
            {
                switch (statisticsType)
                {
                    case CarFlowStatisticsType.perHour:
                        groups = from c in items
                                 orderby c.EventDateTime ascending
                                 group c by c.EventDateTime.ToString("yyyy-MM-dd HH:00:00");
                        break;
                    case CarFlowStatisticsType.perDay:
                        groups = from c in items
                                 orderby c.EventDateTime ascending
                                 group c by c.EventDateTime.ToString("yyyy-MM-dd");
                        break;
                    case CarFlowStatisticsType.perMonth:
                        groups = from c in items
                                 orderby c.EventDateTime ascending
                                 group c by c.EventDateTime.ToString("yyyy-MM");
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

        private void ShowGroupOnGridviewRow(DataGridViewRow row, IGrouping<string, CardEventRecord> group)
        {
            row.Cells["colDateTime"].Value = group.Key;
            if (CustomCardTypeSetting.Current != null && CustomCardTypeSetting.Current.CardTypes != null)
            {
                row.Cells[CardType.MonthRentCard.Name + strIn].Value = group.Count(c => c.CardType == CardType.MonthRentCard && !c.IsExitEvent);
                row.Cells[CardType.PrePayCard.Name + strIn].Value = group.Count(c => c.CardType == CardType.PrePayCard && !c.IsExitEvent);
                row.Cells[CardType.TempCard.Name + strIn].Value = group.Count(c => (c.CardType == CardType.TempCard || c.CardType == CardType.Ticket) && !c.IsExitEvent);
                CardType[] cardTypes = CustomCardTypeSetting.Current.CardTypes;
                foreach (CardType ct in cardTypes)
                {
                    row.Cells[ct.Name + strIn].Value = group.Count(c => c.CardType == ct && !c.IsExitEvent);
                }
            }
            else
            {
                row.Cells[CardType.MonthRentCard.Name + strIn].Value = group.Count(c => c.CardType.IsMonthCard && !c.IsExitEvent);
                row.Cells[CardType.PrePayCard.Name + strIn].Value = group.Count(c => c.CardType.IsPrepayCard && !c.IsExitEvent);
                row.Cells[CardType.TempCard.Name + strIn].Value = group.Count(c => c.CardType.IsTempCard && !c.IsExitEvent);
            }
            row.Cells[strIn + strTotal].Value = group.Count(c => !c.IsExitEvent);

            if (CustomCardTypeSetting.Current != null && CustomCardTypeSetting.Current.CardTypes != null)
            {
                row.Cells[CardType.MonthRentCard.Name + strOut].Value = group.Count(c => c.CardType == CardType.MonthRentCard && c.IsExitEvent);
                row.Cells[CardType.PrePayCard.Name + strOut].Value = group.Count(c => c.CardType == CardType.PrePayCard && c.IsExitEvent);
                row.Cells[CardType.TempCard.Name + strOut].Value = group.Count(c => (c.CardType == CardType.TempCard || c.CardType == CardType.Ticket) && c.IsExitEvent);
                CardType[] cardTypes = CustomCardTypeSetting.Current.CardTypes;
                foreach (CardType ct in cardTypes)
                {
                    row.Cells[ct.Name + strOut].Value = group.Count(c => c.CardType == ct && c.IsExitEvent);
                }
            }
            else
            {
                row.Cells[CardType.MonthRentCard.Name + strOut].Value = group.Count(c => c.CardType.IsMonthCard && c.IsExitEvent);
                row.Cells[CardType.PrePayCard.Name + strOut].Value = group.Count(c => c.CardType.IsPrepayCard && c.IsExitEvent);
                row.Cells[CardType.TempCard.Name + strOut].Value = group.Count(c => c.CardType.IsTempCard && c.IsExitEvent);
            }
            row.Cells[strOut + strTotal].Value = group.Count(c => c.IsExitEvent);
        }
        #endregion

        #region 事件处理程序
        private void FrmCarFlowStatistics_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.ucEntrance1.Init();
            this.carTypeComboBox1.Init();
        }

        protected override void OnItemSearching(EventArgs e)
        {
            this.customDataGridView1.Rows.Clear();

            CardEventSearchCondition con = new CardEventSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.Source = this.ucEntrance1.SelectedEntrances;
            if(this.carTypeComboBox1 .SelectedIndex >=0)con.CarType = this.carTypeComboBox1.SelectedCarType;

            CarFlowStatisticsType cType;
            if (this.rdPerHour.Checked)
            {
                cType = CarFlowStatisticsType.perHour;
            }
            else if (this.rdPerDay.Checked)
            {
                cType = CarFlowStatisticsType.perDay;
            }
            else
            {
                cType = CarFlowStatisticsType.perMonth;
            }
            CarFlowStaticstics(con, cType);
        }
        #endregion
    }
}
