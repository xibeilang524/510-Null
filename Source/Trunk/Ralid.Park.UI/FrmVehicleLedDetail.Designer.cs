namespace Ralid.Park.UI
{
    partial class FrmVehicleLedDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVehicleLedDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comPort = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.comSubAddress1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comSubAddress2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comSubAddress3 = new System.Windows.Forms.ComboBox();
            this.txtMemo = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkShowTitle = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSubInterval1 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtSubInterval2 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtSubInterval3 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.chkEnabledInterval = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubTitle1 = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.txtSubTitle2 = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.txtSubTitle3 = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.ucEntrance = new Ralid.Park.UserControls.UCEntrance();
            this.comSubMessage3 = new Ralid.Park.UserControls.VehicleLEDMessageTypeCombobox(this.components);
            this.comSubMessage2 = new Ralid.Park.UserControls.VehicleLEDMessageTypeCombobox(this.components);
            this.comSubMessage1 = new Ralid.Park.UserControls.VehicleLEDMessageTypeCombobox(this.components);
            this.comStation = new Ralid.Park.UserControls.StationIDCombobox(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comPort
            // 
            this.comPort.FormattingEnabled = true;
            resources.ApplyResources(this.comPort, "comPort");
            this.comPort.Name = "comPort";
            // 
            // comSubAddress1
            // 
            this.comSubAddress1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSubAddress1.FormattingEnabled = true;
            resources.ApplyResources(this.comSubAddress1, "comSubAddress1");
            this.comSubAddress1.Name = "comSubAddress1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // comSubAddress2
            // 
            this.comSubAddress2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSubAddress2.FormattingEnabled = true;
            resources.ApplyResources(this.comSubAddress2, "comSubAddress2");
            this.comSubAddress2.Name = "comSubAddress2";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comSubAddress3
            // 
            this.comSubAddress3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSubAddress3.FormattingEnabled = true;
            resources.ApplyResources(this.comSubAddress3, "comSubAddress3");
            this.comSubAddress3.Name = "comSubAddress3";
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.Name = "txtMemo";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkShowTitle
            // 
            resources.ApplyResources(this.chkShowTitle, "chkShowTitle");
            this.chkShowTitle.Name = "chkShowTitle";
            this.chkShowTitle.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtSubInterval1
            // 
            resources.ApplyResources(this.txtSubInterval1, "txtSubInterval1");
            this.txtSubInterval1.MaxValue = 100000;
            this.txtSubInterval1.MinValue = 0;
            this.txtSubInterval1.Name = "txtSubInterval1";
            // 
            // txtSubInterval2
            // 
            resources.ApplyResources(this.txtSubInterval2, "txtSubInterval2");
            this.txtSubInterval2.MaxValue = 100000;
            this.txtSubInterval2.MinValue = 0;
            this.txtSubInterval2.Name = "txtSubInterval2";
            // 
            // txtSubInterval3
            // 
            resources.ApplyResources(this.txtSubInterval3, "txtSubInterval3");
            this.txtSubInterval3.MaxValue = 100000;
            this.txtSubInterval3.MinValue = 0;
            this.txtSubInterval3.Name = "txtSubInterval3";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label15.Name = "label15";
            // 
            // chkEnabledInterval
            // 
            resources.ApplyResources(this.chkEnabledInterval, "chkEnabledInterval");
            this.chkEnabledInterval.Name = "chkEnabledInterval";
            this.chkEnabledInterval.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtSubTitle1
            // 
            resources.ApplyResources(this.txtSubTitle1, "txtSubTitle1");
            this.txtSubTitle1.Name = "txtSubTitle1";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // txtSubTitle2
            // 
            resources.ApplyResources(this.txtSubTitle2, "txtSubTitle2");
            this.txtSubTitle2.Name = "txtSubTitle2";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtSubTitle3
            // 
            resources.ApplyResources(this.txtSubTitle3, "txtSubTitle3");
            this.txtSubTitle3.Name = "txtSubTitle3";
            // 
            // ucEntrance
            // 
            resources.ApplyResources(this.ucEntrance, "ucEntrance");
            this.ucEntrance.Name = "ucEntrance";
            this.ucEntrance.OnlyExit = false;
            // 
            // comSubMessage3
            // 
            this.comSubMessage3.FormattingEnabled = true;
            resources.ApplyResources(this.comSubMessage3, "comSubMessage3");
            this.comSubMessage3.Name = "comSubMessage3";
            // 
            // comSubMessage2
            // 
            this.comSubMessage2.FormattingEnabled = true;
            resources.ApplyResources(this.comSubMessage2, "comSubMessage2");
            this.comSubMessage2.Name = "comSubMessage2";
            // 
            // comSubMessage1
            // 
            this.comSubMessage1.FormattingEnabled = true;
            resources.ApplyResources(this.comSubMessage1, "comSubMessage1");
            this.comSubMessage1.Name = "comSubMessage1";
            // 
            // comStation
            // 
            this.comStation.FormattingEnabled = true;
            resources.ApplyResources(this.comStation, "comStation");
            this.comStation.Name = "comStation";
            // 
            // FrmVehicleLedDetail
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.ucEntrance);
            this.Controls.Add(this.txtSubInterval3);
            this.Controls.Add(this.txtSubInterval2);
            this.Controls.Add(this.txtSubInterval1);
            this.Controls.Add(this.chkEnabledInterval);
            this.Controls.Add(this.chkShowTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSubTitle3);
            this.Controls.Add(this.txtSubTitle2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtSubTitle1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comSubMessage3);
            this.Controls.Add(this.comSubMessage2);
            this.Controls.Add(this.comSubMessage1);
            this.Controls.Add(this.comSubAddress3);
            this.Controls.Add(this.comSubAddress2);
            this.Controls.Add(this.comSubAddress1);
            this.Controls.Add(this.comPort);
            this.Controls.Add(this.comStation);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVehicleLedDetail";
            this.Load += new System.EventHandler(this.FrmVehicleLedDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.DBCTextBox txtName;
        private System.Windows.Forms.Label label3;
        private UserControls.StationIDCombobox comStation;
        private System.Windows.Forms.Label label4;
        private GeneralLibrary.WinformControl.ComPortComboBox comPort;
        private System.Windows.Forms.ComboBox comSubAddress1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private UserControls.VehicleLEDMessageTypeCombobox comSubMessage1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comSubAddress2;
        private UserControls.VehicleLEDMessageTypeCombobox comSubMessage2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comSubAddress3;
        private UserControls.VehicleLEDMessageTypeCombobox comSubMessage3;
        private GeneralLibrary.WinformControl.DBCTextBox txtMemo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkShowTitle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private GeneralLibrary.WinformControl.IntergerTextBox txtSubInterval1;
        private GeneralLibrary.WinformControl.IntergerTextBox txtSubInterval2;
        private GeneralLibrary.WinformControl.IntergerTextBox txtSubInterval3;
        private System.Windows.Forms.Label label15;
        private UserControls.UCEntrance ucEntrance;
        private System.Windows.Forms.CheckBox chkEnabledInterval;
        private System.Windows.Forms.Label label2;
        private GeneralLibrary.WinformControl.DBCTextBox txtSubTitle1;
        private System.Windows.Forms.Label label16;
        private GeneralLibrary.WinformControl.DBCTextBox txtSubTitle2;
        private System.Windows.Forms.Label label17;
        private GeneralLibrary.WinformControl.DBCTextBox txtSubTitle3;
    }
}