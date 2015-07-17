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
            this.deptComboBox1.Init();
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
            con.Dept = this.deptComboBox1.SelectedDept;
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;

            OperatorSettleBLL bll = new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect);
            List<OperatorSettleLog> items = bll.GetOperatorLogs(con).QueryObjects;
            ShowReportsOnGrid(items);
        }

        private void ShowReportsOnGrid(List<OperatorSettleLog> items)
        {
            decimal totalCash = 0;
            decimal? totalHandInCash = null;
            decimal? totalCashDiffrence = null;
            decimal totalNonCash = 0;
            decimal? totalHandInPOS = null;

            this.GridView.Rows.Clear();
            items = (from OperatorSettleLog cr in items
                     orderby cr.SettleFrom descending
                     select cr).ToList();
            //DeptBll bll = new DeptBll(AppSettings.CurrentSetting.ParkConnect);
            //List<DeptInfo> depts = bll.GetAllDepts().QueryObjects;
            //DeptInfo deptInfo = null;
            foreach (OperatorSettleLog record in items)
            {
                if (record.SettleDateTime != null)
                {
                    int index = GridView.Rows.Add();
                    DataGridViewRow row = GridView.Rows[index];
                    //string dept = string.Empty;
                    //if (record.DeptID != null && depts != null && depts.Count > 0)
                    //{
                    //    if (deptInfo == null || deptInfo.DeptID != record.DeptID.Value)
                    //    {
                    //        //部门信息为空或与记录的部门ID不同时，才去查找部门，减小查找部门的时间
                    //        deptInfo = depts.FirstOrDefault(o => o.DeptID == record.DeptID.Value);
                    //    }
                    //    if (deptInfo != null)
                    //    {
                    //        dept = deptInfo.DeptName;
                    //    }
                    //}
                    ShowOperatorSettleOnRow(record, row);

                    totalCash += record.TotalCash;
                    if (record.HandInCash.HasValue)
                    {
                        if (!totalHandInCash.HasValue) totalHandInCash = 0;
                        totalHandInCash += record.HandInCash.Value;
                    }
                    if (record.CashDiffrence.HasValue)
                    {
                        if (!totalCashDiffrence.HasValue) totalCashDiffrence = 0;
                        totalCashDiffrence += record.CashDiffrence.Value;
                    }
                    totalNonCash += record.TotalNonCash;
                    if (record.HandInPOS.HasValue)
                    {
                        if (!totalHandInPOS.HasValue) totalHandInPOS = 0;
                        totalHandInPOS += record.HandInPOS.Value;
                    }
                }
            }

            this.lblTotalCash.Text = totalCash.ToString("N");
            this.lblTotalHandInCash.Text = totalHandInCash.HasValue ? totalHandInCash.Value.ToString("N") : string.Empty;
            this.lblTotalCashDiffrence.Text = totalCashDiffrence.HasValue ? totalCashDiffrence.Value.ToString("N") : string.Empty;
            this.lblTotalNonCash.Text = totalNonCash.ToString("N");
            this.lblTotalHandInPOS.Text = totalHandInPOS.HasValue ? totalHandInPOS.Value.ToString("N") : string.Empty;
        }

        private static void ShowOperatorSettleOnRow(OperatorSettleLog record, DataGridViewRow row)
        {
            row.Tag = record;
            row.Cells["colOperatorID"].Value = record.OperatorID;
            //row.Cells["colDept"].Value = dept;
            row.Cells["colDept"].Value = record.Dept != null ? record.Dept.DeptName : string.Empty;
            row.Cells["colSettleDateTime"].Value = record.SettleDateTime;
            row.Cells["colCashParkFact"].Value = record.CashParkFact;
            row.Cells["colCashOperatorCard"].Value = record.CashOperatorCard;
            row.Cells["colCashOfPOS"].Value = (record.CashOfPOS == null ? 0 : record.CashOfPOS.Value).ToString("F2");
            row.Cells["colCashDiscount"].Value = record.CashParkDiscount;
            row.Cells["colCashOfCard"].Value = record.CashOfCard;
            row.Cells["colCashOfDeposit"].Value = record.CashOfDeposit;
            row.Cells["colCashOfCardRecycle"].Value = record.CashOfCardRecycle;
            row.Cells["colCashOfCardLost"].Value = record.CashOfCardLost;
            row.Cells["colCashOfRefund"].Value = record.CashOfRefund == null ? "0.00" : record.CashOfRefund.Value.ToString("F2");
            row.Cells["colTotalCash"].Value = record.TotalCash;
            row.Cells["colHandInCash"].Value = record.HandInCash == null ? string.Empty : record.HandInCash.Value.ToString("F2");
            row.Cells["colCashDiffrence"].Value = record.CashDiffrence == null ? string.Empty : record.CashDiffrence.Value.ToString("F2");
            row.Cells["colNonCashParkFact"].Value = record.NonCashParkFact;
            row.Cells["colNonCashDiscount"].Value = record.NonCashParkDiscount;
            row.Cells["colNonCashOfCard"].Value = record.NonCashOfCard;
            row.Cells["colNonCashOfDeposit"].Value = record.NonCashOfDeposit;
            row.Cells["colNonCashOfCardLost"].Value = record.NonCashOfCardLost;
            row.Cells["colTotalNonCash"].Value = record.TotalNonCash;
            row.Cells["colHandInPOS"].Value = record.HandInPOS == null ? string.Empty : record.HandInPOS.Value.ToString("F2");
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
                decimal? handInPOS = null;
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
                        handInPOS = frm.HandInPOS;
                    }
                    else
                    {
                        return;
                    }
                }
                FrmOperatorSettle frmShift = new FrmOperatorSettle();
                frmShift.Operator = comOperator.SelectecOperator;
                frmShift.HandInCash = handInCash;
                frmShift.HandInPOS = handInPOS;
                frmShift.OperatorCard = operatorCard;
                if (frmShift.ShowDialog() == DialogResult.OK)
                {
                    int index = GridView.Rows.Add();
                    DataGridViewRow row = GridView.Rows[index];
                    ////因为是新生成的记录，这里直接使用操作员的部门就可以了
                    //string dept = frmShift.Operator != null && frmShift.Operator.Dept != null ? frmShift.Operator.Dept.DeptName : string.Empty;
                    //ShowOperatorSettleOnRow(frmShift.SettledLog, row, dept);
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
