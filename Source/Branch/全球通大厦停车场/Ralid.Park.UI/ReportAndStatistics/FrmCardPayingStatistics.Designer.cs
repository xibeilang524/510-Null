namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardPayingStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardPayingStatistics));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtPerMonth = new System.Windows.Forms.RadioButton();
            this.rdPerDay = new System.Windows.Forms.RadioButton();
            this.rdPerHour = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comPaymentCode = new Ralid.Park.UserControls.PaymentCodeComboBox(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.carTypeComboBox1 = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.workStationCombobox1 = new Ralid.Park.UserControls.StationNameComboBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.chkPaymentMode = new System.Windows.Forms.CheckBox();
            this.comPaymentMode = new Ralid.Park.UserControls.PaymentModeComboBox(this.components);
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.comOperator = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.txtOperatorCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colChargeDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAccounts = new System.Windows.Forms.Label();
            this.txtPaid = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.rtPerMonth);
            this.groupBox3.Controls.Add(this.rdPerDay);
            this.groupBox3.Controls.Add(this.rdPerHour);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // rtPerMonth
            // 
            resources.ApplyResources(this.rtPerMonth, "rtPerMonth");
            this.rtPerMonth.Name = "rtPerMonth";
            this.rtPerMonth.UseVisualStyleBackColor = true;
            // 
            // rdPerDay
            // 
            resources.ApplyResources(this.rdPerDay, "rdPerDay");
            this.rdPerDay.Checked = true;
            this.rdPerDay.Name = "rdPerDay";
            this.rdPerDay.TabStop = true;
            this.rdPerDay.UseVisualStyleBackColor = true;
            // 
            // rdPerHour
            // 
            resources.ApplyResources(this.rdPerHour, "rdPerHour");
            this.rdPerHour.Name = "rdPerHour";
            this.rdPerHour.UseVisualStyleBackColor = true;
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
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.comPaymentCode);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.carTypeComboBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.workStationCombobox1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.chkPaymentMode);
            this.groupBox2.Controls.Add(this.comPaymentMode);
            this.groupBox2.Controls.Add(this.comCardType);
            this.groupBox2.Controls.Add(this.comOperator);
            this.groupBox2.Controls.Add(this.txtOperatorCardID);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // carTypeComboBox1
            // 
            resources.ApplyResources(this.carTypeComboBox1, "carTypeComboBox1");
            this.carTypeComboBox1.FormattingEnabled = true;
            this.carTypeComboBox1.Name = "carTypeComboBox1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            this.colChargeDateTime,
            this.colCount,
            this.colAccounts,
            this.colDiscount,
            this.colPaid,
            this.colMemo});
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.customDataGridView1.RowTemplate.Height = 23;
            // 
            // colChargeDateTime
            // 
            resources.ApplyResources(this.colChargeDateTime, "colChargeDateTime");
            this.colChargeDateTime.Name = "colChargeDateTime";
            this.colChargeDateTime.ReadOnly = true;
            // 
            // colCount
            // 
            resources.ApplyResources(this.colCount, "colCount");
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            // 
            // colAccounts
            // 
            resources.ApplyResources(this.colAccounts, "colAccounts");
            this.colAccounts.Name = "colAccounts";
            this.colAccounts.ReadOnly = true;
            // 
            // colDiscount
            // 
            resources.ApplyResources(this.colDiscount, "colDiscount");
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            // 
            // colPaid
            // 
            resources.ApplyResources(this.colPaid, "colPaid");
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtAccounts);
            this.groupBox4.Controls.Add(this.txtPaid);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtAccounts
            // 
            resources.ApplyResources(this.txtAccounts, "txtAccounts");
            this.txtAccounts.ForeColor = System.Drawing.Color.Red;
            this.txtAccounts.Name = "txtAccounts";
            // 
            // txtPaid
            // 
            resources.ApplyResources(this.txtPaid, "txtPaid");
            this.txtPaid.ForeColor = System.Drawing.Color.Blue;
            this.txtPaid.Name = "txtPaid";
            // 
            // FrmCardPayingStatistics
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCardPayingStatistics";
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rtPerMonth;
        private System.Windows.Forms.RadioButton rdPerDay;
        private System.Windows.Forms.RadioButton rdPerHour;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControls.StationNameComboBox workStationCombobox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkPaymentMode;
        private UserControls.PaymentModeComboBox comPaymentMode;
        private UserControls.CardTypeComboBox comCardType;
        private UserControls.OperatorComboBox comOperator;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private UserControls.CarTypeComboBox carTypeComboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private GeneralLibrary.WinformControl.DBCTextBox txtOperatorCardID;
        private UserControls.PaymentCodeComboBox comPaymentCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txtAccounts;
        private System.Windows.Forms.Label txtPaid;
    }
}