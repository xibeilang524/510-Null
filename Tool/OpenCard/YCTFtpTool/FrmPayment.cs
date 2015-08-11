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
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.OpenCard.YCTFtpTool
{
    public partial class FrmPayment : Form
    {
        public FrmPayment()
        {
            InitializeComponent();
        }

        private void FrmPayment_Load(object sender, EventArgs e)
        {
            this.UCChargeDateTime.Init();
            this.UCChargeDateTime.SelectToday();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            YCTPaymentRecordSearchCondition con = new YCTPaymentRecordSearchCondition();
            con.PaymentDateTimeRange = new DateTimeRange(UCChargeDateTime.StartDateTime, UCChargeDateTime.EndDateTime);
            con.LCN = txtCardID.Text;
            if (!string.IsNullOrEmpty(cmbWalletType.Text)) con.WalletType = cmbWalletType.SelectedIndex;
            if (!string.IsNullOrEmpty(cmbState.Text))
            {
                if (cmbState.Text == "支付成功") con.State = (int)YCTPaymentRecordState.PaidOk;
                else if (cmbState.Text == "服务器已接收") con.State = (int)YCTPaymentRecordState.ServiceAccepted;
                else if (cmbState.Text == "服务器拒绝") con.State = (int)YCTPaymentRecordState.ServiceDenied;
                else if (cmbState.Text == "支付失败") con.State = (int)YCTPaymentRecordState.PaidFail;
            }
            if (chkUnupload.Checked) con.UnUploaded = true;
            List<YCTPaymentRecord> records = new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(con).QueryObjects;
            if (records != null && records.Count > 0)
            {
                foreach (var record in records)
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], record);
                }
            }
            lblMsg.Text = string.Format("共有 {0} 项", records != null ? records.Count : 0);
        }

        private void ShowItemOnRow(DataGridViewRow row, YCTPaymentRecord record)
        {
            row.Cells["colPID"].Value = record.PID;
            row.Cells["colPSN"].Value = record.PSN;
            row.Cells["colCardID"].Value = record.LCN;
            row.Cells["colFee"].Value = ((decimal)record.FEE / 100).ToString("F2");
            row.Cells["colBAL"].Value = ((decimal)record.BAL / 100).ToString("F2");
            row.Cells["colWalletType"].Value = record.WalletType == 1 ? "M1钱包" : "CPU钱包";
            row.Cells["colTIM"].Value = record.TIM.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colFile"].Value = record.UploadFile;
            row.Cells["colState"].Value = StateString(record.State);
        }

        private string StateString(YCTPaymentRecordState state)
        {
            if (state == YCTPaymentRecordState.PaidFail) return "支付失败";
            if (state == YCTPaymentRecordState.PaidOk) return "支付成功";
            if (state == YCTPaymentRecordState.ServiceAccepted) return "服务器已接收";
            if (state == YCTPaymentRecordState.ServiceDenied) return "服务器拒绝";
            return state.ToString();
        }
    }
}
