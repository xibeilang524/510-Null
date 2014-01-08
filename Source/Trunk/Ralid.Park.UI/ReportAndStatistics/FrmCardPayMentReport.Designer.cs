namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardPaymentReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardPaymentReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comPaymentCode = new Ralid.Park.UserControls.PaymentCodeComboBox(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.chkCenterCharge = new System.Windows.Forms.CheckBox();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.carTypeComboBox1 = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdSettled = new System.Windows.Forms.RadioButton();
            this.rdBoth = new System.Windows.Forms.RadioButton();
            this.rdUnsettled = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdPaid = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdFree = new System.Windows.Forms.RadioButton();
            this.chkPaymentMode = new System.Windows.Forms.CheckBox();
            this.comPaymentMode = new Ralid.Park.UserControls.PaymentModeComboBox(this.components);
            this.workStationCombobox1 = new Ralid.Park.UserControls.StationNameComboBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.comOperator = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.txtOperatorCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.txtPaid = new System.Windows.Forms.Label();
            this.txtAccounts = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.Label();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExitDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnterDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeSpan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAccounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTariffType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHandled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSettleDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            // 
            // btnSaveAs
            // 
            resources.ApplyResources(this.btnSaveAs, "btnSaveAs");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ucDateTimeInterval1
            // 
            resources.ApplyResources(this.ucDateTimeInterval1, "ucDateTimeInterval1");
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 9, 23, 59, 59, 0);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 9, 16, 56, 56, 625);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.comPaymentCode);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.chkCenterCharge);
            this.groupBox3.Controls.Add(this.txtCertificate);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.carTypeComboBox1);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.chkPaymentMode);
            this.groupBox3.Controls.Add(this.comPaymentMode);
            this.groupBox3.Controls.Add(this.workStationCombobox1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtCarPlate);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comCardType);
            this.groupBox3.Controls.Add(this.comOperator);
            this.groupBox3.Controls.Add(this.txtOperatorCardID);
            this.groupBox3.Controls.Add(this.txtCardID);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // comPaymentCode
            // 
            resources.ApplyResources(this.comPaymentCode, "comPaymentCode");
            this.comPaymentCode.FormattingEnabled = true;
            this.comPaymentCode.Name = "comPaymentCode";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // chkCenterCharge
            // 
            resources.ApplyResources(this.chkCenterCharge, "chkCenterCharge");
            this.chkCenterCharge.Name = "chkCenterCharge";
            this.chkCenterCharge.UseVisualStyleBackColor = true;
            // 
            // txtCertificate
            // 
            resources.ApplyResources(this.txtCertificate, "txtCertificate");
            this.txtCertificate.Name = "txtCertificate";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // carTypeComboBox1
            // 
            resources.ApplyResources(this.carTypeComboBox1, "carTypeComboBox1");
            this.carTypeComboBox1.FormattingEnabled = true;
            this.carTypeComboBox1.Name = "carTypeComboBox1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rdSettled);
            this.panel2.Controls.Add(this.rdBoth);
            this.panel2.Controls.Add(this.rdUnsettled);
            this.panel2.Name = "panel2";
            // 
            // rdSettled
            // 
            resources.ApplyResources(this.rdSettled, "rdSettled");
            this.rdSettled.Name = "rdSettled";
            this.rdSettled.UseVisualStyleBackColor = true;
            // 
            // rdBoth
            // 
            resources.ApplyResources(this.rdBoth, "rdBoth");
            this.rdBoth.Checked = true;
            this.rdBoth.Name = "rdBoth";
            this.rdBoth.TabStop = true;
            this.rdBoth.UseVisualStyleBackColor = true;
            // 
            // rdUnsettled
            // 
            resources.ApplyResources(this.rdUnsettled, "rdUnsettled");
            this.rdUnsettled.Name = "rdUnsettled";
            this.rdUnsettled.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rdPaid);
            this.panel1.Controls.Add(this.rdAll);
            this.panel1.Controls.Add(this.rdFree);
            this.panel1.Name = "panel1";
            // 
            // rdPaid
            // 
            resources.ApplyResources(this.rdPaid, "rdPaid");
            this.rdPaid.Name = "rdPaid";
            this.rdPaid.UseVisualStyleBackColor = true;
            // 
            // rdAll
            // 
            resources.ApplyResources(this.rdAll, "rdAll");
            this.rdAll.Checked = true;
            this.rdAll.Name = "rdAll";
            this.rdAll.TabStop = true;
            this.rdAll.UseVisualStyleBackColor = true;
            // 
            // rdFree
            // 
            resources.ApplyResources(this.rdFree, "rdFree");
            this.rdFree.Name = "rdFree";
            this.rdFree.UseVisualStyleBackColor = true;
            // 
            // chkPaymentMode
            // 
            resources.ApplyResources(this.chkPaymentMode, "chkPaymentMode");
            this.chkPaymentMode.Name = "chkPaymentMode";
            this.chkPaymentMode.UseVisualStyleBackColor = true;
            // 
            // comPaymentMode
            // 
            resources.ApplyResources(this.comPaymentMode, "comPaymentMode");
            this.comPaymentMode.FormattingEnabled = true;
            this.comPaymentMode.Name = "comPaymentMode";
            // 
            // workStationCombobox1
            // 
            resources.ApplyResources(this.workStationCombobox1, "workStationCombobox1");
            this.workStationCombobox1.FormattingEnabled = true;
            this.workStationCombobox1.Name = "workStationCombobox1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
            // 
            // comOperator
            // 
            resources.ApplyResources(this.comOperator, "comOperator");
            this.comOperator.FormattingEnabled = true;
            this.comOperator.Name = "comOperator";
            // 
            // txtOperatorCardID
            // 
            resources.ApplyResources(this.txtOperatorCardID, "txtOperatorCardID");
            this.txtOperatorCardID.Name = "txtOperatorCardID";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // customDataGridView1
            // 
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colCardCertificate,
            this.colCarPlate,
            this.colCardType,
            this.colCarType,
            this.colExitDateTime,
            this.colEnterDateTime,
            this.colTimeSpan,
            this.colTotalAccounts,
            this.colPaid,
            this.colDiscount,
            this.colTariffType,
            this.colPaymentMode,
            this.colHandled,
            this.colSettleDateTime,
            this.colTotalPaid,
            this.colTotalDiscount,
            this.colMemo,
            this.colOperator,
            this.colOperatorCardID,
            this.colPaymentCode,
            this.colStation});
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridView1_CellDoubleClick);
            // 
            // txtPaid
            // 
            resources.ApplyResources(this.txtPaid, "txtPaid");
            this.txtPaid.ForeColor = System.Drawing.Color.Blue;
            this.txtPaid.Name = "txtPaid";
            // 
            // txtAccounts
            // 
            resources.ApplyResources(this.txtAccounts, "txtAccounts");
            this.txtAccounts.ForeColor = System.Drawing.Color.Red;
            this.txtAccounts.Name = "txtAccounts";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtTotal
            // 
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.ForeColor = System.Drawing.Color.Black;
            this.txtTotal.Name = "txtTotal";
            // 
            // colCardID
            // 
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colCardCertificate
            // 
            resources.ApplyResources(this.colCardCertificate, "colCardCertificate");
            this.colCardCertificate.Name = "colCardCertificate";
            this.colCardCertificate.ReadOnly = true;
            // 
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colCardType
            // 
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colCarType
            // 
            resources.ApplyResources(this.colCarType, "colCarType");
            this.colCarType.Name = "colCarType";
            this.colCarType.ReadOnly = true;
            // 
            // colExitDateTime
            // 
            resources.ApplyResources(this.colExitDateTime, "colExitDateTime");
            this.colExitDateTime.Name = "colExitDateTime";
            this.colExitDateTime.ReadOnly = true;
            // 
            // colEnterDateTime
            // 
            resources.ApplyResources(this.colEnterDateTime, "colEnterDateTime");
            this.colEnterDateTime.Name = "colEnterDateTime";
            this.colEnterDateTime.ReadOnly = true;
            // 
            // colTimeSpan
            // 
            resources.ApplyResources(this.colTimeSpan, "colTimeSpan");
            this.colTimeSpan.Name = "colTimeSpan";
            this.colTimeSpan.ReadOnly = true;
            // 
            // colTotalAccounts
            // 
            resources.ApplyResources(this.colTotalAccounts, "colTotalAccounts");
            this.colTotalAccounts.Name = "colTotalAccounts";
            this.colTotalAccounts.ReadOnly = true;
            // 
            // colPaid
            // 
            resources.ApplyResources(this.colPaid, "colPaid");
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            // 
            // colDiscount
            // 
            resources.ApplyResources(this.colDiscount, "colDiscount");
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            // 
            // colTariffType
            // 
            resources.ApplyResources(this.colTariffType, "colTariffType");
            this.colTariffType.Name = "colTariffType";
            this.colTariffType.ReadOnly = true;
            // 
            // colPaymentMode
            // 
            resources.ApplyResources(this.colPaymentMode, "colPaymentMode");
            this.colPaymentMode.Name = "colPaymentMode";
            this.colPaymentMode.ReadOnly = true;
            // 
            // colHandled
            // 
            resources.ApplyResources(this.colHandled, "colHandled");
            this.colHandled.Name = "colHandled";
            this.colHandled.ReadOnly = true;
            this.colHandled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSettleDateTime
            // 
            resources.ApplyResources(this.colSettleDateTime, "colSettleDateTime");
            this.colSettleDateTime.Name = "colSettleDateTime";
            this.colSettleDateTime.ReadOnly = true;
            // 
            // colTotalPaid
            // 
            resources.ApplyResources(this.colTotalPaid, "colTotalPaid");
            this.colTotalPaid.Name = "colTotalPaid";
            this.colTotalPaid.ReadOnly = true;
            // 
            // colTotalDiscount
            // 
            resources.ApplyResources(this.colTotalDiscount, "colTotalDiscount");
            this.colTotalDiscount.Name = "colTotalDiscount";
            this.colTotalDiscount.ReadOnly = true;
            // 
            // colMemo
            // 
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // colOperator
            // 
            resources.ApplyResources(this.colOperator, "colOperator");
            this.colOperator.Name = "colOperator";
            this.colOperator.ReadOnly = true;
            // 
            // colOperatorCardID
            // 
            resources.ApplyResources(this.colOperatorCardID, "colOperatorCardID");
            this.colOperatorCardID.Name = "colOperatorCardID";
            this.colOperatorCardID.ReadOnly = true;
            // 
            // colPaymentCode
            // 
            resources.ApplyResources(this.colPaymentCode, "colPaymentCode");
            this.colPaymentCode.Name = "colPaymentCode";
            this.colPaymentCode.ReadOnly = true;
            // 
            // colStation
            // 
            this.colStation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colStation, "colStation");
            this.colStation.Name = "colStation";
            this.colStation.ReadOnly = true;
            // 
            // FrmCardPaymentReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtAccounts);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtPaid);
            this.Name = "FrmCardPaymentReport";
            this.Load += new System.EventHandler(this.FrmParkingPayDetailReport_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.txtPaid, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.txtAccounts, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox3;
        private Ralid.Park.UserControls.OperatorComboBox comOperator;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label7;
        private Ralid.Park.UserControls.PaymentModeComboBox comPaymentMode;
        private System.Windows.Forms.CheckBox chkPaymentMode;
        private Ralid.Park.UserControls.StationNameComboBox workStationCombobox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdPaid;
        private System.Windows.Forms.RadioButton rdFree;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.Label txtPaid;
        private System.Windows.Forms.Label txtAccounts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdSettled;
        private System.Windows.Forms.RadioButton rdBoth;
        private System.Windows.Forms.RadioButton rdUnsettled;
        private System.Windows.Forms.Panel panel1;
        private UserControls.CarTypeComboBox carTypeComboBox1;
        private System.Windows.Forms.Label label4;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkCenterCharge;
        private UserControls.PaymentCodeComboBox comPaymentCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private GeneralLibrary.WinformControl.DBCTextBox txtOperatorCardID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExitDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnterDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeSpan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTariffType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHandled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSettleDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
    }
}