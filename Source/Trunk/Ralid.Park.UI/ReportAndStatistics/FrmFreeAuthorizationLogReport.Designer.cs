namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmFreeAuthorizationLogReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFreeAuthorizationLogReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.customDataGridview1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comWorkStation = new Ralid.Park.UserControls.StationNameComboBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.comOperator = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colLogDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeginDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInPark = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNotCheckOut = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            resources.ApplyResources(this.customDataGridview1, "customDataGridview1");
            this.customDataGridview1.AllowUserToAddRows = false;
            this.customDataGridview1.AllowUserToDeleteRows = false;
            this.customDataGridview1.AllowUserToResizeRows = false;
            this.customDataGridview1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridview1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLogDateTime,
            this.colOperatorID,
            this.colCardID,
            this.colBeginDateTime,
            this.colEndDateTime,
            this.colStationID,
            this.colInPark,
            this.colNotCheckOut,
            this.colMemo});
            this.customDataGridview1.Name = "customDataGridview1";
            this.customDataGridview1.RowHeadersVisible = false;
            this.customDataGridview1.RowTemplate.Height = 23;
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
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 6, 15, 23, 28, 975);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 5, 17, 12, 37, 562);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.comWorkStation);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.comOperator);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // comWorkStation
            // 
            resources.ApplyResources(this.comWorkStation, "comWorkStation");
            this.comWorkStation.FormattingEnabled = true;
            this.comWorkStation.Name = "comWorkStation";
            this.comWorkStation.OnlyStation = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            // 
            // comOperator
            // 
            resources.ApplyResources(this.comOperator, "comOperator");
            this.comOperator.FormattingEnabled = true;
            this.comOperator.Name = "comOperator";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // colLogDateTime
            // 
            this.colLogDateTime.DataPropertyName = "LogDateTime";
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.colLogDateTime.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.colLogDateTime, "colLogDateTime");
            this.colLogDateTime.Name = "colLogDateTime";
            this.colLogDateTime.ReadOnly = true;
            // 
            // colOperatorID
            // 
            this.colOperatorID.DataPropertyName = "OperatorID";
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            // 
            // colCardID
            // 
            this.colCardID.DataPropertyName = "CardID";
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colBeginDateTime
            // 
            this.colBeginDateTime.DataPropertyName = "BeginDateTime";
            resources.ApplyResources(this.colBeginDateTime, "colBeginDateTime");
            this.colBeginDateTime.Name = "colBeginDateTime";
            this.colBeginDateTime.ReadOnly = true;
            // 
            // colEndDateTime
            // 
            this.colEndDateTime.DataPropertyName = "EndDateTime";
            resources.ApplyResources(this.colEndDateTime, "colEndDateTime");
            this.colEndDateTime.Name = "colEndDateTime";
            this.colEndDateTime.ReadOnly = true;
            // 
            // colStationID
            // 
            this.colStationID.DataPropertyName = "StationID";
            resources.ApplyResources(this.colStationID, "colStationID");
            this.colStationID.Name = "colStationID";
            this.colStationID.ReadOnly = true;
            // 
            // colInPark
            // 
            this.colInPark.DataPropertyName = "InPark";
            resources.ApplyResources(this.colInPark, "colInPark");
            this.colInPark.Name = "colInPark";
            this.colInPark.ReadOnly = true;
            // 
            // colNotCheckOut
            // 
            resources.ApplyResources(this.colNotCheckOut, "colNotCheckOut");
            this.colNotCheckOut.Name = "colNotCheckOut";
            this.colNotCheckOut.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMemo.DataPropertyName = "Memo";
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // FrmFreeAuthorizationLogReport
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.customDataGridview1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmFreeAuthorizationLogReport";
            this.Load += new System.EventHandler(this.FrmFreeAuthorizationLogReport_Load);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridview1, 0);
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
        private UserControls.OperatorComboBox comOperator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private UserControls.StationNameComboBox comWorkStation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeginDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStationID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colInPark;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colNotCheckOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
    }
}
