using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Configuration;

namespace ExcelExport
{
    /// <summary>
    /// 用于把操作员当班信息保存到EXCEL中
    /// </summary>
    public class CardPaymentReportToExcel
    {
        public CardPaymentReportToExcel(string modalPath)
        {
            ReportModal = modalPath;
        }

        /// <summary>
        /// 获取或设置EXCEL模板的路径
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

            List<CardPaymentInfo> cardPaymentRecords = (new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect)).GetItems(recordCon).QueryObjects;
            cardPaymentRecords = (from card in cardPaymentRecords
                                  where card.PaymentCode != PaymentCode.APM
                                  orderby card.PaymentCode,card.ChargeDateTime descending
                                  select card).ToList();
            FillDetail(sheet, cardPaymentRecords, 6);
            book.SaveAs(path, XlFileFormat.xlXMLSpreadsheet, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            book.Close(false, Type.Missing, Type.Missing);
        }

        /// <summary>
        /// 导出操作员当班信息到EXCEL后直接打印
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

            List<CardPaymentInfo> cardPaymentRecords = (new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect)).GetItems(recordCon).QueryObjects;
            cardPaymentRecords = (from card in cardPaymentRecords 
                                  where card.PaymentCode!=PaymentCode.APM
                                  orderby card.PaymentCode, card.ChargeDateTime descending
                                  select card).ToList();
            FillDetail(sheet, cardPaymentRecords, 6);
            sheet.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            book.Close(false, Type.Missing, Type.Missing);
        }

        #region 私有方法
        private void FillOperatorLog(Worksheet sheet, OperatorSettleLog optLog, int row)
        {
            Range r = sheet.get_Range("A" + row, Type.Missing);
            r.Value2 = optLog.OperatorID;
            r = sheet.get_Range("C" + row, Type.Missing);
            r.Value2 = optLog.SettleDateTime;
            r = sheet.get_Range("E" + row, Type.Missing);
            r.Value2 = optLog.CashParkFact;
            r = sheet.get_Range("F" + row, Type.Missing);
            r.Value2 = optLog.CashOperatorCard;
            r = sheet.get_Range("G" + row, Type.Missing);
            r.Value2 = optLog.HandInCash;
            r = sheet.get_Range("I" + row, Type.Missing);
            r.Value2 = optLog.CashDiffrence;
            r = sheet.get_Range("K" + row, Type.Missing);
            r.Value2 = optLog.CashParkDiscount;
        }

        private void FillDetail(Worksheet sheet, List<CardPaymentInfo> items, int row)
        {
            Range r = null;
            int firstRow = row;
            foreach (CardPaymentInfo record in items)
            {
                try
                {
                    r = sheet.get_Range("A" + row, Type.Missing);
                    r.Value2 = record.CardID;
                    r = sheet.get_Range("B" + row, Type.Missing);
                    r.Value2 = record.CardType.ToString();

                    r = sheet.get_Range("C" + row, Type.Missing);
                    r.Value2 = CarTypeSetting.Current.GetDescription(record.CarType);

                    r = sheet.get_Range("D" + row, Type.Missing);
                    r.Value2 = record.CarPlate;

                    r = sheet.get_Range("E" + row, Type.Missing);
                    r.Value2 = record.EnterDateTime != null ? record.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;

                    r = sheet.get_Range("F" + row, Type.Missing);
                    r.Value2 = record.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    r = sheet.get_Range("G" + row, Type.Missing);
                    r.Value2 = Ralid.Park.BusinessModel.Resouce.PaymentCodeDescription.GetDescription(record.PaymentCode);

                    r = sheet.get_Range("H" + row, Type.Missing);  //应收
                    r.Value2 = record.Accounts;

                    r = sheet.get_Range("I" + row, Type.Missing);  //现金
                    r.Value2 = record.PaymentMode == PaymentMode.Cash ? record.Paid : 0;

                    r = sheet.get_Range("J" + row, Type.Missing); //其它支付
                    r.Value2 = record.PaymentMode == PaymentMode.Cash ? 0 : record.Paid;

                    r = sheet.get_Range("K" + row, Type.Missing);  //折扣
                    r.Value2 = record.Discount;

                    r = sheet.get_Range("L" + row, Type.Missing);
                    r.Value2 = record.Memo;
                    row++;
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }

            r = sheet.get_Range("A" + row, Type.Missing);
            r.Value2 = "电脑收费合计";
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
            r.Value2 = items.Sum(c => c.PaymentCode == PaymentCode.Computer ? c.Accounts : 0);
            r = sheet.get_Range("I" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.PaymentMode == PaymentMode.Cash && c.PaymentCode == PaymentCode.Computer ? c.Paid : 0);
            r = sheet.get_Range("J" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.PaymentMode != PaymentMode.Cash && c.PaymentCode == PaymentCode.Computer ? c.Paid : 0);
            r = sheet.get_Range("K" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.Discount);
            r = sheet.get_Range("L" + row, Type.Missing);
            r.Value2 = string.Empty;

            row++;

            r = sheet.get_Range("A" + row, Type.Missing);
            r.Value2 = "功能卡收费合计";
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
            r.Value2 = items.Sum(c => c.PaymentCode == PaymentCode.FunctionCard ? c.Accounts : 0);
            r = sheet.get_Range("I" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.PaymentMode == PaymentMode.Cash && c.PaymentCode == PaymentCode.FunctionCard ? c.Paid : 0);
            r = sheet.get_Range("J" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.PaymentMode != PaymentMode.Cash && c.PaymentCode == PaymentCode.FunctionCard ? c.Paid : 0);
            r = sheet.get_Range("K" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.Discount);
            r = sheet.get_Range("L" + row, Type.Missing);
            r.Value2 = string.Empty;

            row++;


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
            r.Value2 = items.Sum(c => c.Accounts);
            r = sheet.get_Range("I" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.PaymentMode == PaymentMode.Cash ? c.Paid : 0);
            r = sheet.get_Range("J" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.PaymentMode != PaymentMode.Cash ? c.Paid : 0);
            r = sheet.get_Range("K" + row, Type.Missing);
            r.Value2 = items.Sum(c => c.Discount);
            r = sheet.get_Range("L" + row, Type.Missing);
            r.Value2 = string.Empty;

            r = sheet.get_Range("A" + firstRow, "L" + row);
            AddBorder(r);
        }

        private void AddBorder(Range r)
        {
            r.Borders.LineStyle = XlLineStyle.xlContinuous;
        }
        #endregion
    }
}
