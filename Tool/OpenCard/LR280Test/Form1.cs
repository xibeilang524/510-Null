using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.OpenCard.OpenCardService.LR280;
using Newtonsoft.Json;

namespace LR280Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private LR280POS reader = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comPort.Init();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "断开")
            {
                if (reader != null) reader.Close();
                reader = null;
                btn读卡.Enabled = false;
                btn扣款.Enabled = false;
                btn签到.Enabled = false;
                btn查余额.Enabled = false;
                btn结算.Enabled = false;
                btnConnect.Text = "连接";
            }
            else
            {
                if (comPort.ComPort > 0)
                {
                    reader = new LR280POS(comPort.ComPort, 9600);
                    reader.Log = true;
                    var ret = reader.Open();
                    if (ret == 0)
                    {
                        btnConnect.Text = "断开";
                        btn读卡.Enabled = true;
                        btn签到.Enabled = true;
                        btn结算.Enabled = true;
                        btn查余额.Enabled = true;
                        btn扣款.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("打开设备失败");
                        reader = null;
                    }
                }
            }
        }

        private void btn签到_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                var ret = reader.CheckIn();
                txtResponse .Text = JsonConvert.SerializeObject(ret);
            }
        }

        private void btn结算_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                var ret = reader.Clear();
                txtResponse.Text = JsonConvert.SerializeObject(ret);
            }
        }

        private void btn读卡_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                var ret = reader.ReadCard();
                txtResponse.Text = JsonConvert.SerializeObject(ret);
            }
        }

        private void btn查余额_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                var ret = reader.查余额();
                txtResponse.Text = JsonConvert.SerializeObject(ret);
            }
        }

        private void btn扣款_Click(object sender, EventArgs e)
        {
            if (reader != null && txtAmount.DecimalValue >= 0)
            {
                var ret = reader.Pay("0000000000", (int)(txtAmount.DecimalValue * 100));
                txtResponse.Text = JsonConvert.SerializeObject(ret);
            }
        }

        
    }
}
