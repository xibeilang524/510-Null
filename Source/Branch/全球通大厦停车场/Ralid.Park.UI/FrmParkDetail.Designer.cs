namespace Ralid.Park.UI
{
    partial class FrmParkDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParkDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtParkName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtMaxPortCount = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtVacant = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comWorkStation = new Ralid.Park.UserControls.StationIDCombobox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtVacantText = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.comPort = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.txtParkFullText = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.chkIsNested = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comDeviceType = new System.Windows.Forms.ComboBox();
            this.comWorkMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.Name = "label2";
            // 
            // txtParkName
            // 
            resources.ApplyResources(this.txtParkName, "txtParkName");
            this.txtParkName.Name = "txtParkName";
            // 
            // txtMaxPortCount
            // 
            resources.ApplyResources(this.txtMaxPortCount, "txtMaxPortCount");
            this.txtMaxPortCount.MaxValue = 32767;
            this.txtMaxPortCount.MinValue = 0;
            this.txtMaxPortCount.Name = "txtMaxPortCount";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label4.Name = "label4";
            // 
            // txtVacant
            // 
            resources.ApplyResources(this.txtVacant, "txtVacant");
            this.txtVacant.MaxValue = 32767;
            this.txtVacant.MinValue = 0;
            this.txtVacant.Name = "txtVacant";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label5.Name = "label5";
            // 
            // comWorkStation
            // 
            resources.ApplyResources(this.comWorkStation, "comWorkStation");
            this.comWorkStation.FormattingEnabled = true;
            this.comWorkStation.Name = "comWorkStation";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label6.Name = "label6";
            // 
            // txtVacantText
            // 
            resources.ApplyResources(this.txtVacantText, "txtVacantText");
            this.txtVacantText.Name = "txtVacantText";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label7.Name = "label7";
            // 
            // comPort
            // 
            resources.ApplyResources(this.comPort, "comPort");
            this.comPort.FormattingEnabled = true;
            this.comPort.Name = "comPort";
            // 
            // txtParkFullText
            // 
            resources.ApplyResources(this.txtParkFullText, "txtParkFullText");
            this.txtParkFullText.Name = "txtParkFullText";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label3.Name = "label3";
            // 
            // chkIsNested
            // 
            resources.ApplyResources(this.chkIsNested, "chkIsNested");
            this.chkIsNested.Checked = true;
            this.chkIsNested.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsNested.Name = "chkIsNested";
            this.chkIsNested.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label9.Name = "label9";
            // 
            // comDeviceType
            // 
            resources.ApplyResources(this.comDeviceType, "comDeviceType");
            this.comDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDeviceType.FormattingEnabled = true;
            this.comDeviceType.Name = "comDeviceType";
            this.comDeviceType.SelectedIndexChanged += new System.EventHandler(this.comDeviceType_SelectedIndexChanged);
            // 
            // comWorkMode
            // 
            resources.ApplyResources(this.comWorkMode, "comWorkMode");
            this.comWorkMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comWorkMode.FormattingEnabled = true;
            this.comWorkMode.Name = "comWorkMode";
            // 
            // FrmParkDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comWorkMode);
            this.Controls.Add(this.comDeviceType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtParkFullText);
            this.Controls.Add(this.chkIsNested);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtVacantText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comWorkStation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtVacant);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaxPortCount);
            this.Controls.Add(this.txtParkName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmParkDetail";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtParkName, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.txtMaxPortCount, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtVacant, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.comWorkStation, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtVacantText, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.comPort, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.chkIsNested, 0);
            this.Controls.SetChildIndex(this.txtParkFullText, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.comDeviceType, 0);
            this.Controls.SetChildIndex(this.comWorkMode, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtParkName;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtMaxPortCount;
        private System.Windows.Forms.Label label4;
        private Ralid.GeneralLibrary .WinformControl .IntergerTextBox txtVacant;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private Ralid.Park.UserControls.StationIDCombobox comWorkStation;
        private System.Windows.Forms.Label label6;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtVacantText;
        private System.Windows.Forms.Label label7;
        private GeneralLibrary.WinformControl.ComPortComboBox comPort;
        private GeneralLibrary.WinformControl.DBCTextBox txtParkFullText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIsNested;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comDeviceType;
        private System.Windows.Forms.ComboBox comWorkMode;
    }
}