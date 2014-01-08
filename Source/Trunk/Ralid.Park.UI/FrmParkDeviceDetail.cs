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
    public partial class FrmParkDeviceDetail : Form
    {
        public FrmParkDeviceDetail()
        {
            InitializeComponent();
        }

        public ParkDevice UpdatingEntrance { get; set; }

        #region 私有方法
        private void ShowDeviceInfo(DeviceInfo info)
        {
            this.txtSerialNum.Text = info.StrSerialNum;
            this.address.Text = info.Address.ToString();
            this.parknum.Text = info.StrParkNum;
            this.hardwareV.Text = info.HardwareVersion;
            this.softwareV.Text = info.SoftwareVersion;
            this.commV.Text = info.CommunicationVersion;
        }

        private void ShowLANInfo(LANInfo info)
        {
            this.Text = info.IPAddress;
            txtIPAddress.IP = info.IPAddress;
            txtIPMask.IP = info.IPMask;
            txtGateWay.IP = info.GateWay;
            txtMasterIP.IP = info.MasterIP;
            txtEventListenerIP.IP = info.EventListenerIP;
            this.txtmac.Text = info.MACAddress;
            this.controlPort.Text = info.ControlPort.ToString();
            this.eventPort.Text = info.EventPort.ToString();
            this.listenerPort.Text = info.EventListenerPort.ToString();
        }

        private void ShowCardTypeProperty(ushort[] property)
        {
            this.ucCardTypeProperty1.CardTypeProperty = property;
        }

        private DeviceInfo GetDeviceInfoFromInput()
        {
            DeviceInfo info = new DeviceInfo();
            info.Address = byte.Parse(this.address.Text);
            info.StrParkNum = this.parknum.Text.Trim();
            return info;
        }

        private LANInfo GetLANInfoFromInput()
        {
            LANInfo info = new LANInfo();
            info.IPAddress = txtIPAddress.IP;
            info.IPMask = txtIPMask.IP;
            info.GateWay = txtGateWay.IP;
            info.MasterIP = txtMasterIP.IP;
            info.EventListenerIP = txtEventListenerIP.IP;
            info.EventListenerPort = int.Parse(listenerPort.Text);
            info.MACAddress = this.txtmac.Text;
            return info;
        }

        private void ShowWorkmodeInfo(WorkmodeInfo bs)
        {
            this.rdOffLine.Checked = (bs.WorkmodeOptions & WorkmodeOptions.IsOffline) == WorkmodeOptions.IsOffline;
            this.rdOnline.Checked = (bs.WorkmodeOptions & WorkmodeOptions.IsOffline) == 0;
            this.rdHost.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NoneMaster) == 0;
            this.rdNotHost.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NoneMaster) == WorkmodeOptions.NoneMaster;
            this.rdEnter.Checked = (bs.WorkmodeOptions & WorkmodeOptions.IsEnterDevice) == WorkmodeOptions.IsEnterDevice;
            this.rdExit.Checked = (bs.WorkmodeOptions & WorkmodeOptions.IsEnterDevice) == 0;
            this.chkTakeCardNeedCarSense.Checked = (bs.WorkmodeOptions & WorkmodeOptions.TakeCardNeedCarSense) == WorkmodeOptions.TakeCardNeedCarSense;
            this.chkAllowEjectCardWhithoutRead.Checked = (bs.WorkmodeOptions & WorkmodeOptions.AllowEjectCardWhithoutRead) == WorkmodeOptions.AllowEjectCardWhithoutRead;
            this.chkLightEnable.Checked = (bs.WorkmodeOptions & WorkmodeOptions.LightEnable) == WorkmodeOptions.LightEnable;
            this.chkForbidWhenCardExpired.Checked = (bs.WorkmodeOptions & WorkmodeOptions.ForbidExitWhenCardExpired) == WorkmodeOptions.ForbidExitWhenCardExpired;
            this.chkForbidWhenFull.Checked = (bs.WorkmodeOptions & WorkmodeOptions.ForbidEnterWhenFull) == WorkmodeOptions.ForbidEnterWhenFull;
            this.chkEnableTempCard.Checked = (bs.WorkmodeOptions & WorkmodeOptions.EnableTempCard) == WorkmodeOptions.EnableTempCard;
            this.chkExportCharge.Checked = (bs.WorkmodeOptions & WorkmodeOptions.ExportCharge) == WorkmodeOptions.ExportCharge;
            this.chkNoParkingCount.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NoParkingCount) == WorkmodeOptions.NoParkingCount;
            this.chkValid.Checked = (bs.WorkmodeOptions & WorkmodeOptions.Valid) == WorkmodeOptions.Valid;
            this.txtCardReadInterval.Text = bs.CardReadInterval.ToString();
            this.chkRoadGateModel.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NoneRoadGateModel) == 0;
        }

        private WorkmodeInfo GetWorkmodeInfoFromInput()
        {
            WorkmodeInfo bs = new WorkmodeInfo();
            if (this.rdOffLine.Checked) bs.WorkmodeOptions |= WorkmodeOptions.IsOffline;
            if (this.rdNotHost.Checked) bs.WorkmodeOptions |= WorkmodeOptions.NoneMaster;
            if (this.rdEnter.Checked) bs.WorkmodeOptions |= WorkmodeOptions.IsEnterDevice;
            if (this.chkTakeCardNeedCarSense.Checked) bs.WorkmodeOptions |= WorkmodeOptions.TakeCardNeedCarSense;
            if (this.chkAllowEjectCardWhithoutRead.Checked) bs.WorkmodeOptions |= WorkmodeOptions.AllowEjectCardWhithoutRead;
            if (this.chkLightEnable.Checked) bs.WorkmodeOptions |= WorkmodeOptions.LightEnable;
            if (this.chkForbidWhenCardExpired.Checked) bs.WorkmodeOptions |= WorkmodeOptions.ForbidExitWhenCardExpired;
            if (this.chkForbidWhenFull.Checked) bs.WorkmodeOptions |= WorkmodeOptions.ForbidEnterWhenFull;
            if (this.chkEnableTempCard.Checked) bs.WorkmodeOptions |= WorkmodeOptions.EnableTempCard;
            if (!this.chkRoadGateModel.Checked) bs.WorkmodeOptions |= WorkmodeOptions.NoneRoadGateModel;
            bs.CardReadInterval = int.Parse(this.txtCardReadInterval.Text);
            return bs;
        }


        private bool CheckInput()
        {
            string parkNum = parknum.Text.Trim ();
            if (parkNum.Length != 6)
            {
                MessageBox.Show("停车场编号不正确,正确格式为6个数字,请重新输入");
                return false;
            }
            return true;
        }
        #endregion

        #region 事件处理函数
        private void FrmEntranceDetail_Load(object sender, EventArgs e)
        {
            this.ucCardTypeProperty1.Init();
            if (this.UpdatingEntrance != null)
            {
                Ralid.Park.Hardware.DeviceInfo deviceInfo;
                if (UpdatingEntrance.GetDeviceInfo(out deviceInfo))
                {
                    ShowDeviceInfo(deviceInfo);
                }
                LANInfo li;
                if (UpdatingEntrance.GetLANInfo(out li))
                {
                    ShowLANInfo(li);
                }
                WorkmodeInfo wi;
                if (UpdatingEntrance.GetWorkmode(out wi))
                {
                    ShowWorkmodeInfo(wi);
                }
                List<string> txtCarPlateNotify = UpdatingEntrance.GetCarPlateNotifyControllerIP();
                if (txtCarPlateNotify != null && txtCarPlateNotify.Count == 3)
                {
                    this.txtCarPlateNotifyController.IP = txtCarPlateNotify[0];
                    txtCarPlateNotifyControllerSecond1.IP = txtCarPlateNotify[1];
                    txtCarPlateNotifyControllerSecond2.IP = txtCarPlateNotify[2];
                }
                else
                {
                    txtCarPlateNotifyController.IP = "0.0.0.0";
                    txtCarPlateNotifyControllerSecond1.IP = "0.0.0.0";
                    txtCarPlateNotifyControllerSecond2.IP = "0.0.0.0";
                }
                ushort[] property = UpdatingEntrance.GetCardTypeProperty();
                if (property != null)
                {
                    ShowCardTypeProperty(property);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            Ralid.Park.Hardware.DeviceInfo di;
            if (UpdatingEntrance.GetDeviceInfo(out di))
            {
                ShowDeviceInfo(di);
                MessageBox.Show("获取硬件参数成功");
            }
            else
            {
                MessageBox.Show("获取硬件参数失败");
            }
            LANInfo li;
            if (UpdatingEntrance.GetLANInfo(out li))
            {
                ShowLANInfo(li);
                MessageBox.Show("获取LAN参数成功");
            }
            else
            {
                MessageBox.Show("获取LAN参数失败");
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    DeviceInfo info = GetDeviceInfoFromInput();
                    if (UpdatingEntrance.SetParkNum(info.DeviceNum))
                    {
                        MessageBox.Show("硬件参数设置成功");
                    }
                    else
                    {
                        MessageBox.Show("硬件参数设置失败");
                    }
                    LANInfo li = GetLANInfoFromInput();
                    if (UpdatingEntrance.SetLANInfo(li))
                    {
                        MessageBox.Show("LAN参数设置成功");
                    }
                    else
                    {
                        MessageBox.Show("LAN参数设置失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWorkmodeGet_Click(object sender, EventArgs e)
        {
            WorkmodeInfo bs = new WorkmodeInfo();
            if (UpdatingEntrance.GetWorkmode(out bs))
            {
                ShowWorkmodeInfo(bs);
                MessageBox.Show("获取工作模式成功");
            }
            else
            {
                MessageBox.Show("获取工作模式失败");
            }
        }

        private void btnWorkmodeSet_Click(object sender, EventArgs e)
        {
            WorkmodeInfo bs = GetWorkmodeInfoFromInput();
            if (UpdatingEntrance.SetWorkmode(bs))
            {
                MessageBox.Show("设置工作模式成功");
            }
            else
            {
                MessageBox.Show("设置工作模式失败");
            }
        }
        #endregion
    }
}
