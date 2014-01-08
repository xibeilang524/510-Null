using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Ralid.Park.BusinessModel.Configuration;

namespace OutDoorLEDTool
{
    public partial class FrmConnect : Form
    {
        public FrmConnect()
        {
            InitializeComponent();
        }

        private bool CheckConnect(string conStr)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = conStr;
                    con.Open();
                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void FrmConnect_Load(object sender, EventArgs e)
        {
            this.txtParkUserID.Enabled = rdParkUser.Checked;
            this.txtParkPasswd.Enabled = rdParkUser.Checked;
            if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.ParkConnect))
            {
                if (CheckConnect(AppSettings.CurrentSetting.ParkConnect))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnParkApply_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.PersistSecurityInfo = true;
            sb.IntegratedSecurity = rdParkSystem.Checked;
            sb.DataSource = txtParkServer.Text;
            sb.InitialCatalog = txtParkDB.Text;
            sb.UserID = txtParkUserID.Text;
            sb.Password = txtParkPasswd.Text;
            if (CheckConnect(sb.ConnectionString))
            {
                AppSettings.CurrentSetting.ParkConnect = sb.ConnectionString;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(Resource1.FrmConnect_SQLConnectedFail);
            }
        }

        private void rdParkSystem_CheckedChanged(object sender, EventArgs e)
        {
            this.txtParkUserID.Enabled = rdParkUser.Checked;
            this.txtParkPasswd.Enabled = rdParkUser.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
