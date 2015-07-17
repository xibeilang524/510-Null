namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmAPMRefundReport
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAPMRefundReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSerialNum = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.comAPM = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.workStationCombobox1 = new Ralid.Park.UserControls.StationNameComboBox(this.components);
            this.operatorCombobox1 = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTurnBackMoney = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentSerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRefundDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParkFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPaidFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRefundMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnterDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaidDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSettled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSettleDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtSerialNum);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.comAPM);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtCertificate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.workStationCombobox1);
            this.groupBox2.Controls.Add(this.operatorCombobox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtSerialNum
            // 
            resources.ApplyResources(this.txtSerialNum, "txtSerialNum");
            this.txtSerialNum.Name = "txtSerialNum";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // comAPM
            // 
            resources.ApplyResources(this.comAPM, "comAPM");
            this.comAPM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comAPM.FormattingEnabled = true;
            this.comAPM.Name = "comAPM";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
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
            this.workStationCombobox1.OnlyStation = false;
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
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.txtTurnBackMoney);
            this.groupBox3.Controls.Add(this.txtCount);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
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
            // customDataGridView1
            // 
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colMID,
            this.colPaymentSerialNumber,
            this.colRefundDateTime,
            this.colParkFee,
            this.colTotalPaidFee,
            this.colRefundMoney,
            this.colEnterDateTime,
            this.colPaidDateTime,
            this.colSettled,
            this.colSettleDateTime,
            this.colOperatorID,
            this.colStation,
            this.colCardCertificate,
            this.ColMemo});
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
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
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 5, 14, 47, 25, 796);
            // 
            // colCardID
            // 
            this.colCardID.DataPropertyName = "CardID";
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colMID
            // 
            resources.ApplyResources(this.colMID, "colMID");
            this.colMID.Name = "colMID";
            this.colMID.ReadOnly = true;
            // 
            // colPaymentSerialNumber
            // 
            resources.ApplyResources(this.colPaymentSerialNumber, "colPaymentSerialNumber");
            this.colPaymentSerialNumber.Name = "colPaymentSerialNumber";
            this.colPaymentSerialNumber.ReadOnly = true;
            // 
            // colRefundDateTime
            // 
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.colRefundDateTime.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.colRefundDateTime, "colRefundDateTime");
            this.colRefundDateTime.Name = "colRefundDateTime";
            this.colRefundDateTime.ReadOnly = true;
            // 
            // colParkFee
            // 
            resources.ApplyResources(this.colParkFee, "colParkFee");
            this.colParkFee.Name = "colParkFee";
            this.colParkFee.ReadOnly = true;
            // 
            // colTotalPaidFee
            // 
            resources.ApplyResources(this.colTotalPaidFee, "colTotalPaidFee");
            this.colTotalPaidFee.Name = "colTotalPaidFee";
            this.colTotalPaidFee.ReadOnly = true;
            // 
            // colRefundMoney
            // 
            resources.ApplyResources(this.colRefundMoney, "colRefundMoney");
            this.colRefundMoney.Name = "colRefundMoney";
            this.colRefundMoney.ReadOnly = true;
            // 
            // colEnterDateTime
            // 
            resources.ApplyResources(this.colEnterDateTime, "colEnterDateTime");
            this.colEnterDateTime.Name = "colEnterDateTime";
            this.colEnterDateTime.ReadOnly = true;
            // 
            // colPaidDateTime
            // 
            resources.ApplyResources(this.colPaidDateTime, "colPaidDateTime");
            this.colPaidDateTime.Name = "colPaidDateTime";
            this.colPaidDateTime.ReadOnly = true;
            // 
            // colSettled
            // 
            resources.ApplyResources(this.colSettled, "colSettled");
            this.colSettled.Name = "colSettled";
            this.colSettled.ReadOnly = true;
            this.colSettled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSettleDateTime
            // 
            resources.ApplyResources(this.colSettleDateTime, "colSettleDateTime");
            this.colSettleDateTime.Name = "colSettleDateTime";
            this.colSettleDateTime.ReadOnly = true;
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
            // colCardCertificate
            // 
            resources.ApplyResources(this.colCardCertificate, "colCardCertificate");
            this.colCardCertificate.Name = "colCardCertificate";
            this.colCardCertificate.ReadOnly = true;
            // 
            // ColMemo
            // 
            this.ColMemo.DataPropertyName = "Memo";
            resources.ApplyResources(this.ColMemo, "ColMemo");
            this.ColMemo.Name = "ColMemo";
            this.ColMemo.ReadOnly = true;
            // 
            // FrmAPMRefundReport
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAPMRefundReport";
            this.Load += new System.EventHandler(this.FrmAPMRefundReport_Load);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label6;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private UserControls.StationNameComboBox workStationCombobox1;
        private UserControls.OperatorComboBox operatorCombobox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label txtTurnBackMoney;
        private System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private GeneralLibrary.WinformControl.DBCTextBox txtSerialNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comAPM;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentSerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefundDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParkFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPaidFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefundMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnterDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaidDateTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSettled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSettleDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMemo;
    }
}
