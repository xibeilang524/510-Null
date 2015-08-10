namespace Ralid.OpenCard.YCTFtpTool
{
    partial class Form1
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
            this.txtFTPPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtFTPPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFTPUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFTPServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtFTPPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.button4 = new System.Windows.Forms.Button();
            this.lblFtpState = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFTPPort
            // 
            this.txtFTPPort.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtFTPPort.Location = new System.Drawing.Point(393, 12);
            this.txtFTPPort.MaxValue = 65535;
            this.txtFTPPort.MinValue = 0;
            this.txtFTPPort.Name = "txtFTPPort";
            this.txtFTPPort.NumberWithCommas = false;
            this.txtFTPPort.Size = new System.Drawing.Size(162, 21);
            this.txtFTPPort.TabIndex = 38;
            this.txtFTPPort.Text = "21";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(358, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 37;
            this.label7.Text = "密码";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(562, 44);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 23);
            this.btnConnect.TabIndex = 36;
            this.btnConnect.Text = "连  接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtFTPPwd
            // 
            this.txtFTPPwd.Location = new System.Drawing.Point(390, 45);
            this.txtFTPPwd.Name = "txtFTPPwd";
            this.txtFTPPwd.PasswordChar = '*';
            this.txtFTPPwd.Size = new System.Drawing.Size(165, 21);
            this.txtFTPPwd.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(358, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "密码";
            // 
            // txtFTPUser
            // 
            this.txtFTPUser.Location = new System.Drawing.Point(95, 45);
            this.txtFTPUser.Name = "txtFTPUser";
            this.txtFTPUser.Size = new System.Drawing.Size(241, 21);
            this.txtFTPUser.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "用户名";
            // 
            // txtFTPServer
            // 
            this.txtFTPServer.Location = new System.Drawing.Point(95, 12);
            this.txtFTPServer.Name = "txtFTPServer";
            this.txtFTPServer.Size = new System.Drawing.Size(241, 21);
            this.txtFTPServer.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "FTP服务器";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(562, 78);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(80, 23);
            this.btnBrowser.TabIndex = 29;
            this.btnBrowser.Text = "...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtFTPPath
            // 
            this.txtFTPPath.Enabled = false;
            this.txtFTPPath.Location = new System.Drawing.Point(95, 79);
            this.txtFTPPath.Name = "txtFTPPath";
            this.txtFTPPath.Size = new System.Drawing.Size(460, 21);
            this.txtFTPPath.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "Ftp文件位置";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFtpState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(672, 22);
            this.statusStrip1.TabIndex = 39;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(562, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 34);
            this.button4.TabIndex = 40;
            this.button4.Text = "生成ZIP";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblFtpState
            // 
            this.lblFtpState.Name = "lblFtpState";
            this.lblFtpState.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 387);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtFTPPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtFTPPwd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFTPUser);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFTPServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtFTPPath);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.Text = "羊城通FTP同步工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtFTPPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtFTPPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFTPUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFTPServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtFTPPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStripStatusLabel lblFtpState;
    }
}

