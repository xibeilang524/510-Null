namespace Ralid.Park.UI
{
    partial class FrmSpeedingDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpeedingDetail));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCardID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCarPlate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOwner = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ucVideoes = new Ralid.Park.UserControls.VideoPanels.UCVideoListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvSpeeding = new System.Windows.Forms.DataGridView();
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.colSpeedingDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeeding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.ucVideoes);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCardID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCarPlate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblOwner, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDepartment, 1, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblCardID
            // 
            resources.ApplyResources(this.lblCardID, "lblCardID");
            this.lblCardID.Name = "lblCardID";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblCarPlate
            // 
            resources.ApplyResources(this.lblCarPlate, "lblCarPlate");
            this.lblCarPlate.ForeColor = System.Drawing.Color.Blue;
            this.lblCarPlate.Name = "lblCarPlate";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblOwner
            // 
            resources.ApplyResources(this.lblOwner, "lblOwner");
            this.lblOwner.Name = "lblOwner";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // lblDepartment
            // 
            resources.ApplyResources(this.lblDepartment, "lblDepartment");
            this.lblDepartment.Name = "lblDepartment";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucVideoes
            // 
            resources.ApplyResources(this.ucVideoes, "ucVideoes");
            this.ucVideoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucVideoes.Name = "ucVideoes";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dgvSpeeding);
            this.groupBox2.Controls.Add(this.pbPhoto);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dgvSpeeding
            // 
            resources.ApplyResources(this.dgvSpeeding, "dgvSpeeding");
            this.dgvSpeeding.AllowUserToAddRows = false;
            this.dgvSpeeding.AllowUserToDeleteRows = false;
            this.dgvSpeeding.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpeeding.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSpeedingDateTime,
            this.colSpeed,
            this.colPlace,
            this.colMemo});
            this.dgvSpeeding.Name = "dgvSpeeding";
            this.dgvSpeeding.ReadOnly = true;
            this.dgvSpeeding.RowHeadersVisible = false;
            this.dgvSpeeding.RowTemplate.Height = 23;
            this.dgvSpeeding.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpeeding_RowEnter);
            // 
            // pbPhoto
            // 
            resources.ApplyResources(this.pbPhoto, "pbPhoto");
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.TabStop = false;
            // 
            // colSpeedingDateTime
            // 
            resources.ApplyResources(this.colSpeedingDateTime, "colSpeedingDateTime");
            this.colSpeedingDateTime.Name = "colSpeedingDateTime";
            this.colSpeedingDateTime.ReadOnly = true;
            // 
            // colSpeed
            // 
            resources.ApplyResources(this.colSpeed, "colSpeed");
            this.colSpeed.Name = "colSpeed";
            this.colSpeed.ReadOnly = true;
            // 
            // colPlace
            // 
            this.colPlace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colPlace, "colPlace");
            this.colPlace.Name = "colPlace";
            this.colPlace.ReadOnly = true;
            // 
            // colMemo
            // 
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // FrmSpeedingDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSpeedingDetail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSpeedingDetail_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSpeedingDetail_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpeeding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControls.VideoPanels.UCVideoListView ucVideoes;
        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.DataGridView dgvSpeeding;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCarPlate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpeedingDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
    }
}