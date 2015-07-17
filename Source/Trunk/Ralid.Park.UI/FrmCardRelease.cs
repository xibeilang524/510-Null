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
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
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

        #region 私有事件
        private void OptionChangedHandler(object sender, EventArgs e)
        {
            if (chkWriteCard.Visible)
            {
                if (this.chkWriteCard.Enabled)
                {
                    if (sender is CheckBox)
                    {
                        CheckBox chk = sender as CheckBox;
                        if (chk.Name == "chkOnlineHandleWhenOfflineMode")
                        {
                            this.chkWriteCard.Checked = !chk.Checked;
                        }
                    }
                }
                if (sender is RadioButton)
                {
                    RadioButton rdb = sender as RadioButton;
                    if (rdb.Name == "rdbCardList")
                    {
                        if (!rdb.Checked)
                        {
                            this.chkWriteCard.Checked = false;
                        }
                        else
                        {
                            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
                        }
                        this.chkWriteCard.Enabled = rdb.Checked;
                    }
                }
            }
        }
        #endregion

        #region 私有方法

        private void InitControls()
        {
            this.ucCard1.Init();
            this.ucCard1.UseToRelease();
            this.ucCard1.txtCardID.TextChanged += txtCardID_TextChanged;
            this.ucCard1.txtCarPlate.TextChanged += this.txtCarPlate_TextChanged;
            this.ucCard1.rdbCardList.CheckedChanged += this.rdbCardList_CheckedChanged;
            this.comPaymentMode.Init();
            this.comPaymentMode.SelectedPaymentMode = PaymentMode.Cash;
            this.ucCard1.OptionChangedEvent += this.OptionChangedHandler;
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

                if (ReleasingCard != null)
                {
                    this.ucCard1.Card = ReleasingCard;
                }

                readCard = true;
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
            //这里先停止读卡是为了防止有些读卡器会不停读卡，在卡片操作过程中，读卡器又读到卡片，影响操作
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).StopReadCard();
            try
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
                                CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
                                return;
                            }
                        }

                        CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                        CommandResult result = null;
                        if (ReleasingCard.ListType == CardListType.Card)
                        {
                            result = bll.CardRelease(ReleasingCard, txtRecieveMoney.DecimalValue, comPaymentMode.SelectedPaymentMode, txtMemo.Text.Trim());
                        }
                        else
                        {
                            //发行车牌名单，生成卡号最多重试10次
                            result = bll.CarPlateRelease(ReleasingCard, txtRecieveMoney.DecimalValue, comPaymentMode.SelectedPaymentMode, txtMemo.Text.Trim(), 10);
                        }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
        }

        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            //卡片名单时才检查
            if (this.ucCard1.rdbCardList.Checked)
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
                            this.ucCard1.txtCardID.Focus();
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
        }

        private void txtCarPlate_TextChanged(object sender, EventArgs e)
        {
            //车牌名单时才检查
            if (this.ucCard1.rdbCarPlateList.Checked)
            {
                string carPlate = this.ucCard1.txtCarPlate.Text.Trim(' ', '\n');
                if (!string.IsNullOrEmpty(carPlate))
                {
                    CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                    CardSearchCondition search = new CardSearchCondition();
                    search.ListType = CardListType.CarPlate;
                    search.ListCarPlate = carPlate;
                    QueryResultList<CardInfo> result = bll.GetCards(search);
                    if (result.Result == ResultCode.Successful
                        && result.QueryObjects != null
                        && result.QueryObjects.Count > 0)
                    {
                        CardInfo info = result.QueryObjects[0];
                        if (!info.ReleasAble)
                        {
                            MessageBox.Show(string.Format(Resources.Resource1.UcCard_CannotRelease, carPlate));
                            this.ucCard1.txtCarPlate.Focus();
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
                        this.ucCard1.txtCardID.Text = string.Empty;
                        this._isAdding = true;
                        btnOk.Enabled = true;
                    }
                }
            }
        }

        private void rdbCardList_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ucCard1.rdbCardList.Checked)
            {
                this.ucCard1.txtCardID.Text = string.Empty;
                this.ucCard1.txtCardID.Enabled = true;
                txtCardID_TextChanged(sender, e);
            }
            else
            {
                this.ucCard1.txtCardID.Enabled = false;
                txtCarPlate_TextChanged(sender, e);
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
                this.ucCard1.txtCardID.Text = e.CardID;
                if (AppSettings.CurrentSetting.EnableWriteCard)
                {
                    CardReadOffLineHandler(e);
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
