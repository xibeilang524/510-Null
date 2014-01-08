using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.Hardware;
using Ralid.GeneralLibrary;
using Ralid.Park .BusinessModel .Model ;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmSearchDevice : Form
    {
        public FrmSearchDevice()
        {
            InitializeComponent();
        }

        #region 公共事件
        /// <summary>
        /// 当要增加某个控制器到系统时产生此事件,要增加的硬件做为sender传递
        /// </summary>
        public event EventHandler ItemAdding;
        #endregion

        #region 公共属性
        #endregion

        #region 事件处理方法
        private void FrmSearchDevice_Load(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            grid.Rows.Clear();
            List<ParkDevice> devices = DeviceSearcher.EnumAllParkDevices();
            if (devices != null && devices.Count > 0)
            {
                foreach (ParkDevice device in devices)
                {
                    int row = grid.Rows.Add();
                    grid.Rows[row].Tag = device;
                    Ralid.Park.Hardware.DeviceInfo deviceInfo;
                    if (device.GetDeviceInfo(out deviceInfo))
                    {
                        grid.Rows[row].Cells["colSerialNum"].Value = deviceInfo.StrSerialNum;
                    }
                    grid.Rows[row].Cells["colIP"].Value = device.LANInfo.IPAddress;
                    grid.Rows[row].Cells["colMAC"].Value = device.LANInfo.MACAddress;
                }
            }
            this.Cursor = Cursors.Arrow;
        }

        private void mnu_AddTo_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 1)
            {
                ParkDevice device = grid.SelectedRows[0].Tag as ParkDevice;
                if (device != null)
                {
                    List<EntranceInfo> items = (new EntranceBll(AppSettings.CurrentSetting.ParkConnect)).GetAllEntraces().QueryObjects;
                    if (items.Exists(item => item.IPAddress == device.LANInfo.IPAddress))
                    {
                        MessageBox.Show(string.Format(Resources.Resource1.FrmSearchDevice_DeviceExists, device.LANInfo.IPAddress));
                        return;
                    }
                    else
                    {
                        if (this.ItemAdding != null)
                        {
                            ItemAdding(device, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        private void grid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    row.Selected = false;
                }
                grid.Rows[e.RowIndex].Selected = true;
            }
        }

        private void mnu_Update_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 1)
            {
                ParkDevice device = grid.SelectedRows[0].Tag as ParkDevice;
                if (device != null)
                {
                    List<EntranceInfo> items = (new EntranceBll(AppSettings.CurrentSetting.ParkConnect)).GetAllEntraces().QueryObjects;
                    if (items.Exists(item => item.IPAddress == device.LANInfo.IPAddress))
                    {
                        MessageBox.Show(string.Format(Resources.Resource1.FrmSearchDevice_DeviceExists, device.LANInfo.IPAddress));
                        return;
                    }
                    FrmParkDeviceIP frm = new FrmParkDeviceIP();
                    frm.UpdatingDevice = device;
                    frm.ShowDialog();
                    grid.SelectedRows[0].Cells["colIP"].Value = frm.UpdatingDevice.LANInfo.IPAddress;
                }
            }
        }
        #endregion
    }
}
