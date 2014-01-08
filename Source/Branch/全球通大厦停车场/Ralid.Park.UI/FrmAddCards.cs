using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.ParkAdapter;

namespace Ralid.Park.UI
{
    public partial class FrmAddCards : Form
    {
        public FrmAddCards()
        {
            InitializeComponent();
        }

        public event EventHandler<ItemAddedEventArgs> CardAdded;

        #region 私有变量和方法
        private const int  step_welcome = 0; //表示欢迎步骤
        private const int step_input = 1;//表示输入基本信息步骤
        private const int step_readCard = 2;//表示读卡步骤
        private const int step_excute = 3; //表示执行步骤
        private const int step_recycle=4; //表示继续加卡步骤

        private int _curStep =0; //表示向导执行到第几步

        private void ShowNextStep(int step)
        {
            if (step <= step_excute && step >= step_welcome)
            {
                Rectangle rec = new Rectangle(tabControl1.TabPages[step].Left, tabControl1.TabPages[step].Top,
                    tabControl1.TabPages[step].Width, tabControl1.TabPages[step].Height);
                this.tabControl1.Region = new Region(rec);
                this.tabControl1.SelectedIndex = step;
                this.btnPrevious.Enabled = (step != step_welcome);
                this.btnNext.Text = Resources.Resource1.FrmAddCards_Next;
            }
            else if (step == step_recycle)
            {
                this.btnNext.Text = Resources.Resource1.FrmAddCards_Continue;
                this.btnPrevious.Enabled = false;
            }
        }

        private void InitCardGrid()
        {
            this.cardView.Init();
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtPrefix.Text) && !chkAutoIncrement.Checked && string.IsNullOrEmpty(txtSuffix.Text))
            {
                MessageBox.Show(Resources.Resource1.FrmAddCards_InvalidOwnerName);
                return false;
            }
            if (comCardType.SelectedCardType == null)
            {
                MessageBox.Show(Resources.Resource1.FrmAddCards_InvalidCardType);
                return false;
            }
            return true;
        }

        private void ReadCardSuccess_Handler(string cardID)
        {
            CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            if (bll.GetCardByID(cardID).Result == ResultCode.Successful)
            {
                lblAlarm.Text = string.Format(Resources.Resource1.FrmAddCards_CardExists, cardID);
                lblAlarm.ForeColor = Color.Red;
                return; //如果卡片已经存在什么也不做
            }
            if (cardView.IndexOfCard(cardID) == -1)
            {
                CardInfo card = new CardInfo();
                card.CardID = cardID;
                card.CardType = this.comCardType.SelectedCardType;
                card.AccessID = (byte)this.comAccessLevel.AccesslevelID;
                card.Balance = this.txtBalance.DecimalValue;
                card.ValidDate = this.dtValidDate.Value;
                card.CarType = this.comChargeType.SelectedCarType;
                card.Status = CardStatus.Enabled;
                card.CanRepeatIn = this.chkRepeatIn.Checked;
                card.CanRepeatOut = this.chkRepeatOut.Checked;
                card.HolidayEnabled = this.chkHoliday.Checked;
                card.WithCount = this.chkWithCount.Checked;
                card.CanEnterWhenFull = this.chkCanEnterWhenFull.Checked;
                card.EnableWhenExpired = this.chkEnableWhenExpired.Checked;
                card.OnlineHandleWhenOfflineMode = this.chkOnlineHandleWhenOfflineMode.Checked;
                card.OwnerName = string.Format("{0}{1}{2}", this.txtPrefix.Text.Trim(),
                                                        chkAutoIncrement.Checked ? (txtAutoIncrement.IntergerValue + cardView.Rows.Count).ToString() : string.Empty,
                                                        txtSuffix.Text.Trim());

                //如果是写卡模式，这里需将信息写入卡片成功后才能继续
                if (this.chkWriteCard.Checked)
                {
                    lblAlarm.Text = string.Format(Resources.Resource1.FrmAddCards_WritingCard, cardID);
                    lblAlarm.ForeColor = Color.Blue;
                    lblAlarm.Refresh();

                    //密钥不正确的，需要初始化密钥
                    if (CardOperationManager.Instance.WriteCardLoop(card,true) != CardOperationResultCode.Success)
                    {
                        lblAlarm.Text = string.Format(Resources.Resource1.FrmAddCards_WriteCardFail, cardID);
                        lblAlarm.ForeColor = Color.Red;
                        return;//如果卡片信息写入失败什么也不做
                    }
                }
                cardView.AddCardInfo(card);
                cardView.SelectedCard(card.CardID);
                lblAlarm.Text = string.Format(Resources.Resource1.FrmAddCards_CardCount, cardView.Rows.Count);
                lblAlarm.ForeColor = Color.Blue;
            }
            else
            {
                cardView.SelectedCard(cardID);
            }
        }
        
       #endregion

        private void FrmAddCards_Load(object sender, EventArgs e)
        {
            this.chkWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时默认选中
            this.chkWriteCard.Visible = AppSettings.CurrentSetting.EnableWriteCard;//写卡模式时显示

            this.comCardType.Init();
            this.comChargeType.Init();
            this.comAccessLevel.Init();
            this.comCardType.SelectedCardType = CardType.TempCard;
            this.comChargeType.SelectedCarType = CarTypeSetting.DefaultCarType;
            this.comAccessLevel.AccesslevelID = 0;
            this._curStep = step_welcome;
            InitCardGrid();
            ShowNextStep(_curStep);
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                if (_curStep == step_readCard)
                {
                    ReadCardSuccess_Handler(e.CardID);
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (_curStep)
            {
                case step_welcome:
                    _curStep++;
                    break;
                case step_input:
                    if (CheckInput())
                    {
                        _curStep++;
                    }
                    break;
                case step_readCard:
                    _curStep++;
                    break;
                case step_excute:
                    List<CardInfo> cards = cardView.CardSource;
                    if (cards != null && cards.Count > 0)
                    {
                        CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                        this.progressBar1.Maximum = cards.Count;
                        this.progressBar1.Value = 0;
                        //bool download = true;
                        foreach (CardInfo card in cards)
                        {
                            CommandResult ret = null;
                            if (card.Status == CardStatus.Enabled)
                            {
                                ret = bll.CardRelease(card, 0, PaymentMode.Cash, string.Empty, !this.chkWriteCard.Checked);
                            }
                            else
                            {
                                ret = bll.AddCard(card);
                            }
                            if (ret.Result == ResultCode.Successful)
                            {
                                this.CardAdded(this, new ItemAddedEventArgs(card));
                            }
                            progressBar1.Value++;
                            lblStatus.Text = string.Format(Resources.Resource1.FrmAddCards_Processing,
                                progressBar1.Value, progressBar1.Maximum);
                            lblStatus.Refresh();
                        }
                    }
                    lblAlarm.Text = string.Format(Resources.Resource1.FrmAddCards_CardCount, 0);
                    _curStep++;
                    break;
                case step_recycle:
                    _curStep = step_welcome;
                    InitCardGrid();
                    this.progressBar1.Value = 0;
                    break;
            }
            ShowNextStep(_curStep);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_curStep > step_welcome)
            {
                _curStep--;
                ShowNextStep(_curStep);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddCards_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }

    }
}
