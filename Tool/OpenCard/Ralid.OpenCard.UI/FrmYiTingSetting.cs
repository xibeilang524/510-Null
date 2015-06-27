using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.OpenCard.OpenCardService;

namespace Ralid.OpenCard.UI
{
    public partial class FrmYiTingSetting : Form
    {
        #region 构造函数
        public FrmYiTingSetting()
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
            YiTingShanFuSetting yt = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<YiTingShanFuSetting>();
            if (yt != null )
            {
                txtIP.IP = yt.IP;
                txtPort.IntergerValue = yt.Port;
                dataGridView1.Rows.Clear();
                if (yt.Items != null && yt.Items.Count > 0)
                {
                    foreach (YiTingPOS item in yt.Items)
                    {
                        EntranceInfo entrance = item.EntranceID != null ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
                        int row = dataGridView1.Rows.Add();
                        ShowItemOnRow(dataGridView1.Rows[row], item.ID, entrance != null ? entrance.EntranceName : string.Empty, entrance != null ? entrance.EntranceID : 0, item.Memo);
                    }
                }
            }
        }

        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmYiTingDetail frm = new FrmYiTingDetail();
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
                FrmYiTingDetail frm = new FrmYiTingDetail();
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
            DialogResult result = MessageBox.Show("询问", "确定要删除吗?", MessageBoxButtons.YesNo);
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
            YiTingShanFuSetting yt = new YiTingShanFuSetting();
            yt.IP = txtIP.IP;
            yt.Port = txtPort.IntergerValue;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells["colEntrance"].Value.ToString()))
                {
                    YiTingPOS item = new YiTingPOS()
                    {
                        ID = (string)row.Cells["colReaderIP"].Value,
                        EntranceID = (int)row.Cells["colEntrance"].Tag,
                        Memo = (string)row.Cells["colMemo"].Value
                    };
                    yt.Items.Add(item);
                }
                else
                {
                    YiTingPOS item = new YiTingPOS()
                    {
                        ID = (string)row.Cells["colReaderIP"].Value,
                        EntranceID = 0,
                        Memo = (string)row.Cells["colMemo"].Value
                    };
                    yt.Items.Add(item);
                }
            }
            CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).SaveSetting<YiTingShanFuSetting>(yt);
            if (ret.Result == ResultCode.Successful)
            {
                OpenCardMessageHandler handler = GlobalSettings.Current.Get<OpenCardMessageHandler>();
                if (handler != null)
                {
                    handler.Init(yt);
                }
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show(ret.Message);
            }
        }
        #endregion
    }
}
