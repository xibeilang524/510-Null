using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmYangChenTongLogReport : FrmReportBase
    {
        public FrmYangChenTongLogReport()
        {
            InitializeComponent();
        }

        #region 重写基类方法
        protected override void OnItemSearching(EventArgs e)
        {
            GridView.Rows.Clear();
            RecordSearchCondition con = new RecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange(ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            List<YangChenTongLog> items = (new YangChenTongLogBll(AppSettings.CurrentSetting.ParkConnect)).GetItems(con).QueryObjects;
            foreach (YangChenTongLog item in items)
            {
                int row = this.GridView.Rows.Add();
                ShowPayOperationLogOnRow(GridView.Rows[row], item);
            }
        }

        private void ShowPayOperationLogOnRow(DataGridViewRow row, YangChenTongLog item)
        {
            row.Tag = item;
            row.Cells["colLogID"].Value = item.LogID;
            row.Cells["colLogDateTime"].Value = item.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colCardID"].Value = item.CardID;
            row.Cells["colLogicalID"].Value = item.LogicalID;
            row.Cells["colPayment"].Value = item.Payment;
            row.Cells["colBalance"].Value = item.Balance;
        }
        #endregion

        private void FrmYangChenTongLogReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.ucDateTimeInterval1.SelectToday();
        }

        private void btnExportToRecord_Click(object sender, EventArgs e)
        {
            if (this.GridView.Rows.Count > 0)
            {
                try
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "文本文档|*.txt";
                    saveFileDialog1.FileName = "Record.txt";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog1.FileName;
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            using (StreamWriter bw = new StreamWriter(fs, System.Text.ASCIIEncoding.ASCII))
                            {
                                foreach (DataGridViewRow row in this.GridView.Rows)
                                {
                                    bw.WriteLine((row.Tag as YangChenTongLog).Data);
                                }
                            }
                        }
                        MessageBox.Show(Resources.Resource1.FrmReportBase_SaveSuccess);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
