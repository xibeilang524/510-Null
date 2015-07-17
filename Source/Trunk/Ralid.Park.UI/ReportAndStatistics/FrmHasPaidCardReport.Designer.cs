namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmHasPaidCardReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHasPaidCardReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeduction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBegin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.ComboBox();
            this.txtMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.txtMonthlyFee = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colOwnerName,
            this.colCardCertificate,
            this.colCardType,
            this.colCarPlate,
            this.colPaid,
            this.colDeduction,
            this.colAccount,
            this.colBalance,
            this.colBegin,
            this.colEnd});
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
            // 
            // colCardID
            // 
            this.colCardID.DataPropertyName = "CardID";
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colOwnerName
            // 
            resources.ApplyResources(this.colOwnerName, "colOwnerName");
            this.colOwnerName.Name = "colOwnerName";
            this.colOwnerName.ReadOnly = true;
            // 
            // colCardCertificate
            // 
            resources.ApplyResources(this.colCardCertificate, "colCardCertificate");
            this.colCardCertificate.Name = "colCardCertificate";
            this.colCardCertificate.ReadOnly = true;
            // 
            // colCardType
            // 
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colPaid
            // 
            resources.ApplyResources(this.colPaid, "colPaid");
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            // 
            // colDeduction
            // 
            resources.ApplyResources(this.colDeduction, "colDeduction");
            this.colDeduction.Name = "colDeduction";
            this.colDeduction.ReadOnly = true;
            // 
            // colAccount
            // 
            resources.ApplyResources(this.colAccount, "colAccount");
            this.colAccount.Name = "colAccount";
            this.colAccount.ReadOnly = true;
            // 
            // colBalance
            // 
            resources.ApplyResources(this.colBalance, "colBalance");
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // colBegin
            // 
            resources.ApplyResources(this.colBegin, "colBegin");
            this.colBegin.Name = "colBegin";
            this.colBegin.ReadOnly = true;
            // 
            // colEnd
            // 
            this.colEnd.DataPropertyName = "CurrentDate";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd";
            this.colEnd.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colEnd, "colEnd");
            this.colEnd.Name = "colEnd";
            this.colEnd.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtYear
            // 
            this.txtYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtYear.FormattingEnabled = true;
            this.txtYear.Items.AddRange(new object[] {
            resources.GetString("txtYear.Items"),
            resources.GetString("txtYear.Items1"),
            resources.GetString("txtYear.Items2"),
            resources.GetString("txtYear.Items3"),
            resources.GetString("txtYear.Items4"),
            resources.GetString("txtYear.Items5"),
            resources.GetString("txtYear.Items6"),
            resources.GetString("txtYear.Items7"),
            resources.GetString("txtYear.Items8"),
            resources.GetString("txtYear.Items9"),
            resources.GetString("txtYear.Items10"),
            resources.GetString("txtYear.Items11"),
            resources.GetString("txtYear.Items12"),
            resources.GetString("txtYear.Items13")});
            resources.ApplyResources(this.txtYear, "txtYear");
            this.txtYear.Name = "txtYear";
            // 
            // txtMonth
            // 
            this.txtMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtMonth.FormattingEnabled = true;
            this.txtMonth.Items.AddRange(new object[] {
            resources.GetString("txtMonth.Items"),
            resources.GetString("txtMonth.Items1"),
            resources.GetString("txtMonth.Items2"),
            resources.GetString("txtMonth.Items3"),
            resources.GetString("txtMonth.Items4"),
            resources.GetString("txtMonth.Items5"),
            resources.GetString("txtMonth.Items6"),
            resources.GetString("txtMonth.Items7"),
            resources.GetString("txtMonth.Items8"),
            resources.GetString("txtMonth.Items9"),
            resources.GetString("txtMonth.Items10"),
            resources.GetString("txtMonth.Items11")});
            resources.ApplyResources(this.txtMonth, "txtMonth");
            this.txtMonth.Name = "txtMonth";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.Name = "comCardType";
            // 
            // txtMonthlyFee
            // 
            resources.ApplyResources(this.txtMonthlyFee, "txtMonthlyFee");
            this.txtMonthlyFee.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtMonthlyFee.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMonthlyFee.Name = "txtMonthlyFee";
            this.txtMonthlyFee.NumberWithCommas = false;
            this.txtMonthlyFee.PointCount = 2;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // FrmHasPaidCardReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMonthlyFee);
            this.Controls.Add(this.comCardType);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmHasPaidCardReport";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtYear, 0);
            this.Controls.SetChildIndex(this.txtMonth, 0);
            this.Controls.SetChildIndex(this.comCardType, 0);
            this.Controls.SetChildIndex(this.txtMonthlyFee, 0);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox txtYear;
        private System.Windows.Forms.ComboBox txtMonth;
        private System.Windows.Forms.Label label4;
        private UserControls.CardTypeComboBox comCardType;
        private GeneralLibrary.WinformControl.DecimalTextBox txtMonthlyFee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeduction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBegin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnd;
    }
}