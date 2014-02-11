namespace WifiDemo_CSharp
{
    partial class WifiDemo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNICName = new System.Windows.Forms.TextBox();
            this.txtAPCnt = new System.Windows.Forms.TextBox();
            this.txtAssociatedSSID = new System.Windows.Forms.TextBox();
            this.bnRefresh = new System.Windows.Forms.Button();
            this.bnDisconnect = new System.Windows.Forms.Button();
            this.bnConfig = new System.Windows.Forms.Button();
            this.dgWirelessAP = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "网卡名称";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "AP个数";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "已连接到";
            // 
            // txtNICName
            // 
            this.txtNICName.Location = new System.Drawing.Point(79, 9);
            this.txtNICName.Name = "txtNICName";
            this.txtNICName.ReadOnly = true;
            this.txtNICName.Size = new System.Drawing.Size(144, 23);
            this.txtNICName.TabIndex = 3;
            // 
            // txtAPCnt
            // 
            this.txtAPCnt.AcceptsReturn = true;
            this.txtAPCnt.Location = new System.Drawing.Point(79, 48);
            this.txtAPCnt.Multiline = true;
            this.txtAPCnt.Name = "txtAPCnt";
            this.txtAPCnt.Size = new System.Drawing.Size(144, 23);
            this.txtAPCnt.TabIndex = 3;
            // 
            // txtAssociatedSSID
            // 
            this.txtAssociatedSSID.Location = new System.Drawing.Point(79, 87);
            this.txtAssociatedSSID.Multiline = true;
            this.txtAssociatedSSID.Name = "txtAssociatedSSID";
            this.txtAssociatedSSID.Size = new System.Drawing.Size(144, 23);
            this.txtAssociatedSSID.TabIndex = 3;
            // 
            // bnRefresh
            // 
            this.bnRefresh.Location = new System.Drawing.Point(16, 254);
            this.bnRefresh.Name = "bnRefresh";
            this.bnRefresh.Size = new System.Drawing.Size(50, 29);
            this.bnRefresh.TabIndex = 5;
            this.bnRefresh.Text = "刷新";
            this.bnRefresh.Click += new System.EventHandler(this.bnRefresh_Click);
            // 
            // bnDisconnect
            // 
            this.bnDisconnect.Location = new System.Drawing.Point(72, 254);
            this.bnDisconnect.Name = "bnDisconnect";
            this.bnDisconnect.Size = new System.Drawing.Size(50, 29);
            this.bnDisconnect.TabIndex = 5;
            this.bnDisconnect.Text = "断开";
            this.bnDisconnect.Click += new System.EventHandler(this.bnDisconnect_Click);
            // 
            // bnConfig
            // 
            this.bnConfig.Location = new System.Drawing.Point(145, 254);
            this.bnConfig.Name = "bnConfig";
            this.bnConfig.Size = new System.Drawing.Size(78, 29);
            this.bnConfig.TabIndex = 5;
            this.bnConfig.Text = "配置WZC...";
            this.bnConfig.Click += new System.EventHandler(this.bnConfig_Click);
            // 
            // dgWirelessAP
            // 
            this.dgWirelessAP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgWirelessAP.ColumnHeadersVisible = false;
            this.dgWirelessAP.Location = new System.Drawing.Point(13, 121);
            this.dgWirelessAP.Name = "dgWirelessAP";
            this.dgWirelessAP.RowHeadersVisible = false;
            this.dgWirelessAP.Size = new System.Drawing.Size(210, 122);
            this.dgWirelessAP.TabIndex = 9;
            this.dgWirelessAP.DoubleClick += new System.EventHandler(this.dgWirelessAP_DoubleClick);
            // 
            // WifiDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.dgWirelessAP);
            this.Controls.Add(this.bnConfig);
            this.Controls.Add(this.bnDisconnect);
            this.Controls.Add(this.bnRefresh);
            this.Controls.Add(this.txtAssociatedSSID);
            this.Controls.Add(this.txtAPCnt);
            this.Controls.Add(this.txtNICName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WifiDemo";
            this.Text = "WifiDemo";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNICName;
        private System.Windows.Forms.TextBox txtAPCnt;
        private System.Windows.Forms.TextBox txtAssociatedSSID;
        private System.Windows.Forms.Button bnRefresh;
        private System.Windows.Forms.Button bnDisconnect;
        private System.Windows.Forms.Button bnConfig;
        private System.Windows.Forms.DataGrid dgWirelessAP;
    }
}

