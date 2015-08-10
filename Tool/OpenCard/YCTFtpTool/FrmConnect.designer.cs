namespace Ralid.OpenCard.YCTFtpTool
{
    partial class FrmConnect
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
            this.gpDB = new System.Windows.Forms.GroupBox();
            this.txtParkPasswd = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtParkDB = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtParkUserID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtParkServer = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdParkUser = new System.Windows.Forms.RadioButton();
            this.rdParkSystem = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnParkApply = new System.Windows.Forms.Button();
            this.gpDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpDB
            // 
            this.gpDB.Controls.Add(this.txtParkPasswd);
            this.gpDB.Controls.Add(this.txtParkDB);
            this.gpDB.Controls.Add(this.txtParkUserID);
            this.gpDB.Controls.Add(this.txtParkServer);
            this.gpDB.Controls.Add(this.label6);
            this.gpDB.Controls.Add(this.label5);
            this.gpDB.Controls.Add(this.label4);
            this.gpDB.Controls.Add(this.label3);
            this.gpDB.Controls.Add(this.rdParkUser);
            this.gpDB.Controls.Add(this.rdParkSystem);
            this.gpDB.Location = new System.Drawing.Point(3, 6);
            this.gpDB.Name = "gpDB";
            this.gpDB.Size = new System.Drawing.Size(436, 101);
            this.gpDB.TabIndex = 8;
            this.gpDB.TabStop = false;
            // 
            // txtParkPasswd
            // 
            this.txtParkPasswd.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtParkPasswd.Location = new System.Drawing.Point(272, 70);
            this.txtParkPasswd.Name = "txtParkPasswd";
            this.txtParkPasswd.PasswordChar = '*';
            this.txtParkPasswd.Size = new System.Drawing.Size(139, 21);
            this.txtParkPasswd.TabIndex = 3;
            // 
            // txtParkDB
            // 
            this.txtParkDB.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtParkDB.Location = new System.Drawing.Point(272, 42);
            this.txtParkDB.Name = "txtParkDB";
            this.txtParkDB.Size = new System.Drawing.Size(139, 21);
            this.txtParkDB.TabIndex = 1;
            // 
            // txtParkUserID
            // 
            this.txtParkUserID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtParkUserID.Location = new System.Drawing.Point(66, 70);
            this.txtParkUserID.Name = "txtParkUserID";
            this.txtParkUserID.Size = new System.Drawing.Size(139, 21);
            this.txtParkUserID.TabIndex = 2;
            // 
            // txtParkServer
            // 
            this.txtParkServer.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtParkServer.Location = new System.Drawing.Point(66, 42);
            this.txtParkServer.Name = "txtParkServer";
            this.txtParkServer.Size = new System.Drawing.Size(139, 21);
            this.txtParkServer.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(234, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(222, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "数据库：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(16, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "服务器：";
            // 
            // rdParkUser
            // 
            this.rdParkUser.AutoSize = true;
            this.rdParkUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdParkUser.Location = new System.Drawing.Point(175, 20);
            this.rdParkUser.Name = "rdParkUser";
            this.rdParkUser.Size = new System.Drawing.Size(107, 16);
            this.rdParkUser.TabIndex = 5;
            this.rdParkUser.Text = "用户名密码验证";
            this.rdParkUser.UseVisualStyleBackColor = true;
            this.rdParkUser.CheckedChanged += new System.EventHandler(this.rdParkSystem_CheckedChanged);
            // 
            // rdParkSystem
            // 
            this.rdParkSystem.AutoSize = true;
            this.rdParkSystem.Checked = true;
            this.rdParkSystem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdParkSystem.Location = new System.Drawing.Point(41, 20);
            this.rdParkSystem.Name = "rdParkSystem";
            this.rdParkSystem.Size = new System.Drawing.Size(95, 16);
            this.rdParkSystem.TabIndex = 4;
            this.rdParkSystem.TabStop = true;
            this.rdParkSystem.Text = "系统集成验证";
            this.rdParkSystem.UseVisualStyleBackColor = true;
            this.rdParkSystem.CheckedChanged += new System.EventHandler(this.rdParkSystem_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(327, 122);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 42);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnParkApply
            // 
            this.btnParkApply.Location = new System.Drawing.Point(198, 122);
            this.btnParkApply.Name = "btnParkApply";
            this.btnParkApply.Size = new System.Drawing.Size(107, 42);
            this.btnParkApply.TabIndex = 9;
            this.btnParkApply.Text = "确定(&O)";
            this.btnParkApply.UseVisualStyleBackColor = true;
            this.btnParkApply.Click += new System.EventHandler(this.btnParkApply_Click);
            // 
            // FrmConnect
            // 
            this.AcceptButton = this.btnParkApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(445, 173);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gpDB);
            this.Controls.Add(this.btnParkApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库连接设置";
            this.Load += new System.EventHandler(this.FrmConnect_Load);
            this.gpDB.ResumeLayout(false);
            this.gpDB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpDB;
        private System.Windows.Forms.Button btnParkApply;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtParkPasswd;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtParkDB;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtParkUserID;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtParkServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdParkUser;
        private System.Windows.Forms.RadioButton rdParkSystem;
        private System.Windows.Forms.Button btnClose;
    }
}