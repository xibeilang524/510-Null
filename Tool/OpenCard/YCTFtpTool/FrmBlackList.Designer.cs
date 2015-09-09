namespace Ralid.OpenCard.YCTFtpTool
{
    partial class FrmBlackList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colLCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCatchAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUploadFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExport = new System.Windows.Forms.Button();
            this.chkCatched = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(441, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(123, 34);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "逻辑卡号";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(72, 18);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(199, 21);
            this.txtCardID.TabIndex = 41;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLCN,
            this.colFCN,
            this.colReason,
            this.colCatchAt,
            this.colUploadFile});
            this.dataGridView1.Location = new System.Drawing.Point(0, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(757, 336);
            this.dataGridView1.TabIndex = 42;
            // 
            // colLCN
            // 
            this.colLCN.HeaderText = "逻辑卡号";
            this.colLCN.MinimumWidth = 150;
            this.colLCN.Name = "colLCN";
            this.colLCN.ReadOnly = true;
            this.colLCN.Width = 150;
            // 
            // colFCN
            // 
            this.colFCN.HeaderText = "物理卡号";
            this.colFCN.MinimumWidth = 150;
            this.colFCN.Name = "colFCN";
            this.colFCN.ReadOnly = true;
            this.colFCN.Width = 150;
            // 
            // colReason
            // 
            this.colReason.HeaderText = "禁用原因";
            this.colReason.Name = "colReason";
            this.colReason.ReadOnly = true;
            this.colReason.Width = 150;
            // 
            // colCatchAt
            // 
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colCatchAt.DefaultCellStyle = dataGridViewCellStyle1;
            this.colCatchAt.HeaderText = "捕捉";
            this.colCatchAt.Name = "colCatchAt";
            this.colCatchAt.ReadOnly = true;
            this.colCatchAt.Width = 130;
            // 
            // colUploadFile
            // 
            this.colUploadFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUploadFile.HeaderText = "上传文件";
            this.colUploadFile.Name = "colUploadFile";
            this.colUploadFile.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 396);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(757, 22);
            this.statusStrip1.TabIndex = 43;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMsg
            // 
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(742, 17);
            this.lblMsg.Spring = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(585, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(123, 34);
            this.btnExport.TabIndex = 44;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // chkCatched
            // 
            this.chkCatched.AutoSize = true;
            this.chkCatched.Location = new System.Drawing.Point(278, 22);
            this.chkCatched.Name = "chkCatched";
            this.chkCatched.Size = new System.Drawing.Size(144, 16);
            this.chkCatched.TabIndex = 45;
            this.chkCatched.Text = "只显示已捕捉的黑名单";
            this.chkCatched.UseVisualStyleBackColor = true;
            // 
            // FrmBlackList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 418);
            this.Controls.Add(this.chkCatched);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Name = "FrmBlackList";
            this.Text = "黑名单查询";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatchAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUploadFile;
        private System.Windows.Forms.CheckBox chkCatched;
    }
}