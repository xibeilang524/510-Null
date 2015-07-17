using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI
{
    public partial class FrmTariffSelection : Form
    {
        public FrmTariffSelection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置选择的收费费率
        /// </summary>
        public TariffBase SelectedTariff { get; set; }

        #region 私有方法
        private void ShowTariff(TariffBase info)
        {
            if (info is TariffPerTime)
            {
                ShowTariff(info as TariffPerTime);
            }
            else if (info is TariffPerDay)
            {
                ShowTariff(info as TariffPerDay);
            }
            else if (info is TariffOfTurning)
            {
                ShowTariff(info as TariffOfTurning);
            }
            else if (info is TariffOfTurningLimited)
            {
                ShowTariff(info as TariffOfTurningLimited);
            }
            else if (info is TariffOfLimitation)
            {
                ShowTariff(info as TariffOfLimitation);
            }
            else if (info is TariffOfGuanZhou)
            {
                ShowTariff(info as TariffOfGuanZhou);
            }
            else if (info is TariffOfDixiakongjian)
            {
                ShowTariff(info as TariffOfDixiakongjian);
            }
            else if (info is TariffOfThreeTimeZone)
            {
                ShowTariff(info as TariffOfThreeTimeZone);
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            if (TariffSetting.Current != null
                && TariffSetting.Current.TariffOption != null
                && !string.IsNullOrEmpty(TariffSetting.Current.TariffOption.MoneyUnit))
            {
                string moneyUnit = TariffSetting.Current.TariffOption.MoneyUnit;

                foreach (Control ctr in this.Controls)
                {
                    if (ctr is Label
                        && (ctr.Text == "元" || ctr.Text == "Yuan"))
                    {
                        ctr.Text = moneyUnit;
                    }
                }

                //label53.Text = moneyUnit;
                //label70.Text = moneyUnit;
                //label65.Text = moneyUnit;
                //label64.Text = moneyUnit;
                //label63.Text = moneyUnit;
                //label62.Text = moneyUnit;
                //label61.Text = moneyUnit;
                //label60.Text = moneyUnit;
                //label56.Text = moneyUnit;
                //label54.Text = moneyUnit;
                //label48.Text = moneyUnit;
                //label21.Text = moneyUnit;
                //label27.Text = moneyUnit;
                //label16.Text = moneyUnit;
                //label22.Text = moneyUnit;
                //label15.Text = moneyUnit;
                //label46.Text = moneyUnit;
                //label4.Text = moneyUnit;
                //label5.Text = moneyUnit;
                //label52.Text = moneyUnit;
                //label7.Text = moneyUnit;
                //label37.Text = moneyUnit;
                //label9.Text = moneyUnit;
                //label10.Text = moneyUnit;
                //label23.Text = moneyUnit;
                //label35.Text = moneyUnit;
                //label69.Text = moneyUnit;
                //label89.Text = moneyUnit;
                //label83.Text = moneyUnit;
                //label76.Text = moneyUnit;
                //label94.Text = moneyUnit;
                //label74.Text = moneyUnit;
                //label77.Text = moneyUnit;
                //label75.Text = moneyUnit;
                //label95.Text = moneyUnit;
            }

        }
        #endregion

        #region 按次收费
        private bool CheckInputOfTariffPerTime()
        {
            if (txtFreeMinutes1.IntergerValue > 255 || txtFreeMinutes1.IntergerValue < 0)
            {
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidFreeMinutes);
                txtFreeMinutes1.Focus();
                return false;
            }
            if (txtFeePerTime.DecimalValue < 0)
            {
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidMoney);
                txtFeePerTime.Focus();
                return false;
            }
            return true;
        }

        private void ShowTariff(TariffPerTime info)
        {
            this.rdTariffPerTime.Checked = true;
            this.txtFreeMinutes1.IntergerValue = info.FreeMinutes;
            this.txtFeePerTime.DecimalValue = info.FeePerTime;
        }

        private TariffPerTime GetTariffPerTimeFromInput()
        {
            return new TariffPerTime((byte)this.txtFreeMinutes1.IntergerValue, this.txtFeePerTime.DecimalValue);
        }

        private void rdTariffPerTime_CheckChanged(object sender, EventArgs e)
        {
            this.txtFreeMinutes1.Enabled = rdTariffPerTime.Checked;
            this.txtFeePerTime.Enabled = rdTariffPerTime.Checked;
        }
        #endregion

        #region 按天收费
        private bool CheckInputOfTariffPerDay()
        {
            if (txtFreeMinutes2.IntergerValue > 255 || txtFreeMinutes2.IntergerValue < 0)
            {
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidFreeMinutes);
                txtFreeMinutes2.Focus();
                return false;
            }
            if (txtFeePerDay1.DecimalValue < 0)
            {
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidMoney);
                return false;
            }
            return true;
        }

        private void ShowTariff(TariffPerDay info)
        {
            this.rdTariffPerDay.Checked = true;
            this.txtFreeMinutes2.IntergerValue = info.FreeMinutes;
            this.txtFeePerDay1.DecimalValue = info.FeePerDay;
            this.chkOverDay.Checked = info.OverDay > 0;
            this.txtOverDay.IntergerValue = info.OverDay;
            this.txtFeePerOverDay.DecimalValue = info.FeePerOverDay;
            this.chkPerDayFeeOfMax.Checked = info.FeeOfMax > 0;
            this.txtPerDayFeeOfMax.DecimalValue = info.FeeOfMax;
        }

        private TariffPerDay GetTariffPerDayFromInput()
        {
            TariffPerDay tariff = new TariffPerDay((byte)this.txtFreeMinutes2.IntergerValue, this.txtFeePerDay1.DecimalValue);
            tariff.OverDay = (short)(chkOverDay.Checked ? this.txtOverDay.IntergerValue : 0);
            tariff.FeePerOverDay = chkOverDay.Checked ? this.txtFeePerOverDay.DecimalValue : 0;
            tariff.FeeOfMax = chkPerDayFeeOfMax.Checked ? this.txtPerDayFeeOfMax.DecimalValue : 0;
            return tariff;
        }

        private void rdTariffPerDay_CheckChanged(object sender, EventArgs e)
        {
            this.txtFeePerDay1.Enabled = rdTariffPerDay.Checked;
            this.txtFreeMinutes2.Enabled = rdTariffPerDay.Checked;
            this.chkOverDay.Enabled = rdTariffPerDay.Checked;
            this.txtOverDay.Enabled = rdTariffPerDay.Checked;
            this.txtFeePerOverDay.Enabled = rdTariffPerDay.Checked;
            this.chkPerDayFeeOfMax.Enabled = rdTariffPerDay.Checked;
            this.txtPerDayFeeOfMax.Enabled = rdTariffPerDay.Checked;
        }
        #endregion

        #region 过点收费
        private bool CheckInputOfTariffOfMidNight()
        {
            return true;
        }

        private void ShowTariff(TariffOfTurning info)
        {
            this.rdTariffOfTurning.Checked = true;
            this.txtFreeMinutes3.IntergerValue = info.FreeMinutes;
            this.txtFirstFee.DecimalValue = info.FirstFee;
            this.dtTurning.Value = new DateTime(2000, 1, 1, info.Turning.Hour, info.Turning.Minute, 0);
            this.txtFeeOfMidNight.DecimalValue = info.FeeOfTurning;
            this.chkTuringFeeOfMax.Checked = info.FeeOfMax > 0;
            if (this.chkTuringFeeOfMax.Checked)
            {
                this.txtTuringFeeOfMax.DecimalValue = info.FeeOfMax;
            }
        }

        private TariffOfTurning GetTariffOfTurningFromInput()
        {
            TariffOfTurning tariff = new TariffOfTurning();
            tariff.FreeMinutes = (byte)this.txtFreeMinutes3.IntergerValue;
            tariff.FirstFee = this.txtFirstFee.DecimalValue;
            tariff.Turning = new TimeEntity(dtTurning.Value.Hour, dtTurning.Value.Minute);
            tariff.FeeOfTurning = this.txtFeeOfMidNight.DecimalValue;
            tariff.FeeOfMax = this.chkTuringFeeOfMax.Checked ? this.txtTuringFeeOfMax.DecimalValue : 0;
            return tariff;
        }

        private void rdTariffOfTurning_CheckChanged(object sender, EventArgs e)
        {
            this.txtFreeMinutes3.Enabled = this.rdTariffOfTurning.Checked;
            this.txtFirstFee.Enabled = this.rdTariffOfTurning.Checked;
            this.txtFeeOfMidNight.Enabled = this.rdTariffOfTurning.Checked;
            this.dtTurning.Enabled = this.rdTariffOfTurning.Checked;
            this.chkTuringFeeOfMax.Enabled = this.rdTariffOfTurning.Checked;
            this.txtTuringFeeOfMax.Enabled = this.rdTariffOfTurning.Checked;
        }
        #endregion

        #region 限时过点收费
        private bool CheckInputOfTariffOfTurningLimited()
        {
            return true;
        }

        private void ShowTariff(TariffOfTurningLimited info)
        {
            this.rdTariffOfTurningLimited.Checked = true;
            this.tpd_txtFreeMinutes.IntergerValue = info.FreeMinutes;
            this.tpd_txtFirstFee.DecimalValue = info.FirstFee;
            this.tpd_dtTurning.Value = new DateTime(2000, 1, 1, info.Turning.Hour, info.Turning.Minute, 0);
            this.tpd_FeeOfTurning.DecimalValue = info.FeeOfTurning;
            this.chkTuringLimitedOfMax.Checked = info.FeeOfMax > 0;
            if (this.chkTuringLimitedOfMax.Checked)
            {
                this.txtTuringLimitedOfMax.DecimalValue = info.FeeOfMax;
            }
        }

        private TariffOfTurningLimited GetTariffOfTurningLimitedFromInput()
        {
            TariffOfTurningLimited tariff = new TariffOfTurningLimited();
            tariff.FreeMinutes = (byte)this.tpd_txtFreeMinutes.IntergerValue;
            tariff.FirstFee = this.tpd_txtFirstFee.DecimalValue;
            tariff.Turning = new TimeEntity(tpd_dtTurning.Value.Hour, tpd_dtTurning.Value.Minute);
            tariff.FeeOfTurning = this.tpd_FeeOfTurning.DecimalValue;
            tariff.FeeOfMax = this.chkTuringLimitedOfMax.Checked ? this.txtTuringLimitedOfMax.DecimalValue : 0;
            return tariff;
        }

        private void rdTariffOfTurningLimited_CheckChanged(object sender, EventArgs e)
        {
            this.tpd_txtFreeMinutes.Enabled = this.rdTariffOfTurningLimited.Checked;
            this.tpd_txtFirstFee.Enabled = this.rdTariffOfTurningLimited.Checked;
            this.tpd_FeeOfTurning.Enabled = this.rdTariffOfTurningLimited.Checked;
            this.tpd_dtTurning.Enabled = this.rdTariffOfTurningLimited.Checked;
            this.chkTuringLimitedOfMax.Enabled = this.rdTariffOfTurningLimited.Checked;
            this.txtTuringLimitedOfMax.Enabled = this.rdTariffOfTurningLimited.Checked;
        }
        #endregion

        #region 有限额收费
        private bool CheckInputOfTariffOfLimitation()
        {
            return true;
        }

        private void ShowTariff(TariffOfLimitation info)
        {
            this.rdTariffOfLimitation.Checked = true;
            this.txtFreeMinutes4.IntergerValue = info.FreeMinutes;
            if (info.FirstCharge != null)
            {
                this.chkFirstCharge.Checked = true;
                this.txtFirstMinutes.IntergerValue = (short)info.FirstCharge.Minutes;
                this.txtFirstFee1.DecimalValue = info.FirstCharge.Fee;
            }
            else
            {
                this.chkFirstCharge.Checked = false;
                this.txtFirstMinutes.IntergerValue = 0;
                this.txtFirstFee1.DecimalValue = 0;
            }
            this.ck_Is12Hour.Checked = info.FeeOf12Hour > 0;
            if (this.ck_Is12Hour.Checked)
            {
                this.txtFee12Hour.DecimalValue = info.FeeOf12Hour;
            }
            this.ck_Is24Hour.Checked = info.FeeOf24Hour > 0;
            if (this.ck_Is24Hour.Checked)
            {
                this.txtFee24Hour.DecimalValue = info.FeeOf24Hour;
            }
            this.chkLimitFeeOfMax.Checked = info.FeeOfMax > 0;
            if (this.chkLimitFeeOfMax.Checked)
            {
                this.txtLimitFeeOfMax.DecimalValue = info.FeeOfMax;
            }
            this.txtRegularMinutes.IntergerValue = (short)info.RegularCharge.Minutes;
            this.txtRegularFee.DecimalValue = info.RegularCharge.Fee;
        }

        private TariffOfLimitation GetTariffOfLimitationFromInput()
        {
            TariffOfLimitation tariff = new TariffOfLimitation();
            tariff.FreeMinutes = (byte)this.txtFreeMinutes4.IntergerValue;
            if (this.chkFirstCharge.Checked)
            {
                tariff.FirstCharge = new ChargeUnit((short)this.txtFirstMinutes.IntergerValue, this.txtFirstFee1.DecimalValue);
            }
            tariff.RegularCharge = new ChargeUnit((short)this.txtRegularMinutes.IntergerValue, this.txtRegularFee.DecimalValue);
            tariff.FeeOf12Hour = this.ck_Is12Hour.Checked ? this.txtFee12Hour.DecimalValue : 0;
            tariff.FeeOf24Hour = this.ck_Is24Hour.Checked ? this.txtFee24Hour.DecimalValue : 0;
            tariff.FeeOfMax = this.chkLimitFeeOfMax.Checked ? this.txtLimitFeeOfMax.DecimalValue : 0;
            return tariff;
        }

        private void rdTariffOfLimitation_CheckChanged(object sender, EventArgs e)
        {
            this.txtFreeMinutes4.Enabled = rdTariffOfLimitation.Checked;
            this.chkFirstCharge.Enabled = rdTariffOfLimitation.Checked;
            this.txtFirstMinutes.Enabled = rdTariffOfLimitation.Checked;
            this.txtFirstFee1.Enabled = rdTariffOfLimitation.Checked;
            this.txtRegularMinutes.Enabled = rdTariffOfLimitation.Checked;
            this.txtRegularFee.Enabled = rdTariffOfLimitation.Checked;
            this.ck_Is12Hour.Enabled = rdTariffOfLimitation.Checked;
            this.ck_Is24Hour.Enabled = rdTariffOfLimitation.Checked;
            this.txtFee12Hour.Enabled = rdTariffOfLimitation.Checked;
            this.txtFee24Hour.Enabled = rdTariffOfLimitation.Checked;
            this.chkLimitFeeOfMax.Enabled = rdTariffOfLimitation.Checked;
            this.txtLimitFeeOfMax.Enabled = rdTariffOfLimitation.Checked;
        }
        #endregion

        #region 广州收费标准
        private bool CheckInputOfTariffOfGuanZhou()
        {
            if (txtFreeMinutes5.IntergerValue > 255 || txtFreeMinutes5.IntergerValue < 0)
            {
                txtFreeMinutes5.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidFreeMinutes);
                return false;
            }
            if (this.txtFeePerDay4.DecimalValue < 0)
            {
                txtFeePerDay4.Focus();
                MessageBox.Show(Resource1.FrmTariffSelectlion_InvalidDayFee);
                return false;
            }
            if (this.txtDayMinutes.IntergerValue <= 0)
            {
                this.txtDayMinutes.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidDayMinutes);
                return false;
            }
            if (this.txtDayFee.DecimalValue < 0)
            {
                this.txtDayFee.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidDayFee);
                return false;
            }
            if (chkDayLimite.Checked && this.txtDayLimitFee.DecimalValue < 0)
            {
                this.txtDayLimitFee.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidDayLimitFee);
                return false;
            }
            TimeEntity dayBegin = new TimeEntity(dtDayBegin.Value.Hour, dtDayBegin.Value.Minute);
            TimeEntity dayEnd = new TimeEntity(dtDayEnd.Value.Hour, dtDayEnd.Value.Minute);
            TimeEntity nightBegin = new TimeEntity(dtNightBegin.Value.Hour, dtNightBegin.Value.Minute);
            TimeEntity nightEnd = new TimeEntity(dtNightEnd.Value.Hour, dtNightEnd.Value.Minute);
            if (!(dayBegin.TotalMinutes == nightEnd.TotalMinutes && dayEnd.TotalMinutes == nightBegin.TotalMinutes))
            {
                this.dtNightBegin.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidTimezone);
                return false;
            }
            if (this.txtNightMinutes.IntergerValue <= 0)
            {
                this.txtNightMinutes.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidNightMinutes);
                return false;
            }
            if (this.txtNightFee.DecimalValue < 0)
            {
                this.txtNightFee.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidNightFee);
                return false;
            }
            if (chkNightLimite.Checked && this.txtNightLimitFee.DecimalValue < 0)
            {
                this.txtNightLimitFee.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidNightLimitation);
                return false;
            }
            return true;
        }
        private void ShowTariff(TariffOfGuanZhou info)
        {
            this.rdTariffOfGuanZhou.Checked = true;
            this.txtFreeMinutes5.IntergerValue = info.FreeMinutes;
            this.chkGuangZhouFeeOf24.Checked = info.FeeOf24Hour > 0;
            if (this.chkGuangZhouFeeOf24.Checked)
            {
                this.txtFeePerDay4.DecimalValue = info.FeeOf24Hour;
            }
            this.chkGuangZhouFeeOfMax.Checked = info.FeeOfMax > 0;
            if (info.FeeOfMax > 0)
            {
                this.txtGuangZhouFeeOfMax.DecimalValue = info.FeeOfMax;
            }
            if (info.DayTimezone != null)
            {
                this.dtDayBegin.Value = new DateTime(2000, 1, 1, info.DayTimezone.Beginning.Hour, info.DayTimezone.Beginning.Minute, 0);
                this.dtDayEnd.Value = new DateTime(2000, 1, 1, info.DayTimezone.Ending.Hour, info.DayTimezone.Ending.Minute, 0);
                this.txtDayMinutes.IntergerValue = info.DayTimezone.RegularCharge.Minutes;
                this.txtDayFee.DecimalValue = info.DayTimezone.RegularCharge.Fee;
                if (info.DayTimezone.LimiteFee != null && info.DayTimezone.LimiteFee.Value > 0)
                {
                    this.chkDayLimite.Checked = true;
                    this.txtDayLimitFee.DecimalValue = info.DayTimezone.LimiteFee.Value;
                }
                else
                {
                    this.chkDayLimite.Checked = false;
                    //this.txtDayLimitFee.DecimalValue = 0;
                }
            }
            if (info.NightTimezone != null)
            {
                this.dtNightBegin.Value = new DateTime(2000, 1, 1, info.NightTimezone.Beginning.Hour, info.NightTimezone.Beginning.Minute, 0);
                this.dtNightEnd.Value = new DateTime(2000, 1, 1, info.NightTimezone.Ending.Hour, info.NightTimezone.Ending.Minute, 0);
                this.txtNightMinutes.IntergerValue = info.NightTimezone.RegularCharge.Minutes;
                this.txtNightFee.DecimalValue = info.NightTimezone.RegularCharge.Fee;
                if (info.NightTimezone.LimiteFee != null && info.NightTimezone.LimiteFee.Value > 0)
                {
                    this.chkNightLimite.Checked = true;
                    this.txtNightLimitFee.DecimalValue = info.NightTimezone.LimiteFee.Value;
                }
                else
                {
                    this.chkNightLimite.Checked = false;
                    //this.txtNightLimitFee.DecimalValue = 0;
                }
            }
        }

        private TariffOfGuanZhou GetTariffOfGuanZhouFromInput()
        {
            TariffOfGuanZhou tariff = new TariffOfGuanZhou();
            tariff.FreeMinutes = (byte)this.txtFreeMinutes5.IntergerValue;
            tariff.FeeOf24Hour = this.chkGuangZhouFeeOf24.Checked ? this.txtFeePerDay4.DecimalValue : 0;
            tariff.FeeOfMax = this.chkGuangZhouFeeOfMax.Checked ? this.txtGuangZhouFeeOfMax.DecimalValue : 0;

            tariff.DayTimezone = new TariffTimeZone();
            tariff.DayTimezone.Beginning = new TimeEntity(this.dtDayBegin.Value);
            tariff.DayTimezone.Ending = new TimeEntity(this.dtDayEnd.Value);
            tariff.DayTimezone.RegularCharge = new ChargeUnit();
            tariff.DayTimezone.RegularCharge.Minutes = (short)this.txtDayMinutes.IntergerValue;
            tariff.DayTimezone.RegularCharge.Fee = this.txtDayFee.DecimalValue;
            if (chkDayLimite.Checked && this.txtDayLimitFee.DecimalValue > 0)
            {
                tariff.DayTimezone.LimiteFee = this.txtDayLimitFee.DecimalValue;
            }

            tariff.NightTimezone = new TariffTimeZone();
            tariff.NightTimezone.Beginning = new TimeEntity(this.dtNightBegin.Value);
            tariff.NightTimezone.Ending = new TimeEntity(this.dtNightEnd.Value);
            tariff.NightTimezone.RegularCharge = new ChargeUnit();
            tariff.NightTimezone.RegularCharge.Minutes = (short)this.txtNightMinutes.IntergerValue;
            tariff.NightTimezone.RegularCharge.Fee = this.txtNightFee.DecimalValue;
            if (chkNightLimite.Checked && this.txtNightLimitFee.DecimalValue > 0)
            {
                tariff.NightTimezone.LimiteFee = this.txtNightLimitFee.DecimalValue;
            }
            return tariff;
        }

        private void rdTariffOfGuanZhou_CheckChanged(object sender, EventArgs e)
        {
            this.txtFreeMinutes5.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.chkGuangZhouFeeOf24.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtFeePerDay4.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.dtDayBegin.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.dtDayEnd.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtDayMinutes.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtDayFee.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.dtNightBegin.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.dtNightEnd.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtNightMinutes.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtNightFee.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.chkNightLimite.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtNightLimitFee.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtDayLimitFee.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.chkDayLimite.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.chkGuangZhouFeeOfMax.Enabled = this.rdTariffOfGuanZhou.Checked;
            this.txtGuangZhouFeeOfMax.Enabled = this.rdTariffOfGuanZhou.Checked;
        }

        #endregion

        #region 地下空间收费标准
        private bool CheckInputOfTariffOfTimezoneLimitation()
        {
            return true;
        }

        private void ShowTariff(TariffOfDixiakongjian info)
        {
            rdTimezoneLimitation.Checked = true;
            tl_txtFreeMinutes.IntergerValue = info.FreeMinutes;
            tl_chkFeeOf24.Checked = info.FeeOf24Hour > 0;
            if (tl_chkFeeOf24.Checked)
            {
                tl_txtPerDay.DecimalValue = info.FeeOf24Hour;
            }
            tl_chkFeeOfMax.Checked = info.FeeOfMax > 0;
            if (tl_chkFeeOfMax.Checked)
            {
                tl_txtFeeOfMax.DecimalValue = info.FeeOfMax;
            }
            tl_txtFirstMinutes.IntergerValue = info.FirstMinutes;
            tl_txtFirstFeeMinutes.IntergerValue = info.FirstFee.Minutes;
            tl_txtFirstFee.DecimalValue = info.FirstFee.Fee;
            tl_txtRegularMinutes.IntergerValue = info.RegularFee.Minutes;
            tl_txtRegularFee.DecimalValue = info.RegularFee.Fee;
            tl_Timezone1.Value = new DateTime(2000, 1, 1, info.LimitationTimezone.Beginning.Hour, info.LimitationTimezone.Beginning.Minute, 0);
            tl_Timezone2.Value = new DateTime(2000, 1, 1, info.LimitationTimezone.Ending.Hour, info.LimitationTimezone.Ending.Minute, 0);
            tl_txtLimitationMinutes.IntergerValue = info.LimitationRegularFee != null ? info.LimitationRegularFee.Minutes : 0;
            tl_txtLimitationFee.DecimalValue = info.LimitationRegularFee != null ? info.LimitationRegularFee.Fee : 0;
            tl_txtLimitation.DecimalValue = info.Limitation;
        }

        private TariffOfDixiakongjian GetTariffOfTimezoneLimitationFromInput()
        {
            TariffOfDixiakongjian tariff = new TariffOfDixiakongjian();
            tariff.FreeMinutes = (byte)tl_txtFreeMinutes.IntergerValue;
            tariff.FeeOf24Hour = tl_chkFeeOf24.Checked ? tl_txtPerDay.DecimalValue : 0;
            tariff.FeeOfMax = tl_chkFeeOfMax.Checked ? tl_txtFeeOfMax.DecimalValue : 0;
            tariff.FirstMinutes = tl_txtFirstMinutes.IntergerValue;
            tariff.FirstFee = new ChargeUnit();
            tariff.FirstFee.Minutes = (short)tl_txtFirstFeeMinutes.IntergerValue;
            tariff.FirstFee.Fee = tl_txtFirstFee.DecimalValue;
            tariff.RegularFee = new ChargeUnit();
            tariff.RegularFee.Minutes = (short)tl_txtRegularMinutes.IntergerValue;
            tariff.RegularFee.Fee = tl_txtRegularFee.DecimalValue;
            tariff.LimitationTimezone = new BusinessModel.Model.TimeZone();
            tariff.LimitationTimezone.Beginning = new TimeEntity(tl_Timezone1.Value);
            tariff.LimitationTimezone.Ending = new TimeEntity(tl_Timezone2.Value);
            tariff.LimitationRegularFee = new ChargeUnit();
            tariff.LimitationRegularFee.Minutes = (short)tl_txtLimitationMinutes.IntergerValue;
            tariff.LimitationRegularFee.Fee = tl_txtLimitationFee.DecimalValue;
            tariff.Limitation = tl_txtLimitation.DecimalValue;
            return tariff;
        }

        private void rdTimezoneLimitation_CheckedChanged(object sender, EventArgs e)
        {
            tl_txtFreeMinutes.Enabled = rdTimezoneLimitation.Checked;
            tl_txtPerDay.Enabled = rdTimezoneLimitation.Checked;
            tl_txtFirstMinutes.Enabled = rdTimezoneLimitation.Checked;
            tl_txtFirstFeeMinutes.Enabled = rdTimezoneLimitation.Checked;
            tl_txtFirstFee.Enabled = rdTimezoneLimitation.Checked;
            tl_txtRegularMinutes.Enabled = rdTimezoneLimitation.Checked;
            tl_txtRegularFee.Enabled = rdTimezoneLimitation.Checked;
            tl_Timezone1.Enabled = rdTimezoneLimitation.Checked;
            tl_Timezone2.Enabled = rdTimezoneLimitation.Checked;
            tl_txtLimitation.Enabled = rdTimezoneLimitation.Checked;
            tl_chkFeeOfMax.Enabled = rdTimezoneLimitation.Checked;
            tl_txtFeeOfMax.Enabled = rdTimezoneLimitation.Checked;
            tl_chkFeeOf24.Enabled = rdTimezoneLimitation.Checked;
            tl_txtLimitationMinutes.Enabled = rdTimezoneLimitation.Checked;
            tl_txtLimitationFee.Enabled = rdTimezoneLimitation.Checked;
        }
        #endregion

        #region 三个时段收费标准
        private bool CheckInputOfTariffOfThreeTimeZone()
        {
            TimeEntity time1Begin = new TimeEntity(tThree_dtTime1Begin.Value.Hour, tThree_dtTime1Begin.Value.Minute);
            TimeEntity time1End = new TimeEntity(tThree_dtTime1End.Value.Hour, tThree_dtTime1End.Value.Minute);

            TimeEntity time2Begin = new TimeEntity(tThree_dtTime2Begin.Value.Hour, tThree_dtTime2Begin.Value.Minute);
            TimeEntity time2End = new TimeEntity(tThree_dtTime2End.Value.Hour, tThree_dtTime2End.Value.Minute);

            TimeEntity time3Begin = new TimeEntity(tThree_dtTime3Begin.Value.Hour, tThree_dtTime3Begin.Value.Minute);
            TimeEntity time3End = new TimeEntity(tThree_dtTime3End.Value.Hour, tThree_dtTime3End.Value.Minute);

            if (!(time1Begin.TotalMinutes == time3End.TotalMinutes
                && time1End.TotalMinutes == time2Begin.TotalMinutes
                && time2End.TotalMinutes == time3Begin.TotalMinutes))
            {
                this.tThree_dtTime1Begin.Focus();
                MessageBox.Show(Resource1.FrmTariffSelection_InvalidThreeTimezone);
                return false;
            }
            return true;
        }


        private void ShowTariff(TariffOfThreeTimeZone info)
        {
            this.rdTariffOfThreeTimeZone.Checked = true;
            this.tThree_txtFreeMinutes.IntergerValue = info.FreeMinutes;
            this.tThree_chkFeeOf24.Checked = info.FeeOf24Hour > 0;
            if (this.tThree_chkFeeOf24.Checked)
            {
                this.tThree_txtFeeOf24.DecimalValue = info.FeeOf24Hour;
            }
            this.tThree_chkFeeOfMax.Checked = info.FeeOfMax > 0;
            if (this.tThree_chkFeeOfMax.Checked)
            {
                this.tThree_txtFeeOfMax.DecimalValue = info.FeeOfMax;
            }

            //时段1
            this.tThree_dtTime1Begin.Value = new DateTime(2000, 1, 1, info.Timezone1.Beginning.Hour, info.Timezone1.Beginning.Minute, 0);
            this.tThree_dtTime1End.Value = new DateTime(2000, 1, 1, info.Timezone1.Ending.Hour, info.Timezone1.Ending.Minute, 0);
            this.tThree_txtTime1Minutes.IntergerValue = info.Timezone1.RegularCharge.Minutes;
            this.tThree_txtTime1Fee.DecimalValue = info.Timezone1.RegularCharge.Fee;
            if (info.Timezone1.LimiteFee.HasValue && info.Timezone1.LimiteFee.Value > 0)
            {
                this.tThree_chkTime1LimitFee.Checked = true;
                this.tThree_txtTime1LimitFee.DecimalValue = info.Timezone1.LimiteFee.Value;
            }
            else
            {
                this.tThree_chkTime1LimitFee.Checked = false;
            }

            //时段2
            this.tThree_dtTime2Begin.Value = new DateTime(2000, 1, 1, info.Timezone2.Beginning.Hour, info.Timezone2.Beginning.Minute, 0);
            this.tThree_dtTime2End.Value = new DateTime(2000, 1, 1, info.Timezone2.Ending.Hour, info.Timezone2.Ending.Minute, 0);
            this.tThree_txtTime2Minutes.IntergerValue = info.Timezone2.RegularCharge.Minutes;
            this.tThree_txtTime2Fee.DecimalValue = info.Timezone2.RegularCharge.Fee;
            if (info.Timezone2.LimiteFee.HasValue && info.Timezone2.LimiteFee.Value > 0)
            {
                this.tThree_chkTime2LimitFee.Checked = true;
                this.tThree_txtTime2LimitFee.DecimalValue = info.Timezone2.LimiteFee.Value;
            }
            else
            {
                this.tThree_chkTime2LimitFee.Checked = false;
            }

            //时段3
            this.tThree_dtTime3Begin.Value = new DateTime(2000, 1, 1, info.Timezone3.Beginning.Hour, info.Timezone3.Beginning.Minute, 0);
            this.tThree_dtTime3End.Value = new DateTime(2000, 1, 1, info.Timezone3.Ending.Hour, info.Timezone3.Ending.Minute, 0);
            this.tThree_txtTime3Minutes.IntergerValue = info.Timezone3.RegularCharge.Minutes;
            this.tThree_txtTime3Fee.DecimalValue = info.Timezone3.RegularCharge.Fee;
            if (info.Timezone3.LimiteFee.HasValue && info.Timezone3.LimiteFee.Value > 0)
            {
                this.tThree_chkTime3LimitFee.Checked = true;
                this.tThree_txtTime3LimitFee.DecimalValue = info.Timezone3.LimiteFee.Value;
            }
            else
            {
                this.tThree_chkTime3LimitFee.Checked = false;
            }
        }

        private TariffOfThreeTimeZone GetTariffOfThreeTimeZoneFromInput()
        {
            TariffOfThreeTimeZone tariff = new TariffOfThreeTimeZone();
            tariff.FreeMinutes = (byte)this.tThree_txtFreeMinutes.IntergerValue;
            tariff.FeeOf24Hour = this.tThree_chkFeeOf24.Checked ? this.tThree_txtFeeOf24.DecimalValue : 0;
            tariff.FeeOfMax = this.tThree_chkFeeOfMax.Checked ? this.tThree_txtFeeOfMax.DecimalValue : 0;
            
            //时段1
            tariff.Timezone1 = new TariffTimeZone();
            tariff.Timezone1.Beginning = new TimeEntity(this.tThree_dtTime1Begin.Value);
            tariff.Timezone1.Ending = new TimeEntity(this.tThree_dtTime1End.Value);
            tariff.Timezone1.RegularCharge = new ChargeUnit();
            tariff.Timezone1.RegularCharge.Minutes = (short)this.tThree_txtTime1Minutes.IntergerValue;
            tariff.Timezone1.RegularCharge.Fee = this.tThree_txtTime1Fee.DecimalValue;
            if (this.tThree_chkTime1LimitFee.Checked && this.tThree_txtTime1LimitFee.DecimalValue > 0)
            {
                tariff.Timezone1.LimiteFee = this.tThree_txtTime1LimitFee.DecimalValue;
            }

            //时段2
            tariff.Timezone2 = new TariffTimeZone();
            tariff.Timezone2.Beginning = new TimeEntity(this.tThree_dtTime2Begin.Value);
            tariff.Timezone2.Ending = new TimeEntity(this.tThree_dtTime2End.Value);
            tariff.Timezone2.RegularCharge = new ChargeUnit();
            tariff.Timezone2.RegularCharge.Minutes = (short)this.tThree_txtTime2Minutes.IntergerValue;
            tariff.Timezone2.RegularCharge.Fee = this.tThree_txtTime2Fee.DecimalValue;
            if (this.tThree_chkTime2LimitFee.Checked && this.tThree_txtTime2LimitFee.DecimalValue > 0)
            {
                tariff.Timezone2.LimiteFee = this.tThree_txtTime2LimitFee.DecimalValue;
            }

            //时段3
            tariff.Timezone3 = new TariffTimeZone();
            tariff.Timezone3.Beginning = new TimeEntity(this.tThree_dtTime3Begin.Value);
            tariff.Timezone3.Ending = new TimeEntity(this.tThree_dtTime3End.Value);
            tariff.Timezone3.RegularCharge = new ChargeUnit();
            tariff.Timezone3.RegularCharge.Minutes = (short)this.tThree_txtTime3Minutes.IntergerValue;
            tariff.Timezone3.RegularCharge.Fee = this.tThree_txtTime3Fee.DecimalValue;
            if (this.tThree_chkTime3LimitFee.Checked && this.tThree_txtTime3LimitFee.DecimalValue > 0)
            {
                tariff.Timezone3.LimiteFee = this.tThree_txtTime3LimitFee.DecimalValue;
            }
            return tariff;
        }

        private void rdTariffOfThreeTimeZone_CheckedChanged(object sender, EventArgs e)
        {
            tThree_txtFreeMinutes.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_chkFeeOf24.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtFeeOf24.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_chkFeeOfMax.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtFeeOfMax.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_dtTime1Begin.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_dtTime1End.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime1Minutes.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime1Fee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_chkTime1LimitFee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime1LimitFee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_dtTime2Begin.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_dtTime2End.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime2Minutes.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime2Fee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_chkTime2LimitFee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime2LimitFee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_dtTime3Begin.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_dtTime3End.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime3Minutes.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime3Fee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_chkTime3LimitFee.Enabled = rdTariffOfThreeTimeZone.Checked;
            tThree_txtTime3LimitFee.Enabled = rdTariffOfThreeTimeZone.Checked;
        }
        #endregion

        #region 事件处理
        private void btnOk_Click(object sender, EventArgs e)
        {
            TariffBase tf = null;
            if (this.rdTariffPerTime.Checked && CheckInputOfTariffPerTime())
            {
                tf = GetTariffPerTimeFromInput();
            }
            else if (this.rdTariffPerDay.Checked && CheckInputOfTariffPerDay())
            {
                tf = GetTariffPerDayFromInput();
            }
            else if (this.rdTariffOfTurning.Checked && CheckInputOfTariffOfMidNight())
            {
                tf = GetTariffOfTurningFromInput();
            }
            else if (this.rdTariffOfTurningLimited.Checked && CheckInputOfTariffOfTurningLimited())
            {
                tf = GetTariffOfTurningLimitedFromInput();
            }
            else if (this.rdTariffOfLimitation.Checked && CheckInputOfTariffOfLimitation())
            {
                tf = GetTariffOfLimitationFromInput();
            }
            else if (this.rdTariffOfGuanZhou.Checked && CheckInputOfTariffOfGuanZhou())
            {
                tf = GetTariffOfGuanZhouFromInput();
            }
            else if (this.rdTimezoneLimitation.Checked && CheckInputOfTariffOfTimezoneLimitation())
            {
                tf = GetTariffOfTimezoneLimitationFromInput();
            }
            else if (this.rdTariffOfThreeTimeZone.Checked && CheckInputOfTariffOfThreeTimeZone())
            {
                tf = GetTariffOfThreeTimeZoneFromInput();
            }
            if (tf != null)
            {
                this.SelectedTariff = tf;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void FrmTariffSelection_Load(object sender, EventArgs e)
        {
            this.btnOk.Enabled = OperatorInfo.CurrentOperator.Permit(Ralid.Park.BusinessModel.Enum.Permission.EditSysSetting);
                        
            InitControl();
            if (SelectedTariff != null)
            {
                ShowTariff(SelectedTariff);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
