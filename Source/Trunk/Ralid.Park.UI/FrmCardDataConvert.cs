using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardDataConvert : Form
    {
        public FrmCardDataConvert()
        {
            InitializeComponent();
        }

        #region 私有变量
        private List<CardInfo> _Cards = null;
        private bool _StartConvert = false;
        #endregion

        #region 公共事件
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        #endregion

        #region 私有方法
        /// <summary>
        /// 转换到卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool ConvertToCard(CardInfo card)
        {
            bool success = false;
            if (card != null)
            {
                CardOperationResultCode result = CardOperationManager.Instance.WriteCardLoop(card, true);
                success = result == CardOperationResultCode.Success;
            }
            return success;
 
        }

        /// <summary>
        /// 装换到数据库
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool ConvertToDatabase(CardInfo card)
        {
            bool success = false;
            if (card != null)
            {
                success = new CardBll(AppSettings.CurrentSetting.ParkConnect).UpdateCardAll(card).Result == ResultCode.Successful;
            }
            return success; 
        }

        private void ReadCardHandler(object sender, CardReadEventArgs args)
        {
            Action action = delegate()
            {

                CardInfo readcard = CardDateResolver.Instance.GetCardInfoFromData(args.CardID, args[GlobalVariables.ParkingSection]);

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    bool success = false;
                    CardInfo orginal = row.Tag as CardInfo;
                    CardInfo card = orginal.Clone() ;
                    if (card == null) continue;
                    if (this.chkHandleMode.Checked) card.OnlineHandleWhenOfflineMode = this.rdbOnlineHandle.Checked;

                    if (card.CardID == args.CardID)
                    {
                        if (rdToCard.Checked)
                        {
                            if (ConvertToCard(card))
                            {
                                if (this.chkHandleMode.Checked && orginal.OnlineHandleWhenOfflineMode != card.OnlineHandleWhenOfflineMode)
                                {
                                    success = ConvertToDatabase(card);
                                }
                                else
                                {
                                    success = true;
                                }
                            }
                        }
                        else if (rdToDatabase.Checked)
                        {
                            if (CardDateResolver.Instance.CopyCardDataToCard(card, readcard) && ConvertToDatabase(card))
                            {
                                if (this.chkHandleMode.Checked && orginal.OnlineHandleWhenOfflineMode != card.OnlineHandleWhenOfflineMode)
                                {
                                    success = ConvertToCard(card);
                                }
                                else
                                {
                                    success = true;
                                }
                            }
                        }
                    }

                    if (success)
                    {
                        row.Tag = card;
                        this.dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        row.DefaultCellStyle.ForeColor = Color.Green;
                        SelectSingleRow(row);
                        if (this.ItemUpdated != null) this.ItemUpdated(this, new ItemUpdatedEventArgs(card));
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

        private void SelectSingleRow(DataGridViewRow row)
        {
            foreach (DataGridViewRow r in this.dataGridView1.Rows)
            {
                if (r == row)
                {
                    r.Selected = true;
                }
                else
                {
                    r.Selected = false;
                }
            }
        }
        #endregion


        #region 窗体事件
        private void FrmCardDataConvert_Load(object sender, EventArgs e)
        {
            this.comCardType.Init();
            this.btnConvert.Enabled = AppSettings.CurrentSetting.EnableWriteCard
                && OperatorInfo.CurrentOperator.Role.Permit(Permission.CardDataConvert);
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            int count = 0;
            CardSearchCondition con = new CardSearchCondition();
            con.CardType = comCardType.SelectedCardType;
            con.OwnerName = txtOwnerName.Text;
            con.CarPlate = txtCarPlate.Text;
            con.CardCertificate = txtCardCertificate.Text;
            _Cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(con).QueryObjects;
            _Cards = _Cards.FindAll(item => item.CardType != CardType.OperatorCard);
            foreach (CardInfo card in _Cards)
            {
                int row = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[row].Tag = card;
                this.dataGridView1.Rows[row].Cells["colCardID"].Value = card.CardID;
                this.dataGridView1.Rows[row].Cells["colOwnerName"].Value = card.OwnerName;
                this.dataGridView1.Rows[row].Cells["colCardType"].Value = card.CardType.Name;
                this.dataGridView1.Rows[row].Cells["colCertificate"].Value = card.CardCertificate;
                this.dataGridView1.Rows[row].Cells["colCarPlate"].Value = card.CarPlate;
                this.dataGridView1.Rows[row].Cells["colParkingStatus"].Value = ParkingStatusDescription.GetDescription(card.ParkingStatus);
                count++;
            }
            this.lblCount.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, count);
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (_StartConvert)
            {
                this.btnConvert.Text = Resources.Resource1.FrmCardDataConvert_StartConvert;
                _StartConvert = false;
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(ReadCardHandler);
            }
            else
            {
                this.btnConvert.Text = Resources.Resource1.FrmCardDataConvert_StopConvert;
                _StartConvert = true;
                CardReaderManager.GetInstance(WegenType.Wengen34).PushCardReadRequest(ReadCardHandler);
            }
        }
        private void FrmCardDataConvert_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(ReadCardHandler);
        }
        private void chkHandleMode_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlHandleMode.Enabled = this.chkHandleMode.Checked;
        }
        #endregion




    }
}
