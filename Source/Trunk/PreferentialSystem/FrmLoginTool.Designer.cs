namespace PreferentialSystem
{
    partial class FrmLoginTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkRememberLogid = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPassword = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogName = new System.Windows.Forms.ComboBox();
            this.UCStandbyDB = new Ralid.GeneralLibrary.WinformControl.UCDateBaseSetting();
            this.txtPasswd = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtUserID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtDataBase = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtServer = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.UCStandbyDB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLogin.Location = new System.Drawing.Point(315, 30);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(131, 23);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "登录(&L)";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(315, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "退出软件(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkRememberLogid);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtLogName);
            this.groupBox3.Location = new System.Drawing.Point(7, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(295, 93);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // chkRememberLogid
            // 
            this.chkRememberLogid.AutoSize = true;
            this.chkRememberLogid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRememberLogid.Location = new System.Drawing.Point(200, 75);
            this.chkRememberLogid.Name = "chkRememberLogid";
            this.chkRememberLogid.Size = new System.Drawing.Size(84, 16);
            this.chkRememberLogid.TabIndex = 3;
            this.chkRememberLogid.Text = "记住登录名";
            this.chkRememberLogid.UseVisualStyleBackColor = true;
            this.chkRememberLogid.CheckedChanged += new System.EventHandler(this.chkRememberLogid_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PreferentialSystem.Properties.Resources.Cards;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(7, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPassword.Location = new System.Drawing.Point(136, 47);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(142, 21);
            this.txtPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(76, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(84, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLogName
            // 
            this.txtLogName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtLogName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLogName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtLogName.Location = new System.Drawing.Point(136, 17);
            this.txtLogName.MaxLength = 20;
            this.txtLogName.Name = "txtLogName";
            this.txtLogName.Size = new System.Drawing.Size(142, 20);
            this.txtLogName.TabIndex = 0;
            // 
            // UCStandbyDB
            // 
            this.UCStandbyDB.ConnectTimeout = 5;
            this.UCStandbyDB.Controls.Add(this.txtPasswd);
            this.UCStandbyDB.Controls.Add(this.txtUserID);
            this.UCStandbyDB.Controls.Add(this.txtDataBase);
            this.UCStandbyDB.Controls.Add(this.txtServer);
            this.UCStandbyDB.Location = new System.Drawing.Point(7, 19);
            this.UCStandbyDB.Name = "UCStandbyDB";
            this.UCStandbyDB.Size = new System.Drawing.Size(410, 88);
            this.UCStandbyDB.TabIndex = 0;
            // 
            // txtPasswd
            // 
            this.txtPasswd.Enabled = false;
            this.txtPasswd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPasswd.Location = new System.Drawing.Point(270, 52);
            this.txtPasswd.MaxLength = 50;
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(131, 21);
            this.txtPasswd.TabIndex = 14;
            // 
            // txtUserID
            // 
            this.txtUserID.Enabled = false;
            this.txtUserID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtUserID.Location = new System.Drawing.Point(64, 52);
            this.txtUserID.MaxLength = 50;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(131, 21);
            this.txtUserID.TabIndex = 12;
            // 
            // txtDataBase
            // 
            this.txtDataBase.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDataBase.Location = new System.Drawing.Point(270, 25);
            this.txtDataBase.MaxLength = 50;
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.Size = new System.Drawing.Size(131, 21);
            this.txtDataBase.TabIndex = 11;
            // 
            // txtServer
            // 
            this.txtServer.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtServer.Location = new System.Drawing.Point(65, 25);
            this.txtServer.MaxLength = 50;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(131, 21);
            this.txtServer.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UCStandbyDB);
            this.groupBox1.Location = new System.Drawing.Point(7, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 113);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库连接信息";
            // 
            // FrmLoginTool
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(453, 225);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLoginTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RALID停车场优惠系统 登录";
            this.Load += new System.EventHandler(this.FrmLoginTool_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.UCStandbyDB.ResumeLayout(false);
            this.UCStandbyDB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnLogin;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkRememberLogid;
        private System.Windows.Forms.PictureBox pictureBox1;
        public Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPassword;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox txtLogName;
        private Ralid.GeneralLibrary.WinformControl.UCDateBaseSetting UCStandbyDB;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPasswd;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtUserID;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtDataBase;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtServer;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}