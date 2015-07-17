using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.UI.Resources;
using Ralid.GeneralLibrary.Speech;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmHotelAuthorization : Form
    {
        public FrmHotelAuthorization()
        {
            InitializeComponent();
        }

        private CardInfo _cardInfo;//卡片信息
        private CardInfo _OriginalCard;//卡片原始信息

        #region 私有方法
        /// <summary>
        /// 读取到卡号处理
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="info">从卡片扇区数据中读取到的卡片信息</param>
        private void ReadCardIDHandler(string cardID, CardInfo info)
        {
            txtCardID.TextChanged -= txtCardID_TextChanged;
            this.txtCardID.Text = cardID;
            this.txtCardID.ReadOnly = true;
            string msg = string.Empty;
            bool offlineHandleCard = AppSettings.CurrentSetting.EnableWriteCard
                && info != null
                && !info.OnlineHandleWhenOfflineMode;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            //如果只是免费授权，卡片信息只要从主数据库获取就可以了
            CardInfo card = bll.GetCardDetail(cardID);
            if (card == null && offlineHandleCard) card = info.Clone();

            if (!WorkStationInfo.CurrentStation.CanFreeAuthorization(offlineHandleCard, out msg))
            {
                //该工作站不能进行授权
            }
            else if (card == null)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_UnRegister);
            }
            else if (AppSettings.CurrentSetting.EnableWriteCard
                && !card.OnlineHandleWhenOfflineMode
                && !CardDateResolver.Instance.CopyPaidDataToCard(card, info))//只复制缴费相关的信息，如果复制了所有的信息，会覆盖数据库内的卡片状态，如挂失，禁用等状态
                //&& !CardDateResolver.Instance.CopyCardDataToCard(card, info))
            {
                //写卡模式时，卡片信息从扇区数据中获取
                msg = Resource1.FrmCardCenterCharge_CardDataErr;
            }
            else if (!ValidateCard(card, out msg))
            {
                //卡片无效
            }
            else
            {
                _cardInfo = card;
                _OriginalCard = card.Clone();
                ShowCardInfo(_cardInfo);
            }
            if (!string.IsNullOrEmpty(msg))
            {
                if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(msg);
                ClearInput();
                this.txtCardID.Text = cardID;
                MessageBox.Show(msg);
            }
            txtCardID.TextChanged += txtCardID_TextChanged;
        }

        /// <summary>
        /// 检验卡片有效性
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool ValidateCard(CardInfo card, out string msg)
        {
            if (card.Status == CardStatus.Recycled) //卡片已注销
            {
                msg = Resource1.FrmCardCenterCharge_CardRecycled;
                return false;
            }
            if (card.Status == CardStatus.Disabled)  //卡片已锁定
            {
                msg = Resource1.FrmCardCenterCharge_CardDisabled;
                return false;
            }
            if (card.Status == CardStatus.Loss)   //卡片已挂失
            {
                msg = Resource1.FrmCardCenterCharge_CardLoss;
                return false;
            }
            if (card.ActivationDate > DateTime.Now) //卡片未到生效期
            {
                msg = Resource1.FrmCardCenterCharge_CardUnActivate;
                return false;
            }
            if (card.ValidDate < DateTime.Today && card.CardType != Ralid.Park.BusinessModel.Enum.CardType.TempCard && !card.EnableWhenExpired) //卡片已过期
            {
                msg = Resource1.FrmCardCenterCharge_CardExpired;
                return false;
            }
            msg = string.Empty;
            return true;
        }

        private void ClearInput()
        {
            _cardInfo = null;
            _OriginalCard = null;
            this.txtCardID.Text = string.Empty;
            this.txtCardID.ReadOnly = false;
            this.lblInPark.Text = string.Empty;
            this.lblEnterDateTime.Text = string.Empty;
            this.lblAuthorization.Text = string.Empty;
            this.lblCheckOut.Text = string.Empty;
            this.lblOldFreeDateTime.Text = string.Empty;
            this.txtDays.Value = 0;
            this.lblFreeDateTime.Text = string.Empty;
        }

        private void GetInfoFromInput(bool checkOut)
        {
            if (_cardInfo != null)
            {
                _cardInfo.EnableHotelApp = true;
                _cardInfo.HotelCheckOut = checkOut;
                if (string.IsNullOrEmpty(this.lblFreeDateTime.Text))
                {
                    _cardInfo.FreeDateTime = null;
                }
                else
                {
                    DateTime freeDateTime ;
                    if (DateTime.TryParse(this.lblFreeDateTime.Text, out freeDateTime))
                    {
                        _cardInfo.FreeDateTime = freeDateTime;
                    }
                    else
                    {
                        _cardInfo.FreeDateTime = null; 
                    }
                }
            }
        }

        private void ShowCardInfo(CardInfo info)
        {
            this.txtCardID.Text = info.CardID;
            if (info.IsInPark)
            {
                this.lblInPark.Text = Resource1.FrmHotelAuthorization_InPark;
                this.lblEnterDateTime.Text = info.LastDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                this.lblInPark.Text = Resource1.FrmHotelAuthorization_OutPark; 
            }
            this.lblAuthorization.Text = info.EnableHotelApp ? Resource1.FrmHotelAuthorization_Authorization : Resource1.FrmHotelAuthorization_NotAuthorization;
            this.lblCheckOut.Text = info.HotelCheckOut ? Resource1.FrmHotelAuthorization_NotMultipleAccess : Resource1.FrmHotelAuthorization_MultipleAccess;

            int days = 0;
            if (_cardInfo.IsInPark)
            {
                days = GetFreeDays(_cardInfo.LastDateTime, DateTime.Now);
            }
            if (days > 1)
            {
                this.txtDays.Value = days < this.txtDays.Maximum ? days : this.txtDays.Maximum;
            }
            else
            {
                //如果没有默认天数或默认天数小于2天，默认为2天
                if (this.txtDays.Value <= 0)
                    this.txtDays.Value = 2;
                else
                    txtDays_ValueChanged(null, null);
            }

            if (info.FreeDateTime.HasValue)
            {
                this.lblOldFreeDateTime.Text = info.FreeDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");           
            }
            else
            {
                this.lblOldFreeDateTime.Text = string.Empty;
            }

        }

        private bool CheckInput()
        {
            if (_cardInfo == null)
            {
                MessageBox.Show(Resource1.FrmHotelAuthorization_NoCard);
                return false;
            }
            if (this.txtDays.Value < 1)
            {
                MessageBox.Show(Resource1.FrmHotelAuthorization_FreeDays);
                return false; 
            }
            return true;
        }

        private bool CheckWriteCard()
        {
            //写卡模式并且不是按在线模式处理时需要检查卡片是否在读卡区域
            if (AppSettings.CurrentSetting.EnableWriteCard
                && _cardInfo != null
                && !_cardInfo.OnlineHandleWhenOfflineMode)
            {
                return CardOperationManager.Instance.CheckCardWithMessage(_cardInfo.CardID, false, true);
            }

            return true;
        }

        private DateTime CreateFreeDateTime(DateTime begin, int days)
        {
            //DateTime begin = info.IsInPark ? info.LastDateTime : DateTime.Now;
            DateTime free = new DateTime();
            if (rabhours.Checked)
            {
                free = new DateTime(begin.Year, begin.Month, begin.Day, begin.Hour, begin.Minute,0);
                free = free.AddHours(days);
            }
            else
            {
                free = new DateTime(begin.Year, begin.Month, begin.Day, 0, 0, 0);
                free = free.AddDays(days);
            }
            return free;
        }

        /// <summary>
        /// 获取只精确到分钟的日期时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DateTime GetMyDateTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// 获取免费天数
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int GetFreeDays(DateTime begin, DateTime free)
        {
            if (free > begin)
            {
                TimeSpan ts = new TimeSpan(GetMyDateTime(free).Ticks - GetMyDateTime(begin).Ticks);
                return (int)Math.Ceiling(ts.TotalDays);
            }
            return 0;
        }

        private FreeAuthorizationLog CreateFreeAuthorizationLog(CardInfo info)
        {
            FreeAuthorizationLog log = new FreeAuthorizationLog();
            log.LogDateTime = DateTime.Now;
            log.CardID = info.CardID;
            log.BeginDateTime = info.LastDateTime;
            if (info.FreeDateTime.HasValue)
            {
                log.EndDateTime = info.FreeDateTime.Value;
            }
            log.InPark = info.IsInPark;
            log.CheckOut = info.HotelCheckOut;
            log.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
            log.StationID = WorkStationInfo.CurrentStation.StationName;
            log.Memo = string.Empty;
            return log;
        }

        /// <summary>
        /// 免费授权
        /// </summary>
        private void FreeAuthorization(CardInfo cardinfo)
        {
            bool offlineHandleCard = AppSettings.CurrentSetting.EnableWriteCard
                && cardinfo != null
                && !cardinfo.OnlineHandleWhenOfflineMode;
            CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            bool success = _CardBll.CardFreeAuthorizationWithStandby(_cardInfo, WorkStationInfo.CurrentStation.HasStandbyDatabase, AppSettings.CurrentSetting.CurrentStandbyConnect).Result == ResultCode.Successful;

            //写卡模式时需要将授权信息写入卡片扇区，并且只需要写入卡片成功就可以了
            if (offlineHandleCard)
            {
                success = CardOperationManager.Instance.WriteCardLoop(cardinfo) == CardOperationResultCode.Success;
                if (!success)
                {
                    //写入失败时，将数据库的卡片授权信息还原
                    _CardBll.CardFreeAuthorization(_OriginalCard);
                    if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.CurrentStandbyConnect))
                    {
                        //还原备用数据库
                        CardBll standbyCardBll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                        standbyCardBll.CardFreeAuthorization(_OriginalCard);
                    }
                }
            }

            if (success)
            {
                //插入授权记录
                FreeAuthorizationLog log = CreateFreeAuthorizationLog(_cardInfo);
                FreeAuthorizationLogBll logBll = new FreeAuthorizationLogBll(AppSettings.CurrentSetting.ParkConnect);
                logBll.Insert(log);

                MessageBox.Show(Resource1.FrmHotelAuthorization_Authe + Resource1.Form_Success);
                ClearInput();
            }
            else
            {
                MessageBox.Show(Resource1.FrmHotelAuthorization_Authe + Resource1.Form_Fail);
            }
        }
        #endregion

        #region 私有事件
        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!string.IsNullOrEmpty(e.CardID))
                {
                    ClearInput();
                    CardInfo card = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);
                    ReadCardIDHandler(e.CardID, card);
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        #endregion

        #region 窗体事件
        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCardID.Text))
            {
                CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = _CardBll.GetCardByID(this.txtCardID.Text).QueryObject;
                if (card != null)
                {
                    ReadCardIDHandler(txtCardID.Text, null);
                }
            }
        }

        private void FrmHotelAuthorization_Load(object sender, EventArgs e)
        {
            ClearInput();
            
            if (!AppSettings.CurrentSetting.EnableHotel)
            {
                this.btnOK.Visible = false;
                this.btnCheckOut.Text = Resource1.FrmHotelAuthorization_Authe + "[F10]";

                this.btnCheckOut.Left = (this.groupBox1.Width - this.btnCheckOut.Width - this.btnCancel.Width - 40) / 2;
                this.btnCancel.Left = this.btnCheckOut.Left + this.btnCheckOut.Width + 40;
            }

            //从配置文件中获取免费授权的时间单位，0为天，1为小时，默认为天
            string temp = AppSettings.CurrentSetting.GetConfigContent("AuthorizationUnit");
            if (temp == "0")
            {
                this.rabdays.Checked = true;
            }
            else if (temp == "1")
            {
                this.rabhours.Checked = true;
            }
        }
        private void FrmHotelAuthorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }
        private void FrmHotelAuthorization_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    if (this.btnOK.Visible && this.btnOK.Enabled)
                    {
                        btnOK_Click(this.btnOK, EventArgs.Empty);
                    }
                    break;
                case Keys.F10:
                    if (this.btnCheckOut.Visible && this.btnCheckOut.Enabled)
                    {
                        btnCheckOut_Click(this.btnCheckOut, EventArgs.Empty);
                    }
                    break;
                case Keys.F11:
                    if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    {
                        btnCancel_Click(this.btnCancel, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInput() && CheckWriteCard())
            {
                GetInfoFromInput(false);

                FreeAuthorization(_cardInfo);
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {

            if (CheckInput() && CheckWriteCard())
            {
                GetInfoFromInput(true);

                FreeAuthorization(_cardInfo);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput();
        }
        private void txtDays_ValueChanged(object sender, EventArgs e)
        {
            if (_cardInfo != null)
            {
                if (this.txtDays.Value > 0)
                {
                    DateTime freeDateTime = CreateFreeDateTime(_cardInfo.IsInPark ? _cardInfo.LastDateTime : DateTime.Now, (int)this.txtDays.Value);
                    this.lblFreeDateTime.Text = freeDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    this.lblFreeDateTime.Text = string.Empty;
                }
            }
        }
        private void FrmHotelAuthorization_Activated(object sender, EventArgs e)
        {
            this.btnOK.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.FreeAuthorization);
            this.btnCheckOut.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.FreeAuthorization);
            this.txtDays.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditFreeDays);
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
        }
        private void FrmHotelAuthorization_Deactivate(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }

        private void rabhours_CheckedChanged(object sender, EventArgs e)
        {
            txtDays_ValueChanged(null, null);
            //保存免费授权的时间单位到配置文件，0为天，1为小时，默认为天
            string temp = AppSettings.CurrentSetting.GetConfigContent("AuthorizationUnit");
            AppSettings.CurrentSetting.SaveConfig("AuthorizationUnit", this.rabhours.Checked ? "1" : "0");
        }

        private void rabdays_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

       










    }
}
