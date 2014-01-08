using System;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardDetail :FrmDetailBase 
    {
        CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect); 
        public FrmCardDetail()
        {
            InitializeComponent();
        }
        #region 私有变量
        private bool readCard = false;//是否读到需操作的卡片，用于写卡模式
        #endregion

        #region 私有方法
        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!chkWriteCard.Checked) return;

                CardInfo card = (UpdatingItem as CardInfo).Clone() ;

                //检查是否当前操作的卡片
                if (card != null &&
                    !CardOperationManager.Instance.CheckReadCardIDWithMessage(card.CardID, e.CardID))
                {
                    readCard = false;
                    return;
                }

                if (!CardOperationManager.Instance.CheckReadDateWithMessage(e.ParkingDate))
                {
                    readCard = false;
                    return;
                }
                else
                {
                    //转换读出的卡片数据
                    if (card != null)
                    {
                        CardDateResolver.Instance.SetCardInfoFromData(card, e.ParkingDate, true);
                    }
                    else
                    {
                        card = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e.ParkingDate);
                    }
                    this.ucCardInfo.Card = card;
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

        private void WriteCard(CardInfo info)
        {
            //写卡模式时，将卡片信息写入卡片，这里会使用循环写卡，直到成功或用户取消
            if (this.chkWriteCard.Checked)
            {
                CardOperationManager.Instance.WriteCardLoop(info);
            }
        }

        #endregion

        #region 处理基类事件
        protected override void InitControls()
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.ucCardInfo.Init();
            this.ucCardInfo.UseToEdit();
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditCard);

            //增加时该处启动读卡器
            if (AppSettings.CurrentSetting.EnableWriteCard && IsAdding)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            }
        }

        protected override void ItemShowing()
        {
            CardInfo info = (CardInfo)UpdatingItem;
            this.ucCardInfo.Card = info;
            this.Text = info.CardID;

            //修改时该处启动读卡器
            if (AppSettings.CurrentSetting.EnableWriteCard && !IsAdding)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            }
        }

        protected override object GetItemFromInput()
        {
            return ucCardInfo.Card;
        }

        protected override bool CheckInput()
        {
            if (this.chkWriteCard.Checked)
            {
                if (!readCard)
                {
                    MessageBox.Show(Resources.Resource1.FrmCardDetail_ReadCard);
                    return false;
                }

                //读取卡片信息以确定卡片是否存在
                CardInfo card = ucCardInfo.Card;
                if (!CardOperationManager.Instance.CheckCardWithMessage(card.CardID)) return false;
            }

            if (ucCardInfo.Card == null) return false;

            return true;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            CommandResult result = bll.AddCard((CardInfo)addingItem);
            if (result.Result == ResultCode.Successful)
            {
                WriteCard((CardInfo)addingItem);
            }
            return result;
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            CommandResult result = this.chkWriteCard.Checked ? bll.UpdateCardAll((CardInfo)updatingItem) : bll.UpdateCard((CardInfo)updatingItem);
            if (result.Result == ResultCode.Successful)
            {
                WriteCard((CardInfo)updatingItem);
            }
            return result;
        }
       #endregion

        #region 窗体事件
        private void FrmCardDetail_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            }
        }
        #endregion
    }
}
