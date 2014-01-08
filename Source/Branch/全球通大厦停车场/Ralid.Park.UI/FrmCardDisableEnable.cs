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
using Ralid.Park.UI.Resources;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardDisableEnable : Form
    {
        public FrmCardDisableEnable()
        {
            InitializeComponent();
        }

        #region 私有变量
        private bool readCard = false;//是否读到需操作的卡片，用于写卡模式
        #endregion

        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        public CardInfo DisableEnableCard { get; set; }

        #region 私有方法

        private void ShowCardInfo(CardInfo info)
        {
            string caption = null;
            this.ucCardInfo.Card = info;
            if (info.Status == CardStatus.Disabled)
            {
                caption = Resource1.FrmCardDisableEnable_Enable;
            }
            else
            {
                caption = Resource1.FrmCardDisableEnable_Disable;
            }
            this.Text = caption;
            this.groupBox1.Text = caption;
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!this.chkWriteCard.Checked) return;

                //检查是否启用/禁用的卡片
                if (DisableEnableCard != null &&
                    !CardOperationManager.Instance.CheckReadCardIDWithMessage(DisableEnableCard.CardID, e.CardID))
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
                    if (DisableEnableCard != null)
                    {
                        CardDateResolver.Instance.SetCardInfoFromData(DisableEnableCard, e.ParkingDate, true);
                    }
                    else
                    {
                        DisableEnableCard = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e.ParkingDate);
                    }
                    this.ucCardInfo.Card = DisableEnableCard;
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
            if (this.chkWriteCard.Checked)
            {
                if (!readCard)
                {
                    MessageBox.Show(Resource1.FrmCardDisableEnable_ReadCard);
                    return false;
                }

                //写卡模式时，先读取卡片信息
                if (!CardOperationManager.Instance.CheckCardWithMessage(DisableEnableCard.CardID)) return false;
            }
            return true;
        }        
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCardDisableEnable_Load(object sender, EventArgs e)
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.ucCardInfo.Init();
            this.ucCardInfo.UseToShow();
            if (DisableEnableCard != null)
            {
                ShowCardInfo(DisableEnableCard);
            }

            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CommandResult result;
                if (this.DisableEnableCard.Status == CardStatus.Disabled)
                {
                    result = bll.CardEnable(this.DisableEnableCard, this.txtMemo.Text, !AppSettings.CurrentSetting.EnableWriteCard);
                }
                else
                {
                    result = bll.CardDisable(this.DisableEnableCard, this.txtMemo.Text, !AppSettings.CurrentSetting.EnableWriteCard);
                }
                if (result.Result == ResultCode.Successful)
                {

                    //写卡模式时，将卡片信息写入卡片，这里会使用循环写卡，直到成功或用户取消
                    if (this.chkWriteCard.Checked)
                    {
                        CardOperationManager.Instance.WriteCardLoop(DisableEnableCard);                                                
                    }

                    if (ItemUpdated != null) ItemUpdated(this, new ItemUpdatedEventArgs(DisableEnableCard));
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void FrmCardDisableEnable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            }
        }
    }
}
