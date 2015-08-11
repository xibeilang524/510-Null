namespace Ralid.OpenCard.YCTFtpTool
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblFtpState = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnBlack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPayrecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtFTPPath = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.eventList = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFTPPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtFTPPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFTPUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFTPServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFtpState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(983, 22);
            this.statusStrip1.TabIndex = 39;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblFtpState
            // 
            this.lblFtpState.Name = "lblFtpState";
            this.lblFtpState.Size = new System.Drawing.Size(0, 17);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "羊城通FTP工具";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // mnu_Exit
            // 
            this.mnu_Exit.Name = "mnu_Exit";
            this.mnu_Exit.Size = new System.Drawing.Size(124, 22);
            this.mnu_Exit.Text = "退出系统";
            this.mnu_Exit.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBlack,
            this.toolStripSeparator2,
            this.btnPayrecord,
            this.toolStripSeparator1,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(983, 25);
            this.toolStrip1.TabIndex = 43;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnBlack
            // 
            this.btnBlack.Image = global::Ralid.OpenCard.YCTFtpTool.Properties.Resources.Blacklist;
            this.btnBlack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBlack.Name = "btnBlack";
            this.btnBlack.Size = new System.Drawing.Size(88, 22);
            this.btnBlack.Text = "黑名单查询";
            this.btnBlack.Click += new System.EventHandler(this.btnBlack_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPayrecord
            // 
            this.btnPayrecord.Image = global::Ralid.OpenCard.YCTFtpTool.Properties.Resources.payment;
            this.btnPayrecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPayrecord.Name = "btnPayrecord";
            this.btnPayrecord.Size = new System.Drawing.Size(100, 22);
            this.btnPayrecord.Text = "消费记录查询";
            this.btnPayrecord.Click += new System.EventHandler(this.btnPayrecord_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::Ralid.OpenCard.YCTFtpTool.Properties.Resources.exit;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(76, 22);
            this.btnExit.Text = "退出系统";
            this.btnExit.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.splitter2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 266);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(983, 273);
            this.panel2.TabIndex = 50;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(495, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(488, 273);
            this.panel4.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.textBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(488, 38);
            this.panel6.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "远程文件";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(71, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(405, 21);
            this.textBox1.TabIndex = 42;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter2.Location = new System.Drawing.Point(489, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(6, 273);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(489, 273);
            this.panel3.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.btnBrowser);
            this.panel5.Controls.Add(this.txtFTPPath);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(489, 38);
            this.panel5.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "本地文件";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(448, 8);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(35, 23);
            this.btnBrowser.TabIndex = 43;
            this.btnBrowser.Text = "...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            // 
            // txtFTPPath
            // 
            this.txtFTPPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFTPPath.Enabled = false;
            this.txtFTPPath.Location = new System.Drawing.Point(71, 9);
            this.txtFTPPath.Name = "txtFTPPath";
            this.txtFTPPath.Size = new System.Drawing.Size(371, 21);
            this.txtFTPPath.TabIndex = 42;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 260);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(983, 6);
            this.splitter1.TabIndex = 49;
            this.splitter1.TabStop = false;
            // 
            // eventList
            // 
            this.eventList.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventList.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.eventList.ItemHeight = 12;
            this.eventList.Location = new System.Drawing.Point(0, 76);
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(983, 184);
            this.eventList.TabIndex = 48;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFTPPort);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.txtFTPPwd);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtFTPUser);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtFTPServer);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 51);
            this.panel1.TabIndex = 47;
            // 
            // txtFTPPort
            // 
            this.txtFTPPort.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtFTPPort.Location = new System.Drawing.Point(347, 15);
            this.txtFTPPort.MaxValue = 65535;
            this.txtFTPPort.MinValue = 0;
            this.txtFTPPort.Name = "txtFTPPort";
            this.txtFTPPort.NumberWithCommas = false;
            this.txtFTPPort.Size = new System.Drawing.Size(55, 21);
            this.txtFTPPort.TabIndex = 47;
            this.txtFTPPort.Text = "21";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(312, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 46;
            this.label7.Text = "密码";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(721, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 23);
            this.btnConnect.TabIndex = 45;
            this.btnConnect.Text = "连  接";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // txtFTPPwd
            // 
            this.txtFTPPwd.Location = new System.Drawing.Point(610, 15);
            this.txtFTPPwd.Name = "txtFTPPwd";
            this.txtFTPPwd.PasswordChar = '*';
            this.txtFTPPwd.Size = new System.Drawing.Size(96, 21);
            this.txtFTPPwd.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(578, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "密码";
            // 
            // txtFTPUser
            // 
            this.txtFTPUser.Location = new System.Drawing.Point(467, 15);
            this.txtFTPUser.Name = "txtFTPUser";
            this.txtFTPUser.Size = new System.Drawing.Size(96, 21);
            this.txtFTPUser.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(419, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "用户名";
            // 
            // txtFTPServer
            // 
            this.txtFTPServer.Location = new System.Drawing.Point(83, 15);
            this.txtFTPServer.Name = "txtFTPServer";
            this.txtFTPServer.Size = new System.Drawing.Size(214, 21);
            this.txtFTPServer.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "FTP服务器";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.eventList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "羊城通FTP同步工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblFtpState;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Exit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtFTPPath;
        private System.Windows.Forms.Splitter splitter1;
        private Park.UserControls.EventReportListBox eventList;
        private System.Windows.Forms.Panel panel1;
        private GeneralLibrary.WinformControl.IntergerTextBox txtFTPPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtFTPPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFTPUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFTPServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnBlack;
        private System.Windows.Forms.ToolStripButton btnPayrecord;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

