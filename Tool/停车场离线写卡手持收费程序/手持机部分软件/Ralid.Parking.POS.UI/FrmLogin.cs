using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ralid.Parking.POS.Model;

namespace Ralid.Parking.POS.UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.comOperator.Items.Clear();
            if (MySetting.Current.Operators != null && MySetting.Current.Operators.Count > 0)
            {
                foreach (OperatorInfo opt in MySetting.Current.Operators)
                {
                    this.comOperator.Items.Add(opt.OperatorID);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comOperator.SelectedIndex >= 0)
            {
                OperatorInfo opt = MySetting.Current.Operators.SingleOrDefault(item => item.OperatorID == comOperator.Text);
                if (opt != null && (new Tool.DTEncrypt()).DSEncrypt(opt.Password) == txtPassword.Text)
                {
                    OperatorInfo.CurrentOperator = opt;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("密码不正确");
                }
            }
        }
    }
}