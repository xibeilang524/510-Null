using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Factory;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardLostRestore :Form 
    {
        public FrmCardLostRestore()
        {
            InitializeComponent();
        }
        #region 私有变量
        private bool readCard = false;//是否读到需操作的卡片，用于写卡模式
        #endregion

        public CardInfo LossRestoreCard { get; set; }
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        private CardPaymentInfo _ParkPayment;

        private void ShowCardInfo(CardInfo info)
        {
            string caption = null; 
            if (LossRestoreCard != null)
            {
                this.ucCardInfo.Card = LossRestoreCard;
                if (LossRestoreCard.Status == CardStatus.Loss)
                {
                    caption = Resources.Resource1.FrmCardLostRestore_Restore;
                    this.txtCardCost.Enabled = false;
                    this.comPaymentMode.Enabled = false;
                }
                else
                {
                    caption = Resources.Resource1.FrmCardLostRestore_Lost;
                    this.txtCardCost.Enabled = true;
                    this.comPaymentMode.Enabled = true;
                }
                this.Text = caption;
                this.groupBox1.Text = caption;
                
                if (LossRestoreCard.Status != CardStatus.Loss && LossRestoreCard.IsInPark)
                {
                    //EntranceBll eBll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);
                    //EntranceInfo eInfo = eBll.GetEntranceInfo(LossRestoreCard.LastEntrance).QueryObject;
                    //int parkID = 0;
                    //if (eInfo != null)
                    //    parkID = eInfo.ParkID;
                    //this.parkCombobox1.SelectedParkID = parkID;

                    //_ParkPayment = CardPaymentInfoFactory.CreateCardPaymentRecord(LossRestoreCard, TariffSetting.Current, LossRestoreCard.CarType, DateTime.Now);
                    //_ParkPayment = CardPaymentInfoFactory.CreateCardPaymentRecord(this.parkCombobox1.SelectedParkID,LossRestoreCard, TariffSetting.Current, LossRestoreCard.CarType, DateTime.Now);
                    _ParkPayment = CardPaymentInfoFactory.CreateCardPaymentRecord(null, LossRestoreCard, TariffSetting.Current, LossRestoreCard.CarType, DateTime.Now);
                    txtParkFee.DecimalValue = _ParkPayment.Accounts;
                    chkPayParkFee.Enabled = true;
                    chkPayParkFee.Checked = true;
                }

                if (!info.IsCardList)
                {
                    //不是卡片名单时，不需要进行写卡
                    this.chkWriteCard.Checked = false;
                    this.chkWriteCard.Enabled = false;
                }
            }
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!this.chkWriteCard.Checked) return;
                //检查是否当前操作的卡片
                if (LossRestoreCard != null &&
                    !CardOperationManager.Instance.CheckReadCardIDWithMessage(LossRestoreCard.CardID, e.CardID))
                {
                    readCard = false;
                    return;
                }

                if (!CardOperationManager.Instance.CheckReadDateWithMessage(e[GlobalVariables.ParkingSection]))
                {
                    readCard = false;
                    return;
                }
                else
                {
                    //转换读出的卡片数据
                    if (LossRestoreCard != null)
                    {
                        CardDateResolver.Instance.SetCardInfoFromData(LossRestoreCard, e[GlobalVariables.ParkingSection], true);
                    }
                    else
                    {
                        LossRestoreCard = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);
                    }
                    this.ucCardInfo.Card = LossRestoreCard;
                    readCard = true;
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

        private bool CheckInput()
        {
            if (this.chkWriteCard.Checked && LossRestoreCard.Status == CardStatus.Loss)//恢复时才检查
            {
                if (!readCard)
                {
                    MessageBox.Show(Resources.Resource1.FrmCardLostRestore_ReadCard);
                    return false;
                }

                //写卡模式时，先读取卡片信息
                if (!CardOperationManager.Instance.CheckCardWithMessage(LossRestoreCard.CardID)) return false;
            }
            return true;
        }

        private void FrmCardLostRestore_Load(object sender, EventArgs e)
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.comPaymentMode.Init();
            this.comPaymentMode.SelectedPaymentMode = PaymentMode.Cash;
            this.ucCardInfo.Init();
            this.ucCardInfo.UseToShow();
            //this.parkCombobox1.Init();
            ShowCardInfo(LossRestoreCard);

            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckInput()) return;

                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CommandResult result;
                if (LossRestoreCard.Status == CardStatus.Loss)
                {
                    result = bll.CardRestore(LossRestoreCard, this.txtMemo.Text, !AppSettings.CurrentSetting.EnableWriteCard);
                }
                else
                {
                    if (chkPayParkFee.Checked && _ParkPayment != null)
                    {
                        _ParkPayment.Paid = txtParkFee.DecimalValue;
                        _ParkPayment.Discount = _ParkPayment.Accounts - _ParkPayment.Paid;
                        result = bll.CardLoss(LossRestoreCard, this.txtMemo.Text, txtCardCost.DecimalValue, comPaymentMode.SelectedPaymentMode, _ParkPayment);
                    }
                    else
                    {
                        result = bll.CardLoss(LossRestoreCard, this.txtMemo.Text, txtCardCost.DecimalValue, comPaymentMode.SelectedPaymentMode);
                    }
                }
                if (result.Result == ResultCode.Successful)
                {

                    //写卡模式时，将卡片信息写入卡片，这里会使用循环写卡，直到成功或用户取消
                    if (this.chkWriteCard.Checked)
                    {
                        //恢复时才将卡片信息写入，挂失时不写入
                        if (LossRestoreCard.Status == CardStatus.Enabled)
                        {
                            CardOperationManager.Instance.WriteCardLoop(LossRestoreCard);
                        }
                    }

                    if (this.ItemUpdated != null) this.ItemUpdated(this, new ItemUpdatedEventArgs(LossRestoreCard));

                    if (DataBaseConnectionsManager.Current.StandbyConnected)
                    {
                        //备用数据连上时，同步到备用数据库
                        bll.SyncCardToDatabaseWithoutPaymentInfo(LossRestoreCard, AppSettings.CurrentSetting.CurrentStandbyConnect);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkPayParkFee_CheckedChanged(object sender, EventArgs e)
        {
            this.txtParkFee.Enabled = chkPayParkFee.Checked;
            this.lblParkFee.Text = (txtCardCost.DecimalValue + (txtParkFee.Enabled ? txtParkFee.DecimalValue : 0)).ToString("N2");
        }

        private void txtCardCost_TextChanged(object sender, EventArgs e)
        {
            this.lblParkFee.Text = (txtCardCost.DecimalValue + (txtParkFee.Enabled ? txtParkFee.DecimalValue : 0)).ToString("N2");
        }

        private void txtParkFee_TextChanged(object sender, EventArgs e)
        {
            this.lblParkFee.Text = (txtCardCost.DecimalValue + (txtParkFee.Enabled ? txtParkFee.DecimalValue : 0)).ToString("N2");
        }

        private void FrmCardLostRestore_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            }
        }

        private void parkCombobox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LossRestoreCard != null)
            {
                _ParkPayment = CardPaymentInfoFactory.CreateCardPaymentRecord(this.parkCombobox1.SelectedParkID, LossRestoreCard, TariffSetting.Current, LossRestoreCard.CarType, DateTime.Now);
                txtParkFee.DecimalValue = _ParkPayment.Accounts;
            }
        }
    }
}
