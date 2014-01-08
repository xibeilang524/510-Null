using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using Ralid.Park.BusinessModel.Model ;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Factory;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel .Interface ;
using Ralid.Park.ParkAdapter;
using Ralid.Park.UserControls.VideoPanels;
using Ralid.GeneralLibrary.Printer;
using Ralid.GeneralLibrary .CardReader ;
using Ralid.GeneralLibrary.LED;
using Ralid.GeneralLibrary.Speech;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI
{
    public partial class FrmCardPaying : Form, IReportHandler
    {
        public FrmCardPaying()
        {
            InitializeComponent();
        }

        #region 私有字段
        private CardEventReport _processingEvent;
        private BarCodeReader _TicketReader;
        private EpsonmodePrinter _BillPrinter;
        private IParkingLed _ChargeLed = null;
        private YangChengTongReader _YCTReader;
        private CardEventBll _CardEventBll = new CardEventBll(AppSettings.CurrentSetting.ParkConnect);
        private UCVideoListView _EnterVideoes;

        //这两个参数用于长隆转会员卡功能 bruce 2013-1-9
        private string _TempCardID;  //要转会员卡收费的临时卡卡号
        private string _VipCardID;   //通过刷卡获取的会员卡卡号
        private string _ToVipCard = "转会员卡";
        private string _VipCard = "会员卡";
        //end 

        private CardInfo _cardInfo;//当前读到的卡片，用于写卡模式

        //private string _VideoPath = Application.StartupPath + @"\FrmCardPaying_OpenedVideoes.xml";
        #endregion

        #region 私有方法
        private void ShowCardEventInfo(CardEventReport info)
        {
            this._EnterVideoes.Visible = false;
            this.picIn.Clear();
            this.picIn.Visible = true;
            if (info.IsExitEvent)
            {
                List<SnapShot> imgs = (new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).GetSnapShots(info.LastDateTime.Value, info.CardID);
                if (imgs != null && imgs.Count > 0)
                {
                    this.picIn.ShowSnapShots(imgs);
                }
            }

            try
            {
                //视频控件容易出问题，这里做一个异常处理
                ucVideoes.ShowVideoes(ParkBuffer.Current.GetEntrance(info.EntranceID).VideoSources);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }

            if (info.ChargeAsTempCard)
            {
                this.btnCash.Enabled = true;
                this.btnCardOk.Enabled = UserSetting.Current.OneKeyOpenDoor ? true : false;
                this.btnYCT.Enabled = (_YCTReader != null) ? true : false;
                this.btnInvalidEvent.Enabled = true;
                if (UserSetting.Current.OneKeyOpenDoor)
                {
                    this.btnCardOk.Focus();
                }
                else
                {
                    this.btnCash.Focus();
                }
            }
            else
            {
                this.btnCardOk.Enabled = true;
                this.btnInvalidEvent.Enabled = true;
                this.AcceptButton = this.btnCardOk;
                this.btnCardOk.Focus();
                this.btnCash.Enabled = false;
                this.btnYCT.Enabled = false;
            }

            if (info.CardPaymentInfo != null)
            {
                ShowCardChargeInfo(info.CardPaymentInfo);
            }
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).StopReadCard();
        }

        private void ClearCardEvent()
        {
            ClearCardChargeInfo();
            this.picIn.Clear();
            this.picIn.Visible = false;
            this._EnterVideoes.Visible = true;
            this.btnCardOk.Enabled = false;
            this.btnInvalidEvent.Enabled = false;
            this.btnCash.Enabled = false;
            this.btnYCT.Enabled = false;
            this.btnInvalidEvent.Enabled = false;
            this.AcceptButton = null;
            _processingEvent = null;
            _cardInfo = null;
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
        }

        private void CardPaidOk()
        {
            btnYCT.Enabled = false;
            btnCash.Enabled = false;
            btnCardOk.Enabled = true;
            btnCardOk.Focus();
            this.AcceptButton = btnCardOk;
        }

        private void ShowCardChargeInfo(CardPaymentInfo cardPayment)
        {
            this.txtCardID.Text = cardPayment.CardID;
            this.txtCardID.SelectAll();
            this.lblOwnerName.Text = cardPayment.OwnerName;
            this.lblCarNum.Text = cardPayment.CarPlate;
            this.lblEnterDateTime.Text = cardPayment.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblExitDateTime.Text = cardPayment.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblParkingTime.Text = cardPayment.TimeInterval;
            this.lblCardType.Text = cardPayment.CardType.ToString();
            this.lblTariffType.Text = Ralid.Park.BusinessModel.Resouce.TariffTypeDescription.GetDescription(cardPayment.TariffType);
            this.lblLastTotalPaid.Text = (cardPayment.LastTotalPaid + cardPayment.LastTotalDiscount).ToString();
            this.lblLastWorkstation.Text = cardPayment.LastStationID;
            this.lblAccounts.Text = cardPayment.Accounts.ToString();
            this.txtPaid.DecimalValue = cardPayment.Accounts - cardPayment.Discount;
            this.lblDiscount.Text = cardPayment.Discount.ToString();
            this.txtMemo.Text = string.Empty;

            this.carTypePanel1.SelectedCarType = cardPayment.CarType;

            string msg = string.Format(Resource1.FrmCardPaying_PayingSpeech, TariffSetting.Current.TariffOption.StrMoney(cardPayment.Accounts));
            if (_ChargeLed != null) _ChargeLed.DisplayMsg(msg);
            if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(msg);

            //长隆转会员卡功能
            if (cardPayment.CardID == _TempCardID && CarTypeSetting.Current.GetDescription(cardPayment.CarType) == _ToVipCard)
            {
                this.txtMemo.Text = _ToVipCard + _VipCardID;
            }
        }

        private void ClearCardChargeInfo()
        {
            this.txtCardID.Text = string.Empty;
            this.lblOwnerName.Text = string.Empty;
            this.lblCarNum.Text = string.Empty;
            this.lblEnterDateTime.Text = string.Empty;
            this.lblExitDateTime.Text = string.Empty;
            this.lblParkingTime.Text = string.Empty;
            this.lblCardType.Text = string.Empty;
            this.lblTariffType.Text = string.Empty;
            this.lblLastTotalPaid.Text = string.Empty;
            //this.lblLastTotalDiscount.Text = string.Empty;
            this.lblLastWorkstation.Text = string.Empty;
            this.lblAccounts.Text = string.Empty;
            this.txtPaid.DecimalValue = 0;
            this.lblDiscount.Text = string.Empty;
            this.txtMemo.Text = string.Empty;
        }

        private CommandResult SaveCardPayment(CardPaymentInfo cardPayment, PaymentMode paymentMode)
        {
            CommandResult result = null;
            cardPayment.Paid = txtPaid.DecimalValue;
            cardPayment.Discount = cardPayment.Accounts - cardPayment.Paid;
            cardPayment.Memo = txtMemo.Text;
            cardPayment.PaymentMode = paymentMode;
            cardPayment.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
            cardPayment.StationID = WorkStationInfo.CurrentStation.StationName;
            cardPayment.IsCenterCharge = false;
            CardBll cbll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            result = cbll.PayParkFee(_cardInfo, cardPayment);

            //写卡模式并且不是按在线模式处理时需要将收费信息写入卡片扇区
            if (AppSettings.CurrentSetting.EnableWriteCard 
                && result.Result == ResultCode.Successful 
                && !_processingEvent.OnlineHandleWhenOfflineMode)
            {
                _cardInfo.IsInPark = false;//标记已出场
                CardOperationManager.Instance.WriteCardLoop(_cardInfo);
            }

            return result;
        }

        private bool CheckPaid()
        {
            if (_processingEvent.CardPaymentInfo != null && this.txtPaid.DecimalValue > _processingEvent.CardPaymentInfo.Accounts)
            {
                if (MessageBox.Show(Resource1.FrmCardPaying_MoneyMuchQuery, Resource1.Form_Query, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }
            }
            else if (this.txtPaid.DecimalValue < 0)
            {
                if (MessageBox.Show(Resource1.FrmCardPaying_MoneyLittleQuery, Resource1.Form_Query, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        private bool btnCashHandle()
        {
            if (_processingEvent != null && CheckPaid())
            {
                CommandResult result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.Cash);
                if (result.Result == ResultCode.Successful)
                {
                    //用于打印收费小票打开钱箱收款
                    if (_BillPrinter != null)
                    {
                        ParkBillInfo bill = ParkBillFactory.CreateParkBill(_processingEvent.CardPaymentInfo);
                        _BillPrinter.PrintParkBill(bill);
                    }
                    CardPaidOk();
                    return true;
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
            return false;
        }
        #endregion

        #region ICardEventHandler 成员
        public void ProcessReport(ReportBase report)
        {
            Action<ReportBase> action = delegate(ReportBase report1)
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(report1.EntranceID);
                if (entrance != null && WorkStationInfo.CurrentStation.EntranceList.Exists(e => e == entrance.EntranceID))
                {
                    if (report1 is CardEventReport)
                    {
                        CardEventReport eventInfo = report1 as CardEventReport;
                        if (eventInfo.IsExitEvent)
                        {
                            if (eventInfo.EventStatus != CardEventStatus.Valid)
                            {
                                //写卡模式下需要事件卡号与当前读到的卡号一致
                                if (!AppSettings.CurrentSetting.EnableWriteCard || eventInfo.OnlineHandleWhenOfflineMode || (_cardInfo != null && _cardInfo.CardID == eventInfo.CardID))
                                {
                                    _processingEvent = eventInfo;
                                    ShowCardEventInfo(_processingEvent);
                                }
                            }
                            else
                            {
                                if (_cardInfo != null && _cardInfo.CardID == eventInfo.CardID && eventInfo.Reader == EntranceReader.DeskTopReader)
                                {
                                    //抬闸放行后，写卡模式并且不是按在线模式处理时需要将收费信息写入卡片扇区，
                                    if (AppSettings.CurrentSetting.EnableWriteCard && _cardInfo != null
                                        && _cardInfo.CardID == eventInfo.CardID
                                        && !eventInfo.OnlineHandleWhenOfflineMode
                                        && _cardInfo.IsInPark)
                                    {
                                        //只有当前卡片与只有待处理事件的卡号一致才会写卡,并且卡片在场时
                                        _cardInfo.IsInPark = false;//标记已出场
                                        if (_cardInfo.CardType.IsPrepayCard)//储值卡时需要将余额写入卡片
                                            _cardInfo.Balance = eventInfo.Balance;
                                        CardOperationManager.Instance.WriteCardLoop(_cardInfo);
                                    }
                                    _cardInfo = null;
                                }

                                if (_processingEvent != null && _processingEvent.EntranceID == report1.EntranceID)  //只有待处理事件与有效事件是同一个通道时才清空
                                {
                                    ClearCardEvent();
                                    _processingEvent = null;
                                }
                            }
                        }
                    }
                    else if ((report1 is CardInvalidEventReport) && _processingEvent != null && _processingEvent.EntranceID == report1.EntranceID) //只有待处理事件与无效事件是同一个通道时才清空
                    {

                        ClearCardEvent();
                        _processingEvent = null;
                    }
                    if (!(report1 is ParkVacantReport) && !(report1 is EntranceRemainTempCardReport) && !(report1 is EntranceStatusReport))
                    {
                        eventList.InsertReport(report1);
                    }
                }
            };

            if (this.InvokeRequired)
            {
                this.Invoke(action, report);
            }
            else
            {
                action(report);
            }
        }
        #endregion

        #region 事件处理程序
        private void FrmCardPaying_Load(object sender, EventArgs e)
        {
            _EnterVideoes = new UCVideoListView();
            _EnterVideoes.Dock = DockStyle.Fill;
            this.tableLayoutPanel2.Controls.Add(_EnterVideoes, 1, 0);
            this.picIn.Visible = false;
            this.carTypePanel1.Init();
            ClearCardEvent();

            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            if (AppSettings.CurrentSetting.TicketReaderCOMPort > 0)
            {
                _TicketReader = new BarCodeReader(AppSettings.CurrentSetting.TicketReaderCOMPort);
                _TicketReader.BarCodeRead += new BarCodeReadEventHandler(TicketReader_BarCodeRead);
                _TicketReader.Open();
            }
            if (AppSettings.CurrentSetting.BillPrinterCOMPort > 0)
            {
                _BillPrinter = new EpsonmodePrinter(AppSettings.CurrentSetting.BillPrinterCOMPort, 9600);
                _BillPrinter.Open();
            }
            if (AppSettings.CurrentSetting.ParkFeeLedCOMPort > 0)
            {
                if (AppSettings.CurrentSetting.ParkFeeLedType == 0)
                {
                    _ChargeLed = new ZhongKuangLed(AppSettings.CurrentSetting.ParkFeeLedCOMPort);
                }
                else
                {
                    _ChargeLed = new YanseDesktopLed(AppSettings.CurrentSetting.ParkFeeLedCOMPort);
                }
                _ChargeLed.Open();
                _ChargeLed.PermanentSentence = "出口收费处 ";
            }
            if (AppSettings.CurrentSetting.YCTReaderCOMPort > 0)
            {
                _YCTReader = new YangChengTongReader(AppSettings.CurrentSetting.YCTReaderCOMPort, 1);
                _YCTReader.Open();
            }

            // 启用一键开闸
            if (UserSetting.Current.OneKeyOpenDoor) this.pnlCash.Visible = false;

            this.txtMemo.Items.Clear();
            if (UserSetting.Current.PaymentComments != null && UserSetting.Current.PaymentComments.Count > 0)
            {
                foreach (string comment in UserSetting.Current.PaymentComments)
                {
                    this.txtMemo.Items.Add(comment);
                }
            }

            //从配置文件中获取收费栏的宽度
            string temp = AppSettings.CurrentSetting.GetConfigContent("PaymentPanelWidth");
            int intVal;
            if (int.TryParse(temp, out intVal) && intVal > 0)
            {
                this.paymentPanel.Width = intVal;
            }
            temp = AppSettings.CurrentSetting.GetConfigContent("VideoPanelHeight");
            if (int.TryParse(temp, out intVal) && intVal > 0)
            {
                this.videoPanel.Height = intVal;
            }

            this.splitter2.Visible = AppSettings.CurrentSetting.ShowAPMMonitor;
            this.ucapmMonitor1.Visible = AppSettings.CurrentSetting.ShowAPMMonitor;
            if (ucapmMonitor1.Visible)
            {
                ucapmMonitor1.Init();
            }
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            btnCashHandle();
        }

        private void btnYCT_Click(object sender, EventArgs e)
        {
            if (_processingEvent != null && CheckPaid())
            {
                CommandResult result = null;
                FrmYCTPayment frmYCT = new FrmYCTPayment();
                frmYCT.Reader = this._YCTReader;
                frmYCT.Payment = this.txtPaid.DecimalValue;
                if (frmYCT.ShowDialog() == DialogResult.OK)
                {
                    result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.YangChengTong);
                }
                if (result != null)
                {
                    if (result.Result == ResultCode.Successful)
                    {
                        //用于打印收费小票打开钱箱收款
                        if (_BillPrinter != null)
                        {
                            ParkBillInfo bill = ParkBillFactory.CreateParkBill(_processingEvent.CardPaymentInfo);
                            _BillPrinter.PrintParkBill(bill);
                        }
                        CardPaidOk();
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
            }
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            if (_processingEvent != null && CheckPaid())
            {
                CommandResult result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.Pos);
                if (result.Result == ResultCode.Successful)
                {
                    //用于打印收费小票打开钱箱收款
                    if (_BillPrinter != null)
                    {
                        ParkBillInfo bill = ParkBillFactory.CreateParkBill(_processingEvent.CardPaymentInfo);
                        _BillPrinter.PrintParkBill(bill);
                    }
                    CardPaidOk();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void btnCardOk_Click(object sender, EventArgs e)
        {
            if (_processingEvent != null)
            {
                if (_processingEvent.CardType.IsPrepayCard && txtPaid.DecimalValue <= _processingEvent.Balance) //储值卡且实收小于或等于余额，则只是扣除余额
                {
                    CommandResult result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.Prepay);
                    if (result.Result != ResultCode.Successful)
                    {
                        MessageBox.Show(result.Message);
                        return;
                    }
                }
                else if (_processingEvent.IsExitEvent && UserSetting.Current.OneKeyOpenDoor) //如果启用一键开闸，则按卡片有效按钮时先要保存卡片费用明细
                {
                    if (!btnCashHandle()) return;
                }
                EventValidNotify n = new EventValidNotify(_processingEvent.EntranceID, OperatorInfo.CurrentOperator, WorkStationInfo.CurrentStation.StationName, this.txtPaid.DecimalValue);
                if (ParkingAdapterManager.Instance[_processingEvent.ParkID] != null)
                {
                    ParkingAdapterManager.Instance[_processingEvent.ParkID].EventValid(n);
                }
                if (_cardInfo != null)
                {
                    CardInfo cardclone = _cardInfo.Clone();
                    ClearCardEvent();
                    _cardInfo = cardclone;
                }
                else
                {
                    ClearCardEvent();
                }
            }
        }

        private void ChargeType_Selected(object sender, EventArgs e)
        {
            if (_processingEvent != null && CarTypeSetting.Current[carTypePanel1.SelectedCarType] != null)
            {
                int entranceID = _processingEvent.EntranceID;
                int parkID = _processingEvent.ParkID;

                //专门针对长隆停车场设置一种“转会员卡"车型，选择“转会员卡"车型时必须重新刷一张会员卡才能时行这个操作，并在收费说明中写入“转会员卡+会员卡号"
                //这个功能采用硬编码
                if (CarTypeSetting.Current[carTypePanel1.SelectedCarType].Description == _ToVipCard)
                {
                    FrmVipCardReader frm = new FrmVipCardReader();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        string cardid = frm.VipCardID;
                        CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(cardid).QueryObject;
                        if (card != null && card.CardType.Name == _VipCard)
                        {
                            _TempCardID = _processingEvent.CardID;
                            _VipCardID = cardid;
                        }
                        else
                        {
                            MessageBox.Show("卡号为 {0} 的卡不存在或者不是会员卡，请刷会员卡", Resource1.Form_Alert);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //end 转会员卡功能 

                //选择车型后，表明当前的收费明细就是没有用的了，要不然用户不会重新选择车型，这也避免了用户在选择车型后立即按了收费按钮，此时由于选择车型的操作
                //是异步的，所以会先处理收费事件，保存收费信息，但新车型的事件上来后用户又会选择一次收费，所以导致偶尔会有同一事件有两条收费记录的情况。
                _processingEvent = null;
                CarTypeSwitchNotify notify = new CarTypeSwitchNotify(entranceID, carTypePanel1.SelectedCarType);
                if (ParkingAdapterManager.Instance[parkID] != null)
                {
                    ParkingAdapterManager.Instance[parkID].SwitchCarType(notify);
                }
            }
        }

        private void btnInvalidEvent_Click(object sender, EventArgs e)
        {
            if (_processingEvent != null)
            {
                EventInvalidNotify notify = new EventInvalidNotify();
                notify.CardEvent = _processingEvent;
                notify.OperatorNum = OperatorInfo.CurrentOperator.OperatorNum;
                notify.InvalidType = EventInvalidType.INV_InvalidImg;
                if (ParkingAdapterManager.Instance[_processingEvent.ParkID] != null)
                {
                    ParkingAdapterManager.Instance[_processingEvent.ParkID].EventInvalid(notify);
                }
                ClearCardEvent();
            }
        }

        private void FrmCardPaying_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                case Keys.D0:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(0);
                    break;
                case Keys.NumPad1:
                case Keys.D1:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(1);
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(2);
                    break;
                case Keys.NumPad3:
                case Keys.D3:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(3);
                    break;
                case Keys.NumPad4:
                case Keys.D4:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(4);
                    break;
                case Keys.NumPad5:
                case Keys.D5:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(5);
                    break;
                case Keys.NumPad6:
                case Keys.D6:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(6);
                    break;
                case Keys.NumPad7:
                case Keys.D7:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(7);
                    break;
                case Keys.NumPad8:
                case Keys.D8:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(8);
                    break;
                case Keys.NumPad9:
                case Keys.D9:
                    if (this.carTypePanel1.Visible) this.carTypePanel1.Select(9);
                    break;
                case Keys.F9:
                    if (this.btnCash.Enabled)
                    {
                        btnCash_Click(this.btnCash, EventArgs.Empty);
                    }
                    break;
                case Keys.F10:
                    if (this.btnYCT.Enabled)
                    {
                        btnYCT_Click(this.btnYCT, EventArgs.Empty);
                    }
                    break;
                case Keys.F11:
                    //if (this.btnPos.Enabled)
                    //{
                    //    btnPos_Click(this.btnCardOk, EventArgs.Empty);
                    //}
                    break;
                case Keys.F12:
                    if (this.btnCardOk.Enabled)
                    {
                        btnCardOk_Click(this.btnCardOk, EventArgs.Empty);
                    }
                    break;
            }
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                //当前读到的卡片
                _cardInfo = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e.ParkingDate);
            }
            foreach (int enID in WorkStationInfo.CurrentStation.EntranceList)
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(enID);
                if (entrance != null && entrance.IsExitDevice)
                {
                    RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, e.CardID, e.ParkingDate);
                    if (_cardInfo != null) notify.LastCarPlate = _cardInfo.LastCarPlate;
                    IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.ParkID];
                    if (pad != null)
                    {
                        pad.RemoteReadCard(notify);
                        break;
                    }
                }
            }
        }

        private void TicketReader_BarCodeRead(object sender, BarCodeReadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.BarCode))
            {
                foreach (int enID in WorkStationInfo.CurrentStation.EntranceList)
                {
                    EntranceInfo entrance = ParkBuffer.Current.GetEntrance(enID);
                    if (entrance != null && entrance.IsExitDevice)
                    {
                        RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, e.BarCode, new byte[0]);
                        IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.ParkID];
                        if (pad != null)
                        {
                            pad.RemoteReadCard(notify);
                            break;
                        }
                    }
                }
            }
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).SelectAll();
            }
            this.KeyPreview = false;

            if (sender == this.txtMemo && AppSettings.CurrentSetting.EnlargeMemo) //如果光标进入优惠说明栏，把优惠说明栏字体变大，并增加高度，
            {
                this.txtMemo.Font = new Font("宋体", 20, FontStyle.Regular, GraphicsUnit.Pixel);
                this.txtMemo.Height = this.txtPaid.Height;
            }
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            if (sender == this.txtMemo) //如果光标退出优惠说明栏，把优惠说明栏字体和高度变成正常大小，
            {
                this.txtMemo.Font = this.txtCardID.Font;
                this.txtMemo.Height = this.txtCardID.Height;
                this.txtMemo.ImeMode = ImeMode.Off;
            }
        }

        private void txtPaid_TextChanged(object sender, EventArgs e)
        {
            if (_processingEvent != null)
            {
                lblDiscount.Text = (_processingEvent.CardPaymentInfo.Accounts - txtPaid.DecimalValue).ToString();
                if (_processingEvent.CardType.IsPrepayCard)
                {
                    btnCash.Enabled = _processingEvent.Balance < txtPaid.DecimalValue;
                    btnYCT.Enabled = (_processingEvent.Balance < txtPaid.DecimalValue && _YCTReader != null);
                    btnCardOk.Enabled = _processingEvent.Balance >= txtPaid.DecimalValue;
                }
            }
        }

        private void FrmCardPaying_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppSettings.CurrentSetting.SaveConfig("PaymentPanelWidth", paymentPanel.Width.ToString());
            AppSettings.CurrentSetting.SaveConfig("VideoPanelHeight", videoPanel.Height.ToString());
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            if (_TicketReader != null) _TicketReader.Close();
            if (_BillPrinter != null) _BillPrinter.Close();
            if (_YCTReader != null) _YCTReader.Close();
            if (_ChargeLed != null) _ChargeLed.Close();
            this.ucVideoes.Clear();
            this._EnterVideoes.Clear();
        }

        private void pnlCash_Resize(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(btnCash);
            buttons.Add(btnYCT);
            LayoutCarTypeButtons(pnlCash,buttons, btnCash.Height, 3);
        }

        private void LayoutCarTypeButtons(Panel container, List<Button> buttons, int buttonHeight, int buttonMargin)
        {
            int buttongWidth = (container.Width - buttonMargin * 3) / 2;  //根据整个控件的大小来确定按钮的大小
            int totalHeight = buttonMargin;

            Point p = new Point(buttonMargin, buttonMargin);
            for (int i = 0; i < buttons.Count; i++)
            {
                int rows = (i / 2) + 1;  //表示目前的行数
                if (i % 2 == 0) //换行
                {
                    p = new Point(buttonMargin, buttonMargin + (buttonHeight + buttonMargin) * (rows - 1));  //新行的起始位置
                    totalHeight += buttonHeight + buttonMargin;
                }
                else   //不换行在第二列
                {
                    p = new Point(buttonMargin + buttongWidth + buttonMargin, buttonMargin + (buttonHeight + buttonMargin) * (rows - 1));  //两个控件之间间隔三个空隙
                }
                buttons[i].Location = p;
                buttons[i].Size = new Size(buttongWidth, buttonHeight);
            }
            container.Size = new Size(container.Width, totalHeight);
        }

        private void panel6_Resize(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(btnCardOk);
            buttons.Add(btnInvalidEvent);
            LayoutCarTypeButtons(panel6,buttons, btnCardOk.Height, 3);
        }
        #endregion
    }
}
