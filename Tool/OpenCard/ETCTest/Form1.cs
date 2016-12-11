using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.OpenCard.OpenCardService.ETC;

namespace ETCTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ETCController _Controller = new ETCController();

        private void AddDeviceToGrid(ETCDevice[] items)
        {
            dataGridView1.Rows.Clear();
            if (items != null && items.Length > 0)
            {
                foreach (var d in items)
                {
                    var row = dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Tag = d;
                    dataGridView1.Rows[row].Cells["colLaneNo"].Value = d.LaneNo;
                    dataGridView1.Rows[row].Cells["colIP"].Value = d.IPAddr;
                    dataGridView1.Rows[row].Cells["colPort"].Value = d.Port;
                    dataGridView1.Rows[row].Cells["colUserName"].Value = d.UserName;
                    dataGridView1.Rows[row].Cells["colPassword"].Value = d.Password;
                    dataGridView1.Rows[row].Cells["colProvinceNo"].Value = d.ProvinceNo;
                    dataGridView1.Rows[row].Cells["colCityNo"].Value = d.CityNo;
                    dataGridView1.Rows[row].Cells["colAreaNo"].Value = d.AreaNo;
                    dataGridView1.Rows[row].Cells["colEcRSUID"].Value = d.EcRSUID;
                    dataGridView1.Rows[row].Cells["colEcReaderID"].Value = d.EcReaderID;
                    dataGridView1.Rows[row].Cells["colTimeout"].Value = d.TimeOut;
                    dataGridView1.Rows[row].Cells["colHeartBeatTime"].Value = d.HeartBeatTime;
                }
            }
            lblCount.Text = string.Format("总共 {0} 项", dataGridView1.Rows.Count);
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _Controller.Init();
            AddDeviceToGrid(_Controller.ETCDevices);
            btnInit.Enabled = false;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                ETCDevice d = row.Tag as ETCDevice;
                var ret = _Controller.HeartBeatEx(d.LaneNo);
                row.Cells["colState"].Value = ret == 0 ? "连接正常" : "断开连接";
            }
        }

        private void 天线扣费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var device = dataGridView1.SelectedRows[0].Tag as ETCDevice;
                device.DoRSUPay(1); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
