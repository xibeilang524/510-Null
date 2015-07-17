using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BLL;
using System.Data.SqlClient;
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

            GlobalVariables.SetCardReaderKeysetting();

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
            ssb.SaveSettingWithUnitWork<UserSetting>(UserSetting.Current);

            BaseCardTypeSetting.Current = GetBaseCardTypeFromInput();
            ssb.SaveSettingWithUnitWork<BaseCardTypeSetting>(BaseCardTypeSetting.Current);

            CarTypeSetting.Current = GetCarTypeSettingFromInput();
            ssb.SaveSettingWithUnitWork<CarTypeSetting>(CarTypeSetting.Current);

            AccessSetting.Current = GetAccessSettingFromInput();
            ssb.SaveSettingWithUnitWork<AccessSetting>(AccessSetting.Current);

            HolidaySetting.Current = GetHolidaySettingFromInput();
            ssb.SaveSettingWithUnitWork<HolidaySetting>(HolidaySetting.Current);

            TariffSetting.Current = GetTariffSettingFromInput();
            TariffSetting.Current.TariffOption = GetTollOptionFromInput();
            ssb.SaveSettingWithUnitWork<TariffSetting>(TariffSetting.Current);

            CustomCardTypeSetting.Current = GetCustomCardTypeFromInput();
            ssb.SaveSettingWithUnitWork<CustomCardTypeSetting>(CustomCardTypeSetting.Current);

            KeySetting.Current = GetKeySettingFromInput();
            ssb.SaveSettingWithUnitWork<KeySetting>(KeySetting.Current);

            ssb.UnitWorkCommit();

            GlobalVariables.SetCardReaderKeysetting();
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
            this.tab1.TabPages.Remove(this.tabGeneral);


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


            this.rdbCPU.CheckedChanged += rdbCPU_CheckedChanged_1;
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
            if (chkEnableDeleteOverTimeCardEvents.Checked && txtCardEventMonth.IntergerValue <= 0)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidMonth);
                this.txtCardEventMonth.Focus();
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

            if(!CheckParkingCouponInput()) return false;

            if (!CheckRotationInput()) return false;

            if (!CheckImageDBConnectString()) return false;

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
            if (this.rdbIC.Checked)
            {
                //IC卡检查
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
            }
            else 
            {
                //CPU卡检查                
                if (this.rdbFixedKey.Checked)
                {
                    if (this.txtCPUKey.HexValue == null || this.txtCPUKey.HexValue.Length != 16)
                    {
                        MessageBox.Show(Resources.Resource1.FrmSystemOption_CPUKeyInvalid);
                        this.tab1.SelectedTab = this.tabKey;
                        this.txtCPUKey.Focus();
                        return false;
                    }
                }

                //检查CPU密钥设置是否已更改，如更改了，需用户确认
                AlgorithmType algorith = this.rdbSM1.Checked ? AlgorithmType.SM1 : AlgorithmType.DES3;
                ReaderReadMode mode = this.rdbSamNo.Checked ? ReaderReadMode.SAM : ReaderReadMode.FixedKey;
                byte[] cpuKey=this.txtCPUKey.HexValue;
                KeySetting ks = KeySetting.Current;
                if (ks != null)
                {
                    if (algorith != ks.AlgorithmType
                        || mode != ks.ReaderReadMode
                        || (ks.ReaderReadMode== ReaderReadMode.FixedKey &&!Ralid.GeneralLibrary.HexStringConverter.HexEquals(cpuKey, ks.ParkingCPUKeyBase)))
                    {
                        if (MessageBox.Show(Resources.Resource1.FrmSystemOption_CPUKeyNote,Resources.Resource1.FrmSystemOption_SaveCPUKeyComfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            this.tab1.SelectedTab = this.tabKey;
                            this.rdbCPU.Focus();
                            return false;
                        }
                    }
                }
                    

            }
            return true;
        }

        private bool CheckParkingCouponInput()
        {
            if (this.gridParkingCoupon.Rows.Count > 0)
            {
                decimal pravalue = 0;

                for (int i = 0; i < this.gridParkingCoupon.Rows.Count; i++)
                {
                    if (!this.gridParkingCoupon.Rows[i].IsNewRow)
                    {
                        if (string.IsNullOrEmpty(this.gridParkingCoupon.Rows[i].Cells["colCouponName"].Value as string))
                        {
                            MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidCouponName);
                            this.tab1.SelectedTab = this.tabParkingCoupon;
                            this.gridParkingCoupon.Focus();
                            this.gridParkingCoupon.CurrentCell = this.gridParkingCoupon.Rows[i].Cells["colCouponName"];
                            return false;
                        }

                        if (this.gridParkingCoupon.Rows[i].Cells["colCouponValue"].Value == null
                            || !decimal.TryParse(this.gridParkingCoupon.Rows[i].Cells["colCouponValue"].Value.ToString(), out pravalue)
                            || pravalue < 0)
                        {
                            MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidCouponValue);
                            this.tab1.SelectedTab = this.tabParkingCoupon;
                            this.gridParkingCoupon.Focus();
                            this.gridParkingCoupon.CurrentCell = this.gridParkingCoupon.Rows[i].Cells["colCouponValue"];
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CheckRotationInput()
        {
            if (this.gridRotation.Rows.Count > 0)
            {
                for (int i = 0; i < this.gridRotation.Rows.Count; i++)
                {
                    if (!this.gridRotation.Rows[i].IsNewRow)
                    {
                        int entranceID = Convert.ToInt32(this.gridRotation.Rows[i].Cells["colRotationEntrance"].Value);
                        if (entranceID <= 0)
                        {
                            MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidRotationEntrance);
                            this.tab1.SelectedTab = this.tabRotation;
                            this.gridRotation.Focus();
                            this.gridRotation.CurrentCell = this.gridRotation.Rows[i].Cells["colRotationEntrance"];
                            this.gridRotation.CurrentCell.Selected = true;
                            return false;
                        }

                        int number = Convert.ToInt32(this.gridRotation.Rows[i].Cells["colRotationNumber"].Value);
                        if (number <= 0)
                        {
                            MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidRotationNumber);
                            this.tab1.SelectedTab = this.tabRotation;
                            this.gridRotation.Focus();
                            this.gridRotation.CurrentCell = this.gridRotation.Rows[i].Cells["colRotationNumber"];
                            this.gridRotation.CurrentCell.Selected = true;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool CheckImageDBConnectString()
        {
            if (!string.IsNullOrEmpty(this.txtServer.Text.Trim())
                || !string.IsNullOrEmpty(this.txtServer.Text.Trim()))
            {
                //其中一个部位空时，表示设置了图片数据库
                if (string.IsNullOrEmpty(this.txtServer.Text.Trim()))
                {
                    MessageBox.Show(Resources.Resource1.FrmLogin_InvalidServer);
                    this.txtServer.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.txtDataBase.Text.Trim()))
                {
                    MessageBox.Show(Resources.Resource1.FrmLogin_InvalidDataBase);
                    this.txtDataBase.Focus();
                    return false;
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

            if (tab1.SelectedTab == tabTariff)
            {
                this.lblMsg.Text = "该处费率为停车场共用费率，如需设置停车场单独费率，请在对应停车场处设置。";
                this.lblMsg.Visible = true;
            }
            else
            {
                this.lblMsg.Visible = false;
            }
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
                TariffSetting.Current.TariffOption = GetTollOptionFromInput();
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

                GlobalVariables.SetCardReaderKeysetting();

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
            this.comLedType.Items.Add(Resources.Resource1.LEDType_HSD);
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
            this.chkSwitchEntrance.Checked = AppSettings.CurrentSetting.SwitchEntrance;
            this.chkEnableHotel.Checked = AppSettings.CurrentSetting.EnableHotel;
        }

        private void SaveAppSetting()
        {
            if (!this.tab1.TabPages.Contains(this.tabGeneral)) return;

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
            AppSettings.CurrentSetting.SwitchEntrance = this.chkSwitchEntrance.Checked;
            AppSettings.CurrentSetting.EnableHotel = this.chkEnableHotel.Checked;
        }
        #endregion

        #region 用户设置
        private void ShowUserSetting(UserSetting us)
        {
            this.comVideoType.Items.Clear();
            this.comVideoType.Items.Add(Resources.Resource1.FrmSystemOption_Type + " " + "A");
            this.comVideoType.Items.Add(Resources.Resource1.FrmSystemOption_Type + " " + "X");
            this.comVideoType.Items.Add(Resources.Resource1.FrmSystemOption_Type + " " + "J");
            this.comVideoType.Items.Add(Resources.Resource1.FrmSystemOption_Type + " " + "D");

            this.txtCompanyName.Text = us.CompanyName;
            if (us.EnableDeleteOverTimeImages)  //数据库优化
            {
                this.chkEnableDeleteOverTimeImages.Checked = true;
            }
            if (us.Month > 0)
            {
                this.txtMonth.IntergerValue = us.Month; 
            }
            if (us.EnableDeleteOverTimeCardEvents)
            {
                this.chkEnableDeleteOverTimeCardEvents.Checked = true;
                this.txtCardEventMonth.IntergerValue = us.CardEventMonth;
            }
            if (us.CardEventMonth > 0)
            {
                this.txtCardEventMonth.IntergerValue = us.CardEventMonth;
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
            this.chkOperatorCardCashWhenSettle.Checked = us.OperatorCardCashWhenSettle;
            this.chkForbiddenEnterWhenSpeeding.Checked = us.ForbiddenEnterWhenSpeeding;
            this.chkForbiddenExitWhenSpeeding.Checked = us.ForbiddenExitWhenSpeeding;
            this.chkNotShowSettleDetail.Checked = us.NotShowSettleDetail;
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
            this.gridParkingCoupon.Rows.Clear();//停车券设置
            if (us.ParkingCoupon != null && us.ParkingCoupon.Count > 0)
            {
                foreach (ParkingCouponInfo coupon in us.ParkingCoupon)
                {
                    int row = this.gridParkingCoupon.Rows.Add();
                    this.gridParkingCoupon.Rows[row].Cells["colCouponName"].Value = coupon.Name;
                    this.gridParkingCoupon.Rows[row].Cells["colCouponValue"].Value = coupon.ParValue;
                }
            }
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
            this.comVideoType.SelectedIndex = this.comVideoType.Items.Count > us.VideoType ? us.VideoType : 0;
            this.chkFixCardAccessWhenRecognize.Checked = us.FixCardAccessWhenRecognize;

            //轮换设置
            this.chkEnableRotation.Checked = us.EnableRotation;
            this.txtRotationVacant.IntergerValue = us.RotationVacant;
            this.gridRotation.Rows.Clear();
            if (us.Rotations != null && us.Rotations.Count > 0)
            {
                foreach (int rkey in us.Rotations.Keys)
                {
                    foreach (RotationInfo rotation in us.Rotations[rkey])
                    {
                        int row = this.gridRotation.Rows.Add();
                        EntranceInfo entrance = ParkBuffer.Current.GetEntrance(rotation.EntranceID);
                        if (entrance != null) this.gridRotation.Rows[row].Cells["colRotationEntrance"].Value = rotation.EntranceID;
                        this.gridRotation.Rows[row].Cells["colRotationNumber"].Value = rotation.Number;
                    }
                }
            }

            if (!string.IsNullOrEmpty(us.ParkingImageConnStr))
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(us.ParkingImageConnStr);
                txtServer.Text = sb.DataSource;
                txtDataBase.Text = sb.InitialCatalog;
                if (sb.IntegratedSecurity)
                {
                    this.rdSystem.Checked = true;
                }
                else
                {
                    this.rdUser.Checked = true;
                    this.txtUserID.Text = sb.UserID;
                    this.txtPasswd.Text = sb.Password;
                }
            }
        }

        private UserSetting GetUserSettingFromInput()
        {
            UserSetting info = new UserSetting();
            info.CompanyName = this.txtCompanyName.Text;
            info.EnableDeleteOverTimeImages = this.chkEnableDeleteOverTimeImages.Checked;
            info.Month = this.txtMonth.IntergerValue;
            info.EnableDeleteOverTimeCardEvents = this.chkEnableDeleteOverTimeCardEvents.Checked;
            info.CardEventMonth = this.txtCardEventMonth.IntergerValue;
            info.EnableForceShifting = this.chkEnableForceShifting.Checked;
            info.OneKeyOpenDoor = this.chkOneKeyOpenDoor.Checked;
            info.ForceShiftingTime = new TimeEntity(this.dtForceShiftingTime.Value.Hour, this.dtForceShiftingTime.Value.Minute);
            info.WegenType = (chkWegen34Enable.Checked ? Ralid.GeneralLibrary.CardReader.WegenType.Wengen34 : Ralid.GeneralLibrary.CardReader.WegenType.Wengen26);
            info.SnapshotWhenCarArrive = chkSnapshotWhenCarArrive.Checked;
            info.InputHandInCashWhenSettle = chkInputHandInCashWhenSettle.Checked;
            info.EnableOutdoorLed = chkEnableOutdoorLed.Checked;
            info.OperatorCardCashWhenSettle = chkOperatorCardCashWhenSettle.Checked;
            info.ForbiddenEnterWhenSpeeding = chkForbiddenEnterWhenSpeeding.Checked;
            info.ForbiddenExitWhenSpeeding = chkForbiddenExitWhenSpeeding.Checked;
            info.NotShowSettleDetail = chkNotShowSettleDetail.Checked;
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
            if (this.gridParkingCoupon.Rows.Count > 0)
            {
                info.ParkingCoupon = new List<ParkingCouponInfo>();
                for (int i = 0; i < this.gridParkingCoupon.Rows.Count; i++)
                {
                    if (!this.gridParkingCoupon.Rows[i].IsNewRow)
                    {
                        string couponName = this.gridParkingCoupon.Rows[i].Cells["colCouponName"].Value as string;
                        decimal couponValue = Convert.ToDecimal(this.gridParkingCoupon.Rows[i].Cells["colCouponValue"].Value);
                        ParkingCouponInfo coupon = new ParkingCouponInfo();
                        coupon.Name = couponName;
                        coupon.ParValue = couponValue;

                        info.ParkingCoupon.Add(coupon);
                    }
                }
            }

            //车片识别
            info.EnableCarPlateRecognize = this.chkEnableCarPlateRecognize.Checked;
            info.MaxCarPlateErrorChar = this.txtMaxCarPlateErrorChar.IntergerValue;
            info.FixCardEnterAndExitWaitWhenCarPlateFail = rdFixEnterAndExit.Checked;
            info.FixCardExitWaitWhenCarPlateFail = rdFixOnlyExit.Checked;
            info.TempCardExitWaitWhenCarPlateFail = rdTempExit.Checked;
            info.SoftWareCarPlateRecognize = rdSoftWareRecognize.Checked;
            info.HardWareCarPlateRecognize = rdHardWareRecognize.Checked;
            info.VideoType = comVideoType.SelectedIndex >= 0 ? comVideoType.SelectedIndex : 0;
            info.FixCardAccessWhenRecognize = this.chkFixCardAccessWhenRecognize.Checked;

            //轮换设置
            info.EnableRotation = this.chkEnableRotation.Checked;
            info.RotationVacant = this.txtRotationVacant.IntergerValue;
            if (this.gridRotation.Rows.Count>0)
            {
                List<RotationInfo> rotations = new List<RotationInfo>();
                for (int i = 0; i < this.gridRotation.Rows.Count; i++)
                {
                    if (!this.gridRotation.Rows[i].IsNewRow)
                    {
                        if (this.gridRotation.Rows[i].Cells["colRotationEntrance"].Value != null)
                        {
                            RotationInfo rotation = new RotationInfo();
                            rotation.EntranceID = Convert.ToInt32(this.gridRotation.Rows[i].Cells["colRotationEntrance"].Value);
                            rotation.Number = Convert.ToInt32(this.gridRotation.Rows[i].Cells["colRotationNumber"].Value);
                            rotations.Add(rotation);
                        }
                    }
                }
                rotations = rotations.OrderBy(r => r.Number).ToList();
                info.Rotations = new Dictionary<int, List<RotationInfo>>();
                foreach (RotationInfo rotation in rotations)
                {
                    if (!info.Rotations.ContainsKey(rotation.Number))
                    {
                        info.Rotations.Add(rotation.Number, new List<RotationInfo>());
                    }
                    info.Rotations[rotation.Number].Add(rotation);
                }
            }

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = this.txtServer.Text.Trim();
            sb.InitialCatalog = this.txtDataBase.Text.Trim();
            sb.IntegratedSecurity = rdSystem.Checked;
            sb.UserID = this.txtUserID.Text.Trim();
            sb.Password = this.txtPasswd.Text.Trim();
            sb.PersistSecurityInfo = true;
            sb.ConnectTimeout = 3;
            if (!string.IsNullOrEmpty(sb.DataSource) && !string.IsNullOrEmpty(sb.ConnectionString))
            {
                this.Cursor = Cursors.WaitCursor;
                bool result = false;
                using (SqlConnection sqlconn = new SqlConnection(sb.ConnectionString))
                {
                    //测试图片数据库连接串的可用性
                    try
                    {
                        sqlconn.Open();
                        result = sqlconn.State == System.Data.ConnectionState.Open;
                    }
                    catch
                    {
                    }
                }
                this.Cursor = Cursors.Default;
                if (!result)
                {
                    MessageBox.Show(Resources.Resource1.FrmSystemOption_ImageDBConnErro);
                    sb.DataSource = "";
                }
                else
                {
                    MessageBox.Show(Resources.Resource1.FrmSystemOption_ImageDBConnSuccess);
                }

                if (!string.IsNullOrEmpty(sb.DataSource) && !string.IsNullOrEmpty(sb.InitialCatalog))
                {
                    //当设置了图片数据库时
                    info.ParkingImageConnStr = sb.ConnectionString;
                }
                else
                {
                    info.ParkingImageConnStr = string.Empty;
                }
                this.Cursor = Cursors.Arrow;
            }
            return info;
        }


        private void dgvRotation_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            List<EntranceInfo> entrances = ParkBuffer.Current.GetEntrances();
            entrances = entrances.Where(et => et.IsExitDevice == false).ToList();
            if (entrances.Count > 0)
            {
                DataGridViewComboBoxCell rotationCol = this.gridRotation.Rows[e.RowIndex].Cells["colRotationEntrance"] as DataGridViewComboBoxCell;
                rotationCol.DataSource = entrances;
                rotationCol.DisplayMember = "EntranceName";
                rotationCol.ValueMember = "EntranceID";
            }
        }

        private void dgvRotation_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            List<EntranceInfo> entrances = ParkBuffer.Current.GetEntrances();
            entrances = entrances.Where(et => et.IsExitDevice == false).ToList();
            if (entrances.Count > 0)
            {
                DataGridViewComboBoxCell rotationCol = this.gridRotation.Rows[e.RowIndex].Cells["colRotationEntrance"] as DataGridViewComboBoxCell;
                rotationCol.DataSource = entrances;
                rotationCol.DisplayMember = "EntranceName";
                rotationCol.ValueMember = "EntranceID";
            }
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
            if (string.IsNullOrEmpty(tos.MoneyUnit))
            {
                this.rdYuan.Checked = tos.PointCount == 0;
                this.rdJiao.Checked = tos.PointCount == 1;
            }
            else
            {
                this.rdCustom.Checked = true;
                this.txtMoneyUnit.Text = tos.MoneyUnit;
            }

            this.lblCouponUnit.Text = tos.GetMoneyUnit();
        }

        private TollOptionSetting GetTollOptionFromInput()
        {
            TollOptionSetting tos = new TollOptionSetting();
            tos.FreeTimeAfterPay = this.txtFreeTimeAfterPay.IntergerValue;
            if (rdCustom.Checked)
            {
                tos.PointCount = 0;//固定0个小数点
                tos.MoneyUnit = this.txtMoneyUnit.Text.Trim();
            }
            else
            {
                tos.PointCount = (byte)(rdYuan.Checked ? 0 : 1);
            }
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
            cardTtypes.Remove(CardType.Ticket);//纸票与临时卡使用同一种费率，所以这里就不在设置纸票的费率了
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
            TariffSetting ts = TariffSetting.Current;
            ts.TariffOption = GetTollOptionFromInput();
            ts.TariffArray.Clear();

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
            this.rdbIC.Checked = ks.ReaderReadMode == ReaderReadMode.MifareIC;
            this.txtCardSection.IntergerValue = ks.ParkingSection;

            this.rdbCPU.Checked = ks.ReaderReadMode != ReaderReadMode.MifareIC;

            this.rdb3Des.Checked = ks.AlgorithmType != AlgorithmType.SM1;
            this.rdbSM1.Checked = ks.AlgorithmType == AlgorithmType.SM1;
            if (ks.ReaderReadMode== ReaderReadMode.SAM)
            {
                this.rdbSamNo.Checked = true;
            }
            else
            {
                this.rdbFixedKey.Checked = true;
            }
            this.chkChangeCPUKey.Enabled = ks.ParkingCPUKeyIsValid;
            if (!ks.ParkingCPUKeyIsValid)
            {
                this.txtCPUKey.PasswordChar = char.MinValue;
            }
            this.txtCPUKey.Enabled = !ks.ParkingCPUKeyIsValid;
            this.txtCPUKey.HexValue = ks.ParkingCPUKeyBase;
        }
        private KeySetting GetKeySettingFromInput()
        {
            KeySetting ks = new KeySetting();

            if (this.rdbIC.Checked)
            {
                ks.ReaderReadMode = ReaderReadMode.MifareIC;
            }
            else
            {
                if (this.rdbSamNo.Checked)
                {
                    ks.ReaderReadMode = ReaderReadMode.SAM;
                }
                else
                {
                    ks.ReaderReadMode = ReaderReadMode.FixedKey;
                }
            }

            //IC卡密钥设置
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

            //CPU卡密钥设置
            ks.AlgorithmType = this.rdbSM1.Checked ? AlgorithmType.SM1 : AlgorithmType.DES3;
            if (this.rdbFixedKey.Checked)
            {
                ks.ParkingCPUKeyBase = this.txtCPUKey.HexValue;
            }
            else 
            {
                ks.ParkingCPUKeyBase = KeySetting.Current.ParkingCPUKeyBase;
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
                    if (txtMonth.IntergerValue <= 0)
                    {
                        MessageBox.Show(Resources.Resource1.FrmSystemOption_InvalidMonth);
                        this.txtMonth.Focus();
                        return;
                    }
                    UserSetting.Current.Month = txtMonth.IntergerValue;

                    this.Cursor = Cursors.WaitCursor;
                    DateTime dt = DateTime.Today.AddMonths(-UserSetting.Current.Month);
                    SnapShotBll ssb = new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr);
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

        private void btnClearExpiredCardEvent_Click(object sender, EventArgs e)
        {

        }

        private void rdb3Des_CheckedChanged(object sender, EventArgs e)
        {
            this.rdbFixedKey.Enabled = this.rdb3Des.Checked;
            if (!this.rdbFixedKey.Enabled && this.rdbFixedKey.Checked)
            {
                this.rdbSamNo.Checked = true;
            }
        }

        private void rdbIC_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlIC.Enabled = this.rdbIC.Checked;
        }

        private void rdbCPU_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlCPU.Enabled = this.rdbCPU.Checked;
        }

        private void rdbCPU_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.rdbCPU.Checked)
            {
                if (MessageBox.Show(Resources.Resource1.FrmSystemOption_CPUKeyNote, Resources.Resource1.FrmSystemOption_UseCPUComfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    this.rdbCPU.CheckedChanged -= rdbCPU_CheckedChanged_1;
                    this.rdbIC.Checked = true;
                    this.rdbCPU.CheckedChanged += rdbCPU_CheckedChanged_1;
                    return;
                }
            }
        }

        private void chkChangeCPUKey_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChangeCPUKey.Checked)
            {
                if (MessageBox.Show(Resources.Resource1.FrmSystemOption_CPUKeyNote, Resources.Resource1.FrmSystemOption_ChangeCPUKeyComfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    this.chkChangeCPUKey.CheckedChanged -= chkChangeCPUKey_CheckedChanged;
                    this.chkChangeCPUKey.Checked = false;
                    this.chkChangeCPUKey.CheckedChanged += chkChangeCPUKey_CheckedChanged;
                    return;
                }

                this.txtCPUKey.Text=string.Empty;
                this.txtCPUKey.PasswordChar = char.MinValue;
            }
            else
            {
                this.txtCPUKey.PasswordChar = '*' ;
                this.txtCPUKey.HexValue = KeySetting.Current.ParkingCPUKeyBase;
            }
            if (this.chkChangeCPUKey.Enabled)
            {
                this.txtCPUKey.Enabled = this.chkChangeCPUKey.Checked;
            }

        }

        private void rdbSamNo_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlFixedKey.Enabled = !this.rdbSamNo.Checked;
        }

        private void rdCustom_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMoneyUnit.Enabled = this.rdCustom.Checked;
        }

        private void rdSystem_CheckedChanged(object sender, EventArgs e)
        {
            this.txtUserID.Enabled = !rdSystem.Checked;
            this.txtPasswd.Enabled = !rdSystem.Checked;
        }

        private void rdUser_CheckedChanged(object sender, EventArgs e)
        {
            this.txtUserID.Enabled = rdUser.Checked;
            this.txtPasswd.Enabled = rdUser.Checked;
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = this.txtServer.Text.Trim();
            sb.InitialCatalog = this.txtDataBase.Text.Trim();
            sb.IntegratedSecurity = rdSystem.Checked;
            sb.UserID = this.txtUserID.Text.Trim();
            sb.Password = this.txtPasswd.Text.Trim();
            sb.PersistSecurityInfo = true;
            sb.ConnectTimeout = 3;

            if (string.IsNullOrEmpty(sb.DataSource))
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_InvalidServer);
                this.txtServer.Focus();
                return;
            }
            if (string.IsNullOrEmpty(sb.InitialCatalog))
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_InvalidDataBase);
                this.txtDataBase.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            bool result = false;
            using (SqlConnection sqlconn = new SqlConnection(sb.ConnectionString))
            {
                //测试图片数据库连接串的可用性
                try
                {
                    sqlconn.Open();
                    result = sqlconn.State == System.Data.ConnectionState.Open;
                }
                catch
                {
                }
            }
            this.Cursor = Cursors.Default;
            if (!result)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_ImageDBConnErro);
            }
            else
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_ImageDBConnSuccess); 
            }
        }

     


    }
}
