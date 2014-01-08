using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmOperatorLogReport : FrmReportBase
    {
        public FrmOperatorLogReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }

        private void FrmOperatorLogReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.operatorCombobox1.Init();
            this.comOperator.Init();
        }

        private void FrmOperatorLogReport_Activated(object sender, EventArgs e)
        {
            this.mnu_ExportCardPayment.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.ExportCardPayment);
            this.mnu_ExportMonthCardPaymentReport.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.ExportMonthCardPaymentReport);
            this.mnu_PrintCardPayment.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.PrintCardPayment);
            this.mnu_PrintMonthCardPayment.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.PrintMonthCardPaymentReport);
            this.mnu_PrintSettleLog.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.PrintOperatorSettleLog);
            comOperator_SelectedIndexChanged(this.comOperator, EventArgs.Empty);
        }

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            RecordSearchCondition con = new RecordSearchCondition();
            con.Operator = this.operatorCombobox1.SelectecOperator;
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;

            OperatorSettleBLL bll = new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect);
            List<OperatorSettleLog> items = bll.GetOperatorLogs(con).QueryObjects;
            ShowReportsOnGrid(items);
        }

        private void ShowReportsOnGrid(List<OperatorSettleLog> items)
        {
            this.GridView.Rows.Clear();
            items = (from OperatorSettleLog cr in items
                     orderby cr.SettleFrom descending
                     select cr).ToList();
            foreach (OperatorSettleLog record in items)
            {
                if (record.SettleDateTime != null)
                {
                    int index = GridView.Rows.Add();
                    DataGridViewRow row = GridView.Rows[index];
                    ShowOperatorSettleOnRow(record, row);
                }
            }
        }

        private static void ShowOperatorSettleOnRow(OperatorSettleLog record, DataGridViewRow row)
        {
            row.Tag = record;
            row.Cells["colOperatorID"].Value = record.OperatorID;
            row.Cells["colSettleDateTime"].Value = record.SettleDateTime;
            row.Cells["colCashParkFact"].Value = record.CashParkFact;
            row.Cells["colCashOperatorCard"].Value = record.CashOperatorCard;
            row.Cells["colCashDiscount"].Value = record.CashParkDiscount;
            row.Cells["colCashOfCard"].Value = record.CashOfCard;
            row.Cells["colCashOfDeposit"].Value = record.CashOfDeposit;
            row.Cells["colCashOfCardRecycle"].Value = record.CashOfCardRecycle;
            row.Cells["colCashOfCardLost"].Value = record.CashOfCardLost;
            row.Cells["colTotalCash"].Value = record.TotalCash;
            row.Cells["colHandInCash"].Value = record.HandInCash == null ? string.Empty : record.HandInCash.Value.ToString("F2");
            row.Cells["colCashDiffrence"].Value = record.CashDiffrence == null ? string.Empty : record.CashDiffrence.Value.ToString("F2");
            row.Cells["colNonCashParkFact"].Value = record.NonCashParkFact;
            row.Cells["colNonCashDiscount"].Value = record.NonCashParkDiscount;
            row.Cells["colNonCashOfCard"].Value = record.NonCashOfCard;
            row.Cells["colNonCashOfDeposit"].Value = record.NonCashOfDeposit;
            row.Cells["colNonCashOfCardLost"].Value = record.NonCashOfCardLost;
            row.Cells["colTotalNonCash"].Value = record.TotalNonCash;
            row.Cells["colOpenDoorCount"].Value = record.OpenDoorCount;
            row.Cells["colTempCardRecycle"].Value = record.TempCardRecycle;
        }

        private void customDataGridview1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = this.customDataGridview1.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0)
                {
                    this.customDataGridview1.SelectNone();
                    this.customDataGridview1.Rows[info.RowIndex].Selected = true;
                    this.customDataGridview1.ContextMenuStrip = this.contextMenuStrip1;
                }
                else
                {
                    this.customDataGridview1.ContextMenuStrip = null;
                }
            }
        }

        private void mnu_ExportCardPayment_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = GridView.SelectedRows[0];
                OperatorSettleLog optLog = row.Tag as OperatorSettleLog;
                if (optLog != null)
                {
                    string path = string.Empty;
                    string modal = System.IO.Path.Combine(Application.StartupPath, @"ReportModal\CardPaymentReportModal.xls");
                    path = System.IO.Path.Combine(TempFolderManager.GetCurrentFolder(), Guid.NewGuid().ToString() + ".xls");
                    ExcelExport.CardPaymentReportToExcel ose = null;
                    if (System.IO.File.Exists(modal))
                    {
                        ose = new ExcelExport.CardPaymentReportToExcel(modal);
                        ose.Export(optLog, path);
                        System.Diagnostics.Process.Start(path);
                    }
                    else
                    {
                        MessageBox.Show(Resources.Resource1.FrmOperatorLogReport_NotFind, Resources.Resource1.Form_Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Resource1.Form_Alert);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void mnu_ExportMonthCardPaymentReport_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = GridView.SelectedRows[0];
                OperatorSettleLog optLog = row.Tag as OperatorSettleLog;
                if (optLog != null)
                {
                    string path = string.Empty;
                    string modal = System.IO.Path.Combine(Application.StartupPath, @"ReportModal\MonthCardPaymentReportModal.xls");
                    path = System.IO.Path.Combine(TempFolderManager.GetCurrentFolder(), Guid.NewGuid().ToString() + ".xls");
                    ExcelExport.MonthCardReportToExcel ose = null;
                    if (System.IO.File.Exists(modal))
                    {
                        ose = new ExcelExport.MonthCardReportToExcel(modal);
                        ose.Export(optLog, path);
                        System.Diagnostics.Process.Start(path);
                    }
                    else
                    {
                        MessageBox.Show(Resources.Resource1.FrmOperatorLogReport_NotFind, Resources.Resource1.Form_Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Resource1.Form_Alert);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void mnu_PrintCardPayment_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = GridView.SelectedRows[0];
                OperatorSettleLog optLog = row.Tag as OperatorSettleLog;
                if (optLog != null)
                {
                    string path = string.Empty;
                    string modal = System.IO.Path.Combine(Application.StartupPath, @"ReportModal\CardPaymentReportModal.xls");
                    ExcelExport.CardPaymentReportToExcel ose = null;
                    if (System.IO.File.Exists(modal))
                    {
                        ose = new ExcelExport.CardPaymentReportToExcel(modal);
                        ose.PrintByExcel(optLog);
                    }
                    else
                    {
                        MessageBox.Show(Resources.Resource1.FrmOperatorLogReport_NotFind, Resources.Resource1.Form_Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Resource1.Form_Alert);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void mnu_PrintMonthCardPayment_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = GridView.SelectedRows[0];
                OperatorSettleLog optLog = row.Tag as OperatorSettleLog;
                if (optLog != null)
                {
                    string path = string.Empty;
                    string modal = System.IO.Path.Combine(Application.StartupPath, @"ReportModal\MonthCardPaymentReportModal.xls");
                    ExcelExport.MonthCardReportToExcel ose = null;
                    if (System.IO.File.Exists(modal))
                    {
                        ose = new ExcelExport.MonthCardReportToExcel(modal);
                        ose.PrintByExcel(optLog);
                    }
                    else
                    {
                        MessageBox.Show(Resources.Resource1.FrmOperatorLogReport_NotFind, Resources.Resource1.Form_Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Resource1.Form_Alert);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            if (comOperator.SelectecOperator != null)
            {
                decimal? handInCash = null;
                CardInfo operatorCard = null;

                if (UserSetting.Current.OperatorCardCashWhenSettle && AppSettings.CurrentSetting.EnableWriteCard)
                {
                    FrmOperatorCardCashComfirm frmOperator = new FrmOperatorCardCashComfirm();
                    frmOperator.Operator = comOperator.SelectecOperator;
                    if (frmOperator.ShowDialog() == DialogResult.OK)
                    {
                        operatorCard = frmOperator.OperatorCard;
                    }
                }
                if (UserSetting.Current.InputHandInCashWhenSettle)
                {
                    FrmHandInCashConfirm frm = new FrmHandInCashConfirm();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        handInCash = frm.HandInCash;
                    }
                    else
                    {
                        return;
                    }
                }
                FrmOperatorSettle frmShift = new FrmOperatorSettle();
                frmShift.Operator = comOperator.SelectecOperator;
                frmShift.HandInCash = handInCash;
                frmShift.OperatorCard = operatorCard;
                if (frmShift.ShowDialog() == DialogResult.OK)
                {
                    int index = GridView.Rows.Add();
                    DataGridViewRow row = GridView.Rows[index];
                    ShowOperatorSettleOnRow(frmShift.SettledLog, row);
                }
            }
        }

        private void comOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comOperator.OperatorID == OperatorInfo.CurrentOperator.OperatorID && !OperatorInfo.CurrentOperator.Permit(Permission.OperatorSettle)) ||
                   (comOperator.OperatorID != OperatorInfo.CurrentOperator.OperatorID && !OperatorInfo.CurrentOperator.Permit(Permission.OtherOperatorSettle)))
            {
                btnSettle.Enabled = false;
            }
            else
            {
                btnSettle.Enabled = true;
            }
        }

        private void mnu_PrintSettleLog_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = GridView.SelectedRows[0];
                OperatorSettleLog optLog = row.Tag as OperatorSettleLog;
                if (optLog != null)
                {
                    FrmOperatorSettle frm = new FrmOperatorSettle();
                    frm.PrintOperatorSettleLog(optLog);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Resource1.Form_Alert);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }


    }
}
