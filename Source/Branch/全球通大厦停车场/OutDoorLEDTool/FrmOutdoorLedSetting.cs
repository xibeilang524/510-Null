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

namespace OutDoorLEDTool
{
    public partial class FrmOutdoorLedSetting : Form
    {
        public FrmOutdoorLedSetting()
        {
            AppSettings.CurrentSetting = new AppSettings(Application.ExecutablePath + ".config");
            GetCurrentCulture();
            InitializeComponent();
        }

        #region 私有变量
        private ParkOutDoorLedManager _Setting;
        private Thread _AutoFreshThread;
        private int _AutoFreshInterval;
        #endregion

        #region 私有方法
        private void ShowParkOutDoorSetting(ParkOutDoorLedManager setting)
        {
            _Setting = setting;
            parkCombobox1.SelectedParkID = _Setting.ParkID;
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

            if (_Setting.MotorEntrances != null)
            {
                foreach (DataGridViewRow row in entranceGrid.Rows)
                {
                    if (_Setting.MotorEntrances.Contains((row.Tag as EntranceInfo).EntranceID))
                    {
                        row.Cells["colCarType"].Value = Resource1.CarType_Motor;
                    }
                    else
                    {
                        row.Cells["colCarType"].Value = Resource1.CarType_Car;
                    }
                }
            }

            this.ledGrid.Rows.Clear();
            if (_Setting.OutDoorLeds != null)
            {
                foreach (OutDoorLed led in _Setting.OutDoorLeds)
                {
                    int row = ledGrid.Rows.Add();
                    ShowLedOnRow(ledGrid.Rows[row], led);
                }
            }
        }

        private ParkOutDoorLedManager GetSettingFromInput()
        {
            if (_Setting == null) _Setting = new ParkOutDoorLedManager();
            _Setting.ParkID = parkCombobox1.SelectedParkID;
            _Setting.Areas[0] = btnArea1.Tag as OutDoorLedArea;
            _Setting.Areas[1] = btnArea2.Tag as OutDoorLedArea;
            _Setting.Areas[2] = btnArea3.Tag as OutDoorLedArea;
            _Setting.Areas[3] = btnArea4.Tag as OutDoorLedArea;
            _Setting.Areas[4] = btnArea5.Tag as OutDoorLedArea;
            _Setting.Areas[5] = btnArea6.Tag as OutDoorLedArea;

            if (_Setting.MotorEntrances == null) _Setting.MotorEntrances = new List<int>();
            _Setting.MotorEntrances.Clear();
            foreach (DataGridViewRow row in entranceGrid.Rows)
            {
                if (row.Cells["colCarType"].Value.ToString() == Resource1.CarType_Motor) _Setting.MotorEntrances.Add((row.Tag as EntranceInfo).EntranceID);
            }
            if (_Setting.OutDoorLeds == null) _Setting.OutDoorLeds = new List<OutDoorLed>();
            _Setting.OutDoorLeds.Clear();
            foreach (DataGridViewRow row in ledGrid.Rows)
            {

                _Setting.OutDoorLeds.Add(row.Tag as OutDoorLed);
            }
            return _Setting;
        }

        private void ShowLedOnRow(DataGridViewRow row, OutDoorLed led)
        {
            row.Cells["colComport"].Value = led.Commport;
            row.Cells["colBaud"].Value = led.Baud;
            row.Cells["colCarAddress"].Value = led.CarLedAddress;
            row.Cells["colMotorAddress"].Value = led.MotorLedAddress;
            row.Cells["colBrightness"].Value = led.Brightness;
            row.Tag = led;
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

        private void ShowLanguage()
        {
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == "zh-Hans" || culture == "zh-CHS")
            {
                this.mnu_SimpleChinese.Checked = true;
            }
            else if (culture == "zh-Hant" || culture == "zh-CHT")
            {
                this.mnu_TraditionalChinese.Checked = true;
            }
            else
            {
                this.mnu_English.Checked = true;
            }
        }

