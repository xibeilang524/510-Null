using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.OpenCard.OpenCardService;
using Ralid.OpenCard.OpenCardService.ETC;

namespace Ralid.OpenCard.UI.ETC
{
    public partial class FrmETCSetting : Form
    {
        public FrmETCSetting()
        {
            InitializeComponent();
        }

        private ETCSetting _ETC = null;

        #region 私有方法
        private void ShowDevicesOnGrid(List<ETCDeviceInfo> items)
        {
            dataGridView1.Rows.Clear();
            if (items != null && items.Count > 0)
            {
                foreach (var d in items)
                {
                    var row = dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Tag = d;
                    dataGridView1.Rows[row].Cells["colLaneNo"].Value = d.LaneNo;
                    dataGridView1.Rows[row].Cells["colIP"].Value = d.IPAddr;
                    dataGridView1.Rows[row].Cells["colPort"].Value = d.Port;
                    dataGridView1.Rows[row].Cells["colUserName"].Value = d.UserName;
                    dataGridView1.Rows[row].Cells["colPassword"].Value = d.Password;
                    dataGridView1.Rows[row].Cells["colProvinceNo"].Value = d.ProvinceNo;
                    dataGridView1.Rows[row].Cells["colCityNo"].Value = d.CityNo;
                    dataGridView1.Rows[row].Cells["colAreaNo"].Value = d.AreaNo;
                    dataGridView1.Rows[row].Cells["colGateNo"].Value = d.GateNo;
                    dataGridView1.Rows[row].Cells["colEcRSUID"].Value = d.EcRSUID;
                    dataGridView1.Rows[row].Cells["colEcReaderID"].Value = d.EcReaderID;
                    dataGridView1.Rows[row].Cells["colTimeout"].Value = d.TimeOut;
                    dataGridView1.Rows[row].Cells["colHeartBeatTime"].Value = d.HeartBeatTime;
                    var en = Park.BLL.ParkBuffer.Current.GetEntrance(d.EntranceID);
                    dataGridView1.Rows[row].Cells["colEntrance"].Value = en != null ? en.EntranceName : null;
                    dataGridView1.Rows[row].Cells["colState"].Value = d.State == 1 ? "设备断开" : null;
                }
            }
            lblCount.Text = string.Format("总共 {0} 项", dataGridView1.Rows.Count);
        }
        #endregion

        private void FrmETCSetting_Load(object sender, EventArgs e)
        {
            _ETC = ETCSetting.GetSetting();
            if (_ETC != null)
            {
                txtReadSameCardInterval.IntergerValue = _ETC.ReadSameCardInterval;
                ShowDevicesOnGrid(_ETC.Devices);
            }
            chkEnable.Checked = GlobalSettings.Current.Get<OpenCardMessageHandler>().ContainService<ETCSetting>();
        }

        private void 设置停车场通道ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 1)
            {
                ETCDeviceInfo d = this.dataGridView1.SelectedRows[0].Tag as ETCDeviceInfo;
                FrmSetEntrance frm = new FrmSetEntrance();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.EntranceID = d.EntranceID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    d.EntranceID = frm.EntranceID;
                    var en = Park.BLL.ParkBuffer.Current.GetEntrance(d.EntranceID);
                    this.dataGridView1.SelectedRows[0].Cells["colEntrance"].Value = en != null ? en.EntranceName : null;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ETC.ReadSameCardInterval = txtReadSameCardInterval.IntergerValue;
            CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).SaveSetting<ETCSetting>(_ETC);
            if (CustomCardTypeSetting.Current.GetCardType(ETCSetting.CardTyte) == null) //增加自定义卡片类型
            {
                CustomCardTypeSetting.Current.AddCardType(ETCSetting.CardTyte, (byte)Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard);
                ret = new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect).SaveSetting<CustomCardTypeSetting>(CustomCardTypeSetting.Current);
            }
            if (ret.Result == ResultCode.Successful)
            {
                AppSettings.CurrentSetting.SaveConfig("EnableETC", chkEnable.Checked.ToString());
                OpenCardMessageHandler handler = GlobalSettings.Current.Get<OpenCardMessageHandler>();
                if (chkEnable.Checked)
                {
                    handler.InitService(_ETC);
                }
                else
                {
                    handler.CloseService<ETCSetting>();
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(ret.Message);
            }
        }
    }
}
