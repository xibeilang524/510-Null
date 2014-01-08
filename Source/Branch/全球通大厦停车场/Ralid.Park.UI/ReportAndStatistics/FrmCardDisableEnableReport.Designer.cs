namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardDisableEnableReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardDisableEnableReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisableDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisableOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisableMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnableDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnableOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnableMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
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
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToResizeRows = false;
            resources.ApplyResources(this.gridView, "gridView");
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colOwnerName,
            this.colCardCertificate,
            this.colCarPlate,
            this.colDisableDateTime,
            this.colDisableOperator,
            this.colDisableMemo,
            this.colEnableDateTime,
            this.colEnableOperator,
            this.colEnableMemo});
            this.gridView.Name = "gridView";
            this.gridView.RowHeadersVisible = false;
            this.gridView.RowTemplate.Height = 23;
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
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colDisableDateTime
            // 
            this.colDisableDateTime.DataPropertyName = "DisableDateTime";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colDisableDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDisableDateTime.FillWeight = 130F;
            resources.ApplyResources(this.colDisableDateTime, "colDisableDateTime");
            this.colDisableDateTime.Name = "colDisableDateTime";
            this.colDisableDateTime.ReadOnly = true;
            // 
            // colDisableOperator
            // 
            this.colDisableOperator.DataPropertyName = "DisableOperator";
            resources.ApplyResources(this.colDisableOperator, "colDisableOperator");
            this.colDisableOperator.Name = "colDisableOperator";
            this.colDisableOperator.ReadOnly = true;
            // 
            // colDisableMemo
            // 
            this.colDisableMemo.DataPropertyName = "DisableMemo";
            resources.ApplyResources(this.colDisableMemo, "colDisableMemo");
            this.colDisableMemo.Name = "colDisableMemo";
            this.colDisableMemo.ReadOnly = true;
            // 
            // colEnableDateTime
            // 
            this.colEnableDateTime.DataPropertyName = "EnableDateTime";
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.colEnableDateTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.colEnableDateTime.FillWeight = 130F;
            resources.ApplyResources(this.colEnableDateTime, "colEnableDateTime");
            this.colEnableDateTime.Name = "colEnableDateTime";
            this.colEnableDateTime.ReadOnly = true;
            // 
            // colEnableOperator
            // 
            this.colEnableOperator.DataPropertyName = "EnableOperator";
            resources.ApplyResources(this.colEnableOperator, "colEnableOperator");
            this.colEnableOperator.Name = "colEnableOperator";
            this.colEnableOperator.ReadOnly = true;
            // 
            // colEnableMemo
            // 
            this.colEnableMemo.DataPropertyName = "EnableMemo";
            resources.ApplyResources(this.colEnableMemo, "colEnableMemo");
            this.colEnableMemo.Name = "colEnableMemo";
            this.colEnableMemo.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ucDateTimeInterval1
            // 
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 6, 23, 59, 59, 0);
            resources.ApplyResources(this.ucDateTimeInterval1, "ucDateTimeInterval1");
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = false;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 5, 14, 47, 25, 796);
            // 
            // groupBox2
            // 
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
            resources.ApplyResources(this.groupBox2, "groupBox2");
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
            this.workStationCombobox1.FormattingEnabled = true;
            resources.ApplyResources(this.workStationCombobox1, "workStationCombobox1");
            this.workStationCombobox1.Name = "workStationCombobox1";
            // 
            // operatorCombobox1
            // 
            this.operatorCombobox1.FormattingEnabled = true;
            resources.ApplyResources(this.operatorCombobox1, "operatorCombobox1");
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
            // FrmCardDisableEnableReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gridView);
            this.Name = "FrmCardDisableEnableReport";
            this.Load += new System.EventHandler(this.FrmCardDisableEnableReport_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.gridView, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView gridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisableDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisableOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisableMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnableDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnableOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnableMemo;
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