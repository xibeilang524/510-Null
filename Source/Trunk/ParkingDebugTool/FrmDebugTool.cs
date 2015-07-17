using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.Hardware;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.GeneralLibrary.CardReader;

namespace ParkingDebugTool
{
    public partial class FrmParkingDebugTool : Form
    {
        public FrmParkingDebugTool()
        {
            InitializeComponent();
        }

        #region 私有变量
        private CardInfo _Card;
        private int _CurrentSection = 0;
        #endregion

        #region 卡片信息

        #region 私有方法
        private void InitCardType()
        {
            comCardType.Items.Add(string.Empty);
            comCardType.Items.AddRange(
                new Ralid.Park.BusinessModel.Enum.CardType[]{
                Ralid.Park.BusinessModel.Enum.CardType.VipCard,
                Ralid.Park.BusinessModel.Enum.CardType.OwnerCard ,
                Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard ,
                Ralid.Park.BusinessModel.Enum.CardType.PrePayCard ,
                Ralid.Park.BusinessModel.Enum.CardType.TempCard ,
                Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1,
                Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2,
                Ralid.Park.BusinessModel.Enum.CardType.OperatorCard
            });
            comCardType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitCarType()
        {
            comChargeType.Items.Add(string.Empty);
            comChargeType.Items.AddRange(
                new string[]{
                    CarTypeDescription.CarType_A,
                    CarTypeDescription.CarType_B, 
                    CarTypeDescription.CarType_C, 
                    CarTypeDescription.CarType_D
            });
            comChargeType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ShowSetting()
        {
            this.chkWeigand34.Checked = UserSetting.Current.WegenType == WegenType.Wengen34;
            this.txtKey.HexValue = GlobalVariables.ParkingKey;
            this.txtSection.IntergerValue = _CurrentSection;
            this.ucSection1.Section = _CurrentSection;
        }

        private void SaveSetting()
        {
            UserSetting.Current.WegenType = this.chkWeigand34.Checked ? WegenType.Wengen34 : WegenType.Wengen26;
            KeySetting.Current.ParkingKey = this.txtKey.HexValue;
            _CurrentSection = this.txtSection.IntergerValue;
            KeySetting.Current.ParkingSection = (byte)_CurrentSection;
        }

        private bool CheckSetting()
        {
            if (this.txtKey.HexValue.Length != 6)
            {
                MessageBox.Show("请输入密钥！");
                this.txtKey.Focus();
                return false;
            }
            return true;
        }

        private bool CheckInput()
        {
            string text;
            //UInt32 val;

            //text = this.txtCardID.Text.Trim();
            if (this.txtCardVersion.IntergerValue <=0)
            {
                MessageBox.Show("请输入卡格式版本！");
                return false;
            }
            if (this.comCardType.SelectedIndex <= 0)
            {
                MessageBox.Show("请选择一种卡片类型！");
                return false;
            }
            if (this.comChargeType.SelectedIndex <= 0)
            {
                MessageBox.Show("请选择一种车型！");
                return false;
            }
            if (this.txtIndexNumber.IntergerValue <= 0)
            {
                MessageBox.Show("请输入自增序号！");
                return false;
            }
            DateTime dateTime = new DateTime(2011, 1, 1);
            if (dtActivationDate.Value < dateTime)
            {
                MessageBox.Show("生效日期不能早于2011年1月1日！");
                return false;
            }
            if (dtValidDate.Value < dateTime)
            {
                MessageBox.Show("有效日期不能早于2011年1月1日！");
                return false;
            }

            //车牌号码的字节组最大只能9个字节,主要是考虑写卡模式时，卡片只能保持9个字节的数据
            text = this.txtCarPlate.Text.Trim();
            byte[] tempbytes = Encoding.GetEncoding("gb2312").GetBytes(text);
            if (tempbytes.Length > 9)
            {
                MessageBox.Show("车牌号码过长，请重新输入！");
                txtCarPlate.Focus();
                return false;
            }

            if (dtEnterTime.Value < dateTime)
            {
                MessageBox.Show("入场时间不能早于2011年1月1日！");
                return false;
            }
            if (this.chkPaidTime.Checked && dtPaidTime.Value < dateTime)
            {
                MessageBox.Show("缴费时间不能早于2011年1月1日！");
                return false;
            }
            if (this.chkFreeDateTime.Checked && dtFreeDateTime.Value < dateTime)
            {
                MessageBox.Show("免费期限不能早于2011年1月1日！");
                return false;
            }

            return true;
        }

        private void ClearInput()
        {
            this.txtCardID.Text = string.Empty;
            this.txtCardVersion.IntergerValue = 0;
            this.comCardType.SelectedIndex = 0;
            this.comChargeType.SelectedIndex = 0; ;
            this.txtAccessLevel.IntergerValue = 0;
            this.txtCarPlate.Text = string.Empty;
            this.txtBalance.DecimalValue = 0;
            this.dtActivationDate.Value = dtActivationDate.MinDate;
            this.dtValidDate.Value = dtValidDate.MinDate;
            this.chkRepeatIn.Checked = false;
            this.chkRepeatOut.Checked = false;
            this.chkHoliday.Checked = false;
            this.chkWithCount.Checked = false;
            this.chkEnableWhenExpired.Checked = false;
            this.chkCanEnterWhenFull.Checked = false;
            this.chkIn1.Checked = false;
            this.chkIn2.Checked = false;
            this.chkPaid1.Checked = false;
            this.chkIn2Mark.Checked = false;
            this.dtEnterTime.Value = dtEnterTime.MinDate;
            this.dtPaidTime.Value = dtPaidTime.MinDate;
            this.txtFee.DecimalValue = 0;
            this.txtPaidFee.DecimalValue = 0;
        }

        private void ShowCard(CardInfo card)
        {
            this.txtCardID.Text = card.CardID;
            this.txtCardVersion.IntergerValue = card.CardVersion;
            this.comCardType.SelectedCardType = card.CardType;
            this.comChargeType.SelectedCarType = card.CarType;
            this.txtAccessLevel.IntergerValue = card.AccessID;
            this.txtCarPlate.Text = card.CarPlate;
            this.txtBalance.DecimalValue = card.Balance;
            this.txtIndexNumber.IntergerValue = card.IndexNumber;
            this.dtActivationDate.Value = card.ActivationDate;
            this.dtValidDate.Value = card.ValidDate;
            this.chkOnlineHandleWhenOfflineMode.Checked = card.OnlineHandleWhenOfflineMode;
            this.chkRepeatIn.Checked = card.CanRepeatIn;
            this.chkRepeatOut.Checked = card.CanRepeatOut;
            this.chkHoliday.Checked = card.HolidayEnabled;
            this.chkWithCount.Checked = card.WithCount;
            this.chkEnableWhenExpired.Checked = card.EnableWhenExpired;
            this.chkCanEnterWhenFull.Checked = card.CanEnterWhenFull;
            this.chkIn1.Checked = card.IsInPark;
            this.chkIn2.Checked = card.IsInNestedPark;
            this.chkPaid1.Checked = card.IsPaid;
            this.chkIn2Mark.Checked = card.IsMarkNestedPark;
            this.chkEnableHotelApp.Checked = card.EnableHotelApp;
            this.chkNotCheckOut.Checked = !card.HotelCheckOut;
            this.dtEnterTime.Value = card.LastDateTime;
            this.chkPaidTime.Checked = card.PaidDateTime.HasValue;
            this.dtPaidTime.Value = card.PaidDateTime.HasValue ? card.PaidDateTime.Value : dtPaidTime.MinDate;
            this.chkFreeDateTime.Checked = card.FreeDateTime.HasValue;
            this.dtFreeDateTime.Value = card.FreeDateTime.HasValue ? card.FreeDateTime.Value : dtFreeDateTime.MinDate;
            this.txtFee.DecimalValue = card.ParkFee;
            this.txtPaidFee.DecimalValue = card.TotalPaidFee;
        }

        private CardInfo GetCardFromInput()
        {
            if (_Card == null) _Card = new CardInfo(); 
            _Card.CardID = this.txtCardID.Text;
            _Card.CardVersion = (byte)this.txtCardVersion.IntergerValue;
            _Card.CardType = this.comCardType.SelectedCardType;
            _Card.CarType = this.comChargeType.SelectedCarType;
            _Card.AccessID = (byte)this.txtAccessLevel.IntergerValue;
            _Card.CarPlate = this.txtCarPlate.Text;
            _Card.Balance = this.txtBalance.DecimalValue;
            _Card.IndexNumber = this.txtIndexNumber.IntergerValue;
            _Card.ActivationDate = this.dtActivationDate.Value;
            _Card.ValidDate = this.dtValidDate.Value;
            _Card.OnlineHandleWhenOfflineMode = this.chkOnlineHandleWhenOfflineMode.Checked;
            _Card.CanRepeatIn = this.chkRepeatIn.Checked;
            _Card.CanRepeatOut = this.chkRepeatOut.Checked;
            _Card.HolidayEnabled = this.chkHoliday.Checked;
            _Card.WithCount = this.chkWithCount.Checked;
            _Card.EnableWhenExpired = this.chkEnableWhenExpired.Checked;
            _Card.CanEnterWhenFull = this.chkCanEnterWhenFull.Checked;
            _Card.IsInPark = this.chkIn1.Checked;
            _Card.IsInNestedPark = this.chkIn2.Checked;
            _Card.IsPaid = this.chkPaid1.Checked;
            _Card.IsMarkNestedPark = this.chkIn2Mark.Checked;
            _Card.EnableHotelApp = this.chkEnableHotelApp.Checked;
            _Card.HotelCheckOut = !this.chkNotCheckOut.Checked;
            _Card.LastDateTime = this.dtEnterTime.Value;
            if (this.chkPaidTime.Checked)
                _Card.PaidDateTime = this.dtPaidTime.Value;
            else
                _Card.PaidDateTime = null;
            if (this.chkFreeDateTime.Checked)
                _Card.FreeDateTime = this.dtFreeDateTime.Value;
            else
                _Card.FreeDateTime = null;
            _Card.ParkFee = this.txtFee.DecimalValue;
            _Card.TotalPaidFee = this.txtPaidFee.DecimalValue;

            return _Card;
        }
        #endregion

        #region 窗体事件

        private void FrmParkingDebugTool_Load(object sender, EventArgs e)
        {
            InitCardType();
            InitCarType();
            UserSetting.Current = new UserSetting();
            UserSetting.Current.WegenType = WegenType.Wengen34;
            KeySetting.Current = new KeySetting();

            _CurrentSection = GlobalVariables.ParkingSection;

            CardReaderManager.GetInstance(UserSetting.Current.WegenType).AddReadSectionAndKey(_CurrentSection, GlobalVariables.ParkingKey);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            _Card = null;
            ClearInput();
            ReadCardResult result = CardReaderManager.GetInstance(UserSetting.Current.WegenType).ReadSection(string.Empty, _CurrentSection, 0, 3, GlobalVariables.ParkingKey, true, true, false);

            if (CardOperationManager.Instance.ShowResultMessage(result.ResultCode))
            {
                this.txtCardID.Text = result.CardID;
                byte[] carddate = result[_CurrentSection];
                _Card = CardDateResolver.Instance.GetCardInfoFromData(result.CardID, carddate);
                List<byte[]> data = new List<byte[]>();
                if (carddate.Length == 16 * 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        byte[] bytes = new byte[16];
                        Array.Copy(carddate, i * 16, bytes, 0, 16);
                        data.Add(bytes);
                    }
                    ucSection1.SectionData = data;
                }
            }
            if (_Card != null)
                ShowCard(_Card);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                GetCardFromInput();
                CardOperationManager.Instance.WriteCardLoop(_Card);
            }
        }

