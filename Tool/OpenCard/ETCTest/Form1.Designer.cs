namespace ETCTest
{
    partial class Form1
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
            this.btnInit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colLaneNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvinceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCityNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAreaNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGateNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEcRSUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEcReaderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeartBeatTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.天线扣费ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.读卡器扣费ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(25, 25);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(115, 29);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
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
            this.colLaneNo,
            this.colIP,
            this.colPort,
            this.colUserName,
            this.colPassword,
            this.colProvinceNo,
            this.colCityNo,
            this.colAreaNo,
            this.colGateNo,
            this.colEcRSUID,
            this.colEcReaderID,
            this.colTimeout,
            this.colHeartBeatTime,
            this.colState});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(3, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1028, 272);
            this.dataGridView1.TabIndex = 2;
            // 
            // colLaneNo
            // 
            this.colLaneNo.HeaderText = "车道编号";
            this.colLaneNo.Name = "colLaneNo";
            this.colLaneNo.ReadOnly = true;
            this.colLaneNo.Width = 80;
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
            // 
            // colPassword
            // 
            this.colPassword.HeaderText = "密码";
            this.colPassword.Name = "colPassword";
            this.colPassword.ReadOnly = true;
            this.colPassword.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProvinceNo
            // 
            this.colProvinceNo.HeaderText = "省份编号";
            this.colProvinceNo.Name = "colProvinceNo";
            this.colProvinceNo.ReadOnly = true;
            this.colProvinceNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colProvinceNo.Width = 60;
            // 
            // colCityNo
            // 
            this.colCityNo.HeaderText = "城市编号";
            this.colCityNo.Name = "colCityNo";
            this.colCityNo.ReadOnly = true;
            this.colCityNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCityNo.Width = 60;
            // 
            // colAreaNo
            // 
            this.colAreaNo.HeaderText = "小区编号";
            this.colAreaNo.Name = "colAreaNo";
            this.colAreaNo.ReadOnly = true;
            this.colAreaNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAreaNo.Width = 60;
            // 
            // colGateNo
            // 
            this.colGateNo.HeaderText = "大门编号";
            this.colGateNo.Name = "colGateNo";
            this.colGateNo.ReadOnly = true;
            this.colGateNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colGateNo.Width = 60;
            // 
            // colEcRSUID
            // 
            this.colEcRSUID.HeaderText = "天线ID";
            this.colEcRSUID.Name = "colEcRSUID";
            this.colEcRSUID.ReadOnly = true;
            this.colEcRSUID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEcRSUID.Width = 60;
            // 
            // colEcReaderID
            // 
            this.colEcReaderID.HeaderText = "读卡器ID";
            this.colEcReaderID.Name = "colEcReaderID";
            this.colEcReaderID.ReadOnly = true;
            this.colEcReaderID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEcReaderID.Width = 60;
            // 
            // colTimeout
            // 
            this.colTimeout.HeaderText = "超时时间";
            this.colTimeout.Name = "colTimeout";
            this.colTimeout.ReadOnly = true;
            this.colTimeout.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTimeout.Width = 60;
            // 
            // colHeartBeatTime
            // 
            this.colHeartBeatTime.HeaderText = "心跳时间";
            this.colHeartBeatTime.Name = "colHeartBeatTime";
            this.colHeartBeatTime.ReadOnly = true;
            this.colHeartBeatTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colHeartBeatTime.Width = 60;
            // 
            // colState
            // 
            this.colState.HeaderText = "状态";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.天线扣费ToolStripMenuItem,
            this.读卡器扣费ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // 天线扣费ToolStripMenuItem
            // 
            this.天线扣费ToolStripMenuItem.Name = "天线扣费ToolStripMenuItem";
            this.天线扣费ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.天线扣费ToolStripMenuItem.Text = "天线扣费";
            this.天线扣费ToolStripMenuItem.Click += new System.EventHandler(this.天线扣费ToolStripMenuItem_Click);
            // 
            // 读卡器扣费ToolStripMenuItem
            // 
            this.读卡器扣费ToolStripMenuItem.Name = "读卡器扣费ToolStripMenuItem";
            this.读卡器扣费ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.读卡器扣费ToolStripMenuItem.Text = "读卡器扣费";
            this.读卡器扣费ToolStripMenuItem.Click += new System.EventHandler(this.读卡器扣费ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 348);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1036, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblCount
            // 
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(1021, 17);
            this.lblCount.Spring = true;
            this.lblCount.Text = "共有 0 项";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 370);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnInit);
            this.Name = "Form1";
            this.Text = "ETC设备测试程序";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCount;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 天线扣费ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 读卡器扣费ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLaneNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvinceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCityNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAreaNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGateNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEcRSUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEcReaderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeout;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeartBeatTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
    }
}

