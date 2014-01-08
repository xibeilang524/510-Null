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

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardReport : FrmReportBase 
    {
        public FrmCardReport()
        {
            InitializeComponent();
        }

        private void FrmCardReport_Load(object sender, EventArgs e)
        {
            this.comCardStatus.Init();
            this.comCardType.Init();
            this.comChargeType.Init();
            this.cardView.Init();
            this.accessComboBox1.Init();
        }

        protected override void OnItemSearching(EventArgs e)
        {
            List<CardInfo> selections = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetAllCards().QueryObjects;
            if (selections != null && selections.Count > 0)
            {
                string cardID = txtCardID.Text.Trim();
                if (!string.IsNullOrEmpty(cardID))
                {
                    selections = selections.Where(card => card.CardID.Contains(cardID)).ToList();
                }
                string name = txtOwnerName.Text.Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    selections = selections.Where(card => card.OwnerName == name).ToList();
                }
                if (comCardStatus.SelectedIndex > 0)
                {
                    selections = selections.Where(card => card.Status == comCardStatus.CardStatus).ToList();
                }
                if (comCardType.SelectedIndex > 0)
                {
                    selections = selections.Where(card => card.CardType == comCardType.SelectedCardType).ToList();
                }
                if (comChargeType.SelectedIndex > 0)
                {
                    selections = selections.Where(card => card.CarType == comChargeType.SelectedCarType).ToList();
                }
                if (cmbCarStatus.SelectedIndex == 1)
                {
                    selections = selections.Where(card => card.IsInPark == true).ToList();
                }
                else if (cmbCarStatus.SelectedIndex == 2)
                {
                    selections = selections.Where(card => card.IsInPark == false).ToList();
                }
                if (!string.IsNullOrEmpty(txtCarPlate.Text.Trim()))
                {
                    selections = selections.Where(card => !string.IsNullOrEmpty(card.CarPlate) && card.CarPlate.Contains(txtCarPlate.Text.Trim())).ToList();
                }
                if (!string.IsNullOrEmpty(txtCardCertificate.Text.Trim()))
                {
                    selections = selections.Where(card => !string.IsNullOrEmpty(card.CardCertificate) && card.CardCertificate.Contains(txtCardCertificate.Text.Trim())).ToList();
                }
                if (accessComboBox1.AccesslevelID > 0)
                {
                    byte accessID = accessComboBox1.AccesslevelID;
                    selections = selections.Where(card => card.AccessID == accessID).ToList();
                }
                if (chkExpireDateAfter.Checked)
                {
                    selections = selections.Where(card => card.ValidDate >= dtExpireDateAfter.Value).ToList();
                }
                if (chkExpireDateBefore.Checked)
                {
                    selections = selections.Where(card => card.ValidDate <= dtExpireDateBefore.Value).ToList();
                }
            }
            this.cardView.CardSource = selections;
        }
    }
}