        #endregion


        #endregion

        #region 控制板信息

        #region 私有方法
        private Ralid.Park.Hardware.H_TariffType ConvertTariffType(Ralid.Park.BusinessModel.Enum.TariffType tariffType)
        {
            if (tariffType == TariffType.InnerRoom || tariffType == TariffType.HolidayAndInnerRoom)//室内费率
            {
                return H_TariffType.InDoorTariff;
            }
            else
            {
                return H_TariffType.Tariff;
            }

        }

        private Ralid.Park.Hardware.H_Tariff_CardType ConvertTariffCardType(byte cardType)
        {
            byte baseCardType = (byte)(cardType & 0x0F);
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.OwnerCard.ID)
            {
                return H_Tariff_CardType.OwnerCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard.ID)
            {
                return H_Tariff_CardType.MonthCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.PrePayCard.ID)
            {
                return H_Tariff_CardType.PrePayCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.TempCard.ID)
            {
                return H_Tariff_CardType.TempCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1.ID)
            {
                return H_Tariff_CardType.UserDefinedCard1;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2.ID)
            {
                return H_Tariff_CardType.UserDefinedCard2;
            }

            return H_Tariff_CardType.OwnerCard;
        }

        private Ralid.Park.Hardware.H_Tariff_CarType ConvertTariffCarType(byte carType, TariffType tariffType)
        {

            if (tariffType == TariffType.Holiday || tariffType == TariffType.HolidayAndInnerRoom)//节假日费率
            {

                return (H_Tariff_CarType)(carType + 4);//4~7为车型节假日费率
            }
            else
            {
                return (H_Tariff_CarType)carType;//0~3为车型工作日费率
            }
        }

