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
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.ParkAdapter;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI
{
    public partial class FrmCards : Form
    {
        #region 静态变量和方法
        private static FrmCards _Instance;

        public static FrmCards GetInstances()
        {
            if (_Instance == null) _Instance = new FrmCards();
            return _Instance;
        }
        #endregion

        #region 构造函数
        public FrmCards()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
        private List<CardInfo> _cards;
        /// <summary>
        /// 卡片查询条件
        /// </summary>
        private CardSearchCondition _search;
        #endregion

        #region 私有方法
        private CardInfo GetSelectedItem()
        {
            List<CardInfo> items = this.cardView.GetSelectedCards();
            if (items.Count > 0)
            {
                return items[0];
            }
            else
            {
                return null;
            }
        }

        private void ShowCardContextMenu(CardInfo info)
        {
            foreach (ToolStripItem item in CardOperatorMenuStrip.Items)
            {
                item.Enabled = false;
            }
            mnu_CardCharge.Enabled = info.Chargable;
            mnu_CardDefer.Enabled = info.Deferable;
            mnu_CardRestore.Enabled = (info.Status == CardStatus.Loss);
            mnu_CardEnable.Enabled = (info.Status == CardStatus.Disabled);
            mnu_CardRelease.Enabled = (info.ReleasAble);
            mnu_CardLost.Enabled = (info.Status == CardStatus.Enabled);
            mnu_CardDisable.Enabled = (info.Status == CardStatus.Enabled);
            mnu_CardRecycle.Enabled = info.Recycleable;
            mnu_CardDownload.Enabled = true;
            mnu_CardClear.Enabled = true;
            mnu_Delete.Enabled = true;
            mnu_Property.Enabled = true;
        }

        private void ItemAdded_Handler(object sender, ItemAddedEventArgs e)
        {
            CardInfo info = (CardInfo)e.AddedItem;
            _cards.Add(info);
            cardView.AddCardInfo(info);
            FreshStatusBar();

            if (DataBaseConnectionsManager.Current.StandbyConnected)
            {
                CardBll standbybll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                standbybll.UpdateOrInsert(info);
            }

            //foreach (ParkingAdapter pad in ParkingAdapterManager.Instance.ParkAdapters)
            //{
            //    pad.SaveCard(info, ActionType.Add);
            //}
        }

        private void ItemUpdated_Handler(object sender, ItemUpdatedEventArgs e)
        {
            CardInfo info = (CardInfo)e.UpdatedItem;
            cardView.UpdateCardInfo(info);
            if (DataBaseConnectionsManager.Current.StandbyConnected)
            {
                CardBll standbybll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                standbybll.UpdateOrInsert(info);
            }
            //foreach (ParkingAdapter pad in ParkingAdapterManager.Instance.ParkAdapters)
            //{
            //    pad.SaveCard(info, ActionType.Upate);
            //}
        }

        //删除卡片
        private bool DeletingCard(CardInfo info)
        {
            try
            {
                CommandResult ret = bll.DeleteCard(info);
                if (ret.Result == ResultCode.Successful)
                {
                    if (DataBaseConnectionsManager.Current.StandbyConnected)
                    {
                        CardBll standbybll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                        standbybll.DeleteCard(info);
                    }
                    //foreach (ParkingAdapter pad in ParkingAdapterManager.Instance.ParkAdapters)
                    //{
                    //    pad.DeleteCard(info);
                    //}
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        //强制删除卡片
        private bool DeletingCardAtAll(CardInfo info)
        {
            try
            {
                CommandResult ret = bll.DeleteCardAtAll(info);
                if (ret.Result == ResultCode.Successful)
                {
                    if (DataBaseConnectionsManager.Current.StandbyConnected)
                    {
                        CardBll standbybll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                        standbybll.DeleteCardAtAll(info);
                    }
                    //foreach (ParkingAdapter pad in ParkingAdapterManager.Instance.ParkAdapters)
                    //{
                    //    pad.DeleteCard(info);
                    //}
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void UpdatingCard(CardInfo info)
        {
            if (info != null)
            {
                FrmDetailBase detailForm;
                if (info.CardType.IsManagedCard)
                {
                    detailForm = new FrmManageCardDetail();
                }
                else
                {
                    detailForm = new FrmCardDetail();
                }
                detailForm.IsAdding = false;
                detailForm.UpdatingItem = info;
                detailForm.ItemUpdated += ItemUpdated_Handler;
                detailForm.ShowDialog();
            }
        }

        private void FreshStatusBar()
        {
            this.lblCount.Text = string.Format(Resource1.FrmMasterBase_StatusBar, cardView.Rows.Count);
        }

        private void ShowOperatorRights(OperatorInfo op)
        {
            RoleInfo role = op.Role;
            this.btn_CardRelease.Enabled = role.Permit(Permission.ReleaseCard);
            this.btn_CardCharge.Enabled = role.Permit(Permission.ChargeCard);
            this.btn_CardDefer.Enabled = role.Permit(Permission.DeferCard);
            this.btn_CardRecycle.Enabled = role.Permit(Permission.RecycleCard);
            this.btn_AddCards.Enabled = role.Permit(Permission.AddCards);
            this.btn_DownloadCards.Enabled = role.Permit(Permission.DownloadAllCards);
            this.btnExportCards.Enabled = role.Permit(Permission.ExportCards);
            this.btn_BulkChange.Enabled = role.Permit(Permission.CardBulkChange);
            this.btn_CardIDConvert.Enabled = role.Permit(Permission.CardIDConvert);
            this.btn_AddManageCard.Enabled = role.Permit(Permission.AddManageCard);
            this.btn_ChangeCardKey.Enabled = role.Permit(Permission.ChangeCardKey);
            this.btn_ChangeCardKey.Visible = AppSettings.CurrentSetting.EnableWriteCard && GlobalVariables.UseMifareIC;
            this.btn_CardDataConvert.Enabled = role.Permit(Permission.CardDataConvert);
            this.btn_CardDataConvert.Visible = AppSettings.CurrentSetting.EnableWriteCard;

            this.mnu_CardRelease.Enabled = role.Permit(Permission.ReleaseCard) && mnu_CardRelease.Enabled;
            this.mnu_CardCharge.Enabled = role.Permit(Permission.ChargeCard) && mnu_CardCharge.Enabled;
            this.mnu_CardDefer.Enabled = role.Permit(Permission.DeferCard) && mnu_CardDefer.Enabled;
            this.mnu_CardLost.Enabled = role.Permit(Permission.LostCard) && mnu_CardLost.Enabled;
            this.mnu_CardRestore.Enabled = role.Permit(Permission.RestoreCard) && mnu_CardRestore.Enabled;
            this.mnu_CardDisable.Enabled = role.Permit(Permission.DisableCard) && mnu_CardDisable.Enabled;
            this.mnu_CardEnable.Enabled = role.Permit(Permission.EnableCard) && mnu_CardEnable.Enabled;
            this.mnu_CardRecycle.Enabled = role.Permit(Permission.RecycleCard) && mnu_CardRecycle.Enabled;
            this.mnu_CardDownload.Enabled = role.Permit(Permission.DownloadAllCards) && mnu_CardDownload.Enabled;
            this.mnu_CardClear.Enabled = role.Permit(Permission.DownloadAllCards) && mnu_CardClear.Enabled;
            this.mnu_Delete.Enabled = role.Permit(Permission.EditCard);
            this.ToolStripMenuItem.Enabled = role.Permit(Permission.DeleteAtAll);
            this.mnu_SyncCardToStandby.Enabled = role.Permit(Permission.SyncDataToStandby)
                && DataBaseConnectionsManager.Current.MasterConnected
                && DataBaseConnectionsManager.Current.StandbyConnected;
        }
        #endregion

        #region 右键菜单和工具栏事件处理
        private void mnu_CardLostRestore_Click(object sender, EventArgs e)
        {
            CardInfo card = GetSelectedItem();
            if (card != null)
            {
                FrmCardLostRestore frm = new FrmCardLostRestore();
                frm.ItemUpdated += ItemUpdated_Handler;
                frm.LossRestoreCard = card;
                frm.ShowDialog();
            }
        }

        private void mnu_CardDisableEnable_Click(object sender, EventArgs e)
        {
            CardInfo card = GetSelectedItem();
            if (card != null)
            {
                FrmCardDisableEnable frm = new FrmCardDisableEnable();
                frm.DisableEnableCard = card;
                frm.ItemUpdated += ItemUpdated_Handler;
                frm.ShowDialog();
            }
        }

        private void mnu_CardRelease_Click(object sender, EventArgs e)
        {
            FrmCardRelease frm = new FrmCardRelease();
            if (object.ReferenceEquals(sender, this.mnu_CardRelease))
            {
                frm.ReleasingCard = GetSelectedItem();
            }
            frm.ItemUpdated += ItemUpdated_Handler;
            frm.ItemAdded += ItemAdded_Handler;
            frm.ShowDialog();
        }

        private void mnu_CardCharge_Click(object sender, EventArgs e)
        {
            CardInfo card = GetSelectedItem();
            if (card != null)
            {
                if (card.Chargable)
                {
                    FrmCardCharge frm = new FrmCardCharge();
                    frm.ChargingCard = card;
                    frm.ItemUpdated += ItemUpdated_Handler;
                    frm.ShowDialog();
                }
                else
                {
                    string msg = string.Format(Resource1.FrmCards_CannotCharge, card.CardID);
                    MessageBox.Show(msg, Resource1.Form_Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void mnu_CardDefer_Click(object sender, EventArgs e)
        {
            CardInfo card = GetSelectedItem();
            if (card != null)
            {
                if (card.Deferable)
                {
                    FrmCardDefer frm = new FrmCardDefer();
                    frm.DeferingCard = GetSelectedItem();
                    frm.ItemUpdated += ItemUpdated_Handler;
                    frm.ShowDialog();
                }
                else
                {
                    string msg = string.Format(Resource1.UcCard_CannotDefer, card.CardID);
                    MessageBox.Show(msg, Resource1.Form_Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void mnu_CardRecycle_Click(object sender, EventArgs e)
        {
            CardInfo card = GetSelectedItem();
            if (card != null)
            {
                if (card.Recycleable)
                {
                    FrmCardRecycle frm = new FrmCardRecycle();
                    frm.RecycleCard = GetSelectedItem();
                    frm.ItemUpdated += ItemUpdated_Handler;
                    frm.ShowDialog();
                }
                else
                {
                    string msg = string.Format(Resource1.UcCard_CannotRecycle, card.CardID);
                    MessageBox.Show(msg, Resource1.Form_Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void mnu_SelAll_Click(object sender, EventArgs e)
        {
            this.cardView.SelectAll();
        }

        private void mnu_CardDownload_Click(object sender, EventArgs e)
        {
            List<CardInfo> downloadItems = this.cardView.GetSelectedCards();
            if (downloadItems.Count > 0)
            {
                FrmDownLoadAllCards frm = new FrmDownLoadAllCards();
                frm.Cards = downloadItems;
                frm.ShowDialog();
            }
        }

        private void mnu_CardClear_Click(object sender, EventArgs e)
        {

            List<CardInfo> downloadItems = this.cardView.GetSelectedCards();
            if (downloadItems.Count > 0)
            {
                FrmDownLoadAllCards frm = new FrmDownLoadAllCards();
                frm.Cards = downloadItems;
                frm.DownLoadCard = false;
                frm.ShowDialog();
            }
        }


        private void mnu_SyncCardToStandby_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resource1.FrmSyncDataToStandby_Cover, Resource1.Form_Alert, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2)
                == DialogResult.No) return;

            this.Cursor=Cursors.WaitCursor;
            List<CardInfo> downloadItems = this.cardView.GetSelectedCards();
            if (downloadItems.Count > 0)
            {
                int count = 0;
                CardBll standbybll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                foreach (CardInfo item in downloadItems)
                {
                    if (standbybll.UpdateOrInsert(item).Result == ResultCode.Successful) count++;
                }
                MessageBox.Show(string.Format(Resource1.FrmCards_SyncCardToStandby, downloadItems.Count.ToString(), count.ToString()));
            }
            this.Cursor=Cursors.Arrow;
        }

        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            List<CardInfo> deletingItems = this.cardView.GetSelectedCards();
            if (deletingItems.Count > 0)
            {
                DialogResult result = MessageBox.Show(Resource1.UcCard_DeleteCardQuery, Resource1.Form_Query, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    foreach (CardInfo card in deletingItems)
                    {
                        if (DeletingCard(card))
                        {
                            cardView.DeleteCardInfo(card);
                            _cards.Remove(card);
                        }
                    }
                    FreshStatusBar();
                }
            }
        }

        //强制删除（忽略卡片在场、费用等问题）
        private void 强制删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CardInfo> deletingItems = this.cardView.GetSelectedCards();
            if (deletingItems.Count > 0)
            {
                //提示，本次操作将会忽略卡片状态和卡片费用问题，确定要强制删除吗？
                DialogResult result = MessageBox.Show(Resource1.UcCard_DeleteCardAtAll, Resource1.Form_Query, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    foreach (CardInfo card in deletingItems)
                    {
                        if (DeletingCardAtAll(card))
                        {
                            cardView.DeleteCardInfo(card);
                            _cards.Remove(card);
                        }
                    }
                    FreshStatusBar();
                }
            }
        }

        private void mnu_Property_Click(object sender, EventArgs e)
        {
            CardInfo pre = GetSelectedItem();
            if (pre != null)
            {
                UpdatingCard(pre);
            }
        }

        private void btn_DownLoadAllCards_Click(object sender, EventArgs e)
        {
            FrmDownLoadAllCards frm = new FrmDownLoadAllCards();
            frm.ShowDialog();
        }

        private void btn_AddCards_Click(object sender, EventArgs e)
        {
            FrmAddCards frm = new FrmAddCards();
            frm.CardAdded += ItemAdded_Handler;
            frm.ShowDialog();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            _search = new CardSearchCondition();
            this.ucPaging1.Init(ucPaging1.DefaultPageSizeIndex);

            //_cards = bll.GetAllCards().QueryObjects;
            //cardView.CardSource = _cards;
            //FreshStatusBar();
        }

        private void btnExportCards_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = Resource1.Form_ExcelFilter;
                saveFileDialog1.FileName = Resource1.FrmCards_Card + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog1.FileName;
                    this.cardView.SaveToFile(path);
                }
            }
            catch
            {
                MessageBox.Show(Resource1.Form_SaveExcelFail);
            }
        }

        private void btn_BulkChange_Click(object sender, EventArgs e)
        {
            List<CardInfo> cards = this.cardView.GetSelectedCards();
            if (cards != null && cards.Count > 0)
            {
                FrmCardBulkChange frm = new FrmCardBulkChange();
                frm.BulkChangeCards = cards;
                frm.ItemUpdated += ItemUpdated_Handler;
                frm.ShowDialog();
            }
        }
        #endregion

        #region 事件处理
        private void FrmCards_Load(object sender, EventArgs e)
        {
            //this.btn_DownloadCards.Visible = AppSettings.CurrentSetting.EnableWriteCard;
            //this.mnu_CardDownload.Visible = AppSettings.CurrentSetting.EnableWriteCard;
            //this.mnu_CardClear.Visible = AppSettings.CurrentSetting.EnableWriteCard;

            this.mnu_SyncCardToStandby.Visible = !string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect);
            this.comCardType.ShowManageCardType = true;

            this.comCardStatus.Init();
            this.comListType.Init();
            this.comCardType.Init();
            this.comChargeType.Init();
            this.cardView.Init();
            this.accessComboBox1.Init();
            //_cards = bll.GetAllCards().QueryObjects;
            _search = new CardSearchCondition();
            _search.PageSize = 100;
            _search.PageIndex = 1;
            ucPaging1.GetPageData -= new UCPaging.HandleEventGetPageData(ucPaging1_GetPageData);
            ucPaging1.GetPageData += new UCPaging.HandleEventGetPageData(ucPaging1_GetPageData);
            this.ucPaging1.Init(3);
        }

        void ucPaging1_GetPageData(int index, int pagesize)
        {
            this.Cursor = Cursors.WaitCursor;
            if (_search == null)
            {
                _search = new CardSearchCondition();
            }
            _search.PageSize = pagesize;
            _search.PageIndex = index;
            _cards = bll.GetCards(_search).QueryObjects;

            ucPaging1.TotalCount = _search.TotalCountEx;
            ucPaging1.TotalPages = (int)Math.Ceiling(_search.TotalCountEx * 1.0M / ucPaging1.PageSize );
            
            cardView.CardSource = _cards;

            FreshStatusBar();

            this.Cursor = Cursors.Default;
            cardView.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CardSearchCondition search = new CardSearchCondition();

            //List<CardInfo> selections = _cards;
            string cardID = txtCardID.Text.Trim();
            if (!string.IsNullOrEmpty(cardID))
            {
                search.CardID = cardID;
                //selections = selections.Where(card => card.CardID.Contains(cardID)).ToList();
            }
            string name = txtOwnerName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                search.OwnerName = name;
                //selections = selections.Where(card => card.OwnerName == name).ToList();
            }
            string department = txtDepartment.Text.Trim();
            if (!string.IsNullOrEmpty(department))
            {
                search.Department = department;
                //selections = selections.Where(card => card.Department == department).ToList();
            }
            if (comListType.SelectedIndex > 0)
            {
                search.ListType = comListType.ListType;
                //selections = selections.Where(card => card.ListType == comListType.ListType).ToList();
            }
            if (comCardStatus.SelectedIndex > 0)
            {
                search.Status = comCardStatus.CardStatus;
                //selections = selections.Where(card => card.Status == comCardStatus.CardStatus).ToList();
            }
            if (comCardType.SelectedIndex > 0)
            {
                search.CardType = comCardType.SelectedCardType;
                //selections = selections.Where(card => card.CardType == comCardType.SelectedCardType).ToList();
            }
            if (comChargeType.SelectedIndex > 0)
            {
                search.CarType = comChargeType.SelectedCarType;
                //selections = selections.Where(card => card.CarType == comChargeType.SelectedCarType).ToList();
            }
            if (cmbCarStatus.SelectedIndex == 1)
            {
                search.IsIn = true;
                //selections = selections.Where(card => card.IsInPark == true).ToList();
            }
            else if (cmbCarStatus.SelectedIndex == 2)
            {
                search.IsIn = false;
                //selections = selections.Where(card => card.IsInPark == false).ToList();
            }
            if (!string.IsNullOrEmpty(txtCarPlate.Text.Trim()))
            {
                search.CarPlate = txtCarPlate.Text.Trim();
                //selections = selections.Where(card => !string.IsNullOrEmpty(card.CarPlate) && card.CarPlate.Contains(txtCarPlate.Text.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(txtCardCertificate.Text.Trim()))
            {
                search.CardCertificate = txtCardCertificate.Text.Trim();
                //selections = selections.Where(card => !string.IsNullOrEmpty(card.CardCertificate) && card.CardCertificate.Contains(txtCardCertificate.Text.Trim())).ToList();
            }
            if (accessComboBox1.AccesslevelID > 0)
            {
                search.AccessID = accessComboBox1.AccesslevelID;
                //byte accessID = accessComboBox1.AccesslevelID;
                //selections = selections.Where(card => card.AccessID == accessID).ToList();
            }
            //this.cardView.CardSource = selections;
            //FreshStatusBar();

            _search = search;

            this.ucPaging1.Init(ucPaging1.DefaultPageSizeIndex);

        }

        private void GridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                if (!cardView.Rows[e.RowIndex].Selected)
                {
                    cardView.SelectNone();
                    cardView.Rows[e.RowIndex].Selected = true;
                }
                CardInfo info = cardView.GetCardInfoAt(e.RowIndex);
                if (info != null)
                {
                    ShowCardContextMenu(info);
                    ShowOperatorRights(OperatorInfo.CurrentOperator);
                }
            }
        }

        private void GridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CardInfo info = cardView.GetCardInfoAt(e.RowIndex);
            if (info != null)
            {
                UpdatingCard(info);
            }
        }

        private void btnClosePanel_Click(object sender, EventArgs e)
        {
            this.panelLeft.Visible = false;
            this.splitter1.Visible = false;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            this.panelLeft.Visible = true;
            this.splitter1.Visible = true;
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                this.txtCardID.TextChanged -= txtCardID_TextChanged;

                string cardID = e.CardID.Trim(' ', '\n');
                if (!string.IsNullOrEmpty(cardID))
                {
                    cardView.SelectedCard(cardID);
                }
                this.txtCardID.Text = cardID;

                this.txtCardID.TextChanged -= txtCardID_TextChanged;
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
            string cardID = txtCardID.Text.Trim(' ', '\n');
            if (!string.IsNullOrEmpty(cardID))
            {
                cardView.SelectedCard(cardID);
            }
        }

        private void FrmCards_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);

            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved -= new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }

        private void FrmCards_Activated(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            ShowOperatorRights(OperatorInfo.CurrentOperator);
            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved += new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }

        private void ZSTReader_MessageRecieved(object sender, ZSTReaderEventArgs e)
        {
            if (e.ReaderIP == AppSettings.CurrentSetting.ZSTReaderIP && e.MessageType == "1")
            {
                CardReadEventArgs args = new CardReadEventArgs() { CardID = e.CardID };
                CardReadHandler(sender, args);
            }
        }

        private void FrmCards_Deactivate(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                frm.ZSTReader.MessageRecieved -= new EventHandler<ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            }
        }
        #endregion

        private void btn_CardIDConvert_Click(object sender, EventArgs e)
        {
            FrmCardIDConvert frm = new FrmCardIDConvert();
            frm.ShowDialog();
            btn_Refresh_Click(this.btn_Refresh, EventArgs.Empty); //刷新一次卡片
        }

        private void btn_AddManageCard_Click(object sender, EventArgs e)
        {
            FrmDetailBase detailForm = new FrmManageCardDetail();
            detailForm.IsAdding = true;
            detailForm.ItemAdded += ItemAdded_Handler;
            detailForm.ShowDialog();
        }

        private void btn_ChangeCardKey_Click(object sender, EventArgs e)
        {
            FrmCardChangeKey frm = new FrmCardChangeKey();
            frm.ShowDialog();
        }

        private void btn_CardDataConvert_Click(object sender, EventArgs e)
        {
            FrmCardDataConvert frm = new FrmCardDataConvert();
            frm.ItemUpdated += ItemUpdated_Handler;
            frm.ShowDialog();
        }

      

    }
}
