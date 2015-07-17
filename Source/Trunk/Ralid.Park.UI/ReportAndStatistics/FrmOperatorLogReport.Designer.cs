namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmOperatorLogReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperatorLogReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deptComboBox1 = new Ralid.Park.UserControls.DeptComboBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.operatorCombobox1 = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.customDataGridview1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_ExportCardPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ExportMonthCardPaymentReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_PrintCardPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_PrintMonthCardPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_PrintSettleLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSettle = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comOperator = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalCashDiffrence = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotalHandInPOS = new System.Windows.Forms.Label();
            this.lblTotalNonCash = new System.Windows.Forms.Label();
            this.lblTotalCash = new System.Windows.Forms.Label();
            this.lblTotalHandInCash = new System.Windows.Forms.Label();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSettleDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashParkFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashOperatorCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashOfPOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashOfCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashOfDeposit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashOfCardLost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashOfCardRecycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashOfRefund = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHandInCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashDiffrence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNonCashParkFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNonCashDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNonCashOfCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNonCashOfDeposit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNonCashOfCardLost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalNonCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHandInPOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOpenDoorCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTempCardRecycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridview1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 5, 17, 10, 27, 31);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 5, 14, 47, 25, 796);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.deptComboBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.operatorCombobox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // deptComboBox1
            // 
            resources.ApplyResources(this.deptComboBox1, "deptComboBox1");
            this.deptComboBox1.FormattingEnabled = true;
            this.deptComboBox1.Name = "deptComboBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // customDataGridview1
            // 
            resources.ApplyResources(this.customDataGridview1, "customDataGridview1");
            this.customDataGridview1.AllowUserToAddRows = false;
            this.customDataGridview1.AllowUserToDeleteRows = false;
            this.customDataGridview1.AllowUserToResizeRows = false;
            this.customDataGridview1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridview1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOperatorID,
            this.colDept,
            this.colSettleDateTime,
            this.colCashParkFact,
            this.colCashOperatorCard,
            this.colCashOfPOS,
            this.colCashDiscount,
            this.colCashOfCard,
            this.colCashOfDeposit,
            this.colCashOfCardLost,
            this.colCashOfCardRecycle,
            this.colCashOfRefund,
            this.colTotalCash,
            this.colHandInCash,
            this.colCashDiffrence,
            this.colNonCashParkFact,
            this.colNonCashDiscount,
            this.colNonCashOfCard,
            this.colNonCashOfDeposit,
            this.colNonCashOfCardLost,
            this.colTotalNonCash,
            this.colHandInPOS,
            this.colOpenDoorCount,
            this.colTempCardRecycle});
            this.customDataGridview1.Name = "customDataGridview1";
            this.customDataGridview1.RowHeadersVisible = false;
            this.customDataGridview1.RowTemplate.Height = 23;
            this.customDataGridview1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.customDataGridview1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_ExportCardPayment,
            this.mnu_ExportMonthCardPaymentReport,
            this.mnu_PrintCardPayment,
            this.mnu_PrintMonthCardPayment,
            this.mnu_PrintSettleLog});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // mnu_ExportCardPayment
            // 
            resources.ApplyResources(this.mnu_ExportCardPayment, "mnu_ExportCardPayment");
            this.mnu_ExportCardPayment.Name = "mnu_ExportCardPayment";
            this.mnu_ExportCardPayment.Click += new System.EventHandler(this.mnu_ExportCardPayment_Click);
            // 
            // mnu_ExportMonthCardPaymentReport
            // 
            resources.ApplyResources(this.mnu_ExportMonthCardPaymentReport, "mnu_ExportMonthCardPaymentReport");
            this.mnu_ExportMonthCardPaymentReport.Name = "mnu_ExportMonthCardPaymentReport";
            this.mnu_ExportMonthCardPaymentReport.Click += new System.EventHandler(this.mnu_ExportMonthCardPaymentReport_Click);
            // 
            // mnu_PrintCardPayment
            // 
            resources.ApplyResources(this.mnu_PrintCardPayment, "mnu_PrintCardPayment");
            this.mnu_PrintCardPayment.Name = "mnu_PrintCardPayment";
            this.mnu_PrintCardPayment.Click += new System.EventHandler(this.mnu_PrintCardPayment_Click);
            // 
            // mnu_PrintMonthCardPayment
            // 
            resources.ApplyResources(this.mnu_PrintMonthCardPayment, "mnu_PrintMonthCardPayment");
            this.mnu_PrintMonthCardPayment.Name = "mnu_PrintMonthCardPayment";
            this.mnu_PrintMonthCardPayment.Click += new System.EventHandler(this.mnu_PrintMonthCardPayment_Click);
            // 
            // mnu_PrintSettleLog
            // 
            resources.ApplyResources(this.mnu_PrintSettleLog, "mnu_PrintSettleLog");
            this.mnu_PrintSettleLog.Name = "mnu_PrintSettleLog";
            this.mnu_PrintSettleLog.Click += new System.EventHandler(this.mnu_PrintSettleLog_Click);
            // 
            // btnSettle
            // 
            resources.ApplyResources(this.btnSettle, "btnSettle");
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.UseVisualStyleBackColor = true;
            this.btnSettle.Click += new System.EventHandler(this.btnSettle_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.comOperator);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnSettle);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // comOperator
            // 
            resources.ApplyResources(this.comOperator, "comOperator");
            this.comOperator.FormattingEnabled = true;
            this.comOperator.Name = "comOperator";
            this.comOperator.SelectedIndexChanged += new System.EventHandler(this.comOperator_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.lblTotalCashDiffrence);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.lblTotalHandInPOS);
            this.groupBox5.Controls.Add(this.lblTotalNonCash);
            this.groupBox5.Controls.Add(this.lblTotalCash);
            this.groupBox5.Controls.Add(this.lblTotalHandInCash);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lblTotalCashDiffrence
            // 
            resources.ApplyResources(this.lblTotalCashDiffrence, "lblTotalCashDiffrence");
            this.lblTotalCashDiffrence.ForeColor = System.Drawing.Color.Red;
            this.lblTotalCashDiffrence.Name = "lblTotalCashDiffrence";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lblTotalHandInPOS
            // 
            resources.ApplyResources(this.lblTotalHandInPOS, "lblTotalHandInPOS");
            this.lblTotalHandInPOS.ForeColor = System.Drawing.Color.Black;
            this.lblTotalHandInPOS.Name = "lblTotalHandInPOS";
            // 
            // lblTotalNonCash
            // 
            resources.ApplyResources(this.lblTotalNonCash, "lblTotalNonCash");
            this.lblTotalNonCash.ForeColor = System.Drawing.Color.Black;
            this.lblTotalNonCash.Name = "lblTotalNonCash";
            // 
            // lblTotalCash
            // 
            resources.ApplyResources(this.lblTotalCash, "lblTotalCash");
            this.lblTotalCash.ForeColor = System.Drawing.Color.Black;
            this.lblTotalCash.Name = "lblTotalCash";
            // 
            // lblTotalHandInCash
            // 
            resources.ApplyResources(this.lblTotalHandInCash, "lblTotalHandInCash");
            this.lblTotalHandInCash.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalHandInCash.Name = "lblTotalHandInCash";
            // 
            // colOperatorID
            // 
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            // 
            // colDept
            // 
            resources.ApplyResources(this.colDept, "colDept");
            this.colDept.Name = "colDept";
            this.colDept.ReadOnly = true;
            // 
            // colSettleDateTime
            // 
            dataGridViewCellStyle6.Format = "yyyy-MM-dd HH:mm:ss";
            this.colSettleDateTime.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.colSettleDateTime, "colSettleDateTime");
            this.colSettleDateTime.Name = "colSettleDateTime";
            this.colSettleDateTime.ReadOnly = true;
            // 
            // colCashParkFact
            // 
            resources.ApplyResources(this.colCashParkFact, "colCashParkFact");
            this.colCashParkFact.Name = "colCashParkFact";
            this.colCashParkFact.ReadOnly = true;
            // 
            // colCashOperatorCard
            // 
            resources.ApplyResources(this.colCashOperatorCard, "colCashOperatorCard");
            this.colCashOperatorCard.Name = "colCashOperatorCard";
            this.colCashOperatorCard.ReadOnly = true;
            // 
            // colCashOfPOS
            // 
            resources.ApplyResources(this.colCashOfPOS, "colCashOfPOS");
            this.colCashOfPOS.Name = "colCashOfPOS";
            this.colCashOfPOS.ReadOnly = true;
            // 
            // colCashDiscount
            // 
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Red;
            this.colCashDiscount.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.colCashDiscount, "colCashDiscount");
            this.colCashDiscount.Name = "colCashDiscount";
            this.colCashDiscount.ReadOnly = true;
            // 
            // colCashOfCard
            // 
            resources.ApplyResources(this.colCashOfCard, "colCashOfCard");
            this.colCashOfCard.Name = "colCashOfCard";
            this.colCashOfCard.ReadOnly = true;
            // 
            // colCashOfDeposit
            // 
            resources.ApplyResources(this.colCashOfDeposit, "colCashOfDeposit");
            this.colCashOfDeposit.Name = "colCashOfDeposit";
            this.colCashOfDeposit.ReadOnly = true;
            // 
            // colCashOfCardLost
            // 
            resources.ApplyResources(this.colCashOfCardLost, "colCashOfCardLost");
            this.colCashOfCardLost.Name = "colCashOfCardLost";
            this.colCashOfCardLost.ReadOnly = true;
            // 
            // colCashOfCardRecycle
            // 
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Red;
            this.colCashOfCardRecycle.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.colCashOfCardRecycle, "colCashOfCardRecycle");
            this.colCashOfCardRecycle.Name = "colCashOfCardRecycle";
            this.colCashOfCardRecycle.ReadOnly = true;
            // 
            // colCashOfRefund
            // 
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Red;
            this.colCashOfRefund.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.colCashOfRefund, "colCashOfRefund");
            this.colCashOfRefund.Name = "colCashOfRefund";
            this.colCashOfRefund.ReadOnly = true;
            // 
            // colTotalCash
            // 
            resources.ApplyResources(this.colTotalCash, "colTotalCash");
            this.colTotalCash.Name = "colTotalCash";
            this.colTotalCash.ReadOnly = true;
            // 
            // colHandInCash
            // 
            resources.ApplyResources(this.colHandInCash, "colHandInCash");
            this.colHandInCash.Name = "colHandInCash";
            this.colHandInCash.ReadOnly = true;
            // 
            // colCashDiffrence
            // 
            resources.ApplyResources(this.colCashDiffrence, "colCashDiffrence");
            this.colCashDiffrence.Name = "colCashDiffrence";
            this.colCashDiffrence.ReadOnly = true;
            // 
            // colNonCashParkFact
            // 
            resources.ApplyResources(this.colNonCashParkFact, "colNonCashParkFact");
            this.colNonCashParkFact.Name = "colNonCashParkFact";
            this.colNonCashParkFact.ReadOnly = true;
            // 
            // colNonCashDiscount
            // 
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Red;
            this.colNonCashDiscount.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.colNonCashDiscount, "colNonCashDiscount");
            this.colNonCashDiscount.Name = "colNonCashDiscount";
            // 
            // colNonCashOfCard
            // 
            resources.ApplyResources(this.colNonCashOfCard, "colNonCashOfCard");
            this.colNonCashOfCard.Name = "colNonCashOfCard";
            this.colNonCashOfCard.ReadOnly = true;
            // 
            // colNonCashOfDeposit
            // 
            resources.ApplyResources(this.colNonCashOfDeposit, "colNonCashOfDeposit");
            this.colNonCashOfDeposit.Name = "colNonCashOfDeposit";
            this.colNonCashOfDeposit.ReadOnly = true;
            // 
            // colNonCashOfCardLost
            // 
            resources.ApplyResources(this.colNonCashOfCardLost, "colNonCashOfCardLost");
            this.colNonCashOfCardLost.Name = "colNonCashOfCardLost";
            this.colNonCashOfCardLost.ReadOnly = true;
            // 
            // colTotalNonCash
            // 
            resources.ApplyResources(this.colTotalNonCash, "colTotalNonCash");
            this.colTotalNonCash.Name = "colTotalNonCash";
            this.colTotalNonCash.ReadOnly = true;
            // 
            // colHandInPOS
            // 
            resources.ApplyResources(this.colHandInPOS, "colHandInPOS");
            this.colHandInPOS.Name = "colHandInPOS";
            this.colHandInPOS.ReadOnly = true;
            // 
            // colOpenDoorCount
            // 
            resources.ApplyResources(this.colOpenDoorCount, "colOpenDoorCount");
            this.colOpenDoorCount.Name = "colOpenDoorCount";
            this.colOpenDoorCount.ReadOnly = true;
            // 
            // colTempCardRecycle
            // 
            resources.ApplyResources(this.colTempCardRecycle, "colTempCardRecycle");
            this.colTempCardRecycle.Name = "colTempCardRecycle";
            this.colTempCardRecycle.ReadOnly = true;
            // 
            // FrmOperatorLogReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.customDataGridview1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmOperatorLogReport";
            this.ShowInTaskbar = false;
            this.Activated += new System.EventHandler(this.FrmOperatorLogReport_Activated);
            this.Load += new System.EventHandler(this.FrmOperatorLogReport_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridview1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridview1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView customDataGridview1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private Ralid.Park.UserControls.OperatorComboBox operatorCombobox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_ExportCardPayment;
        private System.Windows.Forms.ToolStripMenuItem mnu_ExportMonthCardPaymentReport;
        private System.Windows.Forms.Button btnSettle;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControls.OperatorComboBox comOperator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem mnu_PrintSettleLog;
        private System.Windows.Forms.ToolStripMenuItem mnu_PrintCardPayment;
        private System.Windows.Forms.ToolStripMenuItem mnu_PrintMonthCardPayment;
        private UserControls.DeptComboBox deptComboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalCashDiffrence;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalNonCash;
        private System.Windows.Forms.Label lblTotalCash;
        private System.Windows.Forms.Label lblTotalHandInCash;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalHandInPOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSettleDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashParkFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashOperatorCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashOfPOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashOfCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashOfDeposit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashOfCardLost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashOfCardRecycle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashOfRefund;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHandInCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashDiffrence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNonCashParkFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNonCashDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNonCashOfCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNonCashOfDeposit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNonCashOfCardLost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalNonCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHandInPOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOpenDoorCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTempCardRecycle;
    }
}