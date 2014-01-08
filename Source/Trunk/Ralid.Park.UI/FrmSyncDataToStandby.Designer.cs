namespace Ralid.Park.UI
{
    partial class FrmSyncDataToStandby
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSyncDataToStandby));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMaster = new System.Windows.Forms.Label();
            this.lblStandby = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSyncMsg = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new Ralid.GeneralLibrary.WinformControl.PercentageProgressBar(this.components);
            this.txtInfo = new System.Windows.Forms.RichTextBox();
            this.gbSyncSelect = new System.Windows.Forms.GroupBox();
            this.chkCards = new System.Windows.Forms.CheckBox();
            this.chkVideoSources = new System.Windows.Forms.CheckBox();
            this.chkEntrances = new System.Windows.Forms.CheckBox();
            this.chkParks = new System.Windows.Forms.CheckBox();
            this.chkWorkStations = new System.Windows.Forms.CheckBox();
            this.chkRoles = new System.Windows.Forms.CheckBox();
            this.chkOperators = new System.Windows.Forms.CheckBox();
            this.chkSystemOptions = new System.Windows.Forms.CheckBox();
            this.gbDatabase = new System.Windows.Forms.GroupBox();
            this.gbSyncSelect.SuspendLayout();
            this.gbDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblMaster
            // 
            resources.ApplyResources(this.lblMaster, "lblMaster");
            this.lblMaster.Name = "lblMaster";
            // 
            // lblStandby
            // 
            resources.ApplyResources(this.lblStandby, "lblStandby");
            this.lblStandby.Name = "lblStandby";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblSyncMsg
            // 
            resources.ApplyResources(this.lblSyncMsg, "lblSyncMsg");
            this.lblSyncMsg.Name = "lblSyncMsg";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Step = 1;
            this.progressBar1.TextColor = System.Drawing.Color.Black;
            this.progressBar1.TextFont = new System.Drawing.Font("宋体", 9F);
            // 
            // txtInfo
            // 
            resources.ApplyResources(this.txtInfo, "txtInfo");
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            // 
            // gbSyncSelect
            // 
            resources.ApplyResources(this.gbSyncSelect, "gbSyncSelect");
            this.gbSyncSelect.Controls.Add(this.chkCards);
            this.gbSyncSelect.Controls.Add(this.chkVideoSources);
            this.gbSyncSelect.Controls.Add(this.chkEntrances);
            this.gbSyncSelect.Controls.Add(this.chkParks);
            this.gbSyncSelect.Controls.Add(this.chkWorkStations);
            this.gbSyncSelect.Controls.Add(this.chkRoles);
            this.gbSyncSelect.Controls.Add(this.chkOperators);
            this.gbSyncSelect.Controls.Add(this.chkSystemOptions);
            this.gbSyncSelect.Name = "gbSyncSelect";
            this.gbSyncSelect.TabStop = false;
            // 
            // chkCards
            // 
            resources.ApplyResources(this.chkCards, "chkCards");
            this.chkCards.Checked = true;
            this.chkCards.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCards.Name = "chkCards";
            this.chkCards.UseVisualStyleBackColor = true;
            // 
            // chkVideoSources
            // 
            resources.ApplyResources(this.chkVideoSources, "chkVideoSources");
            this.chkVideoSources.Checked = true;
            this.chkVideoSources.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVideoSources.Name = "chkVideoSources";
            this.chkVideoSources.UseVisualStyleBackColor = true;
            // 
            // chkEntrances
            // 
            resources.ApplyResources(this.chkEntrances, "chkEntrances");
            this.chkEntrances.Checked = true;
            this.chkEntrances.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEntrances.Name = "chkEntrances";
            this.chkEntrances.UseVisualStyleBackColor = true;
            // 
            // chkParks
            // 
            resources.ApplyResources(this.chkParks, "chkParks");
            this.chkParks.Checked = true;
            this.chkParks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkParks.Name = "chkParks";
            this.chkParks.UseVisualStyleBackColor = true;
            // 
            // chkWorkStations
            // 
            resources.ApplyResources(this.chkWorkStations, "chkWorkStations");
            this.chkWorkStations.Checked = true;
            this.chkWorkStations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWorkStations.Name = "chkWorkStations";
            this.chkWorkStations.UseVisualStyleBackColor = true;
            // 
            // chkRoles
            // 
            resources.ApplyResources(this.chkRoles, "chkRoles");
            this.chkRoles.Checked = true;
            this.chkRoles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRoles.Name = "chkRoles";
            this.chkRoles.UseVisualStyleBackColor = true;
            // 
            // chkOperators
            // 
            resources.ApplyResources(this.chkOperators, "chkOperators");
            this.chkOperators.Checked = true;
            this.chkOperators.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOperators.Name = "chkOperators";
            this.chkOperators.UseVisualStyleBackColor = true;
            // 
            // chkSystemOptions
            // 
            resources.ApplyResources(this.chkSystemOptions, "chkSystemOptions");
            this.chkSystemOptions.Checked = true;
            this.chkSystemOptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSystemOptions.Name = "chkSystemOptions";
            this.chkSystemOptions.UseVisualStyleBackColor = true;
            // 
            // gbDatabase
            // 
            resources.ApplyResources(this.gbDatabase, "gbDatabase");
            this.gbDatabase.Controls.Add(this.label1);
            this.gbDatabase.Controls.Add(this.label2);
            this.gbDatabase.Controls.Add(this.lblMaster);
            this.gbDatabase.Controls.Add(this.lblStandby);
            this.gbDatabase.Name = "gbDatabase";
            this.gbDatabase.TabStop = false;
            // 
            // FrmSyncDataToStandby
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.gbDatabase);
            this.Controls.Add(this.gbSyncSelect);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblSyncMsg);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSyncDataToStandby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSyncDataToStandby_FormClosing);
            this.Load += new System.EventHandler(this.FrmSyncDataToStandby_Load);
            this.gbSyncSelect.ResumeLayout(false);
            this.gbSyncSelect.PerformLayout();
            this.gbDatabase.ResumeLayout(false);
            this.gbDatabase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.Label lblStandby;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSyncMsg;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timer1;
        private GeneralLibrary.WinformControl.PercentageProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox txtInfo;
        private System.Windows.Forms.GroupBox gbSyncSelect;
        private System.Windows.Forms.CheckBox chkSystemOptions;
        private System.Windows.Forms.CheckBox chkCards;
        private System.Windows.Forms.CheckBox chkVideoSources;
        private System.Windows.Forms.CheckBox chkEntrances;
        private System.Windows.Forms.CheckBox chkParks;
        private System.Windows.Forms.CheckBox chkWorkStations;
        private System.Windows.Forms.CheckBox chkRoles;
        private System.Windows.Forms.CheckBox chkOperators;
        private System.Windows.Forms.GroupBox gbDatabase;
    }
}