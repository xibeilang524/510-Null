using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.OpenCard.UI
{
    public partial class FrmOperatorSelection : Form
    {
        public FrmOperatorSelection()
        {
            InitializeComponent();
        }

        #region 公共属性
        public Ralid.Park.BusinessModel.Model.OperatorInfo SelectedOperator { get; set; }
        #endregion

        private void FrmOperatorSelection_Load(object sender, EventArgs e)
        {
            this.comOperator.Init();
            if (OperatorInfo.CurrentOperator != null) comOperator.OperatorID = OperatorInfo.CurrentOperator.OperatorID;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comOperator.SelectecOperator == null)
            {
                MessageBox.Show("请选择一个操作员");
                return;
            }
            SelectedOperator = comOperator.SelectecOperator;
            this.DialogResult = DialogResult.OK;
        }
    }
}
