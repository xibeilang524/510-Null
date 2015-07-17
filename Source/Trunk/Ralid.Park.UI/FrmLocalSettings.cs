using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.UI
{
    public partial class FrmLocalSettings: Form
    {
        public FrmLocalSettings()
        {
            InitializeComponent();
        }

        #region 事件处理
        private void FrmSysPara_Load(object sender, EventArgs e)
        {
            this.butOK.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditLocalSetting);
            ShowAppSetting();
            this.chkNewCardValidCommand.CheckedChanged += chkNewCardValidCommand_CheckedChanged;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                SaveAppSetting();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckInput()
        {
            return true;
        }

        private void chkNewCardValidCommand_CheckedChanged(object sender, EventArgs e)
        {
            this.chkNewCardValidCommand.CheckedChanged -= chkNewCardValidCommand_CheckedChanged;
            if (this.chkNewCardValidCommand.Checked)
            {
                if (MessageBox.Show(Resources.Resource1.FrmLocalSettings_NewCardValidCommand, Resources.Resource1.FrmLocalSettings_EnabledComfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    this.chkNewCardValidCommand.Checked = false;
                }
            }
            this.chkNewCardValidCommand.CheckedChanged += chkNewCardValidCommand_CheckedChanged;
        }
        #endregion

        #region 本地配置
        private void ShowAppSetting()
        {
            this.comTicketReader.Init();
            this.comFeeLed.Init();
            this.comBillPrinter.Init();
            this.comYCT.Init();
            this.comParkFullLed.Init();
            this.comLedType.Items.Clear();
            this.comLedType.Items.Add(Resources.Resource1.LEDType_Zhongkuang);
            this.comLedType.Items.Add(Resources.Resource1.LEDType_YanSe);
            this.comLedType.Items.Add(Resources.Resource1.LEDType_HSD);
            this.comParkingCommunicationIP.Items.Clear();
            this.comParkingCommunicationIP.Items.Add(string.Empty);
            this.comParkingCommunicationIP.Items.AddRange(GeneralLibrary.NetTool.GetLocalIPS());
            this.comSamNo.Items.Clear();
            for (int i = 1; i < 9; i++)
            {
                this.comSamNo.Items.Add("SAM" + i.ToString());
            }


            this.comFeeLed.ComPort = AppSettings.CurrentSetting.ParkFeeLedCOMPort;
            this.comLedType.SelectedIndex = AppSettings.CurrentSetting.ParkFeeLedType;
            this.comTicketReader.ComPort = AppSettings.CurrentSetting.TicketReaderCOMPort;
            this.comBillPrinter.ComPort = AppSettings.CurrentSetting.BillPrinterCOMPort;
            this.comYCT.ComPort = AppSettings.CurrentSetting.YCTReaderCOMPort;
            this.comParkFullLed.ComPort = AppSettings.CurrentSetting.ParkFullLedCOMPort;
            this.chkOpenLastOpenedVideo.Checked = AppSettings.CurrentSetting.OpenLastOpenedVideo;
            this.chkShowOnlyListenedPark.Checked = AppSettings.CurrentSetting.ShowOnlyListenedPark;
            this.chkDebug.Checked = AppSettings.CurrentSetting.Debug;
            this.chkOptimized.Checked = AppSettings.CurrentSetting.Optimized;
            this.chkNeedPasswordWhenExit.Checked = AppSettings.CurrentSetting.NeedPasswordWhenExit;
            this.chkEnableTTS.Checked = AppSettings.CurrentSetting.EnableTTS;
            this.chkEnlargeMemo.Checked = AppSettings.CurrentSetting.EnlargeMemo;
            this.chkChargeAfterMemo.Checked = AppSettings.CurrentSetting.ChargeAfterMemo;
            this.chkShowAPMMonitor.Checked = AppSettings.CurrentSetting.ShowAPMMonitor;
            this.chkEnableZST.Checked = AppSettings.CurrentSetting.EnableZST;
            this.txtZSTReaderIP.IP = AppSettings.CurrentSetting.ZSTReaderIP;
            this.chkEnableWriteCard.Checked = AppSettings.CurrentSetting.EnableWriteCard;
            this.chkAuotAddToFirewallException.Checked = AppSettings.CurrentSetting.AuotAddToFirewallException;
            this.comParkingCommunicationIP.Text = AppSettings.CurrentSetting.ParkingCommunicationIP;
            this.chkCheckConnectionWithPing.Checked = AppSettings.CurrentSetting.CheckConnectionWithPing;
            this.chkSwitchEntrance.Checked = AppSettings.CurrentSetting.SwitchEntrance;
            this.chkEnableHotel.Checked = AppSettings.CurrentSetting.EnableHotel;
            this.chkNewCardValidCommand.Checked = AppSettings.CurrentSetting.NewCardValidCommand;
            this.comSamNo.SelectedIndex = AppSettings.CurrentSetting.ParkingSamNO - 1;
            this.chkShowPosButton.Checked = AppSettings.CurrentSetting.EnablePOSButton;
            this.chkSpeakPromptWhenCarArrival.Checked = AppSettings.CurrentSetting.SpeakPromptWhenCarArrival;
        }

        private void SaveAppSetting()
        {
            AppSettings.CurrentSetting.ParkFeeLedCOMPort = this.comFeeLed.ComPort;
            AppSettings.CurrentSetting.TicketReaderCOMPort = this.comTicketReader.ComPort;
            AppSettings.CurrentSetting.BillPrinterCOMPort = this.comBillPrinter.ComPort;
            AppSettings.CurrentSetting.YCTReaderCOMPort = this.comYCT.ComPort;
            AppSettings.CurrentSetting.ParkFullLedCOMPort = this.comParkFullLed.ComPort;
            AppSettings.CurrentSetting.ParkFeeLedType = (byte)(this.comLedType.SelectedIndex > 0 ? this.comLedType.SelectedIndex : 0);
            AppSettings.CurrentSetting.OpenLastOpenedVideo = this.chkOpenLastOpenedVideo.Checked;
            AppSettings.CurrentSetting.ShowOnlyListenedPark = this.chkShowOnlyListenedPark.Checked;
            AppSettings.CurrentSetting.Debug = this.chkDebug.Checked;
            AppSettings.CurrentSetting.Optimized = this.chkOptimized.Checked;
            AppSettings.CurrentSetting.NeedPasswordWhenExit = this.chkNeedPasswordWhenExit.Checked;
            AppSettings.CurrentSetting.EnableTTS = this.chkEnableTTS.Checked;
            AppSettings.CurrentSetting.EnlargeMemo = this.chkEnlargeMemo.Checked;
            AppSettings.CurrentSetting.ChargeAfterMemo = this.chkChargeAfterMemo.Checked;
            AppSettings.CurrentSetting.ShowAPMMonitor = this.chkShowAPMMonitor.Checked;
            AppSettings.CurrentSetting.EnableZST = this.chkEnableZST.Checked;
            AppSettings.CurrentSetting.ZSTReaderIP = this.txtZSTReaderIP.IP;
            AppSettings.CurrentSetting.EnableWriteCard = this.chkEnableWriteCard.Checked;
            AppSettings.CurrentSetting.AuotAddToFirewallException = this.chkAuotAddToFirewallException.Checked;
            if (AppSettings.CurrentSetting.ParkingCommunicationIP != this.comParkingCommunicationIP.Text)
            {
                MessageBox.Show(Resources.Resource1.FrmSystemOption_ParkingCommunicationIPChangedAlert);
            }
            AppSettings.CurrentSetting.ParkingCommunicationIP = this.comParkingCommunicationIP.Text;
            AppSettings.CurrentSetting.CheckConnectionWithPing = this.chkCheckConnectionWithPing.Checked;
            AppSettings.CurrentSetting.SwitchEntrance = this.chkSwitchEntrance.Checked;
            AppSettings.CurrentSetting.EnableHotel = this.chkEnableHotel.Checked;
            AppSettings.CurrentSetting.NewCardValidCommand = this.chkNewCardValidCommand.Checked;
            AppSettings.CurrentSetting.ParkingSamNO = (byte)(this.comSamNo.SelectedIndex + 1);
            AppSettings.CurrentSetting.EnablePOSButton = this.chkShowPosButton.Checked;
            AppSettings.CurrentSetting.SpeakPromptWhenCarArrival = this.chkSpeakPromptWhenCarArrival.Checked;
        }
        #endregion




    }
}
