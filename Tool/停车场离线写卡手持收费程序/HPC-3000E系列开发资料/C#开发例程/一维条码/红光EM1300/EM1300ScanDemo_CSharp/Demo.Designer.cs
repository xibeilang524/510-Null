namespace EM1300ScanDemo_CSharp
{
    partial class Demo
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
            this.txtShow = new System.Windows.Forms.TextBox();
            this.cbPattern = new System.Windows.Forms.ComboBox();
            this.bnSetPattern = new System.Windows.Forms.Button();
            this.bnStart = new System.Windows.Forms.Button();
            this.bnStop = new System.Windows.Forms.Button();
            this.bnVersion = new System.Windows.Forms.Button();
            this.bnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtShow
            // 
            this.txtShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShow.Location = new System.Drawing.Point(9, 9);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShow.Size = new System.Drawing.Size(221, 195);
            this.txtShow.TabIndex = 0;
            // 
            // cbPattern
            // 
            this.cbPattern.Location = new System.Drawing.Point(20, 212);
            this.cbPattern.Name = "cbPattern";
            this.cbPattern.Size = new System.Drawing.Size(83, 23);
            this.cbPattern.TabIndex = 1;
            // 
            // bnSetPattern
            // 
            this.bnSetPattern.Location = new System.Drawing.Point(135, 213);
            this.bnSetPattern.Name = "bnSetPattern";
            this.bnSetPattern.Size = new System.Drawing.Size(83, 23);
            this.bnSetPattern.TabIndex = 2;
            this.bnSetPattern.Text = "设置模式";
            this.bnSetPattern.Click += new System.EventHandler(this.bnSetPattern_Click);
            // 
            // bnStart
            // 
            this.bnStart.Location = new System.Drawing.Point(20, 242);
            this.bnStart.Name = "bnStart";
            this.bnStart.Size = new System.Drawing.Size(83, 23);
            this.bnStart.TabIndex = 3;
            this.bnStart.Text = "开始扫描";
            this.bnStart.Click += new System.EventHandler(this.bnStart_Click);
            // 
            // bnStop
            // 
            this.bnStop.Location = new System.Drawing.Point(135, 242);
            this.bnStop.Name = "bnStop";
            this.bnStop.Size = new System.Drawing.Size(83, 23);
            this.bnStop.TabIndex = 4;
            this.bnStop.Text = "停止扫描";
            this.bnStop.Click += new System.EventHandler(this.bnStop_Click);
            // 
            // bnVersion
            // 
            this.bnVersion.Location = new System.Drawing.Point(20, 271);
            this.bnVersion.Name = "bnVersion";
            this.bnVersion.Size = new System.Drawing.Size(83, 23);
            this.bnVersion.TabIndex = 5;
            this.bnVersion.Text = "版本";
            this.bnVersion.Click += new System.EventHandler(this.bnVersion_Click);
            // 
            // bnClear
            // 
            this.bnClear.Location = new System.Drawing.Point(135, 271);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(83, 23);
            this.bnClear.TabIndex = 6;
            this.bnClear.Text = "清空窗口";
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.bnClear);
            this.Controls.Add(this.bnVersion);
            this.Controls.Add(this.bnStop);
            this.Controls.Add(this.bnStart);
            this.Controls.Add(this.bnSetPattern);
            this.Controls.Add(this.cbPattern);
            this.Controls.Add(this.txtShow);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "Demo";
            this.Text = "EM1300扫描示例";
            this.TopMost = true;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Demo_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Demo_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.ComboBox cbPattern;
        private System.Windows.Forms.Button bnSetPattern;
        private System.Windows.Forms.Button bnStart;
        private System.Windows.Forms.Button bnStop;
        private System.Windows.Forms.Button bnVersion;
        private System.Windows.Forms.Button bnClear;
    }
}

