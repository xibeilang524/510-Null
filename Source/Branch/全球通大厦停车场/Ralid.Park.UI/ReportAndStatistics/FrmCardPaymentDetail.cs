using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.BusinessModel .Model ;
using Ralid.BusinessModel.Report;
using Ralid.BusinessLayer;

namespace Ralid.Monitor
{
    public partial class FrmCardPaymentDetail : Form
    {
        public FrmCardPaymentDetail()
        {
            InitializeComponent();
        }

        public CardEventReport CardEvent { get; set; }

        private void ShowCardPaymentDetail(CardEventReport info)
        {
            this.lblCardID.Text = info.CardID;
            this.lblOwnerName.Text = info.OwnerName;
            this.lblExitDateTime.Text = info.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblChargingMoneyPlan.Text = info.Accounts.ToString();
            this.lblCardType.Text = info.CardType.ToString();
            this.lblChargeMoney.Text = info.Paid.ToString();
            if (info.LastDateTime != null)
            {
                this.lblEnterDateTime.Text = info.LastDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                this.lblParkingTime.Text = info.ParkInterval;
                Image imgIn = SnapShotBll.GetFirstSnapShot(info.LastDateTime.Value);
                if (imgIn != null)
                {
                    this.picIn.Image = imgIn;
                }
            }
            Image imgOut = SnapShotBll.GetFirstSnapShot(info.EventDateTime);
            if (imgOut != null)
            {
                this.picOut.Image = imgOut;
            }
        }

        private void CardEventDetail_Load(object sender, EventArgs e)
        {
            this.lblCardID.Text = string.Empty;
            this.lblOwnerName.Text = string.Empty;
            this.lblExitDateTime.Text = string.Empty;
            this.lblChargingMoneyPlan.Text = string.Empty;
            this.lblCardType.Text = string.Empty;
            this.lblCarNum.Text = string.Empty;
            this.lblEnterDateTime.Text = string.Empty;
            this.lblChargeMoney.Text = string.Empty;
            this.lblParkingTime.Text = string.Empty;
            if (CardEvent != null)
            { 
                ShowCardPaymentDetail(CardEvent);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
