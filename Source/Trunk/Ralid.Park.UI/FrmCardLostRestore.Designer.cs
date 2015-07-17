namespace Ralid.Park.UI
{
    partial class FrmCardLostRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardLostRestore));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.parkCombobox1 = new Ralid.Park.UserControls.ParkCombobox(this.components);
            this.lblParkFee = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtParkFee = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.chkPayParkFee = new System.Windows.Forms.CheckBox();
            this.comPaymentMode = new Ralid.Park.UserControls.PaymentModeComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtCardCost = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtMemo = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
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
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.parkCombobox1);
            this.groupBox1.Controls.Add(this.lblParkFee);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtParkFee);
            this.groupBox1.Controls.Add(this.chkPayParkFee);
            this.groupBox1.Controls.Add(this.comPaymentMode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCardCost);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMemo);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // parkCombobox1
            // 
            this.parkCombobox1.FormattingEnabled = true;
            resources.ApplyResources(this.parkCombobox1, "parkCombobox1");
            this.parkCombobox1.Name = "parkCombobox1";
            this.parkCombobox1.SelectedIndexChanged += new System.EventHandler(this.parkCombobox1_SelectedIndexChanged);
            // 
            // lblParkFee
            // 
            resources.ApplyResources(this.lblParkFee, "lblParkFee");
            this.lblParkFee.ForeColor = System.Drawing.Color.Blue;
            this.lblParkFee.Name = "lblParkFee";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Name = "label4";
            // 
            // txtParkFee
            // 
            resources.ApplyResources(this.txtParkFee, "txtParkFee");
            this.txtParkFee.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtParkFee.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtParkFee.Name = "txtParkFee";
            this.txtParkFee.NumberWithCommas = true;
            this.txtParkFee.PointCount = 2;
            this.txtParkFee.TextChanged += new System.EventHandler(this.txtParkFee_TextChanged);
            // 
            // chkPayParkFee
            // 
            resources.ApplyResources(this.chkPayParkFee, "chkPayParkFee");
            this.chkPayParkFee.Name = "chkPayParkFee";
            this.chkPayParkFee.UseVisualStyleBackColor = true;
            this.chkPayParkFee.CheckedChanged += new System.EventHandler(this.chkPayParkFee_CheckedChanged);
            // 
            // comPaymentMode
            // 
            this.comPaymentMode.FormattingEnabled = true;
            resources.ApplyResources(this.comPaymentMode, "comPaymentMode");
            this.comPaymentMode.Name = "comPaymentMode";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtCardCost
            // 
            resources.ApplyResources(this.txtCardCost, "txtCardCost");
            this.txtCardCost.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtCardCost.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCardCost.Name = "txtCardCost";
            this.txtCardCost.NumberWithCommas = true;
            this.txtCardCost.PointCount = 2;
            this.txtCardCost.TextChanged += new System.EventHandler(this.txtCardCost_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.Name = "txtMemo";
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
            // FrmCardLostRestore
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkWriteCard);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ucCardInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCardLostRestore";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardLostRestore_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardLostRestore_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.UCCard ucCardInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtMemo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private GeneralLibrary.WinformControl.DecimalTextBox txtCardCost;
        private System.Windows.Forms.Label label3;
        private UserControls.PaymentModeComboBox comPaymentMode;
        private System.Windows.Forms.CheckBox chkPayParkFee;
        private GeneralLibrary.WinformControl.DecimalTextBox txtParkFee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblParkFee;
        private System.Windows.Forms.CheckBox chkWriteCard;
        private System.Windows.Forms.Label label5;
        private UserControls.ParkCombobox parkCombobox1;
    }
}