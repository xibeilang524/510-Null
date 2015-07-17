using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.UI.Resources;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardDefer : Form
    {
        public FrmCardDefer()
        {
            InitializeComponent();
        }

        private bool readCard = false;//是否读到需操作的卡片，用于写卡模式

        private CardInfo _deferingCard;
        public CardInfo DeferingCard
        {
            get
            {
                return _deferingCard;
            }
            set
            {
                _deferingCard = value;
                DateTime dt = _deferingCard.ValidDate.AddDays(1);
                DateTime edt = dt.AddMonths(1).AddDays(-1);
                this.dtBegin.Value = dt > this.dtBegin.MaxDate ? this.dtBegin.MaxDate : dt;
                this.dtEnd.Value = edt > this.dtBegin.MaxDate ? this.dtBegin.MaxDate : edt;
            }
        }

        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;


        #region 私有方法
        private bool CheckInput()
        {
            if (this.chkWriteCard.Checked)
            {
                if (!readCard)
                {
                    MessageBox.Show(Resource1.FrmCardDefer_ReadCard);
                    return false;
                }
            }
            return true;
        }
        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!chkWriteCard.Checked) return;
                //检查是否重新发行的卡片
                if (DeferingCard != null &&
                    !CardOperationManager.Instance.CheckReadCardIDWithMessage(DeferingCard.CardID, e.CardID))
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
                    if (DeferingCard != null)
                    {
                        CardDateResolver.Instance.SetCardInfoFromData(DeferingCard, e[GlobalVariables.ParkingSection], true);
                    }
                    else
                    {
                        DeferingCard = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);
                    }
                    readCard = true;
                    this.ucCardInfo.Card = DeferingCard;
                    
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

        private void FrmCardDefer_Load(object sender, EventArgs e)
        {

            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.ucCardInfo.Init();
            this.ucCardInfo.UseToShow();
            this.comPaymentMode.Init();
            this.comPaymentMode.SelectedPaymentMode = PaymentMode.Cash;
            if (DeferingCard != null)
            {
                
                //获取最新的卡片信息，这是为了防止用户一直打开卡片管理，而使用的卡片信息是缓存信息，导致延期的卡片信息不是最新的
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CardInfo card = bll.GetCardByID(DeferingCard.CardID).QueryObject;
                if (card != null)
                {
                    DeferingCard = card;

                    ucCardInfo.Card = DeferingCard;

                    if (!DeferingCard.IsCardList)
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
                if (DeferingCard != null)
                {
                    if (CheckInput())
                    {
                        if (txtRecieveMoney.DecimalValue <= 0)
                        {
                            if (MessageBox.Show(Resource1.FrmCardPaying_MoneyLittleQuery, Resource1.Form_Alert, MessageBoxButtons.YesNo) == DialogResult.No) return;
                        }

                        //写卡模式时，先读取卡片信息
                        if (this.chkWriteCard.Checked)
                        {
                            if (!CardOperationManager.Instance.CheckCardWithMessage(DeferingCard.CardID)) return;
                        }

                        CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                        decimal recieveMoney = this.txtRecieveMoney.DecimalValue;
                        DateTime newValidDate = this.dtEnd.Value.Date.AddDays(1).AddSeconds(-1);
                        DateTimeRange dr = new DateTimeRange(dtBegin.Value, dtEnd.Value);
                        bool keepParkingStatus = !AppSettings.CurrentSetting.EnableWriteCard || DeferingCard.OnlineHandleWhenOfflineMode;//写卡模式时，脱机处理卡片不需要保持卡片数据库中的运行状态
                        CommandResult result = bll.CardDefer(DeferingCard, dr, this.comPaymentMode.SelectedPaymentMode, recieveMoney, this.txtMemo.Text, keepParkingStatus);
                        if (result.Result == ResultCode.Successful)
                        {
                            //写卡模式时，将卡片信息写入卡片，这里会使用循环写卡，直到成功或用户取消
                            if (this.chkWriteCard.Checked)
                            {
                                CardOperationManager.Instance.WriteCardLoop(DeferingCard);
                            }

                            if (ItemUpdated != null) ItemUpdated(this, new ItemUpdatedEventArgs(DeferingCard));

                            if (DataBaseConnectionsManager.Current.StandbyConnected)
                            {
                                //备用数据连上时，同步到备用数据库
                                bll.SyncCardToDatabaseWithoutPaymentInfo(DeferingCard, AppSettings.CurrentSetting.CurrentStandbyConnect);
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

        private void FrmCardDefer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            }
        }
    }
}
