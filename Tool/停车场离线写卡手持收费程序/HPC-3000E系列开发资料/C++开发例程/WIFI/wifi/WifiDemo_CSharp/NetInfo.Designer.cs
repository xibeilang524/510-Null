namespace WifiDemo_CSharp
{
    partial class NetInfo
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtSSID = new System.Windows.Forms.TextBox();
            this.txtMAC = new System.Windows.Forms.TextBox();
            this.txtRSSI = new System.Windows.Forms.TextBox();
            this.txtPrivacy = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.bnConnect = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "网络名称";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "MAC地址";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "信号强度";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "加密方式";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "网络密钥";
            // 
            // txtSSID
            // 
            this.txtSSID.Location = new System.Drawing.Point(83, 17);
            this.txtSSID.Name = "txtSSID";
            this.txtSSID.ReadOnly = true;
            this.txtSSID.Size = new System.Drawing.Size(116, 23);
            this.txtSSID.TabIndex = 5;
            // 
            // txtMAC
            // 
            this.txtMAC.Location = new System.Drawing.Point(83, 50);
            this.txtMAC.Name = "txtMAC";
            this.txtMAC.ReadOnly = true;
            this.txtMAC.Size = new System.Drawing.Size(116, 23);
            this.txtMAC.TabIndex = 5;
            // 
            // txtRSSI
            // 
            this.txtRSSI.Location = new System.Drawing.Point(83, 86);
            this.txtRSSI.Name = "txtRSSI";
            this.txtRSSI.ReadOnly = true;
            this.txtRSSI.Size = new System.Drawing.Size(116, 23);
            this.txtRSSI.TabIndex = 5;
            // 
            // txtPrivacy
            // 
            this.txtPrivacy.Location = new System.Drawing.Point(83, 119);
            this.txtPrivacy.Name = "txtPrivacy";
            this.txtPrivacy.ReadOnly = true;
            this.txtPrivacy.Size = new System.Drawing.Size(116, 23);
            this.txtPrivacy.TabIndex = 5;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(83, 158);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(116, 23);
            this.txtKey.TabIndex = 5;
            // 
            // bnConnect
            // 
            this.bnConnect.Location = new System.Drawing.Point(23, 211);
            this.bnConnect.Name = "bnConnect";
            this.bnConnect.Size = new System.Drawing.Size(72, 27);
            this.bnConnect.TabIndex = 6;
            this.bnConnect.Text = "连接";
            this.bnConnect.Click += new System.EventHandler(this.bnConnect_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Location = new System.Drawing.Point(114, 211);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(72, 27);
            this.bnCancel.TabIndex = 6;
            this.bnCancel.Text = "取消";
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // NetInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(208, 255);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnConnect);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtPrivacy);
            this.Controls.Add(this.txtRSSI);
            this.Controls.Add(this.txtMAC);
            this.Controls.Add(this.txtSSID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.Name = "NetInfo";
            this.Text = "网络信息";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtSSID;
        public System.Windows.Forms.TextBox txtMAC;
        public System.Windows.Forms.TextBox txtRSSI;
        public System.Windows.Forms.TextBox txtPrivacy;
        public System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button bnConnect;
        private System.Windows.Forms.Button bnCancel;
    }
}