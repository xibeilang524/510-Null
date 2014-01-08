using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park .BusinessModel.Enum;
using Ralid.Park .BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    //add by Jan 2012-5-15
    public partial class FrmClose : Form
    {
        public FrmClose()
        {
            InitializeComponent();
        }

        private void FrmClose_Load(object sender, EventArgs e)
        {
            this.lblOperatorID.Text = OperatorInfo.CurrentOperator.OperatorID;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            string OperatorID = this.lblOperatorID.Text.Trim();
            string pwd = this.txtPassword.Text.Trim();

            if (pwd.Length == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmClose_EmptyPwd);
                return;
            }

            if (DoExit(OperatorID, pwd) == false)
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_AuthenFail);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool DoExit(string OperatorID, string pwd)
        {
            OperatorBll authen = new OperatorBll(AppSettings.CurrentSetting.ParkConnect);
            if (authen.Authentication(OperatorID, pwd))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();

                return true;
            }
            else
            {
                return false;
            }

        }
    }
    //end
}
