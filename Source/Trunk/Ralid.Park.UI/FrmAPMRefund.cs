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
    public partial class FrmAPMRefund : Form
    {
        public FrmAPMRefund()
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

            CardInfo card = bll.GetCardDetail(cardID);
            if (card == null && offlineHandleCard) card = info.Clone();

            if (!offlineHandleCard
                && !DataBaseConnectionsManager.Current.MasterConnected)
            {
                msg = Resource1.FrmAPMRefund_MasterConnectFail;
            }
            else if (card == null)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_UnRegister);
            }
            else if (AppSettings.CurrentSetting.EnableWriteCard
                && !card.OnlineHandleWhenOfflineMode
                && !CardDateResolver.Instance.CopyPaidDataToCard(card, info))//只复制缴费相关的信息，如果复制了所有的信息，会覆盖数据库内的卡片状态，如挂失，禁用等状态
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
            if (!card.IsTempCard)
            {
                msg = Resource1.FrmAPMRefund_NoTempCard;
                return false;
            }
            if (!card.IsInPark)
            {
                msg = CardInvalidDescripition.GetDescription(EventInvalidType.INV_StillOut);
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
            this.lblCardType.Text = string.Empty;
            this.lblEnterDateTime.Text = string.Empty;
            this.lblPaidDateTime.Text = string.Empty;
            this.lblParkFee.Text = string.Empty;
            this.lblTotalPaidFee.Text = string.Empty;
            this.txtTurnbackMoney.DecimalValue = 0;
            this.comAPM.SelectedIndex = 0;
            this.txtSerialNumber.Text = string.Empty;
            this.txtMemo.Text = string.Empty;
        }

        private void ShowCardInfo(CardInfo info)
        {
            this.txtCardID.Text = info.CardID;
            this.lblCardType.Text = info.CardType.Name;
            this.lblEnterDateTime.Text = info.LastDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (info.PaidDateTime.HasValue)
            {
                this.lblPaidDateTime.Text = info.PaidDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            this.lblParkFee.Text = info.ParkFee.ToString();
            this.lblTotalPaidFee.Text = info.TotalPaidFee.ToString();
            this.txtTurnbackMoney.DecimalValue = info.TotalPaidFee;
        }

        private void InitComAPM()
        {
            this.comAPM.Items.Clear();
            this.comAPM.Items.Add(string.Empty);
            List<APM> items = (new APMBll(AppSettings.CurrentSetting.ParkConnect)).GetAllItems().QueryObjects;
            foreach (APM item in items)
            {
                this.comAPM.Items.Add(item.SerialNum);
            }
        }

        private bool CheckInput()
        {
            if (_cardInfo == null)
            {
                MessageBox.Show(Resource1.FrmAPMRefund_NoCard);
                return false;
            }
            if (string.IsNullOrEmpty(this.txtTurnbackMoney.Text.Trim())
                || this.txtTurnbackMoney.DecimalValue == 0)
            {
                MessageBox.Show(Resource1.FrmAPMRefund_AmountInvalid);
                return false;
            }
            if (this.comAPM.SelectedIndex < 1)
            {
                MessageBox.Show(Resource1.FrmAPMRefund_APMInvalid);
                return false;
            }
            if(string.IsNullOrEmpty(this.txtSerialNumber.Text.Trim()))
            {
                MessageBox.Show(Resource1.FrmAPMRefund_SerialNumInvalid);
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

        private APMRefundRecord CreateAPMRefundRecord(CardInfo info)
        {
            APMRefundRecord record = new APMRefundRecord(info);
            record.RefundDateTime = DateTime.Now;
            record.RefundMoney = this.txtTurnbackMoney.DecimalValue;
            record.MID = this.comAPM.Text;
            record.PaymentSerialNumber = this.txtSerialNumber.Text.Trim();
            record.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
            record.StationID = WorkStationInfo.CurrentStation.StationName;
            record.Memo = this.txtMemo.Text.Trim();

            return record;
        }


        /// <summary>
        /// 退款
        /// </summary>
        private void Refund(CardInfo cardinfo)
        {
            bool offlineHandleCard = AppSettings.CurrentSetting.EnableWriteCard
                && cardinfo != null
                && !cardinfo.OnlineHandleWhenOfflineMode;

            APMRefundRecord record = CreateAPMRefundRecord(cardinfo);

            CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            bool success = _CardBll.APMCardRefund(cardinfo, record).Result == ResultCode.Successful;

            if (offlineHandleCard && success)
            {
                success = CardOperationManager.Instance.WriteCardLoop(cardinfo) == CardOperationResultCode.Success;
                if (!success)
                {
                    //写入失败时，将数据库的卡片退款信息还原
                    _CardBll.UpdateCardPaymentInfo(_OriginalCard);
                    APMRefundRecordBll recordBll = new APMRefundRecordBll(AppSettings.CurrentSetting.ParkConnect);
                    recordBll.Delete(record);
                }
            }

            if (success)
            {
                MessageBox.Show(Resource1.FrmAPMRefund_Success);
                ClearInput();
            }
            else
            {
                MessageBox.Show(Resource1.FrmAPMRefund_Fail);
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

        private void FrmAPMRefund_Load(object sender, EventArgs e)
        {
            InitComAPM();
            ClearInput();
        }
        private void FrmAPMRefund_FormClosed(object sender, FormClosedEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }
        private void FrmAPMRefund_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {                
                case Keys.F10:
                    if (this.btnOK.Visible && this.btnOK.Enabled)
                    {
                        btnOK_Click(this.btnOK, EventArgs.Empty);
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
                if (this.txtTurnbackMoney.DecimalValue != _cardInfo.TotalPaidFee)
                {
                    if (MessageBox.Show(Resource1.FrmAPMRefund_Continue, Resources.Resource1.Form_Alert,
                                MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else if (MessageBox.Show(Resource1.FrmAPMRefund_Comfirm, Resources.Resource1.Form_Alert,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Refund(_cardInfo);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInput();
        }
        private void FrmAPMRefund_Activated(object sender, EventArgs e)
        {
            this.btnOK.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.APMRefund);
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
        }
        private void FrmAPMRefund_Deactivate(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }
        #endregion
    }
}
