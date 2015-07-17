using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI
{
    public partial class FrmVipCardReader : Form
    {
        public FrmVipCardReader()
        {
            InitializeComponent();

            VipCardName = "会员卡";
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置从发卡机上读到的卡号
        /// </summary>
        public string VipCardID { get; set; }
        /// <summary>
        /// 获取或设置需要读取的卡片类型名称
        /// </summary>
        public string VipCardName { get; set; }
        #endregion

        #region
        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVipCardReader_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void FrmVipCardReader_Load(object sender, EventArgs e)
        {
            this.Text = "请刷" + VipCardName;
            this.label1.Text = "请在发卡机上刷" + VipCardName;
            CardReaderManager.GetInstance(UserSetting .Current .WegenType).PushCardReadRequest(CardReadEventHandler);
        }

        private void FrmVipCardReader_FormClosed(object sender, FormClosedEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadEventHandler);
        }

        private void CardReadEventHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                VipCardID = e.CardID;
                this.DialogResult = DialogResult.OK;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        #endregion
    }
}
