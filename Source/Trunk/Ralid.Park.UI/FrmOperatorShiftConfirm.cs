using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;

namespace Ralid.Park.UI
{
    public partial class FrmOperatorShiftConfirm : Form
    {
        public FrmOperatorShiftConfirm()
        {
            InitializeComponent();
        }

        public int TempCardInherit
        {
            get
            {
                return this.txtTempCardInherit.IntergerValue;
            }
            set
            {
                this.txtTempCardInherit.IntergerValue = value;
            }
        }

        public decimal CashInherit
        {
            get
            {
                return txtCashInherit.DecimalValue; 
            }
            set
            {
                txtCashInherit.DecimalValue = value;
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
