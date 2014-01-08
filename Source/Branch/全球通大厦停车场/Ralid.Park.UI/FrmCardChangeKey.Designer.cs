namespace Ralid.Park.UI
{
    partial class FrmCardChangeKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardChangeKey));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtOldKey = new Ralid.GeneralLibrary.WinformControl.HexTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.chkShowOld = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbInputKey = new System.Windows.Forms.RadioButton();
            this.rdbDefaultKey = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParkingStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.rdbInputKey);
            this.groupBox2.Controls.Add(this.rdbDefaultKey);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtOldKey);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.chkShowOld);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // txtOldKey
            // 
            resources.ApplyResources(this.txtOldKey, "txtOldKey");
            this.txtOldKey.Name = "txtOldKey";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // chkShowOld
            // 
            resources.ApplyResources(this.chkShowOld, "chkShowOld");
            this.chkShowOld.Name = "chkShowOld";
            this.chkShowOld.UseVisualStyleBackColor = true;
            this.chkShowOld.CheckedChanged += new System.EventHandler(this.chkShowOld_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // rdbInputKey
            // 
            resources.ApplyResources(this.rdbInputKey, "rdbInputKey");
            this.rdbInputKey.Name = "rdbInputKey";
            this.rdbInputKey.UseVisualStyleBackColor = true;
            this.rdbInputKey.CheckedChanged += new System.EventHandler(this.rdbInputKey_CheckedChanged);
            // 
            // rdbDefaultKey
            // 
            resources.ApplyResources(this.rdbDefaultKey, "rdbDefaultKey");
            this.rdbDefaultKey.Checked = true;
            this.rdbDefaultKey.Name = "rdbDefaultKey";
            this.rdbDefaultKey.TabStop = true;
            this.rdbDefaultKey.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Name = "label2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colCardType,
            this.colCarPlate,
            this.colParkingStatus,
            this.colFill});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colCardID
            // 
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCount});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lblCount
            // 
            this.lblCount.Name = "lblCount";
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Spring = true;
            // 
            // FrmCardChangeKey
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCardChangeKey";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardChangeKey_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardChangeKey_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbInputKey;
        private System.Windows.Forms.RadioButton rdbDefaultKey;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel1;
        private GeneralLibrary.WinformControl.HexTextBox txtOldKey;
        private System.Windows.Forms.CheckBox chkShowOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParkingStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFill;
    }
}