using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BLL;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI
{
    public partial class FrmOperatorCardReader : Form
    {
        public FrmOperatorCardReader()
        {
            InitializeComponent();

            OpeCardName = "授权卡";//?是否是授权卡？
            _checkMode = 0;
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置从发卡机上读到的卡号
        /// </summary>
        public string OpeCardID { get; set; }
        /// <summary>
        /// 获取或设置需要读取的卡片类型名称
        /// </summary>
        public string OpeCardName { get; set; }

        /// <summary>
        /// 获取或设置缴费卡片的卡号
        /// </summary>
        public string MoneyCardID { get; set; }

        /// <summary>
        /// 获取或设置授权卡片的卡号
        /// </summary>
        public string OperatorCardID { get; set; }

        /// <summary>
        /// 获取或设置授权卡卡片的持卡人
        /// </summary>
        public string OperatorCardOwnerName { get; set; }

        private int _checkMode;//标示授权卡是否有效

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void FrmOperatorCardReader_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void FrmOperatorCardReader_Load(object sender, EventArgs e)
        {
            //this.Text = "请刷" + OpeCardName;
            //this.label1.Text = "请在发卡机上刷" + OpeCardName;
            this.Text = "请刷授权卡";
            this.label1.Text = "请在发卡机上刷授权卡";
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadEventHandler);
        }

        private void FrmOperatorCardReader_FormClosed(object sender, FormClosedEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadEventHandler);
        }

        private void CardReadEventHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                OpeCardID = e.CardID;
                if (_checkMode == 0)
                {
                    CheckOperatorCard();
                }
                else
                {
                    if (AppSettings.CurrentSetting.EnableWriteCard)//启用脱机模式
                    {
                        if (e.CardID == MoneyCardID)
                            this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                    }
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

        /// <summary>
        /// 验证授权卡的有效性
        /// </summary>
        /// <returns></returns>
        private bool CheckOperatorCard()
        {
            string cardid = OpeCardID;
            if (DataBaseConnectionsManager.Current.MasterConnected || DataBaseConnectionsManager.Current.StandbyConnected)
            {
                CardInfo card = null;
                if (DataBaseConnectionsManager.Current.MasterConnected)
                {
                    card = (new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect)).GetCardByID(cardid).QueryObject;
                }
                else
                {
                    card = (new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect)).GetCardByID(cardid).QueryObject;
                }

                if (card != null && card.CardType == CardType.OperatorCard && card.OperatorAllowSwitchCarType)
                {
                    OperatorCardID = card.CardID;
                    OperatorCardOwnerName = card.OwnerName;
                    _checkMode = 1;//授权卡有效
                    this.Text = "请刷缴费卡片";
                    this.label1.Text = "请放回缴费卡片";
                    return true;
                }
                else
                {
                    MessageBox.Show(string.Format("卡号为 {0} 的卡不存在或者不是授权卡，请刷授权卡", cardid), Resource1.Form_Alert);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("数据库连接失败！");
                return false;
            }
        }

    }
}
