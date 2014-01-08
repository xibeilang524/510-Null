namespace Ralid.Park.UI
{
    partial class FrmVideoSourceDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVideoSourceDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtMediaSource = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtUserName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtPassword = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.chkForCarPlate = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtChannel = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtControlPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtStreamPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtConnectTimeOut = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label6.Name = "label6";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // txtMediaSource
            // 
            resources.ApplyResources(this.txtMediaSource, "txtMediaSource");
            this.txtMediaSource.Name = "txtMediaSource";
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // chkForCarPlate
            // 
            resources.ApplyResources(this.chkForCarPlate, "chkForCarPlate");
            this.chkForCarPlate.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkForCarPlate.Name = "chkForCarPlate";
            this.chkForCarPlate.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label8.Name = "label8";
            // 
            // txtChannel
            // 
            resources.ApplyResources(this.txtChannel, "txtChannel");
            this.txtChannel.MaxValue = 100;
            this.txtChannel.MinValue = 0;
            this.txtChannel.Name = "txtChannel";
            // 
            // txtControlPort
            // 
            resources.ApplyResources(this.txtControlPort, "txtControlPort");
            this.txtControlPort.MaxValue = 65535;
            this.txtControlPort.MinValue = 0;
            this.txtControlPort.Name = "txtControlPort";
            // 
            // txtStreamPort
            // 
            resources.ApplyResources(this.txtStreamPort, "txtStreamPort");
            this.txtStreamPort.MaxValue = 65535;
            this.txtStreamPort.MinValue = 0;
            this.txtStreamPort.Name = "txtStreamPort";
            // 
            // txtConnectTimeOut
            // 
            resources.ApplyResources(this.txtConnectTimeOut, "txtConnectTimeOut");
            this.txtConnectTimeOut.MaxValue = 100;
            this.txtConnectTimeOut.MinValue = 0;
            this.txtConnectTimeOut.Name = "txtConnectTimeOut";
            // 
            // FrmVideoSourceDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtConnectTimeOut);
            this.Controls.Add(this.txtStreamPort);
            this.Controls.Add(this.txtControlPort);
            this.Controls.Add(this.txtChannel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkForCarPlate);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtMediaSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVideoSourceDetail";
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtMediaSource, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.chkForCarPlate, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtChannel, 0);
            this.Controls.SetChildIndex(this.txtControlPort, 0);
            this.Controls.SetChildIndex(this.txtStreamPort, 0);
            this.Controls.SetChildIndex(this.txtConnectTimeOut, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtName;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtMediaSource;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtUserName;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPassword;
        private System.Windows.Forms.CheckBox chkForCarPlate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Ralid.GeneralLibrary .WinformControl .IntergerTextBox txtChannel;
        private GeneralLibrary.WinformControl.IntergerTextBox txtControlPort;
        private GeneralLibrary.WinformControl.IntergerTextBox txtStreamPort;
        private GeneralLibrary.WinformControl.IntergerTextBox txtConnectTimeOut;
    }
}