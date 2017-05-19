using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.OpenCard.OpenCardService;
using Ralid.OpenCard.OpenCardService.LR280;

namespace Ralid.OpenCard.UI.LR280
{
    public partial class FrmLR280Setting : Form
    {
        #region 构造函数
        public FrmLR280Setting()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有方法
        private void ShowItemOnRow(DataGridViewRow row, LR280Item item)
        {
            EntranceInfo entrance = item.EntranceID != null ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            row.Cells["colComport"].Value = "COM" + item.Comport.ToString();
            row.Cells["colEntrance"].Value = entrance != null ? entrance.EntranceName : string.Empty;
            row.Cells["colMemo"].Value = item.Memo;
            row.Tag = item;
        }

        /// <summary>
        /// 获取某读卡器IP在网格中所在行的行号,如果没有找到,返回-1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int FindRow(byte commport)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var it = row.Tag as LR280Item;
                if (it.Comport == commport)
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
            LR280Setting LR280 = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<LR280Setting>();
            if (LR280 != null)
            {
                dataGridView1.Rows.Clear();
                if (LR280.Items != null && LR280.Items.Count > 0)
                {
                    foreach (LR280Item item in LR280.Items)
                    {
                        int row = dataGridView1.Rows.Add();
                        ShowItemOnRow(dataGridView1.Rows[row], item);
                    }
                }
            }
            chkEnable.Checked = GlobalSettings.Current.Get<OpenCardMessageHandler>().ContainService<LR280Setting>();
        }

        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmLR280Detail frm = new FrmLR280Detail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LR280Item item = frm.LR280Item;
                if (FindRow(item.Comport) >= 0)
                {
                    MessageBox.Show("串口号为 " + item.Comport + " 的读卡器已经存在");
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
                FrmLR280Detail frm = new FrmLR280Detail();
                frm.LR280Item = dataGridView1.SelectedRows[0].Tag as LR280Item;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LR280Item item = frm.LR280Item;
                    var row = FindRow(item.Comport);
                    if (row >= 0 && row != dataGridView1.SelectedRows[0].Index)
                    {
                        MessageBox.Show("串口号为 " + item.Comport + " 的读卡器已经存在");
                    }
                    else
                    {
                        ShowItemOnRow(dataGridView1.SelectedRows[0], item);
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

        private bool CheckInput()
        {
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;
            LR280Setting lr280 = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetOrCreateSetting<LR280Setting>();
            lr280.Items.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                lr280.Items.Add(row.Tag as LR280Item);
            }
            CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).SaveSetting<LR280Setting>(lr280);
            if (CustomCardTypeSetting.Current.GetCardType(LR280Setting.CardTyte) == null) //增加自定义卡片类型
            {
                CustomCardTypeSetting.Current.AddCardType(LR280Setting.CardTyte, (byte)Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard);
                new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect).SaveSetting<CustomCardTypeSetting>(CustomCardTypeSetting.Current);
            }
            if (ret.Result == ResultCode.Successful)
            {
                AppSettings.CurrentSetting.SaveConfig("EnableLR280", chkEnable.Checked.ToString());
                OpenCardMessageHandler handler = GlobalSettings.Current.Get<OpenCardMessageHandler>();
                if (chkEnable.Checked)
                {
                    handler.InitService(lr280);
                }
                else
                {
                    handler.CloseService<LR280Setting>();
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
