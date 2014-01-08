using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmZSTPayment : Form
    {
        public FrmZSTPayment()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置以元为单位的扣款金额
        /// </summary>
        public decimal Payment { get; set; }
        #endregion

        private void FrmZSTPayment_Load(object sender, EventArgs e)
        {
            FrmZSTSetting frm = FrmZSTSetting.GetInstance();
            frm.ZSTReader.MessageRecieved -= new EventHandler<GeneralLibrary.CardReader.ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            frm.ZSTReader.MessageRecieved += new EventHandler<GeneralLibrary.CardReader.ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
            frm.ZSTReader.Consumption(AppSettings.CurrentSetting.ZSTReaderIP, Payment);
        }

        private void ZSTReader_MessageRecieved(object sender, GeneralLibrary.CardReader.ZSTReaderEventArgs e)
        {
            if (e.MessageType == "2")
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (e.MessageType == "3")
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmZSTPayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmZSTSetting frm = FrmZSTSetting.GetInstance();
            frm.ZSTReader.MessageRecieved -= new EventHandler<GeneralLibrary.CardReader.ZSTReaderEventArgs>(ZSTReader_MessageRecieved);
        }
    }
}
