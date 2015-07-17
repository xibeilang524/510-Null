namespace Ralid.Park.UI
{
    partial class FrmAPMRefund
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAPMRefund));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSerialNumber = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.comAPM = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtTurnbackMoney = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txtMemo = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.lblTotalPaidFee = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblParkFee = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPaidDateTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEnterDateTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtSerialNumber);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comAPM);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.txtTurnbackMoney);
            this.groupBox1.Controls.Add(this.txtMemo);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCardID);
            this.groupBox1.Controls.Add(this.lblTotalPaidFee);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblParkFee);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblPaidDateTime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblEnterDateTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblCardType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtSerialNumber
            // 
            resources.ApplyResources(this.txtSerialNumber, "txtSerialNumber");
            this.txtSerialNumber.Name = "txtSerialNumber";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // comAPM
            // 
            this.comAPM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comAPM.FormattingEnabled = true;
            resources.ApplyResources(this.comAPM, "comAPM");
            this.comAPM.Name = "comAPM";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtTurnbackMoney
            // 
            resources.ApplyResources(this.txtTurnbackMoney, "txtTurnbackMoney");
            this.txtTurnbackMoney.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtTurnbackMoney.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTurnbackMoney.Name = "txtTurnbackMoney";
            this.txtTurnbackMoney.NumberWithCommas = true;
            this.txtTurnbackMoney.PointCount = 2;
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.Name = "txtMemo";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label12.Name = "label12";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ralid.Park.UI.Properties.Resources.CardReader;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.TextChanged += new System.EventHandler(this.txtCardID_TextChanged);
            // 
            // lblTotalPaidFee
            // 
            resources.ApplyResources(this.lblTotalPaidFee, "lblTotalPaidFee");
            this.lblTotalPaidFee.Name = "lblTotalPaidFee";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // lblParkFee
            // 
            resources.ApplyResources(this.lblParkFee, "lblParkFee");
            this.lblParkFee.Name = "lblParkFee";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lblPaidDateTime
            // 
            resources.ApplyResources(this.lblPaidDateTime, "lblPaidDateTime");
            this.lblPaidDateTime.Name = "lblPaidDateTime";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lblEnterDateTime
            // 
            resources.ApplyResources(this.lblEnterDateTime, "lblEnterDateTime");
            this.lblEnterDateTime.Name = "lblEnterDateTime";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblCardType
            // 
            resources.ApplyResources(this.lblCardType, "lblCardType");
            this.lblCardType.Name = "lblCardType";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // FrmAPMRefund
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "FrmAPMRefund";
            this.Activated += new System.EventHandler(this.FrmAPMRefund_Activated);
            this.Deactivate += new System.EventHandler(this.FrmAPMRefund_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAPMRefund_FormClosed);
            this.Load += new System.EventHandler(this.FrmAPMRefund_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAPMRefund_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblPaidDateTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblEnterDateTime;
        private System.Windows.Forms.Label lblParkFee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalPaidFee;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private GeneralLibrary.WinformControl.DecimalTextBox txtTurnbackMoney;
        private GeneralLibrary.WinformControl.DBCTextBox txtMemo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comAPM;
        private GeneralLibrary.WinformControl.DBCTextBox txtSerialNumber;
        private System.Windows.Forms.Label label5;
    }
}