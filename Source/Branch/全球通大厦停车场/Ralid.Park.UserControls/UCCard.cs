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
        #endregion

        #region 私有方法
        private bool CheckInput()
        {
            string text = string.Empty;
            if (string.IsNullOrEmpty(this.txtCardID.Text.Trim()))
            {
                MessageBox.Show("卡号不能为空");
                return false;
            }
            if (this.comCardType.SelectedIndex <= 0)
            {
                MessageBox.Show(Resources.Resource1.UcCard_InvalidCardType);
                return false;
            }
            text = this.txtOwnerName.Text.Trim();
            if (text.Length == 0)
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
                    _card.Telphone = this.txtTelphone.Text;
                    _card.AccessID = (byte)this.comAccessLevel.AccesslevelID;
                    _card.ActivationDate = this.dtActivationDate.Value.Date;
                    _card.ValidDate = this.dtValidDate.Value.Date.AddDays(1).AddSeconds(-1);
                    _card.CanRepeatIn = this.chkRepeatIn.Checked;
                    _card.CanRepeatOut = this.chkRepeatOut.Checked;
                    _card.HolidayEnabled = this.chkHoliday.Checked;
                    _card.WithCount = this.chkWithCount.Checked;
                    _card.CanEnterWhenFull = this.chkCanEnterWhenFull.Checked;
                    _card.EnableWhenExpired = this.chkEnableWhenExpired.Checked;
                    _card.EnableLimitation = this.chkEnableLimitation.Checked;
                    _card.OnlineHandleWhenOfflineMode = this.chkOnlineHandleWhenOfflineMode.Checked;
                    _card.OwnerName = txtOwnerName.Text.Trim();
                    _card.CarPlate = txtCarPlate.Text.Trim();
                    _card.Memo = txtMemo.Text.Trim();
                    if (txtLimitationRemain.Enabled)
                    {
                        _card.LimitationRemain = txtLimitationRemain.DecimalValue;
                        //如果卡片的剩余时间的更新月分与当前月分不一样，则修改更新时间
                        if (_card.LimitationTimestamp == null || _card.LimitationTimestamp.Value.Date.Month != DateTime.Today.Month)
                        {
                            _card.LimitationTimestamp = DateTime.Now;
                        }
                    }
                    else
                    {
                        _card.LimitationRemain = null;
                        _card.LimitationTimestamp = null;
                    }
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
                    this.txtTelphone.Text = _card.Telphone;
                    this.comAccessLevel.AccesslevelID = _card.AccessID;
                    this.dtActivationDate.Value = _card.ActivationDate;
                    this.dtValidDate.Value = _card.ValidDate;
                    this.chkRepeatIn.Checked = _card.CanRepeatIn;
                    this.chkRepeatOut.Checked = _card.CanRepeatOut;
                    this.chkHoliday.Checked = _card.HolidayEnabled;
                    this.chkWithCount.Checked = _card.WithCount;
                    this.chkEnableWhenExpired.Checked = _card.EnableWhenExpired;
                    this.chkCanEnterWhenFull.Checked = _card.CanEnterWhenFull;
                    this.chkEnableLimitation.Checked = _card.EnableLimitation;
                    this.chkOnlineHandleWhenOfflineMode.Checked = _card.OnlineHandleWhenOfflineMode;
                    this.txtOwnerName.Text = _card.OwnerName;
                    this.txtCarPlate.Text = _card.CarPlate;
                    txtLimitationRemain.Enabled = _card.EnableLimitation;
                    if (_card.EnableLimitation)
                    {
                        txtLimitationRemain.Enabled = true;
                        txtLimitationRemain.DecimalValue = (_card.LimitationRemain == null ? 35 : _card.LimitationRemain.Value);
                    }
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
            this.comAccessLevel.Init();
            this.comCardType.SelectedCardType = CardType.MonthRentCard;
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
            comAccessLevel.Enabled = true;
            txtTelphone.Enabled = true;
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

        private void chkEnableLimitation_CheckedChanged(object sender, EventArgs e)
        {
            txtLimitationRemain.Enabled = chkEnableLimitation.Checked;
        }
    }
}
