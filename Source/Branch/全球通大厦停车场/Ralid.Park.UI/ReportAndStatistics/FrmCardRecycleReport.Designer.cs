namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardRecycleReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardRecycleReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecycleDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValidDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeposit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecycleMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSettled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTurnBackMoney = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            this.colCarPlate,
            this.colRecycleDateTime,
            this.colCardType,
            this.colBalance,
            this.colValidDate,
            this.colDeposit,
            this.colRecycleMoney,
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
            // colRecycleDateTime
            // 
            this.colRecycleDateTime.DataPropertyName = "RecycleDateTime";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colRecycleDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colRecycleDateTime, "colRecycleDateTime");
            this.colRecycleDateTime.Name = "colRecycleDateTime";
            this.colRecycleDateTime.ReadOnly = true;
            // 
            // colCardType
            // 
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colBalance
            // 
            resources.ApplyResources(this.colBalance, "colBalance");
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // colValidDate
            // 
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
            // colRecycleMoney
            // 
            this.colRecycleMoney.DataPropertyName = "RecycleMoney";
            resources.ApplyResources(this.colRecycleMoney, "colRecycleMoney");
            this.colRecycleMoney.Name = "colRecycleMoney";
            this.colRecycleMoney.ReadOnly = true;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTurnBackMoney);
            this.groupBox3.Controls.Add(this.txtCount);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtTurnBackMoney
            // 
            resources.ApplyResources(this.txtTurnBackMoney, "txtTurnBackMoney");
            this.txtTurnBackMoney.ForeColor = System.Drawing.Color.Blue;
            this.txtTurnBackMoney.Name = "txtTurnBackMoney";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.ForeColor = System.Drawing.Color.Blue;
            this.txtCount.Name = "txtCount";
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
            // groupBox2
            // 
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            resources.ApplyResources(this.comCardType, "comCardType");
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FrmCardRecycleReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCardRecycleReport";
            this.Load += new System.EventHandler(this.FrmCardRecycleReport_Load);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label txtTurnBackMoney;
        private System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecycleDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValidDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeposit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecycleMoney;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSettled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMemo;
        private System.Windows.Forms.GroupBox groupBox2;
        private GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label7;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label6;
        private UserControls.CardTypeComboBox comCardType;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private UserControls.StationNameComboBox workStationCombobox1;
        private UserControls.OperatorComboBox operatorCombobox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}