namespace IR_CSharp
{
    partial class IR
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
            this.bnRead = new System.Windows.Forms.Button();
            this.bnClear = new System.Windows.Forms.Button();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bnRead
            // 
            this.bnRead.Location = new System.Drawing.Point(31, 246);
            this.bnRead.Name = "bnRead";
            this.bnRead.Size = new System.Drawing.Size(65, 33);
            this.bnRead.TabIndex = 0;
            this.bnRead.Text = "读电表";
            this.bnRead.Click += new System.EventHandler(this.bnRead_Click);
            // 
            // bnClear
            // 
            this.bnClear.Location = new System.Drawing.Point(142, 246);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(65, 33);
            this.bnClear.TabIndex = 1;
            this.bnClear.Text = "清空";
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // txtShow
            // 
            this.txtShow.Location = new System.Drawing.Point(15, 35);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.Size = new System.Drawing.Size(208, 196);
            this.txtShow.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(74, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.Text = "读电表示例";
            // 
            // IR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtShow);
            this.Controls.Add(this.bnClear);
            this.Controls.Add(this.bnRead);
            this.Name = "IR";
            this.Text = "IR";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.IR_Load);
            this.Closed += new System.EventHandler(this.IR_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.IR_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bnRead;
        private System.Windows.Forms.Button bnClear;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.Label label1;
    }
}

