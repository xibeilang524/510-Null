namespace Ralid.Park.UI
{
    partial class FrmMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn16 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.ucMonitor = new Ralid.Park.UserControls.VideoPanels.UCVideoPanelGrid();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.btn16);
            this.panel1.Controls.Add(this.btn9);
            this.panel1.Controls.Add(this.btn4);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btn16
            // 
            this.btn16.BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this.btn16, "btn16");
            this.btn16.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btn16.Name = "btn16";
            this.btn16.UseVisualStyleBackColor = false;
            this.btn16.Click += new System.EventHandler(this.btnSixteen_Click);
            // 
            // btn9
            // 
            this.btn9.BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this.btn9, "btn9");
            this.btn9.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btn9.Name = "btn9";
            this.btn9.UseVisualStyleBackColor = false;
            this.btn9.Click += new System.EventHandler(this.btnNine_Click);
            // 
            // btn4
            // 
            this.btn4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this.btn4, "btn4");
            this.btn4.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btn4.Name = "btn4";
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.Click += new System.EventHandler(this.btnFour_Click);
            // 
            // ucMonitor
            // 
            resources.ApplyResources(this.ucMonitor, "ucMonitor");
            this.ucMonitor.Name = "ucMonitor";
            // 
            // FrmMonitor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.ucMonitor);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMonitor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMonitor_FormClosed);
            this.Load += new System.EventHandler(this.FrmMonitor_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn16;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn4;
        private UserControls.VideoPanels.UCVideoPanelGrid ucMonitor;



    }
}