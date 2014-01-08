namespace Ralid.Park.UI
{
    partial class FrmParkDeviceIP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParkDeviceIP));
            this.btnSet = new System.Windows.Forms.Button();
            this.txtIPMask = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.txtGateWay = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.txtIPAddress = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            resources.ApplyResources(this.btnSet, "btnSet");
            this.btnSet.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnSet.Name = "btnSet";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
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
            // txtIPAddress
            // 
            resources.ApplyResources(this.txtIPAddress, "txtIPAddress");
            this.txtIPAddress.Name = "txtIPAddress";
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
            // btnGet
            // 
            resources.ApplyResources(this.btnGet, "btnGet");
            this.btnGet.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnGet.Name = "btnGet";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label1.Name = "label1";
            // 
            // FrmParkDeviceIP
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtIPMask);
            this.Controls.Add(this.txtGateWay);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmParkDeviceIP";
            this.Load += new System.EventHandler(this.FrmEntranceDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSet;
        private GeneralLibrary.WinformControl.UCIPTextBox txtIPMask;
        private GeneralLibrary.WinformControl.UCIPTextBox txtGateWay;
        private GeneralLibrary.WinformControl.UCIPTextBox txtIPAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label label1;

    }
}