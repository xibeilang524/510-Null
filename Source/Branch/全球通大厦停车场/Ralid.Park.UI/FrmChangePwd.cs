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
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmChangePwd : Form
    {
        public FrmChangePwd()
        {
            InitializeComponent();
        }

        public OperatorInfo Operator { get; set; }

        private void FrmChangePwd_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                OperatorBll bll = new OperatorBll(AppSettings.CurrentSetting.ParkConnect);
                Operator.Password = txtNewPwd.Text;
                CommandResult result = bll.Update(Operator);
                if (result.Result == ResultCode.Successful)
                {
                    this.Close();
                }
                else
                {
                    Operator.Password = txtOldPwd.Text;
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckInput()
        {
            if (Operator.Password != txtOldPwd.Text)
            {
                MessageBox.Show(Resources.Resource1.FrmChangePwd_InvalidOldPwd);
                txtOldPwd.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(txtNewPwd.Text.Trim()))
            {
                MessageBox.Show(Resources.Resource1.FrmChangePwd_InvalidNewPwd);
                txtNewPwd.SelectAll();
                return false;
            }
            if (txtNewPwd.Text != txtConfirmPwd.Text)
            {
                MessageBox.Show(Resources.Resource1.FrmChangePwd_InvalidConfirmPwd);
                txtConfirmPwd.SelectAll();
                return false;
            }
            return true;
        }
    }
}
