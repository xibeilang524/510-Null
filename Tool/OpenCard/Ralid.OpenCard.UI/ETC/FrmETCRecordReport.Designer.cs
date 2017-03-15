namespace Ralid.OpenCard.UI.ETC
{
    partial class FrmETCRecordReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUnuploaded = new System.Windows.Forms.CheckBox();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colListNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLaneNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUploadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarplate = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(413, 27);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(413, 56);
            // 
            // ucDateTimeInterval1
            // 
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2012, 9, 14, 15, 12, 56, 827);
            this.ucDateTimeInterval1.Location = new System.Drawing.Point(7, 15);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.Size = new System.Drawing.Size(221, 74);
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2012, 9, 14, 15, 12, 56, 830);
            this.ucDateTimeInterval1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 93);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "记录时间";
            // 
            // chkUnuploaded
            // 
            this.chkUnuploaded.AutoSize = true;
            this.chkUnuploaded.Checked = true;
            this.chkUnuploaded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUnuploaded.ForeColor = System.Drawing.Color.Red;
            this.chkUnuploaded.Location = new System.Drawing.Point(266, 27);
            this.chkUnuploaded.Name = "chkUnuploaded";
            this.chkUnuploaded.Size = new System.Drawing.Size(132, 16);
            this.chkUnuploaded.TabIndex = 8;
            this.chkUnuploaded.Text = "只显示未上传的记录";
            this.chkUnuploaded.UseVisualStyleBackColor = true;
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colListNo,
            this.colLaneNo,
            this.colAddTime,
            this.colCardID,
            this.colCarplate,
            this.colPayment,
            this.colBalance,
            this.colUploadTime,
            this.colFill});
            this.customDataGridView1.Location = new System.Drawing.Point(0, 111);
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customDataGridView1.Size = new System.Drawing.Size(978, 272);
            this.customDataGridView1.TabIndex = 9;
            // 
            // colListNo
            // 
            this.colListNo.HeaderText = "流水号";
            this.colListNo.Name = "colListNo";
            this.colListNo.ReadOnly = true;
            this.colListNo.Width = 150;
            // 
            // colLaneNo
            // 
            this.colLaneNo.HeaderText = "通道号";
            this.colLaneNo.Name = "colLaneNo";
            this.colLaneNo.ReadOnly = true;
            this.colLaneNo.Width = 80;
            // 
            // colAddTime
            // 
            this.colAddTime.HeaderText = "生成时间";
            this.colAddTime.Name = "colAddTime";
            this.colAddTime.ReadOnly = true;
            this.colAddTime.Width = 130;
            // 
            // colCardID
            // 
            this.colCardID.HeaderText = "卡号";
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            this.colCardID.Width = 150;
            // 
            // colCarplate
            // 
            this.colCarplate.HeaderText = "车牌号";
            this.colCarplate.Name = "colCarplate";
            this.colCarplate.ReadOnly = true;
            // 
            // colPayment
            // 
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.colPayment.DefaultCellStyle = dataGridViewCellStyle3;
            this.colPayment.HeaderText = "扣款金额";
            this.colPayment.Name = "colPayment";
            this.colPayment.ReadOnly = true;
            // 
            // colBalance
            // 
            dataGridViewCellStyle4.Format = "C2";
            this.colBalance.DefaultCellStyle = dataGridViewCellStyle4;
            this.colBalance.HeaderText = "卡片余额";
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // colUploadTime
            // 
            this.colUploadTime.HeaderText = "上传时间";
            this.colUploadTime.Name = "colUploadTime";
            this.colUploadTime.ReadOnly = true;
            this.colUploadTime.Width = 130;
            // 
            // colFill
            // 
            this.colFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFill.HeaderText = "";
            this.colFill.Name = "colFill";
            this.colFill.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "车牌";
            // 
            // txtCarplate
            // 
            this.txtCarplate.Location = new System.Drawing.Point(298, 63);
            this.txtCarplate.Name = "txtCarplate";
            this.txtCarplate.Size = new System.Drawing.Size(100, 21);
            this.txtCarplate.TabIndex = 11;
            this.txtCarplate.TextChanged += new System.EventHandler(this.txtCarplate_TextChanged);
            // 
            // FrmETCRecordReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 408);
            this.Controls.Add(this.txtCarplate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.chkUnuploaded);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmETCRecordReport";
            this.Text = "ETC消费记录报表";
            this.Load += new System.EventHandler(this.FrmETCRecordReport_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.chkUnuploaded, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtCarplate, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkUnuploaded;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colListNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLaneNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUploadTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCarplate;
    }
}