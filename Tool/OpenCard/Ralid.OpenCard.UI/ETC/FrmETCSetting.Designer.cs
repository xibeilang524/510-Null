namespace Ralid.OpenCard.UI.ETC
{
    partial class FrmETCSetting
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
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvinceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCityNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAreaNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGateNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLaneNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEcRSUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEcReaderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeartBeatTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设置停车场通道ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReadSameCardInterval = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.chkETCCardReaderEnable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIP,
            this.colPort,
            this.colUserName,
            this.colPassword,
            this.colProvinceNo,
            this.colCityNo,
            this.colAreaNo,
            this.colGateNo,
            this.colLaneNo,
            this.colEcRSUID,
            this.colEcReaderID,
            this.colTimeout,
            this.colHeartBeatTime,
            this.colState,
            this.colEntrance});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(0, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1001, 314);
            this.dataGridView1.TabIndex = 3;
            // 
            // colIP
            // 
            this.colIP.HeaderText = "IP";
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            // 
            // colPort
            // 
            this.colPort.HeaderText = "端口号";
            this.colPort.Name = "colPort";
            this.colPort.ReadOnly = true;
            this.colPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPort.Width = 60;
            // 
            // colUserName
            // 
            this.colUserName.HeaderText = "用户名称";
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            this.colUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUserName.Visible = false;
            // 
            // colPassword
            // 
            this.colPassword.HeaderText = "密码";
            this.colPassword.Name = "colPassword";
            this.colPassword.ReadOnly = true;
            this.colPassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPassword.Visible = false;
            // 
            // colProvinceNo
            // 
            this.colProvinceNo.HeaderText = "省份编号";
            this.colProvinceNo.Name = "colProvinceNo";
            this.colProvinceNo.ReadOnly = true;
            this.colProvinceNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colProvinceNo.Width = 80;
            // 
            // colCityNo
            // 
            this.colCityNo.HeaderText = "城市编号";
            this.colCityNo.Name = "colCityNo";
            this.colCityNo.ReadOnly = true;
            this.colCityNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCityNo.Width = 80;
            // 
            // colAreaNo
            // 
            this.colAreaNo.HeaderText = "小区编号";
            this.colAreaNo.Name = "colAreaNo";
            this.colAreaNo.ReadOnly = true;
            this.colAreaNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAreaNo.Width = 80;
            // 
            // colGateNo
            // 
            this.colGateNo.HeaderText = "大门编号";
            this.colGateNo.Name = "colGateNo";
            this.colGateNo.ReadOnly = true;
            this.colGateNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colGateNo.Width = 80;
            // 
            // colLaneNo
            // 
            this.colLaneNo.HeaderText = "车道编号";
            this.colLaneNo.Name = "colLaneNo";
            this.colLaneNo.ReadOnly = true;
            this.colLaneNo.Width = 80;
            // 
            // colEcRSUID
            // 
            this.colEcRSUID.HeaderText = "天线ID";
            this.colEcRSUID.Name = "colEcRSUID";
            this.colEcRSUID.ReadOnly = true;
            this.colEcRSUID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEcRSUID.Width = 80;
            // 
            // colEcReaderID
            // 
            this.colEcReaderID.HeaderText = "读卡器ID";
            this.colEcReaderID.Name = "colEcReaderID";
            this.colEcReaderID.ReadOnly = true;
            this.colEcReaderID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEcReaderID.Width = 80;
            // 
            // colTimeout
            // 
            this.colTimeout.HeaderText = "超时时间";
            this.colTimeout.Name = "colTimeout";
            this.colTimeout.ReadOnly = true;
            this.colTimeout.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTimeout.Visible = false;
            this.colTimeout.Width = 60;
            // 
            // colHeartBeatTime
            // 
            this.colHeartBeatTime.HeaderText = "心跳时间";
            this.colHeartBeatTime.Name = "colHeartBeatTime";
            this.colHeartBeatTime.ReadOnly = true;
            this.colHeartBeatTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colHeartBeatTime.Visible = false;
            this.colHeartBeatTime.Width = 60;
            // 
            // colState
            // 
            this.colState.HeaderText = "状态";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            this.colState.Width = 70;
            // 
            // colEntrance
            // 
            this.colEntrance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colEntrance.HeaderText = "停车场通道";
            this.colEntrance.MinimumWidth = 200;
            this.colEntrance.Name = "colEntrance";
            this.colEntrance.ReadOnly = true;
            this.colEntrance.Width = 200;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置停车场通道ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 26);
            // 
            // 设置停车场通道ToolStripMenuItem
            // 
            this.设置停车场通道ToolStripMenuItem.Name = "设置停车场通道ToolStripMenuItem";
            this.设置停车场通道ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.设置停车场通道ToolStripMenuItem.Text = "设置停车场通道";
            this.设置停车场通道ToolStripMenuItem.Click += new System.EventHandler(this.设置停车场通道ToolStripMenuItem_Click);
            // 
            // chkEnable
            // 
            this.chkEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnable.AutoSize = true;
            this.chkEnable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEnable.ForeColor = System.Drawing.Color.Red;
            this.chkEnable.Location = new System.Drawing.Point(786, 342);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(95, 20);
            this.chkEnable.TabIndex = 18;
            this.chkEnable.Text = "启动服务";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(887, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 34);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCount.ForeColor = System.Drawing.Color.Blue;
            this.lblCount.Location = new System.Drawing.Point(13, 343);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 16);
            this.lblCount.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(545, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "同一张卡读卡间隔（秒）";
            // 
            // txtReadSameCardInterval
            // 
            this.txtReadSameCardInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReadSameCardInterval.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtReadSameCardInterval.Location = new System.Drawing.Point(687, 342);
            this.txtReadSameCardInterval.MaxValue = 100;
            this.txtReadSameCardInterval.MinValue = 1;
            this.txtReadSameCardInterval.Name = "txtReadSameCardInterval";
            this.txtReadSameCardInterval.NumberWithCommas = false;
            this.txtReadSameCardInterval.Size = new System.Drawing.Size(69, 21);
            this.txtReadSameCardInterval.TabIndex = 22;
            this.txtReadSameCardInterval.Text = "30";
            // 
            // chkETCCardReaderEnable
            // 
            this.chkETCCardReaderEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkETCCardReaderEnable.AutoSize = true;
            this.chkETCCardReaderEnable.Location = new System.Drawing.Point(408, 345);
            this.chkETCCardReaderEnable.Name = "chkETCCardReaderEnable";
            this.chkETCCardReaderEnable.Size = new System.Drawing.Size(102, 16);
            this.chkETCCardReaderEnable.TabIndex = 23;
            this.chkETCCardReaderEnable.Text = "启用ETC读卡器";
            this.chkETCCardReaderEnable.UseVisualStyleBackColor = true;
            // 
            // FrmETCSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 390);
            this.Controls.Add(this.chkETCCardReaderEnable);
            this.Controls.Add(this.txtReadSameCardInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.chkEnable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmETCSetting";
            this.Text = "粤通卡设置";
            this.Load += new System.EventHandler(this.FrmETCSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置停车场通道ToolStripMenuItem;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvinceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCityNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAreaNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGateNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLaneNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEcRSUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEcReaderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeout;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeartBeatTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrance;
        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.IntergerTextBox txtReadSameCardInterval;
        private System.Windows.Forms.CheckBox chkETCCardReaderEnable;
    }
}