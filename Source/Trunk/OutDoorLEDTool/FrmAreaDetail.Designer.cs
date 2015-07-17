namespace OutDoorLEDTool
{
    partial class FrmAreaDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAreaDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.txtVacant = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtCarPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.comVacantColor = new System.Windows.Forms.ComboBox();
            this.comFullColor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.Name = "comCardType";
            // 
            // txtVacant
            // 
            resources.ApplyResources(this.txtVacant, "txtVacant");
            this.txtVacant.MaxValue = 2147483647;
            this.txtVacant.MinValue = 0;
            this.txtVacant.Name = "txtVacant";
            // 
            // txtCarPort
            // 
            resources.ApplyResources(this.txtCarPort, "txtCarPort");
            this.txtCarPort.MaxValue = 2147483647;
            this.txtCarPort.MinValue = 0;
            this.txtCarPort.Name = "txtCarPort";
            // 
            // comVacantColor
            // 
            this.comVacantColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comVacantColor.FormattingEnabled = true;
            this.comVacantColor.Items.AddRange(new object[] {
            resources.GetString("comVacantColor.Items"),
            resources.GetString("comVacantColor.Items1"),
            resources.GetString("comVacantColor.Items2")});
            resources.ApplyResources(this.comVacantColor, "comVacantColor");
            this.comVacantColor.Name = "comVacantColor";
            // 
            // comFullColor
            // 
            this.comFullColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comFullColor.FormattingEnabled = true;
            this.comFullColor.Items.AddRange(new object[] {
            resources.GetString("comFullColor.Items"),
            resources.GetString("comFullColor.Items1"),
            resources.GetString("comFullColor.Items2")});
            resources.ApplyResources(this.comFullColor, "comFullColor");
            this.comFullColor.Name = "comFullColor";
            // 
            // FrmAreaDetail
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comFullColor);
            this.Controls.Add(this.comVacantColor);
            this.Controls.Add(this.txtCarPort);
            this.Controls.Add(this.txtVacant);
            this.Controls.Add(this.comCardType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAreaDetail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtVacant;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtCarPort;
        private System.Windows.Forms.ComboBox comVacantColor;
        private System.Windows.Forms.ComboBox comFullColor;
    }
}