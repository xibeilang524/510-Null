namespace PreferentialSystem
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mnu_System = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SysOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_PreferentialInput = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_PreferentialCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_LogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Data = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_WorkStation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Company = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SafeSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Operator = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_RoleManager = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_PRERecord = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Language = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrCheckDog = new System.Windows.Forms.Timer(this.components);
            this.formPanel1 = new Ralid.GeneralLibrary.WinformControl.FormPanel();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnu_Doc = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_System,
            this.mnu_Data,
            this.mnu_SafeSetting,
            this.mnu_Report,
            this.mnu_Language,
            this.mnu_Help});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(757, 25);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // mnu_System
            // 
            this.mnu_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SysOptions,
            this.mnu_PreferentialInput,
            this.mnu_PreferentialCancel,
            this.mnu_LogOut});
            this.mnu_System.Name = "mnu_System";
            this.mnu_System.Size = new System.Drawing.Size(44, 21);
            this.mnu_System.Text = "系统";
            // 
            // mnu_SysOptions
            // 
            this.mnu_SysOptions.Name = "mnu_SysOptions";
            this.mnu_SysOptions.Size = new System.Drawing.Size(124, 22);
            this.mnu_SysOptions.Text = "系统设置";
            this.mnu_SysOptions.Click += new System.EventHandler(this.mnu_SysOptions_Click);
            // 
            // mnu_PreferentialInput
            // 
            this.mnu_PreferentialInput.Name = "mnu_PreferentialInput";
            this.mnu_PreferentialInput.Size = new System.Drawing.Size(124, 22);
            this.mnu_PreferentialInput.Text = "优惠录入";
            this.mnu_PreferentialInput.Click += new System.EventHandler(this.mnu_PreferentialInput_Click);
            // 
            // mnu_PreferentialCancel
            // 
            this.mnu_PreferentialCancel.Name = "mnu_PreferentialCancel";
            this.mnu_PreferentialCancel.Size = new System.Drawing.Size(124, 22);
            this.mnu_PreferentialCancel.Text = "优惠取消";
            this.mnu_PreferentialCancel.Click += new System.EventHandler(this.mnu_PreferentialCancel_Click);
            // 
            // mnu_LogOut
            // 
            this.mnu_LogOut.Name = "mnu_LogOut";
            this.mnu_LogOut.Size = new System.Drawing.Size(124, 22);
            this.mnu_LogOut.Text = "退出";
            this.mnu_LogOut.Click += new System.EventHandler(this.mnu_LogOut_Click);
            // 
            // mnu_Data
            // 
            this.mnu_Data.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_WorkStation,
            this.mnu_Company});
            this.mnu_Data.Name = "mnu_Data";
            this.mnu_Data.Size = new System.Drawing.Size(44, 21);
            this.mnu_Data.Text = "数据";
            // 
            // mnu_WorkStation
            // 
            this.mnu_WorkStation.Name = "mnu_WorkStation";
            this.mnu_WorkStation.Size = new System.Drawing.Size(148, 22);
            this.mnu_WorkStation.Text = "工作站设置";
            this.mnu_WorkStation.Click += new System.EventHandler(this.mnu_WorkStation_Click);
            // 
            // mnu_Company
            // 
            this.mnu_Company.Name = "mnu_Company";
            this.mnu_Company.Size = new System.Drawing.Size(148, 22);
            this.mnu_Company.Text = "商家信息设置";
            this.mnu_Company.Click += new System.EventHandler(this.mnu_Company_Click);
            // 
            // mnu_SafeSetting
            // 
            this.mnu_SafeSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Operator,
            this.mnu_ChangePassword,
            this.mnu_RoleManager});
            this.mnu_SafeSetting.Name = "mnu_SafeSetting";
            this.mnu_SafeSetting.Size = new System.Drawing.Size(44, 21);
            this.mnu_SafeSetting.Text = "安全";
            // 
            // mnu_Operator
            // 
            this.mnu_Operator.Name = "mnu_Operator";
            this.mnu_Operator.Size = new System.Drawing.Size(136, 22);
            this.mnu_Operator.Text = "操作员管理";
            this.mnu_Operator.Click += new System.EventHandler(this.mnu_Operator_Click);
            // 
            // mnu_ChangePassword
            // 
            this.mnu_ChangePassword.Name = "mnu_ChangePassword";
            this.mnu_ChangePassword.Size = new System.Drawing.Size(136, 22);
            this.mnu_ChangePassword.Text = "修改密码";
            this.mnu_ChangePassword.Click += new System.EventHandler(this.mnu_ChangePassword_Click);
            // 
            // mnu_RoleManager
            // 
            this.mnu_RoleManager.Name = "mnu_RoleManager";
            this.mnu_RoleManager.Size = new System.Drawing.Size(136, 22);
            this.mnu_RoleManager.Text = "角色管理";
            this.mnu_RoleManager.Click += new System.EventHandler(this.mnu_RoleManager_Click);
            // 
            // mnu_Report
            // 
            this.mnu_Report.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_PRERecord});
            this.mnu_Report.Name = "mnu_Report";
            this.mnu_Report.Size = new System.Drawing.Size(80, 21);
            this.mnu_Report.Text = "查询与报表";
            // 
            // mnu_PRERecord
            // 
            this.mnu_PRERecord.Name = "mnu_PRERecord";
            this.mnu_PRERecord.Size = new System.Drawing.Size(148, 22);
            this.mnu_PRERecord.Text = "优惠记录查询";
            this.mnu_PRERecord.Click += new System.EventHandler(this.mnu_PRERecord_Click);
            // 
            // mnu_Language
            // 
            this.mnu_Language.Name = "mnu_Language";
            this.mnu_Language.Size = new System.Drawing.Size(44, 21);
            this.mnu_Language.Text = "语言";
            // 
            // mnu_Help
            // 
            this.mnu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Doc,
            this.mnu_About});
            this.mnu_Help.Name = "mnu_Help";
            this.mnu_Help.Size = new System.Drawing.Size(44, 21);
            this.mnu_Help.Text = "帮助";
            // 
            // mnu_About
            // 
            this.mnu_About.Name = "mnu_About";
            this.mnu_About.Size = new System.Drawing.Size(152, 22);
            this.mnu_About.Text = "关于";
            this.mnu_About.Click += new System.EventHandler(this.mnu_About_Click);
            // 
            // tmrCheckDog
            // 
            this.tmrCheckDog.Interval = 60000;
            this.tmrCheckDog.Tick += new System.EventHandler(this.tmrCheckDog_Tick);
            // 
            // formPanel1
            // 
            this.formPanel1.BackColor = System.Drawing.Color.Navy;
            this.formPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.formPanel1.FormHeaderLength = 135;
            this.formPanel1.Location = new System.Drawing.Point(0, 25);
            this.formPanel1.Name = "formPanel1";
            this.formPanel1.Size = new System.Drawing.Size(757, 25);
            this.formPanel1.TabIndex = 2;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 494);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(757, 22);
            this.mainStatusStrip.TabIndex = 4;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "操作员信息";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel2.Text = "工作站信息";
            // 
            // mnu_Doc
            // 
            this.mnu_Doc.Name = "mnu_Doc";
            this.mnu_Doc.Size = new System.Drawing.Size(152, 22);
            this.mnu_Doc.Text = "用户手册";
            this.mnu_Doc.Click += new System.EventHandler(this.mnu_Doc_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 516);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.formPanel1);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RALID停车场优惠系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnu_System;
        private System.Windows.Forms.ToolStripMenuItem mnu_Data;
        private System.Windows.Forms.ToolStripMenuItem mnu_SafeSetting;
        private System.Windows.Forms.ToolStripMenuItem mnu_Report;
        private System.Windows.Forms.ToolStripMenuItem mnu_Language;
        private System.Windows.Forms.ToolStripMenuItem mnu_Help;
        private System.Windows.Forms.ToolStripMenuItem mnu_Operator;
        private System.Windows.Forms.ToolStripMenuItem mnu_ChangePassword;
        private System.Windows.Forms.ToolStripMenuItem mnu_RoleManager;
        private System.Windows.Forms.Timer tmrCheckDog;
        private Ralid.GeneralLibrary.WinformControl.FormPanel formPanel1;
        private System.Windows.Forms.ToolStripMenuItem mnu_WorkStation;
        private System.Windows.Forms.ToolStripMenuItem mnu_Company;
        private System.Windows.Forms.ToolStripMenuItem mnu_SysOptions;
        private System.Windows.Forms.ToolStripMenuItem mnu_PreferentialInput;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem mnu_PreferentialCancel;
        private System.Windows.Forms.ToolStripMenuItem mnu_PRERecord;
        private System.Windows.Forms.ToolStripMenuItem mnu_LogOut;
        private System.Windows.Forms.ToolStripMenuItem mnu_About;
        private System.Windows.Forms.ToolStripMenuItem mnu_Doc;
    }
}

