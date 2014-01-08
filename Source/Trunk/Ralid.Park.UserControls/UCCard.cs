using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model ;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls
{
    public partial class UCCard : UserControl
    {
        public UCCard()
        {
            InitializeComponent();
        }

        #region 私有变量
        private CardInfo _card;
        private bool _cardInfoEditable;
        private bool _readonly;
        #endregion

        #region 私有方法
        private bool CheckInput()
        {
            string text;
            UInt32 val;

            text = this.txtCardID.Text.Trim();
            if (!UInt32.TryParse(text, out val))
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidCardID);
                return false;
            }
            if (this.comCardType.SelectedIndex <= 0)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidCardType);
                return false;
            }
            if (this.comChargeType.SelectedIndex <= 0)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidCarType);
                return false;
            }

            text = this.txtOwnerName.Text.Trim();
            if (text.Length == 0 && comCardType.SelectedCardType != CardType.TempCard)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidOwnerName);
                return false;
            }
            DateTime dateTime = new DateTime(2011, 1, 1);
            DateTime maxDateTime = new DateTime(2099, 12, 31);
            if (dtActivationDate.Value < dateTime)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidActivationDate);
                return false;
            }
            else if (dtActivationDate.Value > maxDateTime)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidMaxActivationDate);
                return false; 
            }
            if (dtValidDate.Value < dateTime)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidValidDate);
                return false;
            }
            else if (dtValidDate.Value > maxDateTime)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidMaxValidDate);
                return false;
            }


            //车牌号码的字节组最大只能9个字节,主要是考虑写卡模式时，卡片只能保持9个字节的数据
            text = this.txtCarPlate.Text.Trim();
            byte[] tempbytes = Encoding.GetEncoding("gb2312").GetBytes(text);
            if (tempbytes.Length > 9)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidCarPlate);
                txtCarPlate.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region 公共属性和方法
        /// <summary>
        /// 设置和获取卡片
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CardInfo Card
        {
            get
            {
                if (CheckInput())
                {
                    if (_card == null) _card = new CardInfo();
                    _card.CardID = this.txtCardID.Text.Trim();
                    _card.CardCertificate = this.txtCertificate.Text.Trim();
                    _card.CardType = comCardType.SelectedCardType;
                    _card.CarType = this.comChargeType.SelectedCarType;
                    _card.AccessID = (byte)this.comAccessLevel.AccesslevelID;
                    _card.Balance = this.txtBalance.DecimalValue;
                    _card.Deposit = this.txtDeposit.DecimalValue;
                    _card.ActivationDate = this.dtActivationDate.Value.Date;
                    _card.ValidDate = this.dtValidDate.Value.Date.AddDays(1).AddSeconds(-1);
                    _card.CanRepeatIn = this.chkRepeatIn.Checked;
                    _card.CanRepeatOut = this.chkRepeatOut.Checked;
                    _card.HolidayEnabled = this.chkHoliday.Checked;
                    _card.WithCount = this.chkWithCount.Checked;
                    _card.CanEnterWhenFull = this.chkCanEnterWhenFull.Checked;
                    _card.EnableWhenExpired = this.chkEnableWhenExpired.Checked;
                    _card.OnlineHandleWhenOfflineMode = this.chkOnlineHandleWhenOfflineMode.Checked;
                    _card.OwnerName = txtOwnerName.Text.Trim();
                    _card.CarPlate = txtCarPlate.Text.Trim();
                    _card.Memo = txtMemo.Text.Trim();
                    return _card;
                }
                return null;
            }
            set
            {
                _card = value;
                if (_card != null)
                {
                    this.txtCardID.Text = _card.CardID;
                    this.txtCertificate.Text = _card.CardCertificate;
                    this.comCardType.SelectedCardType = _card.CardType;
                    this.comChargeType.SelectedCarType = _card.CarType;
                    this.comAccessLevel.AccesslevelID = _card.AccessID;
                    this.txtBalance.DecimalValue = _card.Balance;
                    this.txtDeposit.DecimalValue = _card.Deposit;
                    this.dtActivationDate.Value = _card.ActivationDate;
                    this.dtValidDate.Value = _card.ValidDate;
                    this.chkRepeatIn.Checked = _card.CanRepeatIn;
                    this.chkRepeatOut.Checked = _card.CanRepeatOut;
                    this.chkHoliday.Checked = _card.HolidayEnabled;
                    this.chkWithCount.Checked = _card.WithCount;
                    this.chkEnableWhenExpired.Checked = _card.EnableWhenExpired;
                    this.chkCanEnterWhenFull.Checked = _card.CanEnterWhenFull;
                    this.chkOnlineHandleWhenOfflineMode.Checked = _card.OnlineHandleWhenOfflineMode;
                    this.txtOwnerName.Text = _card.OwnerName;
                    this.txtCarPlate.Text = _card.CarPlate;
                    this.txtMemo.Text = _card.Memo;
                }
                else
                {
                    Clear();
                }
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            this.comCardType.Init();
            this.comChargeType.Init();
            this.comAccessLevel.Init();
            this.comCardType.SelectedCardType = CardType.MonthRentCard;
            this.comChargeType.SelectedCarType = CarTypeSetting.DefaultCarType;
        }
        /// <summary>
        /// 控件用于显示卡片信息，此时控件包括其子控件都不能编辑
        /// </summary>
        public void UseToShow()
        {
            foreach (Control control in this.groupBox2.Controls)
            {
                control.Enabled = false;
            }
            foreach (Control control in this.groupBox3.Controls)
            {
                control.Enabled = false;
            }
        }
        /// <summary>
        /// 控件用于修改卡片，此时只有部分内容可编辑
        /// </summary>
        public void UseToEdit()
        {
            foreach (Control control in this.groupBox2.Controls)
            {
                control.Enabled = (control is Label) ? true : false;
            }
            txtCertificate.Enabled = true;
            comChargeType.Enabled = true;
            comAccessLevel.Enabled = true;
            dtActivationDate.Enabled = true;
            txtOwnerName.Enabled = true;
            txtCarPlate.Enabled = true;
            txtMemo.Enabled = true;
            foreach (Control control in this.groupBox3.Controls)
            {
                control.Enabled = true;
            }
        }
        /// <summary>
        /// 控件用于发行卡片，此时所有内容都可以编辑
        /// </summary>
        public void UseToRelease()
        {
            foreach (Control control in this.groupBox2.Controls)
            {
                control.Enabled = true;
            }
            foreach (Control control in this.groupBox3.Controls)
            {
                control.Enabled = true;
            }
        }
        /// <summary>
        /// 清空信息
        /// </summary>
        public void Clear()
        {
            this._card = null;
            this.txtCardID.Text = string.Empty;
            this.txtCertificate.Text = string.Empty;
            this.txtOwnerName.Text = string.Empty;
            this.txtCarPlate.Text = string.Empty;
            this.txtMemo.Text = string.Empty;
        }
        #endregion
    }
}
