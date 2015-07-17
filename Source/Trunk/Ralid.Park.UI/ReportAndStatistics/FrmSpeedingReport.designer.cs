namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmSpeedingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpeedingReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSpeedingRecord = new System.Windows.Forms.TabPage();
            this.dgvSpeedingRecord = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colSpeedingDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlateNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpSpeedingLog = new System.Windows.Forms.TabPage();
            this.dgvSpeedingLog = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colLSpeedingDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLPlateNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLDealDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLDealOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLDealMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSpeedingRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeedingRecord)).BeginInit();
            this.tpSpeedingLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeedingLog)).BeginInit();
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
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 6, 15, 23, 28, 975);
            resources.ApplyResources(this.ucDateTimeInterval1, "ucDateTimeInterval1");
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 5, 17, 12, 37, 562);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCarPlate);
            this.groupBox2.Controls.Add(this.label7);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
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
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tpSpeedingRecord);
            this.tabControl1.Controls.Add(this.tpSpeedingLog);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tpSpeedingRecord
            // 
            this.tpSpeedingRecord.Controls.Add(this.dgvSpeedingRecord);
            resources.ApplyResources(this.tpSpeedingRecord, "tpSpeedingRecord");
            this.tpSpeedingRecord.Name = "tpSpeedingRecord";
            this.tpSpeedingRecord.UseVisualStyleBackColor = true;
            // 
            // dgvSpeedingRecord
            // 
            resources.ApplyResources(this.dgvSpeedingRecord, "dgvSpeedingRecord");
            this.dgvSpeedingRecord.AllowUserToAddRows = false;
            this.dgvSpeedingRecord.AllowUserToDeleteRows = false;
            this.dgvSpeedingRecord.AllowUserToResizeRows = false;
            this.dgvSpeedingRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpeedingRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSpeedingDateTime,
            this.colPlateNumber,
            this.colPlace,
            this.colSpeed,
            this.colMemo});
            this.dgvSpeedingRecord.Name = "dgvSpeedingRecord";
            this.dgvSpeedingRecord.RowHeadersVisible = false;
            this.dgvSpeedingRecord.RowTemplate.Height = 23;
            this.dgvSpeedingRecord.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridview1_CellDoubleClick);
            // 
            // colSpeedingDateTime
            // 
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colSpeedingDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colSpeedingDateTime, "colSpeedingDateTime");
            this.colSpeedingDateTime.Name = "colSpeedingDateTime";
            this.colSpeedingDateTime.ReadOnly = true;
            // 
            // colPlateNumber
            // 
            resources.ApplyResources(this.colPlateNumber, "colPlateNumber");
            this.colPlateNumber.Name = "colPlateNumber";
            this.colPlateNumber.ReadOnly = true;
            // 
            // colPlace
            // 
            resources.ApplyResources(this.colPlace, "colPlace");
            this.colPlace.Name = "colPlace";
            this.colPlace.ReadOnly = true;
            // 
            // colSpeed
            // 
            resources.ApplyResources(this.colSpeed, "colSpeed");
            this.colSpeed.Name = "colSpeed";
            this.colSpeed.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // tpSpeedingLog
            // 
            this.tpSpeedingLog.Controls.Add(this.dgvSpeedingLog);
            resources.ApplyResources(this.tpSpeedingLog, "tpSpeedingLog");
            this.tpSpeedingLog.Name = "tpSpeedingLog";
            this.tpSpeedingLog.UseVisualStyleBackColor = true;
            // 
            // dgvSpeedingLog
            // 
            resources.ApplyResources(this.dgvSpeedingLog, "dgvSpeedingLog");
            this.dgvSpeedingLog.AllowUserToAddRows = false;
            this.dgvSpeedingLog.AllowUserToDeleteRows = false;
            this.dgvSpeedingLog.AllowUserToResizeRows = false;
            this.dgvSpeedingLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpeedingLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLSpeedingDateTime,
            this.colLPlateNumber,
            this.colLPlace,
            this.colLSpeed,
            this.colLMemo,
            this.colLDealDateTime,
            this.colLDealOperator,
            this.colLDealMemo});
            this.dgvSpeedingLog.Name = "dgvSpeedingLog";
            this.dgvSpeedingLog.RowHeadersVisible = false;
            this.dgvSpeedingLog.RowTemplate.Height = 23;
            this.dgvSpeedingLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridview1_CellDoubleClick);
            // 
            // colLSpeedingDateTime
            // 
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.colLSpeedingDateTime.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.colLSpeedingDateTime, "colLSpeedingDateTime");
            this.colLSpeedingDateTime.Name = "colLSpeedingDateTime";
            this.colLSpeedingDateTime.ReadOnly = true;
            // 
            // colLPlateNumber
            // 
            resources.ApplyResources(this.colLPlateNumber, "colLPlateNumber");
            this.colLPlateNumber.Name = "colLPlateNumber";
            this.colLPlateNumber.ReadOnly = true;
            // 
            // colLPlace
            // 
            resources.ApplyResources(this.colLPlace, "colLPlace");
            this.colLPlace.Name = "colLPlace";
            this.colLPlace.ReadOnly = true;
            // 
            // colLSpeed
            // 
            resources.ApplyResources(this.colLSpeed, "colLSpeed");
            this.colLSpeed.Name = "colLSpeed";
            this.colLSpeed.ReadOnly = true;
            // 
            // colLMemo
            // 
            resources.ApplyResources(this.colLMemo, "colLMemo");
            this.colLMemo.Name = "colLMemo";
            this.colLMemo.ReadOnly = true;
            // 
            // colLDealDateTime
            // 
            resources.ApplyResources(this.colLDealDateTime, "colLDealDateTime");
            this.colLDealDateTime.Name = "colLDealDateTime";
            this.colLDealDateTime.ReadOnly = true;
            // 
            // colLDealOperator
            // 
            resources.ApplyResources(this.colLDealOperator, "colLDealOperator");
            this.colLDealOperator.Name = "colLDealOperator";
            this.colLDealOperator.ReadOnly = true;
            // 
            // colLDealMemo
            // 
            this.colLDealMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colLDealMemo, "colLDealMemo");
            this.colLDealMemo.Name = "colLDealMemo";
            this.colLDealMemo.ReadOnly = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmSpeedingReport
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmSpeedingReport";
            this.Load += new System.EventHandler(this.FrmSpeedingReport_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSpeedingRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeedingRecord)).EndInit();
            this.tpSpeedingLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeedingLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSpeedingRecord;
        private System.Windows.Forms.TabPage tpSpeedingLog;
        private UserControls.CustomDataGridView dgvSpeedingRecord;
        private UserControls.CustomDataGridView dgvSpeedingLog;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpeedingDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlateNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLSpeedingDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLPlateNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLDealDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLDealOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLDealMemo;
    }
}
