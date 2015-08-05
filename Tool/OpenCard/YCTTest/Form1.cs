using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.OpenCard.OpenCardService.YCT;

namespace YCTTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private YCTPOS reader = null;
        private YCTWallet _Wallet = null;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "断开")
            {
                if (reader != null) reader.Close();
                reader = null;
                btnReadCurCard.Enabled = false;
                btnReduceBalance.Enabled = false;
                btnConnect.Text = "连接";
            }
            else
            {
                if (comPort.ComPort > 0)
                {
                    reader = new YCTPOS(comPort.ComPort, 57600);
                    reader.Open();
                    btnReadCurCard.Enabled = reader.IsOpened;
                    btnConnect.Text = reader.IsOpened ? "断开" : "连接";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comPort.Init();
        }

        private void btnReadCurCard_Click(object sender, EventArgs e)
        {
            if (reader != null && reader.IsOpened)
            {
                var wallet = reader.Poll();
                if (wallet != null)
                {
                    ShowWallet(wallet);
                    _Wallet = wallet;
                    btnReduceBalance.Enabled = true;
                }
                else
                {
                    btnReduceBalance.Enabled = false;
                    MessageBox.Show("读羊城通失败");
                }
            }
        }

        private void ShowWallet(YCTWallet w)
        {
            cmbWType.SelectedIndex = w.WalletType;
            this.txtPhysicalID.Text = w.PhysicalCardID;
            this.txtLogicID.Text = w.LogicCardID;
            this.txtCardType.Text = w.CardType;
            this.txtBalance.DecimalValue = (decimal)w.Balance / 100;
            this.txtCount.DecimalValue = w.Count;
            this.txtDeposit.DecimalValue = (decimal)w.Deposit / 100;
        }

        private void btnReduceBalance_Click(object sender, EventArgs e)
        {
            if (txtAmount.DecimalValue > 0 && _Wallet != null && reader.IsOpened)
            {
                var pay = reader.Prepaid((int)(txtAmount.DecimalValue * 100), _Wallet.WalletType);
                if (pay != null)
                {
                    var tac = reader.CompletePaid();
                    if (!string.IsNullOrEmpty(tac))
                    {
                        MessageBox.Show("扣款成功");
                        btnReadCurCard.PerformClick();
                        return;
                    }
                }
                MessageBox.Show("扣款失败, 错误:" + reader.LastError.ToString());
            }
        }
    }
}
