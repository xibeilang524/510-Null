using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardRelease : Form
    {
        public FrmCardRelease()
        {
            InitializeComponent();
        }

        public event EventHandler<ItemAddedEventArgs> ItemAdded;
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        public CardInfo ReleasingCard { get; set; }

        #region 私有变量
        private bool _isAdding;
        private bool _releaseAgain;//是否重新发行卡片
        private bool readCard = false;//是否读到需操作的卡片，用于写卡模式
        #endregion

        #region 私有方法

        private void InitControls()
        {
            this.ucCard1.Init();
            this.ucCard1.UseToRelease();
            this.ucCard1.txtCardID.TextChanged += txtCardID_TextChanged;
            this.comPaymentMode.Init();
            this.comPaymentMode.SelectedPaymentMode = PaymentMode.Cash;
        }

        /// <summary>
        /// 写卡模式读到卡片处理
        /// </summary>
        /// <param name="e"></param>
        private void CardReadOffLineHandler(CardReadEventArgs e)
        {

            if (_releaseAgain && ReleasingCard != null)
            {
                //检查是否重新发行的卡片
                if (!CardOperationManager.Instance.CheckReadCardIDWithMessage(ReleasingCard.CardID, e.CardID))
                {
                    readCard = false;
                    return;
                }
            }
            else
            {
                //不是重新发行，将当期卡片清空
                ReleasingCard = null;
            }

            if (e[GlobalVariables.ParkingSection] == null)
            {
                readCard = true;
            }
            else
            {
                //转换读出的卡片数据
                if (ReleasingCard == null)
                {
                    ReleasingCard = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);
                }
                else
                {
                    CardDateResolver.Instance.SetCardInfoFromData(ReleasingCard, e[GlobalVariables.ParkingSection], true);
                }

                this.ucCard1.Card = ReleasingCard;

                readCard = true;
            }

            if (ReleasingCard == null)
            {
                this.ucCard1.txtCardID.Text = e.CardID;
            }
        }

        private bool CheckInput()
        {
            if (this.chkWriteCard.Checked)
            {
                if (!readCard)
                {
                    MessageBox.Show(Resources.Resource1.FrmCardRelease_ReadCard);
                    return false;
                }

                //写卡模式时，先读取卡片信息
                CardOperationResultCode result = CardOperationManager.Instance.CheckCard(ReleasingCard.CardID);
                if (result == CardOperationResultCode.AuthFail)//初始化卡片
                {
                    //初始化用户卡，修改扇区2密钥
                    result = CardOperationManager.Instance.InitCard();
                    if (result != CardOperationResultCode.Success)
                    {
                        //修改扇区密钥失败
                        result = CardOperationResultCode.InitKeyFail;
                    }

                }
                if (!CardOperationManager.Instance.ShowResultMessage(result))
                {
                    return false;
                }                
            }
            return true;
        }        
        #endregion

        private void FrmCardRelease_Load(object sender, EventArgs e)
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            InitControls();
            if (ReleasingCard != null)
            {
                _isAdding = false;
                this.ucCard1.Card = ReleasingCard;

                _releaseAgain = true;

            }
            else
            {
                _releaseAgain = false;
            }
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ReleasingCard = this.ucCard1.Card;
            if (ReleasingCard != null)
            {
                if (CheckInput())
                {
                    if (txtRecieveMoney.DecimalValue <= 0)
                    {
                        if (MessageBox.Show(Resources.Resource1.FrmCardPaying_MoneyLittleQuery, Resources.Resource1.Form_Alert,
                            MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                    CommandResult result = bll.CardRelease(ReleasingCard, txtRecieveMoney.DecimalValue, comPaymentMode.SelectedPaymentMode, txtMemo.Text.Trim());
                    if (result.Result == ResultCode.Successful)
                    {
                        //写卡模式时，将卡片信息写入卡片，这里会使用循环写卡，直到成功或用户取消
                        if (this.chkWriteCard.Checked)
                        {
                            CardOperationManager.Instance.WriteCardLoop(ReleasingCard);
                        }

                        if (!_isAdding)
                        {
                            if (ItemUpdated != null) ItemUpdated(this, new ItemUpdatedEventArgs(ReleasingCard));
                        }
                        else
                        {
                            if (ItemAdded != null) ItemAdded(this, new ItemAddedEventArgs(ReleasingCard));
                        }
                        this.ucCard1.Clear();
                        this.txtRecieveMoney.DecimalValue = 0;

                        if (DataBaseConnectionsManager.Current.StandbyConnected)
                        {
                            //备用数据连上时，同步到备用数据库
                            bll.SyncCardToDatabaseWithoutPaymentInfo(ReleasingCard, AppSettings.CurrentSetting.CurrentStandbyConnect);
                        }
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
            }
        }

        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            string cardID = this.ucCard1.txtCardID.Text.Trim(' ', '\n');
            if (!string.IsNullOrEmpty(cardID))
            {
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                QueryResult<CardInfo> result = bll.GetCardByID(cardID);
                if (result.Result == ResultCode.Successful)
                {
                    CardInfo info = result.QueryObject;
                    if (!info.ReleasAble)
                    {
                        MessageBox.Show(string.Format(Resources.Resource1.UcCard_CannotRelease, cardID));
                        this.ucCard1 .txtCardID.Focus();
                        btnOk.Enabled = false;
                    }
                    else
                    {
                        ReleasingCard = info;
                        this.ucCard1.Card = ReleasingCard;
                        this._isAdding = false;
                        btnOk.Enabled = true;
                    }
                }
                else
                {
                    this._isAdding = true;
                    btnOk.Enabled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (AppSettings.CurrentSetting.EnableWriteCard)
                {
                    CardReadOffLineHandler(e);
                }
                else
                {
                    this.ucCard1.txtCardID.Text = e.CardID;
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

        private void FrmCardRelease_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }
                
    }
}
