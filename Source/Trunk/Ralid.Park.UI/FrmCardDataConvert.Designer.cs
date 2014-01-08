namespace Ralid.Park.UI
{
    partial class FrmCardDataConvert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardDataConvert));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtCardCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdToDatabase = new System.Windows.Forms.RadioButton();
            this.rdToCard = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.pnlHandleMode = new System.Windows.Forms.Panel();
            this.rdbOnlineHandle = new System.Windows.Forms.RadioButton();
            this.rdbOfflineHandle = new System.Windows.Forms.RadioButton();
            this.chkHandleMode = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParkingStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlHandleMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCount});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.Spring = true;
            // 
            // txtCardCertificate
            // 
            resources.ApplyResources(this.txtCardCertificate, "txtCardCertificate");
            this.txtCardCertificate.Name = "txtCardCertificate";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtOwnerName
            // 
            resources.ApplyResources(this.txtOwnerName, "txtOwnerName");
            this.txtOwnerName.Name = "txtOwnerName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rdToDatabase);
            this.panel1.Controls.Add(this.rdToCard);
            this.panel1.Name = "panel1";
            // 
            // rdToDatabase
            // 
            resources.ApplyResources(this.rdToDatabase, "rdToDatabase");
            this.rdToDatabase.Name = "rdToDatabase";
            this.rdToDatabase.UseVisualStyleBackColor = true;
            // 
            // rdToCard
            // 
            resources.ApplyResources(this.rdToCard, "rdToCard");
            this.rdToCard.Checked = true;
            this.rdToCard.Name = "rdToCard";
            this.rdToCard.TabStop = true;
            this.rdToCard.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colOwnerName,
            this.colCertificate,
            this.colCardType,
            this.colCarPlate,
            this.colParkingStatus,
            this.colFill});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnFilter
            // 
            resources.ApplyResources(this.btnFilter, "btnFilter");
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Name = "label1";
            // 
            // btnConvert
            // 
            resources.ApplyResources(this.btnConvert, "btnConvert");
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // pnlHandleMode
            // 
            resources.ApplyResources(this.pnlHandleMode, "pnlHandleMode");
            this.pnlHandleMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHandleMode.Controls.Add(this.rdbOnlineHandle);
            this.pnlHandleMode.Controls.Add(this.rdbOfflineHandle);
            this.pnlHandleMode.Name = "pnlHandleMode";
            // 
            // rdbOnlineHandle
            // 
            resources.ApplyResources(this.rdbOnlineHandle, "rdbOnlineHandle");
            this.rdbOnlineHandle.Name = "rdbOnlineHandle";
            this.rdbOnlineHandle.UseVisualStyleBackColor = true;
            // 
            // rdbOfflineHandle
            // 
            resources.ApplyResources(this.rdbOfflineHandle, "rdbOfflineHandle");
            this.rdbOfflineHandle.Checked = true;
            this.rdbOfflineHandle.Name = "rdbOfflineHandle";
            this.rdbOfflineHandle.TabStop = true;
            this.rdbOfflineHandle.UseVisualStyleBackColor = true;
            // 
            // chkHandleMode
            // 
            resources.ApplyResources(this.chkHandleMode, "chkHandleMode");
            this.chkHandleMode.Name = "chkHandleMode";
            this.chkHandleMode.UseVisualStyleBackColor = true;
            this.chkHandleMode.CheckedChanged += new System.EventHandler(this.chkHandleMode_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // colCardID
            // 
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colOwnerName
            // 
            resources.ApplyResources(this.colOwnerName, "colOwnerName");
            this.colOwnerName.Name = "colOwnerName";
            this.colOwnerName.ReadOnly = true;
            // 
            // colCertificate
            // 
            resources.ApplyResources(this.colCertificate, "colCertificate");
            this.colCertificate.Name = "colCertificate";
            this.colCertificate.ReadOnly = true;
            // 
            // colCardType
            // 
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colParkingStatus
            // 
            resources.ApplyResources(this.colParkingStatus, "colParkingStatus");
            this.colParkingStatus.Name = "colParkingStatus";
            this.colParkingStatus.ReadOnly = true;
            // 
            // colFill
            // 
            this.colFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colFill, "colFill");
            this.colFill.Name = "colFill";
            this.colFill.ReadOnly = true;
            // 
            // FrmCardDataConvert
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkHandleMode);
            this.Controls.Add(this.pnlHandleMode);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtCardCertificate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCarPlate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOwnerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comCardType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConvert);
            this.Name = "FrmCardDataConvert";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardDataConvert_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardDataConvert_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlHandleMode.ResumeLayout(false);
            this.pnlHandleMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCount;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardCertificate;
        private System.Windows.Forms.Label label8;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label4;
        private GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label2;
        private UserControls.CardTypeComboBox comCardType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdToDatabase;
        private System.Windows.Forms.RadioButton rdToCard;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Panel pnlHandleMode;
        private System.Windows.Forms.RadioButton rdbOnlineHandle;
        private System.Windows.Forms.RadioButton rdbOfflineHandle;
        private System.Windows.Forms.CheckBox chkHandleMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParkingStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFill;
    }
}