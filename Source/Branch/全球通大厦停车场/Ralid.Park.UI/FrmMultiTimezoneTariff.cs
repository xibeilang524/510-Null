using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI
{
    public partial class FrmMultiTimezoneTariff : Form
    {
        public FrmMultiTimezoneTariff()
        {
            InitializeComponent();
        }

        public bool IsAdding { get; set; }
        public TariffBase UpdatingItem { get; set; }

        private void FrmTariffDetail_Load(object sender, EventArgs e)
        {
            this.btnOk.Enabled = OperatorInfo.CurrentOperator.Permit(Ralid.Park.BusinessModel.Enum.Permission.EditSysSetting);

        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void chkFirst2_CheckedChanged(object sender, EventArgs e)
        {
            this.txtTime21.Enabled = chkFirst2.Checked;
            this.txtFee21.Enabled = chkFirst2.Checked;
        }

        private void chkFirst3_CheckedChanged(object sender, EventArgs e)
        {
            this.txtTime31.Enabled = chkFirst3.Checked;
            this.txtFee31.Enabled = chkFirst3.Checked;
        }

        private void chkFirst4_CheckedChanged(object sender, EventArgs e)
        {
            this.txtTime41.Enabled = chkFirst4.Checked;
            this.txtFee41.Enabled = chkFirst4.Checked;
        }

        private void chkFirst5_CheckedChanged(object sender, EventArgs e)
        {
            this.txtTime51.Enabled = chkFirst5.Checked;
            this.txtFee51.Enabled = chkFirst5.Checked;
        }
    }
}
