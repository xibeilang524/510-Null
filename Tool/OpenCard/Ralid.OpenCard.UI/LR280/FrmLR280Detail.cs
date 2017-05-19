using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.OpenCard.OpenCardService.LR280;

namespace Ralid.OpenCard.UI.LR280
{
    public partial class FrmLR280Detail : Form
    {
        public FrmLR280Detail()
        {
            InitializeComponent();
            comEntrance.Init();
        }

        #region 公共属性
        public LR280Item LR280Item { get; set; }
        #endregion

        #region 事件处理
        private void FrmLR280Detail_Load(object sender, EventArgs e)
        {
            txtComport.Init();
            if (LR280Item != null)
            {
                txtComport.ComPort = LR280Item.Comport;
                if (LR280Item.EntranceID.HasValue) comEntrance.SelectedEntranceID = LR280Item.EntranceID.Value;
                txtMemo.Text = LR280Item.Memo;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtComport.ComPort <= 0)
            {
                MessageBox.Show("没有设置串口");
                return;
            }
            if (LR280Item == null)
            {
                LR280Item = new LR280Item();
            }
            LR280Item.Comport = txtComport.ComPort;
            if (string.IsNullOrEmpty(comEntrance.Text)) LR280Item.EntranceID = null;
            else LR280Item.EntranceID = comEntrance.SelectedEntranceID;
            LR280Item.Memo = txtMemo.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
