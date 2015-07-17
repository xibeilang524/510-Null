using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park .Hardware ;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmNetEntranceDetail : FrmDetailBase
    {
        public FrmNetEntranceDetail()
        {
            InitializeComponent();
        }

        #region 私有变量
        private EntranceBll bll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);
        #endregion

        #region 私有方法
        private void ShowGeneralInfo(EntranceInfo entrance)
        {
            this.txtEntranceName.Text = entrance.EntranceName;
            txtIP.IP = entrance.IPAddress;
            txtIPMask.IP = entrance.IPMask;
            txtGateWay.IP = entrance.Gateway;
            this.txtCarPlateIP.IP = entrance.CarPlateIP;
            this.txtVideoID.IntergerValue = entrance.VideoID == null ? 0 : entrance.VideoID.Value;
            if (entrance.CarPlateNotifyIPs != null)
            {
                if (entrance.CarPlateNotifyIPs.Count > 0) this.txtCarPlateNotifyController.IP = entrance.CarPlateNotifyIPs[0];
                if (entrance.CarPlateNotifyIPs.Count > 1) this.txtCarPlateNotifyControllerSecond1.IP = entrance.CarPlateNotifyIPs[1];
                if (entrance.CarPlateNotifyIPs.Count > 2) this.txtCarPlateNotifyControllerSecond2.IP = entrance.CarPlateNotifyIPs[2];
            }

            this.txtPaymentEventIndex.IntergerValue = entrance.PaymentEventIndex;
        }

        private void GetGeneralEntranceInfoFromInput(EntranceInfo info)
        {
            info.EntranceName = this.txtEntranceName.Text;
            info.IPAddress = txtIP.IP;
            info.IPMask = this.txtIPMask.IP;
            info.Gateway = this.txtGateWay.IP;
            info.ControlPort = 4001;
            info.EventPort = 5001;
            info.CarPlateIP = this.txtCarPlateIP.IP;
            info.VideoID = this.txtVideoID.IntergerValue;
            List<string> ips = new List<string>();
            ips.Add(this.txtCarPlateNotifyController.IP);
            ips.Add(this.txtCarPlateNotifyControllerSecond1.IP);
            ips.Add(this.txtCarPlateNotifyControllerSecond2.IP);
            info.CarPlateNotifyIPs = ips;

            info.PaymentEventIndex = this.txtPaymentEventIndex.IntergerValue;
        }

        private void ShowWorkmodeInfo(EntranceInfo entrance)
        {
            this.rdHost.Checked = entrance.IsMaster;
            this.rdNotHost.Checked = !entrance.IsMaster;
            this.rdExit.Checked = entrance.IsExitDevice;
            this.rdEnter.Checked = !entrance.IsExitDevice;
            this.chkTakeCardNeedCarSense.Checked = entrance.ReadAndTakeCardNeedCarSense;
            this.chkAllowEjectCardWhithoutRead.Checked = entrance.AllowEjectCardWhithoutRead;
            this.chkLightEnable.Checked = entrance.LightEnable;
            this.chkExportCharge.Checked = entrance.ExportCharge;
            this.chkOnlineHandleWhenNotOnList.Checked = entrance.OnlineHandleWhenNotOnList;
            this.chkForbidWhenCardExpired.Checked = entrance.ForbidWhenCardExpired;
            this.chkForbidWhenFull.Checked = entrance.ForbidWhenFull;
            this.chkAllowTempCard.Checked = !entrance.DisableTempCard;
            this.chkWeigand34.Checked = entrance.Wiegand34;
            this.chkNoParkingCount.Checked = entrance.NoParkingCount;
            this.chkValid.Checked = entrance.Valid;
            this.txtCardReadInterval.IntergerValue = entrance.ReadCardInterval;
            this.cmbTicketPrinter.ComPort = entrance.TicketPrinterCOMPort;
            this.cmbTicketReader.ComPort = entrance.TicketReaderCOMPort;
            if (entrance.TicketReaderCOMPort2 != null) this.cmbTicketReader2.ComPort = entrance.TicketReaderCOMPort2.Value;
            this.chkEnableParkvacantLed.Checked = entrance.EnableParkvacantLed;
            this.chkNoReaderOnCardCaptuer.Checked = entrance.NoReaderOnCardCaptuer;
            this.chkMonthCardWaitWhenOut.Checked = entrance.MonthCardWaitWhenOut;
            this.chkPrepayCardWaitWhenOut.Checked = entrance.PrepayCardWaitWhenOut;
            this.chkOnlyTempReaderAfterButtonClick.Checked = entrance.OnlyTempReaderAfterButtonClick;
            this.chkUseAsACS.Checked = entrance.UseAsAcs;
        }

        private void GetWorkmodeFromInput(EntranceInfo entrance)
        {
            entrance.WorkMode = EntranceWorkmodeOption.OPT_None;
            entrance.IsMaster = this.rdHost.Checked;
            entrance.IsExitDevice = this.rdExit.Checked;
            entrance.ReadAndTakeCardNeedCarSense = this.chkTakeCardNeedCarSense.Checked;
            entrance.AllowEjectCardWhithoutRead = this.chkAllowEjectCardWhithoutRead.Checked;
            entrance.LightEnable = this.chkLightEnable.Checked;
            entrance.ForbidWhenCardExpired = this.chkForbidWhenCardExpired.Checked;
            entrance.ExportCharge = this.chkExportCharge.Checked;
            entrance.OnlineHandleWhenNotOnList = this.chkOnlineHandleWhenNotOnList.Checked;
            entrance.ForbidWhenFull = this.chkForbidWhenFull.Checked;
            entrance.DisableTempCard = !this.chkAllowTempCard.Checked;
            entrance.Wiegand34 = this.chkWeigand34.Checked;
            entrance.NoParkingCount = this.chkNoParkingCount.Checked;
            entrance.Valid = this.chkValid.Checked;
            entrance.ReadCardInterval = this.txtCardReadInterval.IntergerValue;
            entrance.TicketPrinterCOMPort = this.cmbTicketPrinter.ComPort;
            entrance.TicketReaderCOMPort = this.cmbTicketReader.ComPort;
            entrance.TicketReaderCOMPort2 = this.cmbTicketReader2.ComPort;
            entrance.EnableParkvacantLed = this.chkEnableParkvacantLed.Checked;
            entrance.NoReaderOnCardCaptuer = this.chkNoReaderOnCardCaptuer.Checked;
            entrance.MonthCardWaitWhenOut = this.chkMonthCardWaitWhenOut.Checked;
            entrance.PrepayCardWaitWhenOut = this.chkPrepayCardWaitWhenOut.Checked;
            entrance.OnlyTempReaderAfterButtonClick = this.chkOnlyTempReaderAfterButtonClick.Checked;
            entrance.CardValidNeedResponse = true; //卡片有效指令需要控制器返回确认一直为真
            entrance.UseAsAcs = this.chkUseAsACS.Checked;
        }

        #endregion

        #region 公共属性
        public ParkInfo Park { get; set; }
        public ParkDevice ParkDevice { get; set; }
        #endregion

        #region 重写基类方法
        protected override void InitControls()
        {
            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
                this.btnGetHardwareInfo.Visible = false;
                this.txtIPMask.IP = "255.255.255.0";
                rdEnter_CheckedChanged(rdEnter, EventArgs.Empty);
            }
            this.cmbTicketPrinter.Init();
            this.cmbTicketReader.Init();
            this.cmbTicketReader2.Init();
            this.UCCardTypeProperty.Init();
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditEntrance);
        }

        protected override void ItemShowing()
        {
            EntranceInfo entrance = (EntranceInfo)UpdatingItem;
            ShowGeneralInfo(entrance);
            ShowWorkmodeInfo(entrance);
            rdEnter_CheckedChanged(rdEnter, EventArgs.Empty);
            this.Text = entrance.EntranceName;
            this.UCCardTypeProperty.CardTypeProperty = entrance.CardTypeProperty;
            this.UCCardTypeProperty.IsExit = entrance.IsExitDevice;
        }

        protected override bool CheckInput()
        {
            short s = 0;
            if (string.IsNullOrEmpty(this.txtEntranceName.Text.Trim()))
            {
                MessageBox.Show(Resources.Resource1.FrmNetEntrance_EmptyName);
                return false;
            }

            if (txtIP.IP == "0.0.0.0")
            {
                MessageBox.Show(Resources.Resource1.FrmNetEntrance_InvalidIP);
                txtIP.Focus();
                return false;
            }
            if (this.txtCardReadInterval.IntergerValue < 0 || this.txtCardReadInterval.IntergerValue > 255)
            {
                MessageBox.Show(Resources.Resource1.FrmNetEntrance_InvalidReadInterval);
                return false;
            }
            return true;
        }

        protected override object GetItemFromInput()
        {
            EntranceInfo info = null;
            if (IsAdding)
            {
                info = new EntranceInfo();
            }
            else
            {
                info = UpdatingItem as EntranceInfo;
            }
            GetGeneralEntranceInfoFromInput(info);
            GetWorkmodeFromInput(info);
            if (Park != null)
            {
                info.ParkID = Park.ParkID;
                info.RootParkID = Park.RootParkID;
            }
            info.CardTypeProperty = this.UCCardTypeProperty.CardTypeProperty;
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            EntranceInfo entrance = addingItem as EntranceInfo;
            if (entrance.IsMaster && Park.MastEntrance != null)
            {
                return new CommandResult(ResultCode.Fail, Resources.Resource1.FrmNetEntrance_MastExists);
            }
            return bll.Insert((EntranceInfo)addingItem);
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            EntranceInfo entrance = updatingItem as EntranceInfo;
            ParkInfo park = ParkBuffer.Current.GetPark(entrance.ParkID);
            if (entrance.IsMaster && park.MastEntrance != null && park.MastEntrance.EntranceID != entrance.EntranceID)
            {
                return new CommandResult(ResultCode.Fail, Resources.Resource1.FrmNetEntrance_MastExists);
            }
            return bll.Update((EntranceInfo)updatingItem);
        }
        #endregion

        #region 事件处理程序
        private void FrmEntranceDetail_Load(object sender, EventArgs e)
        {
            if (ParkDevice != null)
            {
                ShowDeviceInfo(ParkDevice);
            }
        }

        private void rdEnter_CheckedChanged(object sender, EventArgs e)
        {
            cmbTicketReader.Enabled = (rdExit.Checked);
            cmbTicketReader2.Enabled = (rdExit.Checked);
            cmbTicketPrinter.Enabled = (rdEnter.Checked);
            chkAllowEjectCardWhithoutRead.Enabled = (rdEnter.Checked);
            chkForbidWhenFull.Enabled = (rdEnter.Checked);
            chkMonthCardWaitWhenOut.Enabled = (rdExit.Checked);
            chkPrepayCardWaitWhenOut.Enabled = (rdExit.Checked);
            chkNoReaderOnCardCaptuer.Enabled = (rdExit.Checked);
            chkEnableParkvacantLed.Enabled = (rdEnter.Checked);
            chkOnlyTempReaderAfterButtonClick.Enabled = (rdEnter.Checked);

            if (!cmbTicketReader.Enabled) cmbTicketReader.ComPort = 0;
            if (!cmbTicketPrinter.Enabled) cmbTicketPrinter.ComPort = 0;
            if (!cmbTicketReader2.Enabled) cmbTicketReader2.ComPort = 0;
            if (!chkAllowEjectCardWhithoutRead.Enabled) chkAllowEjectCardWhithoutRead.Checked = false;
            if (!chkForbidWhenFull.Enabled) chkForbidWhenFull.Checked = false;
            if (!chkMonthCardWaitWhenOut.Enabled) chkMonthCardWaitWhenOut.Checked = false;
            if (!chkPrepayCardWaitWhenOut.Enabled) chkPrepayCardWaitWhenOut.Checked = false;
            if (!chkNoReaderOnCardCaptuer.Enabled) chkNoReaderOnCardCaptuer.Checked = false;
            if (!chkEnableParkvacantLed.Enabled) chkEnableParkvacantLed.Checked = false;
            if (!chkOnlyTempReaderAfterButtonClick.Enabled) chkOnlyTempReaderAfterButtonClick.Checked = false;
        }

        private void btnGetHardwareInfo_Click(object sender, EventArgs e)
        {
            if (UpdatingItem != null)
            {
                EntranceInfo entrance = UpdatingItem as EntranceInfo;
                FrmParkDeviceDetail frm = new FrmParkDeviceDetail();
                LANInfo lan = new LANInfo()
                {
                    IPAddress = entrance.IPAddress,
                    IPMask = entrance.IPMask,
                    GateWay = entrance.Gateway,
                    ControlPort = entrance.ControlPort,
                };
                ParkDevice pd = new Hardware.ParkDevice(lan);
                frm.TopMost = true;
                frm.UpdatingEntrance = pd;
                frm.Show();
            }
        }
        #endregion

        #region 与增加搜索到的控制器相关
        private void ShowDeviceInfo(ParkDevice device)
        {
            LANInfo li;
            if (device.GetLANInfo(out li))
            {
                ShowLANInfo(li);
            }
            else
            {
                ShowLANInfo(device.LANInfo);
            }

            WorkmodeInfo ws;
            if (device.GetWorkmode(out ws)) ShowWorkmodeInfo(ws);
            List<string> carPlateNotifyIp = device.GetCarPlateNotifyControllerIP();
            if (carPlateNotifyIp != null && carPlateNotifyIp.Count == 3)
            {
                this.txtCarPlateNotifyController.IP = carPlateNotifyIp[0];
                this.txtCarPlateNotifyControllerSecond1.IP = carPlateNotifyIp[1];
                this.txtCarPlateNotifyControllerSecond2.IP = carPlateNotifyIp[2];
            }
        }

        private void ShowLANInfo(LANInfo info)
        {
            this.Text = info.IPAddress;
            txtIP.IP = info.IPAddress;
            txtIP.Enabled = false;
            txtIPMask.IP = info.IPMask;
            txtIPMask.Enabled = false;
            txtGateWay.IP = info.GateWay;
            txtGateWay.Enabled = false;
        }

        private void ShowWorkmodeInfo(WorkmodeInfo bs)
        {
            this.rdHost.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NoneMaster) == 0;
            this.rdNotHost.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NoneMaster) == WorkmodeOptions.NoneMaster;
            this.rdEnter.Checked = (bs.WorkmodeOptions & WorkmodeOptions.IsEnterDevice) == WorkmodeOptions.IsEnterDevice;
            this.rdExit.Checked = (bs.WorkmodeOptions & WorkmodeOptions.IsEnterDevice) == 0;
            this.chkTakeCardNeedCarSense.Checked = (bs.WorkmodeOptions & WorkmodeOptions.TakeCardNeedCarSense) == WorkmodeOptions.TakeCardNeedCarSense;
            this.chkAllowEjectCardWhithoutRead.Checked = (bs.WorkmodeOptions & WorkmodeOptions.AllowEjectCardWhithoutRead) == WorkmodeOptions.AllowEjectCardWhithoutRead;
            this.chkLightEnable.Checked = (bs.WorkmodeOptions & WorkmodeOptions.LightEnable) == WorkmodeOptions.LightEnable;
            this.chkForbidWhenCardExpired.Checked = (bs.WorkmodeOptions & WorkmodeOptions.ForbidExitWhenCardExpired) == WorkmodeOptions.ForbidExitWhenCardExpired;
            this.chkExportCharge.Checked = (bs.WorkmodeOptions & WorkmodeOptions.ExportCharge) == WorkmodeOptions.ExportCharge;
            this.chkOnlineHandleWhenNotOnList.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NotOnlineHandleWhenNotOnList) == 0;
            this.chkForbidWhenFull.Checked = (bs.WorkmodeOptions & WorkmodeOptions.ForbidEnterWhenFull) == WorkmodeOptions.ForbidEnterWhenFull;
            this.chkAllowTempCard.Checked = (bs.WorkmodeOptions & WorkmodeOptions.EnableTempCard) == WorkmodeOptions.EnableTempCard;
            this.chkNoParkingCount.Checked = (bs.WorkmodeOptions & WorkmodeOptions.NoParkingCount) == WorkmodeOptions.NoParkingCount;
            this.chkValid.Checked = (bs.WorkmodeOptions & WorkmodeOptions.Valid) == WorkmodeOptions.Valid;
            this.txtCardReadInterval.Text = bs.CardReadInterval.ToString();
        }
        #endregion
    }
}
