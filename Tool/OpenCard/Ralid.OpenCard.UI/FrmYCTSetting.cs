using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.OpenCard.OpenCardService;
using Ralid.OpenCard.OpenCardService.YCT;

namespace Ralid.OpenCard.UI
{
    public partial class FrmYCTSetting : Form
    {
        #region 构造函数
        public FrmYCTSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有方法
        private void ShowItemOnRow(DataGridViewRow row, YCTItem item)
        {
            EntranceInfo entrance = item.EntranceID != null ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            row.Cells["colID"].Value = item.ID;
            row.Cells["colComport"].Value = item.Comport;
            row.Cells["colEntrance"].Value = entrance != null ? entrance.EntranceName : string.Empty;
            row.Cells["colMemo"].Value = item.Memo;
            row.Tag = item;
        }

        /// <summary>
        /// 获取某读卡器IP在网格中所在行的行号,如果没有找到,返回-1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int FindRow(string id)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["colID"].Value.ToString() == id)
                {
                    return row.Index;
                }
            }
            return -1;
        }
        #endregion

        #region 事件处理程序
        private void FrmZSTSetting_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            YCTSetting yct = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<YCTSetting>();
            if (yct != null)
            {
                txtServiceCode.IntergerValue = yct.ServiceCode;
                txtReaderCode.IntergerValue = yct.ReaderCode;
                txtFTPServer.Text = yct.FTPServer;
                txtFTPUser.Text = yct.FTPUser;
                txtFTPPwd.Text = yct.FTPPassword;
                dataGridView1.Rows.Clear();
                if (yct.Items != null && yct.Items.Count > 0)
                {
                    foreach (YCTItem item in yct.Items)
                    {
                        int row = dataGridView1.Rows.Add();
                        ShowItemOnRow(dataGridView1.Rows[row], item);
                    }
                }
            }

            string ftpPath = AppSettings.CurrentSetting.GetConfigContent("YCTFtpPath");
            if (string.IsNullOrEmpty(ftpPath)) ftpPath = System.IO.Path.Combine(Application.StartupPath, "羊城通FTP");
            txtFTPPath.Text = ftpPath;
        }

        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmYCTDetail frm = new FrmYCTDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                YCTItem item = frm.YCTItem;
                if (FindRow(item.ID) >= 0)
                {
                    MessageBox.Show("编号为 " + item.ID + " 的读卡器已经存在");
                }
                else
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], item);
                }
            }
        }

        private void mnu_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FrmYCTDetail frm = new FrmYCTDetail();
                frm.YCTItem = dataGridView1.SelectedRows[0].Tag as YCTItem;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    YCTItem item = frm.YCTItem;
                    ShowItemOnRow(dataGridView1.SelectedRows[0], item);
                }
            }
        }

        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要删除吗?", "询问", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
        }

        private bool CheckInput()
        {
            if (txtServiceCode.IntergerValue < 0 || txtServiceCode.IntergerValue > 9999)
            {
                MessageBox.Show("服务商代码设置不正确");
                return false;
            }
            if (txtReaderCode.IntergerValue < 0 || txtReaderCode.IntergerValue > 9999)
            {
                MessageBox.Show("刷卡点代码设置不正确");
                return false;
            }
            if (string.IsNullOrEmpty(txtFTPPath.Text))
            {
                MessageBox.Show("羊城通FTP文件夹没有设置");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;
            AppSettings.CurrentSetting.SaveConfig("YCTFtpPath", txtFTPPath.Text);
            YCTSetting yct = new YCTSetting();
            yct.Items.Clear();
            yct.ServiceCode = txtServiceCode.IntergerValue;
            yct.ReaderCode = txtReaderCode.IntergerValue;
            yct.FTPServer = txtFTPServer.Text;
            yct.FTPUser = txtFTPUser.Text;
            yct.FTPPassword = txtFTPPwd.Text;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                yct.Items.Add(row.Tag as YCTItem);
            }
            CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).SaveSetting<YCTSetting>(yct);
            if (ret.Result == ResultCode.Successful)
            {
                OpenCardMessageHandler handler = GlobalSettings.Current.Get<OpenCardMessageHandler>();
                if (handler != null)
                {
                    handler.Init(yct);
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(ret.Message);
            }
        }
        #endregion

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dig = new FolderBrowserDialog();
            if (dig.ShowDialog() == DialogResult.OK) txtFTPPath.Text = dig.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string zip= YCTUploadFileFactory.CreateM1UploadFile();
            if (!string.IsNullOrEmpty(zip))
            {

            }
        }
    }
}
