using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ralid.Parking .POS .Device;
using Ralid.Parking .POS .Model ;

namespace Ralid.Parking.POS.UI
{
    public partial class FrmCardWrite : Form
    {
        public FrmCardWrite()
        {
            InitializeComponent();
        }

        #region 公共属性
        public CardInfo Card { get; set; }

        public CardInfoReader CardReader { get; set; }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.DialogResult = DialogResult.Cancel;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool ret = CardReader.Write(Card);
            if (ret)
            {
                this.timer1.Enabled = false;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FrmCardWrite_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
        }
    }
}