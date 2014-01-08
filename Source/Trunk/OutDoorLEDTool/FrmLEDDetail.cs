using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OutDoorLEDTool
{
    public partial class FrmLEDDetail : Form
    {
        public FrmLEDDetail()
        {
            InitializeComponent();
            this.comCommport.Init();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置串口号
        /// </summary>
        public byte Commport
        {
            get
            {
                return comCommport.ComPort;
            }
            set
            {
                comCommport.ComPort = value;
            }
        }
        /// <summary>
        /// 获取或设置波特率
        /// </summary>
        public int BaudRate
        {
            get
            {
                if (comBaud.SelectedIndex == 0) return 9600;
                return 57600;
            }
            set
            {
                if (value == 9600)
                {
                    this.comBaud.SelectedIndex = 0;
                }
                else
                {
                    this.comBaud.SelectedIndex = 1;
                }
            }
        }
        /// <summary>
        /// 获取或设置小车屏地址
        /// </summary>
        public int CarAddress
        {
            get { return txtCarAddress.IntergerValue; }
            set
            {
                this.txtCarAddress.IntergerValue = value;
            }
        }
        /// <summary>
        /// 获取或设置电单车地址
        /// </summary>
        public int MotorAddress
        {
            get { return txtMotorAddress.IntergerValue; }
            set
            {
                this.txtMotorAddress.IntergerValue = value;
            }
        }
        /// <summary>
        /// 获取或设置屏亮度
        /// </summary>
        public byte Brightness
        {
            get { return (byte)txtBrightness.IntergerValue; }
            set
            {
                this.txtBrightness.IntergerValue = value;
            }
        }
        #endregion

        #region 事件处理程序
        private void FrmLEDDetail_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comCommport.ComPort <= 0)
            {
                MessageBox.Show(Resource1.FrmLEDDetail_EmptyCommport);
                comCommport.Focus();
                return;
            }
            if (comBaud.SelectedIndex < 0)
            {
                MessageBox.Show(Resource1.FrmLEDDetail_EmptyBaudRate);
                comBaud.Focus();
                return;
            }
            if (txtCarAddress.IntergerValue <= 0)
            {
                MessageBox.Show(Resource1.FrmLEDDetail_EmptyCarLedAddress);
                txtCarAddress.Focus();
                return;
            }
            if (txtMotorAddress.IntergerValue <= 0)
            {
                MessageBox.Show(Resource1.FrmLEDDetail_EmptyMotorLedAddress);
                txtMotorAddress.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
