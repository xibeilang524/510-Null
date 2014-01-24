namespace Ralid.Park.DownloadCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnu_Connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDBSetting = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDBState = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnu_DownloadCards = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Connect,
            this.toolStripSeparator2,
            this.mnu_DownloadCards,
            this.toolStripSeparator1,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(620, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnu_Connect
            // 
            this.mnu_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_Connect.Name = "mnu_Connect";
            this.mnu_Connect.Size = new System.Drawing.Size(60, 22);
            this.mnu_Connect.Text = "设置连接";
            this.mnu_Connect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mnu_Connect.Click += new System.EventHandler(this.mnu_Connect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExit
            // 
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 22);
            this.btnExit.Text = "退出系统";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDBSetting,
            this.lblDBState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(620, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblDBSetting
            // 
            this.lblDBSetting.Name = "lblDBSetting";
            this.lblDBSetting.Size = new System.Drawing.Size(0, 17);
            // 
            // lblDBState
            // 
            this.lblDBState.Name = "lblDBState";
            this.lblDBState.Size = new System.Drawing.Size(0, 17);
            // 
            // mnu_DownloadCards
            // 
            this.mnu_DownloadCards.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnu_DownloadCards.Image = ((System.Drawing.Image)(resources.GetObject("mnu_DownloadCards.Image")));
            this.mnu_DownloadCards.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_DownloadCards.Name = "mnu_DownloadCards";
            this.mnu_DownloadCards.Size = new System.Drawing.Size(60, 22);
            this.mnu_DownloadCards.Text = "下发卡片";
            this.mnu_DownloadCards.Click += new System.EventHandler(this.mnu_DownloadCards_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 375);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "停车场离线收费手持机管理工具";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripButton mnu_Connect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblDBSetting;
        private System.Windows.Forms.ToolStripStatusLabel lblDBState;
        private System.Windows.Forms.ToolStripButton mnu_DownloadCards;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

    }
}

