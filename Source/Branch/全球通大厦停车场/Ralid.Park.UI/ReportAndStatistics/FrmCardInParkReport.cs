using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardInParkReport : FrmReportBase
    {
        public FrmCardInParkReport()
        {
            InitializeComponent();
            this.ItemSearching += new EventHandler(FrmCardInParkReport_ItemSearching);
        }

        private List<CardPaymentInfo> paymentRecords;
        private int _EventIndex;

        private void AddCardToGrid(CardInfo card)
        {
            int row = this.GridView.Rows.Add();
            this.GridView.Rows[row].Tag = card;
            this.GridView.Rows[row].Cells["colCardID"].Value = card.CardID;
            this.GridView.Rows[row].Cells["colOwnerName"].Value = card.OwnerName;
            this.GridView.Rows[row].Cells["colCardCertificate"].Value = card.CardCertificate;
            this.GridView.Rows[row].Cells["colCardType"].Value = card.CardType.ToString();
            this.GridView.Rows[row].Cells["colCarType"].Value = CarTypeSetting.Current.GetDescription(card.CarType);
            this.GridView.Rows[row].Cells["colEnterDateTime"].Value = card.LastDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.GridView.Rows[row].Cells["colLastCarPlate"].Value = card.LastCarPlate;
            this.GridView.Rows[row].Cells["colEntrance"].Value = GetEntrance(card);
            this.GridView.Rows[row].Cells["colHasPaid"].Value = HasPaid(card);
        }

        private bool HasPaid(CardInfo card)
        {
            if (paymentRecords != null && paymentRecords.Count > 0)
            {
                return paymentRecords.Exists(c => c.EnterDateTime == card.LastDateTime && c.CardID == card.CardID);
            }
            return false;
        }

        private string GetEntrance(CardInfo card)
        {
            if (ParkBuffer.Current != null)
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(card.LastEntrance);
                return entrance != null ? entrance.EntranceName : string.Empty;
            }
            return card.LastEntrance.ToString();
        }

        private void FrmCardInParkReport_ItemSearching(object sender, EventArgs e)
        {
            this.gridCard.Rows.Clear();
            CardPaymentRecordSearchCondition pc = new CardPaymentRecordSearchCondition();
            if (!string.IsNullOrEmpty(this.txtCardID.Text)) pc.CardID = this.txtCardID.Text;
            if (!string.IsNullOrEmpty(this.txtCarPlate.Text)) pc.CarPlate = this.txtCarPlate.Text;
            if (this.comCardType.SelectedCardType != null) pc.CardType = this.comCardType.SelectedCardType;
            pc.RecordDateTimeRange = new DateTimeRange(this.ucDateTimeInterval1.StartDateTime, this.ucDateTimeInterval1.EndDateTime);
            paymentRecords = (new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect)).GetItems(pc).QueryObjects;

            CardSearchCondition cc = new CardSearchCondition();
            cc.CardID = this.txtCardID.Text;
            cc.LastCarPlate = this.txtCarPlate.Text;
            cc.OwnerName = this.txtOwnerName.Text;
            cc.CardCertificate = this.txtCertificate.Text;
            if (this.comCardType.SelectedCardType != null) cc.CardType = this.comCardType.SelectedCardType;
            if (this.carTypeComboBox1.SelectedIndex > 0) cc.CarType = this.carTypeComboBox1.SelectedCarType;
            cc.LastDateTime = new DateTimeRange(this.ucDateTimeInterval1.StartDateTime, this.ucDateTimeInterval1.EndDateTime);
            List<CardInfo> cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(cc).QueryObjects;
            if (cards != null && cards.Count > 0)
            {
                foreach (CardInfo card in cards)
                {
                    if (card.IsInPark) AddCardToGrid(card);
                }
            }
        }

        private void FrmCardInParkReport_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.comCardType.Init();
            this.carTypeComboBox1.Init();
        }

        private void gridCard_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _EventIndex = e.RowIndex;
                CardInfo info = this.GridView.Rows[e.RowIndex].Tag as CardInfo;
                FrmSnapShotViewer frm = new FrmSnapShotViewer();
                frm.PreRecord += new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord += new RecordPositionEventHandler(frm_NextRecord);
                frm.ShowImage(info.LastDateTime, info.CardID);
                frm.ShowDialog();
                frm.PreRecord -= new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord -= new RecordPositionEventHandler(frm_NextRecord);
            }
        }

        void frm_NextRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex < gridCard.Rows.Count - 1)
            {
                this.gridCard.Rows[_EventIndex].Selected = false;
                _EventIndex++;
                this.gridCard.Rows[_EventIndex].Selected = true;
                if (_EventIndex > this.gridCard.FirstDisplayedScrollingRowIndex + this.gridCard.DisplayedRowCount(false) - 1)
                {
                    this.gridCard.FirstDisplayedScrollingRowIndex += this.gridCard.DisplayedRowCount(false);
                }
                CardInfo info = this.GridView.Rows[_EventIndex].Tag as CardInfo;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                frm.ShowImage(info.LastDateTime, info.CardID);
            }
        }

        void frm_PreRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex > 0)
            {
                this.gridCard.Rows[_EventIndex].Selected = false;
                _EventIndex--;
                this.gridCard.Rows[_EventIndex].Selected = true;
                if (_EventIndex < this.gridCard.FirstDisplayedScrollingRowIndex)
                {
                    if (this.gridCard.FirstDisplayedScrollingRowIndex >= this.gridCard.DisplayedRowCount(false))
                    {
                        this.gridCard.FirstDisplayedScrollingRowIndex -= this.gridCard.DisplayedRowCount(false);
                    }
                    else
                    {
                        this.gridCard.FirstDisplayedScrollingRowIndex = 0;
                    }
                }
                CardInfo info = this.GridView.Rows[_EventIndex].Tag as CardInfo;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                frm.ShowImage(info.LastDateTime, info.CardID);
            }
            e.IsTopRecord = (_EventIndex == 0);
        }
    }
}
