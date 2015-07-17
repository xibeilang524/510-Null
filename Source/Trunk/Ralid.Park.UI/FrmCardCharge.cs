using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model ;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.UI.Resources;
using Ralid.Park.BLL ;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardCharge : Form
    {
        public FrmCardCharge()
        {
            InitializeComponent();
        }

        #region 私有变量
        private bool readCard = false;//是否读到需操作的卡片，用于写卡模式
        #endregion

        #region 公共属性和事件
        public CardInfo ChargingCard { get; set; }
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        #endregion

        #region 私有方法
        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!this.chkWriteCard.Checked) return;
                //检查是否重新发行的卡片
                if (ChargingCard != null &&
                    !CardOperationManager.Instance.CheckReadCardIDWithMessage(ChargingCard.CardID, e.CardID))
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
                    if (ChargingCard != null)
                    {
                        CardDateResolver.Instance.SetCardInfoFromData(ChargingCard, e[GlobalVariables.ParkingSection], true);
                    }
                    else
                    {
                        ChargingCard = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);
                    }
                    this.ucCardInfo.Card = ChargingCard;
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
            //如果卡片的余额为正,且充值金额为负(用于冲掉多充的金额),则相加后的卡片余额为能小于零
            if (ChargingCard.Balance + txtChargeAmount.DecimalValue < 0)
            {
                MessageBox.Show(Resource1.FrmCardCharge_InvalidAmounts);
                return false;
            }
            if (ChargingCard.Balance + txtChargeAmount.DecimalValue > 99999999.99M)
            {
                MessageBox.Show(string.Format(Resource1.FrmCardCharge_AmountsOver, "99999999.99"));
                return false;
            }
            if (this.chkWriteCard.Checked)
            {
                if (ChargingCard.Balance + txtChargeAmount.DecimalValue > 167772.15M)
                {
                    MessageBox.Show(string.Format(Resource1.FrmCardCharge_AmountsOver, "167772.15"));
                    return false;
                }
                if (!readCard)
                {
                    MessageBox.Show(Resource1.FrmCardCharge_ReadCard);
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 窗体事件
        private void FrmCardCharge_Load(object sender, EventArgs e)
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.ucCardInfo.Init();
            this.ucCardInfo.UseToShow();
            this.comPaymentMode.Init();
            this.comPaymentMode.SelectedPaymentMode = PaymentMode.Cash;
            if (ChargingCard != null)
            {
                //获取最新的卡片信息，这是为了防止用户一直打开卡片管理，而使用的卡片信息是缓存信息，导致充值的卡片信息不是最新的
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = bll.GetCardByID(ChargingCard.CardID).QueryObject;
                if (card != null)
                {
                    ChargingCard = card;

                    this.ucCardInfo.Card = ChargingCard;
                    if (ChargingCard.ValidDate > this.dtValidDate.MaxDate)
                    {
                        this.dtValidDate.Value = this.dtValidDate.MaxDate;
                    }
                    else if (ChargingCard.ValidDate < this.dtValidDate.MinDate)
                    {
                        this.dtValidDate.Value = this.dtValidDate.MinDate;
                    }
                    else
                    {
                        this.dtValidDate.Value = ChargingCard.ValidDate;
                    }
                    if (!ChargingCard.IsCardList)
                    {
                        //不是卡片名单时，不需要进行写卡
                        this.chkWriteCard.Checked = false;
                        this.chkWriteCard.Enabled = false;
                    }
                }
                else
                {
                    this.btnOk.Enabled = false;
                    MessageBox.Show(Resource1.FrmMain_NoCard);
                }
            }

            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChargingCard != null)
                {
                    if (CheckInput())
                    {
                        if (this.txtRecieveMoney.DecimalValue <= 0)
                        {
                            if (MessageBox.Show(Resource1.FrmCardPaying_MoneyLittleQuery, Resource1.Form_Alert, MessageBoxButtons.YesNo) == DialogResult.No) return;
                        }
                        //写卡模式时，先读取卡片信息
                        if (this.chkWriteCard.Checked)
                        {
                            if (!CardOperationManager.Instance.CheckCardWithMessage(ChargingCard.CardID)) return;
                        }

                        CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                        decimal chargeAmount = this.txtChargeAmount.DecimalValue;
                        decimal recieveMoney = this.txtRecieveMoney.DecimalValue;
                        DateTime newValid = this.dtValidDate.Value.Date.AddDays(1).AddSeconds(-1);
                        bool keepParkingStatus = !AppSettings.CurrentSetting.EnableWriteCard || ChargingCard.OnlineHandleWhenOfflineMode;//写卡模式时，脱机处理卡片不需要保持卡片数据库中的运行状态
                        CommandResult result = bll.CardCharge(ChargingCard, chargeAmount, recieveMoney, this.comPaymentMode.SelectedPaymentMode, newValid, this.txtMemo.Text, keepParkingStatus);
                        if (result.Result == ResultCode.Successful)
                        {
                            //写卡模式时，将卡片信息写入卡片，这里会使用循环写卡，直到成功或用户取消
                            if (this.chkWriteCard.Checked)
                            {
                                CardOperationManager.Instance.WriteCardLoop(ChargingCard);
                            }

                            if (ItemUpdated != null) ItemUpdated(this, new ItemUpdatedEventArgs(ChargingCard));

                            if (DataBaseConnectionsManager.Current.StandbyConnected)
                            {
                                //备用数据连上时，同步到备用数据库
                                bll.SyncCardToDatabaseWithoutPaymentInfo(ChargingCard, AppSettings.CurrentSetting.CurrentStandbyConnect);
                            }

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(result.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtChargeAmount_TextChanged(object sender, EventArgs e)
        {
            this.txtRecieveMoney.DecimalValue = this.txtChargeAmount.DecimalValue;
        }

        #endregion

        private void FrmCardCharge_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            }
        }
    }
}
