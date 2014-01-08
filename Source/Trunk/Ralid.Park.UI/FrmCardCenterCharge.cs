using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Factory;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.LED;
using Ralid.GeneralLibrary.Speech;
using Ralid.GeneralLibrary.Printer;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.UI.Resources;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.UI
{
    public partial class FrmCardCenterCharge : Form, IReportHandler
    {
        public FrmCardCenterCharge()
        {
            InitializeComponent();
        }

        #region 私有字段
        private CardPaymentInfo _ChargeRecord;
        private CardInfo _cardInfo;
        private BarCodeReader _TicketReader;
        private YangChengTongReader _YCTReader;
        private EpsonmodePrinter _BillPrinter;
        private IParkingLed _ChargeLed = null;
        //private CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
        //private CardEventBll _CardEventBll = new CardEventBll(AppSettings.CurrentSetting.ParkConnect);
        //private SnapShotBll _SnapShotBll = new SnapShotBll(AppSettings.CurrentSetting.ParkConnect);
        private FrmCardEventDetail frmEventDetail;
        private FrmCarPlateFailDetail frmCarPlateFailDetail;

        //这两个参数用于长隆转会员卡功能 
        private string _TempCardID;  //要转会员卡收费的临时卡卡号
        private string _VipCardID;   //通过刷卡获取的会员卡卡号
        private string _ToVipCard = "转会员卡";
        private string _VipCard = "会员卡";
        //end 
        #endregion

        #region 私有方法
        private void ShowCardPaymentInfo(CardPaymentInfo cardPayment)
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
            //this.lblLastTotalPaid.Text = cardPayment.LastTotalPaid.ToString();
            this.lblLastTotalPaid.Text = _cardInfo.TotalPaidFee.ToString();
            //this.lblLastTotalDiscount.Text = cardPayment.LastTotalDiscount.ToString();
            this.lblAccounts.Text = cardPayment.Accounts.ToString();
            this.lblLastWorkstation.Text = cardPayment.LastStationID;
            this.txtPaid.DecimalValue = cardPayment.Accounts - cardPayment.Discount;
            this.lblDiscount.Text = cardPayment.Discount.ToString();
            this.txtMemo.Text = string.Empty;

            this.picIn.Clear();
            SnapShotBll _SnapShotBll = new SnapShotBll(AppSettings.CurrentSetting.ParkConnect);
            List<SnapShot> imgs = _SnapShotBll.GetSnapShots(cardPayment.EnterDateTime.Value, cardPayment.CardID);
            if (imgs != null && imgs.Count > 0)
            {
                this.picIn.ShowSnapShots(imgs);
            }

            string msg = string.Format(Resource1.FrmCardPaying_PayingSpeech, TariffSetting.Current.TariffOption.StrMoney(cardPayment.Accounts));

            this.carTypePanel1.SelectedCarType = cardPayment.CarType;
            this.btnCash.Enabled = true;
            this.btnCash.Focus();
            if (cardPayment.CardType.Name.Contains ("中山通") && 
                AppSettings.CurrentSetting.EnableZST && !string.IsNullOrEmpty(AppSettings.CurrentSetting.ZSTReaderIP))
            {
                this.btnYCT.Text = "中山通[&F10]";
                this.btnYCT.Enabled = true;
            }
            else
            {
                this.btnYCT.Enabled = (_YCTReader != null) ? true : false;
            }
            this.btnCancel.Enabled = true;
            this.btnRepay.Enabled = _cardInfo.IsPaid;

            if (_cardInfo.IsCompletedPaid && TariffSetting.Current.IsInFreeTime(_cardInfo.PaidDateTime.Value, cardPayment.ChargeDateTime))
            {
                //已缴费，并且未过免费时间
                msg = string.Format(Resource1.FrmCardCenterCharge_FreeRemain, TariffSetting.Current.FreeTimeRemaining(_cardInfo.PaidDateTime.Value, cardPayment.ChargeDateTime));
                this.eventList.InsertMessage(msg);
                this.txtMemo.Text = msg;
                //不允许缴费
                this.btnCash.Enabled = false;
                this.btnYCT.Enabled = false;
            }

            if (_ChargeLed != null) _ChargeLed.DisplayMsg(msg);
            if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(msg);

            //长隆转会员卡功能
            if (cardPayment.CardID == _TempCardID && CarTypeSetting.Current.GetDescription(cardPayment.CarType) == _ToVipCard)
            {
                this.txtMemo.Text = _ToVipCard + _VipCardID;
            }

            CardReaderManager.GetInstance(UserSetting.Current.WegenType).StopReadCard();
        }

        private void ClearInput()
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
            this.lblAccounts.Text = string.Empty;
            this.lblLastWorkstation.Text = string.Empty;
            this.txtPaid.DecimalValue = 0;
            this.lblDiscount.Text = string.Empty;
            this.btnCancel.Enabled = false;
            this.btnCash.Enabled = false;
            this.btnYCT.Enabled = false;
            this.btnRepay.Enabled = false;
            this._cardInfo = null;
            this._ChargeRecord = null;
            this.picIn.Clear();
            this.txtCardID.ReadOnly = false;
            this.txtMemo.Text = string.Empty;
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
        }

        private CommandResult SaveCardPayment(PaymentMode paymentMode)
        {
            CommandResult result = null;
            _ChargeRecord.PaymentMode = paymentMode;
            _ChargeRecord.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
            _ChargeRecord.StationID = WorkStationInfo.CurrentStation.StationName;
            _ChargeRecord.Paid = this.txtPaid.DecimalValue;
            _ChargeRecord.Discount = _ChargeRecord.Accounts - this.txtPaid.DecimalValue;
            _ChargeRecord.IsCenterCharge = true;
            _ChargeRecord.Memo = this.txtMemo.Text;
            
            CardBll cbll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);

            bool both = WorkStationInfo.CurrentStation.NeedBothDatabaseUpdate;
            bool offlineHandleCard = AppSettings.CurrentSetting.EnableWriteCard
                && _cardInfo != null
                && !_cardInfo.OnlineHandleWhenOfflineMode;

            result = cbll.PayParkFee(_cardInfo, _ChargeRecord, AppSettings.CurrentSetting.CurrentStandbyConnect, both, offlineHandleCard);

            if (result.Result != ResultCode.Successful && offlineHandleCard)
            {
                //与主数据库通信故障时，脱机模式时按脱机模式处理的卡片，收费信息写入本地数据库，待通信正在时，上传到主数据库
                LDB_CardPaymentRecordBll ldb_cbll = new LDB_CardPaymentRecordBll(LDB_AppSettings.Current.LDBConnect);
                result = ldb_cbll.PayParkFee(_cardInfo, _ChargeRecord);
            }

            //写卡模式需要将收费信息写入卡片扇区
            if (result.Result == ResultCode.Successful && offlineHandleCard)
            {
                CardOperationManager.Instance.WriteCardLoop(_cardInfo);
            }
            return result;
        }

        private void ProcessCardEvent(CardEventReport report)
        {
            if (report.ChargeAsTempCard) return; //有收费的肯定不会要确认放行,就回来收费
            //只处理车片对比失败要确认放行的事件
            if (report.ComparisonResult==CarPlateComparisonResult.Fail
                ||report.EventStatus == CardEventStatus.CarPlateFail) //待处理的卡片事件
            {
                AddCardEventReportToGridView(report);
                if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(string.Format(Resource1.FrmCardPaying_CardWaitExitSpeech, report.SourceName));
            }
            else if (report.EventStatus == CardEventStatus.Valid 
                && report.ComparisonResult != CarPlateComparisonResult.Fail)
            {
                RemoveCardEventReportFromGridView(report);
            }
        }

        private void RemoveCardEventReportFromGridView(CardEventReport report)
        {
            DataGridViewRow row = null;
            foreach (DataGridViewRow r in GridView.Rows)
            {
                CardEventReport preEvent = r.Tag as CardEventReport;
                if (preEvent.EntranceID == report.EntranceID && preEvent.ParkID == report.ParkID)
                {
                    row = r;
                    break;
                }
            }
            if (row != null)
            {
                GridView.Rows.Remove(row);
            }
        }

        private void AddCardEventReportToGridView(CardEventReport report)
        {
            DataGridViewRow row = null;
            foreach (DataGridViewRow r in GridView.Rows)
            {
                CardEventReport preEvent = r.Tag as CardEventReport;
                if (preEvent != null && preEvent.EntranceID == report.EntranceID && preEvent.ParkID == report.ParkID)
                {
                    row = r;
                    break;
                }
            }
            if (row != null)
            {
                GridView.Rows.Remove(row);
            }

            GridView.Rows.Insert(0, 1);
            row = GridView.Rows[0];
            row.Tag = report;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.ForeColor = Color.Red;
            row.DefaultCellStyle = style;
            row.Cells["colCardID"].Value = report.CardID;
            row.Cells["colCardType"].Value = report.CardType.ToString();
            row.Cells["colEventDateTime"].Value = report.EventDateTime;
            row.Cells["colEntranceName"].Value = ParkBuffer.Current.GetEntrance(report.EntranceID).EntranceName;
            row.Cells["colAccounts"].Value = report.CardPaymentInfo != null ? report.CardPaymentInfo.Accounts : 0;
            row.Cells["colLastCarPlate"].Value = report.IsExitEvent ? report.LastCarPlate : report.CarPlate;//入口事件中，显示的是识别到的车牌
            row.Cells["colCarPlate"].Value = report.IsExitEvent ? report.CarPlate : string.Empty;//入口事件中，没有出口车牌
        }

        private bool CheckPaid()
        {
            if (this.txtPaid.DecimalValue > _ChargeRecord.Accounts)
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
        #endregion

        #region ICardEventHandler 成员
        public void ProcessReport(ReportBase report)
        {
            Action<ReportBase> action = delegate(ReportBase report1)
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(report1.EntranceID);
                if (entrance != null && WorkStationInfo.CurrentStation.EntranceList.Exists(e => e == entrance.EntranceID))//是否是工作站要处理的事件
                {
                    if (report1 is CardEventReport)
                    {
                        CardEventReport eventInfo = report1 as CardEventReport;
                        ProcessCardEvent(eventInfo);
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
        private void FrmCardCenterCharge_Load(object sender, EventArgs e)
        {
            this.carTypePanel1.Init();
            ClearInput();

            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);

            if (AppSettings.CurrentSetting.TicketReaderCOMPort > 0)
            {
                _TicketReader = new BarCodeReader(AppSettings.CurrentSetting.TicketReaderCOMPort);
                _TicketReader.BarCodeRead += new BarCodeReadEventHandler(TicketReader_BarCodeRead);
                _TicketReader.Open();
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
                _ChargeLed.PermanentSentence = Resource1.FrmCardPaying_CentralCharge;
            }
            if (AppSettings.CurrentSetting.BillPrinterCOMPort > 0)
            {
                _BillPrinter = new EpsonmodePrinter(AppSettings.CurrentSetting.BillPrinterCOMPort, 9600);
                _BillPrinter.Open();
            }
            if (AppSettings.CurrentSetting.YCTReaderCOMPort > 0)
            {
                _YCTReader = new YangChengTongReader(AppSettings.CurrentSetting.YCTReaderCOMPort, 1);
                _YCTReader.Open();
            }

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

            //写卡模式不允许输入卡号
            //this.txtCardID.Enabled = !GlobalVariables.IsNETParkAndOffLie;
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            if (_ChargeRecord != null && CheckPaid() && CheckWriteCard())
            {
                if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(Resource1.FrmCardPaying_Paying);
                CommandResult result = SaveCardPayment(PaymentMode.Cash);
                if (result.Result == ResultCode.Successful)
                {
                    //用于打印收费小票打开钱箱收款
                    if (_BillPrinter != null)
                    {
                        ParkBillInfo bill = ParkBillFactory.CreateParkBill(_ChargeRecord);
                        _BillPrinter.PrintParkBill(bill);
                    }
                    ClearInput();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void btnYCT_Click(object sender, EventArgs e)
        {
            if (_ChargeRecord != null && CheckPaid())
            {
                CommandResult result = null;
                if (btnYCT.Text.Contains("中山通"))
                {
                    FrmZSTPayment frmZST = new FrmZSTPayment();
                    frmZST.Payment = this.txtPaid.DecimalValue;
                    if (frmZST.ShowDialog() == DialogResult.OK)
                    {
                        result = SaveCardPayment(PaymentMode.ZhongShanTong);
                    }
                }
                else
                {
                    FrmYCTPayment frmYCT = new FrmYCTPayment();
                    frmYCT.Reader = this._YCTReader;
                    frmYCT.Payment = this.txtPaid.DecimalValue;
                    if (frmYCT.ShowDialog() == DialogResult.OK)
                    {
                        result = SaveCardPayment(PaymentMode.YangChengTong);
                    }
                }
                if (result != null)
                {
                    if (result.Result == ResultCode.Successful)
                    {
                        //用于打印收费小票打开钱箱收款
                        if (_BillPrinter != null)
                        {
                            ParkBillInfo bill = ParkBillFactory.CreateParkBill(_ChargeRecord);
                            _BillPrinter.PrintParkBill(bill);
                        }
                        ClearInput();
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
            if (_ChargeRecord != null && CheckPaid())
            {
                if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(Resource1.FrmCardPaying_Paying);
                CommandResult result = SaveCardPayment(PaymentMode.Pos);
                if (result.Result == ResultCode.Successful)
                {
                    //用于打印收费小票打开钱箱收款
                    if (_BillPrinter != null)
                    {
                        ParkBillInfo bill = ParkBillFactory.CreateParkBill(_ChargeRecord);
                        _BillPrinter.PrintParkBill(bill);
                    }
                    ClearInput();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void btnRepay_Click(object sender, EventArgs e)
        {
            if (!DataBaseConnectionsManager.Current.BothCconnectedOrNoStandby)
            {
                MessageBox.Show("与数据库连接断开。");
                return;
            }

            if (_cardInfo != null)
            {
                bool hadCard = true;
                //写卡模式时,需要删除卡片内的缴费数据，重新写入缴费数据
                if (AppSettings.CurrentSetting.EnableWriteCard && !_cardInfo.OnlineHandleWhenOfflineMode)
                {
                    hadCard = CardOperationManager.Instance.CheckCard(_cardInfo.CardID) == CardOperationResultCode.Success;
                    if (!hadCard)
                    {
                        if (MessageBox.Show(Resource1.FrmCardCenterCharge_NotCard, Resource1.Form_Query,
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                if (MessageBox.Show(Resource1.FrmCardPaying_CancelPaymentQuey, Resource1.Form_Query,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
                    CommandResult result = _CardBll.DeleteLastPayment(_cardInfo, OperatorInfo.CurrentOperator, AppSettings.CurrentSetting.CurrentStandbyConnect, WorkStationInfo.CurrentStation.HasStandbyDatabase);
                    if (result.Result == ResultCode.Successful)
                    {
                        //写卡模式时,需要删除卡片内的缴费数据，重新写入缴费数据
                        if (hadCard
                            && AppSettings.CurrentSetting.EnableWriteCard
                            && !_cardInfo.OnlineHandleWhenOfflineMode)
                        {
                            CardOperationManager.Instance.WriteCardLoop(_cardInfo);
                        }
                        ReadCardIDHandler(_cardInfo.CardID, _cardInfo);
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_ChargeRecord != null)
            {
                _ChargeRecord = null;
                ClearInput();
            }
        }

        private void CarType_Selected(object sender, EventArgs e)
        {
            if (_ChargeRecord != null && CarTypeSetting.Current[carTypePanel1.SelectedCarType] != null)
            {
                //专门针对长隆停车场设置一种“转会员卡"车型，选择“转会员卡"车型时必须重新刷一张会员卡才能时行这个操作，并在收费说明中写入“转会员卡+会员卡号"
                //这个功能采用硬编码
                if (CarTypeSetting.Current[carTypePanel1.SelectedCarType].Description == _ToVipCard)
                {
                    FrmVipCardReader frm = new FrmVipCardReader();
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

                            if (card != null && card.CardType.Name == _VipCard)
                            {
                                _TempCardID = _ChargeRecord.CardID;
                                _VipCardID = cardid;
                            }
                            else
                            {
                                MessageBox.Show(string.Format("卡号为 {0} 的卡不存在或者不是会员卡，请刷会员卡", cardid), Resource1.Form_Alert);
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
                //end 转会员卡功能 

                _ChargeRecord = CardPaymentInfoFactory.CreateCardPaymentRecord(_cardInfo, TariffSetting.Current, carTypePanel1.SelectedCarType, _ChargeRecord.ChargeDateTime);
                ShowCardPaymentInfo(_ChargeRecord);
            }
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

        ///// <summary>
        ///// 脱机模式时按在线模式处理的检验
        ///// </summary>
        ///// <param name="card"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //private bool OnlineHandleWhenOfflineModeCheck(CardInfo card, out string msg)
        //{

        //    if (AppSettings.CurrentSetting.EnableWriteCard
        //        && !card.OnlineHandleWhenOfflineMode)//脱机模式时，按脱机模式处理
        //    {
        //        if (!DataBaseConnectionsManager.Current.BothCconnectedOrNoStandby)
        //        {
        //            msg = "与主数据库或备用数据库连接失败！";
        //            return false;
        //        }

        //        if (DataBaseConnectionsManager.Current.StandbyConnected)
        //        {
        //            //与备用数据库连接上时，检查主数据库与备用数据库的数据是否一致
        //            CardBll scbll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
        //            CardInfo scard = scbll.GetCardByID(card.CardID).QueryObject;
        //            if (scard == null
        //                || scard.ParkingStatus != card.ParkingStatus   //停车状态
        //                || scard.LastDateTime != card.LastDateTime     //最近一次刷卡时间
        //                ||scard.PaidDateTime!=card.PaidDateTime        //缴费时间
        //                ||scard.TotalPaidFee!=card.TotalPaidFee)       //已缴费用
        //            {
        //                msg = "主数据库与备用数据库信息不一致！";
        //                return false;
        //            }
        //        }
        //    }

        //    msg = string.Empty;
        //    return true;
            
        //}
        
        /// <summary>
        /// 获取卡片详细信息
        /// </summary>
        /// <param name="offlineHandleCard">是否脱机处理的卡片</param>
        /// <param name="cardID">卡号</param>
        /// <param name="info">从卡片扇区数据中读取到的卡片信息</param>
        /// <param name="card">返回的卡片</param>
        /// <param name="msg">返回的错误信息</param>
        /// <returns></returns>
        private bool GetCardDetail(bool offlineHandleCard, string cardID, CardInfo info, out CardInfo card, out string msg)
        {
            card = null;
            if (DataBaseConnectionsManager.Current.MasterConnected
                    || DataBaseConnectionsManager.Current.StandbyConnected)
            {

                CardInfo mastercard = null;
                CardInfo standbycard = null;

                if (DataBaseConnectionsManager.Current.MasterConnected)
                {
                    CardBll mcbll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
                    mastercard = mcbll.GetCardDetail(cardID);
                }

                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    //需要获取备用数据库的卡片信息进行比对
                    //通信工作站如果没有连接上主数据库，需要从备用数据库中获取卡片信息
                    CardBll scbll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    standbycard = scbll.GetCardDetail(cardID);
                }

                //在线处理的卡片,主数据库连上，有备份数据库的，需要与获取备用数据库的卡片信息进行比对
                if (!offlineHandleCard
                    &&DataBaseConnectionsManager.Current.MasterConnected
                    &&WorkStationInfo.CurrentStation.HasStandbyDatabase)
                {
                    if (mastercard == null && standbycard == null)
                    {
                        //没有该卡片
                    }
                    else if (mastercard == null
                        || standbycard == null
                        || !mastercard.CompareChargeInfo(standbycard))
                    {
                        msg = "主数据库与备用数据库信息不一致！";
                        return false;
                    }
                }

                if (mastercard != null)
                {
                    card = mastercard;
                }
                else if (WorkStationInfo.CurrentStation.IsHostWorkstation || offlineHandleCard)
                {
                    card = standbycard;
                }
            }
            else if (offlineHandleCard)
            {
                //与主数据库和备用连接断开时，如果是脱机处理的卡片，获取的为读到的卡片信息
                card = info.Clone();
            }

            msg = string.Empty;
            return true;
        }

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
            CardInfo card = null;

            bool offlineHandleCard = AppSettings.CurrentSetting.EnableWriteCard
                && info != null
                && !info.OnlineHandleWhenOfflineMode;

            if (!WorkStationInfo.CurrentStation.CanPayment(offlineHandleCard, out msg))
            {
                //该工作站不能进行收费
            }
            else if (!GetCardDetail(offlineHandleCard, cardID, info, out card, out msg))
            {
                //获取卡片信息失败
            }
            else if (card == null)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_UnRegister);
            }
            else if (AppSettings.CurrentSetting.EnableWriteCard
                && !card.OnlineHandleWhenOfflineMode
                && !CardDateResolver.Instance.CopyCardDataToCard(card, info))
            {
                //写卡模式时，卡片信息从扇区数据中获取
                msg = Resource1.FrmCardCenterCharge_CardDataErr;
            }
            else if (!ValidateCard(card, out msg))
            {
                //卡片无效
            }
            else if (TariffSetting.Current.GetTariff(card.CardType.ID, card.CarType) == null)
            {
                //msg = Resource1.FrmCardPaying_NotTempCard;
                msg = Resource1.FrmCardCenterCharge_NotPaymentCard;
            }
            else if (!card.IsInPark)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_StillOut);
            }
            else if (card.LastDateTime > DateTime.Now)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_WrongInTime);
            }
            else
            {
                _cardInfo = card;
                _ChargeRecord = CardPaymentInfoFactory.CreateCardPaymentRecord(_cardInfo, TariffSetting.Current, _cardInfo.CarType, DateTime.Now);
                ShowCardPaymentInfo(_ChargeRecord);
            }
            if (!string.IsNullOrEmpty(msg))
            {
                if (_ChargeLed != null) _ChargeLed.DisplayMsg(msg);
                if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(msg);
                ClearInput();
                this.txtCardID.Text = cardID;
                this.eventList.InsertMessage(msg);
                this.txtMemo.Text = msg;
            }
            txtCardID.TextChanged += txtCardID_TextChanged;
        }

        private void FrmCardCenterCharge_KeyDown(object sender, KeyEventArgs e)
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
                    //    btnPos_Click(this.btnPos, EventArgs.Empty);
                    //}
                    break;
                case Keys.F12:
                    if (this.btnCancel.Enabled)
                    {
                        btnCancel_Click(this.btnCancel, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }
        }

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

        private void TicketReader_BarCodeRead(object sender, BarCodeReadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.BarCode))
            {
                ClearInput();
                ReadCardIDHandler(e.BarCode, null);
            }
        }

        private void GridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CardEventReport info = this.GridView.Rows[e.RowIndex].Tag as CardEventReport;
                if (info != null)
                {
                    if (info.EventStatus == CardEventStatus.CarPlateFail)
                    {
                        if (frmEventDetail == null)
                        {
                            frmEventDetail = new FrmCardEventDetail();
                            frmEventDetail.CardEventProcessed += new Ralid.Park.UI.EventArgument.CardEventProcessedHandler(frm_CardEventProcessed);
                        }
                        frmEventDetail.ProcessingEvent = info;
                        frmEventDetail.Show();
                        frmEventDetail.Activate();
                    }
                    else if (info.EventStatus == CardEventStatus.Valid)
                    {
                        if (frmCarPlateFailDetail == null)
                        {
                            frmCarPlateFailDetail = new FrmCarPlateFailDetail();
                            frmCarPlateFailDetail.CardEventProcessed += new Ralid.Park.UI.EventArgument.CardEventProcessedHandler(frm_CardEventProcessed);
                        }
                        frmCarPlateFailDetail.ProcessingEvent = info;
                        frmCarPlateFailDetail.Show();
                        frmCarPlateFailDetail.Activate();
                    }
                }
            }
        }

        private void frm_CardEventProcessed(object sender, Ralid.Park.UI.EventArgument.CardEventProcessedArgs e)
        {
            DataGridViewRow row = null;
            foreach (DataGridViewRow r in GridView.Rows)
            {
                CardEventReport preEvent = r.Tag as CardEventReport;
                if (preEvent.EntranceID == e.ProcessedEvent.EntranceID && preEvent.ParkID == e.ProcessedEvent.ParkID)
                {
                    row = r;
                    break;
                }
            }
            if (row != null)
            {
                GridView.Rows.Remove(row);
            }
        }

        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCardID.Text))
            {
                CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = _CardBll.GetCardByID(this.txtCardID.Text).QueryObject;
                if (card != null)
                {
                    ReadCardIDHandler(txtCardID.Text, card);
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
            }
        }

        private void txtPaid_TextChanged(object sender, EventArgs e)
        {
            if (_ChargeRecord != null)
            {
                lblDiscount.Text = (_ChargeRecord.Accounts - txtPaid.DecimalValue).ToString();
            }
        }

        private void FrmCardCenterCharge_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved -= new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }

        private void FrmCardCenterCharge_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppSettings.CurrentSetting.SaveConfig("PaymentPanelWidth", paymentPanel.Width.ToString());
            AppSettings.CurrentSetting.SaveConfig("VideoPanelHeight", videoPanel.Height.ToString());
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            if (_TicketReader != null) _TicketReader.Close();
            if (_BillPrinter != null) _BillPrinter.Close();
            if (_YCTReader != null) _YCTReader.Close();
            if (_ChargeLed != null) _ChargeLed.Close();
        }
        #endregion

        private void panel6_Resize(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(btnCash);
            buttons.Add(btnYCT);
            LayoutCarTypeButtons(panel6, buttons, btnCash.Height, 3);
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

        private void panel7_Resize(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>();
            buttons.Add(btnCancel);
            buttons.Add(btnRepay);
            LayoutCarTypeButtons(panel6, buttons, btnCancel.Height, 3);
        }

        private void txtMemo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnCash.Enabled && AppSettings.CurrentSetting.ChargeAfterMemo)
            {
                btnCash_Click(this.btnCash, EventArgs.Empty);
            }
        }

        #region 与中山通读卡器有关
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
        #endregion
    }
}