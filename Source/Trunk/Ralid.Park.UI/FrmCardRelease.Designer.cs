namespace Ralid.Park.UI
{
    partial class FrmCardRelease
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardRelease));
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comPaymentMode = new Ralid.Park.UserControls.PaymentModeComboBox(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.txtRecieveMoney = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txtMemo = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.ucCard1 = new Ralid.Park.UserControls.UCCard();
            this.chkWriteCard = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comPaymentMode);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtRecieveMoney);
            this.groupBox1.Controls.Add(this.txtMemo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // comPaymentMode
            // 
            this.comPaymentMode.FormattingEnabled = true;
            resources.ApplyResources(this.comPaymentMode, "comPaymentMode");
            this.comPaymentMode.Name = "comPaymentMode";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txtRecieveMoney
            // 
            resources.ApplyResources(this.txtRecieveMoney, "txtRecieveMoney");
            this.txtRecieveMoney.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtRecieveMoney.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRecieveMoney.Name = "txtRecieveMoney";
            this.txtRecieveMoney.NumberWithCommas = true;
            this.txtRecieveMoney.PointCount = 2;
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.Name = "txtMemo";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ucCard1
            // 
            resources.ApplyResources(this.ucCard1, "ucCard1");
            this.ucCard1.Name = "ucCard1";
            // 
            // chkWriteCard
            // 
            resources.ApplyResources(this.chkWriteCard, "chkWriteCard");
            this.chkWriteCard.Name = "chkWriteCard";
            this.chkWriteCard.UseVisualStyleBackColor = true;
            // 
            // FrmCardRelease
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.chkWriteCard);
            this.Controls.Add(this.ucCard1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCardRelease";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardRelease_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardRelease_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtMemo;
        private Ralid.GeneralLibrary .WinformControl .DecimalTextBox txtRecieveMoney;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label15;
        private Ralid.Park.UserControls.PaymentModeComboBox comPaymentMode;
        private UserControls.UCCard ucCard1;
        private System.Windows.Forms.CheckBox chkWriteCard;
    }
}