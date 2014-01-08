using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;

namespace ExcelExport
{
    public class MonthCardReportToExcel
    {
        public MonthCardReportToExcel(string modalPath)
        {
            ReportModal = modalPath;
        }

        /// <summary>
        /// 获取或设置模板的路径
        /// </summary>
        public string ReportModal { get; set; }

        /// <summary>
        /// 导出操作员当班信息到EXCEL中
        /// </summary>
        /// <param name="optLog"></param>
        public void Export(OperatorSettleLog optLog, string path)
        {
            RecordSearchCondition recordCon = new RecordSearchCondition();
            recordCon.SettleDateTime = optLog.SettleDateTime;

            Application app = new Application();
            Workbook book = null;
            book = app.Workbooks.Add(ReportModal);
            Worksheet sheet = book.ActiveSheet as Worksheet;
            FillOperatorLog(sheet, optLog, 3);
            List<MonthCardPaymentInfo> cardPaymentRecords = GetMonthCardPaymentInfoes(recordCon);
            FillDetail(sheet, cardPaymentRecords, 6);
            book.SaveAs(path, XlFileFormat.xlXMLSpreadsheet, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            book.Close(false, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// 导出操作员当班信息到EXCEL中,并直接打印出来
        /// </summary>
        /// <param name="optLog"></param>
        public void PrintByExcel(OperatorSettleLog optLog)
        {
            RecordSearchCondition recordCon = new RecordSearchCondition();
            recordCon.SettleDateTime = optLog.SettleDateTime;

            Application app = new Application();
            Workbook book = null;
            book = app.Workbooks.Add(ReportModal);
            Worksheet sheet = book.ActiveSheet as Worksheet;
            FillOperatorLog(sheet, optLog, 3);
            List<MonthCardPaymentInfo> cardPaymentRecords = GetMonthCardPaymentInfoes(recordCon);
            FillDetail(sheet, cardPaymentRecords, 6);
            sheet.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            book.Close(false, Type.Missing, Type.Missing);
        }

        #region 私有方法
        private void FillOperatorLog(Worksheet sheet, OperatorSettleLog optLog, int row)
        {
            Range r = sheet.get_Range("A" + row, Type.Missing);
            r.Value2 = optLog.OperatorID;
            r = sheet.get_Range("B" + row, Type.Missing);
            r.Value2 = optLog.SettleDateTime;
            r = sheet.get_Range("C" + row, Type.Missing);
            r.Value2 = optLog.CashOfCard + optLog.CashOfCardLost + optLog.CashOfDeposit + optLog.CashOfCardRecycle;
            r = sheet.get_Range("D" + row, Type.Missing);
            r.Value2 = optLog.HandInCash;
            r = sheet.get_Range("E" + row, Type.Missing);
            r.Value2 = optLog.CashDiffrence;
            r = sheet.get_Range("F" + row, Type.Missing);
            r.Value2 = optLog.CashParkDiscount;
        }

        private void FillDetail(Worksheet sheet, List<MonthCardPaymentInfo> items, int row)
        {
            Range r = null;
            int firstRow = row;
            foreach (MonthCardPaymentInfo record in items)
            {
                if (record.Cash == 0 && record.NonCash == 0) continue;
                try
                {
                    r = sheet.get_Range("A" + row, Type.Missing);
                    r.Value2 = record.CardID;

                    r = sheet.get_Range("B" + row, Type.Missing);
                    r.Value2 = record.OwnerName;

                    r = sheet.get_Range("C" + row, Type.Missing);
                    r.Value2 = record.CarPlate;

                    r = sheet.get_Range("D" + row, Type.Missing);
                    r.Value2 = record.PaymentDateTime;

                    r = sheet.get_Range("E" + row, Type.Missing);
                    r.Value2 = record.OldExpireDate;

                    r = sheet.get_Range("F" + row, Type.Missing);
                    r.Value2 = record.NewExpireDate;

                    r = sheet.get_Range("G" + row, Type.Missing);
                    r.Value2 = record.PaymentType;

                    r = sheet.get_Range("H" + row, Type.Missing);
                    r.Value2 = record.Cash;

                    r = sheet.get_Range("I" + row, Type.Missing);
                    r.Value2 = record.NonCash;

                    r = sheet.get_Range("J" + row, Type.Missing);
                    r.Value2 = record.Amount;
                    row++;
                }
                catch
                {
                }
            }
            r = sheet.get_Range("A" + row, Type.Missing);
            r.Value2 = "合计";

            r = sheet.get_Range("B" + row, Type.Missing);
            r.Value2 = string.Empty;

            r = sheet.get_Range("C" + row, Type.Missing);
            r.Value2 = string.Empty;

            r = sheet.get_Range("D" + row, Type.Missing);
            r.Value2 = string.Empty;

            r = sheet.get_Range("E" + row, Type.Missing);
            r.Value2 = string.Empty;

            r = sheet.get_Range("F" + row, Type.Missing);
            r.Value2 = string.Empty;

            r = sheet.get_Range("G" + row, Type.Missing);
            r.Value2 = string.Empty;

            r = sheet.get_Range("H" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.Cash);

            r = sheet.get_Range("I" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.NonCash);

            r = sheet.get_Range("J" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.Amount);

            r = sheet.get_Range("A" + firstRow, "J" + row);
            AddBorder(r);
        }

        private void AddBorder(Range r)
        {
            r.Borders.LineStyle = XlLineStyle.xlContinuous;
        }

        private List<MonthCardPaymentInfo> GetMonthCardPaymentInfoes(RecordSearchCondition recordCon)
        {
            CardBll cbll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            List<MonthCardPaymentInfo> items = new List<MonthCardPaymentInfo>();
            List<CardReleaseRecord> releases = cbll.GetCardReleaseRecords(recordCon).QueryObjects;
            foreach (CardReleaseRecord record in releases)
            {
                MonthCardPaymentInfo mci = new MonthCardPaymentInfo
                {
                    CardID = record.CardID,
                    OwnerName = record.OwnerName,
                    CarPlate = record.CarPlate,
                    PaymentDateTime = record.ReleaseDateTime,
                    OldExpireDate = record.ReleaseDateTime.ToString("yyyy-MM-dd"),
                    NewExpireDate = record.ValidDate.ToString("yyyy-MM-dd"),
                    PaymentType = "发行",
                    Cash = record.PaymentMode == PaymentMode.Cash ? record.ReleaseMoney - record.Deposit : 0,
                    NonCash = record.PaymentMode == PaymentMode.Cash ? 0 : record.ReleaseMoney - record.Deposit,
                };
                items.Add(mci);
                mci = new MonthCardPaymentInfo
                {
                    CardID = record.CardID,
                    OwnerName = record.OwnerName,
                    CarPlate = record.CarPlate,
                    PaymentDateTime = record.ReleaseDateTime,
                    OldExpireDate = string.Empty,
                    NewExpireDate = string.Empty,
                    PaymentType = "押金",
                    Cash = record.PaymentMode == PaymentMode.Cash ? record.Deposit : 0,
                    NonCash = record.PaymentMode == PaymentMode.Cash ? 0 : record.Deposit,
                };
                items.Add(mci);
            }

            List<CardDeferRecord> defers = cbll.GetCardDeferRecords(recordCon).QueryObjects;
            foreach (CardDeferRecord record in defers)
            {
                MonthCardPaymentInfo mci = new MonthCardPaymentInfo
                {
                    CardID = record.CardID,
                    OwnerName = record.OwnerName,
                    CarPlate = record.CarPlate,
                    PaymentDateTime = record.DeferDateTime,
                    OldExpireDate = record.OriginalDate.ToString("yyyy-MM-dd"),
                    NewExpireDate = record.CurrentDate.ToString("yyyy-MM-dd"),
                    PaymentType = "延期",
                    Cash = record.PaymentMode == PaymentMode.Cash ? record.DeferMoney : 0,
                    NonCash = record.PaymentMode == PaymentMode.Cash ? 0 : record.DeferMoney,
                };
                items.Add(mci);
            }

            List<CardChargeRecord> charges = cbll.GetCardChargeRecords(recordCon).QueryObjects;
            foreach (CardChargeRecord record in charges)
            {
                MonthCardPaymentInfo mci = new MonthCardPaymentInfo
                {
                    CardID = record.CardID,
                    OwnerName = record.OwnerName,
                    CarPlate = record.CarPlate,
                    PaymentDateTime = record.ChargeDateTime,
                    OldExpireDate = string.Empty,
                    NewExpireDate = string.Empty,
                    PaymentType = "充值",
                    Cash = record.PaymentMode == PaymentMode.Cash ? record.Payment : 0,
                    NonCash = record.PaymentMode == PaymentMode.Cash ? 0 : record.Payment,
                };
                items.Add(mci);
            }

            List<CardRecycleRecord> recycles = cbll.GetCardRecycleRecords(recordCon).QueryObjects;
            foreach (CardRecycleRecord record in recycles)
            {
                MonthCardPaymentInfo mci = new MonthCardPaymentInfo
                {
                    CardID = record.CardID,
                    OwnerName = record.OwnerName,
                    CarPlate = record.CarPlate,
                    PaymentDateTime = record.RecycleDateTime,
                    OldExpireDate = string.Empty,
                    NewExpireDate = string.Empty,
                    PaymentType = "返还押金",
                    Cash = -record.RecycleMoney,
                    NonCash = 0,
                };
                items.Add(mci);
            }

            List<CardLostRestoreRecord> lostRecords = cbll.GetCardLostRestoreRecords(recordCon).QueryObjects;
            foreach (CardLostRestoreRecord record in lostRecords)
            {
                MonthCardPaymentInfo mci = new MonthCardPaymentInfo
                {
                    CardID = record.CardID,
                    OwnerName = record.OwnerName,
                    CarPlate = record.CarPlate,
                    PaymentDateTime = record.LostDateTime,
                    OldExpireDate = string.Empty,
                    NewExpireDate = string.Empty,
                    PaymentType = "卡片挂失",
                    Cash = record.PaymentMode == PaymentMode.Cash ? (record.LostCardCost != null ? record.LostCardCost.Value : 0) : 0,
                    NonCash = record.PaymentMode != PaymentMode.Cash ? (record.LostCardCost != null ? record.LostCardCost.Value : 0) : 0,
                };
                items.Add(mci);
            }
            return items;
        }
        #endregion
    }

    #region 私有类
    class MonthCardPaymentInfo
    {
        public string CardID { get; set; }

        public string OwnerName { get; set; }

        public string CarPlate { get; set; }

        public DateTime PaymentDateTime { get; set; }

        public string OldExpireDate { get; set; }

        public string NewExpireDate { get; set; }

        public string PaymentType { get; set; }

        public decimal Cash { get; set; }

        public decimal NonCash { get; set; }

        public decimal Amount
        {
            get
            {
                return Cash + NonCash;
            }
        }
    }
    #endregion
}
