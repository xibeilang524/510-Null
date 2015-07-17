namespace Ralid.Park.UI
{
    partial class FrmHostStandbySetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHostStandbySetting));
            this.label1 = new System.Windows.Forms.Label();
            this.ucipHost = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ucipStandby = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.gvSMSList = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTelephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comPark = new Ralid.Park.UserControls.ParkCombobox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSendSMS = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvSMSList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ucipHost
            // 
            resources.ApplyResources(this.ucipHost, "ucipHost");
            this.ucipHost.Name = "ucipHost";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ucipStandby
            // 
            resources.ApplyResources(this.ucipStandby, "ucipStandby");
            this.ucipStandby.Name = "ucipStandby";
            // 
            // gvSMSList
            // 
            this.gvSMSList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSMSList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colTelephone,
            this.colMemo});
            resources.ApplyResources(this.gvSMSList, "gvSMSList");
            this.gvSMSList.Name = "gvSMSList";
            this.gvSMSList.RowTemplate.Height = 23;
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.Name = "colName";
            // 
            // colTelephone
            // 
            resources.ApplyResources(this.colTelephone, "colTelephone");
            this.colTelephone.Name = "colTelephone";
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // comPark
            // 
            this.comPark.FormattingEnabled = true;
            resources.ApplyResources(this.comPark, "comPark");
            this.comPark.Name = "comPark";
            this.comPark.SelectedIndexChanged += new System.EventHandler(this.comPark_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.gvSMSList);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkSendSMS
            // 
            resources.ApplyResources(this.chkSendSMS, "chkSendSMS");
            this.chkSendSMS.Name = "chkSendSMS";
            this.chkSendSMS.UseVisualStyleBackColor = true;
            // 
            // FrmHostStandbySetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkSendSMS);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.comPark);
            this.Controls.Add(this.ucipStandby);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucipHost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHostStandbySetting";
            this.Load += new System.EventHandler(this.FrmHostStandbySetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvSMSList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.UCIPTextBox ucipHost;
        private System.Windows.Forms.Label label2;
        private GeneralLibrary.WinformControl.UCIPTextBox ucipStandby;
        private System.Windows.Forms.DataGridView gvSMSList;
        private UserControls.ParkCombobox comPark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSendSMS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTelephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
    }
}