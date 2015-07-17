namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmServerSwitchReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmServerSwitchReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.customDataGridview1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comPark = new Ralid.Park.UserControls.ParkCombobox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.colPark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSwitchDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSwitchServerIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSwitchStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSMSStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridview1)).BeginInit();
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
            // customDataGridview1
            // 
            this.customDataGridview1.AllowUserToAddRows = false;
            this.customDataGridview1.AllowUserToDeleteRows = false;
            this.customDataGridview1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.customDataGridview1, "customDataGridview1");
            this.customDataGridview1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridview1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPark,
            this.colSwitchDateTime,
            this.colSwitchServerIP,
            this.colSwitchStatus,
            this.colOperator,
            this.colLastDateTime,
            this.colLastIP,
            this.colLastStatus,
            this.colSMSStatus,
            this.colMemo});
            this.customDataGridview1.Name = "customDataGridview1";
            this.customDataGridview1.RowHeadersVisible = false;
            this.customDataGridview1.RowTemplate.Height = 23;
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
            this.groupBox2.Controls.Add(this.comPark);
            this.groupBox2.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // comPark
            // 
            this.comPark.FormattingEnabled = true;
            resources.ApplyResources(this.comPark, "comPark");
            this.comPark.Name = "comPark";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // colPark
            // 
            resources.ApplyResources(this.colPark, "colPark");
            this.colPark.Name = "colPark";
            this.colPark.ReadOnly = true;
            // 
            // colSwitchDateTime
            // 
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colSwitchDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colSwitchDateTime, "colSwitchDateTime");
            this.colSwitchDateTime.Name = "colSwitchDateTime";
            this.colSwitchDateTime.ReadOnly = true;
            // 
            // colSwitchServerIP
            // 
            resources.ApplyResources(this.colSwitchServerIP, "colSwitchServerIP");
            this.colSwitchServerIP.Name = "colSwitchServerIP";
            this.colSwitchServerIP.ReadOnly = true;
            // 
            // colSwitchStatus
            // 
            resources.ApplyResources(this.colSwitchStatus, "colSwitchStatus");
            this.colSwitchStatus.Name = "colSwitchStatus";
            this.colSwitchStatus.ReadOnly = true;
            // 
            // colOperator
            // 
            this.colOperator.DataPropertyName = "OperatorID";
            resources.ApplyResources(this.colOperator, "colOperator");
            this.colOperator.Name = "colOperator";
            this.colOperator.ReadOnly = true;
            // 
            // colLastDateTime
            // 
            resources.ApplyResources(this.colLastDateTime, "colLastDateTime");
            this.colLastDateTime.Name = "colLastDateTime";
            this.colLastDateTime.ReadOnly = true;
            // 
            // colLastIP
            // 
            resources.ApplyResources(this.colLastIP, "colLastIP");
            this.colLastIP.Name = "colLastIP";
            this.colLastIP.ReadOnly = true;
            // 
            // colLastStatus
            // 
            resources.ApplyResources(this.colLastStatus, "colLastStatus");
            this.colLastStatus.Name = "colLastStatus";
            this.colLastStatus.ReadOnly = true;
            // 
            // colSMSStatus
            // 
            resources.ApplyResources(this.colSMSStatus, "colSMSStatus");
            this.colSMSStatus.Name = "colSMSStatus";
            this.colSMSStatus.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMemo.DataPropertyName = "AlarmDescr";
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // FrmServerSwitchReport
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.customDataGridview1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmServerSwitchReport";
            this.Load += new System.EventHandler(this.FrmServerSwitchReport_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridview1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridview1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.CustomDataGridView customDataGridview1;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControls.ParkCombobox comPark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSwitchDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSwitchServerIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSwitchStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSMSStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;

    }
}
