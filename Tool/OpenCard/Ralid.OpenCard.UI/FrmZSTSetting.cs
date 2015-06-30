using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ralid.GeneralLibrary .CardReader ;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.OpenCard.OpenCardService;

namespace Ralid.OpenCard.UI
{
    public partial class FrmZSTSetting : Form
    {
        #region 构造函数
        public FrmZSTSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private ZSTReader _Reader = null;
        #endregion

        #region 私有方法
        private void ShowItemOnRow(DataGridViewRow row, string ip, string entranceName, int entranceID, string memo)
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
            if (_Reader == null)
            {
                _Reader = new ZSTReader();
                _Reader.Init();
                Ralid.OpenCard.OpenCardService.GlobalSettings.Current.Set<ZSTReader>(_Reader);
            }
            dataGridView1.Rows.Clear();
            ZSTSetting _ZSTSetting = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<ZSTSetting>();
            if (_ZSTSetting != null && _ZSTSetting.Items != null && _ZSTSetting.Items.Count > 0)
            {
                foreach (ZSTItem item in _ZSTSetting.Items)
                {
                    EntranceInfo entrance = ParkBuffer.Current.GetEntrance(item.EntranceID);
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], item.ReaderIP, entrance != null ? entrance.EntranceName : string.Empty, entrance != null ? entrance.EntranceID : 0, item.Memo);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> ips = _Reader.SearchReaders();
                if (ips != null && ips.Count > 0)
                {
                    foreach (string ip in ips)
                    {
                        if (FindRow(ip) == -1)
                        {
                            int row = dataGridView1.Rows.Add();
                            ShowItemOnRow(dataGridView1.Rows[row], ip, string.Empty, 0, string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmZSTDetail frm = new FrmZSTDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (FindRow(frm.ReaderIP) >= 0)
                {
                    MessageBox.Show("IP 地址为 " + frm.ReaderIP + " 的读卡器已经存在");
                }
                else
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], frm.ReaderIP, frm.EntranceName, frm.EntranceID, frm.Memo);
                }
            }
        }

        private void mnu_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FrmZSTDetail frm = new FrmZSTDetail();
                frm.ReaderIP = dataGridView1.SelectedRows[0].Cells["colReaderIP"].Value as string;
                frm.EntranceID = (int)dataGridView1.SelectedRows[0].Cells["colEntrance"].Tag;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (FindRow(frm.ReaderIP) != dataGridView1.SelectedRows[0].Index)
                    {
                        MessageBox.Show("IP 地址为 " + frm.ReaderIP + " 的读卡器已经存在");
                    }
                    else
                    {
                        ShowItemOnRow(dataGridView1.SelectedRows[0], frm.ReaderIP, frm.EntranceName, frm.EntranceID, frm.Memo);
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
            ZSTSetting zst = new ZSTSetting();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells["colEntrance"].Value.ToString()))
                {
                    ZSTItem item = new ZSTItem()
                    {
                        ReaderIP = (string)row.Cells["colReaderIP"].Value,
                        EntranceID = (int)row.Cells["colEntrance"].Tag,
                        Memo = (string)row.Cells["colMemo"].Value
                    };
                    zst.Items.Add(item);
                }
                else
                {
                    ZSTItem item = new ZSTItem()
                    {
                        ReaderIP = (string)row.Cells["colReaderIP"].Value,
                        EntranceID = 0,
                        Memo = (string)row.Cells["colMemo"].Value
                    };
                    zst.Items.Add(item);
                }
            }
            CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).SaveSetting<ZSTSetting>(zst);
            if (ret.Result == ResultCode.Successful)
            {
                OpenCardMessageHandler handler = GlobalSettings.Current.Get<OpenCardMessageHandler>();
                if (handler != null)
                {
                    handler.Init(zst);
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(ret.Message);
            }
        }

        private void mnu_ParaSetting_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FrmZSTParameter frm = new FrmZSTParameter();
                frm.ReaderIP = dataGridView1.SelectedRows[0].Cells["colReaderIP"].Value as string;
                frm.Reader = _Reader;
                frm.ShowDialog();
            }
        }
        #endregion
    }
}
