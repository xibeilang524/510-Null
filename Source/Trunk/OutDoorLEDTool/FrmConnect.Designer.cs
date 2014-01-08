namespace OutDoorLEDTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnect));
            this.gpDB = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnParkApply = new System.Windows.Forms.Button();
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
            this.gpDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpDB
            // 
            resources.ApplyResources(this.gpDB, "gpDB");
            this.gpDB.Controls.Add(this.btnClose);
            this.gpDB.Controls.Add(this.btnParkApply);
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
            this.gpDB.Name = "gpDB";
            this.gpDB.TabStop = false;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnParkApply
            // 
            resources.ApplyResources(this.btnParkApply, "btnParkApply");
            this.btnParkApply.Name = "btnParkApply";
            this.btnParkApply.UseVisualStyleBackColor = true;
            this.btnParkApply.Click += new System.EventHandler(this.btnParkApply_Click);
            // 
            // txtParkPasswd
            // 
            resources.ApplyResources(this.txtParkPasswd, "txtParkPasswd");
            this.txtParkPasswd.Name = "txtParkPasswd";
            // 
            // txtParkDB
            // 
            resources.ApplyResources(this.txtParkDB, "txtParkDB");
            this.txtParkDB.Name = "txtParkDB";
            // 
            // txtParkUserID
            // 
            resources.ApplyResources(this.txtParkUserID, "txtParkUserID");
            this.txtParkUserID.Name = "txtParkUserID";
            // 
            // txtParkServer
            // 
            resources.ApplyResources(this.txtParkServer, "txtParkServer");
            this.txtParkServer.Name = "txtParkServer";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // rdParkUser
            // 
            resources.ApplyResources(this.rdParkUser, "rdParkUser");
            this.rdParkUser.Name = "rdParkUser";
            this.rdParkUser.UseVisualStyleBackColor = true;
            this.rdParkUser.CheckedChanged += new System.EventHandler(this.rdParkSystem_CheckedChanged);
            // 
            // rdParkSystem
            // 
            resources.ApplyResources(this.rdParkSystem, "rdParkSystem");
            this.rdParkSystem.Checked = true;
            this.rdParkSystem.Name = "rdParkSystem";
            this.rdParkSystem.TabStop = true;
            this.rdParkSystem.UseVisualStyleBackColor = true;
            this.rdParkSystem.CheckedChanged += new System.EventHandler(this.rdParkSystem_CheckedChanged);
            // 
            // FrmConnect
            // 
            this.AcceptButton = this.btnParkApply;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.gpDB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnect";
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