using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
using Ralid.GeneralLibrary.CardReader.YCT;
using Ralid.GeneralLibrary.LED;
using Ralid.GeneralLibrary.Speech;
using Ralid.Park.UI.Resources;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;

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
        private YCTPOS _YCTPOS;  //2015-8-18 bruce
        private System.Threading.Timer _TmrYCT = null;
        private CardEventBll _CardEventBll = new CardEventBll(AppSettings.CurrentSetting.ParkConnect);
        private UCVideoListView _EnterVideoes;

        //这两个参数用于长隆转会员卡、转员工卡功能 bruce 2013-1-9 Jan 2014-5-6 增加转员工卡
        private string _TempCardID;  //要转会员卡、转员工卡收费的临时卡卡号
        private string _VipCardID;   //通过刷卡获取的会员卡或员工卡卡号
        private string _ToVipCard = "转会员卡";
        private string _VipCard = "会员卡";
        private string _ToStaffCard = "转员工卡";
        private string _StaffCard = "员工卡";
        //end 

        private CardInfo _cardInfo;//当前读到的卡片，用于写卡模式

        private string _OperatorCardID;//授权卡卡号
        private string _OperatorOwnerName;//授权卡持卡人

        private FrmSpeedingDetail _frmSpeedingDetail;//超速违章详细信息窗口

        private string _LastOpenCardID = string.Empty;//开发卡片读卡器最后一次读到的卡号
        #endregion

        #region 私有方法
        private void ShowCardEventInfo(CardEventReport info)
        {
            //如果打开了超速违章详细信息窗口，先关闭窗口
            if (this._frmSpeedingDetail != null && this._frmSpeedingDetail.ProcessingEvent != null)
            {
                this._frmSpeedingDetail.CloseSpeedingDetail();
            }

            this._EnterVideoes.Visible = false;
            this.picIn.Clear();
            this.picIn.Visible = true;
            if (info.IsExitEvent)
            {
                List<SnapShot> imgs = (new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr)).GetSnapShots(info.LastDateTime.Value, info.CardID);
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
                if (info.CardType.Name.Contains("中山通") &&
                AppSettings.CurrentSetting.EnableZST && !string.IsNullOrEmpty(AppSettings.CurrentSetting.ZSTReaderIP))
                {
                    this.btnYCT.Text = "中山通[&F10]";
                    this.btnYCT.Enabled = true;
                }
                else
                {
                    this.btnYCT.Enabled = (_YCTReader != null || _YCTPOS != null);
                }
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

            this.entrancePanel1.SelectedEntranceID = info.EntranceID;
            //modify by Jan 2014-07-17 只有桌面读卡器读卡才能更改通道
            this.entrancePanel1.Enabled = info.Reader == EntranceReader.DeskTopReader;

            if (info.CardPaymentInfo != null)
            {
                ShowCardChargeInfo(info.CardPaymentInfo);
            }
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).StopReadCard();
            if (_TmrYCT != null) _TmrYCT.Change(3000, System.Threading.Timeout.Infinite); //停止读卡
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
            this.entrancePanel1.Enabled = true;
            this.AcceptButton = null;
            _processingEvent = null;
            _cardInfo = null;
            this.lblDiscountHour.Text = string.Empty;
            this.txtCardID.Focus();
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
            if (_YCTPOS != null || _YCTReader != null) _TmrYCT.Change(3000, 500); //重新恢复读卡
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
            this.lblCertificate.Text = cardPayment.CardCertificate;
            this.lblOwnerName.Text = cardPayment.OwnerName;
            this.lblCarNum.Text = cardPayment.CarPlate;
            this.lblEnterDateTime.Text = cardPayment.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblExitDateTime.Text = cardPayment.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblParkingTime.Text = cardPayment.TimeInterval;
            this.lblCardType.Text = cardPayment.CardType.ToString();
            this.lblTariffType.Text = Ralid.Park.BusinessModel.Resouce.TariffTypeDescription.GetDescription(cardPayment.TariffType);
            this.lblLastTotalPaid.Text = (cardPayment.LastTotalPaid + cardPayment.LastTotalDiscount).ToString();
            this.lblLastWorkstation.Text = cardPayment.LastStationID;
            this.lblAccounts.Text = cardPayment.Accounts.ToString("N");
            this.txtPaid.DecimalValue = cardPayment.Accounts - cardPayment.Discount;
            this.lblDiscount.Text = cardPayment.Discount.ToString("N");
            this.lblDiscountHour.Text = cardPayment.CurrDiscountHour.HasValue ? cardPayment.CurrDiscountHour.Value.ToString() : "0";
            //this.txtMemo.Text = string.IsNullOrEmpty(cardPayment.Memo) ? string.Empty : cardPayment.Memo;
            this.lblDiscountMemo.Text = string.IsNullOrEmpty(cardPayment.Memo) ? string.Empty : cardPayment.Memo;

            this.carTypePanel1.SelectedCarType = cardPayment.CarType;

            //decimal paid = cardPayment.Accounts;
            decimal paid = this.txtPaid.DecimalValue;
            string msg = string.Format(Resource1.FrmCardPaying_PayingSpeech, TariffSetting.Current.TariffOption.StrMoney(paid) + TariffSetting.Current.TariffOption.GetMoneyUnit());
            if (_ChargeLed != null) _ChargeLed.DisplayMsg(msg);
            if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(msg);

            //长隆转会员卡、转员工卡功能
            if (cardPayment.CardID == _TempCardID)
            {
                if (CarTypeSetting.Current.GetDescription(cardPayment.CarType) == _ToVipCard)
                {
                    this.txtMemo.Text = _ToVipCard + _VipCardID;
                }
                else if (CarTypeSetting.Current.GetDescription(cardPayment.CarType) == _ToStaffCard)
                {
                    this.txtMemo.Text = _ToStaffCard + _VipCardID;
                }
            }

            if (!string.IsNullOrEmpty(_OperatorCardID) && CarTypeSetting.Current.GetDescription(cardPayment.CarType) == "特种车")
            {
                this.txtMemo.Text = "授权卡" + _OperatorCardID + "转换特种车";
            }
        }

        private void ClearCardChargeInfo()
        {
            this.txtCardID.Text = string.Empty;
            this.lblCertificate.Text = string.Empty;
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
            this.lblDiscountMemo.Text = string.Empty;
            this.txtMemo.Text = string.Empty;
        }

        /// <summary>
        /// 缴费信息回滚
        /// </summary>
        /// <param name="ldb_cbll">本地数据库连接</param>
        /// <param name="info">缴费前的卡片信息</param>
        /// <param name="record">收费记录</param>
        private void PaymentRollback(LDB_CardPaymentRecordBll ldb_cbll, CardInfo info, CardPaymentInfo record)
        {
            if (info != null)
            {
                _cardInfo = info;
                //当本地数据库连接不为空时，说明主数据库和备用数据库都写失败了
                if (ldb_cbll != null)
                {
                    LDB_CardPaymentInfo ldbRecord = LDB_InfoFactory.CreateLDBCardPaymentInfo(record);
                    //这里使用DeleteCardPayment是因为sqlite插入记录后不会自动返回自增的主键，所有需要通过卡号和缴费时间查询来删除
                    ldb_cbll.DeleteCardPayment(record.CardID, record.ChargeDateTime);
                }
                else
                {
                    CardBll cbll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
                    if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect))
                    {
                        //这里使用收费时间来回滚，主要是因为不清楚record的主键ID是主数据库的还是备用数据库的
                        cbll.RollbackPayment(info, record.ChargeDateTime);
                        if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.CurrentStandbyConnect))
                        {
                            CardBll standby = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                            standby.RollbackPayment(info, record.ChargeDateTime);
                        }
                    }
                    else
                    {
                        cbll.RollbackPayment(info, record);
                    }
                }
            }
        }

        private CommandResult SaveCardPayment(CardPaymentInfo cardPayment, PaymentMode paymentMode)
        {
            CommandResult result = null;
            cardPayment.Paid = txtPaid.DecimalValue;

            //优惠金额
            decimal discount = cardPayment.Accounts - cardPayment.Paid;//总优惠
            string artificialMemo = string.Empty;
            if (discount != cardPayment.Discount)
            {
                //总优惠与电子优惠不相同时，记录人工优惠
                decimal artificialDiscount = discount - cardPayment.Discount;
                artificialMemo = string.Format(Resource1.FrmCardPaying_ArtificialDiscount, artificialDiscount);

                cardPayment.Discount = discount;
            }
            //cardPayment.Discount = cardPayment.Accounts - cardPayment.Paid;

            cardPayment.Memo = this.lblDiscountMemo.Text + artificialMemo + this.txtMemo.Text;
            cardPayment.PaymentMode = paymentMode;
            cardPayment.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
            cardPayment.OperatorDeptID = OperatorInfo.CurrentOperator.DeptID;
            cardPayment.StationID = WorkStationInfo.CurrentStation.StationName;
            cardPayment.StationDeptID = WorkStationInfo.CurrentStation.DeptID;
            cardPayment.IsCenterCharge = false;

            LDB_CardPaymentRecordBll ldb_cbll = null;
            CardBll cbll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            bool both = WorkStationInfo.CurrentStation.NeedBothDatabaseUpdate;
            bool offlineHandleCard = AppSettings.CurrentSetting.EnableWriteCard
                && _cardInfo != null
                && !_processingEvent.OnlineHandleWhenOfflineMode;
            //&& (paymentMode == PaymentMode.Cash || paymentMode == PaymentMode.Prepay);2014-12-11 注销 写卡与收费模式无关

            CardInfo payBefore = _cardInfo == null ? null : _cardInfo.Clone();
            result = cbll.PayParkFee(_cardInfo, cardPayment, AppSettings.CurrentSetting.CurrentStandbyConnect, both, offlineHandleCard);

            if (result.Result != ResultCode.Successful && offlineHandleCard)
            {
                //与主数据库通信故障时，脱机模式时按脱机模式处理的卡片，收费信息写入本地数据库，待通信正在时，上传到主数据库
                _cardInfo.UpdateFlag = false;
                ldb_cbll = new LDB_CardPaymentRecordBll(LDB_AppSettings.Current.LDBConnect);
                result = ldb_cbll.PayParkFee(_cardInfo, cardPayment);
            }

            //写卡模式并且不是按在线模式处理时需要将收费信息写入卡片扇区
            if (result.Result == ResultCode.Successful && offlineHandleCard)
            {
                _cardInfo.SetEventReportData(_processingEvent);
                _cardInfo.IsInPark = false;//标记已出场
                if (CardOperationManager.Instance.WriteCardLoop(_cardInfo) != CardOperationResultCode.Success)
                {
                    result = new CommandResult(ResultCode.Fail);
                    PaymentRollback(ldb_cbll, payBefore, cardPayment);
                }
            }

            if (result.Result == ResultCode.Successful)
            {
                //保存转换特种车授权操作警报
                if (!string.IsNullOrEmpty(_OperatorCardID) && CarTypeSetting.Current.GetDescription(cardPayment.CarType) == "特种车")
                {
                    AlarmInfo alarm = new AlarmInfo()
                    {
                        AlarmDateTime = _processingEvent.EventDateTime,
                        AlarmSource = _processingEvent.SourceName,
                        AlarmType = AlarmType.OperatorCardWork,
                        AlarmDescr = string.Format("收费卡：{0}，进场时间：{1}，转换特种车，授权卡{2}操作。", _processingEvent.CardID, _processingEvent.LastDateTime, _OperatorCardID),
                        OperatorID = _OperatorOwnerName,
                    };
                    (new AlarmBll(AppSettings.CurrentSetting.ParkConnect)).Insert(alarm);
                }
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
            if (_processingEvent != null && CheckPaid() && CheckWriteCard())
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

        private bool CheckWriteCard()
        {
            //写卡模式并且不是按在线模式处理时需要检查卡片是否在读卡区域
            if (AppSettings.CurrentSetting.EnableWriteCard
                && _cardInfo != null
                && _processingEvent != null
                && !_processingEvent.OnlineHandleWhenOfflineMode)
            {
                //需要检查收费金额是否有效
                if (this.txtPaid.DecimalValue > 167772.15M)
                {
                    MessageBox.Show(Resources.Resource1.UcCard_PaidOver);
                    return false;
                }
                return CardOperationManager.Instance.CheckCardWithMessage(_cardInfo.CardID, false, true);
            }

            return true;
        }

        private string GetCardIDFromBarCode(string barcode)
        {
            if (!string.IsNullOrEmpty(barcode))
            {
                if (barcode.Length == 7)
                {
                    return barcode;
                }
                if (barcode.Length == 8)
                {
                    string ck = Ralid.GeneralLibrary.ITFCheckCreater.Create(barcode.Substring(0, barcode.Length - 1));
                    if (!string.IsNullOrEmpty(ck) && ck == barcode.Substring(barcode.Length - 1, 1))
                    {
                        return barcode.Substring(0, barcode.Length - 1);
                    }
                }
            }
            return string.Empty;
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
                                //if (!AppSettings.CurrentSetting.EnableWriteCard 
                                //    || eventInfo.OnlineHandleWhenOfflineMode 
                                //    || (_cardInfo != null && _cardInfo.CardID == eventInfo.CardID)
                                //    || eventInfo.EventStatus == CardEventStatus.CarPlateFail)//脱机模式的车牌识别确认事件
                                if (AppSettings.CurrentSetting.EnableWriteCard
                                    && _cardInfo != null
                                    && _cardInfo.CardID != eventInfo.CardID)
                                {
                                    //当前读到卡片与事件卡片不一致
                                }
                                else
                                {
                                    //modify by Jan 2014-07-17
                                    //当前如果正在处理事件，并且读卡事件与当前处理事件不是同一通道，忽略读卡事件
                                    if (_processingEvent == null
                                        || _processingEvent.EntranceID == eventInfo.EntranceID)
                                    {
                                        _processingEvent = eventInfo;
                                        ShowCardEventInfo(_processingEvent);
                                    }
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
                                        _cardInfo.SetEventReportData(eventInfo);
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
                        if (report1 is CardInvalidEventReport)
                        {
                            CardInvalidEventReport ciereport = report1 as CardInvalidEventReport;
                            //如果是超速行驶违章，弹出超速行驶违章详细信息
                            if (ciereport.InvalidType == EventInvalidType.INV_Speeding)
                            {
                                if (_frmSpeedingDetail == null)
                                {
                                    _frmSpeedingDetail = new FrmSpeedingDetail();
                                }
                                _frmSpeedingDetail.ProcessingEvent = ciereport;
                                _frmSpeedingDetail.Show();
                                _frmSpeedingDetail.Activate();
                            }
                        }
                    }
                }
            };

            if (this.InvokeRequired)
            {
                this.BeginInvoke(action, report);
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
            this.entrancePanel1.Init();
            this.entrancePanel1.Visible = AppSettings.CurrentSetting.SwitchEntrance;
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
                if (AppSettings.CurrentSetting.ParkFeeLedType == 1)
                {
                    _ChargeLed = new YanseDesktopLed(AppSettings.CurrentSetting.ParkFeeLedCOMPort);
                }
                else if (AppSettings.CurrentSetting.ParkFeeLedType == 2)
                {
                    _ChargeLed = new HSDDesktopLed(AppSettings.CurrentSetting.ParkFeeLedCOMPort);
                }
                else
                {
                    _ChargeLed = new ZhongKuangLed(AppSettings.CurrentSetting.ParkFeeLedCOMPort);
                }
                _ChargeLed.Open();
                _ChargeLed.PermanentSentence = "出口收费处 ";
                _ChargeLed.DisplayMsg("出口收费处 ", int.MaxValue);
            }
            if (AppSettings.CurrentSetting.YCTReaderCOMPort > 0)
            {
                if (UserSetting.Current.YCTReadType == 0)
                {
                    _YCTPOS = new YCTPOS(AppSettings.CurrentSetting.YCTReaderCOMPort, 57600);
                    _YCTPOS.Open();
                    if (_YCTPOS.IsOpened) _YCTPOS.SetServiceCode(UserSetting.Current.YCTServiceCode);
                }
                else if (UserSetting.Current.YCTReadType == 1)
                {
                    _YCTReader = new YangChengTongReader(AppSettings.CurrentSetting.YCTReaderCOMPort, 1);
                    _YCTReader.Open();
                }
                if (_YCTPOS != null || _YCTReader != null) _TmrYCT = new System.Threading.Timer(tmr_YCT_Tick, null, 3000, 500);
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

            //是否显示POS收费按钮
            if (!AppSettings.CurrentSetting.EnablePOSButton)
            {
                this.btnPos.Visible = false;
            }

            if (UserSetting.Current.ParkingCoupon == null || UserSetting.Current.ParkingCoupon.Count == 0)
            {
                this.btnCoupon.Visible = false;
            }
            //重新排列按钮
            pnlCash_Resize(this, EventArgs.Empty);
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
                if (btnYCT.Text.Contains("中山通"))
                {
                    FrmZSTPayment frmZST = new FrmZSTPayment();
                    frmZST.Payment = this.txtPaid.DecimalValue;
                    if (frmZST.ShowDialog() == DialogResult.OK)
                    {
                        result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.ZhongShanTong);
                    }
                }
                else
                {
                    if (_YCTReader != null)
                    {
                        FrmYCTPayment frmYCT = new FrmYCTPayment();
                        frmYCT.Reader = this._YCTReader;
                        frmYCT.Payment = this.txtPaid.DecimalValue;
                        if (frmYCT.ShowDialog() == DialogResult.OK)
                        {
                            if (this._YCTReader.LastCard != null)
                            {
                                this._LastOpenCardID = this._YCTReader.LastCard.CardID;
                            }
                            result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.YangChengTong);
                        }
                    }
                    else if (_YCTPOS != null)
                    {
                        FrmYCTPOSPayment frmYCT = new FrmYCTPOSPayment();
                        frmYCT.Reader = _YCTPOS;
                        frmYCT.Payment = this.txtPaid.DecimalValue;
                        frmYCT.ChargeLed = _ChargeLed;
                        if (frmYCT.ShowDialog() == DialogResult.OK)
                        {
                            //如果羊城通扣费成功，开发读卡器最后一次读到的卡号为扣费的羊城通卡号
                            //这是为了防止当IC卡缴费使用羊城通缴费时，羊城通扣费成功后，马上又读到了扣费的羊城通，进入该羊城通的停车场缴费处理流程
                            if (this._YCTPOS.LastWallet != null)
                            {
                                this._LastOpenCardID = this._YCTPOS.LastWallet.LogicCardID;
                            }
                            result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.YangChengTong);
                        }
                    }
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
            if (_processingEvent != null && CheckPaid() && CheckWriteCard())
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
                    if (CheckWriteCard())
                    {
                        CommandResult result = SaveCardPayment(_processingEvent.CardPaymentInfo, PaymentMode.Prepay);
                        if (result.Result != ResultCode.Successful)
                        {
                            MessageBox.Show(result.Message);
                            return;
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (_processingEvent.IsExitEvent && UserSetting.Current.OneKeyOpenDoor) //如果启用一键开闸，则按卡片有效按钮时先要保存卡片费用明细
                {
                    if (!btnCashHandle()) return;
                }

                if (!_processingEvent.ChargeAsTempCard)
                {
                    //写卡模式并且不是按在线模式处理时需要写入卡片已出场
                    if (AppSettings.CurrentSetting.EnableWriteCard
                        && _cardInfo != null
                        && !_processingEvent.OnlineHandleWhenOfflineMode)
                    {
                        _cardInfo.SetEventReportData(_processingEvent);
                        _cardInfo.IsInPark = false;//标记已出场
                        CardOperationManager.Instance.WriteCardLoop(_cardInfo);
                    }
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

                //专门针对长隆停车场设置的“转会员卡"和“转员工卡"车型，选择“转会员卡"或“转员工卡"车型时必须重新刷一张会员卡或员工卡才能时行这个操作，并在收费说明中写入“转会员卡\转员工卡+会员卡号"
                //这个功能采用硬编码
                if (CarTypeSetting.Current[carTypePanel1.SelectedCarType].Description == _ToVipCard
                    || CarTypeSetting.Current[carTypePanel1.SelectedCarType].Description == _ToStaffCard)
                {
                    string toCard = CarTypeSetting.Current[carTypePanel1.SelectedCarType].Description == _ToVipCard ? _VipCard : _StaffCard;

                    FrmVipCardReader frm = new FrmVipCardReader();
                    frm.VipCardName = toCard;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        string cardid = frm.VipCardID;
                        if (DataBaseConnectionsManager.Current.MasterConnected || DataBaseConnectionsManager.Current.StandbyConnected)
                        {
                            CardInfo card = null;
                            if (DataBaseConnectionsManager.Current.MasterConnected)
                            {
                                card = (new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect)).GetCardByID(cardid).QueryObject;
                            }
                            else
                            {
                                card = (new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect)).GetCardByID(cardid).QueryObject;
                            }

                            if (card != null && card.CardType.Name == toCard)
                            {
                                _TempCardID = _processingEvent.CardID;
                                _VipCardID = cardid;
                            }
                            else
                            {
                                MessageBox.Show(string.Format("卡号为 {0} 的卡不存在或者不是{1}，请刷{1}", cardid, toCard), Resource1.Form_Alert);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("数据库连接失败！");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //end 转会员卡、转员工卡功能 

                //新增需求，特种车需要读取授权卡
                if (CarTypeSetting.Current[carTypePanel1.SelectedCarType].Description == "特种车")
                {
                    string toCard = "特种车";
                    FrmOperatorCardReader frm = new FrmOperatorCardReader();
                    frm.OpeCardName = toCard;
                    frm.MoneyCardID = _processingEvent.CardID;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        _OperatorCardID = frm.OperatorCardID;
                        _OperatorOwnerName = frm.OperatorCardOwnerName;
                        //...
                    }
                    else
                    {
                        this.carTypePanel1.Select(0);
                        ClearCardEvent();

                        return;
                    }
                }

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
                case Keys.F8:
                    if (this.btnCoupon.Enabled)
                    {
                        btnCoupon_Click(this.btnCoupon, EventArgs.Empty);
                    }
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
                    if (this.btnPos.Enabled)
                    {
                        btnPos_Click(this.btnPos, EventArgs.Empty);
                    }
                    break;
                case Keys.F12:
                    if (this.btnCardOk.Enabled)
                    {
                        btnCardOk_Click(this.btnCardOk, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                //当前读到的卡片
                _cardInfo = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);
            }

            EntranceInfo entrance = null;
            if (this.entrancePanel1.SelectedEntranceID > 0)
            {
                entrance = ParkBuffer.Current.GetEntrance(this.entrancePanel1.SelectedEntranceID);
            }
            //如果已选择了通道，默认该通道收费
            if (entrance != null)
            {
                RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, e.CardID, e[GlobalVariables.ParkingSection]);
                if (_cardInfo != null) notify.LastCarPlate = _cardInfo.LastCarPlate;
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.ParkID];
                if (pad != null)
                {
                    pad.RemoteReadCard(notify);
                    return;
                }
            }
            //没有选择时再查找
            foreach (int enID in WorkStationInfo.CurrentStation.EntranceList)
            {
                entrance = ParkBuffer.Current.GetEntrance(enID);
                if (entrance != null && entrance.IsExitDevice)
                {
                    RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, e.CardID, e[GlobalVariables.ParkingSection]);
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
                        string cardID = GetCardIDFromBarCode(e.BarCode);
                        RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, cardID);
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
                lblDiscount.Text = (_processingEvent.CardPaymentInfo.Accounts - txtPaid.DecimalValue).ToString("N");
                if (_processingEvent.CardType.IsPrepayCard)
                {
                    btnCash.Enabled = _processingEvent.Balance < txtPaid.DecimalValue;
                    btnYCT.Enabled = (_processingEvent.Balance < txtPaid.DecimalValue && (_YCTReader != null || _YCTPOS != null));
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
            if (_YCTPOS != null) _YCTPOS.Close();
            if (_ChargeLed != null) _ChargeLed.Close();

            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved -= new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
            this.ucVideoes.Clear();
            this._EnterVideoes.Clear();
        }

        private void pnlCash_Resize(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(btnCash);
            buttons.Add(btnYCT);
            if (btnPos.Visible)
            {
                buttons.Add(btnPos);
            }
            if (btnCoupon.Visible)
            {
                buttons.Add(btnCoupon);
            }
            LayoutCarTypeButtons(pnlCash, buttons, btnCash.Height, 3);
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
            LayoutCarTypeButtons(panel6, buttons, btnCardOk.Height, 3);
        }

        private void FrmCardPaying_Activated(object sender, EventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved += new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }

        private void FrmCardPaying_Deactivate(object sender, EventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved -= new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }

        private void ZSTReader_MessageRecieved(object sender, ZSTReaderEventArgs e)
        {
            if (e.ReaderIP == AppSettings.CurrentSetting.ZSTReaderIP && e.MessageType == "1")
            {
                CardReadEventArgs args = new CardReadEventArgs() { CardID = e.CardID };
                CardReadHandler(sender, args);
            }
        }

        private void entrancePanel1_EntranceSelectedChanged(object sender, EventArgs e)
        {
            if (_processingEvent != null)
            {
                EntranceInfo newEntrance = ParkBuffer.Current.GetEntrance(entrancePanel1.SelectedEntranceID);
                if (newEntrance != null)
                {
                    int entranceID = _processingEvent.EntranceID;
                    int parkID = _processingEvent.ParkID;

                    //先清空当前事件
                    _processingEvent = null;
                    EntranceSwitchNotify notify = new EntranceSwitchNotify(entranceID, newEntrance.EntranceID);
                    if (ParkingAdapterManager.Instance[parkID] != null)
                    {
                        ParkingAdapterManager.Instance[parkID].SwitchEntrance(notify);
                    }
                }
            }
        }
        private void btnCash_EnabledChanged(object sender, EventArgs e)
        {
            this.btnPos.Enabled = this.btnCash.Enabled;
            this.btnCoupon.Enabled = this.btnCash.Enabled;
        }
        private void btnCoupon_Click(object sender, EventArgs e)
        {
            FrmParkingCouponInput frm = new FrmParkingCouponInput();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                decimal coupon = frm.CouponDiscount;
                if (coupon > 0)
                {
                    decimal discount = _processingEvent.CardPaymentInfo.Discount + coupon;
                    //如果停车券优惠大于应缴费用，停车券优惠为应缴费用
                    if (discount > _processingEvent.CardPaymentInfo.Accounts)
                    {
                        discount = _processingEvent.CardPaymentInfo.Accounts;
                    }
                    string discountMemo = _processingEvent.CardPaymentInfo.Memo + frm.CouponDescription;

                    this.lblDiscount.Text = discount.ToString("N");
                    this.lblDiscountMemo.Text = string.IsNullOrEmpty(discountMemo) ? string.Empty : discountMemo;

                    this.txtPaid.DecimalValue = _processingEvent.CardPaymentInfo.Accounts - discount;
                }
            }
        }
        #endregion

        private void tmr_YCT_Tick(object state)
        {
            if (_YCTPOS != null && _YCTPOS.IsOpened)
            {
                var c = _YCTPOS.ReadCard(UserSetting.Current.WegenType);
                if (c != null)
                {
                    if (_LastOpenCardID != c.LogicCardID)
                    {
                        _LastOpenCardID = c.LogicCardID;
                        this.Invoke((Action)(() =>   //其它线程访问UI控件
                            {
                                CardReadEventArgs args = new CardReadEventArgs { CardID = c.LogicCardID };
                                CardReadHandler(this, args);
                            }));
                    }
                }
                else
                {
                    _LastOpenCardID = string.Empty;
                }
            }
            //这里只支持龙杰的羊城通读卡器进行读卡，铭鸿的羊城通读卡器只作为缴费使用
            //else if (_YCTReader != null)
            //{
            //    YangChengTongCardInfo c = null;
            //    _YCTReader.ReadCard(out c);
            //    if (c != null)
            //    {
            //        if (_LastOpenCardID != c.CardID)
            //        {
            //            _LastOpenCardID = c.CardID;

            //            CardReadEventArgs args = new CardReadEventArgs { CardID = c.CardID };
            //            CardReadHandler(this, args);
            //        }
            //    }
            //    else
            //    {
            //        _LastOpenCardID = string.Empty;
            //    }
            //}
        }
    }
}
