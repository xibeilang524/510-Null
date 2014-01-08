using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.Hardware;


namespace Ralid.Park.UI
{
    public partial class FrmParkDeviceIP : Form
    {
        public FrmParkDeviceIP()
        {
            InitializeComponent();
        }

        public ParkDevice UpdatingDevice { get; set; }

        #region 私有方法
        private void ShowLANInfo(LANInfo info)
        {
            this.Text = info.IPAddress;
            txtIPAddress.IP = info.IPAddress;
            txtIPMask.IP = info.IPMask;
            txtGateWay.IP = info.GateWay;
        }

        private LANInfo GetLANInfoFromInput()
        {
            LANInfo info = new LANInfo();
            info.IPAddress = txtIPAddress.IP;
            info.IPMask = txtIPMask.IP;
            info.GateWay = txtGateWay.IP;
            info.ControlPort = 4001;
            info.EventPort = 5001;
            return info;
        }
        #endregion

        #region 事件处理函数
        private void FrmEntranceDetail_Load(object sender, EventArgs e)
        {
            if (this.UpdatingDevice != null)
            {
                LANInfo li;
                if (UpdatingDevice.GetLANInfo(out li))
                {
                    ShowLANInfo(li);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            LANInfo li;
            if (UpdatingDevice.GetLANInfo(out li))
            {
                ShowLANInfo(li);
                MessageBox.Show(Resources.Resource1.FrmParkDeviceDetail_GetLanSuccess);
            }
            else
            {
                MessageBox.Show(Resources.Resource1.FrmParkDeviceDetail_GetLanFail);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                LANInfo li = GetLANInfoFromInput();
                if (UpdatingDevice.SetLANInfo(li))
                {
                    MessageBox.Show(Resources .Resource1 .FrmParkDeviceDetail_SetLanSuccess );
                }
                else
                {
                    MessageBox.Show(Resources .Resource1 .FrmParkDeviceDetail_SetLanFail );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
