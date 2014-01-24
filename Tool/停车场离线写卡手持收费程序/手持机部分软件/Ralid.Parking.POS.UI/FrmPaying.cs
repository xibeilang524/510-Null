using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Ralid.Parking.POS.Device;
using Ralid.Parking.POS.Model;
using Ralid.Parking.POS.DAL;

namespace Ralid.Parking.POS.UI
{
    public partial class FrmPaying : Form
    {
        public FrmPaying()
        {
            InitializeComponent();
        }

        #region 私有变量
        private CardInfoReader _CardReader = null;
        private CardPaymentInfo _ChargeRecord;
        private CardInfo _Card;
        #endregion

        #region 私有方法
        private void Clear()
        {
            _ChargeRecord = null;
            _Card = null;
            lblCardID.Text = string.Empty;
            lblEnterDT.Text = string.Empty;
            lblInterval.Text = string.Empty;
            lblTotal.Text = string.Empty;
            lblHasPaid.Text = string.Empty;
            lblAccount.Text = string.Empty;
            txtPaid.Text = string.Empty;
            this.timer1.Enabled = true;
        }

        private void ShowCardPaymentInfo(CardPaymentInfo cardPayment)
        {
            lblCardID.Text = cardPayment.CardID;
            if (cardPayment.EnterDateTime != null) lblEnterDT.Text = cardPayment.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:dd");
            lblHasPaid.Text = cardPayment.LastTotalPaid.ToString();
            lblInterval.Text = cardPayment.TimeInterval;
            lblTotal.Text = cardPayment.ParkFee.ToString();
            lblHasPaid.Text = cardPayment.LastTotalPaid.ToString();
            lblAccount.Text = cardPayment.Accounts.ToString();
            txtPaid.Text = cardPayment.Accounts.ToString();
            btnOK.Focus();
        }
        #endregion

        #region 事件处理程序
        private void FrmPaying_Load(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_ChargeRecord == null) return;

            decimal paid = 0;
            try
            {
                paid = decimal.Parse(txtPaid.Text);
                if (paid > _ChargeRecord.Accounts)
                {
                    MessageBox.Show("实收金额不能大于应收金额");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("输入的实收金额不是有效的数字");
                return;
            }

            CardInfo card = _Card.Clone(); //先保存一个克隆版

            _ChargeRecord.Paid = paid;
            _ChargeRecord.Discount = _ChargeRecord.Accounts - paid;
            _ChargeRecord.Operator = OperatorInfo.CurrentOperator.OperatorName;

            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string mdf = Path.Combine(appPath, "Record.txt");

            //初始化所有配置参数
            CardPaymentInfoProvider provider = new CardPaymentInfoProvider();
            bool ret = provider.Add(_ChargeRecord); //保存数据
            if (ret)
            {
                card.SetPaidData(_ChargeRecord);  //更新卡片状态
                FrmCardWrite frm = new FrmCardWrite();
                frm.Location = new Point(0, this.btnOK.Top - 10);
                frm.Card = card;
                frm.CardReader = _CardReader;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    provider.SaveToFile(_ChargeRecord, mdf);
                    Clear();
                }
                else  //// 写卡不成功，将保存的数据删除
                {
                    provider.Delete(_ChargeRecord);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_CardReader == null)
            {
                _CardReader = new CardInfoReader();
                _CardReader.ParkingKey = MySetting.Current.ParkingKey;
                _CardReader.ParkingSection = MySetting.Current.ParkingSection;
                _CardReader.Device = new HPC3000();
                _CardReader.OpenDevice();
            }

            CardInfo card = _CardReader.Read();
            if (card != null)
            {
                this.timer1.Enabled = false;
                ReadCardHandler(card);
            }
        }

        /// <summary>
        /// 读取到卡号处理
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="info">从卡片扇区数据中读取到的卡片信息</param>
        private void ReadCardHandler(CardInfo card)
        {
            string msg = string.Empty;
            if (card.OnlineHandleWhenOfflineMode)
            {
                MessageBox.Show("卡片不能写入，请到停车场收费处缴费");
                this.timer1.Enabled = true;
                return; //卡片只能在线处理，返回
            }
            if (!card.IsInPark)
            {
                MessageBox.Show("卡片已出场");
                this.timer1.Enabled = true;
                return;
            }
            if (card.LastDateTime > DateTime.Now)
            {
                MessageBox.Show("入场时间大于当前时间");
                this.timer1.Enabled = true;
                return;
            }
            if (MySetting.Current.GetTariff(card.CardType.ID, card.CarType) == null)
            {
                MessageBox.Show("非收费卡，可直接出场");
                this.timer1.Enabled = true;
                return;
            }

            _Card = card;
            _ChargeRecord = CardPaymentInfoFactory.CreateCardPaymentRecord(card, MySetting.Current, card.CarType, DateTime.Now);
            ShowCardPaymentInfo(_ChargeRecord);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPaying_Closed(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            _CardReader.CloseDevice();
        }
        #endregion
    }
}