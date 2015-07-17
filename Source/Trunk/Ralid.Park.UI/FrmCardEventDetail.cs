using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BLL;
using Ralid.Park.UserControls;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.ParkAdapter;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.UI.EventArgument;

namespace Ralid.Park.UI
{
    public partial class FrmCardEventDetail : Form
    {
        #region 构造函数
        public FrmCardEventDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private CardEventReport _ProcessingEvent;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置要处理的事件
        /// </summary>
        public CardEventReport ProcessingEvent
        {
            get
            {
                return _ProcessingEvent;
            }
            set
            {
                _ProcessingEvent = value;
                if (value != null)
                {
                    ShowCardEventDetail(value);
                }
            }
        }
        #endregion

        #region 事件
        public event CardEventProcessedHandler CardEventProcessed;
        #endregion

        #region 私有方法
        private void ClearEventDetail()
        {
            this.txtCardID.Text = string.Empty;
            this.lblOwnerName.Text = string.Empty;
            this.lblCardCarplate.Text = string.Empty;
            this.lblEnterDateTime.Text = string.Empty;
            this.lblExitDateTime.Text = string.Empty;
            this.lblParkingTime.Text = string.Empty;
            this.lblCardType.Text = string.Empty;
            this.lblValidDate.Text = string.Empty;
            this.lblTariffType.Text = string.Empty;
            this.lblBalance.Text = string.Empty;
            this.lblChargingMoneyPlan.Text = string.Empty;
            this.lblCardCarplate.Text = string.Empty;
            this.txtChargedMoney.Text = "0.00";
        }

        private CommandResult SaveCardPayment(CardPaymentInfo cardPayment, PaymentMode paymentMode)
        {
            CommandResult result = null;
            cardPayment.PaymentMode = paymentMode;
            cardPayment.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
            cardPayment.StationID = WorkStationInfo.CurrentStation.StationName;
            cardPayment.IsCenterCharge = true;

            CardBll cbll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);

            result = cbll.PayParkFee(null, cardPayment, AppSettings.CurrentSetting.CurrentStandbyConnect, false, false);
            
            return result;
        }
        #endregion

        #region 事件处理
        private void CardEventDetail_Load(object sender, EventArgs e)
        {
            this.txtChargedMoney.ReadOnly = true;
        }

        private void ShowCardEventDetail(CardEventReport info)
        {
            ClearEventDetail();

            this.txtCardID.Text = info.CardID;
            this.lblOwnerName.Text = info.OwnerName;
            this.lblCardCarplate.Text = info.IsExitEvent ? info.LastCarPlate : info.CarPlate;//入口事件中，为识别到的车牌号
            this.lblValidDate.Text = info.ValidDate.ToString("yyyy-MM-dd");

            if (info.IsExitEvent)
            {
                if (info.LastDateTime != null)
                {
                    this.lblEnterDateTime.Text = info.LastDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                this.lblExitDateTime.Text = info.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.lblParkingTime.Text = (info.CardPaymentInfo != null) ? info.CardPaymentInfo.TimeInterval : string.Empty;
            this.lblCardType.Text = info.CardType.ToString();
            this.lblTariffType.Text = info.CardPaymentInfo != null ? Ralid.Park.BusinessModel.Resouce.TariffTypeDescription.GetDescription(info.CardPaymentInfo.TariffType) : string.Empty;
            this.lblChargingMoneyPlan.Text = (info.CardPaymentInfo != null ? info.CardPaymentInfo.Accounts : 0).ToString();
            if (info.IsOnlineHandleEvent)
            {
                this.lblBalance.Text = info.Balance.ToString();
            }
            this.txtChargedMoney.DecimalValue = info.CardPaymentInfo != null ? info.CardPaymentInfo.Accounts : 0;
            this.lblCardCarplate.Text = info.CardCarPlate;
            this.picIn.Clear();
            DateTime? shotDateTime = info.IsExitEvent ? info.LastDateTime : info.EventDateTime;//入口时间时，返回刷卡事件时抓拍的图片
            if (shotDateTime != null)
            {
                List<SnapShot> imgs  = (new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr)).GetSnapShots(shotDateTime.Value, info.CardID);
                if (imgs != null && imgs.Count > 0)
                {
                    this.picIn.ShowSnapShots(imgs);
                }
            }
            ucVideoes.ShowVideoes(ParkBuffer.Current.GetEntrance(info.EntranceID).VideoSources);
        }

        private void btnCardOk_Click(object sender, EventArgs e)
        {
            if (ProcessingEvent != null)
            {
                if (ProcessingEvent.PrepayCardExitNeedPay)
                {
                    //是储值卡出口事件时，需要保存扣费记录
                    //因为只有在线模式的储值卡确认事件才需要扣费，所以只需要在在线模式储值卡扣费处理就可以了
                    CommandResult result = SaveCardPayment(ProcessingEvent.CardPaymentInfo, PaymentMode.Prepay);
                    if (result.Result != ResultCode.Successful)
                    {
                        MessageBox.Show(result.Message);
                        return;
                    }
                }

                EventValidNotify n = new EventValidNotify(ProcessingEvent.EntranceID, OperatorInfo.CurrentOperator, WorkStationInfo.CurrentStation.StationName, this.txtChargedMoney.DecimalValue);
                if (ParkingAdapterManager.Instance[ProcessingEvent.ParkID] != null && ParkingAdapterManager.Instance[ProcessingEvent.ParkID].EventValid(n))
                {
                    if (this.CardEventProcessed != null)
                    {
                        this.CardEventProcessed(this, new CardEventProcessedArgs(ProcessingEvent, 0));
                    }
                    this.ucVideoes.Clear();
                    this.Hide();
                }
            }
        }

        private void btnInvalidEvent_Click(object sender, EventArgs e)
        {
            if (ProcessingEvent != null)
            {
                EventInvalidNotify notify = new EventInvalidNotify();
                notify.CardEvent = ProcessingEvent;
                notify.OperatorNum = OperatorInfo.CurrentOperator.OperatorNum;
                notify.InvalidType = EventInvalidType.INV_Invalid;
                if (ParkingAdapterManager.Instance[ProcessingEvent.ParkID] != null)
                {
                    ParkingAdapterManager.Instance[ProcessingEvent.ParkID].EventInvalid(notify);
                }
                if (this.CardEventProcessed != null)
                {
                    this.CardEventProcessed(this, new CardEventProcessedArgs(ProcessingEvent, 1));
                }
                this.ucVideoes.Clear();
                this.Hide();
            }
        }

        
        private void FrmCardEventDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11:
                    if (this.btnCardOk.Enabled)
                    {
                        btnCardOk_Click(this.btnCardOk, EventArgs.Empty);
                    }
                    break;
                case Keys.F12:
                    if (this.btnInvalidEvent.Enabled)
                    {
                        btnInvalidEvent_Click(this.btnInvalidEvent, EventArgs.Empty);
                    }
                    break;
            }
        }
        
        private void FrmCardEventDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        #endregion

    }
}

