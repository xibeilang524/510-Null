using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.Park.UI
{
    public partial class FrmHandInCashConfirm : Form
    {
        public FrmHandInCashConfirm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置实收金额
        /// </summary>
        public decimal HandInCash
        {
            get { return this.txtHandInCash.DecimalValue; }
            set
            {
                this.txtHandInCash.DecimalValue = value;
            }
        }

        /// <summary>
        /// 获取或设置POS机实收
        /// </summary>
        public decimal HandInPOS
        {
            get { return this.txtHandInPOS.DecimalValue; }
            set
            {
                this.txtHandInPOS.DecimalValue = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
