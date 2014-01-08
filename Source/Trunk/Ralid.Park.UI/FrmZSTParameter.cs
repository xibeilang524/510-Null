using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmZSTParameter : Form
    {
        public FrmZSTParameter()
        {
            InitializeComponent();
        }

        #region 公共属性
        public ZSTReader Reader{get;set;}

        public string ReaderIP { get; set; }
        #endregion

        #region 事件处理程序
        private void btn_ConsumptionReport_Click(object sender, EventArgs e)
        {
            string report = Reader.ConsumptionReport(ReaderIP, ucDateTimeInterval1.StartDateTime, ucDateTimeInterval1.EndDateTime);
            txtMessage.Text = report;
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            string report = Reader.UploadFile(ReaderIP);
            txtMessage.Text = report;
        }

        private void btnSetUploadTime_Click(object sender, EventArgs e)
        {
            List<DateTime> times = new List<DateTime>();
            if (dateTimePicker1.Checked) times.Add(dateTimePicker1.Value);
            if (dateTimePicker2.Checked) times.Add(dateTimePicker2.Value);
            if (dateTimePicker3.Checked) times.Add(dateTimePicker3.Value);
            if (dateTimePicker4.Checked) times.Add(dateTimePicker4.Value);
            if (dateTimePicker5.Checked) times.Add(dateTimePicker5.Value);
            if (dateTimePicker6.Checked) times.Add(dateTimePicker6.Value);
            if (dateTimePicker7.Checked) times.Add(dateTimePicker7.Value);
            if (dateTimePicker8.Checked) times.Add(dateTimePicker8.Value);
            string report = Reader.SetUploadTimer(ReaderIP, times);
            txtMessage.Text = report;
        }
        #endregion

        private void FrmZSTParameter_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.Init();
            this.ucDateTimeInterval1.SelectToday();
            this.Text += " [" + ReaderIP + "]";
        }
    }
}
