namespace Ralid.OpenCard.YCTFtpTool
{
    partial class FrmPayment
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UCChargeDateTime = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWalletType = new System.Windows.Forms.ComboBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkUnupload = new System.Windows.Forms.CheckBox();
            this.colPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWalletType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTIM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPID,
            this.colPSN,
            this.colCardID,
            this.colWalletType,
            this.colFee,
            this.colBAL,
            this.colTIM,
            this.colState,
            this.colFile});
            this.dataGridView1.Location = new System.Drawing.Point(0, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1125, 373);
            this.dataGridView1.TabIndex = 43;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 497);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1125, 22);
            this.statusStrip1.TabIndex = 44;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMsg
            // 
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(1110, 17);
            this.lblMsg.Spring = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(241, 105);
            this.groupBox4.TabIndex = 45;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "消费时间";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UCChargeDateTime);
            this.groupBox1.Location = new System.Drawing.Point(7, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 75);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // UCChargeDateTime
            // 
            this.UCChargeDateTime.EndDateTime = new System.DateTime(2010, 1, 9, 23, 59, 59, 0);
            this.UCChargeDateTime.Location = new System.Drawing.Point(3, -4);
            this.UCChargeDateTime.Name = "UCChargeDateTime";
            this.UCChargeDateTime.ShowTime = true;
            this.UCChargeDateTime.Size = new System.Drawing.Size(221, 74);
            this.UCChargeDateTime.StartDateTime = new System.DateTime(2010, 1, 9, 16, 56, 56, 625);
            this.UCChargeDateTime.TabIndex = 0;
            // 
            // txtCardID
            // 
            this.txtCardID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardID.Location = new System.Drawing.Point(342, 13);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(155, 21);
            this.txtCardID.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(291, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "卡号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(267, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "钱包类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(291, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 49;
            this.label3.Text = "状态";
            // 
            // cmbWalletType
            // 
            this.cmbWalletType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWalletType.FormattingEnabled = true;
            this.cmbWalletType.Items.AddRange(new object[] {
            "",
            "1-M1钱包",
            "2-CPU钱包"});
            this.cmbWalletType.Location = new System.Drawing.Point(342, 44);
            this.cmbWalletType.Name = "cmbWalletType";
            this.cmbWalletType.Size = new System.Drawing.Size(155, 20);
            this.cmbWalletType.TabIndex = 50;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Items.AddRange(new object[] {
            "",
            "支付成功",
            "服务器已接收",
            "服务器拒绝",
            "支付失败"});
            this.cmbState.Location = new System.Drawing.Point(342, 75);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(155, 20);
            this.cmbState.TabIndex = 51;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(543, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(118, 72);
            this.btnSearch.TabIndex = 52;
            this.btnSearch.Text = "查  询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkUnupload
            // 
            this.chkUnupload.AutoSize = true;
            this.chkUnupload.Location = new System.Drawing.Point(342, 103);
            this.chkUnupload.Name = "chkUnupload";
            this.chkUnupload.Size = new System.Drawing.Size(120, 16);
            this.chkUnupload.TabIndex = 53;
            this.chkUnupload.Text = "只显示未上传记录";
            this.chkUnupload.UseVisualStyleBackColor = true;
            // 
            // colPID
            // 
            this.colPID.HeaderText = "交易设备序列号";
            this.colPID.Name = "colPID";
            this.colPID.ReadOnly = true;
            this.colPID.Width = 150;
            // 
            // colPSN
            // 
            this.colPSN.HeaderText = "交易流水号";
            this.colPSN.Name = "colPSN";
            this.colPSN.ReadOnly = true;
            this.colPSN.Width = 150;
            // 
            // colCardID
            // 
            this.colCardID.HeaderText = "卡号";
            this.colCardID.MinimumWidth = 150;
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            this.colCardID.Width = 150;
            // 
            // colWalletType
            // 
            this.colWalletType.HeaderText = "钱包类型";
            this.colWalletType.Name = "colWalletType";
            this.colWalletType.ReadOnly = true;
            // 
            // colFee
            // 
            this.colFee.HeaderText = "交易金额";
            this.colFee.Name = "colFee";
            this.colFee.ReadOnly = true;
            // 
            // colBAL
            // 
            this.colBAL.HeaderText = "余额";
            this.colBAL.Name = "colBAL";
            this.colBAL.ReadOnly = true;
            // 
            // colTIM
            // 
            this.colTIM.HeaderText = "交易日期";
            this.colTIM.Name = "colTIM";
            this.colTIM.ReadOnly = true;
            this.colTIM.Width = 130;
            // 
            // colState
            // 
            this.colState.HeaderText = "状态";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            // 
            // colFile
            // 
            this.colFile.HeaderText = "上传文件";
            this.colFile.Name = "colFile";
            this.colFile.ReadOnly = true;
            // 
            // FrmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 519);
            this.Controls.Add(this.chkUnupload);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbState);
            this.Controls.Add(this.cmbWalletType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmPayment";
            this.Text = "消费记录";
            this.Load += new System.EventHandler(this.FrmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private Park.UserControls.UCDateTimeInterval UCChargeDateTime;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWalletType;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkUnupload;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWalletType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTIM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFile;
    }
}