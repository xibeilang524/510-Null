using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.UI
{
    public partial class FrmSystemOption : Form
    {
        public FrmSystemOption()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置操作的数据库是否本地数据库
        /// </summary>
        public bool IsLDB { get; set; }
        #endregion

        #region 私有事件
        private List<AccessInfo> GetAccessesHandler(object sender, EntranceEventArgs e)
        {
            List<AccessInfo> accesses = new List<AccessInfo>();
            for (int i = 0; i < this.accessGrid.Rows.Count; i++)
            {
                AccessInfo a = this.accessGrid.Rows[i].Tag as AccessInfo;
                if (a.ID == 0) a.ID = (byte)(i + 1);  //新增加的权限组要有新的ID
                foreach (AccessTimeZone zone in a.AccessTimeZones)
                {
                    if (zone.AccessEntrances.Any(item => item == e.EntranceID))
                    {
                        accesses.Add(a);
                        break;
                    }
                }
            }
            return accesses;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 显示当前的参数
        /// </summary>
        private void ShowCurrentParking()
        {
            if (UserSetting.Current != null) ShowUserSetting(UserSetting.Current);
            if (KeySetting.Current != null) ShowKeySetting(KeySetting.Current);
            if (BaseCardTypeSetting.Current != null) ShowBaseCardTypeSetting(BaseCardTypeSetting.Current);
            if (CarTypeSetting.Current != null) ShowCarTypeSetting(CarTypeSetting.Current);
            if (AccessSetting.Current != null) ShowAccessSetting(AccessSetting.Current);
            if (HolidaySetting.Current != null) ShowHolidaySetting(HolidaySetting.Current);
            if (TariffSetting.Current != null)
            {
                ShowTariffSetting(TariffSetting.Current);
                if (TariffSetting.Current.TariffOption != null) ShowTollOption(TariffSetting.Current.TariffOption);
            }
            if (CustomCardTypeSetting.Current != null) ShowCustomCardTypeSetting(CustomCardTypeSetting.Current);
        }
        /// <summary>
        /// 显示停车场数据库
        /// </summary>
        private void ShowParkingDB()
        {
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
            UserSetting us = ssb.GetOrCreateSetting<UserSetting>();
            ShowUserSetting(us);
            KeySetting ks = ssb.GetOrCreateSetting<KeySetting>();
            ShowKeySetting(ks);
            BaseCardTypeSetting bts = ssb.GetOrCreateSetting<BaseCardTypeSetting>();
            ShowBaseCardTypeSetting(bts);
            CarTypeSetting cts = ssb.GetOrCreateSetting<CarTypeSetting>();
            ShowCarTypeSetting(cts);
            AccessSetting acs = ssb.GetOrCreateSetting<AccessSetting>();
            ShowAccessSetting(acs);
            HolidaySetting hs = ssb.GetOrCreateSetting<HolidaySetting>();
            ShowHolidaySetting(hs);
            TariffSetting tas = ssb.GetOrCreateSetting<TariffSetting>();
            ShowTariffSetting(tas);
            if (tas.TariffOption != null) ShowTollOption(tas.TariffOption);
            CustomCardTypeSetting ccts = ssb.GetSetting<CustomCardTypeSetting>();
            if (ccts != null) ShowCustomCardTypeSetting(ccts);
        }
        /// <summary>
        /// 显示本地数据库
        /// </summary>
        private void ShowLDB()
        {
            LDB_SysParaSettingsBll ssb = new LDB_SysParaSettingsBll(LDB_AppSettings.Current.LDBConnect);
            UserSetting us = ssb.GetOrCreateSetting<UserSetting>();
            ShowUserSetting(us);
            KeySetting ks = ssb.GetOrCreateSetting<KeySetting>();
            ShowKeySetting(ks);
            BaseCardTypeSetting bts = ssb.GetOrCreateSetting<BaseCardTypeSetting>();
            ShowBaseCardTypeSetting(bts);
            CarTypeSetting cts = ssb.GetOrCreateSetting<CarTypeSetting>();
            ShowCarTypeSetting(cts);
            AccessSetting acs = ssb.GetOrCreateSetting<AccessSetting>();
            ShowAccessSetting(acs);
            HolidaySetting hs = ssb.GetOrCreateSetting<HolidaySetting>();
            ShowHolidaySetting(hs);
            TariffSetting tas = ssb.GetOrCreateSetting<TariffSetting>();
            ShowTariffSetting(tas);
            if (tas.TariffOption != null) ShowTollOption(tas.TariffOption);
            CustomCardTypeSetting ccts = ssb.GetSetting<CustomCardTypeSetting>();
            if (ccts != null) ShowCustomCardTypeSetting(ccts);
        }

        /// <summary>
        /// 保存停车场数据库
        /// </summary>
        private void SaveParkingDB()
        {
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
            UserSetting.Current = GetUserSettingFromInput();
            ssb.SaveSetting<UserSetting>(UserSetting.Current);

            BaseCardTypeSetting.Current = GetBaseCardTypeFromInput();
            ssb.SaveSetting<BaseCardTypeSetting>(BaseCardTypeSetting.Current);

            CarTypeSetting.Current = GetCarTypeSettingFromInput();
            ssb.SaveSetting<CarTypeSetting>(CarTypeSetting.Current);

            AccessSetting.Current = GetAccessSettingFromInput();
            ssb.SaveSetting<AccessSetting>(AccessSetting.Current);

            HolidaySetting.Current = GetHolidaySettingFromInput();
            ssb.SaveSetting<HolidaySetting>(HolidaySetting.Current);

            TariffSetting.Current = GetTariffSettingFromInput();
            TariffSetting.Current.TariffOption = GetTollOptionFromInput();
            ssb.SaveSetting<TariffSetting>(TariffSetting.Current);

            CustomCardTypeSetting.Current = GetCustomCardTypeFromInput();
            ssb.SaveSetting<CustomCardTypeSetting>(CustomCardTypeSetting.Current);

            KeySetting.Current = GetKeySettingFromInput();
            ssb.SaveSetting<KeySetting>(KeySetting.Current);
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).AddReadSectionAndKey(GlobalVariables.ParkingSection, GlobalVariables.ParkingKey);

            SaveToStandby();

            Action action = delegate()
            {
                foreach (IParkingAdapter ad in ParkingAdapterManager.Instance.ParkAdapters)
                {
                    ad.DownLoadUserSetting(UserSetting.Current);
                    System.Threading.Thread.Sleep(100);
                    ad.DownloadCarTypeSetting(CarTypeSetting.Current);
                    System.Threading.Thread.Sleep(100);
                    ad.DownloadTariffSetting(TariffSetting.Current);
                    System.Threading.Thread.Sleep(100);
                    ad.DownloadAccessSetting(AccessSetting.Current);
                    System.Threading.Thread.Sleep(100);
                    ad.DownloadHolidaySetting(HolidaySetting.Current);
                    System.Threading.Thread.Sleep(100);
                    ad.DownloadKeySetting(KeySetting.Current);
                }
            };
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(action));
            t.Start();
        }

        /// <summary>
        /// 保存到备用数据库
        /// </summary>
        private void SaveToStandby()
        {
            if (DataBaseConnectionsManager.Current.StandbyConnected)
            {
                SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                ssb.SaveSetting<UserSetting>(UserSetting.Current);
                ssb.SaveSetting<BaseCardTypeSetting>(BaseCardTypeSetting.Current);
                ssb.SaveSetting<CarTypeSetting>(CarTypeSetting.Current);
                ssb.SaveSetting<AccessSetting>(AccessSetting.Current);
                ssb.SaveSetting<HolidaySetting>(HolidaySetting.Current);
                ssb.SaveSetting<TariffSetting>(TariffSetting.Current);
                ssb.SaveSetting<CustomCardTypeSetting>(CustomCardTypeSetting.Current);
                ssb.SaveSetting<KeySetting>(KeySetting.Current);
            }
        }

        /// <summary>
        /// 保存本地数据库
        /// </summary>
        private void SaveLDB()
        {
            LDB_SysParaSettingsBll ssb = new LDB_SysParaSettingsBll(LDB_AppSettings.Current.LDBConnect);
            UserSetting.Current = GetUserSettingFromInput();
            ssb.SaveSetting<UserSetting>(UserSetting.Current);

            BaseCardTypeSetting.Current = GetBaseCardTypeFromInput();
            ssb.SaveSetting<BaseCardTypeSetting>(BaseCardTypeSetting.Current);

            CarTypeSetting.Current = GetCarTypeSettingFromInput();
            ssb.SaveSetting<CarTypeSetting>(CarTypeSetting.Current);

            AccessSetting.Current = GetAccessSettingFromInput();
            ssb.SaveSetting<AccessSetting>(AccessSetting.Current);

            HolidaySetting.Current = GetHolidaySettingFromInput();
            ssb.SaveSetting<HolidaySetting>(HolidaySetting.Current);

            TariffSetting.Current = GetTariffSettingFromInput();
            TariffSetting.Current.TariffOption = GetTollOptionFromInput();
            ssb.SaveSetting<TariffSetting>(TariffSetting.Current);

            CustomCardTypeSetting.Current = GetCustomCardTypeFromInput();
            ssb.SaveSetting<CustomCardTypeSetting>(CustomCardTypeSetting.Current);

            KeySetting.Current = GetKeySettingFromInput();
            ssb.SaveSetting<KeySetting>(KeySetting.Current);
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).AddReadSectionAndKey(GlobalVariables.ParkingSection, GlobalVariables.ParkingKey);
        }
        #endregion

        #region 事件处理
        private void FrmSysPara_Load(object sender, EventArgs e)
        {
            //if (GlobalVariables.IsNETParkAndOffLie)
            //{
            //    //写卡模式不显示以下项
            //    //this.tab1.TabPages.Remove(this.tabCardType);
            //    this.CardTypeGrid.ContextMenuStrip = null;
            //    this.CardTypeGrid.Columns["colTariff"].Visible = false;
            //    this.carTypeGrid.AllowUserToAddRows = false;
            //    this.carTypeGrid.AllowUserToDeleteRows = false;
            //}

            InitKeyInput();

            this.butOK.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);
            this.btnDownLoad.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);
            ShowAppSetting();

            if (IsLDB)
            {
                ShowLDB();
            }
            else if (DataBaseConnectionsManager.Current.MasterConnected)
            {
                ShowParkingDB();
            }
            else
            {
                ShowCurrentParking();
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                SaveAppSetting();
                if (IsLDB)
                {
                    SaveLDB();
                }
                else
                {
                    SaveParkingDB();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckInput()
        {
            byte[] bytes = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(this.txtCompanyName.Text.Trim());
            if (bytes != null && bytes.Length > 45)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidCompanyName);
                return false;
            }
            if (carTypeGrid.Rows.Count <= 1)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_NoCarType);
                this.carTypeGrid.Focus();
                return false;
            }
            int carTypeCount = 0;
            for (int i = 0; i < carTypeGrid.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty((string)carTypeGrid.Rows[i].Cells["colCarTypeDescr"].Value))
                {
                    carTypeCount++;
                }
            }
            if (carTypeCount == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_NoCarType);
                this.carTypeGrid.Focus();
                return false;
            }
            if (chkEnableDeleteOverTimeImages.Checked && txtMonth.IntergerValue <= 0)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidMonth);
                this.txtMonth.Focus();
                return false;
            }
            if (this.txtMaxCarPlateErrorChar.IntergerValue > 7 || this.txtMaxCarPlateErrorChar.IntergerValue < 0)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidMaxCarplateErrorChar);
                this.txtMaxCarPlateErrorChar.Focus();
                return false;
            }

            if (!CheckBaseCardTypeInput()) return false;

            if (!CheckCardTypeInput()) return false;

            if (!CheckTariffInput()) return false;

            if (!CheckKeyInput()) return false;

            return true;
        }

        private bool CheckBaseCardTypeInput()
        {
            foreach (DataGridViewRow row in this.BaseCardTypeGrid.Rows)
            {
                if (string.IsNullOrEmpty(row.Cells["colUserDefinedName"].Value as string))
                {
                    this.tab1.SelectedTab = this.tabBaseCardType;
                    this.BaseCardTypeGrid.Focus();
                    this.BaseCardTypeGrid.CurrentCell = row.Cells["colUserDefinedName"];
                    MessageBox.Show(Resources.Resource1.FrmSystemOption_InputBaseCardTypeName);
                    return false;
                }
            }
            return true;
        }

        private bool CheckCardTypeInput()
        {
            //最多添加4*15+2个自定义卡片，每种原型卡类型为非自定义卡片可设置15种，每种原型卡类型为自定义卡片可设置2种
            if (CardTypeGrid.Rows.Count > 4 * 15 + 2 + (CardTypeGrid.AllowUserToAddRows ? 1 : 0))//允许添加行时加1
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_MostCustomCard);
                this.CardTypeGrid.Focus();
                return false;
            }

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            count1 = GetCardTypeCount(CardType.UserDefinedCard1.ID);
            count2 = GetCardTypeCount(CardType.UserDefinedCard2.ID);
            if (count1 > 1 || count1 > 1)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_CustomCardOnly);
                this.CardTypeGrid.Focus();
                return false;
            }

            count1 = GetCardTypeCount(CardType.MonthRentCard.ID);
            count2 = GetCardTypeCount(CardType.OwnerCard.ID);
            count3 = GetCardTypeCount(CardType.PrePayCard.ID);
            count4 = GetCardTypeCount(CardType.TempCard.ID);

            if (count1 > 15 || count2 > 15 || count3 > 15 || count4 > 15)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_NonCustomCardOnly);
                this.CardTypeGrid.Focus();
                return false;
            }

            return true;

        }

        private bool CheckTariffInput()
        {
            bool result = true;
            TariffBase defaultTariff = null;
            foreach (DataGridViewRow row in this.tariffGrid.Rows)
            {
                defaultTariff = null;

                TariffBase tempTariff = row.Cells["colGeneral"].Tag as TariffBase;
                if (tempTariff != null) defaultTariff = tempTariff;

                tempTariff = row.Cells["colHoliday"].Tag as TariffBase;
                if (tempTariff != null)
                {
                    if (defaultTariff == null)
                    {
                        defaultTariff = tempTariff;
                    }
                    else if (tempTariff.GetType() != defaultTariff.GetType())
                    {
                        result = false;
                        row.Selected = true;
                        break;
                    }
                }

                tempTariff = row.Cells["colInnerRoom"].Tag as TariffBase;
                if (tempTariff != null)
                {
                    if (defaultTariff == null)
                    {
                        defaultTariff = tempTariff;
                    }
                    else if (tempTariff.GetType() != defaultTariff.GetType())
                    {
                        result = false;
                        row.Selected = true;
                        break;
                    }
                }

                tempTariff = row.Cells["colHolidayAndInnerRoom"].Tag as TariffBase;
                if (tempTariff != null)
                {
                    if (defaultTariff == null)
                    {
                        defaultTariff = tempTariff;
                    }
                    else if (tempTariff.GetType() != defaultTariff.GetType())
                    {
                        result = false;
                        row.Selected = true;
                        break;
                    }
                }
            }

            if (!result)
            {
                this.tariffGrid.Focus();
                MessageBox.Show(Resources.Resource1.FrmSystemOption_SameTariff);
            }
            return result;
        }

        private bool CheckKeyInput()
        {
            if (string.IsNullOrEmpty(txtCardSection.Text))
            {
                this.tab1.SelectedTab = this.tabKey;
                this.chkCardSection.Checked = true;
                this.txtCardSection.Focus();
                MessageBox.Show(Resources.Resource1.FrmSystemOption_InputCardSection);
                return false;
            }

            if (chkChangeKey.Checked)
            {
                if (this.pnlOldKey.Enabled)
                {
                    if (this.txtOldKey.HexValue.Length != 6)
                    {
                        MessageBox.Show(Resources.Resource1.FrmSystemOption_InputOldKey);
                        this.tab1.SelectedTab = this.tabKey;
                        this.txtOldKey.Focus();
                        return false;
                    }
                    if (!Ralid.GeneralLibrary.HexStringConverter.HexEquals(this.txtOldKey.HexValue, KeySetting.Current.ParkingKey))
                    {
                        MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidOldKey);
                        this.tab1.SelectedTab = this.tabKey;
                        this.txtOldKey.Focus();
                        return false;
                    }
                }
                if (rdbInputKey.Checked)
                {
                    if (this.txtNewKey.HexValue.Length != 6)
                    {
                        MessageBox.Show(Resources.Resource1.FrmSystemOption_InputNewKey);
                        this.tab1.SelectedTab = this.tabKey;
                        this.txtNewKey.Focus();
                        return false;
                    }
                    if (this.txtConfirmKey.HexValue.Length != 6)
                    {
                        MessageBox.Show(Resources.Resource1.FrmSystemOption_InputConfirmNewKey);
                        this.tab1.SelectedTab = this.tabKey;
                        this.txtConfirmKey.Focus();
                        return false;
                    }
                    if (this.txtNewKey.Text.Trim().ToLower() != this.txtConfirmKey.Text.Trim().ToLower())
                    {
                        MessageBox.Show(Resources.Resource1.FrmSystemOption_NewKeyInconformity);
                        this.tab1.SelectedTab = this.tabKey;
                        this.txtConfirmKey.Focus();
                        return false;
                    }
                }
            }
            return true;
        }


        private void chkShowOld_CheckedChanged(object sender, EventArgs e)
        {
            this.txtOldKey.PasswordChar = this.chkShowOld.Checked ? char.MinValue : '*';
        }

        private void chkChangeKey_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlChangeKey.Enabled = this.chkChangeKey.Checked;
        }

        private void chkCardSection_CheckedChanged(object sender, EventArgs e)
        {
            this.txtCardSection.Enabled = this.chkCardSection.Checked;
        }


        private void rdbInputKey_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlNewKey.Enabled = this.rdbInputKey.Checked;
        }

        private void tab1_Selected(object sender, TabControlEventArgs e)
        {
            this.btnDownLoad.Visible = !IsLDB
                && (tab1.SelectedTab == tabAccess
                || tab1.SelectedTab == tabHoliday
                || tab1.SelectedTab == tabTariff
                || tab1.SelectedTab == tabKey);
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            FrmDownLoadBase frm = null;
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);

            if (tab1.SelectedTab == tabAccess)
            {
                AccessSetting.Current = GetAccessSettingFromInput();
                ssb.SaveSetting<AccessSetting>(AccessSetting.Current);
                foreach (IParkingAdapter ad in ParkingAdapterManager.Instance.ParkAdapters)
                {
                    ad.DownloadAccessSetting(AccessSetting.Current);
                }

                frm = new FrmDownLoadAccessSetting();
            }
            else if (tab1.SelectedTab == tabHoliday)
            {
                HolidaySetting.Current = GetHolidaySettingFromInput();
                ssb.SaveSetting<HolidaySetting>(HolidaySetting.Current);
                foreach (IParkingAdapter ad in ParkingAdapterManager.Instance.ParkAdapters)
                {
                    ad.DownloadHolidaySetting(HolidaySetting.Current);
                }

                frm = new FrmDownLoadHolidaySetting();
            }
            else if (tab1.SelectedTab == tabTariff && CheckTariffInput())
            {
                TariffSetting.Current = GetTariffSettingFromInput();
                ssb.SaveSetting<TariffSetting>(TariffSetting.Current);
                foreach (IParkingAdapter ad in ParkingAdapterManager.Instance.ParkAdapters)
                {
                    ad.DownloadTariffSetting(TariffSetting.Current);
                }

                frm = new FrmDownLoadTariffSetting();
            }
            else if (tab1.SelectedTab == tabKey && CheckKeyInput())
            {
                KeySetting.Current = GetKeySettingFromInput();
                ssb.SaveSetting<KeySetting>(KeySetting.Current);
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).AddReadSectionAndKey(GlobalVariables.ParkingSection, GlobalVariables.ParkingKey);
                foreach (IParkingAdapter ad in ParkingAdapterManager.Instance.ParkAdapters)
                {
                    ad.DownloadKeySetting(KeySetting.Current);
                }
                frm = new FrmDownLoadKeySetting();
                if (chkChangeKey.Checked)
                {
                    MessageBox.Show(Resources.Resource1.FrmSystemOption_ChangeKeySuccess);
                    InitKeyInput();
                }
            }

            if (frm != null)
            {
                frm.ShowDialog();
            }
        }


        private void carTypeGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!CarTypeSetting.Current.CanDel(e.Row.Index))
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_DeafultCarType);
                e.Cancel = true;
            }

        }

        private void comVideoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comVideoType.Text == "X")
            {
                this.chkOptimized.Checked = false;
                this.chkOptimized.Enabled = false;
            }
            else
            {
                this.chkOptimized.Enabled = true;
            }
        }
        #endregion

        #region 本地配置
        private void ShowAppSetting()
        {
            this.comTicketReader.Init();
            this.comFeeLed.Init();
            this.comBillPrinter.Init();
            this.comYCT.Init();
            this.comParkFullLed.Init();
            this.comLedType.Items.Clear();
            this.comLedType.Items.Add(Resources.Resource1.LEDType_Zhongkuang);
            this.comLedType.Items.Add(Resources.Resource1.LEDType_YanSe);
            this.comParkingCommunicationIP.Items.Clear();
            this.comParkingCommunicationIP.Items.Add(string.Empty);
            this.comParkingCommunicationIP.Items.AddRange(GeneralLibrary.NetTool.GetLocalIPS());


            this.comFeeLed.ComPort = AppSettings.CurrentSetting.ParkFeeLedCOMPort;
            this.comLedType.SelectedIndex = AppSettings.CurrentSetting.ParkFeeLedType;
            this.comTicketReader.ComPort = AppSettings.CurrentSetting.TicketReaderCOMPort;
            this.comBillPrinter.ComPort = AppSettings.CurrentSetting.BillPrinterCOMPort;
            this.comYCT.ComPort = AppSettings.CurrentSetting.YCTReaderCOMPort;
            this.comParkFullLed.ComPort = AppSettings.CurrentSetting.ParkFullLedCOMPort;
            this.chkOpenLastOpenedVideo.Checked = AppSettings.CurrentSetting.OpenLastOpenedVideo;
            this.chkShowOnlyListenedPark.Checked = AppSettings.CurrentSetting.ShowOnlyListenedPark;
            this.chkDebug.Checked = AppSettings.CurrentSetting.Debug;
            this.chkOptimized.Checked = AppSettings.CurrentSetting.Optimized;
            this.chkNeedPasswordWhenExit.Checked = AppSettings.CurrentSetting.NeedPasswordWhenExit;
            this.chkEnableTTS.Checked = AppSettings.CurrentSetting.EnableTTS;
            this.chkEnlargeMemo.Checked = AppSettings.CurrentSetting.EnlargeMemo;
            this.chkChargeAfterMemo.Checked = AppSettings.CurrentSetting.ChargeAfterMemo;
            this.chkShowAPMMonitor.Checked = AppSettings.CurrentSetting.ShowAPMMonitor;
            this.chkEnableZST.Checked = AppSettings.CurrentSetting.EnableZST;
            this.txtZSTReaderIP.IP = AppSettings.CurrentSetting.ZSTReaderIP;
            this.chkEnableWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;
            this.chkAuotAddToFirewallException.Checked = AppSettings.CurrentSetting.AuotAddToFirewallException;
            this.comParkingCommunicationIP.Text = AppSettings.CurrentSetting.ParkingCommunicationIP;
            this.chkCheckConnectionWithPing.Checked = AppSettings.CurrentSetting.CheckConnectionWithPing;
        }

        private void SaveAppSetting()
        {
            AppSettings.CurrentSetting.ParkFeeLedCOMPort = this.comFeeLed.ComPort;
            AppSettings.CurrentSetting.TicketReaderCOMPort = this.comTicketReader.ComPort;
            AppSettings.CurrentSetting.BillPrinterCOMPort = this.comBillPrinter.ComPort;
            AppSettings.CurrentSetting.YCTReaderCOMPort = this.comYCT.ComPort;
            AppSettings.CurrentSetting.ParkFullLedCOMPort = this.comParkFullLed.ComPort;
            AppSettings.CurrentSetting.ParkFeeLedType = (byte)(this.comLedType.SelectedIndex > 0 ? this.comLedType.SelectedIndex : 0);
            AppSettings.CurrentSetting.OpenLastOpenedVideo = this.chkOpenLastOpenedVideo.Checked;
            AppSettings.CurrentSetting.ShowOnlyListenedPark = this.chkShowOnlyListenedPark.Checked;
            AppSettings.CurrentSetting.Debug = this.chkDebug.Checked;
            AppSettings.CurrentSetting.Optimized = this.chkOptimized.Checked;
            AppSettings.CurrentSetting.NeedPasswordWhenExit = this.chkNeedPasswordWhenExit.Checked;
            AppSettings.CurrentSetting.EnableTTS = this.chkEnableTTS.Checked;
            AppSettings.CurrentSetting.EnlargeMemo = this.chkEnlargeMemo.Checked;
            AppSettings.CurrentSetting.ChargeAfterMemo = this.chkChargeAfterMemo.Checked;
            AppSettings.CurrentSetting.ShowAPMMonitor = this.chkShowAPMMonitor.Checked;
            AppSettings.CurrentSetting.EnableZST = this.chkEnableZST.Checked;
            AppSettings.CurrentSetting.ZSTReaderIP = this.txtZSTReaderIP.IP;
            AppSettings.CurrentSetting.EnableWriteCard = this.chkEnableWriteCard.Checked;
            AppSettings.CurrentSetting.AuotAddToFirewallException = this.chkAuotAddToFirewallException.Checked;
            if (AppSettings.CurrentSetting.ParkingCommunicationIP != this.comParkingCommunicationIP.Text)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_ParkingCommunicationIPChangedAlert);
            }
            AppSettings.CurrentSetting.ParkingCommunicationIP = this.comParkingCommunicationIP.Text;
            AppSettings.CurrentSetting.CheckConnectionWithPing = this.chkCheckConnectionWithPing.Checked;
        }
        #endregion

        #region 用户设置
        private void ShowUserSetting(UserSetting us)
        {
            this.comVideoType.Items.Clear();
            this.comVideoType.Items.Add(Resources.Resource1.FrmSystemOption_Type + " " + "A");
            this.comVideoType.Items.Add(Resources.Resource1.FrmSystemOption_Type + " " + "X");

            this.txtCompanyName.Text = us.CompanyName;
            if (us.EnableDeleteOverTimeImages)  //数据库优化
            {
                this.chkEnableDeleteOverTimeImages.Checked = true;
                this.txtMonth.IntergerValue = us.Month;
            }
            this.chkEnableForceShifting.Checked = us.EnableForceShifting;//强制交班
            if (us.ForceShiftingTime != null)
            {
                this.dtForceShiftingTime.Value = new DateTime(2011, 1, 1, us.ForceShiftingTime.Hour, us.ForceShiftingTime.Minute, 0);
            }
            this.chkOneKeyOpenDoor.Checked = us.OneKeyOpenDoor;
            this.chkWegen34Enable.Checked = us.WegenType == Ralid.GeneralLibrary.CardReader.WegenType.Wengen34;
            this.chkSnapshotWhenCarArrive.Checked = us.SnapshotWhenCarArrive;
            this.chkInputHandInCashWhenSettle.Checked = us.InputHandInCashWhenSettle;
            this.chkEnableOutdoorLed.Checked = us.EnableOutdoorLed;
            this.gridPaymentComments.Rows.Clear();  //收费说明
            if (us.PaymentComments != null && us.PaymentComments.Count > 0)
            {
                foreach (string comment in us.PaymentComments)
                {
                    int row = this.gridPaymentComments.Rows.Add(1);
                    this.gridPaymentComments.Rows[row].Cells["colComment"].Value = comment;
                }
            }
            this.txtMinTempCard.IntergerValue = us.MinTempCard;
            //车牌识别
            this.chkEnableCarPlateRecognize.Checked = us.EnableCarPlateRecognize;
            this.txtMaxCarPlateErrorChar.IntergerValue = us.MaxCarPlateErrorChar;
            this.rdFixNone.Checked = (!us.FixCardEnterAndExitWaitWhenCarPlateFail && !us.FixCardExitWaitWhenCarPlateFail);
            this.rdFixOnlyExit.Checked = us.FixCardExitWaitWhenCarPlateFail;
            this.rdFixEnterAndExit.Checked = us.FixCardEnterAndExitWaitWhenCarPlateFail;
            this.rdTempNone.Checked = !us.TempCardExitWaitWhenCarPlateFail;
            this.rdTempExit.Checked = us.TempCardExitWaitWhenCarPlateFail;
            this.rdSoftWareRecognize.Checked = us.SoftWareCarPlateRecognize;
            this.rdHardWareRecognize.Checked = us.HardWareCarPlateRecognize;
            this.comVideoType.SelectedIndex = us.VideoType;
            this.chkOperatorCardCashWhenSettle.Checked = us.OperatorCardCashWhenSettle;
        }

        private UserSetting GetUserSettingFromInput()
        {
            UserSetting info = new UserSetting();
            info.CompanyName = this.txtCompanyName.Text;
            info.EnableDeleteOverTimeImages = this.chkEnableDeleteOverTimeImages.Checked;
            info.Month = this.txtMonth.IntergerValue;
            info.EnableForceShifting = this.chkEnableForceShifting.Checked;
            info.OneKeyOpenDoor = this.chkOneKeyOpenDoor.Checked;
            info.ForceShiftingTime = new TimeEntity(this.dtForceShiftingTime.Value.Hour, this.dtForceShiftingTime.Value.Minute);
            info.WegenType = (chkWegen34Enable.Checked ? Ralid.GeneralLibrary.CardReader.WegenType.Wengen34 : Ralid.GeneralLibrary.CardReader.WegenType.Wengen26);
            info.SnapshotWhenCarArrive = chkSnapshotWhenCarArrive.Checked;
            info.InputHandInCashWhenSettle = chkInputHandInCashWhenSettle.Checked;
            info.EnableOutdoorLed = chkEnableOutdoorLed.Checked;
            if (this.gridPaymentComments.Rows.Count > 0)
            {
                info.PaymentComments = new List<string>();
                for (int i = 0; i < this.gridPaymentComments.Rows.Count; i++)
                {
                    string comment = (string)this.gridPaymentComments.Rows[i].Cells["colComment"].Value;
                    if (!string.IsNullOrEmpty(comment)) info.PaymentComments.Add(comment);
                }
            }
            info.MinTempCard = this.txtMinTempCard.IntergerValue;
            //车片识别
            info.EnableCarPlateRecognize = this.chkEnableCarPlateRecognize.Checked;
            info.MaxCarPlateErrorChar = this.txtMaxCarPlateErrorChar.IntergerValue;
            info.FixCardEnterAndExitWaitWhenCarPlateFail = rdFixEnterAndExit.Checked;
            info.FixCardExitWaitWhenCarPlateFail = rdFixOnlyExit.Checked;
            info.TempCardExitWaitWhenCarPlateFail = rdTempExit.Checked;
            info.SoftWareCarPlateRecognize = rdSoftWareRecognize.Checked;
            info.HardWareCarPlateRecognize = rdHardWareRecognize.Checked;
            info.VideoType = comVideoType.SelectedIndex >= 0 ? comVideoType.SelectedIndex : 0;
            info.OperatorCardCashWhenSettle = chkOperatorCardCashWhenSettle.Checked;
            return info;
        }
        #endregion

        #region 车型设置
        private void ShowCarTypeSetting(CarTypeSetting cts)
        {
            if (cts.CarTypes != null)
            {
                foreach (CarType carType in cts.CarTypes)
                {
                    int row = carTypeGrid.Rows.Add();
                    carTypeGrid.Rows[row].Cells["colCarTypeDescr"].Value = carType.Description;
                    carTypeGrid.Rows[row].Cells["colVoiceNumber"].Value = carType.HardwareCarType;
                }
            }
        }

        private CarTypeSetting GetCarTypeSettingFromInput()
        {
            CarTypeSetting cts = new CarTypeSetting();
            cts.Clear();
            for (int i = 0; i < carTypeGrid.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty((string)carTypeGrid.Rows[i].Cells["colCarTypeDescr"].Value))
                {
                    CarType carType = new CarType();
                    carType.ID = i;
                    carType.Description = (string)carTypeGrid.Rows[i].Cells["colCarTypeDescr"].Value;
                    if (carTypeGrid.Rows[i].Cells["colVoiceNumber"].Value != null) carType.HardwareCarType = (int)carTypeGrid.Rows[i].Cells["colVoiceNumber"].Value;
                    cts.Add(carType);
                }
            }
            return cts;
        }

        private void carTypeGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewComboBoxCell voiceCol = carTypeGrid.Rows[e.RowIndex].Cells["colVoiceNumber"] as DataGridViewComboBoxCell;
            voiceCol.DataSource = new[] {new{ID=0,Description=CarTypeDescription .CarType_CarOrA },
                                             new {ID=1,Description=CarTypeDescription .CarType_TruckOrB },
                                             new{ID=2,Description=CarTypeDescription .CarType_SuperTruckOrC },
                                             new{ID=3,Description=CarTypeDescription .CarType_MotorOrD }};
            voiceCol.DisplayMember = "Description";
            voiceCol.ValueMember = "ID";
        }

        private void carTypeGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewComboBoxCell voiceCol = carTypeGrid.Rows[e.RowIndex].Cells["colVoiceNumber"] as DataGridViewComboBoxCell;
            voiceCol.DataSource = new[] {new{ID=0,Description=CarTypeDescription .CarType_CarOrA },
                                             new {ID=1,Description=CarTypeDescription .CarType_TruckOrB },
                                             new{ID=2,Description=CarTypeDescription .CarType_SuperTruckOrC },
                                             new{ID=3,Description=CarTypeDescription .CarType_MotorOrD }};
            voiceCol.DisplayMember = "Description";
            voiceCol.ValueMember = "ID";
        }
        #endregion

        #region 通道权限
        private void ShowAccessSetting(AccessSetting acs)
        {
            this.accessGrid.RowCount = 0;
            if (acs.Accesses != null && acs.Accesses.Count > 0)
            {
                foreach (AccessInfo info in acs.Accesses)
                {
                    int row = this.accessGrid.Rows.Add();
                    accessGrid.Rows[row].Tag = info;
                    accessGrid.Rows[row].Cells["colAccessLevelName"].Value = info.Name;
                }
            }
        }

        private AccessSetting GetAccessSettingFromInput()
        {
            AccessSetting acs = new AccessSetting();
            acs.Accesses = new List<AccessInfo>();
            foreach (DataGridViewRow row in this.accessGrid.Rows)
            {
                AccessInfo a = row.Tag as AccessInfo;
                if (a.ID == 0) a.ID = (byte)(acs.Accesses.Count > 0 ? acs.Accesses.Max(item => item.ID) + 1 : 1);  //新增加的权限组要有新的ID
                acs.Accesses.Add(a);
            }
            return acs;
        }

        private void accessGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FrmAccessLevelDetail frm = new FrmAccessLevelDetail();
                frm.GetAccesses += GetAccessesHandler;
                frm.UpdatingItem = accessGrid.Rows[e.RowIndex].Tag as AccessInfo;

                frm.ItemUpdated += delegate(object obj, ItemUpdatedEventArgs args)
                {
                    AccessInfo info = args.UpdatedItem as AccessInfo;
                    accessGrid.Rows[e.RowIndex].Tag = info;
                    accessGrid.Rows[e.RowIndex].Cells["colAccessLevelName"].Value = info.Name;
                    frm.Close();
                };
                frm.ShowDialog();
            }
        }

        private void mnu_AddAccess_Click(object sender, EventArgs e)
        {
            FrmAccessLevelDetail frm = new FrmAccessLevelDetail();
            frm.GetAccesses += GetAccessesHandler;

            frm.ItemAdded += delegate(object obj, ItemAddedEventArgs args)
            {
                AccessInfo info = args.AddedItem as AccessInfo;
                foreach (DataGridViewRow r in accessGrid.Rows)
                {
                    if (r.Cells["colAccessLevelName"].Value.ToString() == info.Name)
                    {
                        MessageBox.Show(string.Format(Resources.Resource1.FrmSystemOption_SameAccessName, info.Name));
                        return;
                    }
                }
                int row = accessGrid.Rows.Add();
                accessGrid.Rows[row].Tag = info;
                accessGrid.Rows[row].Cells["colAccessLevelName"].Value = info.Name;
                frm.Close();
            };
            frm.ShowDialog();
        }

        private void mnu_DeleteAccess_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in accessGrid.SelectedRows)
            {
                AccessInfo access = row.Tag as AccessInfo;
                accessGrid.Rows.Remove(row);
            }
        }


        #endregion

        #region 节假日
        private void ShowHolidaySetting(HolidaySetting hs)
        {
            this.chkSaturdayIsHoliday.Checked = hs.SaturdayIsRest;
            this.chkSundayIsHoliday.Checked = hs.SundayIsRest;
            holidayGrid.RowCount = 0;
            foreach (HolidayInfo info in hs.Holidays)
            {
                int row = holidayGrid.Rows.Add();
                holidayGrid.Rows[row].Tag = info;
                holidayGrid.Rows[row].Cells["colStartDate"].Value = info.StartDate.ToString("yyyy-MM-dd");
                holidayGrid.Rows[row].Cells["colEndDate"].Value = info.EndDate.ToString("yyyy-MM-dd");
            }
        }

        private HolidaySetting GetHolidaySettingFromInput()
        {
            HolidaySetting hs = new HolidaySetting();
            hs.SaturdayIsRest = chkSaturdayIsHoliday.Checked;
            hs.SundayIsRest = chkSundayIsHoliday.Checked;
            foreach (DataGridViewRow row in holidayGrid.Rows)
            {
                HolidayInfo info = row.Tag as HolidayInfo;
                hs.Holidays.Add(info);
            }
            return hs;
        }

        private void holidayGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FrmHolidayDetail frm = new FrmHolidayDetail();
                frm.UpdatingItem = holidayGrid.Rows[e.RowIndex].Tag as HolidayInfo;

                frm.ItemUpdated += delegate(object obj, ItemUpdatedEventArgs args)
                {
                    HolidayInfo info = args.UpdatedItem as HolidayInfo;
                    holidayGrid.Rows[e.RowIndex].Tag = info;
                    holidayGrid.Rows[e.RowIndex].Cells["colStartDate"].Value = info.StartDate.ToString("yyyy-MM-dd");
                    holidayGrid.Rows[e.RowIndex].Cells["colEndDate"].Value = info.EndDate.ToString("yyyy-MM-dd");
                    frm.Close();
                };
                frm.ShowDialog();
            }
        }


        private void mnu_Add_Click(object sender, EventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard && HolidaySetting.Current.Holidays.Count > 7)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_MostHoliday);
                return;
            }
            FrmHolidayDetail frm = new FrmHolidayDetail();
            frm.IsAdding = true;

            frm.ItemAdded += delegate(object obj, ItemAddedEventArgs args)
            {
                HolidayInfo info = args.AddedItem as HolidayInfo;
                int row = holidayGrid.Rows.Add();
                holidayGrid.Rows[row].Tag = info;
                holidayGrid.Rows[row].Cells["colStartDate"].Value = info.StartDate.ToString("yyyy-MM-dd");
                holidayGrid.Rows[row].Cells["colEndDate"].Value = info.EndDate.ToString("yyyy-MM-dd");
                frm.Close();
            };
            frm.ShowDialog();
        }

        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in holidayGrid.SelectedRows)
            {
                holidayGrid.Rows.Remove(row);
            }
        }

        #endregion

        #region 收费选项
        private void ShowTollOption(TollOptionSetting tos)
        {
            this.txtFreeTimeAfterPay.IntergerValue = (int)tos.FreeTimeAfterPay;
            this.rdYuan.Checked = tos.PointCount == 0;
            this.rdJiao.Checked = tos.PointCount == 1;
        }

        private TollOptionSetting GetTollOptionFromInput()
        {
            TollOptionSetting tos = new TollOptionSetting();
            tos.FreeTimeAfterPay = this.txtFreeTimeAfterPay.IntergerValue;
            tos.PointCount = (byte)(rdYuan.Checked ? 0 : 1);
            return tos;
        }
        #endregion

        #region 收费标准
        private void InitTariffGridRow(DataGridViewRow row, CardType cardType, CarType carType, TariffSetting ts)
        {
            TariffBase tariff = null;

            row.Cells["colCardType"].Value = cardType.Name;
            row.Cells["colCardType"].Tag = cardType.ID;

            row.Cells["colCarType"].Value = carType.Description;
            row.Cells["colCarType"].Tag = carType.ID;

            tariff = ts.GetTariff(cardType.ID, carType.ID, TariffType.Normal);
            row.Cells["colGeneral"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colGeneral"].Tag = (tariff != null) ? tariff : null;

            tariff = ts.GetTariff(cardType.ID, carType.ID, TariffType.Holiday);
            row.Cells["colHoliday"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colHoliday"].Tag = (tariff != null) ? tariff : null;

            tariff = ts.GetTariff(cardType.ID, carType.ID, TariffType.InnerRoom);
            row.Cells["colInnerRoom"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colInnerRoom"].Tag = (tariff != null) ? tariff : null;

            tariff = ts.GetTariff(cardType.ID, carType.ID, TariffType.HolidayAndInnerRoom);
            row.Cells["colHolidayAndInnerRoom"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colHolidayAndInnerRoom"].Tag = (tariff != null) ? tariff : null;
        }

        private void ShowTariffSetting(TariffSetting ts)
        {
            tariffGrid.Columns["colGeneral"].Tag = TariffType.Normal;
            tariffGrid.Columns["colHoliday"].Tag = TariffType.Holiday;
            tariffGrid.Columns["colInnerRoom"].Tag = TariffType.InnerRoom;
            tariffGrid.Columns["colHolidayAndInnerRoom"].Tag = TariffType.HolidayAndInnerRoom;

            List<CardType> cardTtypes = CardType.GetBaseCardTypes();
            if (CustomCardTypeSetting.Current != null && CustomCardTypeSetting.Current.CardTypes != null)
            {
                cardTtypes.AddRange(CustomCardTypeSetting.Current.CardTypes);
            }
            foreach (CardType cardType in cardTtypes)
            {
                foreach (CarType carType in CarTypeSetting.Current.CarTypes)
                {
                    int row = tariffGrid.Rows.Add();
                    InitTariffGridRow(tariffGrid.Rows[row], cardType, carType, ts);
                }
            }
        }

        private TariffSetting GetTariffSettingFromInput()
        {
            TariffSetting ts = new TariffSetting();

            foreach (DataGridViewRow row in tariffGrid.Rows)
            {
                for (int i = 1; i < tariffGrid.Columns.Count; i++)
                {
                    TariffBase tariff = row.Cells[i].Tag as TariffBase;
                    if (tariff != null)
                    {
                        ts.TariffArray.Add(tariff);
                    }
                }
            }
            return ts;
        }

        private void tariffGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 1)
            {
                FrmTariffSelection frm = new FrmTariffSelection();
                if (tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
                {
                    frm.SelectedTariff = tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as TariffBase;
                }
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    TariffBase info = frm.SelectedTariff;
                    info.CardType = Convert.ToByte(tariffGrid.Rows[e.RowIndex].Cells["colCardType"].Tag);
                    info.CarType = Convert.ToByte(tariffGrid.Rows[e.RowIndex].Cells["colCarType"].Tag);
                    if (tariffGrid.Columns[e.ColumnIndex].Tag is TariffType)
                    {
                        info.TariffType = (TariffType)tariffGrid.Columns[e.ColumnIndex].Tag;
                    }
                    tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = info.ToString();
                    tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = info;
                }
            }
        }

        private void mnu_Clear_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in tariffGrid.SelectedCells)
            {
                if (cell.Tag != null && cell.Tag is TariffBase)
                {
                    cell.Tag = null;
                    cell.Value = "N/A";
                }
            }
        }
        #endregion

        #region 自定义卡片类型
        private void ShowCustomCardTypeSetting(CustomCardTypeSetting ccts)
        {
            this.CardTypeGrid.Rows.Clear();
            CardType[] items = ccts.CardTypes;
            if (items != null && items.Length > 0)
            {
                foreach (CardType item in items)
                {
                    int row = CardTypeGrid.Rows.Add();
                    byte baseID = CardType.GetBaseCardType(item.ID).ID;
                    if (baseID == CardType.UserDefinedCard1.ID
                        || baseID == CardType.UserDefinedCard2.ID)
                    {
                        DataGridViewComboBoxCell baseCardType = CardTypeGrid.Rows[row].Cells["colBaseCardType"] as DataGridViewComboBoxCell;

                        baseCardType.DataSource = new[]
                            {
                                new {ID=CardType.UserDefinedCard1.ID  ,Description=CardType.UserDefinedCard1.ToString () },
                                new{ID=CardType.UserDefinedCard2.ID  ,Description=CardType.UserDefinedCard2 .ToString () },
                            };
                    }
                    CardTypeGrid.Rows[row].Cells["colName"].Value = item.Name;
                    CardTypeGrid.Rows[row].Cells["colBaseCardType"].Value = CardType.GetBaseCardType(item.ID).ID;
                    //TariffBase tariff = item.GetTariff();
                    //CardTypeGrid.Rows[row].Cells["colTariff"].Value = tariff != null ? tariff.ToString() : "N/A";
                    //CardTypeGrid.Rows[row].Cells["colTariff"].Tag = tariff;
                }
            }
        }

        private CustomCardTypeSetting GetCustomCardTypeFromInput()
        {
            if (CardTypeGrid.Rows.Count > 0)
            {
                CustomCardTypeSetting setting = new CustomCardTypeSetting();
                foreach (DataGridViewRow row in CardTypeGrid.Rows)
                {
                    if (row.Cells["colName"].Value != null && row.Cells["colBaseCardType"].Value != null)
                    {
                        //setting.AddCardType(row.Cells["colName"].Value.ToString(), (byte)(row.Cells["colBaseCardType"].Value), (row.Cells["colTariff"].Tag as TariffBase));
                        setting.AddCardType(row.Cells["colName"].Value.ToString(), (byte)(row.Cells["colBaseCardType"].Value));
                    }
                }
                return setting;
            }
            return null;
        }

        private void customCardTypeGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewComboBoxCell baseCardType = this.CardTypeGrid.Rows[e.RowIndex].Cells["colBaseCardType"] as DataGridViewComboBoxCell;
            //if (GlobalVariables.IsNETParkAndOffLie)
            //{
            //    baseCardType.DataSource = new[]
            //    {
            //    new {ID=CardType.UserDefinedCard1.ID  ,Description=CardType.UserDefinedCard1.ToString () },
            //    new{ID=CardType.UserDefinedCard2.ID  ,Description=CardType.UserDefinedCard2 .ToString () },
            //    };
            //}
            //else
            //{
            //    baseCardType.DataSource = new[]
            //    {
            //    new {ID=CardType.MonthRentCard .ID  ,Description=CardType.MonthRentCard .ToString () },
            //    new{ID=CardType.OwnerCard.ID  ,Description=CardType.OwnerCard .ToString () },
            //    new{ID=CardType.PrePayCard.ID ,Description=CardType.PrePayCard.ToString ()},
            //    new{ID=CardType.TempCard.ID ,Description=CardType.TempCard .ToString () },
            //    };
            //}
            //baseCardType.DisplayMember = "Description";
            //baseCardType.ValueMember = "ID";
            //CardTypeGrid.Rows[e.RowIndex].Cells["colTariff"].Value =
            //    CardTypeGrid.Rows[e.RowIndex].Cells["colTariff"].Tag != null ? CardTypeGrid.Rows[e.RowIndex].Cells["colTariff"].Tag.ToString() : "N/A";
        }

        private void customCardTypeGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && CardTypeGrid.Columns[e.ColumnIndex].Name == "colTariff")
            {
                FrmTariffSelection frm = new FrmTariffSelection();
                if (CardTypeGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null) frm.SelectedTariff = CardTypeGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as TariffBase;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CardTypeGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.SelectedTariff;
                    CardTypeGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = frm.SelectedTariff.ToString();
                }
            }
        }

        private void CardTypeGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void CardTypeGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewComboBoxCell baseCardType = this.CardTypeGrid.Rows[e.RowIndex].Cells["colBaseCardType"] as DataGridViewComboBoxCell;
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                baseCardType.DataSource = new[]
                {
                new {ID=CardType.UserDefinedCard1.ID  ,Description=CardType.UserDefinedCard1.ToString () },
                new{ID=CardType.UserDefinedCard2.ID  ,Description=CardType.UserDefinedCard2 .ToString () },
                };
            }
            else
            {
                baseCardType.DataSource = new[]
                {
                new {ID=CardType.MonthRentCard.ID  ,Description=CardType.MonthRentCard .ToString () },
                new{ID=CardType.OwnerCard.ID  ,Description=CardType.OwnerCard .ToString () },
                new{ID=CardType.PrePayCard.ID ,Description=CardType.PrePayCard.ToString ()},
                new{ID=CardType.TempCard.ID ,Description=CardType.TempCard .ToString () },
                };
            }
            baseCardType.DisplayMember = "Description";
            baseCardType.ValueMember = "ID";
            //CardTypeGrid.Rows[e.RowIndex].Cells["colTariff"].Value =
            //    CardTypeGrid.Rows[e.RowIndex].Cells["colTariff"].Tag != null ? CardTypeGrid.Rows[e.RowIndex].Cells["colTariff"].Tag.ToString() : "N/A";                        
        }

        private void mnu_ClearTariff_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in CardTypeGrid.SelectedRows)
            {
                row.Cells["colTariff"].Value = "N/A";
                row.Cells["colTariff"].Tag = null;
            }
        }

        private int GetCardTypeCount(byte baseID)
        {
            int count = 0;
            foreach (DataGridViewRow row in CardTypeGrid.Rows)
            {
                if (row.Cells["colBaseCardType"].Value != null
                    && (byte)row.Cells["colBaseCardType"].Value == baseID)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region 密钥设置
        private void ShowKeySetting(KeySetting ks)
        {
            this.txtCardSection.IntergerValue = ks.ParkingSection;
        }
        private KeySetting GetKeySettingFromInput()
        {
            KeySetting ks = new KeySetting();
            ks.ParkingSection = (byte)this.txtCardSection.IntergerValue;
            if (chkChangeKey.Checked)
            {
                if (rdbInputKey.Checked)
                {
                    ks.ParkingKey = this.txtNewKey.HexValue;
                }
                return ks;
            }
            else
            {
                ks.ParkingKey = KeySetting.Current.ParkingKey;
            }
            return ks;
        }

        private void InitKeyInput()
        {
            this.chkChangeKey.Checked = false;
            this.chkShowOld.Checked = false;
            this.rdbInputKey.Checked = true;
            this.txtOldKey.Text = string.Empty;
            this.txtNewKey.Text = string.Empty;
            this.txtConfirmKey.Text = string.Empty;
            this.pnlOldKey.Enabled = KeySetting.Current.ParkingKeyIsValid;
        }
        #endregion

        #region 基本卡片类型设置
        private void ShowBaseCardTypeSetting(BaseCardTypeSetting bcts)
        {
            this.BaseCardTypeGrid.Rows.Clear();
            List<CardType> cardTtypes = CardType.GetBaseCardTypes();
            foreach (CardType item in cardTtypes)
            {
                int row = BaseCardTypeGrid.Rows.Add();
                BaseCardTypeGrid.Rows[row].Tag = item;
                BaseCardTypeGrid.Rows[row].Cells["colBaseCardTypeName"].Value = item.DefaultName;
                BaseCardTypeGrid.Rows[row].Cells["colUserDefinedName"].Value = item.Name;
            }
        }

        private BaseCardTypeSetting GetBaseCardTypeFromInput()
        {
            if (BaseCardTypeGrid.Rows.Count > 0)
            {
                BaseCardTypeSetting setting = new BaseCardTypeSetting();
                foreach (DataGridViewRow row in BaseCardTypeGrid.Rows)
                {
                    if (row.Cells["colBaseCardTypeName"].Value.ToString().Trim() != row.Cells["colUserDefinedName"].Value.ToString().Trim()
                        && row.Tag is CardType)
                    {
                        CardType cardType = row.Tag as CardType;
                        setting.AddUserDefinedName(cardType, row.Cells["colUserDefinedName"].Value.ToString());
                    }
                }
                return setting;
            }
            return null;
        }
        #endregion

        private void btnClearExpiredSnapShot_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult ret = MessageBox.Show(Resources.Resource1.FrmSystemOptions_ClearSnapshotQuery, Resources.Resource1.Form_Query, MessageBoxButtons.YesNo);
                if (ret == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DateTime dt = DateTime.Today.AddMonths(-UserSetting.Current.Month);
                    SnapShotBll ssb = new SnapShotBll(AppSettings.CurrentSetting.ParkConnect);
                    ssb.DeleteAllSnapShotBefore(dt);
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(Resources.Resource1.FrmSystemOptions_ClearSnapshotComplete, Resources.Resource1.Form_Success);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Arrow;
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
    }
}
