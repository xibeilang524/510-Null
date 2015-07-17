namespace PreferentialSystem
{
    partial class FrmPREOperatorDetail
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
            this.btnChangePwd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOperatorNum = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtOperatorName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtOperatorID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comRoleList = new Ralid.Park.UserControls.PRERoleComboBox(this.components);
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(293, 36);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(293, 103);
            // 
            // btnChangePwd
            // 
            this.btnChangePwd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChangePwd.Location = new System.Drawing.Point(211, 68);
            this.btnChangePwd.Name = "btnChangePwd";
            this.btnChangePwd.Size = new System.Drawing.Size(28, 23);
            this.btnChangePwd.TabIndex = 31;
            this.btnChangePwd.Text = "改";
            this.btnChangePwd.UseVisualStyleBackColor = true;
            this.btnChangePwd.Click += new System.EventHandler(this.btnChangePwd_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(174, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "(1-255)";
            // 
            // txtOperatorNum
            // 
            this.txtOperatorNum.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtOperatorNum.Location = new System.Drawing.Point(89, 125);
            this.txtOperatorNum.MaxValue = 255;
            this.txtOperatorNum.MinValue = 1;
            this.txtOperatorNum.Name = "txtOperatorNum";
            this.txtOperatorNum.Size = new System.Drawing.Size(69, 21);
            this.txtOperatorNum.TabIndex = 27;
            this.txtOperatorNum.Text = "1";
            // 
            // txtOperatorName
            // 
            this.txtOperatorName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtOperatorName.Location = new System.Drawing.Point(89, 40);
            this.txtOperatorName.MaxLength = 20;
            this.txtOperatorName.Name = "txtOperatorName";
            this.txtOperatorName.Size = new System.Drawing.Size(150, 21);
            this.txtOperatorName.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(32, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "真实姓名:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(8, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "操作员编号:";
            // 
            // txtPassword
            // 
            this.txtPassword.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPassword.Location = new System.Drawing.Point(89, 68);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(115, 21);
            this.txtPassword.TabIndex = 24;
            // 
            // txtOperatorID
            // 
            this.txtOperatorID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtOperatorID.Location = new System.Drawing.Point(89, 12);
            this.txtOperatorID.MaxLength = 20;
            this.txtOperatorID.Name = "txtOperatorID";
            this.txtOperatorID.Size = new System.Drawing.Size(150, 21);
            this.txtOperatorID.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(50, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "角色:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(50, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "密码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(44, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "登录ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(259, -77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1, 300);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // comRoleList
            // 
            this.comRoleList.FormattingEnabled = true;
            this.comRoleList.Location = new System.Drawing.Point(89, 98);
            this.comRoleList.Name = "comRoleList";
            this.comRoleList.Size = new System.Drawing.Size(150, 20);
            this.comRoleList.TabIndex = 33;
            // 
            // FrmPREOperatorDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 150);
            this.Controls.Add(this.comRoleList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnChangePwd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOperatorNum);
            this.Controls.Add(this.txtOperatorName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtOperatorID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmPREOperatorDetail";
            this.Text = "FrmPREOperatorDetail";
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtOperatorID, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtOperatorName, 0);
            this.Controls.SetChildIndex(this.txtOperatorNum, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.btnChangePwd, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.comRoleList, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangePwd;
        private System.Windows.Forms.Label label8;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtOperatorNum;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOperatorName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPassword;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOperatorID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.PRERoleComboBox comRoleList;

    }
}