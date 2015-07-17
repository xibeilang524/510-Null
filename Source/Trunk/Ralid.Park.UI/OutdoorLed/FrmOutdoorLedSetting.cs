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
            if (_Setting.Areas != null && _Setting.Areas.Length > 0)
            {
                OutDoorLedArea[] cars = _Setting.Areas.Where(item => item != null && item.CarType == 0).ToArray();
                if (cars != null && cars.Length >= 1)
                {
                    btnCarA.Tag = cars[0];
                    ShowBtnAreaText(btnCarA);
                }
                if (cars != null && cars.Length >= 2)
                {
                    btnCarB.Tag = cars[1];
                    ShowBtnAreaText(btnCarB);
                }
                if (cars != null && cars.Length >= 3)
                {
                    btnCarC.Tag = cars[2];
                    ShowBtnAreaText(btnCarC);
                }
                if (cars != null && cars.Length >= 4)
                {
                    btnCarD.Tag = cars[3];
                    ShowBtnAreaText(btnCarD);
                }
                if (cars != null && cars.Length >= 5)
                {
                    btnCarE.Tag = cars[4];
                    ShowBtnAreaText(btnCarE);
                }
                OutDoorLedArea[] bikes = _Setting.Areas.Where(item => item != null && item.CarType == 1).ToArray();
                if (bikes != null && bikes.Length >= 1)
                {
                    btnBikeA.Tag = bikes[0];
                    ShowBtnAreaText(btnBikeA);
                }
                if (bikes != null && bikes.Length >= 2)
                {
                    btnBikeB.Tag = bikes[1];
                    ShowBtnAreaText(btnBikeB);
                }
                if (bikes != null && bikes.Length >= 3)
                {
                    btnBikeC.Tag = bikes[2];
                    ShowBtnAreaText(btnBikeC);
                }
                if (bikes != null && bikes.Length >= 4)
                {
                    btnBikeD.Tag = bikes[3];
                    ShowBtnAreaText(btnBikeD);
                }
                if (bikes != null && bikes.Length >= 5)
                {
                    btnBikeE.Tag = bikes[4];
                    ShowBtnAreaText(btnBikeE);
                }
            }
        }

        private ParkOutDoorLedManager GetSettingFromInput()
        {
            _Setting.Areas = new OutDoorLedArea[10];
            _Setting.Areas[0] = btnCarA.Tag as OutDoorLedArea;
            _Setting.Areas[1] = btnCarB.Tag as OutDoorLedArea;
            _Setting.Areas[2] = btnCarC.Tag as OutDoorLedArea;
            _Setting.Areas[3] = btnCarD.Tag as OutDoorLedArea;
            _Setting.Areas[4] = btnCarE.Tag as OutDoorLedArea;

            _Setting.Areas[5] = btnBikeA.Tag as OutDoorLedArea;
            _Setting.Areas[6] = btnBikeB.Tag as OutDoorLedArea;
            _Setting.Areas[7] = btnBikeC.Tag as OutDoorLedArea;
            _Setting.Areas[8] = btnBikeD.Tag as OutDoorLedArea;
            _Setting.Areas[9] = btnBikeE.Tag as OutDoorLedArea;
            _Setting.LastUpdate = DateTime.Now;
            return _Setting;
        }

        private void ShowBtnAreaText(Button btn)
        {
            OutDoorLedArea area = btn.Tag as OutDoorLedArea;
            if (area != null && area.CardType != null)
            {
                btn.Text = area.Vacant.ToString();
                if (area.Vacant > 0)
                {
                    if (area.VacantColor == 1) btn.ForeColor = Color.Red;
                    if (area.VacantColor == 2) btn.ForeColor = Color.Green;
                    if (area.VacantColor == 3) btn.ForeColor = Color.Yellow;
                }
                else
                {
                    if (area.FullColor == 1) btn.ForeColor = Color.Red;
                    if (area.FullColor == 2) btn.ForeColor = Color.Green;
                    if (area.FullColor == 3) btn.ForeColor = Color.Yellow;
                }
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

        private void btnCar_Click(object sender, EventArgs e)
        {
            FrmAreaDetail frm = new FrmAreaDetail();
            OutDoorLedArea area = (sender as Button).Tag as OutDoorLedArea;
            if (area != null)
            {
                if (area.CardType != null) frm.CardType = area.CardType.Value;
                frm.Vacant = area.Vacant;
                frm.CarPort = area.CarPort;
                frm.VacantColor = area.VacantColor;
                frm.FullColor = area.FullColor;
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                area = new OutDoorLedArea();
                area.CardType = frm.CardType;
                area.CarType = 0;
                area.Vacant = frm.Vacant;
                area.VacantColor = frm.VacantColor;
                area.CarPort = frm.CarPort;
                area.FullColor = frm.FullColor;
                (sender as Button).Tag = area;
                ShowBtnAreaText(sender as Button);
            }
        }

        private void btnBike_Click(object sender, EventArgs e)
        {
            FrmAreaDetail frm = new FrmAreaDetail();
            OutDoorLedArea area = (sender as Button).Tag as OutDoorLedArea;
            if (area != null)
            {
                if (area.CardType != null) frm.CardType = area.CardType.Value;
                frm.Vacant = area.Vacant;
                frm.CarPort = area.CarPort;
                frm.VacantColor = area.VacantColor;
                frm.FullColor = area.FullColor;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                area = new OutDoorLedArea();
                area.CardType = frm.CardType;
                area.CarType = 1;
                area.Vacant = frm.Vacant;
                area.VacantColor = frm.VacantColor;
                area.CarPort = frm.CarPort;
                area.FullColor = frm.FullColor;
                (sender as Button).Tag = area;
                ShowBtnAreaText(sender as Button);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ParkOutDoorLedManager settings = GetSettingFromInput();
            if (ParkOutDoorLedSettingsStorage.Save(settings))
            {
                int ports = settings.Areas.Sum(area => area != null && area.CardType != null ? area.CarPort : 0);
                int vacant = settings.Areas.Sum(area => area != null && area.CardType != null ? area.Vacant : 0);
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
