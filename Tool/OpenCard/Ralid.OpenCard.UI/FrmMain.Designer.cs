namespace Ralid.OpenCard.UI
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblOperator = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStation = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStartFrom = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrCheckDog = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_YiTing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ZST = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SelOperator = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SelStation = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblOperator,
            this.lblStation,
            this.lblStartFrom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 443);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(713, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblOperator
            // 
            this.lblOperator.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblOperator.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(48, 21);
            this.lblOperator.Text = "操作员";
            // 
            // lblStation
            // 
            this.lblStation.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblStation.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(48, 21);
            this.lblStation.Text = "工作站";
            // 
            // lblStartFrom
            // 
            this.lblStartFrom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartFrom.Name = "lblStartFrom";
            this.lblStartFrom.Size = new System.Drawing.Size(56, 21);
            this.lblStartFrom.Text = "启动时间";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.参数设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(713, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SelOperator,
            this.mnu_SelStation});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_YiTing,
            this.mnu_ZST});
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // mnu_YiTing
            // 
            this.mnu_YiTing.Name = "mnu_YiTing";
            this.mnu_YiTing.Size = new System.Drawing.Size(124, 22);
            this.mnu_YiTing.Text = "驿停闪付";
            this.mnu_YiTing.Click += new System.EventHandler(this.mnu_YiTing_Click);
            // 
            // mnu_ZST
            // 
            this.mnu_ZST.Name = "mnu_ZST";
            this.mnu_ZST.Size = new System.Drawing.Size(124, 22);
            this.mnu_ZST.Text = "中山通";
            this.mnu_ZST.Click += new System.EventHandler(this.mnu_ZST_Click);
            // 
            // mnu_SelOperator
            // 
            this.mnu_SelOperator.Name = "mnu_SelOperator";
            this.mnu_SelOperator.Size = new System.Drawing.Size(160, 22);
            this.mnu_SelOperator.Text = "选择当前操作员";
            this.mnu_SelOperator.Click += new System.EventHandler(this.mnu_SelOperator_Click);
            // 
            // mnu_SelStation
            // 
            this.mnu_SelStation.Name = "mnu_SelStation";
            this.mnu_SelStation.Size = new System.Drawing.Size(160, 22);
            this.mnu_SelStation.Text = "选择当前工作站";
            this.mnu_SelStation.Click += new System.EventHandler(this.mnu_SelStation_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 469);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStartFrom;
        private System.Windows.Forms.Timer tmrCheckDog;
        private System.Windows.Forms.ToolStripStatusLabel lblOperator;
        private System.Windows.Forms.ToolStripStatusLabel lblStation;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_ZST;
        private System.Windows.Forms.ToolStripMenuItem mnu_YiTing;
        private System.Windows.Forms.ToolStripMenuItem mnu_SelOperator;
        private System.Windows.Forms.ToolStripMenuItem mnu_SelStation;
    }
}

