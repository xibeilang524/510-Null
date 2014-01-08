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
using Ralid.Park.BusinessModel.Model;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmOperatorCardCashComfirm : Form
    {
        #region 构造函数
        public FrmOperatorCardCashComfirm()
        {
            InitializeComponent();
        }

        #endregion
        
        #region 公共属性
        /// <summary>
        /// 获取或设置结帐操作员
        /// </summary>
        public OperatorInfo Operator { get; set; }
        /// <summary>
        /// 获取或设置用于收费的操作员卡
        /// </summary>
        public CardInfo OperatorCard{ get; set; }
        #endregion

        #region 私有事件
        private void ShowMsg(string msg)
        {
            this.lblMsg.Text = msg;
            int left = (this.Width - this.lblMsg.Width) / 2;
            this.lblMsg.Location = new Point(left > 0 ? left : 0, this.lblMsg.Location.Y);
        }
        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!string.IsNullOrEmpty(e.CardID))
                {
                    CardInfo card = CardDateResolver.Instance.GetCardInfoFromData(e.CardID, e[GlobalVariables.ParkingSection]);

                    string msg = string.Empty;
                    if (card == null)
                    {
                        msg = Resources.Resource1.FrmOperatorCardCashComfirm_InvalidCard;
                    }
                    else if (!card.CardType.IsOperatorCard)
                    {
                        msg = Resources.Resource1.FrmOperatorCardCashComfirm_NotOperatorCard;
                    }
                    else
                    {
                        CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                        CardInfo info = bll.GetCardByID(e.CardID).QueryObject;
                        if (info == null)
                        {
                            msg = Resources.Resource1.FrmOperatorCardCashComfirm_InvalidCard;
                        }
                        else if (Operator == null || info.OwnerName != Operator.OperatorName)
                        {
                            msg = Resources.Resource1.FrmOperatorCardCashComfirm_NotCurrentOperatorCard;
                        }
                    }

                    if (!string.IsNullOrEmpty(msg))
                    {
                        ShowMsg(msg);
                        return;
                    }

                    this.OperatorCard = card;
                    this.DialogResult = DialogResult.OK;
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
        private void FrmOperatorCardCashComfirm_Load(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
        }
        private void FrmOperatorCardCashComfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }
        #endregion

    }
}
