namespace WifiDemo_CSharp
{
    partial class WZCConfig
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtRefreshInterval = new System.Windows.Forms.TextBox();
            this.txtAssociateTimeout = new System.Windows.Forms.TextBox();
            this.txtIntervalOnConn = new System.Windows.Forms.TextBox();
            this.txtIntervalOnDisconn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bnSet = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "刷新间隔";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.Text = "已关联时刷新间隔";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "关联超时";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 20);
            this.label4.Text = "未关联时刷新间隔";
            // 
            // txtRefreshInterval
            // 
            this.txtRefreshInterval.Location = new System.Drawing.Point(79, 21);
            this.txtRefreshInterval.Name = "txtRefreshInterval";
            this.txtRefreshInterval.Size = new System.Drawing.Size(86, 23);
            this.txtRefreshInterval.TabIndex = 4;
            this.txtRefreshInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // txtAssociateTimeout
            // 
            this.txtAssociateTimeout.Location = new System.Drawing.Point(79, 63);
            this.txtAssociateTimeout.Name = "txtAssociateTimeout";
            this.txtAssociateTimeout.Size = new System.Drawing.Size(86, 23);
            this.txtAssociateTimeout.TabIndex = 4;
            this.txtAssociateTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // txtIntervalOnConn
            // 
            this.txtIntervalOnConn.Location = new System.Drawing.Point(79, 130);
            this.txtIntervalOnConn.Name = "txtIntervalOnConn";
            this.txtIntervalOnConn.Size = new System.Drawing.Size(86, 23);
            this.txtIntervalOnConn.TabIndex = 4;
            this.txtIntervalOnConn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // txtIntervalOnDisconn
            // 
            this.txtIntervalOnDisconn.Location = new System.Drawing.Point(79, 188);
            this.txtIntervalOnDisconn.Name = "txtIntervalOnDisconn";
            this.txtIntervalOnDisconn.Size = new System.Drawing.Size(86, 23);
            this.txtIntervalOnDisconn.TabIndex = 4;
            this.txtIntervalOnDisconn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(174, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 20);
            this.label5.Text = "ms";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(174, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 20);
            this.label6.Text = "ms";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(174, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 20);
            this.label7.Text = "ms";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(174, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 20);
            this.label8.Text = "ms";
            // 
            // bnSet
            // 
            this.bnSet.Location = new System.Drawing.Point(20, 217);
            this.bnSet.Name = "bnSet";
            this.bnSet.Size = new System.Drawing.Size(72, 27);
            this.bnSet.TabIndex = 9;
            this.bnSet.Text = "设置";
            this.bnSet.Click += new System.EventHandler(this.bnSet_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Location = new System.Drawing.Point(117, 217);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(72, 27);
            this.bnCancel.TabIndex = 9;
            this.bnCancel.Text = "取消";
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // WZCConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(208, 255);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnSet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIntervalOnDisconn);
            this.Controls.Add(this.txtIntervalOnConn);
            this.Controls.Add(this.txtAssociateTimeout);
            this.Controls.Add(this.txtRefreshInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.Name = "WZCConfig";
            this.Text = "WZCConfig";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtRefreshInterval;
        public System.Windows.Forms.TextBox txtAssociateTimeout;
        public System.Windows.Forms.TextBox txtIntervalOnConn;
        public System.Windows.Forms.TextBox txtIntervalOnDisconn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bnSet;
        private System.Windows.Forms.Button bnCancel;
    }
}