namespace Ralid.Park.UI
{
    partial class FrmCarPlateFailDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarPlateFailDetail));
            this.btnClose = new System.Windows.Forms.Button();
            this.ucVideoes = new Ralid.Park.UserControls.VideoPanels.UCVideoListView();
            this.picIn = new Ralid.Park.UserControls.UCPictureListView();
            this.picOut = new Ralid.Park.UserControls.UCPictureListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblEnterCarPlate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblExitCarPlate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblEnterDateTime = new System.Windows.Forms.Label();
            this.lblExitDateTime = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // picIn
            // 
            resources.ApplyResources(this.picIn, "picIn");
            this.picIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIn.Name = "picIn";
            // 
            // picOut
            // 
            resources.ApplyResources(this.picOut, "picOut");
            this.picOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOut.Name = "picOut";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblEnterCarPlate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblExitCarPlate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblEnterDateTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblExitDateTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtCardID, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // lblEnterCarPlate
            // 
            resources.ApplyResources(this.lblEnterCarPlate, "lblEnterCarPlate");
            this.lblEnterCarPlate.Name = "lblEnterCarPlate";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // lblExitCarPlate
            // 
            resources.ApplyResources(this.lblExitCarPlate, "lblExitCarPlate");
            this.lblExitCarPlate.Name = "lblExitCarPlate";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // lblEnterDateTime
            // 
            resources.ApplyResources(this.lblEnterDateTime, "lblEnterDateTime");
            this.lblEnterDateTime.Name = "lblEnterDateTime";
            // 
            // lblExitDateTime
            // 
            resources.ApplyResources(this.lblExitDateTime, "lblExitDateTime");
            this.lblExitDateTime.Name = "lblExitDateTime";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardID.Name = "txtCardID";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.picIn);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.picOut);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FrmCarPlateFailDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucVideoes);
            this.Name = "FrmCarPlateFailDetail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCarPlateFailDetail_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCarPlateFailDetail_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private UserControls.VideoPanels.UCVideoListView ucVideoes;
        private UserControls.UCPictureListView picIn;
        private UserControls.UCPictureListView picOut;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblEnterCarPlate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblExitCarPlate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblEnterDateTime;
        private System.Windows.Forms.Label lblExitDateTime;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClear;
    }
}