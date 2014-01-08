using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OfflineCardPayingTool.Resources;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Factory;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;
using Ralid.Park.UI;
using Ralid.GeneralLibrary.CardReader;
using Ralid.GeneralLibrary.Printer;
using Ralid.GeneralLibrary.LED;
using Ralid.GeneralLibrary.Speech;

namespace OfflineCardPayingTool
{
    public partial class FrmPaying : Form
    {
        #region 构造函数
        public FrmPaying()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private CardPaymentInfo _ChargeRecord;
        private BarCodeReader _TicketReader;
        private EpsonmodePrinter _BillPrinter;
        private IParkingLed _ChargeLed = null;
        private YangChengTongReader _YCTReader;
        //这两个参数用于长隆转会员卡功能 bruce 2013-1-9
        private string _TempCardID;  //要转会员卡收费的临时卡卡号
        private string _VipCardID;   //通过刷卡获取的会员卡卡号
        private string _ToVipCard = "转会员卡";
        private string _VipCard = "会员卡";
        //end 

        private CardInfo _cardInfo;//当前读到的卡片，用于写卡模式
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
            this.lblBalance.Text = _cardInfo.Balance.ToString("F2");
            //this.lblLastTotalPaid.Text = cardPayment.LastTotalPaid.ToString();
            this.lblLastTotalPaid.Text = _cardInfo.TotalPaidFee.ToString();
            //this.lblLastTotalDiscount.Text = cardPayment.LastTotalDiscount.ToString();
            this.lblAccounts.Text = cardPayment.Accounts.ToString();
            this.lblLastWorkstation.Text = cardPayment.LastStationID;
            this.txtPaid.DecimalValue = cardPayment.Accounts - cardPayment.Discount;
            this.lblDiscount.Text = cardPayment.Discount.ToString();
            this.txtMemo.Text = string.Empty;

            if (_cardInfo.CardType.IsPrepayCard && _cardInfo.Balance >= cardPayment.Accounts)
            {
                this.btnCash.Text = "储值扣费[F9]";
            }
            else
            {
                this.btnCash.Text = "现金收费[F9]";
            }

            string msg = string.Format(Resource1.FrmCardPaying_PayingSpeech, TariffSetting.Current.TariffOption.StrMoney(cardPayment.Accounts));

            this.carTypePanel1.SelectedCarType = cardPayment.CarType;
            this.btnCash.Enabled = true;
            this.btnCash.Focus();
            if (cardPayment.CardType.Name.Contains("中山通") &&
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

            CardReaderManager.GetInstance(UserSetting.Current.WegenType).StopReadCard();
        }
        private void ClearInput()
        {
            this.btnCash.Text = "现金收费[F9]";
            this.txtCardID.Text = string.Empty;
            this.lblOwnerName.Text = string.Empty;
            this.lblCarNum.Text = string.Empty;
            this.lblEnterDateTime.Text = string.Empty;
            this.lblExitDateTime.Text = string.Empty;
            this.lblParkingTime.Text = string.Empty;
            this.lblCardType.Text = string.Empty;
            this.lblBalance.Text = string.Empty;
            this.lblLastTotalPaid.Text = string.Empty;
            this.lblAccounts.Text = string.Empty;
            this.lblLastWorkstation.Text = string.Empty;
            this.txtPaid.DecimalValue = 0;
            this.lblDiscount.Text = string.Empty;
            this.btnCancel.Enabled = false;
            this.btnCash.Enabled = false;
            this.btnYCT.Enabled = false;
            this._cardInfo = null;
            this._ChargeRecord = null;
            this.txtCardID.ReadOnly = false;
            this.txtMemo.Text = string.Empty;
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
        }

