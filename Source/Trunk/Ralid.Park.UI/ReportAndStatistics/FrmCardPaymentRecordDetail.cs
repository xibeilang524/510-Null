using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardPaymentRecordDetail : Form
    {
        public FrmCardPaymentRecordDetail()
        {
            InitializeComponent();
        }

        public CardPaymentInfo CardPaymentRecord { get; set; }

        private void FrmCardPaymentRecordDetail_Load(object sender, EventArgs e)
        {
            if (CardPaymentRecord != null)
            {
                ShowCardChargeInfo(CardPaymentRecord);
            }
        }

        private void ShowCardChargeInfo(CardPaymentInfo cardPayment)
        {
            this.txtCardID.Text = cardPayment.CardID;
            this.txtCardID.SelectAll();
            this.lblOwnerName.Text = cardPayment.OwnerName;
            this.lblCarNum.Text = cardPayment.CarPlate;
            this.lblEnterDateTime.Text = cardPayment.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblExitDateTime.Text = cardPayment.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblParkingTime.Text = cardPayment.TimeInterval;
            this.lblCardType.Text = cardPayment.CardType.ToString();
            this.lblTariffType.Text = Ralid.Park.BusinessModel.Resouce.TariffTypeDescription.GetDescription(cardPayment.TariffType);
            this.lblLastPaid.Text = cardPayment.LastTotalPaid.ToString();
            this.lblLastDiscount.Text = cardPayment.LastTotalDiscount.ToString();
            this.lblAccounts.Text = cardPayment.Accounts.ToString();
            this.txtChargedMoney.DecimalValue = cardPayment.Paid;
            this.txtChargedMoney.Focus();
            this.txtMemo.Text = cardPayment.Memo;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CardPaymentRecord != null)
            {
                AlarmInfo alarm = new AlarmInfo();
                alarm.AlarmDateTime = DateTime.Now;
                alarm.AlarmType = AlarmType.ModifyCardPayment;
                alarm.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                alarm.AlarmDescr = string.Format(Resources.Resource1.FrmCardPaymentRecordDetail_ModifyPayment,
                    CardPaymentRecord.CardID,
                    CardPaymentRecord.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    CardPaymentRecord.EnterDateTime.Value.ToString("yyyy-MM-dd HH;mm:ss"),
                    CardPaymentRecord.Accounts,
                    CardPaymentRecord.Paid, 
                    CardPaymentRecord.Memo, 
                    txtChargedMoney.DecimalValue, 
                    txtMemo.Text);

                CommandResult ret = (new AlarmBll(AppSettings.CurrentSetting.ParkConnect)).Insert(alarm);
                if (ret.Result == ResultCode.Successful)
                {
                    CardPaymentRecord.Paid = this.txtChargedMoney.DecimalValue;
                    CardPaymentRecord.Memo = this.txtMemo.Text;
                    ret = (new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect)).Update(CardPaymentRecord);
                    if (ret.Result == ResultCode.Successful)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(ret.Message);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
