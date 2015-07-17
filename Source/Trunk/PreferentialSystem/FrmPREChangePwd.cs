using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;

namespace PreferentialSystem
{
    public partial class FrmPREChangePwd : Form
    {
        public FrmPREChangePwd()
        {
            InitializeComponent();
        }

        public PREOperatorInfo Operator { get; set; }

        private void FrmPREChangePwd_Load(object sender, EventArgs e)
        {
            if (Operator != null && Operator.Role != null && !Operator.Role.IsAdmin &&
               PREOperatorInfo.CurrentOperator.Role != null && PREOperatorInfo.CurrentOperator.Role.IsAdmin)  //系统管理员修改其它人员的密码时不用再输入旧密码
            {
                txtOldPwd.Text = "*************";
                txtOldPwd.Enabled = false;
            }
            else
            {
                txtOldPwd.Enabled = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                PREOperatorBll bll = new PREOperatorBll(AppSettings.CurrentSetting.ParkConnect);
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
            if (txtOldPwd.Enabled && Operator.Password != txtOldPwd.Text)
            {
                MessageBox.Show("旧密码无效！");
                txtOldPwd.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(txtNewPwd.Text.Trim()))
            {
                MessageBox.Show("没有新密码！");
                txtNewPwd.SelectAll();
                return false;
            }
            if (txtNewPwd.Text != txtConfirmPwd.Text)
            {
                MessageBox.Show("新密码两次输入的不一致！");
                txtConfirmPwd.SelectAll();
                return false;
            }
            return true;
        }
    }
}
