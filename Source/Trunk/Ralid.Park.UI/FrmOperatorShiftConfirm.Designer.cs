namespace Ralid.Park.UI
{
    partial class FrmOperatorShiftConfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperatorShiftConfirm));
            this.label23 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.txtCashInherit = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txtTempCardInherit = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.SuspendLayout();
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label23.Name = "label23";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label12.Name = "label12";
            // 
            // butOK
            // 
            this.butOK.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            resources.ApplyResources(this.butOK, "butOK");
            this.butOK.Name = "butOK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // txtCashInherit
            // 
            this.txtCashInherit.BackColor = System.Drawing.SystemColors.HighlightText;
            resources.ApplyResources(this.txtCashInherit, "txtCashInherit");
            this.txtCashInherit.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtCashInherit.MinValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147352576});
            this.txtCashInherit.Name = "txtCashInherit";
            this.txtCashInherit.NumberWithCommas = true;
            this.txtCashInherit.PointCount = 2;
            // 
            // txtTempCardInherit
            // 
            this.txtTempCardInherit.BackColor = System.Drawing.SystemColors.HighlightText;
            resources.ApplyResources(this.txtTempCardInherit, "txtTempCardInherit");
            this.txtTempCardInherit.MaxValue = 99999999;
            this.txtTempCardInherit.MinValue = -99999999;
            this.txtTempCardInherit.Name = "txtTempCardInherit";
            this.txtTempCardInherit.NumberWithCommas = true;
            // 
            // FrmOperatorShiftConfirm
            // 
            this.AcceptButton = this.butOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.txtCashInherit);
            this.Controls.Add(this.txtTempCardInherit);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOperatorShiftConfirm";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.GeneralLibrary .WinformControl .DecimalTextBox txtCashInherit;
        private Ralid.GeneralLibrary .WinformControl .IntergerTextBox txtTempCardInherit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button butOK;
    }
}