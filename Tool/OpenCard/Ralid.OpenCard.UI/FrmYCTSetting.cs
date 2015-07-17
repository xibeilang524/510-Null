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
        private void ShowItemOnRow(DataGridViewRow row, string ip, string entranceName, int? entranceID, string memo)
        {
            row.Cells["colReaderIP"].Value = ip;
            row.Cells["colEntrance"].Value = entranceName;
            row.Cells["colEntrance"].Tag = entranceID;
            row.Cells["colMemo"].Value = memo;
        }

        /// <summary>
        /// 获取某读卡器IP在网格中所在行的行号,如果没有找到,返回-1
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private int FindRow(string ip)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["colReaderIP"].Value.ToString() == ip)
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
                dataGridView1.Rows.Clear();
                if (yct.Items != null && yct.Items.Count > 0)
                {
                    foreach (YCTItem item in yct.Items)
                    {
                        EntranceInfo entrance = item.EntranceID != null ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
                        int row = dataGridView1.Rows.Add();
                        ShowItemOnRow(dataGridView1.Rows[row], item.Comport.ToString(), entrance != null ? entrance.EntranceName : string.Empty, entrance != null ? entrance.EntranceID : 0, item.Memo);
                    }
                }
            }
        }

        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmYCTDetail frm = new FrmYCTDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (FindRow(frm.ReaderID) >= 0)
                {
                    MessageBox.Show("ID为 " + frm.ReaderID + " 的读卡器已经存在");
                }
                else
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], frm.ReaderID, frm.EntranceName, frm.EntranceID, frm.Memo);
                }
            }
        }

        private void mnu_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FrmYCTDetail frm = new FrmYCTDetail();
                frm.ReaderID = dataGridView1.SelectedRows[0].Cells["colReaderIP"].Value as string;
                frm.EntranceID = (int)dataGridView1.SelectedRows[0].Cells["colEntrance"].Tag;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (FindRow(frm.ReaderID) != dataGridView1.SelectedRows[0].Index)
                    {
                        MessageBox.Show("ID为 " + frm.ReaderID + " 的读卡器已经存在");
                    }
                    else
                    {
                        ShowItemOnRow(dataGridView1.SelectedRows[0], frm.ReaderID, frm.EntranceName, frm.EntranceID, frm.Memo);
                    }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            YCTSetting yct = new YCTSetting();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells["colEntrance"].Value.ToString()))
                {
                    YCTItem item = new YCTItem
                    {
                        Comport = Convert.ToInt32(row.Cells["colReaderIP"].Value),
                        EntranceID = (int)row.Cells["colEntrance"].Tag,
                        Memo = (string)row.Cells["colMemo"].Value
                    };
                    yct.Items.Add(item);
                }
                else
                {
                    YCTItem item = new YCTItem
                    {
                        Comport = Convert.ToInt32(row.Cells["colReaderIP"].Value),
                        EntranceID = 0,
                        Memo = (string)row.Cells["colMemo"].Value
                    };
                    yct.Items.Add(item);
                }
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
    }
}
