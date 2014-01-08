namespace Ralid.Park.UI
{
    partial class FrmOperatorCardCashComfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperatorCardCashComfirm));
            this.btnClose = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblMsg
            // 
            resources.ApplyResources(this.lblMsg, "lblMsg");
            this.lblMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblMsg.Name = "lblMsg";
            // 
            // FrmOperatorCardCashComfirm
            // 
            this.AcceptButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOperatorCardCashComfirm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOperatorCardCashComfirm_FormClosing);
            this.Load += new System.EventHandler(this.FrmOperatorCardCashComfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMsg;
    }
}