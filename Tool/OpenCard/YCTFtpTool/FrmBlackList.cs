using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;

namespace Ralid.OpenCard.YCTFtpTool
{
    public partial class FrmBlackList : Form
    {
        public FrmBlackList()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<YCTBlacklist> bls = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(null).QueryObjects ;
            if (!string.IsNullOrEmpty(txtCardID.Text)) bls = bls.Where(it => it.CardID.Contains(txtCardID.Text)).ToList ();
            dataGridView1.Rows.Clear();
            foreach (var bl in bls)
            {
                int row = dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells["colCardID"].Value = bl.CardID;
                dataGridView1.Rows[row].Cells["colReason"].Value = GetReason(bl.Reason);
            }
            lblMsg.Text = string.Format("共有 {0} 项", bls != null ? bls.Count : 0);
        }

        private string GetReason(string code)
        {
			if(code=="A")return "非法卡";
			if(code=="B")return "已挂失";
			if(code=="D")return "记帐卡止付";
			if(code=="E")return "记账卡欠费";
			if(code=="G")return "记名卡止付";
			if(code =="I")return "卡已锁死";
			if(code=="J")return "流水断序";
			if(code=="K")return "无充值记录";
			if(code=="L")return "非法充值";
            return code;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView view = this.dataGridView1;
                if (view != null)
                {
                    SaveFileDialog dig = new SaveFileDialog();
                    dig.Filter = "Excel文档|*.xls;*.xlsx|所有文件(*.*)|*.*";
                    dig.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    if (dig.ShowDialog() == DialogResult.OK)
                    {
                        string path = dig.FileName;
                        NPOIExcelHelper.Export(view, path);
                        MessageBox.Show("导出成功");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存到电子表格时出现错误!");
            }
        }
    }
}
