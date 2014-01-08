namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardChargeReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardChargeReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRecieveMoney = new System.Windows.Forms.Label();
            this.txtChargeAmount = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.customDataGridview1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChargeDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChargeAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiredDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSettled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.workStationCombobox1 = new Ralid.Park.UserControls.StationNameComboBox(this.components);
            this.operatorCombobox1 = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridview1)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 6, 23, 59, 59, 0);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = false;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 5, 14, 47, 25, 796);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.txtRecieveMoney);
            this.groupBox3.Controls.Add(this.txtChargeAmount);
            this.groupBox3.Controls.Add(this.txtCount);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtRecieveMoney
            // 
            resources.ApplyResources(this.txtRecieveMoney, "txtRecieveMoney");
            this.txtRecieveMoney.ForeColor = System.Drawing.Color.Blue;
            this.txtRecieveMoney.Name = "txtRecieveMoney";
            // 
            // txtChargeAmount
            // 
            resources.ApplyResources(this.txtChargeAmount, "txtChargeAmount");
            this.txtChargeAmount.ForeColor = System.Drawing.Color.Blue;
            this.txtChargeAmount.Name = "txtChargeAmount";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.ForeColor = System.Drawing.Color.Blue;
            this.txtCount.Name = "txtCount";
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // customDataGridview1
            // 
            resources.ApplyResources(this.customDataGridview1, "customDataGridview1");
            this.customDataGridview1.AllowUserToAddRows = false;
            this.customDataGridview1.AllowUserToDeleteRows = false;
            this.customDataGridview1.AllowUserToResizeRows = false;
            this.customDataGridview1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridview1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colOwnerName,
            this.colCardCertificate,
            this.colCarPlate,
            this.colChargeDateTime,
            this.colChargeAmount,
            this.colPayment,
            this.colPaymentMode,
            this.colExpiredDate,
            this.colBalance,
            this.colSettled,
            this.colOperatorID,
            this.colStation,
            this.colMemo});
            this.customDataGridview1.Name = "customDataGridview1";
            this.customDataGridview1.RowHeadersVisible = false;
            this.customDataGridview1.RowTemplate.Height = 23;
            // 
            // colCardID
            // 
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
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colChargeDateTime
            // 
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colChargeDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colChargeDateTime, "colChargeDateTime");
            this.colChargeDateTime.Name = "colChargeDateTime";
            this.colChargeDateTime.ReadOnly = true;
            // 
            // colChargeAmount
            // 
            resources.ApplyResources(this.colChargeAmount, "colChargeAmount");
            this.colChargeAmount.Name = "colChargeAmount";
            this.colChargeAmount.ReadOnly = true;
            // 
            // colPayment
            // 
            resources.ApplyResources(this.colPayment, "colPayment");
            this.colPayment.Name = "colPayment";
            this.colPayment.ReadOnly = true;
            // 
            // colPaymentMode
            // 
            resources.ApplyResources(this.colPaymentMode, "colPaymentMode");
            this.colPaymentMode.Name = "colPaymentMode";
            this.colPaymentMode.ReadOnly = true;
            // 
            // colExpiredDate
            // 
            resources.ApplyResources(this.colExpiredDate, "colExpiredDate");
            this.colExpiredDate.Name = "colExpiredDate";
            this.colExpiredDate.ReadOnly = true;
            // 
            // colBalance
            // 
            resources.ApplyResources(this.colBalance, "colBalance");
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // colSettled
            // 
            resources.ApplyResources(this.colSettled, "colSettled");
            this.colSettled.Name = "colSettled";
            this.colSettled.ReadOnly = true;
            this.colSettled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSettled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colOperatorID
            // 
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            // 
            // colStation
            // 
            resources.ApplyResources(this.colStation, "colStation");
            this.colStation.Name = "colStation";
            this.colStation.ReadOnly = true;
            // 
            // colMemo
            // 
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtOwnerName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCertificate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCarPlate);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.workStationCombobox1);
            this.groupBox2.Controls.Add(this.operatorCombobox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtOwnerName
            // 
            resources.ApplyResources(this.txtOwnerName, "txtOwnerName");
            this.txtOwnerName.Name = "txtOwnerName";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtCertificate
            // 
            resources.ApplyResources(this.txtCertificate, "txtCertificate");
            this.txtCertificate.Name = "txtCertificate";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
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
            // workStationCombobox1
            // 
            resources.ApplyResources(this.workStationCombobox1, "workStationCombobox1");
            this.workStationCombobox1.FormattingEnabled = true;
            this.workStationCombobox1.Name = "workStationCombobox1";
            // 
            // operatorCombobox1
            // 
            resources.ApplyResources(this.operatorCombobox1, "operatorCombobox1");
            this.operatorCombobox1.FormattingEnabled = true;
            this.operatorCombobox1.Name = "operatorCombobox1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // FrmCardChargeReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.customDataGridview1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmCardChargeReport";
            this.Load += new System.EventHandler(this.FrmCardChargeReport_Load);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.customDataGridview1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridview1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label txtRecieveMoney;
        private System.Windows.Forms.Label txtChargeAmount;
        private System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private UserControls.CustomDataGridView customDataGridview1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpiredDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSettled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private System.Windows.Forms.GroupBox groupBox2;
        private GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label7;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label9;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private UserControls.StationNameComboBox workStationCombobox1;
        private UserControls.OperatorComboBox operatorCombobox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
    }
}