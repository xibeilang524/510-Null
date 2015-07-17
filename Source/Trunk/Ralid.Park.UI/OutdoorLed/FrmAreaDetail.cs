using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI.OutdoorLed
{
    public partial class FrmAreaDetail : Form
    {
        public FrmAreaDetail()
        {
            InitializeComponent();
            this.comCardType.Init();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置区域要显示哪种卡片类型的车位数
        /// </summary>
        public byte? CardType
        {
            get
            {
                if (comCardType.SelectedCardType == null)
                {
                    return null;
                }
                else
                {
                    return (byte)comCardType.SelectedCardType;
                }
            }
            set
            {
                if (value != null)
                {
                    comCardType.SelectedCardType = (CardType)value;
                }
            }
        }
        /// <summary>
        /// 获取或设置区域的车位余数
        /// </summary>
        public int Vacant
        {
            get { return txtVacant.IntergerValue; }
            set { txtVacant.IntergerValue = value; }
        }
        /// <summary>
        /// 获取或设置区域的车位总数
        /// </summary>
        public int CarPort
        {
            get { return txtCarPort.IntergerValue; }
            set { txtCarPort.IntergerValue = value; }
        }
        /// <summary>
        /// 获取或设置余位显示颜色(1红 2绿 3黄)
        /// </summary>
        public int VacantColor
        {
            get { return comVacantColor.SelectedIndex + 1; }
            set { comVacantColor.SelectedIndex = value - 1; }
        }
        /// <summary>
        /// 获取或设置满位显示颜色(1红 2绿 3黄)
        /// </summary>
        public int FullColor
        {
            get { return comFullColor.SelectedIndex + 1; }
            set { this.comFullColor.SelectedIndex = value - 1; }
        }
        #endregion

        #region 事件处理程序
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comCardType.SelectedIndex < 0)
            {
                MessageBox.Show(Resource1.FrmAreaDetail_EmptyCardType);
                comCardType.Focus();
                return;
            }
            if (comVacantColor.SelectedIndex < 0)
            {
                MessageBox.Show(Resource1.FrmAreaDetail_EmptyVacantColor);
                comVacantColor.Focus();
                return;
            }
            if (txtCarPort.IntergerValue <= 0)
            {
                MessageBox.Show(Resource1.FrmAreaDetail_EmptyCarport);
                txtCarPort.Focus();
                return;
            }
            if (txtCarPort.IntergerValue < txtVacant.IntergerValue)
            {
                MessageBox.Show(Resource1.FrmAreaDetail_InvalidCarport);
                txtCarPort.Focus();
                return;
            }
            if (comFullColor.SelectedIndex < 0)
            {
                MessageBox.Show(Resource1.FrmAreaDetail_EmptyFullColor);
                comFullColor.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
