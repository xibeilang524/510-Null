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
    public partial class frmTempCardSetting : Form
    {
        public frmTempCardSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置临时卡数量
        /// </summary>
        public int TempCardCount
        {
            get
            {
                return this.intergerTextBox1.IntergerValue;
            }
            set
            {
                this.intergerTextBox1.IntergerValue = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (TempCardCount >= 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(Resources.Resource1.FrmTempCardSetting_InvalidTempCard);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmTempCardSetting_Load(object sender, EventArgs e)
        {
            this.intergerTextBox1.Focus();
            this.intergerTextBox1.SelectAll();
        }

    }
}
