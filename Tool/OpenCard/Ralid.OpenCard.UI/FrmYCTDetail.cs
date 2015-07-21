using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park .BusinessModel .Model ;
using Ralid.OpenCard .OpenCardService .YCT ;

namespace Ralid.OpenCard.UI
{
    public partial class FrmYCTDetail : Form
    {
        public FrmYCTDetail()
        {
            InitializeComponent();
            comEntrance.Init();
        }

        #region 公共属性
        public YCTItem YCTItem { get; set; }
        #endregion

        #region 事件处理
        private void FrmYCTDetail_Load(object sender, EventArgs e)
        {
            txtComport.Init();
            if (YCTItem != null)
            {
                txtID.Text = YCTItem.ID;
                txtID.Enabled = false;
                txtComport.ComPort = YCTItem.Comport;
                if (YCTItem.EntranceID.HasValue) comEntrance.SelectedEntranceID = YCTItem.EntranceID.Value;
                txtMemo.Text = YCTItem.Memo;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("没有设置读卡器编号");
                return;
            }
            if (txtComport.ComPort <= 0)
            {
                MessageBox.Show("没有设置串口");
                return;
            }
            if (YCTItem == null)
            {
                YCTItem = new YCTItem();
                YCTItem.ID = txtID.Text.Trim();
            }
            YCTItem.Comport = txtComport.ComPort;
            if (string.IsNullOrEmpty(comEntrance.Text)) YCTItem.EntranceID = null;
            else YCTItem.EntranceID = comEntrance.SelectedEntranceID;
            YCTItem.Memo = txtMemo.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
