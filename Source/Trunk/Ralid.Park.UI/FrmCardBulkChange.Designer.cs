namespace Ralid.Park.UI
{
    partial class FrmCardBulkChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardBulkChange));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkOnlineHandleWhenOfflineMode = new System.Windows.Forms.CheckBox();
            this.chkEnableWhenExpired = new System.Windows.Forms.CheckBox();
            this.chkCanEnterWhenFull = new System.Windows.Forms.CheckBox();
            this.chkRepeatIn = new System.Windows.Forms.CheckBox();
            this.chkWithCount = new System.Windows.Forms.CheckBox();
            this.chkRepeatOut = new System.Windows.Forms.CheckBox();
            this.chkHoliday = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.comAccessLevel = new Ralid.Park.UserControls.AccessComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblAlarm = new System.Windows.Forms.Label();
            this.chkWriteCard = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.chkOnlineHandleWhenOfflineMode);
            this.groupBox3.Controls.Add(this.chkEnableWhenExpired);
            this.groupBox3.Controls.Add(this.chkCanEnterWhenFull);
            this.groupBox3.Controls.Add(this.chkRepeatIn);
            this.groupBox3.Controls.Add(this.chkWithCount);
            this.groupBox3.Controls.Add(this.chkRepeatOut);
            this.groupBox3.Controls.Add(this.chkHoliday);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // chkOnlineHandleWhenOfflineMode
            // 
            resources.ApplyResources(this.chkOnlineHandleWhenOfflineMode, "chkOnlineHandleWhenOfflineMode");
            this.chkOnlineHandleWhenOfflineMode.Name = "chkOnlineHandleWhenOfflineMode";
            this.chkOnlineHandleWhenOfflineMode.UseVisualStyleBackColor = true;
            // 
            // chkEnableWhenExpired
            // 
            resources.ApplyResources(this.chkEnableWhenExpired, "chkEnableWhenExpired");
            this.chkEnableWhenExpired.Name = "chkEnableWhenExpired";
            this.chkEnableWhenExpired.UseVisualStyleBackColor = true;
            // 
            // chkCanEnterWhenFull
            // 
            resources.ApplyResources(this.chkCanEnterWhenFull, "chkCanEnterWhenFull");
            this.chkCanEnterWhenFull.Name = "chkCanEnterWhenFull";
            this.chkCanEnterWhenFull.UseVisualStyleBackColor = true;
            // 
            // chkRepeatIn
            // 
            resources.ApplyResources(this.chkRepeatIn, "chkRepeatIn");
            this.chkRepeatIn.Name = "chkRepeatIn";
            this.chkRepeatIn.UseVisualStyleBackColor = true;
            // 
            // chkWithCount
            // 
            resources.ApplyResources(this.chkWithCount, "chkWithCount");
            this.chkWithCount.Checked = true;
            this.chkWithCount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWithCount.Name = "chkWithCount";
            this.chkWithCount.UseVisualStyleBackColor = true;
            // 
            // chkRepeatOut
            // 
            resources.ApplyResources(this.chkRepeatOut, "chkRepeatOut");
            this.chkRepeatOut.Name = "chkRepeatOut";
            this.chkRepeatOut.UseVisualStyleBackColor = true;
            // 
            // chkHoliday
            // 
            resources.ApplyResources(this.chkHoliday, "chkHoliday");
            this.chkHoliday.Checked = true;
            this.chkHoliday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHoliday.Name = "chkHoliday";
            this.chkHoliday.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // comAccessLevel
            // 
            resources.ApplyResources(this.comAccessLevel, "comAccessLevel");
            this.comAccessLevel.FormattingEnabled = true;
            this.comAccessLevel.Name = "comAccessLevel";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // lblAlarm
            // 
            resources.ApplyResources(this.lblAlarm, "lblAlarm");
            this.lblAlarm.Name = "lblAlarm";
            // 
            // chkWriteCard
            // 
            resources.ApplyResources(this.chkWriteCard, "chkWriteCard");
            this.chkWriteCard.Name = "chkWriteCard";
            this.chkWriteCard.UseVisualStyleBackColor = true;
            this.chkWriteCard.CheckedChanged += new System.EventHandler(this.chkWriteCard_CheckedChanged);
            // 
            // FrmCardBulkChange
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.chkWriteCard);
            this.Controls.Add(this.lblAlarm);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.comAccessLevel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCardBulkChange";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardBulkChange_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardBulkChange_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkEnableWhenExpired;
        private System.Windows.Forms.CheckBox chkCanEnterWhenFull;
        private System.Windows.Forms.CheckBox chkRepeatIn;
        private System.Windows.Forms.CheckBox chkWithCount;
        private System.Windows.Forms.CheckBox chkRepeatOut;
        private System.Windows.Forms.CheckBox chkHoliday;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Button btnOk;
        private UserControls.AccessComboBox comAccessLevel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.CheckBox chkWriteCard;
        private System.Windows.Forms.CheckBox chkOnlineHandleWhenOfflineMode;
    }
}