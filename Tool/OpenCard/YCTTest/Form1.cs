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

        private void eventList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (comPort.ComPort > 0)
            {
                reader = new YCTPOS(comPort.ComPort, 57600);
                reader.Open();
                MessageBox.Show("打开读卡器" + (reader.IsOpened ? "成功" : "失败"));
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
                    this.txtBalance.DecimalValue = wallet.Balance;
                }
                else
                {
                    MessageBox.Show("读羊城通失败");
                }
            }
        }

        private void btnReduceBalance_Click(object sender, EventArgs e)
        {
            
        }
    }
}
