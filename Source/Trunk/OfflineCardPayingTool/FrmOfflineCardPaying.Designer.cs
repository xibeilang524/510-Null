namespace OfflineCardPayingTool
{
    partial class FrmOfflineCardPaying
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOfflineCardPaying));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_CardPaying = new System.Windows.Forms.ToolStripButton();
            this.btn_SystemOption = new System.Windows.Forms.ToolStripButton();
            this.btn_CardPaymentReport = new System.Windows.Forms.ToolStripButton();
            this.formPanel1 = new Ralid.GeneralLibrary.WinformControl.FormPanel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mnu_SysManager = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SystemOption = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Language = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SimpleChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_TraditionalChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_English = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrCheckDog = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblOperator = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStation = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStartFrom = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEventServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnu_LocalSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_CardPaying,
            this.btn_SystemOption,
            this.btn_CardPaymentReport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(769, 88);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_CardPaying
            // 
            this.btn_CardPaying.Image = global::OfflineCardPayingTool.Properties.Resources.CardCharge;
            this.btn_CardPaying.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_CardPaying.Name = "btn_CardPaying";
            this.btn_CardPaying.Size = new System.Drawing.Size(68, 85);
            this.btn_CardPaying.Text = "收费";
            this.btn_CardPaying.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_CardPaying.Click += new System.EventHandler(this.btn_CardPaying_Click);
            // 
            // btn_SystemOption
            // 
            this.btn_SystemOption.Image = global::OfflineCardPayingTool.Properties.Resources.Setting;
            this.btn_SystemOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_SystemOption.Name = "btn_SystemOption";
            this.btn_SystemOption.Size = new System.Drawing.Size(68, 85);
            this.btn_SystemOption.Text = "设置";
            this.btn_SystemOption.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_SystemOption.Click += new System.EventHandler(this.mnu_SystemOption_Click);
            // 
            // btn_CardPaymentReport
            // 
            this.btn_CardPaymentReport.Image = global::OfflineCardPayingTool.Properties.Resources.coins;
            this.btn_CardPaymentReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_CardPaymentReport.Name = "btn_CardPaymentReport";
            this.btn_CardPaymentReport.Size = new System.Drawing.Size(68, 85);
            this.btn_CardPaymentReport.Text = "收费记录";
            this.btn_CardPaymentReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_CardPaymentReport.Click += new System.EventHandler(this.btn_CardPaymentReport_Click);
            // 
            // formPanel1
            // 
            this.formPanel1.BackColor = System.Drawing.Color.Navy;
            this.formPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.formPanel1.FormHeaderLength = 135;
            this.formPanel1.Location = new System.Drawing.Point(0, 113);
            this.formPanel1.Name = "formPanel1";
            this.formPanel1.Size = new System.Drawing.Size(769, 25);
            this.formPanel1.TabIndex = 1;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SysManager,
            this.mnu_Language});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(769, 25);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // mnu_SysManager
            // 
            this.mnu_SysManager.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SystemOption,
            this.mnu_LocalSettings,
            this.mnu_Exit});
            this.mnu_SysManager.Name = "mnu_SysManager";
            this.mnu_SysManager.Size = new System.Drawing.Size(44, 21);
            this.mnu_SysManager.Text = "系统";
            // 
            // mnu_SystemOption
            // 
            this.mnu_SystemOption.Name = "mnu_SystemOption";
            this.mnu_SystemOption.Size = new System.Drawing.Size(152, 22);
            this.mnu_SystemOption.Text = "参数设置";
            this.mnu_SystemOption.Click += new System.EventHandler(this.mnu_SystemOption_Click);
            // 
            // mnu_Exit
            // 
            this.mnu_Exit.Name = "mnu_Exit";
            this.mnu_Exit.Size = new System.Drawing.Size(152, 22);
            this.mnu_Exit.Text = "退出";
            this.mnu_Exit.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // mnu_Language
            // 
            this.mnu_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SimpleChinese,
            this.mnu_TraditionalChinese,
            this.mnu_English});
            this.mnu_Language.Name = "mnu_Language";
            this.mnu_Language.Size = new System.Drawing.Size(44, 21);
            this.mnu_Language.Text = "语言";
            this.mnu_Language.Visible = false;
            // 
            // mnu_SimpleChinese
            // 
            this.mnu_SimpleChinese.Name = "mnu_SimpleChinese";
            this.mnu_SimpleChinese.Size = new System.Drawing.Size(124, 22);
            this.mnu_SimpleChinese.Text = "简体中文";
            this.mnu_SimpleChinese.Click += new System.EventHandler(this.mnu_Language_Clicked);
            // 
            // mnu_TraditionalChinese
            // 
            this.mnu_TraditionalChinese.Name = "mnu_TraditionalChinese";
            this.mnu_TraditionalChinese.Size = new System.Drawing.Size(124, 22);
            this.mnu_TraditionalChinese.Text = "繁体中文";
            this.mnu_TraditionalChinese.Click += new System.EventHandler(this.mnu_Language_Clicked);
            // 
            // mnu_English
            // 
            this.mnu_English.Name = "mnu_English";
            this.mnu_English.Size = new System.Drawing.Size(124, 22);
            this.mnu_English.Text = "英文";
            this.mnu_English.Click += new System.EventHandler(this.mnu_Language_Clicked);
            // 
            // tmrCheckDog
            // 
            this.tmrCheckDog.Interval = 60000;
            this.tmrCheckDog.Tick += new System.EventHandler(this.tmrCheckDog_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblOperator,
            this.lblStation,
            this.lblStartFrom,
            this.lblEventServiceStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(769, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblOperator
            // 
            this.lblOperator.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblOperator.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblOperator.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(4, 17);
            // 
            // lblStation
            // 
            this.lblStation.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblStation.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblStation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(4, 17);
            // 
            // lblStartFrom
            // 
            this.lblStartFrom.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblStartFrom.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblStartFrom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.lblStartFrom.Name = "lblStartFrom";
            this.lblStartFrom.Size = new System.Drawing.Size(4, 17);
            // 
            // lblEventServiceStatus
            // 
            this.lblEventServiceStatus.AutoSize = false;
            this.lblEventServiceStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblEventServiceStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.lblEventServiceStatus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.lblEventServiceStatus.ForeColor = System.Drawing.Color.Black;
            this.lblEventServiceStatus.Name = "lblEventServiceStatus";
            this.lblEventServiceStatus.Size = new System.Drawing.Size(742, 17);
            this.lblEventServiceStatus.Spring = true;
            this.lblEventServiceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnu_LocalSettings
            // 
            this.mnu_LocalSettings.Name = "mnu_LocalSettings";
            this.mnu_LocalSettings.Size = new System.Drawing.Size(152, 22);
            this.mnu_LocalSettings.Text = "本地设置";
            this.mnu_LocalSettings.Click += new System.EventHandler(this.mnu_LocalSettings_Click);
            // 
            // FrmOfflineCardPaying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(769, 477);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.formPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "FrmOfflineCardPaying";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "停车场脱机收费工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOfflineCardPaying_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_CardPaying;
        private System.Windows.Forms.ToolStripButton btn_SystemOption;
        private Ralid.GeneralLibrary.WinformControl.FormPanel formPanel1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnu_SysManager;
        private System.Windows.Forms.ToolStripMenuItem mnu_SystemOption;
        private System.Windows.Forms.ToolStripMenuItem mnu_Exit;
        private System.Windows.Forms.ToolStripMenuItem mnu_Language;
        private System.Windows.Forms.ToolStripMenuItem mnu_SimpleChinese;
        private System.Windows.Forms.ToolStripMenuItem mnu_TraditionalChinese;
        private System.Windows.Forms.ToolStripMenuItem mnu_English;
        private System.Windows.Forms.ToolStripButton btn_CardPaymentReport;
        private System.Windows.Forms.Timer tmrCheckDog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblOperator;
        private System.Windows.Forms.ToolStripStatusLabel lblStation;
        private System.Windows.Forms.ToolStripStatusLabel lblStartFrom;
        private System.Windows.Forms.ToolStripStatusLabel lblEventServiceStatus;
        private System.Windows.Forms.ToolStripMenuItem mnu_LocalSettings;
    }
}

