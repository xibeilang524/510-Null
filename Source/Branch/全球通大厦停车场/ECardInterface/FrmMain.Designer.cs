namespace ECardInterface
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.notify1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnu_Para = new System.Windows.Forms.ToolStripButton();
            this.mnu_EcardRecords = new System.Windows.Forms.ToolStripButton();
            this.mnu_NotifyTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.mnu_Exit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.eventReportListBox1 = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notify1
            // 
            this.notify1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notify1.BalloonTipText = "户外屏刷新工具";
            this.notify1.BalloonTipTitle = "户外屏刷新工具";
            this.notify1.Icon = ((System.Drawing.Icon)(resources.GetObject("notify1.Icon")));
            this.notify1.Text = "户外屏刷新工具";
            this.notify1.Visible = true;
            this.notify1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify1_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Para,
            this.mnu_EcardRecords,
            this.mnu_NotifyTest,
            this.toolStripButton1,
            this.mnu_Exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(680, 56);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnu_Para
            // 
            this.mnu_Para.Image = ((System.Drawing.Image)(resources.GetObject("mnu_Para.Image")));
            this.mnu_Para.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_Para.Name = "mnu_Para";
            this.mnu_Para.Size = new System.Drawing.Size(36, 53);
            this.mnu_Para.Text = "配置";
            this.mnu_Para.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mnu_Para.Click += new System.EventHandler(this.mnu_Para_Click);
            // 
            // mnu_EcardRecords
            // 
            this.mnu_EcardRecords.Image = ((System.Drawing.Image)(resources.GetObject("mnu_EcardRecords.Image")));
            this.mnu_EcardRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_EcardRecords.Name = "mnu_EcardRecords";
            this.mnu_EcardRecords.Size = new System.Drawing.Size(72, 53);
            this.mnu_EcardRecords.Text = "未上传记录";
            this.mnu_EcardRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mnu_EcardRecords.Click += new System.EventHandler(this.mnu_EcardRecords_Click);
            // 
            // mnu_NotifyTest
            // 
            this.mnu_NotifyTest.Image = ((System.Drawing.Image)(resources.GetObject("mnu_NotifyTest.Image")));
            this.mnu_NotifyTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_NotifyTest.Name = "mnu_NotifyTest";
            this.mnu_NotifyTest.Size = new System.Drawing.Size(60, 53);
            this.mnu_NotifyTest.Text = "短信测试";
            this.mnu_NotifyTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mnu_NotifyTest.Click += new System.EventHandler(this.mnu_NotifyTest_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 53);
            this.toolStripButton1.Text = "连通测试";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // mnu_Exit
            // 
            this.mnu_Exit.Image = ((System.Drawing.Image)(resources.GetObject("mnu_Exit.Image")));
            this.mnu_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_Exit.Name = "mnu_Exit";
            this.mnu_Exit.Size = new System.Drawing.Size(36, 53);
            this.mnu_Exit.Text = "退出";
            this.mnu_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mnu_Exit.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 285);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(680, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // eventReportListBox1
            // 
            this.eventReportListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventReportListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventReportListBox1.FormattingEnabled = true;
            this.eventReportListBox1.ItemHeight = 12;
            this.eventReportListBox1.Location = new System.Drawing.Point(0, 56);
            this.eventReportListBox1.Name = "eventReportListBox1";
            this.eventReportListBox1.Size = new System.Drawing.Size(680, 229);
            this.eventReportListBox1.TabIndex = 4;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 307);
            this.Controls.Add(this.eventReportListBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMain";
            this.Text = "E车通同步工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notify1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mnu_Exit;
        private System.Windows.Forms.ToolStripButton mnu_Para;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Ralid.Park.UserControls.EventReportListBox eventReportListBox1;
        private System.Windows.Forms.ToolStripButton mnu_NotifyTest;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton mnu_EcardRecords;
    }
}

