namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardReleaseReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardReleaseReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReleaseDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActivationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValidDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeposit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPayMentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReleaseMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSettled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.workStationCombobox1 = new Ralid.Park.UserControls.StationNameComboBox(this.components);
            this.operatorCombobox1 = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCount = new System.Windows.Forms.Label();
            this.txtRecieveMoney = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            // customDataGridView1
            // 
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colOwnerName,
            this.colCardCertificate,
            this.colCarPlate,
            this.colReleaseDateTime,
            this.colCardType,
            this.colActivationDate,
            this.colValidDate,
            this.colDeposit,
            this.colPayMentMode,
            this.colReleaseMoney,
            this.colSettled,
            this.colOperatorID,
            this.colStation,
            this.ColMemo});
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
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colReleaseDateTime
            // 
            this.colReleaseDateTime.DataPropertyName = "ReleaseDateTime";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colReleaseDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colReleaseDateTime, "colReleaseDateTime");
            this.colReleaseDateTime.Name = "colReleaseDateTime";
            this.colReleaseDateTime.ReadOnly = true;
            // 
            // colCardType
            // 
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colActivationDate
            // 
            dataGridViewCellStyle2.Format = "yyyy-MM-dd";
            this.colActivationDate.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.colActivationDate, "colActivationDate");
            this.colActivationDate.Name = "colActivationDate";
            this.colActivationDate.ReadOnly = true;
            // 
            // colValidDate
            // 
            dataGridViewCellStyle3.Format = "yyyy-MM-dd";
            this.colValidDate.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.colValidDate, "colValidDate");
            this.colValidDate.Name = "colValidDate";
            this.colValidDate.ReadOnly = true;
            // 
            // colDeposit
            // 
            resources.ApplyResources(this.colDeposit, "colDeposit");
            this.colDeposit.Name = "colDeposit";
            this.colDeposit.ReadOnly = true;
            // 
            // colPayMentMode
            // 
            resources.ApplyResources(this.colPayMentMode, "colPayMentMode");
            this.colPayMentMode.Name = "colPayMentMode";
            this.colPayMentMode.ReadOnly = true;
            // 
            // colReleaseMoney
            // 
            this.colReleaseMoney.DataPropertyName = "ReleaseMoney";
            resources.ApplyResources(this.colReleaseMoney, "colReleaseMoney");
            this.colReleaseMoney.Name = "colReleaseMoney";
            this.colReleaseMoney.ReadOnly = true;
            // 
            // colSettled
            // 
            resources.ApplyResources(this.colSettled, "colSettled");
            this.colSettled.Name = "colSettled";
            this.colSettled.ReadOnly = true;
            this.colSettled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colOperatorID
            // 
            this.colOperatorID.DataPropertyName = "OperatorID";
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            // 
            // colStation
            // 
            this.colStation.DataPropertyName = "StationID";
            resources.ApplyResources(this.colStation, "colStation");
            this.colStation.Name = "colStation";
            this.colStation.ReadOnly = true;
            // 
            // ColMemo
            // 
            this.ColMemo.DataPropertyName = "Memo";
            resources.ApplyResources(this.ColMemo, "ColMemo");
            this.ColMemo.Name = "ColMemo";
            this.ColMemo.ReadOnly = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtOwnerName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCertificate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.comCardType);
            this.groupBox2.Controls.Add(this.txtCarPlate);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.workStationCombobox1);
            this.groupBox2.Controls.Add(this.operatorCombobox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            this.groupBox3.Controls.Add(this.txtCount);
            this.groupBox3.Controls.Add(this.txtRecieveMoney);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.ForeColor = System.Drawing.Color.Blue;
            this.txtCount.Name = "txtCount";
            // 
            // txtRecieveMoney
            // 
            resources.ApplyResources(this.txtRecieveMoney, "txtRecieveMoney");
            this.txtRecieveMoney.ForeColor = System.Drawing.Color.Blue;
            this.txtRecieveMoney.Name = "txtRecieveMoney";
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
            // FrmCardReleaseReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.customDataGridView1);
            this.Name = "FrmCardReleaseReport";
            this.Load += new System.EventHandler(this.FrmCardReleaseReport_Load);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private Ralid.Park.UserControls.StationNameComboBox workStationCombobox1;
        private Ralid.Park.UserControls.OperatorComboBox operatorCombobox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.Label txtRecieveMoney;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReleaseDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActivationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValidDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeposit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPayMentMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReleaseMoney;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSettled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMemo;
        private GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label7;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label6;
        private UserControls.CardTypeComboBox comCardType;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}