        private static void GetCurrentCulture()
        {
            if (string.IsNullOrEmpty(AppSettings.CurrentSetting.Language))
            {
                string culture = Thread.CurrentThread.CurrentCulture.Parent.Name;
                if (culture == "zh-Hans" || culture == "zh-CHS")
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-Hans");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hans");
                }
                else if (culture == "zh-Hant" || culture == "zh-CHT")
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-Hant");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hant");
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                }
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(AppSettings.CurrentSetting.Language);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(AppSettings.CurrentSetting.Language);
            }
        }

        private void DoWork()
        {
            if (_AutoFreshThread == null && _AutoFreshInterval> 0)
            {
                _AutoFreshThread = new Thread(AutoFresh_Thread);
                _AutoFreshThread.Start();
            }
            if (_AutoFreshThread != null && _AutoFreshInterval == 0)
            {
                _AutoFreshThread.Abort();
                _AutoFreshThread = null;
            }
        }

        private void AutoFresh_Thread()
        {
            try
            {
                int parkid = 0;
                string temp = AppSettings.CurrentSetting.GetConfigContent("Park");
                if (int.TryParse(temp, out parkid))
                {
                    while (true)
                    {
                        Thread.Sleep(_AutoFreshInterval * 1000);
                        ParkOutDoorLedManager man = ParkOutDoorLedSettingsStorage.Get(parkid);
                        if (man != null && man.LastUpdate != null && man.LastUpdate != _Setting.LastUpdate)
                        {
                            _Setting = man;
                            _Setting.ShowLed();
                            _Setting.SetBrightness();
                            Action action = delegate()
                            {
                                ShowParkOutDoorSetting(_Setting);
                            };
                            this.Invoke(action);
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 户外屏设置
        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmLEDDetail frm = new FrmLEDDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                OutDoorLed led = new OutDoorLed()
                {
                    Commport = frm.Commport,
                    Baud = frm.BaudRate,
                    CarLedAddress = frm.CarAddress,
                    MotorLedAddress = frm.MotorAddress,
                    Brightness = frm.Brightness,
                };
                int row = ledGrid.Rows.Add();
                ShowLedOnRow(ledGrid.Rows[row], led);
            }
        }

        private void mnu_Update_Click(object sender, EventArgs e)
        {
            if (ledGrid.SelectedRows.Count == 1)
            {
                OutDoorLed led = ledGrid.SelectedRows[0].Tag as OutDoorLed;
                FrmLEDDetail frm = new FrmLEDDetail();
                frm.Commport = led.Commport;
                frm.BaudRate = led.Baud;
                frm.CarAddress = led.CarLedAddress;
                frm.MotorAddress = led.MotorLedAddress;
                frm.Brightness = led.Brightness;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    led.Commport = frm.Commport;
                    led.Baud = frm.BaudRate;
                    led.CarLedAddress = frm.CarAddress;
                    led.MotorLedAddress = frm.MotorAddress;
                    led.Brightness = frm.Brightness;
                    ShowLedOnRow(ledGrid.SelectedRows[0], led);
                }
            }
        }

        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            if (ledGrid.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(Resource1.FrmMain_SureDelete, Resource1.Form_Query, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in ledGrid.SelectedRows)
                    {
                        ledGrid.Rows.Remove(row);
                    }
                }
            }
        }
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            ShowLanguage();

            FrmConnect frm = new FrmConnect();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                mnu_Exit_Click(this.mnu_Exit, EventArgs.Empty);
                return;
            }

            ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
            ParkBuffer.Current.InValid();
            this.parkCombobox1.Init(string.Empty);
            CustomCardTypeSetting.Current = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<CustomCardTypeSetting>();

            string temp = AppSettings.CurrentSetting.GetConfigContent("Park");
            int parkid = 0;
            if (int.TryParse(temp, out parkid))
            {
                _Setting = ParkOutDoorLedSettingsStorage.Get(parkid);
                if (_Setting == null)
                {
                    _Setting = new ParkOutDoorLedManager();
                    ShowParkOutDoorSetting(_Setting);
                }
                else
                {
                    ShowParkOutDoorSetting(_Setting);
                    _Setting.ShowLed();
                    _Setting.SetBrightness();

                    int interval =0;
                    temp= AppSettings.CurrentSetting.GetConfigContent("AutoFreshInterval");
                    int.TryParse(temp,out interval);
                    this.txtAutoFreshInterval.IntergerValue = interval;
                    _AutoFreshInterval = interval;
                    DoWork();
                }
            }
        }

        private void parkCombobox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.entranceGrid.Rows.Clear();
            ParkBuffer.Current.InValid();
            if (parkCombobox1.SelectedParkID > 0)
            {
                ParkInfo park = ParkBuffer.Current.GetPark(parkCombobox1.SelectedParkID);
                if (park != null)
                {
                    foreach (EntranceInfo entrance in park.Entrances)
                    {
                        int row = entranceGrid.Rows.Add();
                        entranceGrid.Rows[row].Tag = entrance;
                        entranceGrid.Rows[row].Cells["colEntranceName"].Value = entrance.EntranceName;
                        entranceGrid.Rows[row].Cells["colEntranceType"].Value = entrance.IsExitDevice ? Resource1.Entrance_Out : Resource1.Entrance_In;
                        DataGridViewComboBoxCell comboCell = entranceGrid.Rows[row].Cells["colCarType"] as DataGridViewComboBoxCell;
                        comboCell.Items.Add(Resource1.CarType_Car);
                        comboCell.Items.Add(Resource1.CarType_Motor);
                        comboCell.Value = Resource1.CarType_Car;
                    }
                }
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

                    if (_AutoFreshThread != null)
                    {
                        _AutoFreshThread.Abort();
                        _AutoFreshThread = null;
                    }
                }
            }
        }

        private void btnArea_Enter(object sender, EventArgs e)
        {
            (sender as Control).Text = Resource1.FrmMain_Setting;
        }

        private void btnArea_Leave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ShowBtnAreaText(btn);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ParkOutDoorLedManager settings = GetSettingFromInput();
            bool ret = ParkOutDoorLedSettingsStorage.Save(settings);
            if (ret)
            {
                AppSettings.CurrentSetting.SaveConfig("Park", settings.ParkID.ToString());
                AppSettings.CurrentSetting.SaveConfig("AutoFreshInterval", txtAutoFreshInterval.IntergerValue.ToString());
                settings.ShowLed();
                settings.SetBrightness();
                MessageBox.Show(Resource1.FrmMain_SaveSuccess);

                _AutoFreshInterval = txtAutoFreshInterval.IntergerValue;
                DoWork();
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notify1.Dispose();
            Environment.Exit(0);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void notify1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
            this.Show();
        }

        private void mnu_Exit_Click(object sender, EventArgs e)
        {
            this.notify1.Dispose();
            Environment.Exit(0);
        }

        private void mnu_Language_Clicked(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in mnu_Language.DropDownItems)
            {
                item.Checked = false;
            }

            if (sender == mnu_SimpleChinese)
            {
                mnu_SimpleChinese.Checked = true;
                AppSettings.CurrentSetting.Language = "zh-Hans";
            }
            else if (sender == mnu_TraditionalChinese)
            {
                mnu_TraditionalChinese.Checked = true;
                AppSettings.CurrentSetting.Language = "zh-Hant";
            }
            else if (sender == mnu_English)
            {
                mnu_English.Checked = true;
                AppSettings.CurrentSetting.Language = "en";
            }

            System.Globalization.CultureInfo cli = new System.Globalization.CultureInfo(AppSettings.CurrentSetting.Language);
            if (System.Threading.Thread.CurrentThread.CurrentUICulture != cli)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = cli;
                System.Threading.Thread.CurrentThread.CurrentCulture = cli;
                ResourceUtil.ApplyResource(this);
            }

            if (_Setting != null)
            {
                ShowParkOutDoorSetting(_Setting);
            }
        }

        private void entranceGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        #endregion



        
    }
}
