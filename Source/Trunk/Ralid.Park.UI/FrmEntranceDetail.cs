using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.UI
{
    public partial class FrmEntranceDetail : FrmDetailBase
    {
        private EntranceBll bll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);

        public FrmEntranceDetail()
        {
            InitializeComponent();
        }

        public ParkInfo Park { get; set; }

        #region 重写基类方法
        protected override void InitControls()
        {
            this.comAddress.Init();
            this.cmbTicketPrinter.Init();
            this.cmbTicketReader.Init();
            this.cmbTicketReader2.Init();

            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
            }
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditEntrance);
        }

        protected override void ItemShowing()
        {
            EntranceInfo info = (EntranceInfo)UpdatingItem;
            this.Text = info.EntranceName;
            this.txtEntranceName.Text = info.EntranceName;
            this.comAddress.EntranceAddress = info.Address;
            this.cmbTicketPrinter.ComPort = info.TicketPrinterCOMPort;
            this.cmbTicketReader.ComPort = info.TicketReaderCOMPort;
            if (info.TicketReaderCOMPort2 != null) this.cmbTicketReader2.ComPort = info.TicketReaderCOMPort2.Value;
            this.chkReadCardNeedCarSense.Checked = info.ReadAndTakeCardNeedCarSense;
            this.chkLightOnWhenCarArrive.Checked = info.LightEnable;
            this.txtCardReadInterval.IntergerValue = info.ReadCardInterval;
            this.chkEnableParkvacantLed.Checked = info.EnableParkvacantLed;
            this.chkNoReaderOnCardCaptuer.Checked = info.NoReaderOnCardCaptuer;
            this.chkMonthCardWaitWhenOut.Checked = info.MonthCardWaitWhenOut;
            this.chkPrepayCardWaitWhenOut.Checked = info.PrepayCardWaitWhenOut;
            this.chkCardValidNeedResponse.Checked = info.CardValidNeedResponse;
            this.chkOnlyTempReaderAfterButtonClick.Checked = info.OnlyTempReaderAfterButtonClick;
            this.chkNoParkingCount.Checked = info.NoParkingCount;
            this.chkUseAsACS.Checked = info.UseAsAcs;
            this.chkForbidWhenCardExpired.Checked = info.ForbidWhenCardExpired;
            this.chkForbidWhenFull.Checked = info.ForbidWhenFull;
            this.txtCarPlateIP.IP = info.CarPlateIP;
            this.txtVideoID.IntergerValue = info.VideoID == null ? 0 : info.VideoID.Value;
        }

        protected override bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtEntranceName.Text.Trim()))
            {
                MessageBox.Show(Resources.Resource1.FrmEntranceDetail_InvalidName);
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
                info.WorkMode = EntranceWorkmodeOption.OPT_None;
            }
            info.EntranceName = this.txtEntranceName.Text;
            info.Address = this.comAddress.EntranceAddress;
            info.TicketPrinterCOMPort = this.cmbTicketPrinter.ComPort;
            info.TicketReaderCOMPort = this.cmbTicketReader.ComPort;
            info.TicketReaderCOMPort2 = this.cmbTicketReader2.ComPort;
            info.ReadAndTakeCardNeedCarSense = this.chkReadCardNeedCarSense.Checked;
            info.LightEnable = this.chkLightOnWhenCarArrive.Checked;
            info.ReadCardInterval = this.txtCardReadInterval.IntergerValue;
            info.EnableParkvacantLed = this.chkEnableParkvacantLed.Checked;
            info.NoReaderOnCardCaptuer = this.chkNoReaderOnCardCaptuer.Checked;
            info.MonthCardWaitWhenOut = this.chkMonthCardWaitWhenOut.Checked;
            info.PrepayCardWaitWhenOut = this.chkPrepayCardWaitWhenOut.Checked;
            info.CardValidNeedResponse = this.chkCardValidNeedResponse.Checked;
            info.OnlyTempReaderAfterButtonClick = this.chkOnlyTempReaderAfterButtonClick.Checked;
            info.NoParkingCount = this.chkNoParkingCount.Checked;
            info.UseAsAcs = this.chkUseAsACS.Checked;
            info.ForbidWhenCardExpired = this.chkForbidWhenCardExpired.Checked;
            info.ForbidWhenFull = this.chkForbidWhenFull.Checked;
            info.CarPlateIP = txtCarPlateIP.IP;
            info.VideoID = txtVideoID.IntergerValue;
            if (Park != null)
            {
                info.ParkID = Park.ParkID;
                info.RootParkID = Park.RootParkID;
            }
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            return bll.Insert((EntranceInfo)addingItem);
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            return bll.Update((EntranceInfo)updatingItem);
        }
        #endregion

        #region 事件处理程序
        private void comAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTicketReader.Enabled = (comAddress.EntranceAddress % 2 == 1);
            cmbTicketReader2.Enabled = (comAddress.EntranceAddress % 2 == 1);
            cmbTicketPrinter.Enabled = (comAddress.EntranceAddress % 2 == 0);
            chkMonthCardWaitWhenOut.Enabled = (comAddress.EntranceAddress % 2 == 1);
            chkPrepayCardWaitWhenOut.Enabled = (comAddress.EntranceAddress % 2 == 1);
            chkNoReaderOnCardCaptuer.Enabled = (comAddress.EntranceAddress % 2 == 1);
            chkEnableParkvacantLed.Enabled = (comAddress.EntranceAddress % 2 == 0);
            chkOnlyTempReaderAfterButtonClick.Enabled = (comAddress.EntranceAddress % 2 == 0);

            if (!cmbTicketReader.Enabled) cmbTicketReader.ComPort = 0;
            if (!cmbTicketPrinter.Enabled) cmbTicketPrinter.ComPort = 0;
            if (!chkMonthCardWaitWhenOut.Enabled) chkMonthCardWaitWhenOut.Checked = false;
            if (!chkPrepayCardWaitWhenOut.Enabled) chkPrepayCardWaitWhenOut.Checked = false;
            if (!chkNoReaderOnCardCaptuer.Enabled) chkNoReaderOnCardCaptuer.Checked = false;
            if (!chkEnableParkvacantLed.Enabled) chkEnableParkvacantLed.Checked = false;
            if (!chkOnlyTempReaderAfterButtonClick.Enabled) chkOnlyTempReaderAfterButtonClick.Checked = false;
        }
        #endregion
    }
}
