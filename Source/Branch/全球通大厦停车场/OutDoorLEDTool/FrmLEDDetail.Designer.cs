namespace OutDoorLEDTool
{
    partial class FrmLEDDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLEDDetail));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comCommport = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.comBaud = new System.Windows.Forms.ComboBox();
            this.txtCarAddress = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtMotorAddress = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtBrightness = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comCommport
            // 
            this.comCommport.FormattingEnabled = true;
            resources.ApplyResources(this.comCommport, "comCommport");
            this.comCommport.Name = "comCommport";
            // 
            // comBaud
            // 
            this.comBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBaud.FormattingEnabled = true;
            this.comBaud.Items.AddRange(new object[] {
            resources.GetString("comBaud.Items"),
            resources.GetString("comBaud.Items1")});
            resources.ApplyResources(this.comBaud, "comBaud");
            this.comBaud.Name = "comBaud";
            // 
            // txtCarAddress
            // 
            resources.ApplyResources(this.txtCarAddress, "txtCarAddress");
            this.txtCarAddress.MaxValue = 1000;
            this.txtCarAddress.MinValue = 0;
            this.txtCarAddress.Name = "txtCarAddress";
            // 
            // txtMotorAddress
            // 
            resources.ApplyResources(this.txtMotorAddress, "txtMotorAddress");
            this.txtMotorAddress.MaxValue = 1000;
            this.txtMotorAddress.MinValue = 0;
            this.txtMotorAddress.Name = "txtMotorAddress";
            // 
            // txtBrightness
            // 
            resources.ApplyResources(this.txtBrightness, "txtBrightness");
            this.txtBrightness.MaxValue = 15;
            this.txtBrightness.MinValue = 0;
            this.txtBrightness.Name = "txtBrightness";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // FrmLEDDetail
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.txtBrightness);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMotorAddress);
            this.Controls.Add(this.txtCarAddress);
            this.Controls.Add(this.comBaud);
            this.Controls.Add(this.comCommport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLEDDetail";
            this.Load += new System.EventHandler(this.FrmLEDDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Ralid.GeneralLibrary.WinformControl.ComPortComboBox comCommport;
        private System.Windows.Forms.ComboBox comBaud;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtCarAddress;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtMotorAddress;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtBrightness;
        private System.Windows.Forms.Label label5;
    }
}