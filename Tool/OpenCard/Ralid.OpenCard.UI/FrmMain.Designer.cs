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
            this.lblYCT = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblYiTingShanFu = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblZST = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrCheckDog = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SelOperator = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SelStation = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_YCT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_YiTing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ZST = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlPark = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.chkOpenEvent = new System.Windows.Forms.CheckBox();
            this.chkCardEvent = new System.Windows.Forms.CheckBox();
            this.eventList = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblOperator,
            this.lblStation,
            this.lblStartFrom,
            this.lblYCT,
            this.lblYiTingShanFu,
            this.lblZST});
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
            this.lblStartFrom.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblStartFrom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartFrom.Name = "lblStartFrom";
            this.lblStartFrom.Size = new System.Drawing.Size(446, 21);
            this.lblStartFrom.Spring = true;
            this.lblStartFrom.Text = "启动时间";
            this.lblStartFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblYCT
            // 
            this.lblYCT.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblYCT.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblYCT.ForeColor = System.Drawing.Color.Blue;
            this.lblYCT.Name = "lblYCT";
            this.lblYCT.Size = new System.Drawing.Size(48, 21);
            this.lblYCT.Text = "羊城通";
            // 
            // lblYiTingShanFu
            // 
            this.lblYiTingShanFu.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblYiTingShanFu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblYiTingShanFu.ForeColor = System.Drawing.Color.Blue;
            this.lblYiTingShanFu.Name = "lblYiTingShanFu";
            this.lblYiTingShanFu.Size = new System.Drawing.Size(60, 21);
            this.lblYiTingShanFu.Text = "驿停闪付";
            // 
            // lblZST
            // 
            this.lblZST.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblZST.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblZST.ForeColor = System.Drawing.Color.Blue;
            this.lblZST.Name = "lblZST";
            this.lblZST.Size = new System.Drawing.Size(48, 21);
            this.lblZST.Text = "中山通";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
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
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_YCT,
            this.mnu_YiTing,
            this.mnu_ZST});
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // mnu_YCT
            // 
            this.mnu_YCT.Name = "mnu_YCT";
            this.mnu_YCT.Size = new System.Drawing.Size(124, 22);
            this.mnu_YCT.Text = "羊城通";
            this.mnu_YCT.Click += new System.EventHandler(this.mnu_YCT_Click);
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
            this.mnu_ZST.Visible = false;
            this.mnu_ZST.Click += new System.EventHandler(this.mnu_ZST_Click);
            // 
            // pnlPark
            // 
            this.pnlPark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPark.Location = new System.Drawing.Point(0, 383);
            this.pnlPark.Name = "pnlPark";
            this.pnlPark.Size = new System.Drawing.Size(713, 60);
            this.pnlPark.TabIndex = 10;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 377);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(713, 6);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkDebug);
            this.panel2.Controls.Add(this.chkOpenEvent);
            this.panel2.Controls.Add(this.chkCardEvent);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(713, 43);
            this.panel2.TabIndex = 12;
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Checked = true;
            this.chkDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDebug.Location = new System.Drawing.Point(605, 15);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(96, 16);
            this.chkDebug.TabIndex = 10;
            this.chkDebug.Text = "启用日志记录";
            this.chkDebug.UseVisualStyleBackColor = true;
            this.chkDebug.CheckedChanged += new System.EventHandler(this.chkDebug_CheckedChanged);
            // 
            // chkOpenEvent
            // 
            this.chkOpenEvent.AutoSize = true;
            this.chkOpenEvent.Checked = true;
            this.chkOpenEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenEvent.Location = new System.Drawing.Point(145, 15);
            this.chkOpenEvent.Name = "chkOpenEvent";
            this.chkOpenEvent.Size = new System.Drawing.Size(96, 16);
            this.chkOpenEvent.TabIndex = 10;
            this.chkOpenEvent.Text = "开放卡片事件";
            this.chkOpenEvent.UseVisualStyleBackColor = true;
            // 
            // chkCardEvent
            // 
            this.chkCardEvent.AutoSize = true;
            this.chkCardEvent.Checked = true;
            this.chkCardEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCardEvent.Location = new System.Drawing.Point(12, 15);
            this.chkCardEvent.Name = "chkCardEvent";
            this.chkCardEvent.Size = new System.Drawing.Size(108, 16);
            this.chkCardEvent.TabIndex = 9;
            this.chkCardEvent.Text = "停车场进出事件";
            this.chkCardEvent.UseVisualStyleBackColor = true;
            // 
            // eventList
            // 
            this.eventList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventList.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventList.ItemHeight = 12;
            this.eventList.Location = new System.Drawing.Point(0, 68);
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(713, 309);
            this.eventList.TabIndex = 13;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(713, 469);
            this.Controls.Add(this.eventList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlPark);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "瑞立德开放卡片管理系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem mnu_YCT;
        private System.Windows.Forms.Panel pnlPark;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkOpenEvent;
        private System.Windows.Forms.CheckBox chkCardEvent;
        private Park.UserControls.EventReportListBox eventList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel lblZST;
        private System.Windows.Forms.ToolStripStatusLabel lblYiTingShanFu;
        private System.Windows.Forms.ToolStripStatusLabel lblYCT;
        private System.Windows.Forms.CheckBox chkDebug;
    }
}

