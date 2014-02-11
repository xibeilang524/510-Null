namespace Ralid.Parking.POS.UI
{
    partial class FrmMain
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
            this.btnPaymentStatistics = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblStatusBar = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPaymentStatistics
            // 
            this.btnPaymentStatistics.Location = new System.Drawing.Point(126, 33);
            this.btnPaymentStatistics.Name = "btnPaymentStatistics";
            this.btnPaymentStatistics.Size = new System.Drawing.Size(106, 49);
            this.btnPaymentStatistics.TabIndex = 0;
            this.btnPaymentStatistics.Text = "收费统计";
            this.btnPaymentStatistics.Click += new System.EventHandler(this.btnPaymentStatistics_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(10, 33);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(106, 49);
            this.btnPayment.TabIndex = 2;
            this.btnPayment.Text = "停车收费";
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(10, 109);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(106, 49);
            this.btnLogOut.TabIndex = 4;
            this.btnLogOut.Text = "注销(&U)";
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(126, 109);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(106, 49);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatusBar.ForeColor = System.Drawing.Color.Blue;
            this.lblStatusBar.Location = new System.Drawing.Point(0, 300);
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(240, 20);
            // 
            // lblVersion
            // 
            this.lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Location = new System.Drawing.Point(12, 193);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(201, 20);
            this.lblVersion.Text = "软件版本号: 1.0.2014.0115";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblStatusBar);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.btnPaymentStatistics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmMain";
            this.Text = "瑞立德停车场手持收费软件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Closed += new System.EventHandler(this.FrmMain_Closed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPaymentStatistics;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblStatusBar;
        private System.Windows.Forms.Label lblVersion;
    }
}