        /// <summary>
        /// 读取到卡号处理
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="info">从卡片扇区数据中读取到的卡片信息</param>
        private void ReadCardIDHandler(string cardID, CardInfo info)
        {
            this.txtCardID.Text = cardID;
            this.txtCardID.ReadOnly = true;
            string msg = string.Empty;
            CardInfo card = info;
            if (card == null)
            {
                msg = Resource1.FrmCardCenterCharge_CardDataErr;
            }
            else if (!ValidateCard(card, out msg))
            {
                //卡片无效
            }
            else if (TariffSetting.Current.GetTariff(card.CardType.ID, card.CarType) == null)
            {
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
                _ChargeRecord.CarPlate = _cardInfo.CarPlate;
                ShowCardPaymentInfo(_ChargeRecord);
            }
            if (!string.IsNullOrEmpty(msg))
            {
                if (_ChargeLed != null) _ChargeLed.DisplayMsg(msg);
                if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(msg);
                ClearInput();
                this.txtCardID.Text = cardID;
                this.txtMemo.Text = msg;
                this.eventList.InsertMessage(msg);
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
                msg =Resource1.FrmCardCenterCharge_CardUnActivate;
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

        private CommandResult SaveCardPayment(PaymentMode paymentMode)
        {
            CommandResult result = null;
            _ChargeRecord.PaymentMode = paymentMode;
            _ChargeRecord.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
            _ChargeRecord.StationID = WorkStationInfo.CurrentStation.StationName;
            _ChargeRecord.Paid = this.txtPaid.DecimalValue;
            _ChargeRecord.Discount = _ChargeRecord.Accounts - this.txtPaid.DecimalValue;
            _ChargeRecord.IsCenterCharge = WorkStationInfo.CurrentStation.IsCenterCharge;
            _ChargeRecord.Memo = this.txtMemo.Text;
            LDB_CardPaymentRecordBll cbll = new LDB_CardPaymentRecordBll(LDB_AppSettings.Current.LDBConnect);
            result = cbll.PayParkFee(_cardInfo, _ChargeRecord);

            if (result.Result == ResultCode.Successful)
            {
                _cardInfo.IsInPark = false;//标记已出场
                CardOperationManager.Instance.WriteCardLoop(_cardInfo);
            }
            return result;
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
                    CardInfo card = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e.ParkingDate);
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
        #endregion

        #region 窗体事件处理

        private void FrmPaying_Load(object sender, EventArgs e)
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

        }
        private void btnCash_Click(object sender, EventArgs e)
        {
            if (_ChargeRecord != null && CheckPaid())
            {
                if (AppSettings.CurrentSetting.EnableTTS) TTSSpeech.Instance.Speek(Resource1.FrmCardPaying_Paying);
                PaymentMode mode = this.btnCash.Text == "现金收费[F9]" ? PaymentMode.Cash : PaymentMode.Prepay;
                CommandResult result = SaveCardPayment(mode);
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
        private void carTypePanel1_CarTypeSelectedChanged(object sender, EventArgs e)
        {
            if (_ChargeRecord != null && CarTypeSetting.Current[carTypePanel1.SelectedCarType] != null)
            {
                _ChargeRecord = CardPaymentInfoFactory.CreateCardPaymentRecord(_cardInfo, TariffSetting.Current, carTypePanel1.SelectedCarType, _ChargeRecord.ChargeDateTime);
                ShowCardPaymentInfo(_ChargeRecord);
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
        private void FrmPaying_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved -= new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }
        private void FrmPaying_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppSettings.CurrentSetting.SaveConfig("PaymentPanelWidth", paymentPanel.Width.ToString());
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            if (_TicketReader != null) _TicketReader.Close();
            if (_BillPrinter != null) _BillPrinter.Close();
            if (_YCTReader != null) _YCTReader.Close();
            if (_ChargeLed != null) _ChargeLed.Close();
        }
        #endregion

        #region 与中山通读卡器有关
        private void FrmPaying_Activated(object sender, EventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved += new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }

        private void FrmPaying_Deactivate(object sender, EventArgs e)
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
