namespace Ralid.Park.UI
{
    partial class FrmOperatorDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperatorDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOperatorID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtPassword = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.comRoleList = new Ralid.Park.UserControls.RoleComboBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOperatorName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtOperatorNum = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.btnChangePwd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label4.Name = "label4";
            // 
            // txtOperatorID
            // 
            resources.ApplyResources(this.txtOperatorID, "txtOperatorID");
            this.txtOperatorID.Name = "txtOperatorID";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label5.Name = "label5";
            // 
            // comRoleList
            // 
            resources.ApplyResources(this.comRoleList, "comRoleList");
            this.comRoleList.FormattingEnabled = true;
            this.comRoleList.Name = "comRoleList";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label3.Name = "label3";
            // 
            // txtOperatorName
            // 
            resources.ApplyResources(this.txtOperatorName, "txtOperatorName");
            this.txtOperatorName.Name = "txtOperatorName";
            // 
            // txtOperatorNum
            // 
            resources.ApplyResources(this.txtOperatorNum, "txtOperatorNum");
            this.txtOperatorNum.MaxValue = 255;
            this.txtOperatorNum.MinValue = 1;
            this.txtOperatorNum.Name = "txtOperatorNum";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label8.Name = "label8";
            // 
            // btnChangePwd
            // 
            resources.ApplyResources(this.btnChangePwd, "btnChangePwd");
            this.btnChangePwd.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnChangePwd.Name = "btnChangePwd";
            this.btnChangePwd.UseVisualStyleBackColor = true;
            this.btnChangePwd.Click += new System.EventHandler(this.btnChangePwd_Click);
            // 
            // FrmOperatorDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnChangePwd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOperatorNum);
            this.Controls.Add(this.txtOperatorName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comRoleList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtOperatorID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmOperatorDetail";
            this.ShowInTaskbar = false;
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtOperatorID, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.comRoleList, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtOperatorName, 0);
            this.Controls.SetChildIndex(this.txtOperatorNum, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.btnChangePwd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOperatorID;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private Ralid.Park.UserControls.RoleComboBox comRoleList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOperatorName;
        private Ralid.GeneralLibrary .WinformControl .IntergerTextBox txtOperatorNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnChangePwd;
    }
}