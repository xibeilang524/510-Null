using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ralid.GeneralLibrary.LED;
using Ralid.GeneralLibrary.CardReader;
using Ralid.GeneralLibrary.CardReader.YCT;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;


namespace Ralid.Park.UI
{
    public partial class FrmYCTPOSPayment : Form
    {
        public FrmYCTPOSPayment()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置羊城通读卡器
        /// </summary>
        public YCTPOS  Reader { get; set; }
        /// <summary>
        /// 获取或设置以元为单位的扣款金额
        /// </summary>
        public decimal Payment { get; set; }
        /// <summary>
        /// 获取或设置收费显示屏
        /// </summary>
        public IParkingLed ChargeLed { get; set; }
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
                        var w = Reader.ReadCard();
                        if (w != null && w.WalletType != 0)
                        {
                            int p = (int)(Payment * 100);
                            if (w.Balance >= p)
                            {
                                var payment = Reader.Paid(p, w.WalletType);
                                if (payment != null)
                                {
                                    YCTPaymentRecord record = CreateRecord(payment);
                                    record.WalletType = w.WalletType;
                                    record.EnterDateTime = DateTime.Now;
                                    record.State = YCTPaymentRecordState.PaidOk;
                                    YCTPaymentRecordBll bll = new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect);
                                    CommandResult result = bll.Insert(record);
                                    string msg = string.Format("扣款{0}元  余额{1}元", Payment, (decimal)payment.本次余额 / 100);
                                    ShowMessage(msg);
                                    if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(msg);
                                    if (ChargeLed != null) ChargeLed.DisplayMsg(msg);
                                    this.DialogResult = DialogResult.OK;
                                    break;
                                }
                                else
                                {
                                    ShowMessage(Resources.Resource1.FrmYCTPayment_Fail + "  原因:" + Reader.LastErrorDescr);
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

        private YCTPaymentRecord CreateRecord(YCTPaymentInfo payment)
        {
            YCTPaymentRecord record = new YCTPaymentRecord();
            record.PID = payment.本次交易设备编号;
            record.PSN = payment.终端交易流水号;
            record.TIM = payment.本次交易日期时间;
            record.FCN = payment.物理卡号;
            record.LCN = payment.逻辑卡号;
            record.TF = payment.交易金额;
            record.FEE = payment.票价;
            record.BAL = payment.本次余额;
            record.TT = payment.交易类型;
            record.ATT = payment.附加交易类型;
            record.CRN = payment.票卡充值交易计数;
            record.XRN = payment.票卡消费交易计数;
            record.DMON = payment.累计门槛月份;
            record.BDCT = payment.公交门槛计数;
            record.MDCT = payment.地铁门槛计数;
            record.UDCT = payment.联乘门槛计数;
            record.EPID = payment.本次交易入口设备编号;
            record.ETIM = payment.本次交易入口日期时间;
            record.LPID = payment.上次交易设备编号;
            record.LTIM = payment.上次交易日期时间;
            record.AREA = payment.区域代码;
            record.ACT = payment.区域卡类型;
            record.SAREA = payment.区域子码;
            record.TAC = payment.交易认证码;
            return record;
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
