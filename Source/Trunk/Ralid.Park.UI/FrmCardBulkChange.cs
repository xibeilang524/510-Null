using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ralid.Park .BLL ;
using Ralid.Park .BusinessModel .Model ;
using Ralid.Park .BusinessModel .Result ;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardBulkChange : Form
    {
        public FrmCardBulkChange()
        {
            InitializeComponent();
        }

        #region 私有属性
        private List<CardInfo> _changedCards = new List<CardInfo>();//已经修改的卡片
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置要批量修改的卡片
        /// </summary>
        public List<CardInfo> BulkChangeCards { get; set; }
        /// <summary>
        /// 当更新成功一张卡片时产生此事件
        /// </summary>
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        #endregion

        #region 私有方法
        /// <summary>
        /// 设置卡片属性
        /// </summary>
        /// <param name="card"></param>
        private void SetCardFromInput(CardInfo card)
        {
            card.CanRepeatIn = this.chkRepeatIn.Checked;
            card.CanRepeatOut = this.chkRepeatOut.Checked;
            card.HolidayEnabled = this.chkHoliday.Checked;
            card.WithCount = this.chkWithCount.Checked;
            card.CanEnterWhenFull = this.chkCanEnterWhenFull.Checked;
            card.EnableWhenExpired = this.chkEnableWhenExpired.Checked;
            card.OnlineHandleWhenOfflineMode = this.chkOnlineHandleWhenOfflineMode.Checked;
            card.AccessID = this.comAccessLevel.AccesslevelID;
        }

        /// <summary>
        /// 读卡
        /// </summary>
        private void ReadCard()
        {
            progressBar1.Maximum = BulkChangeCards.Count;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            this.comAccessLevel.Enabled = false;
            this.groupBox3.Enabled = false;
            this.lblAlarm.Text = string.Format(Resources.Resource1.FrmCardBulkChange_PleaseReadCard);
            this.lblAlarm.ForeColor = Color.Blue;
            this.lblAlarm.Refresh();
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
        }

        /// <summary>
        /// 读到卡片处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (!this.chkWriteCard.Checked) return;
                if (e[GlobalVariables.ParkingSection] == null)
                {
                    this.lblAlarm.Text = string.Format(Resources.Resource1.FrmCardBulkChange_NoReadData);
                    this.lblAlarm.ForeColor = Color.Blue;
                    this.lblAlarm.Refresh();
                    return;
                }

                //判断卡片是否已修改
                if (_changedCards.Any(item => item.CardID == e.CardID))
                {
                    this.lblAlarm.Text = string.Format(Resources.Resource1.FrmCardBulkChange_HadWrite, e.CardID);
                    this.lblAlarm.ForeColor = Color.Red;
                    this.lblAlarm.Refresh();
                    return;
                }

                //判断卡片是否需要修改的卡片
                CardInfo card = BulkChangeCards.Find(item => item.CardID == e.CardID);
                if (card == null)
                {
                    this.lblAlarm.Text = string.Format(Resources.Resource1.FrmCardBulkChange_UnneedWrite, e.CardID);
                    this.lblAlarm.ForeColor = Color.Red;
                    this.lblAlarm.Refresh();
                    return;
                }
                //从卡片数据中获取卡片信息
                CardDateResolver.Instance.SetCardInfoFromData(card, e[GlobalVariables.ParkingSection], true);
                //修改卡片
                SetCardFromInput(card);

                this.lblAlarm.Text = string.Format(Resources.Resource1.FrmCardBulkChange_WritingCard, e.CardID);
                this.lblAlarm.ForeColor = Color.Blue;
                this.lblAlarm.Refresh();

                //信息重新写入卡片
                if (CardOperationManager.Instance.WriteCardLoop(card) == CardOperationResultCode.Success)
                {
                    _changedCards.Add(card);
                    progressBar1.Value++;
                    this.lblStatus.Text = string.Format("{0} / {1}", progressBar1.Value, progressBar1.Maximum);
                    this.lblStatus.Refresh();

                    this.lblAlarm.Text = string.Format(Resources.Resource1.FrmCardBulkChange_WriteSuccess, e.CardID);
                    this.lblAlarm.ForeColor = Color.Blue;
                    this.lblAlarm.Refresh();
                }
                else
                {
                    this.lblAlarm.Text = string.Format(Resources.Resource1.FrmCardBulkChange_WriteFail, e.CardID);
                    this.lblAlarm.ForeColor = Color.Red;
                    this.lblAlarm.Refresh();
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

        #region 事件处理程序
        private void FrmCardBulkChange_Load(object sender, EventArgs e)
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.comAccessLevel.Init();
            if (this.chkWriteCard.Checked)
            {
                this.btnOk.Text = Resources.Resource1.FrmCardBulkChange_ReadCard + "(&R)";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (BulkChangeCards != null)
            {
                if (btnOk.Text == Resources.Resource1.FrmCardBulkChange_ReadCard + "(&R)")
                {
                    btnOk.Text = Resources.Resource1.FrmCardBulkChange_OK + "(&O)";
                    this.chkWriteCard.Enabled = false;
                    ReadCard();
                    return;
                }
                else if (this.chkWriteCard.Checked && progressBar1.Value < progressBar1.Maximum)
                {
                    if (MessageBox.Show(Resources.Resource1.FrmCardBulkChange_SaveQuery, Resources.Resource1.Form_Query, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.No)
                    {
                        return;
                    }
                }

                List<CardInfo> cards = this.chkWriteCard.Checked ? _changedCards.ToList() : BulkChangeCards;

                progressBar1.Maximum = cards.Count;
                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
                foreach (CardInfo card in cards)
                {
                    SetCardFromInput(card);

                    CommandResult ret = null;
                    if (this.chkWriteCard.Checked)
                    {
                        //写卡模式时，以卡片信息为准
                        ret = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).UpdateCardAll(card);
                    }
                    else
                    {
                        ret = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).UpdateCard(card);
                    }
                    progressBar1.Value++;
                    this.lblStatus.Text = string.Format("{0} / {1}", progressBar1.Value, progressBar1.Maximum);
                    this.lblStatus.Refresh();
                    if (ret.Result == ResultCode.Successful)
                    {
                        _changedCards.Remove(card);
                        if (this.ItemUpdated != null) this.ItemUpdated(this, new ItemUpdatedEventArgs(card));
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmCardBulkChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.chkWriteCard.Checked && _changedCards.Count > 0)
            {
                if (MessageBox.Show(Resources.Resource1.FrmCardBulkChange_CloseQuery, Resources.Resource1.Form_Query, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (this.chkWriteCard.Checked)
            {
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            }
        }
        #endregion

        private void chkWriteCard_CheckedChanged(object sender, EventArgs e)
        {
            btnOk.Text = chkWriteCard.Checked ? Resources.Resource1.FrmCardBulkChange_ReadCard + "(&R)" : Resources.Resource1.FrmCardBulkChange_OK + "(&O)";
        }

    }
}
