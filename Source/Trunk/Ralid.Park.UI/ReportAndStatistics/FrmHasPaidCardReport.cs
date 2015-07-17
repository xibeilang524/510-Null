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
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmHasPaidCardReport : FrmReportBase
    {
        public FrmHasPaidCardReport()
        {
            InitializeComponent();
        }

        #region 私有方法
        private List<HaspaidCardRecord> GetCardDeferRecords()
        {
            List<HaspaidCardRecord> items = new List<HaspaidCardRecord>();
            List<CardDeferRecord> records = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDeferRecords(null).QueryObjects;
            CardType cardType = this.comCardType.SelectedCardType;
            if (records != null && records.Count > 0)
            {
                foreach (CardDeferRecord record in records)
                {
                    if (cardType == null || record.CardType == cardType)
                    {
                        HaspaidCardRecord item = new HaspaidCardRecord()
                        {
                            CardID = record.CardID,
                            OwnerName = record.OwnerName,
                            CardType = record.CardType,
                            Certificate = record.CardCertificate,
                            CarPlate = record.CarPlate,
                            RecordDateTime = record.DeferDateTime,
                            Money = record.DeferMoney,
                            OriginalDate = record.OriginalDate,
                            CurrentDate = record.CurrentDate
                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        private List<HaspaidCardRecord> GetCardReleaseRecords()
        {
            List<HaspaidCardRecord> items = new List<HaspaidCardRecord>();
            List<CardReleaseRecord> records = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardReleaseRecords(null).QueryObjects;
            CardType cardType = this.comCardType.SelectedCardType;
            if (records != null && records.Count > 0)
            {
                foreach (CardReleaseRecord record in records)
                {
                    if (record.CardType.IsMonthCard)
                    {
                        if (cardType == null || record.CardType == cardType)
                        {
                            HaspaidCardRecord item = new HaspaidCardRecord()
                            {
                                CardID = record.CardID,
                                OwnerName = record.OwnerName,
                                CardType = record.CardType,
                                Certificate = record.CardCertificate,
                                CarPlate = record.CarPlate,
                                RecordDateTime = record.ReleaseDateTime,
                                Money = record.ReleaseMoney,
                                OriginalDate = record.ActivationDate.Date,
                                CurrentDate = record.ValidDate
                            };
                            items.Add(item);
                        }
                    }
                }
            }
            return items;
        }

        /// <summary>
        /// 获取已扣费用
        /// </summary>
        /// <param name="monthlyFee">月租费</param>
        /// <param name="begin">开始日期</param>
        /// <param name="end">结算日期</param>
        /// <returns></returns>
        private decimal GetDeduction(decimal monthlyFee, DateTime begin, DateTime end)
        {
            if (monthlyFee == 0) return 0;

            int month = 0;
            DateTime tempTime = begin;
            while (end >= tempTime)
            {
                month++;
                tempTime = tempTime.AddMonths(1);
            }
            return month * monthlyFee;
        }
        #endregion

        #region 重写基类方法
        protected override void OnLoad(EventArgs e)
        {
            this.txtYear.Text = DateTime.Now.Year.ToString();
            this.txtMonth.Text = DateTime.Now.Month.ToString();
            this.comCardType.Init();
        }

        protected override void OnItemSearching(EventArgs e)
        {
            this.customDataGridView1.Rows.Clear();
            List<HaspaidCardRecord> records = new List<HaspaidCardRecord>();
            records.AddRange(GetCardDeferRecords());
            records.AddRange(GetCardReleaseRecords());
            if (records != null && records.Count > 0)
            {
                DateTime begin = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), 1);
                DateTime end = begin.AddMonths(1).AddSeconds(-1);
                decimal account = this.txtMonthlyFee.DecimalValue;
                foreach (HaspaidCardRecord record in records)
                {
                    if ((record.OriginalDate <= begin || (record.OriginalDate.Year == begin.Year && record.OriginalDate.Month == begin.Month)) //如果卡片有效起始日期小于本月第一天，或在本月内
                        && (record.CurrentDate >= end || (record.CurrentDate.Year == end.Year && record.CurrentDate.Month == end.Month)))  //如果卡片有效结束日期大于本月最后一天，或在本月内
                    {
                        int row = this.customDataGridView1.Rows.Add();
                        this.customDataGridView1.Rows[row].Cells["colCardID"].Value = record.CardID;
                        this.customDataGridView1.Rows[row].Cells["colOwnerName"].Value = record.OwnerName;
                        this.customDataGridView1.Rows[row].Cells["colCardType"].Value = record.CardType != null ? record.CardType.Name : string.Empty; ;
                        this.customDataGridView1.Rows[row].Cells["colCardCertificate"].Value = record.Certificate;
                        this.customDataGridView1.Rows[row].Cells["colCarPlate"].Value = record.CarPlate;
                        this.customDataGridView1.Rows[row].Cells["colPaid"].Value = record.Money;
                        decimal deduction = GetDeduction(account, record.OriginalDate, begin.AddSeconds(-1));
                        this.customDataGridView1.Rows[row].Cells["colDeduction"].Value = deduction;
                        this.customDataGridView1.Rows[row].Cells["colAccount"].Value = account;
                        this.customDataGridView1.Rows[row].Cells["colBalance"].Value = record.Money - deduction - account;
                        this.customDataGridView1.Rows[row].Cells["colBegin"].Value = record.OriginalDate.ToLongDateString();
                        this.customDataGridView1.Rows[row].Cells["colEnd"].Value = record.CurrentDate.ToLongDateString();

                        if (record.CurrentDate < DateTime.Now)
                        {
                            this.customDataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                }
            }
            base.OnItemSearching(e);
        }
        #endregion

        private class HaspaidCardRecord
        {
            #region 构造函数

            #endregion

            #region 公共属性
            public string CardID { get; set; }

            public string OwnerName { get; set; }

            public CardType CardType { get; set; }

            public string Certificate { get; set; }

            public string CarPlate { get; set; }

            public DateTime RecordDateTime { get; set; }

            public decimal Money { get; set; }

            public DateTime OriginalDate { get; set; }

            public DateTime CurrentDate { get; set; }
            #endregion
        }
    }
}
