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

        #region 事件处理
        private void CardEventDetail_Load(object sender, EventArgs e)
        {
        }

        private void ShowCardEventDetail(CardEventReport info)
        {
            CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(info.CardID).QueryObject;
            if (card != null)
            {
                this.txtCardID.Text = card.CardID;
                this.lblOwnerName.Text = card.OwnerName;
                this.lblCarNum.Text = card.CarPlate;
                this.lblValidDate.Text = card.ValidDate.ToString("yyyy-MM-dd");
            }
            if (info.LastDateTime != null)
            {
                this.lblEnterDateTime.Text = info.LastDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.lblExitDateTime.Text = info.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblParkingTime.Text = (info.CardPaymentInfo != null) ? info.CardPaymentInfo.TimeInterval : string.Empty;
            this.lblCardType.Text = info.CardType.ToString();
            this.lblTariffType.Text = info.CardPaymentInfo != null ? Ralid.Park.BusinessModel.Resouce.TariffTypeDescription.GetDescription(info.CardPaymentInfo.TariffType) : string.Empty;
            this.lblChargingMoneyPlan.Text = (info.CardPaymentInfo != null ? info.CardPaymentInfo.Accounts : 0).ToString();
            this.lblBalance.Text = info.Balance.ToString();
            this.txtChargedMoney.DecimalValue = info.CardPaymentInfo != null ? info.CardPaymentInfo.Accounts : 0;

            this.picIn.Clear();
            if (info.LastDateTime != null)
            {
                List<SnapShot> imgs = (new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).GetSnapShots(info.LastDateTime.Value,info.CardID);
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

