using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;

namespace Ralid.Park.UI
{
    public partial class FrmYCTPayment : Form
    {
        public FrmYCTPayment()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置羊城通读卡器
        /// </summary>
        public Ralid.GeneralLibrary.CardReader.YangChengTongReader Reader { get; set; }
        /// <summary>
        /// 获取或设置以元为单位的扣款金额
        /// </summary>
        public decimal Payment { get; set; }
        #endregion

        #region 私有变量
        private Thread _YCTThread;
        #endregion

        private void YCTPayment_Thread()
        {
            try
            {
                while (true)
                {
                    if (Reader != null)
                    {
                        YangChengTongCardInfo card;
                        if (Reader.ReadCard(out card) == YangChengTongOperationResult.Success)
                        {
                            if (card.Balance >= Payment)
                            {
                                YangChengTongPaymentRecord record;
                                YangChengTongOperationResult ret = Reader.CardPay(Payment, out record);
                                if (ret == YangChengTongOperationResult.Success)
                                {
                                    YangChenTongLog log = new YangChenTongLog()
                                    {
                                        LogDateTime = DateTime.Now,
                                        CardID = record.CardID,
                                        LogicalID = record.LogicalID,
                                        Payment = record.Payment,
                                        Balance = record.Balance,
                                        Data = record.Data
                                    };
                                    (new YangChenTongLogBll(AppSettings.CurrentSetting.ParkConnect)).Insert(log);
                                    ShowMessage(string.Format("初始余额 {0}   扣款 {1}  剩余 {2}", card.Balance, Payment, card.Balance - Payment));
                                    Reader.Beep(100);
                                    if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(Resources.Resource1.FrmYCTPayment_Success);
                                    this.DialogResult = DialogResult.OK;
                                    break;
                                }
                                else
                                {
                                    ShowMessage(Resources.Resource1.FrmYCTPayment_Fail + "  Reason:" + ret.ToString());
                                    if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(Resources.Resource1.FrmYCTPayment_Fail);
                                }
                            }
                            else
                            {
                                ShowMessage(Resources.Resource1.FrmYCTPayment_BalanceNotEnough);
                                if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(Resources.Resource1.FrmYCTPayment_BalanceNotEnough);
                            }
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {

            }
        }

        private void ShowMessage(string msg)
        {
            Action action = delegate()
            {
                this.label1.Text = msg;
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

        private void FrmYCTPayment_Load(object sender, EventArgs e)
        {
            if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(Resources.Resource1.FrmYCTPayment_PleaseRead);
            _YCTThread = new Thread(YCTPayment_Thread);
            _YCTThread.IsBackground = true;
            _YCTThread.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _YCTThread.Abort();
            this.Close();
        }
    }
}
