using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.ThirdCommunication
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        public void WriteLine(string msg)
        {
            eventReportListBox1.InsertMessage(msg);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = "交委接口 " + Application.ProductVersion;
            FrmConnect frm = new FrmConnect();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                mnu_Exit_Click(this.mnu_Exit, EventArgs.Empty);
                return;
            }

            ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
            ParkBuffer.Current.InValid();
            string str = AppSettings.CurrentSetting.GetConfigContent("ThirdCommunication");
            ThirdCommunicationServer server = new ThirdCommunicationServer(str);
            server.MainForm = this;
            server.Start();
        }

        private void mnu_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
