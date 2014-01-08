namespace Ralid.Park.UI
{
    partial class FrmCardCharge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardCharge));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comPaymentMode = new Ralid.Park.UserControls.PaymentModeComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.dtValidDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMemo = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtRecieveMoney = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtChargeAmount = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucCardInfo = new Ralid.Park.UserControls.UCCard();
            this.chkWriteCard = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comPaymentMode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtValidDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMemo);
            this.groupBox1.Controls.Add(this.txtRecieveMoney);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtChargeAmount);
            this.groupBox1.Controls.Add(this.label2);
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // dtValidDate
            // 
            resources.ApplyResources(this.dtValidDate, "dtValidDate");
            this.dtValidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtValidDate.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtValidDate.MinDate = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            this.dtValidDate.Name = "dtValidDate";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.Name = "txtMemo";
            // 
            // txtRecieveMoney
            // 
            resources.ApplyResources(this.txtRecieveMoney, "txtRecieveMoney");
            this.txtRecieveMoney.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtRecieveMoney.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtRecieveMoney.Name = "txtRecieveMoney";
            this.txtRecieveMoney.PointCount = 2;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtChargeAmount
            // 
            resources.ApplyResources(this.txtChargeAmount, "txtChargeAmount");
            this.txtChargeAmount.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtChargeAmount.MinValue = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.txtChargeAmount.Name = "txtChargeAmount";
            this.txtChargeAmount.PointCount = 2;
            this.txtChargeAmount.TextChanged += new System.EventHandler(this.txtChargeAmount_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ucCardInfo
            // 
            resources.ApplyResources(this.ucCardInfo, "ucCardInfo");
            this.ucCardInfo.Name = "ucCardInfo";
            // 
            // chkWriteCard
            // 
            resources.ApplyResources(this.chkWriteCard, "chkWriteCard");
            this.chkWriteCard.Name = "chkWriteCard";
            this.chkWriteCard.UseVisualStyleBackColor = true;
            // 
            // FrmCardCharge
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.chkWriteCard);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ucCardInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCardCharge";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardCharge_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardCharge_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.UCCard ucCardInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtMemo;
        private Ralid.GeneralLibrary .WinformControl .DecimalTextBox txtRecieveMoney;
        private Ralid.GeneralLibrary .WinformControl .DecimalTextBox txtChargeAmount;
        private System.Windows.Forms.DateTimePicker dtValidDate;
        private System.Windows.Forms.Label label4;
        private Ralid.Park.UserControls.PaymentModeComboBox comPaymentMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkWriteCard;

    }
}