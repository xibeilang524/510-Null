namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmAlarmReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlarmReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.customDataGridview1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colAlarmDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlarmDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.alarmTypeComboBox1 = new Ralid.Park.UserControls.AlarmTypeComboBox(this.components);
            this.entranceComboBox1 = new Ralid.Park.UserControls.EntranceComboBox(this.components);
            this.operatorCombobox1 = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.colAlarmDateTime,
            this.colAlarmSource,
            this.colAlarmType,
            this.colAlarmDescr,
            this.colOperatorID});
            this.customDataGridview1.Name = "customDataGridview1";
            this.customDataGridview1.RowHeadersVisible = false;
            this.customDataGridview1.RowTemplate.Height = 23;
            this.customDataGridview1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridview1_CellDoubleClick);
            // 
            // colAlarmDateTime
            // 
            this.colAlarmDateTime.DataPropertyName = "AlarmDateTime";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colAlarmDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colAlarmDateTime, "colAlarmDateTime");
            this.colAlarmDateTime.Name = "colAlarmDateTime";
            this.colAlarmDateTime.ReadOnly = true;
            // 
            // colAlarmSource
            // 
            this.colAlarmSource.DataPropertyName = "AlarmSource";
            resources.ApplyResources(this.colAlarmSource, "colAlarmSource");
            this.colAlarmSource.Name = "colAlarmSource";
            this.colAlarmSource.ReadOnly = true;
            // 
            // colAlarmType
            // 
            this.colAlarmType.DataPropertyName = "AlarmType";
            resources.ApplyResources(this.colAlarmType, "colAlarmType");
            this.colAlarmType.Name = "colAlarmType";
            this.colAlarmType.ReadOnly = true;
            // 
            // colAlarmDescr
            // 
            this.colAlarmDescr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAlarmDescr.DataPropertyName = "AlarmDescr";
            resources.ApplyResources(this.colAlarmDescr, "colAlarmDescr");
            this.colAlarmDescr.Name = "colAlarmDescr";
            this.colAlarmDescr.ReadOnly = true;
            // 
            // colOperatorID
            // 
            this.colOperatorID.DataPropertyName = "OperatorID";
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
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
            this.groupBox2.Controls.Add(this.alarmTypeComboBox1);
            this.groupBox2.Controls.Add(this.entranceComboBox1);
            this.groupBox2.Controls.Add(this.operatorCombobox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // alarmTypeComboBox1
            // 
            this.alarmTypeComboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.alarmTypeComboBox1, "alarmTypeComboBox1");
            this.alarmTypeComboBox1.Name = "alarmTypeComboBox1";
            // 
            // entranceComboBox1
            // 
            this.entranceComboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.entranceComboBox1, "entranceComboBox1");
            this.entranceComboBox1.Name = "entranceComboBox1";
            // 
            // operatorCombobox1
            // 
            this.operatorCombobox1.FormattingEnabled = true;
            resources.ApplyResources(this.operatorCombobox1, "operatorCombobox1");
            this.operatorCombobox1.Name = "operatorCombobox1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // FrmAlarmReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customDataGridview1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmAlarmReport";
            this.Load += new System.EventHandler(this.FrmAlarmReport_Load);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
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

        private Ralid.Park.UserControls.CustomDataGridView customDataGridview1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Ralid.Park.UserControls.OperatorComboBox operatorCombobox1;
        private Ralid.Park.UserControls.EntranceComboBox entranceComboBox1;
        private Ralid.Park.UserControls.AlarmTypeComboBox alarmTypeComboBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlarmDescr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
    }
}