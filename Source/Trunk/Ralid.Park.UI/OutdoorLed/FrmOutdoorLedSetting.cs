using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.OutdoorLEDSetting;
using Ralid.Park.UI.Resources;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.UI.OutdoorLed
{
    public partial class FrmOutdoorLedSetting : Form
    {
        public FrmOutdoorLedSetting()
        {
            AppSettings.CurrentSetting = new AppSettings(Application.ExecutablePath + ".config");
            InitializeComponent();
        }

        #region 私有变量
        private ParkOutDoorLedManager _Setting;
        #endregion

        #region 私有方法
        private void ShowParkOutDoorSetting(ParkOutDoorLedManager setting)
        {
            _Setting = setting;
            btnArea1.Tag = _Setting.Areas[0];
            btnArea2.Tag = _Setting.Areas[1];
            btnArea3.Tag = _Setting.Areas[2];
            btnArea4.Tag = _Setting.Areas[3];
            btnArea5.Tag = _Setting.Areas[4];
            btnArea6.Tag = _Setting.Areas[5];
            ShowBtnAreaText(btnArea1);
            ShowBtnAreaText(btnArea2);
            ShowBtnAreaText(btnArea3);
            ShowBtnAreaText(btnArea4);
            ShowBtnAreaText(btnArea5);
            ShowBtnAreaText(btnArea6);
        }

        private ParkOutDoorLedManager GetSettingFromInput()
        {
            _Setting.Areas[0] = btnArea1.Tag as OutDoorLedArea;
            _Setting.Areas[1] = btnArea2.Tag as OutDoorLedArea;
            _Setting.Areas[2] = btnArea3.Tag as OutDoorLedArea;
            _Setting.Areas[3] = btnArea4.Tag as OutDoorLedArea;
            _Setting.Areas[4] = btnArea5.Tag as OutDoorLedArea;
            _Setting.Areas[5] = btnArea6.Tag as OutDoorLedArea;
            _Setting.LastUpdate = DateTime.Now;
            return _Setting;
        }

        private void ShowBtnAreaText(Button btn)
        {
            OutDoorLedArea area = btn.Tag as OutDoorLedArea;
            if (area != null && area.CardType != null)
            {
                btn.Text = area.Vacant.ToString();
                if (area.VacantColor == 1) btn.ForeColor = Color.Red;
                if (area.VacantColor == 2) btn.ForeColor = Color.Green;
                if (area.VacantColor == 3) btn.ForeColor = Color.Yellow;
            }
            else
            {
                btn.Text = Resource1.FrmMain_Setting;
            }
        }

        private void ShowSetting(ParkInfo park)
        {
            ParkOutDoorLedManager settings = ParkOutDoorLedSettingsStorage.Get(park.ParkID);
            if (settings == null)
            {
                settings = new ParkOutDoorLedManager();
                settings.ParkID = park.ParkID;
                ShowParkOutDoorSetting(settings);
            }
            else
            {
                ShowParkOutDoorSetting(settings);
            }
        }
        #endregion

        #region 公共事件
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置当前设置所属停车场
        /// </summary>
        public ParkInfo Park { get; set; }
        #endregion 

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (Park != null)
            {
                ShowSetting(Park);
            }
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            OutDoorLedArea area = (sender as Button).Tag as OutDoorLedArea;
            if (area != null)
            {
                FrmAreaDetail frm = new FrmAreaDetail();
                if (area.CardType != null) frm.CardType = area.CardType.Value;
                frm.Vacant = area.Vacant;
                frm.CarPort = area.CarPort;
                frm.VacantColor = area.VacantColor;
                frm.FullColor = area.FullColor;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    area.CardType = frm.CardType;
                    area.Vacant = frm.Vacant;
                    area.VacantColor = frm.VacantColor;
                    area.CarPort = frm.CarPort;
                    area.FullColor = frm.FullColor;
                    ShowBtnAreaText(sender as Button);
                }
            }
        }

        private void btnArea_Enter(object sender, EventArgs e)
        {

        }

        private void btnArea_Leave(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ParkOutDoorLedManager settings = GetSettingFromInput();
            if (ParkOutDoorLedSettingsStorage.Save(settings))
            {
                int ports = settings.Areas.Sum(area => area.CardType != null ? area.CarPort : 0);
                int vacant = settings.Areas.Sum(area => area.CardType != null ? area.Vacant : 0);
                if (Park.Vacant != vacant || Park.TotalPosition != ports) //如果更新了车位数或余位数
                {
                    Park.Vacant = (short)vacant;
                    Park.TotalPosition = (short)ports;
                    if ((new ParkBll(AppSettings.CurrentSetting.ParkConnect)).Update(Park).Result == ResultCode.Successful)
                    {
                        if (this.ItemUpdated != null)
                        {
                            this.ItemUpdated(this, new ItemUpdatedEventArgs(Park));
                        }
                    }
                }
            }
            this.Close();
        }
        #endregion
    }
}
