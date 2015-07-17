namespace Ralid.Park.UI
{
    partial class FrmEntranceDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEntranceDetail));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEntranceName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.comAddress = new Ralid.Park.UserControls.EntranceAddressComboBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTicketPrinter = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label29 = new System.Windows.Forms.Label();
            this.chkLightOnWhenCarArrive = new System.Windows.Forms.CheckBox();
            this.txtCardReadInterval = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.chkReadCardNeedCarSense = new System.Windows.Forms.CheckBox();
            this.chkOnlyTempReaderAfterButtonClick = new System.Windows.Forms.CheckBox();
            this.chkPrepayCardWaitWhenOut = new System.Windows.Forms.CheckBox();
            this.chkMonthCardWaitWhenOut = new System.Windows.Forms.CheckBox();
            this.chkCardValidNeedResponse = new System.Windows.Forms.CheckBox();
            this.chkNoReaderOnCardCaptuer = new System.Windows.Forms.CheckBox();
            this.chkEnableParkvacantLed = new System.Windows.Forms.CheckBox();
            this.chkNoParkingCount = new System.Windows.Forms.CheckBox();
            this.chkUseAsACS = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVideoID = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtCarPlateIP = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTicketReader = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.cmbTicketReader2 = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkForbidWhenFull = new System.Windows.Forms.CheckBox();
            this.chkForbidWhenCardExpired = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtEntranceName
            // 
            resources.ApplyResources(this.txtEntranceName, "txtEntranceName");
            this.txtEntranceName.Name = "txtEntranceName";
            // 
            // comAddress
            // 
            this.comAddress.FormattingEnabled = true;
            resources.ApplyResources(this.comAddress, "comAddress");
            this.comAddress.Name = "comAddress";
            this.comAddress.SelectedIndexChanged += new System.EventHandler(this.comAddress_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbTicketPrinter
            // 
            this.cmbTicketPrinter.FormattingEnabled = true;
            resources.ApplyResources(this.cmbTicketPrinter, "cmbTicketPrinter");
            this.cmbTicketPrinter.Name = "cmbTicketPrinter";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // chkLightOnWhenCarArrive
            // 
            resources.ApplyResources(this.chkLightOnWhenCarArrive, "chkLightOnWhenCarArrive");
            this.chkLightOnWhenCarArrive.Name = "chkLightOnWhenCarArrive";
            this.chkLightOnWhenCarArrive.UseVisualStyleBackColor = true;
            // 
            // txtCardReadInterval
            // 
            resources.ApplyResources(this.txtCardReadInterval, "txtCardReadInterval");
            this.txtCardReadInterval.MaxValue = 25;
            this.txtCardReadInterval.MinValue = 0;
            this.txtCardReadInterval.Name = "txtCardReadInterval";
            // 
            // chkReadCardNeedCarSense
            // 
            resources.ApplyResources(this.chkReadCardNeedCarSense, "chkReadCardNeedCarSense");
            this.chkReadCardNeedCarSense.Name = "chkReadCardNeedCarSense";
            this.chkReadCardNeedCarSense.UseVisualStyleBackColor = true;
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
            // chkCardValidNeedResponse
            // 
            resources.ApplyResources(this.chkCardValidNeedResponse, "chkCardValidNeedResponse");
            this.chkCardValidNeedResponse.Name = "chkCardValidNeedResponse";
            this.chkCardValidNeedResponse.UseVisualStyleBackColor = true;
            // 
            // chkNoReaderOnCardCaptuer
            // 
            resources.ApplyResources(this.chkNoReaderOnCardCaptuer, "chkNoReaderOnCardCaptuer");
            this.chkNoReaderOnCardCaptuer.Name = "chkNoReaderOnCardCaptuer";
            this.chkNoReaderOnCardCaptuer.UseVisualStyleBackColor = true;
            // 
            // chkEnableParkvacantLed
            // 
            resources.ApplyResources(this.chkEnableParkvacantLed, "chkEnableParkvacantLed");
            this.chkEnableParkvacantLed.Name = "chkEnableParkvacantLed";
            this.chkEnableParkvacantLed.UseVisualStyleBackColor = true;
            // 
            // chkNoParkingCount
            // 
            resources.ApplyResources(this.chkNoParkingCount, "chkNoParkingCount");
            this.chkNoParkingCount.Name = "chkNoParkingCount";
            this.chkNoParkingCount.UseVisualStyleBackColor = true;
            // 
            // chkUseAsACS
            // 
            resources.ApplyResources(this.chkUseAsACS, "chkUseAsACS");
            this.chkUseAsACS.Name = "chkUseAsACS";
            this.chkUseAsACS.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtVideoID);
            this.groupBox2.Controls.Add(this.txtCarPlateIP);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtVideoID
            // 
            resources.ApplyResources(this.txtVideoID, "txtVideoID");
            this.txtVideoID.MaxValue = 2147483647;
            this.txtVideoID.MinValue = -2147483648;
            this.txtVideoID.Name = "txtVideoID";
            // 
            // txtCarPlateIP
            // 
            resources.ApplyResources(this.txtCarPlateIP, "txtCarPlateIP");
            this.txtCarPlateIP.Name = "txtCarPlateIP";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cmbTicketReader
            // 
            this.cmbTicketReader.FormattingEnabled = true;
            resources.ApplyResources(this.cmbTicketReader, "cmbTicketReader");
            this.cmbTicketReader.Name = "cmbTicketReader";
            // 
            // cmbTicketReader2
            // 
            this.cmbTicketReader2.FormattingEnabled = true;
            resources.ApplyResources(this.cmbTicketReader2, "cmbTicketReader2");
            this.cmbTicketReader2.Name = "cmbTicketReader2";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
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
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // FrmEntranceDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkUseAsACS);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.chkForbidWhenFull);
            this.Controls.Add(this.chkForbidWhenCardExpired);
            this.Controls.Add(this.chkCardValidNeedResponse);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmbTicketReader2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkNoParkingCount);
            this.Controls.Add(this.chkEnableParkvacantLed);
            this.Controls.Add(this.chkOnlyTempReaderAfterButtonClick);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkPrepayCardWaitWhenOut);
            this.Controls.Add(this.chkMonthCardWaitWhenOut);
            this.Controls.Add(this.chkNoReaderOnCardCaptuer);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.txtCardReadInterval);
            this.Controls.Add(this.chkReadCardNeedCarSense);
            this.Controls.Add(this.cmbTicketReader);
            this.Controls.Add(this.cmbTicketPrinter);
            this.Controls.Add(this.chkLightOnWhenCarArrive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comAddress);
            this.Controls.Add(this.txtEntranceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmEntranceDetail";
            this.ShowInTaskbar = false;
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtEntranceName, 0);
            this.Controls.SetChildIndex(this.comAddress, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.chkLightOnWhenCarArrive, 0);
            this.Controls.SetChildIndex(this.cmbTicketPrinter, 0);
            this.Controls.SetChildIndex(this.cmbTicketReader, 0);
            this.Controls.SetChildIndex(this.chkReadCardNeedCarSense, 0);
            this.Controls.SetChildIndex(this.txtCardReadInterval, 0);
            this.Controls.SetChildIndex(this.label29, 0);
            this.Controls.SetChildIndex(this.chkNoReaderOnCardCaptuer, 0);
            this.Controls.SetChildIndex(this.chkMonthCardWaitWhenOut, 0);
            this.Controls.SetChildIndex(this.chkPrepayCardWaitWhenOut, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.chkOnlyTempReaderAfterButtonClick, 0);
            this.Controls.SetChildIndex(this.chkEnableParkvacantLed, 0);
            this.Controls.SetChildIndex(this.chkNoParkingCount, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.cmbTicketReader2, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.chkCardValidNeedResponse, 0);
            this.Controls.SetChildIndex(this.chkForbidWhenCardExpired, 0);
            this.Controls.SetChildIndex(this.chkForbidWhenFull, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            this.Controls.SetChildIndex(this.chkUseAsACS, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtEntranceName;
        private Ralid.Park.UserControls.EntranceAddressComboBox comAddress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Ralid.GeneralLibrary.WinformControl.ComPortComboBox cmbTicketPrinter;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.CheckBox chkLightOnWhenCarArrive;
        private GeneralLibrary.WinformControl.IntergerTextBox txtCardReadInterval;
        private System.Windows.Forms.CheckBox chkReadCardNeedCarSense;
        private System.Windows.Forms.CheckBox chkOnlyTempReaderAfterButtonClick;
        private System.Windows.Forms.CheckBox chkPrepayCardWaitWhenOut;
        private System.Windows.Forms.CheckBox chkMonthCardWaitWhenOut;
        private System.Windows.Forms.CheckBox chkCardValidNeedResponse;
        private System.Windows.Forms.CheckBox chkNoReaderOnCardCaptuer;
        private System.Windows.Forms.CheckBox chkEnableParkvacantLed;
        private System.Windows.Forms.CheckBox chkNoParkingCount;
        private System.Windows.Forms.CheckBox chkUseAsACS;
        private System.Windows.Forms.GroupBox groupBox2;
        private Ralid.GeneralLibrary.WinformControl.UCIPTextBox txtCarPlateIP;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtVideoID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private GeneralLibrary.WinformControl.ComPortComboBox cmbTicketReader;
        private GeneralLibrary.WinformControl.ComPortComboBox cmbTicketReader2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkForbidWhenFull;
        private System.Windows.Forms.CheckBox chkForbidWhenCardExpired;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}