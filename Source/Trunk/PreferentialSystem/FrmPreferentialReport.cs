using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UI.ReportAndStatistics;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.UserControls;

namespace PreferentialSystem
{
    public partial class FrmPreferentialReport : FrmReportBase
    {
        public FrmPreferentialReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
            customDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //customDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        #region 私有变量
        private PREPreferentialLogBll _LogBLL = new PREPreferentialLogBll(AppSettings.CurrentSetting.ParkConnect);
        #endregion

        private void FrmPreferentialReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.cmbBusiness.Init();
            this.txtHour.Text = string.Empty;
        }

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            PreferentialReportSearchCondition con = new PreferentialReportSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CardID = this.txtCardID.Text.Trim();
            con.BusinessName = this.cmbBusiness.Text.Trim();
            con.StationIDs = this.txtWorkstations.Tag as List<string>;
            con.OperatorNames = this.txtOperators.Tag as List<string>;
            //附加查询条件
            con.CancelReason = this.txtCancelReason.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtHour.Text))
            {
                try
                {
                    con.Hour = Convert.ToInt32(this.txtHour.Text.Trim());
                }
                catch { }
            }

            QueryResultList<PREPreferentialLog> result = _LogBLL.GetPreferentials(con);
            if (result.Result == ResultCode.Successful)
            {
                ShowReportsOnGrid(result.QueryObjects);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void ShowReportsOnGrid(List<PREPreferentialLog> items)
        {
            int totalHours = 0;
            int totalCancelHours = 0;
            //decimal paid = 0;
            //decimal discount = 0;
            this.GridView.Rows.Clear();
            items = (from PREPreferentialLog cr in items
                     orderby cr.OperatorTime descending
                     select cr).ToList();
            foreach (PREPreferentialLog info in items)
            {
                //if (info.Paid == 0 && this.rdPaid.Checked) continue; //只查询收费记录时排除掉免费记录

                //if (info.Paid > 0 && this.rdFree.Checked) continue;  //只查询免费记录时排除掉收费记录

                int row = GridView.Rows.Add();
                ShowPreferentialLogOnRow(info, row);
                //paid += info.Paid;
                //discount += info.Discount;
                if (info.IsCancel == 1)
                    totalCancelHours += info.PreferentialHour;
                else
                    totalHours += info.PreferentialHour;
            }
            this.txtTotal.Text = (totalHours - totalCancelHours).ToString();
            if (!string.IsNullOrEmpty(this.txtCancelReason.Text)) this.txtTotal.Text = "0";
            //this.txtPaid.Text = paid.ToString();
            //this.txtAccounts.Text = discount.ToString();
        }

        private void ShowPreferentialLogOnRow(PREPreferentialLog info, int row)
        {
            GridView.Rows[row].Tag = info;
            GridView.Rows[row].Cells["colCardID"].Value = info.CardID;
            GridView.Rows[row].Cells["colEntranceTime"].Value = info.EntranceTime;
            GridView.Rows[row].Cells["colPreferentialHour"].Value = info.PreferentialHour;
            GridView.Rows[row].Cells["colBusiness1"].Value = info.BusinessesName1;
            GridView.Rows[row].Cells["colCost1"].Value = info.BusinessesMoney1;
            GridView.Rows[row].Cells["colBusiness2"].Value = info.BusinessesName2;
            GridView.Rows[row].Cells["colCost2"].Value = info.BusinessesMoney2;
            GridView.Rows[row].Cells["colBusiness3"].Value = info.BusinessesName3;
            GridView.Rows[row].Cells["colCost3"].Value = info.BusinessesMoney3;
            GridView.Rows[row].Cells["colNotes"].Value = info.Notes;
            GridView.Rows[row].Cells["colWorkstationName"].Value = info.WorkstationName;
            GridView.Rows[row].Cells["colOperator"].Value = info.Operator.OperatorName;
            GridView.Rows[row].Cells["colOperatorTime"].Value = info.OperatorTime;
            GridView.Rows[row].Cells["colIsCancel"].Value = info.IsCancel == 1 ? "是" : string.Empty;
            GridView.Rows[row].Cells["colCancelReason"].Value = info.CancelReason;
        }

        private void btnSelectColumns_Click(object sender, EventArgs e)
        {
            FrmColumnSelection frm = new FrmColumnSelection();
            frm.Selectee = this.GridView;
            frm.SelectedColumns = GetAllVisiableColumns();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string[] cols = frm.SelectedColumns;
                if (cols != null && cols.Length > 0)
                {
                    string temp = string.Join(",", cols);
                    AppSettings.CurrentSetting.SaveConfig(string.Format("{0}_Columns", this.GetType().Name), temp);
                    InitGridViewColumns();
                }
            }
        }

        private string[] GetAllVisiableColumns()
        {
            if (GridView != null)
            {
                List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
                foreach (DataGridViewColumn col in GridView.Columns)
                {
                    if (col.Visible) cols.Add(col);
                }
                return (from col in cols
                        orderby col.DisplayIndex ascending
                        select col.Name).ToArray();
            }
            return null;
        }

        private void InitGridViewColumns()
        {
            DataGridView grid = this.GridView;
            if (grid == null) return;
            string temp = AppSettings.CurrentSetting.GetConfigContent(string.Format("{0}_Columns", this.GetType().Name));
            if (string.IsNullOrEmpty(temp)) return;
            List<string> cols = temp.Split(',').ToList();

            foreach (DataGridViewColumn c in grid.Columns)
            {
                int index = cols.IndexOf(c.Name);
                c.Visible = index >= 0;
                if (index >= 0)
                {
                    c.DisplayIndex = index;
                }
            }
        }

        private void mnu_PrintRecord_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = GridView.SelectedRows[0];
                PREPreferentialLog Log = row.Tag as PREPreferentialLog;
                if (Log != null)
                {
                    string path = string.Empty;
                    string modal = System.IO.Path.Combine(Application.StartupPath, @"ReportModal\PreferentialLogReportModal.xls");
                    ExcelExport.MonthCardReportToExcel ose = null;
                    if (System.IO.File.Exists(modal))
                    {
                        ose = new ExcelExport.MonthCardReportToExcel(modal);
                        ose.PrintByExcel(Log);
                    }
                    else
                    {
                        MessageBox.Show("未找到打印模板！", "提示");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void customDataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    DataGridView.HitTestInfo info = this.customDataGridView1.HitTest(e.X, e.Y);
            //    if (info.RowIndex >= 0)
            //    {
            //        this.customDataGridView1.SelectNone();
            //        this.customDataGridView1.Rows[info.RowIndex].Selected = true;
            //        this.customDataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            //    }
            //    else
            //    {
            //        this.customDataGridView1.ContextMenuStrip = null;
            //    }
            //}
        }

        private void lblOperators_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPREOperatorSelect frm = new FrmPREOperatorSelect();
            frm.SelectionItems = this.txtOperators.Tag as List<string>;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtOperators.Tag = frm.SelectionItems;
                this.txtOperators.Text = string.Join(";", frm.SelectionItems);
            }
        }

        private void lblWorkstations_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPREWorksationSelect frm = new FrmPREWorksationSelect();
            frm.SelectionItems = this.txtWorkstations.Tag as List<string>;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtWorkstations.Tag = frm.SelectionItems;
                this.txtWorkstations.Text = string.Join(";", frm.SelectionItems);
            }
        }

        /// <summary>
        /// 另存为按钮事件

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAs2_Click(object sender, EventArgs e)
        {//父类的按钮已被隐藏，子类重新写新的私有事件

            try
            {
                DataGridView view = this.GridView;
                if (view != null)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Excel文档|*.xls|所有文件(*.*)|*.*";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog1.FileName;
                        if (Ralid.GeneralLibrary.WinformControl.DataGridViewExporter.Export(view, path, false))
                        {
                            MessageBox.Show("导出成功");
                        }
                        else
                        {
                            MessageBox.Show("保存到电子表格时出现错误!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string title = string.Format("优惠记录明细   优惠总时数：{0}小时   打印操作员：{1}   打印工作站：{2}",this.txtTotal.Text.Trim(),PREOperatorInfo.CurrentOperator.OperatorName,PRESysOptionSetting.Current.PRESysOption.CurrentWorkstation);
            PrintDataGridView.Print_DataGridView(this.customDataGridView1, title);
        }

    }
}
