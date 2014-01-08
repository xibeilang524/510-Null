using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECardInterface
{
    public partial class FrmNotifyTest : Form
    {
        public FrmNotifyTest()
        {
            InitializeComponent();
        }

        public string Msg
        {
            get
            {
                return txtMsg.Text;
            }
            set
            {
                txtMsg.Text = value;
            }
        }

        public string CardID
        {
            get
            {
                return txtCardID.Text;
            }
            set
            {
                txtCardID.Text = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMsg.Text) && !string.IsNullOrEmpty(txtCardID.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
