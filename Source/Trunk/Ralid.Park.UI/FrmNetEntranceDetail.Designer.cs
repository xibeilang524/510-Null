namespace Ralid.Park.UI
{
    partial class FrmNetEntranceDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNetEntranceDetail));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.txtCarPlateNotifyControllerSecond2 = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.txtCarPlateNotifyControllerSecond1 = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.ip2 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip4 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip3 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip1 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.pnlPaymentEventIndex = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPaymentEventIndex = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtCarPlateNotifyController = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCarPlateIP = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtVideoID = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdExit = new System.Windows.Forms.RadioButton();
            this.rdEnter = new System.Windows.Forms.RadioButton();
            this.label33 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdHost = new System.Windows.Forms.RadioButton();
            this.rdNotHost = new System.Windows.Forms.RadioButton();
            this.txtIPMask = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.txtGateWay = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.txtIP = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.txtEntranceName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtEventPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtControlPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkUseAsACS = new System.Windows.Forms.CheckBox();
            this.chkValid = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkNoParkingCount = new System.Windows.Forms.CheckBox();
            this.chkExportCharge = new System.Windows.Forms.CheckBox();
            this.chkEnableParkvacantLed = new System.Windows.Forms.CheckBox();
            this.chkOnlyTempReaderAfterButtonClick = new System.Windows.Forms.CheckBox();
            this.chkPrepayCardWaitWhenOut = new System.Windows.Forms.CheckBox();
            this.chkMonthCardWaitWhenOut = new System.Windows.Forms.CheckBox();
            this.chkNoReaderOnCardCaptuer = new System.Windows.Forms.CheckBox();
            this.cmbTicketReader = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.cmbTicketPrinter = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkAllowTempCard = new System.Windows.Forms.CheckBox();
            this.chkAllowEjectCardWhithoutRead = new System.Windows.Forms.CheckBox();
            this.chkForbidWhenFull = new System.Windows.Forms.CheckBox();
            this.chkForbidWhenCardExpired = new System.Windows.Forms.CheckBox();
            this.txtCardReadInterval = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.chkLightEnable = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.chkTakeCardNeedCarSense = new System.Windows.Forms.CheckBox();
            this.tabCardProperty = new System.Windows.Forms.TabPage();
            this.UCCardTypeProperty = new Ralid.Park.UserControls.UCCardTypeProperty();
            this.btnGetHardwareInfo = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.txtCarPlateNotifyControllerSecond1.SuspendLayout();
            this.pnlPaymentEventIndex.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabCardProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabCardProperty);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabGeneral.Controls.Add(this.txtCarPlateNotifyControllerSecond2);
            this.tabGeneral.Controls.Add(this.txtCarPlateNotifyControllerSecond1);
            this.tabGeneral.Controls.Add(this.pnlPaymentEventIndex);
            this.tabGeneral.Controls.Add(this.txtCarPlateNotifyController);
            this.tabGeneral.Controls.Add(this.label13);
            this.tabGeneral.Controls.Add(this.label12);
            this.tabGeneral.Controls.Add(this.label15);
            this.tabGeneral.Controls.Add(this.txtCarPlateIP);
            this.tabGeneral.Controls.Add(this.label8);
            this.tabGeneral.Controls.Add(this.txtVideoID);
            this.tabGeneral.Controls.Add(this.label10);
            this.tabGeneral.Controls.Add(this.label34);
            this.tabGeneral.Controls.Add(this.groupBox5);
            this.tabGeneral.Controls.Add(this.label33);
            this.tabGeneral.Controls.Add(this.groupBox4);
            this.tabGeneral.Controls.Add(this.txtIPMask);
            this.tabGeneral.Controls.Add(this.txtGateWay);
            this.tabGeneral.Controls.Add(this.txtIP);
            this.tabGeneral.Controls.Add(this.txtEntranceName);
            this.tabGeneral.Controls.Add(this.label7);
            this.tabGeneral.Controls.Add(this.txtEventPort);
            this.tabGeneral.Controls.Add(this.txtControlPort);
            this.tabGeneral.Controls.Add(this.label5);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.ToolTipText = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // txtCarPlateNotifyControllerSecond2
            // 
            resources.ApplyResources(this.txtCarPlateNotifyControllerSecond2, "txtCarPlateNotifyControllerSecond2");
            this.txtCarPlateNotifyControllerSecond2.Name = "txtCarPlateNotifyControllerSecond2";
            // 
            // txtCarPlateNotifyControllerSecond1
            // 
            this.txtCarPlateNotifyControllerSecond1.Controls.Add(this.ip2);
            this.txtCarPlateNotifyControllerSecond1.Controls.Add(this.ip4);
            this.txtCarPlateNotifyControllerSecond1.Controls.Add(this.ip3);
            this.txtCarPlateNotifyControllerSecond1.Controls.Add(this.ip1);
            resources.ApplyResources(this.txtCarPlateNotifyControllerSecond1, "txtCarPlateNotifyControllerSecond1");
            this.txtCarPlateNotifyControllerSecond1.Name = "txtCarPlateNotifyControllerSecond1";
            // 
            // ip2
            // 
            resources.ApplyResources(this.ip2, "ip2");
            this.ip2.MaxValue = 255;
            this.ip2.MinValue = 0;
            this.ip2.Name = "ip2";
            // 
            // ip4
            // 
            resources.ApplyResources(this.ip4, "ip4");
            this.ip4.MaxValue = 255;
            this.ip4.MinValue = 0;
            this.ip4.Name = "ip4";
            // 
            // ip3
            // 
            resources.ApplyResources(this.ip3, "ip3");
            this.ip3.MaxValue = 255;
            this.ip3.MinValue = 0;
            this.ip3.Name = "ip3";
            // 
            // ip1
            // 
            resources.ApplyResources(this.ip1, "ip1");
            this.ip1.MaxValue = 255;
            this.ip1.MinValue = 0;
            this.ip1.Name = "ip1";
            // 
            // pnlPaymentEventIndex
            // 
            this.pnlPaymentEventIndex.Controls.Add(this.label11);
            this.pnlPaymentEventIndex.Controls.Add(this.txtPaymentEventIndex);
            resources.ApplyResources(this.pnlPaymentEventIndex, "pnlPaymentEventIndex");
            this.pnlPaymentEventIndex.Name = "pnlPaymentEventIndex";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtPaymentEventIndex
            // 
            resources.ApplyResources(this.txtPaymentEventIndex, "txtPaymentEventIndex");
            this.txtPaymentEventIndex.MaxValue = 16777215;
            this.txtPaymentEventIndex.MinValue = 0;
            this.txtPaymentEventIndex.Name = "txtPaymentEventIndex";
            // 
            // txtCarPlateNotifyController
            // 
            resources.ApplyResources(this.txtCarPlateNotifyController, "txtCarPlateNotifyController");
            this.txtCarPlateNotifyController.Name = "txtCarPlateNotifyController";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txtCarPlateIP
            // 
            resources.ApplyResources(this.txtCarPlateIP, "txtCarPlateIP");
            this.txtCarPlateIP.Name = "txtCarPlateIP";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtVideoID
            // 
            resources.ApplyResources(this.txtVideoID, "txtVideoID");
            this.txtVideoID.MaxValue = 10;
            this.txtVideoID.MinValue = 0;
            this.txtVideoID.Name = "txtVideoID";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label34.Name = "label34";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdExit);
            this.groupBox5.Controls.Add(this.rdEnter);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // rdExit
            // 
            resources.ApplyResources(this.rdExit, "rdExit");
            this.rdExit.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.rdExit.Name = "rdExit";
            this.rdExit.UseVisualStyleBackColor = true;
            this.rdExit.CheckedChanged += new System.EventHandler(this.rdEnter_CheckedChanged);
            // 
            // rdEnter
            // 
            resources.ApplyResources(this.rdEnter, "rdEnter");
            this.rdEnter.Checked = true;
            this.rdEnter.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.rdEnter.Name = "rdEnter";
            this.rdEnter.TabStop = true;
            this.rdEnter.UseVisualStyleBackColor = true;
            this.rdEnter.CheckedChanged += new System.EventHandler(this.rdEnter_CheckedChanged);
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label33.Name = "label33";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdHost);
            this.groupBox4.Controls.Add(this.rdNotHost);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // rdHost
            // 
            resources.ApplyResources(this.rdHost, "rdHost");
            this.rdHost.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.rdHost.Name = "rdHost";
            this.rdHost.UseVisualStyleBackColor = true;
            // 
            // rdNotHost
            // 
            resources.ApplyResources(this.rdNotHost, "rdNotHost");
            this.rdNotHost.Checked = true;
            this.rdNotHost.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.rdNotHost.Name = "rdNotHost";
            this.rdNotHost.TabStop = true;
            this.rdNotHost.UseVisualStyleBackColor = true;
            // 
            // txtIPMask
            // 
            resources.ApplyResources(this.txtIPMask, "txtIPMask");
            this.txtIPMask.Name = "txtIPMask";
            // 
            // txtGateWay
            // 
            resources.ApplyResources(this.txtGateWay, "txtGateWay");
            this.txtGateWay.Name = "txtGateWay";
            // 
            // txtIP
            // 
            resources.ApplyResources(this.txtIP, "txtIP");
            this.txtIP.Name = "txtIP";
            // 
            // txtEntranceName
            // 
            resources.ApplyResources(this.txtEntranceName, "txtEntranceName");
            this.txtEntranceName.Name = "txtEntranceName";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label7.Name = "label7";
            // 
            // txtEventPort
            // 
            resources.ApplyResources(this.txtEventPort, "txtEventPort");
            this.txtEventPort.MaxValue = 65535;
            this.txtEventPort.MinValue = 0;
            this.txtEventPort.Name = "txtEventPort";
            // 
            // txtControlPort
            // 
            resources.ApplyResources(this.txtControlPort, "txtControlPort");
            this.txtControlPort.MaxValue = 65535;
            this.txtControlPort.MinValue = 0;
            this.txtControlPort.Name = "txtControlPort";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label1.Name = "label1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.chkUseAsACS);
            this.tabPage2.Controls.Add(this.chkValid);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.chkNoParkingCount);
            this.tabPage2.Controls.Add(this.chkExportCharge);
            this.tabPage2.Controls.Add(this.chkEnableParkvacantLed);
            this.tabPage2.Controls.Add(this.chkOnlyTempReaderAfterButtonClick);
            this.tabPage2.Controls.Add(this.chkPrepayCardWaitWhenOut);
            this.tabPage2.Controls.Add(this.chkMonthCardWaitWhenOut);
            this.tabPage2.Controls.Add(this.chkNoReaderOnCardCaptuer);
            this.tabPage2.Controls.Add(this.cmbTicketReader);
            this.tabPage2.Controls.Add(this.cmbTicketPrinter);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.chkAllowTempCard);
            this.tabPage2.Controls.Add(this.chkAllowEjectCardWhithoutRead);
            this.tabPage2.Controls.Add(this.chkForbidWhenFull);
            this.tabPage2.Controls.Add(this.chkForbidWhenCardExpired);
            this.tabPage2.Controls.Add(this.txtCardReadInterval);
            this.tabPage2.Controls.Add(this.chkLightEnable);
            this.tabPage2.Controls.Add(this.label29);
            this.tabPage2.Controls.Add(this.chkTakeCardNeedCarSense);
            this.tabPage2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.ToolTipText = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // chkUseAsACS
            // 
            resources.ApplyResources(this.chkUseAsACS, "chkUseAsACS");
            this.chkUseAsACS.Name = "chkUseAsACS";
            this.chkUseAsACS.UseVisualStyleBackColor = true;
            // 
            // chkValid
            // 
            resources.ApplyResources(this.chkValid, "chkValid");
            this.chkValid.Checked = true;
            this.chkValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkValid.Name = "chkValid";
            this.chkValid.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkNoParkingCount
            // 
            resources.ApplyResources(this.chkNoParkingCount, "chkNoParkingCount");
            this.chkNoParkingCount.Name = "chkNoParkingCount";
            this.chkNoParkingCount.UseVisualStyleBackColor = true;
            // 
            // chkExportCharge
            // 
            resources.ApplyResources(this.chkExportCharge, "chkExportCharge");
            this.chkExportCharge.Checked = true;
            this.chkExportCharge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportCharge.Name = "chkExportCharge";
            this.chkExportCharge.UseVisualStyleBackColor = true;
            // 
            // chkEnableParkvacantLed
            // 
            resources.ApplyResources(this.chkEnableParkvacantLed, "chkEnableParkvacantLed");
            this.chkEnableParkvacantLed.Name = "chkEnableParkvacantLed";
            this.chkEnableParkvacantLed.UseVisualStyleBackColor = true;
            // 
            // chkOnlyTempReaderAfterButtonClick
            // 
            resources.ApplyResources(this.chkOnlyTempReaderAfterButtonClick, "chkOnlyTempReaderAfterButtonClick");
            this.chkOnlyTempReaderAfterButtonClick.Name = "chkOnlyTempReaderAfterButtonClick";
            this.chkOnlyTempReaderAfterButtonClick.UseVisualStyleBackColor = true;
            // 
            // chkPrepayCardWaitWhenOut
            // 
            resources.ApplyResources(this.chkPrepayCardWaitWhenOut, "chkPrepayCardWaitWhenOut");
            this.chkPrepayCardWaitWhenOut.Name = "chkPrepayCardWaitWhenOut";
            this.chkPrepayCardWaitWhenOut.UseVisualStyleBackColor = true;
            // 
            // chkMonthCardWaitWhenOut
            // 
            resources.ApplyResources(this.chkMonthCardWaitWhenOut, "chkMonthCardWaitWhenOut");
            this.chkMonthCardWaitWhenOut.Name = "chkMonthCardWaitWhenOut";
            this.chkMonthCardWaitWhenOut.UseVisualStyleBackColor = true;
            // 
            // chkNoReaderOnCardCaptuer
            // 
            resources.ApplyResources(this.chkNoReaderOnCardCaptuer, "chkNoReaderOnCardCaptuer");
            this.chkNoReaderOnCardCaptuer.Name = "chkNoReaderOnCardCaptuer";
            this.chkNoReaderOnCardCaptuer.UseVisualStyleBackColor = true;
            // 
            // cmbTicketReader
            // 
            this.cmbTicketReader.FormattingEnabled = true;
            resources.ApplyResources(this.cmbTicketReader, "cmbTicketReader");
            this.cmbTicketReader.Name = "cmbTicketReader";
            // 
            // cmbTicketPrinter
            // 
            this.cmbTicketPrinter.FormattingEnabled = true;
            resources.ApplyResources(this.cmbTicketPrinter, "cmbTicketPrinter");
            this.cmbTicketPrinter.Name = "cmbTicketPrinter";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // chkAllowTempCard
            // 
            resources.ApplyResources(this.chkAllowTempCard, "chkAllowTempCard");
            this.chkAllowTempCard.Checked = true;
            this.chkAllowTempCard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowTempCard.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkAllowTempCard.Name = "chkAllowTempCard";
            this.chkAllowTempCard.UseVisualStyleBackColor = true;
            // 
            // chkAllowEjectCardWhithoutRead
            // 
            resources.ApplyResources(this.chkAllowEjectCardWhithoutRead, "chkAllowEjectCardWhithoutRead");
            this.chkAllowEjectCardWhithoutRead.Checked = true;
            this.chkAllowEjectCardWhithoutRead.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowEjectCardWhithoutRead.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkAllowEjectCardWhithoutRead.Name = "chkAllowEjectCardWhithoutRead";
            this.chkAllowEjectCardWhithoutRead.UseVisualStyleBackColor = true;
            // 
            // chkForbidWhenFull
            // 
            resources.ApplyResources(this.chkForbidWhenFull, "chkForbidWhenFull");
            this.chkForbidWhenFull.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkForbidWhenFull.Name = "chkForbidWhenFull";
            this.chkForbidWhenFull.UseVisualStyleBackColor = true;
            // 
            // chkForbidWhenCardExpired
            // 
            resources.ApplyResources(this.chkForbidWhenCardExpired, "chkForbidWhenCardExpired");
            this.chkForbidWhenCardExpired.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkForbidWhenCardExpired.Name = "chkForbidWhenCardExpired";
            this.chkForbidWhenCardExpired.UseVisualStyleBackColor = true;
            // 
            // txtCardReadInterval
            // 
            resources.ApplyResources(this.txtCardReadInterval, "txtCardReadInterval");
            this.txtCardReadInterval.MaxValue = 2147483647;
            this.txtCardReadInterval.MinValue = -2147483648;
            this.txtCardReadInterval.Name = "txtCardReadInterval";
            // 
            // chkLightEnable
            // 
            resources.ApplyResources(this.chkLightEnable, "chkLightEnable");
            this.chkLightEnable.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkLightEnable.Name = "chkLightEnable";
            this.chkLightEnable.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label29.Name = "label29";
            // 
            // chkTakeCardNeedCarSense
            // 
            resources.ApplyResources(this.chkTakeCardNeedCarSense, "chkTakeCardNeedCarSense");
            this.chkTakeCardNeedCarSense.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkTakeCardNeedCarSense.Name = "chkTakeCardNeedCarSense";
            this.chkTakeCardNeedCarSense.UseVisualStyleBackColor = true;
            // 
            // tabCardProperty
            // 
            this.tabCardProperty.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabCardProperty.Controls.Add(this.UCCardTypeProperty);
            resources.ApplyResources(this.tabCardProperty, "tabCardProperty");
            this.tabCardProperty.Name = "tabCardProperty";
            // 
            // UCCardTypeProperty
            // 
            resources.ApplyResources(this.UCCardTypeProperty, "UCCardTypeProperty");
            this.UCCardTypeProperty.Name = "UCCardTypeProperty";
            // 
            // btnGetHardwareInfo
            // 
            resources.ApplyResources(this.btnGetHardwareInfo, "btnGetHardwareInfo");
            this.btnGetHardwareInfo.Name = "btnGetHardwareInfo";
            this.btnGetHardwareInfo.UseVisualStyleBackColor = true;
            this.btnGetHardwareInfo.Click += new System.EventHandler(this.btnGetHardwareInfo_Click);
            // 
            // FrmNetEntranceDetail
            // 
            this.AcceptButton = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGetHardwareInfo);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNetEntranceDetail";
            this.Load += new System.EventHandler(this.FrmEntranceDetail_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.btnGetHardwareInfo, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.txtCarPlateNotifyControllerSecond1.ResumeLayout(false);
            this.txtCarPlateNotifyControllerSecond1.PerformLayout();
            this.pnlPaymentEventIndex.ResumeLayout(false);
            this.pnlPaymentEventIndex.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabCardProperty.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkTakeCardNeedCarSense;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.CheckBox chkLightEnable;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtEventPort;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtControlPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtEntranceName;
        private System.Windows.Forms.Label label7;
        private Ralid.GeneralLibrary.WinformControl.UCIPTextBox txtIPMask;
        private Ralid.GeneralLibrary.WinformControl.UCIPTextBox txtGateWay;
        private Ralid.GeneralLibrary.WinformControl.UCIPTextBox txtIP;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtCardReadInterval;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdExit;
        private System.Windows.Forms.RadioButton rdEnter;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdHost;
        private System.Windows.Forms.RadioButton rdNotHost;
        private System.Windows.Forms.CheckBox chkForbidWhenFull;
        private System.Windows.Forms.CheckBox chkForbidWhenCardExpired;
        private System.Windows.Forms.CheckBox chkAllowEjectCardWhithoutRead;
        private System.Windows.Forms.CheckBox chkAllowTempCard;
        private GeneralLibrary.WinformControl.ComPortComboBox cmbTicketReader;
        private GeneralLibrary.WinformControl.ComPortComboBox cmbTicketPrinter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkOnlyTempReaderAfterButtonClick;
        private System.Windows.Forms.CheckBox chkPrepayCardWaitWhenOut;
        private System.Windows.Forms.CheckBox chkMonthCardWaitWhenOut;
        private System.Windows.Forms.CheckBox chkNoReaderOnCardCaptuer;
        private System.Windows.Forms.CheckBox chkEnableParkvacantLed;
        private System.Windows.Forms.CheckBox chkValid;
        private System.Windows.Forms.CheckBox chkNoParkingCount;
        private System.Windows.Forms.CheckBox chkExportCharge;
        private System.Windows.Forms.Button btnGetHardwareInfo;
        private System.Windows.Forms.CheckBox chkUseAsACS;
        private System.Windows.Forms.Label label8;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtVideoID;
        private System.Windows.Forms.Label label10;
        private Ralid.GeneralLibrary.WinformControl.UCIPTextBox txtCarPlateIP;
        private GeneralLibrary.WinformControl.UCIPTextBox txtCarPlateNotifyController;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlPaymentEventIndex;
        private System.Windows.Forms.Label label11;
        private GeneralLibrary.WinformControl.IntergerTextBox txtPaymentEventIndex;
        private System.Windows.Forms.TabPage tabCardProperty;
        private UserControls.UCCardTypeProperty UCCardTypeProperty;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private GeneralLibrary.WinformControl.UCIPTextBox txtCarPlateNotifyControllerSecond2;
        private GeneralLibrary.WinformControl.UCIPTextBox txtCarPlateNotifyControllerSecond1;
        private GeneralLibrary.WinformControl.IntergerTextBox ip2;
        private GeneralLibrary.WinformControl.IntergerTextBox ip4;
        private GeneralLibrary.WinformControl.IntergerTextBox ip3;
        private GeneralLibrary.WinformControl.IntergerTextBox ip1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}