using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.UserControls;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.ParkAdapter;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI
{
    public partial class FrmManageCardDetail : FrmDetailBase
    {
        public FrmManageCardDetail()
        {
            InitializeComponent();
        }

        #region 私有变量
        /// <summary>
        /// 读取到的卡片
        /// </summary>
        private CardInfo _ReadCard = null;
        #endregion

        #region 重写基类方法
        protected override void InitControls()
        {
            this.comCardType.Init();
            this.operatorComboBox1.Init();
            this.dtValidDate.Value = new DateTime(2099, 12, 31);
            this.lblReceivedFees.Text = string.Empty;
            if (IsAdding)
            {
                this.Text = Resource1.FrmManageCardDetail_AddCard;
            }
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditCard);
        }

        protected override void ItemShowing()
        {
            CardInfo info = this.UpdatingItem as CardInfo;
            ShowCardInfo(info);
        }

        protected override object GetItemFromInput()
        {
            CardInfo info;
            if (IsAdding)
            {
                info = new CardInfo();
                info.Status = CardStatus.Enabled;
            }
            else
            {
                info = this.UpdatingItem as CardInfo;
            }
            info.CardID = this.txtCardID.Text;
            info.CardType = this.comCardType.SelectedCardType;
            info.ValidDate = dtValidDate.Value;
            info.OnlineHandleWhenOfflineMode = false;
            OperatorInfo owner = this.operatorComboBox1.SelectecOperator;
            if (owner == null)
            {
                info.OwnerName = info.CardType.ToString();
                info.CardNum = 255;
            }
            else
            {
                info.OwnerName = owner.OperatorName;
                info.CardNum = owner.OperatorNum;
            }
            return info;
        }

        protected override bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtCardID.Text.Trim()))
            {
                MessageBox.Show(Resource1.FrmManageCardDetail_InvalidCardID);
                return false;
            }
            if (comCardType.SelectedCardType == null)
            {
                MessageBox.Show(Resource1.FrmManageCardDetail_InvalidCardType);
                return false;
            }
            if (this.operatorComboBox1.SelectecOperator == null)
            {
                MessageBox.Show(Resource1.FrmManageCardDetail_InvalidOwner);
                return false;
            }
            if (this.chkWriteCard.Checked)
            {
                //写卡模式时，先读取卡片信息
                CardOperationResultCode result = CardOperationManager.Instance.CheckCard(this.txtCardID.Text);
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

        protected override CommandResult AddItem(object addingItem)
        {
            CardInfo card = (CardInfo)addingItem;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            CommandResult result = bll.AddCard(card);
            if (result.Result == ResultCode.Successful)
            {
                WriteCard(card);

                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    //备用数据连上时，同步到备用数据库
                    bll.SyncCardToDatabaseWithoutPaymentInfo((CardInfo)addingItem, AppSettings.CurrentSetting.CurrentStandbyConnect);
                }
            }
            return result;
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            CardInfo card = (CardInfo)updatingItem;
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            CommandResult result = bll.UpdateCardAll(card);
            if (result.Result == ResultCode.Successful)
            {
                WriteCard(card);
                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    //备用数据连上时，同步到备用数据库
                    bll.SyncCardToDatabaseWithoutPaymentInfo((CardInfo)updatingItem, AppSettings.CurrentSetting.CurrentStandbyConnect);
                }
            }
            return result;
        }
        #endregion

        #region 私有方法
        private void WriteCard(CardInfo info)
        {
            //写卡模式时，将卡片信息写入卡片，这里会使用循环写卡，直到成功或用户取消
            if (this.chkWriteCard.Checked)
            {
                CardOperationManager.Instance.WriteCardLoop(info);
            }
        }

        private void ShowCardInfo(CardInfo info)
        {
            this.txtCardID.Text = info.CardID;
            this.txtCardID.Enabled = false;
            this.txtCardID.BackColor = Color.White;
            this.comCardType.SelectedCardType = info.CardType;
            this.operatorComboBox1.OperatorName = info.OwnerName;
            this.dtValidDate.Value = info.ValidDate;
            this.lblReceivedFees.Text = info.ParkFee.ToString("F2");
        }
        #endregion

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (AppSettings.CurrentSetting.EnableWriteCard)
                {
                    _ReadCard = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);
                }
                this.txtCardID.Text = e.CardID;
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

        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            string cardID = this.txtCardID.Text.Trim(' ', '\n');
            if (!string.IsNullOrEmpty(cardID))
            {
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                QueryResult<CardInfo> result = bll.GetCardByID(cardID);
                if (result.Result == ResultCode.Successful)
                {
                    CardInfo info = result.QueryObject;
                    if (_ReadCard != null)
                    {
                        CardDateResolver.Instance.CopyCardDataToCard(info, _ReadCard);
                    }

                    if (!info.CardType.IsManagedCard)
                    {
                        MessageBox.Show(string.Format(Resource1.FrmManageCardDetail_NotManagement, cardID));
                        this.txtCardID.Focus();
                        btnOk.Enabled = false;
                    }
                    else
                    {
                        UpdatingItem = info;
                        this.IsAdding = false;
                        btnOk.Enabled = true;
                        ShowCardInfo(info);
                    }
                }
                else
                {
                    if (_ReadCard != null)
                    {
                        ShowCardInfo(_ReadCard);
                    }
                    this.IsAdding = true;
                    btnOk.Enabled = true;
                }
            }
        }

        private void txtCardID_Enter(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
        }

        private void txtCardID_Leave(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }

        private void FrmManageCardDetail_Load(object sender, EventArgs e)
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.txtCardID.TextChanged += txtCardID_TextChanged;
        }

        private void FrmManageCardDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }

    }
}
