namespace Ralid.Park.UI
{
    partial class FrmAPMMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAPMMaster));
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colSerialNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActiveDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSerialNum,
            this.colIP,
            this.colMAC,
            this.colStatus,
            this.colActiveDateTime,
            this.colMemo});
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colSerialNum
            // 
            resources.ApplyResources(this.colSerialNum, "colSerialNum");
            this.colSerialNum.Name = "colSerialNum";
            this.colSerialNum.ReadOnly = true;
            // 
            // colIP
            // 
            resources.ApplyResources(this.colIP, "colIP");
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            // 
            // colMAC
            // 
            resources.ApplyResources(this.colMAC, "colMAC");
            this.colMAC.Name = "colMAC";
            this.colMAC.ReadOnly = true;
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colActiveDateTime
            // 
            resources.ApplyResources(this.colActiveDateTime, "colActiveDateTime");
            this.colActiveDateTime.Name = "colActiveDateTime";
            this.colActiveDateTime.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // FrmAPMMaster
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customDataGridView1);
            this.Name = "FrmAPMMaster";
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerialNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActiveDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
    }
}