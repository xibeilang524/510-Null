namespace Ralid.Park.UI
{
    partial class FrmLocalSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocalSettings));
            this.tab1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.chkSpeakPromptWhenCarArrival = new System.Windows.Forms.CheckBox();
            this.chkShowPosButton = new System.Windows.Forms.CheckBox();
            this.comSamNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comParkingCommunicationIP = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.chkCheckConnectionWithPing = new System.Windows.Forms.CheckBox();
            this.chkNewCardValidCommand = new System.Windows.Forms.CheckBox();
            this.chkEnableHotel = new System.Windows.Forms.CheckBox();
            this.chkEnableWriteCard = new System.Windows.Forms.CheckBox();
            this.txtZSTReaderIP = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkAuotAddToFirewallException = new System.Windows.Forms.CheckBox();
            this.chkSwitchEntrance = new System.Windows.Forms.CheckBox();
            this.chkEnableZST = new System.Windows.Forms.CheckBox();
            this.chkShowAPMMonitor = new System.Windows.Forms.CheckBox();
            this.chkChargeAfterMemo = new System.Windows.Forms.CheckBox();
            this.chkEnlargeMemo = new System.Windows.Forms.CheckBox();
            this.chkShowOnlyListenedPark = new System.Windows.Forms.CheckBox();
            this.chkOptimized = new System.Windows.Forms.CheckBox();
            this.chkNeedPasswordWhenExit = new System.Windows.Forms.CheckBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.chkEnableTTS = new System.Windows.Forms.CheckBox();
            this.chkOpenLastOpenedVideo = new System.Windows.Forms.CheckBox();
            this.comLedType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comParkFullLed = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.comYCT = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.comBillPrinter = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.comFeeLed = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.comTicketReader = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accessMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_AddAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_DeleteAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.holidayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.TariffMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CustomCardType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_ClearTariff = new System.Windows.Forms.ToolStripMenuItem();
            this.butOK = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.tab1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.accessMenu.SuspendLayout();
            this.holidayMenu.SuspendLayout();
            this.TariffMenu.SuspendLayout();
            this.mnu_CustomCardType.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            resources.ApplyResources(this.tab1, "tab1");
            this.tab1.Controls.Add(this.tabGeneral);
            this.tab1.Name = "tab1";
            this.tab1.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.Controls.Add(this.chkSpeakPromptWhenCarArrival);
            this.tabGeneral.Controls.Add(this.chkShowPosButton);
            this.tabGeneral.Controls.Add(this.comSamNo);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.groupBox3);
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.comParkingCommunicationIP);
            this.tabGeneral.Controls.Add(this.label29);
            this.tabGeneral.Controls.Add(this.chkCheckConnectionWithPing);
            this.tabGeneral.Controls.Add(this.chkNewCardValidCommand);
            this.tabGeneral.Controls.Add(this.chkEnableHotel);
            this.tabGeneral.Controls.Add(this.chkEnableWriteCard);
            this.tabGeneral.Controls.Add(this.txtZSTReaderIP);
            this.tabGeneral.Controls.Add(this.label9);
            this.tabGeneral.Controls.Add(this.chkAuotAddToFirewallException);
            this.tabGeneral.Controls.Add(this.chkSwitchEntrance);
            this.tabGeneral.Controls.Add(this.chkEnableZST);
            this.tabGeneral.Controls.Add(this.chkShowAPMMonitor);
            this.tabGeneral.Controls.Add(this.chkChargeAfterMemo);
            this.tabGeneral.Controls.Add(this.chkEnlargeMemo);
            this.tabGeneral.Controls.Add(this.chkShowOnlyListenedPark);
            this.tabGeneral.Controls.Add(this.chkOptimized);
            this.tabGeneral.Controls.Add(this.chkNeedPasswordWhenExit);
            this.tabGeneral.Controls.Add(this.chkDebug);
            this.tabGeneral.Controls.Add(this.chkEnableTTS);
            this.tabGeneral.Controls.Add(this.chkOpenLastOpenedVideo);
            this.tabGeneral.Controls.Add(this.comLedType);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.comParkFullLed);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.comYCT);
            this.tabGeneral.Controls.Add(this.label7);
            this.tabGeneral.Controls.Add(this.comBillPrinter);
            this.tabGeneral.Controls.Add(this.label6);
            this.tabGeneral.Controls.Add(this.comFeeLed);
            this.tabGeneral.Controls.Add(this.label5);
            this.tabGeneral.Controls.Add(this.comTicketReader);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // chkSpeakPromptWhenCarArrival
            // 
            resources.ApplyResources(this.chkSpeakPromptWhenCarArrival, "chkSpeakPromptWhenCarArrival");
            this.chkSpeakPromptWhenCarArrival.Name = "chkSpeakPromptWhenCarArrival";
            this.chkSpeakPromptWhenCarArrival.UseVisualStyleBackColor = true;
            // 
            // chkShowPosButton
            // 
            resources.ApplyResources(this.chkShowPosButton, "chkShowPosButton");
            this.chkShowPosButton.Checked = true;
            this.chkShowPosButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPosButton.Name = "chkShowPosButton";
            this.chkShowPosButton.UseVisualStyleBackColor = true;
            // 
            // comSamNo
            // 
            resources.ApplyResources(this.comSamNo, "comSamNo");
            this.comSamNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSamNo.FormattingEnabled = true;
            this.comSamNo.Name = "comSamNo";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // comParkingCommunicationIP
            // 
            resources.ApplyResources(this.comParkingCommunicationIP, "comParkingCommunicationIP");
            this.comParkingCommunicationIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comParkingCommunicationIP.FormattingEnabled = true;
            this.comParkingCommunicationIP.Name = "comParkingCommunicationIP";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // chkCheckConnectionWithPing
            // 
            resources.ApplyResources(this.chkCheckConnectionWithPing, "chkCheckConnectionWithPing");
            this.chkCheckConnectionWithPing.Checked = true;
            this.chkCheckConnectionWithPing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheckConnectionWithPing.Name = "chkCheckConnectionWithPing";
            this.chkCheckConnectionWithPing.UseVisualStyleBackColor = true;
            // 
            // chkNewCardValidCommand
            // 
            resources.ApplyResources(this.chkNewCardValidCommand, "chkNewCardValidCommand");
            this.chkNewCardValidCommand.Name = "chkNewCardValidCommand";
            this.chkNewCardValidCommand.UseVisualStyleBackColor = true;
            // 
            // chkEnableHotel
            // 
            resources.ApplyResources(this.chkEnableHotel, "chkEnableHotel");
            this.chkEnableHotel.Name = "chkEnableHotel";
            this.chkEnableHotel.UseVisualStyleBackColor = true;
            // 
            // chkEnableWriteCard
            // 
            resources.ApplyResources(this.chkEnableWriteCard, "chkEnableWriteCard");
            this.chkEnableWriteCard.Name = "chkEnableWriteCard";
            this.chkEnableWriteCard.UseVisualStyleBackColor = true;
            // 
            // txtZSTReaderIP
            // 
            resources.ApplyResources(this.txtZSTReaderIP, "txtZSTReaderIP");
            this.txtZSTReaderIP.Name = "txtZSTReaderIP";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // chkAuotAddToFirewallException
            // 
            resources.ApplyResources(this.chkAuotAddToFirewallException, "chkAuotAddToFirewallException");
            this.chkAuotAddToFirewallException.Checked = true;
            this.chkAuotAddToFirewallException.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuotAddToFirewallException.Name = "chkAuotAddToFirewallException";
            this.chkAuotAddToFirewallException.UseVisualStyleBackColor = true;
            // 
            // chkSwitchEntrance
            // 
            resources.ApplyResources(this.chkSwitchEntrance, "chkSwitchEntrance");
            this.chkSwitchEntrance.Name = "chkSwitchEntrance";
            this.chkSwitchEntrance.UseVisualStyleBackColor = true;
            // 
            // chkEnableZST
            // 
            resources.ApplyResources(this.chkEnableZST, "chkEnableZST");
            this.chkEnableZST.Name = "chkEnableZST";
            this.chkEnableZST.UseVisualStyleBackColor = true;
            // 
            // chkShowAPMMonitor
            // 
            resources.ApplyResources(this.chkShowAPMMonitor, "chkShowAPMMonitor");
            this.chkShowAPMMonitor.Name = "chkShowAPMMonitor";
            this.chkShowAPMMonitor.UseVisualStyleBackColor = true;
            // 
            // chkChargeAfterMemo
            // 
            resources.ApplyResources(this.chkChargeAfterMemo, "chkChargeAfterMemo");
            this.chkChargeAfterMemo.Name = "chkChargeAfterMemo";
            this.chkChargeAfterMemo.UseVisualStyleBackColor = true;
            // 
            // chkEnlargeMemo
            // 
            resources.ApplyResources(this.chkEnlargeMemo, "chkEnlargeMemo");
            this.chkEnlargeMemo.Name = "chkEnlargeMemo";
            this.chkEnlargeMemo.UseVisualStyleBackColor = true;
            // 
            // chkShowOnlyListenedPark
            // 
            resources.ApplyResources(this.chkShowOnlyListenedPark, "chkShowOnlyListenedPark");
            this.chkShowOnlyListenedPark.Checked = true;
            this.chkShowOnlyListenedPark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowOnlyListenedPark.Name = "chkShowOnlyListenedPark";
            this.chkShowOnlyListenedPark.UseVisualStyleBackColor = true;
            // 
            // chkOptimized
            // 
            resources.ApplyResources(this.chkOptimized, "chkOptimized");
            this.chkOptimized.Checked = true;
            this.chkOptimized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOptimized.Name = "chkOptimized";
            this.chkOptimized.UseVisualStyleBackColor = true;
            // 
            // chkNeedPasswordWhenExit
            // 
            resources.ApplyResources(this.chkNeedPasswordWhenExit, "chkNeedPasswordWhenExit");
            this.chkNeedPasswordWhenExit.Name = "chkNeedPasswordWhenExit";
            this.chkNeedPasswordWhenExit.UseVisualStyleBackColor = true;
            // 
            // chkDebug
            // 
            resources.ApplyResources(this.chkDebug, "chkDebug");
            this.chkDebug.Checked = true;
            this.chkDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // chkEnableTTS
            // 
            resources.ApplyResources(this.chkEnableTTS, "chkEnableTTS");
            this.chkEnableTTS.Name = "chkEnableTTS";
            this.chkEnableTTS.UseVisualStyleBackColor = true;
            // 
            // chkOpenLastOpenedVideo
            // 
            resources.ApplyResources(this.chkOpenLastOpenedVideo, "chkOpenLastOpenedVideo");
            this.chkOpenLastOpenedVideo.Name = "chkOpenLastOpenedVideo";
            this.chkOpenLastOpenedVideo.UseVisualStyleBackColor = true;
            // 
            // comLedType
            // 
            resources.ApplyResources(this.comLedType, "comLedType");
            this.comLedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comLedType.FormattingEnabled = true;
            this.comLedType.Name = "comLedType";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comParkFullLed
            // 
            resources.ApplyResources(this.comParkFullLed, "comParkFullLed");
            this.comParkFullLed.FormattingEnabled = true;
            this.comParkFullLed.Name = "comParkFullLed";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comYCT
            // 
            resources.ApplyResources(this.comYCT, "comYCT");
            this.comYCT.FormattingEnabled = true;
            this.comYCT.Name = "comYCT";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // comBillPrinter
            // 
            resources.ApplyResources(this.comBillPrinter, "comBillPrinter");
            this.comBillPrinter.FormattingEnabled = true;
            this.comBillPrinter.Name = "comBillPrinter";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // comFeeLed
            // 
            resources.ApplyResources(this.comFeeLed, "comFeeLed");
            this.comFeeLed.FormattingEnabled = true;
            this.comFeeLed.Name = "comFeeLed";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // comTicketReader
            // 
            resources.ApplyResources(this.comTicketReader, "comTicketReader");
            this.comTicketReader.FormattingEnabled = true;
            this.comTicketReader.Name = "comTicketReader";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // accessMenu
            // 
            resources.ApplyResources(this.accessMenu, "accessMenu");
            this.accessMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_AddAccess,
            this.mnu_DeleteAccess});
            this.accessMenu.Name = "contextMenuStrip1";
            // 
            // mnu_AddAccess
            // 
            resources.ApplyResources(this.mnu_AddAccess, "mnu_AddAccess");
            this.mnu_AddAccess.Name = "mnu_AddAccess";
            // 
            // mnu_DeleteAccess
            // 
            resources.ApplyResources(this.mnu_DeleteAccess, "mnu_DeleteAccess");
            this.mnu_DeleteAccess.Name = "mnu_DeleteAccess";
            // 
            // holidayMenu
            // 
            resources.ApplyResources(this.holidayMenu, "holidayMenu");
            this.holidayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Add,
            this.mnu_Delete});
            this.holidayMenu.Name = "contextMenuStrip1";
            // 
            // mnu_Add
            // 
            resources.ApplyResources(this.mnu_Add, "mnu_Add");
            this.mnu_Add.Name = "mnu_Add";
            // 
            // mnu_Delete
            // 
            resources.ApplyResources(this.mnu_Delete, "mnu_Delete");
            this.mnu_Delete.Name = "mnu_Delete";
            // 
            // TariffMenu
            // 
            resources.ApplyResources(this.TariffMenu, "TariffMenu");
            this.TariffMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Clear});
            this.TariffMenu.Name = "TariffMenu";
            // 
            // mnu_Clear
            // 
            resources.ApplyResources(this.mnu_Clear, "mnu_Clear");
            this.mnu_Clear.Name = "mnu_Clear";
            // 
            // mnu_CustomCardType
            // 
            resources.ApplyResources(this.mnu_CustomCardType, "mnu_CustomCardType");
            this.mnu_CustomCardType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_ClearTariff});
            this.mnu_CustomCardType.Name = "mnu_CustomCardType";
            // 
            // mnu_ClearTariff
            // 
            resources.ApplyResources(this.mnu_ClearTariff, "mnu_ClearTariff");
            this.mnu_ClearTariff.Name = "mnu_ClearTariff";
            // 
            // butOK
            // 
            resources.ApplyResources(this.butOK, "butOK");
            this.butOK.Name = "butOK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // FrmLocalSettings
            // 
            this.AcceptButton = this.butOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.Controls.Add(this.tab1);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLocalSettings";
            this.Load += new System.EventHandler(this.FrmSysPara_Load);
            this.tab1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.accessMenu.ResumeLayout(false);
            this.holidayMenu.ResumeLayout(false);
            this.TariffMenu.ResumeLayout(false);
            this.mnu_CustomCardType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.ContextMenuStrip holidayMenu;
        private System.Windows.Forms.ToolStripMenuItem mnu_Add;
        private System.Windows.Forms.ToolStripMenuItem mnu_Delete;
        private System.Windows.Forms.ContextMenuStrip TariffMenu;
        private System.Windows.Forms.ToolStripMenuItem mnu_Clear;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.GeneralLibrary .WinformControl .ComPortComboBox comFeeLed;
        private System.Windows.Forms.Label label5;
        private Ralid.GeneralLibrary .WinformControl .ComPortComboBox comTicketReader;
        private System.Windows.Forms.Label label2;
        private Ralid.GeneralLibrary .WinformControl .ComPortComboBox comBillPrinter;
        private System.Windows.Forms.Label label6;
        private Ralid.GeneralLibrary .WinformControl .ComPortComboBox comYCT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkOpenLastOpenedVideo;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.ContextMenuStrip accessMenu;
        private System.Windows.Forms.ToolStripMenuItem mnu_AddAccess;
        private System.Windows.Forms.ToolStripMenuItem mnu_DeleteAccess;
        private Ralid.GeneralLibrary.WinformControl.ComPortComboBox comParkFullLed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkEnableTTS;
        private System.Windows.Forms.CheckBox chkNeedPasswordWhenExit;
        private System.Windows.Forms.ContextMenuStrip mnu_CustomCardType;
        private System.Windows.Forms.ToolStripMenuItem mnu_ClearTariff;
        private System.Windows.Forms.ComboBox comLedType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkShowOnlyListenedPark;
        private System.Windows.Forms.CheckBox chkOptimized;
        private System.Windows.Forms.CheckBox chkEnlargeMemo;
        private System.Windows.Forms.CheckBox chkChargeAfterMemo;
        private System.Windows.Forms.CheckBox chkShowAPMMonitor;
        private System.Windows.Forms.CheckBox chkEnableZST;
        private System.Windows.Forms.Label label9;
        private GeneralLibrary.WinformControl.UCIPTextBox txtZSTReaderIP;
        private System.Windows.Forms.CheckBox chkEnableWriteCard;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox comParkingCommunicationIP;
        private System.Windows.Forms.CheckBox chkAuotAddToFirewallException;
        private System.Windows.Forms.CheckBox chkCheckConnectionWithPing;
        private System.Windows.Forms.CheckBox chkSwitchEntrance;
        private System.Windows.Forms.CheckBox chkEnableHotel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkNewCardValidCommand;
        private System.Windows.Forms.ComboBox comSamNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowPosButton;
        private System.Windows.Forms.CheckBox chkSpeakPromptWhenCarArrival;
    }
}