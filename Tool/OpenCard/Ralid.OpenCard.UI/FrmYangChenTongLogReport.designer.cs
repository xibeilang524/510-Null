namespace Ralid.OpenCard.UI
{
    partial class FrmYangChenTongLogReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmYangChenTongLogReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.btnExportToRecord = new System.Windows.Forms.Button();
            this.colLogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogicalID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
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
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2012, 9, 14, 15, 12, 56, 827);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2012, 9, 14, 15, 12, 56, 830);
            // 
            // customDataGridView1
            // 
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLogID,
            this.colLogDateTime,
            this.colCardID,
            this.colLogicalID,
            this.colPayment,
            this.colBalance,
            this.colFill});
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnExportToRecord
            // 
            resources.ApplyResources(this.btnExportToRecord, "btnExportToRecord");
            this.btnExportToRecord.Name = "btnExportToRecord";
            this.btnExportToRecord.UseVisualStyleBackColor = true;
            this.btnExportToRecord.Click += new System.EventHandler(this.btnExportToRecord_Click);
            // 
            // colLogID
            // 
            resources.ApplyResources(this.colLogID, "colLogID");
            this.colLogID.Name = "colLogID";
            this.colLogID.ReadOnly = true;
            // 
            // colLogDateTime
            // 
            resources.ApplyResources(this.colLogDateTime, "colLogDateTime");
            this.colLogDateTime.Name = "colLogDateTime";
            this.colLogDateTime.ReadOnly = true;
            // 
            // colCardID
            // 
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colLogicalID
            // 
            resources.ApplyResources(this.colLogicalID, "colLogicalID");
            this.colLogicalID.Name = "colLogicalID";
            this.colLogicalID.ReadOnly = true;
            // 
            // colPayment
            // 
            resources.ApplyResources(this.colPayment, "colPayment");
            this.colPayment.Name = "colPayment";
            this.colPayment.ReadOnly = true;
            // 
            // colBalance
            // 
            resources.ApplyResources(this.colBalance, "colBalance");
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // colFill
            // 
            this.colFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colFill, "colFill");
            this.colFill.Name = "colFill";
            this.colFill.ReadOnly = true;
            // 
            // FrmYangChenTongLogReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExportToRecord);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmYangChenTongLogReport";
            this.Load += new System.EventHandler(this.FrmYangChenTongLogReport_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.btnExportToRecord, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.Button btnExportToRecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogicalID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFill;
    }
}