        private Ralid.Park.Hardware.H_TariffInfo ConvertTariffInfo(short freeTimeAfterPay, Ralid.Park.BusinessModel.Model.TariffBase tariff)
        {
            H_TariffInfo h_Tariff = new H_TariffInfo();
            h_Tariff.TariffType = ConvertTariffType(tariff.TariffType);
            h_Tariff.CardType = ConvertTariffCardType(tariff.CardType);
            h_Tariff.CarType = ConvertTariffCarType(tariff.CarType, tariff.TariffType);
            h_Tariff.T2 = freeTimeAfterPay;

            if (tariff is TariffPerTime)//按次收费
            {
                TariffPerTime t = tariff as TariffPerTime;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode1;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid
                    | H_Tariff_ChargeProperty.DailyLimitInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid
                    | H_Tariff_ChargeProperty.MaximumAmountInvalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M1[0] = (int)(t.FeePerTime * 100);
            }

            if (tariff is TariffPerDay)//按天收费
            {
                TariffPerDay t = tariff as TariffPerDay;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode2;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.DailyLimitInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M1[0] = (int)(t.FeePerDay * 100);

                if (t.OverDay > 0)
                {
                    h_Tariff.T3 = t.OverDay;
                    h_Tariff.M2 = (int)(t.FeePerOverDay * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.OverTimeInvalid;
                }

                if (t.FeeOfMax > 0)
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfTurning)//过点收费
            {
                TariffOfTurning t = tariff as TariffOfTurning;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode1;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.DailyLimitInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M1[0] = (int)(t.FirstFee * 100);
                h_Tariff.T3 = (short)(Ralid.GeneralLibrary.BCDConverter.IntToBCD(t.Turning.Hour) * 0x100 + Ralid.GeneralLibrary.BCDConverter.IntToBCD(t.Turning.Minute));
                h_Tariff.M2 = (int)(t.FeeOfTurning * 100);

                if (t.FeeOfMax > 0)
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfLimitation)//限额收费
            {
                TariffOfLimitation t = tariff as TariffOfLimitation;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode4;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                if (t.FirstCharge != null)
                {
                    h_Tariff.T4 = t.FirstCharge.Minutes;
                    h_Tariff.T5[0] = t.FirstCharge.Minutes;
                    h_Tariff.M1[0] = (int)(t.FirstCharge.Fee * 100);
                }
                h_Tariff.T6[0] = t.RegularCharge.Minutes;
                h_Tariff.M3[0] = (int)(t.RegularCharge.Fee * 100);
                if (t.FeeOf12Hour > 0)
                {
                    h_Tariff.T7 = 12 * 60;//12小时限额
                    h_Tariff.M5 = (int)(t.FeeOf12Hour * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.IntervalTop1Invalid;
                }
                if (t.FeeOf24Hour > 0)
                {
                    h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.DailyLimitInvalid;
                }
                if (t.FeeOfMax > 0)//封顶收费，最高收费
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfGuanZhou)//日夜差异收费
            {
                TariffOfGuanZhou t = tariff as TariffOfGuanZhou;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode3;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额

                //白天时段
                h_Tariff.TimeInterval[0] = new H_TimeInterval();
                h_Tariff.TimeInterval[0].BeginTime = new H_TimeEntity(t.DayTimezone.Beginning.Hour, t.DayTimezone.Beginning.Minute);
                h_Tariff.TimeInterval[0].EndTime = new H_TimeEntity(t.DayTimezone.Ending.Hour, t.DayTimezone.Ending.Minute);
                h_Tariff.T6[0] = t.DayTimezone.RegularCharge.Minutes;
                h_Tariff.M3[0] = (int)(t.DayTimezone.RegularCharge.Fee * 100);
                if (t.DayTimezone.LimiteFee.HasValue && t.DayTimezone.LimiteFee.Value > 0)//白天时段有最高限额
                {
                    h_Tariff.M4[0] = (int)(t.DayTimezone.LimiteFee.Value * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.IntervalTop1Invalid;
                }

                //夜间时段
                h_Tariff.TimeInterval[1] = new H_TimeInterval();
                h_Tariff.TimeInterval[1].BeginTime = new H_TimeEntity(t.NightTimezone.Beginning.Hour, t.NightTimezone.Beginning.Minute);
                h_Tariff.TimeInterval[1].EndTime = new H_TimeEntity(t.NightTimezone.Ending.Hour, t.NightTimezone.Ending.Minute);
                h_Tariff.T6[1] = t.NightTimezone.RegularCharge.Minutes;
                h_Tariff.M3[1] = (int)(t.NightTimezone.RegularCharge.Fee * 100);
                if (t.NightTimezone.LimiteFee.HasValue && t.NightTimezone.LimiteFee.Value > 0)//夜间有最高限额
                {
                    h_Tariff.M4[1] = (int)(t.NightTimezone.LimiteFee.Value * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.IntervalTop2Invalid;
                }

                if (t.FeeOf24Hour > 0)
                {
                    h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.DailyLimitInvalid;
                }

                if (t.FeeOfMax > 0)//封顶收费，最高收费
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfDixiakongjian)//时段限额收费
            {
                TariffOfDixiakongjian t = tariff as TariffOfDixiakongjian;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode3;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额

                //正常时段
                h_Tariff.TimeInterval[0] = new H_TimeInterval();
                h_Tariff.TimeInterval[0].BeginTime = new H_TimeEntity(t.LimitationTimezone.Ending.Hour, t.LimitationTimezone.Ending.Minute);//以限价时段结束时间为开始时间
                h_Tariff.TimeInterval[0].EndTime = new H_TimeEntity(t.LimitationTimezone.Beginning.Hour, t.LimitationTimezone.Beginning.Minute);//以限价时段开始时间为结束时间
                h_Tariff.T4 = (short)t.FirstMinutes;
                h_Tariff.T5[0] = t.FirstFee.Minutes;
                h_Tariff.M1[0] = (int)(t.FirstFee.Fee * 100);
                h_Tariff.T6[0] = t.RegularFee.Minutes;
                h_Tariff.M3[0] = (int)(t.RegularFee.Fee * 100);

                //限价时段
                h_Tariff.TimeInterval[1] = new H_TimeInterval();
                h_Tariff.TimeInterval[1].BeginTime = new H_TimeEntity(t.LimitationTimezone.Beginning.Hour, t.LimitationTimezone.Beginning.Minute);
                h_Tariff.TimeInterval[1].EndTime = new H_TimeEntity(t.LimitationTimezone.Ending.Hour, t.LimitationTimezone.Ending.Minute);
                h_Tariff.T6[1] = t.LimitationRegularFee.Minutes;
                h_Tariff.M3[1] = (int)(t.LimitationRegularFee.Fee * 100);
                h_Tariff.M4[1] = (int)(t.Limitation * 100);

                if (t.FeeOf24Hour > 0)
                {
                    h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.DailyLimitInvalid;
                }
                if (t.FeeOfMax > 0)//封顶收费，最高收费
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            return h_Tariff;
        }

        private List<Ralid.Park.Hardware.H_TariffSetting> GetCardTypeTariffSettingFrom(Ralid.Park.BusinessModel.Model.TariffSetting tariffSetting, byte cardType)
        {
            List<H_TariffSetting> h_TariffSettings = new List<H_TariffSetting>();
            H_TariffSetting h_TariffSetting1 = new H_TariffSetting();//正常费率
            H_TariffSetting h_TariffSetting2 = new H_TariffSetting();//室内费率
            h_TariffSetting1.CardType = ConvertTariffCardType(cardType);
            h_TariffSetting2.CardType = ConvertTariffCardType(cardType);
            h_TariffSetting1.TariffType = H_TariffType.Tariff;
            h_TariffSetting2.TariffType = H_TariffType.InDoorTariff;

            List<TariffBase> tariffs = tariffSetting.GetBaseCarTypeTariffs(cardType);
            if (tariffs != null && tariffs.Count > 0)
            {
                foreach (TariffBase tariff in tariffs)
                {
                    H_TariffInfo h_Tariff = ConvertTariffInfo((short)tariffSetting.TariffOption.FreeTimeAfterPay, tariff);
                    if (h_Tariff.TariffType == H_TariffType.Tariff)
                    {
                        h_TariffSetting1.AddTariff(h_Tariff.CarType, h_Tariff);
                    }
                    else
                    {
                        h_TariffSetting2.AddTariff(h_Tariff.CarType, h_Tariff);
                    }

                }
            }
            h_TariffSettings.Add(h_TariffSetting1);
            h_TariffSettings.Add(h_TariffSetting2);

            return h_TariffSettings;
        }

        private List<Ralid.Park.Hardware.H_TariffSetting> GetTariffSettingFrom(Ralid.Park.BusinessModel.Model.TariffSetting tariffSetting)
        {
            List<H_TariffSetting> h_TariffSettings = new List<H_TariffSetting>();

            //业主卡
            List<H_TariffSetting> tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.OwnerCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //月租卡
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //储值卡
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.PrePayCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //临时卡
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.TempCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //自定义卡片1
            byte cardTypeID = Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1.ID;
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, CustomCardTypeSetting.Current.GetFirstCardTypeFromBase(cardTypeID).ID);//只下载第一个的费率
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //自定义卡片2
            cardTypeID = Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2.ID;
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, CustomCardTypeSetting.Current.GetFirstCardTypeFromBase(cardTypeID).ID);//只下载第一个的费率
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }


            return h_TariffSettings;
        }
        #endregion

        #region 窗体事件

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            grid.Rows.Clear();
            List<ParkDevice> devices = DeviceSearcher.EnumAllParkDevices();
            if (devices != null && devices.Count > 0)
            {
                foreach (ParkDevice device in devices)
                {
                    int row = grid.Rows.Add();
                    grid.Rows[row].Tag = device;
                    Ralid.Park.Hardware.DeviceInfo deviceInfo;
                    if (device.GetDeviceInfo(out deviceInfo))
                    {
                        grid.Rows[row].Cells["colSerialNum"].Value = deviceInfo.StrSerialNum;
                    }
                    grid.Rows[row].Cells["colIP"].Value = device.LANInfo.IPAddress;
                    grid.Rows[row].Cells["colMAC"].Value = device.LANInfo.MACAddress;
                }
            }
            this.Cursor = Cursors.Arrow;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckSetting())
            {
                SaveSetting();
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).AddReadSectionAndKey(GlobalVariables.ParkingSection, GlobalVariables.ParkingKey);
                MessageBox.Show("保存成功！");
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabSetting
                || this.tabControl1.SelectedTab == this.tabCard)
            {
                ShowSetting();
            }
        }
        private void btnWriteSection_Click(object sender, EventArgs e)
        {
            if (ucSection1.CheckInput())
            {
                List<byte[]> data = ucSection1.SectionData;
                if (data.Any(item => item != null))
                {
                    bool success = true;
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i] != null && data[i].Length == 16)
                        {
                            CardOperationResultCode result = CardReaderManager.GetInstance(UserSetting.Current.WegenType).WriteSection(string.Empty, _CurrentSection, i, 1, data[i], GlobalVariables.ParkingKey, false, false);
                            success = success ? result == CardOperationResultCode.Success : success;
                        }
                    }
                    if (success) CardReaderManager.GetInstance(UserSetting.Current.WegenType).SucessBuz();
                }
                else
                {
                    MessageBox.Show("请选择块");
                    ucSection1.Focus();
                }
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.eventReportListBox1.InsertMessage("Red", Color.Red);
            this.eventReportListBox1.InsertMessage("Yellow", Color.Yellow);
            this.eventReportListBox1.InsertMessage("Blue", Color.Blue);
        }

        



        #endregion
    }
}
