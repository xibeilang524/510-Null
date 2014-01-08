using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Result;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmRemoteReadCard : Form
    {
        public FrmRemoteReadCard()
        {
            InitializeComponent();
        }

        #region 私有变量
        private byte[] _parkingData;
        #endregion

        #region 公共属性和方法
        /// <summary>
        /// 获取或设置要远程读卡的卡号
        /// </summary>
        public string CardID
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        /// <summary>
        /// 获取或设置远程读卡的停车场卡片数据（用于写卡模式）
        /// </summary>
        public byte[] ParkingData
        {
            get { return _parkingData; }
            set { _parkingData = value; }
        }
        #endregion

        private void FrmManualEnter_Load(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PushCardReadRequest(CardReadHandler);
            //textBox1.ReadOnly = GlobalVariables.IsNETParkAndOffLie;//写卡模式时不能输入卡号
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void CardReadHandler(object sender, CardReadEventArgs e)
        {
            Action action = delegate()
            {
                this.textBox1.Text = e.CardID;
                _parkingData = e[GlobalVariables.ParkingSection];
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmManualEnter_FormClosing(object sender, FormClosingEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(CardReadHandler);
        }
    }
}
