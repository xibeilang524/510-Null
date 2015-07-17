namespace Ralid.Park.UI
{
    partial class FrmCarPlateOfDaHua
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarPlateOfDaHua));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSnap = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.textBoxLane = new System.Windows.Forms.TextBox();
            this.pictureBoxSnapshot = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBoxCar = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxAutoColor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPlate = new System.Windows.Forms.TextBox();
            this.textBoxPlateColor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxImg = new System.Windows.Forms.PictureBox();
            this.btnRealPlay = new System.Windows.Forms.Button();
            this.btnStopRealPlay = new System.Windows.Forms.Button();
            this.colEntranceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVideoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnapshot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCar)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEntranceName,
            this.colIP,
            this.colVideoID,
            this.colState,
            this.colEventDate,
            this.colCarPlate});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.btnSnap);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxTime);
            this.groupBox2.Controls.Add(this.textBoxLane);
            this.groupBox2.Controls.Add(this.pictureBoxSnapshot);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.pictureBoxCar);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBoxAutoColor);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxPlate);
            this.groupBox2.Controls.Add(this.textBoxPlateColor);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnSnap
            // 
            resources.ApplyResources(this.btnSnap, "btnSnap");
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // textBoxTime
            // 
            resources.ApplyResources(this.textBoxTime, "textBoxTime");
            this.textBoxTime.Name = "textBoxTime";
            // 
            // textBoxLane
            // 
            resources.ApplyResources(this.textBoxLane, "textBoxLane");
            this.textBoxLane.Name = "textBoxLane";
            // 
            // pictureBoxSnapshot
            // 
            resources.ApplyResources(this.pictureBoxSnapshot, "pictureBoxSnapshot");
            this.pictureBoxSnapshot.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBoxSnapshot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSnapshot.Name = "pictureBoxSnapshot";
            this.pictureBoxSnapshot.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // pictureBoxCar
            // 
            resources.ApplyResources(this.pictureBoxCar, "pictureBoxCar");
            this.pictureBoxCar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBoxCar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxCar.Name = "pictureBoxCar";
            this.pictureBoxCar.TabStop = false;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // textBoxAutoColor
            // 
            resources.ApplyResources(this.textBoxAutoColor, "textBoxAutoColor");
            this.textBoxAutoColor.Name = "textBoxAutoColor";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBoxPlate
            // 
            resources.ApplyResources(this.textBoxPlate, "textBoxPlate");
            this.textBoxPlate.Name = "textBoxPlate";
            // 
            // textBoxPlateColor
            // 
            resources.ApplyResources(this.textBoxPlateColor, "textBoxPlateColor");
            this.textBoxPlateColor.Name = "textBoxPlateColor";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.pictureBoxImg);
            this.groupBox3.Controls.Add(this.btnRealPlay);
            this.groupBox3.Controls.Add(this.btnStopRealPlay);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // pictureBoxImg
            // 
            resources.ApplyResources(this.pictureBoxImg, "pictureBoxImg");
            this.pictureBoxImg.BackColor = System.Drawing.SystemColors.MenuText;
            this.pictureBoxImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxImg.Name = "pictureBoxImg";
            this.pictureBoxImg.TabStop = false;
            // 
            // btnRealPlay
            // 
            resources.ApplyResources(this.btnRealPlay, "btnRealPlay");
            this.btnRealPlay.Name = "btnRealPlay";
            this.btnRealPlay.UseVisualStyleBackColor = true;
            this.btnRealPlay.Click += new System.EventHandler(this.btnRealPlay_Click);
            // 
            // btnStopRealPlay
            // 
            resources.ApplyResources(this.btnStopRealPlay, "btnStopRealPlay");
            this.btnStopRealPlay.Name = "btnStopRealPlay";
            this.btnStopRealPlay.UseVisualStyleBackColor = true;
            this.btnStopRealPlay.Click += new System.EventHandler(this.btnStopRealPlay_Click);
            // 
            // colEntranceName
            // 
            resources.ApplyResources(this.colEntranceName, "colEntranceName");
            this.colEntranceName.Name = "colEntranceName";
            this.colEntranceName.ReadOnly = true;
            // 
            // colIP
            // 
            resources.ApplyResources(this.colIP, "colIP");
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            // 
            // colVideoID
            // 
            resources.ApplyResources(this.colVideoID, "colVideoID");
            this.colVideoID.Name = "colVideoID";
            this.colVideoID.ReadOnly = true;
            // 
            // colState
            // 
            resources.ApplyResources(this.colState, "colState");
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            // 
            // colEventDate
            // 
            resources.ApplyResources(this.colEventDate, "colEventDate");
            this.colEventDate.Name = "colEventDate";
            this.colEventDate.ReadOnly = true;
            // 
            // colCarPlate
            // 
            this.colCarPlate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // FrmCarPlateOfDaHua
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmCarPlateOfDaHua";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCarPlateOfDaHua_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnapshot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSnap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.TextBox textBoxLane;
        private System.Windows.Forms.PictureBox pictureBoxSnapshot;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBoxCar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxAutoColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPlate;
        private System.Windows.Forms.TextBox textBoxPlateColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBoxImg;
        private System.Windows.Forms.Button btnRealPlay;
        private System.Windows.Forms.Button btnStopRealPlay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVideoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
    }
}