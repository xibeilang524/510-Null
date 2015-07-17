namespace Ralid.Park.UI
{
    partial class FrmHotelAuthorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHotelAuthorization));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rabdays = new System.Windows.Forms.RadioButton();
            this.rabhours = new System.Windows.Forms.RadioButton();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtDays = new System.Windows.Forms.NumericUpDown();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblInPark = new System.Windows.Forms.Label();
            this.lblEnterDateTime = new System.Windows.Forms.Label();
            this.lblAuthorization = new System.Windows.Forms.Label();
            this.lblOldFreeDateTime = new System.Windows.Forms.Label();
            this.lblFreeDateTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.TextChanged += new System.EventHandler(this.txtCardID_TextChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lblCheckOut);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.txtDays);
            this.groupBox1.Controls.Add(this.btnCheckOut);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lblInPark);
            this.groupBox1.Controls.Add(this.lblEnterDateTime);
            this.groupBox1.Controls.Add(this.lblAuthorization);
            this.groupBox1.Controls.Add(this.lblOldFreeDateTime);
            this.groupBox1.Controls.Add(this.lblFreeDateTime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCardID);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rabdays);
            this.panel1.Controls.Add(this.rabhours);
            this.panel1.Name = "panel1";
            // 
            // rabdays
            // 
            resources.ApplyResources(this.rabdays, "rabdays");
            this.rabdays.Checked = true;
            this.rabdays.Name = "rabdays";
            this.rabdays.TabStop = true;
            this.rabdays.UseVisualStyleBackColor = true;
            this.rabdays.CheckedChanged += new System.EventHandler(this.rabdays_CheckedChanged);
            // 
            // rabhours
            // 
            resources.ApplyResources(this.rabhours, "rabhours");
            this.rabhours.Name = "rabhours";
            this.rabhours.UseVisualStyleBackColor = true;
            this.rabhours.CheckedChanged += new System.EventHandler(this.rabhours_CheckedChanged);
            // 
            // lblCheckOut
            // 
            resources.ApplyResources(this.lblCheckOut, "lblCheckOut");
            this.lblCheckOut.Name = "lblCheckOut";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Name = "label9";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtDays
            // 
            resources.ApplyResources(this.txtDays, "txtDays");
            this.txtDays.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.txtDays.Name = "txtDays";
            this.txtDays.ValueChanged += new System.EventHandler(this.txtDays_ValueChanged);
            // 
            // btnCheckOut
            // 
            resources.ApplyResources(this.btnCheckOut, "btnCheckOut");
            this.btnCheckOut.BackColor = System.Drawing.SystemColors.Control;
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.UseVisualStyleBackColor = false;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::Ralid.Park.UI.Properties.Resources.CardReader;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // lblInPark
            // 
            resources.ApplyResources(this.lblInPark, "lblInPark");
            this.lblInPark.Name = "lblInPark";
            // 
            // lblEnterDateTime
            // 
            resources.ApplyResources(this.lblEnterDateTime, "lblEnterDateTime");
            this.lblEnterDateTime.Name = "lblEnterDateTime";
            // 
            // lblAuthorization
            // 
            resources.ApplyResources(this.lblAuthorization, "lblAuthorization");
            this.lblAuthorization.Name = "lblAuthorization";
            // 
            // lblOldFreeDateTime
            // 
            resources.ApplyResources(this.lblOldFreeDateTime, "lblOldFreeDateTime");
            this.lblOldFreeDateTime.Name = "lblOldFreeDateTime";
            // 
            // lblFreeDateTime
            // 
            resources.ApplyResources(this.lblFreeDateTime, "lblFreeDateTime");
            this.lblFreeDateTime.Name = "lblFreeDateTime";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Name = "label11";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
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
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // FrmHotelAuthorization
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "FrmHotelAuthorization";
            this.Activated += new System.EventHandler(this.FrmHotelAuthorization_Activated);
            this.Deactivate += new System.EventHandler(this.FrmHotelAuthorization_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmHotelAuthorization_FormClosed);
            this.Load += new System.EventHandler(this.FrmHotelAuthorization_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHotelAuthorization_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblEnterDateTime;
        private System.Windows.Forms.Label lblAuthorization;
        private System.Windows.Forms.Label lblFreeDateTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.NumericUpDown txtDays;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblInPark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblOldFreeDateTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rabhours;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rabdays;
    }
}