namespace ReadBarcode_CSharp
{
    partial class Scan
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
            this.SuspendLayout();
            // 
            // txtShow
            // 
            this.txtShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.txtShow.Location = new System.Drawing.Point(20, 50);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.ReadOnly = true;
            this.txtShow.Size = new System.Drawing.Size(200, 200);
            this.txtShow.TabIndex = 0;
            // 
            // Scan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.txtShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Scan";
            this.Text = "Scan";
            this.TopMost = true;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scan_MouseUp);
            this.Closed += new System.EventHandler(this.Scan_Closed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scan_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scan_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Scan_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Scan_KeyUp);            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtShow;
    }
}

