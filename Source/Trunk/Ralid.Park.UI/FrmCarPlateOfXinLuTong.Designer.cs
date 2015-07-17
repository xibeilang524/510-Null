namespace Ralid.Park.UI
{
    partial class FrmCarPlateOfXinLuTong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarPlateOfXinLuTong));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.picPlate = new System.Windows.Forms.PictureBox();
            this.video = new System.Windows.Forms.PictureBox();
            this.btnForceSnap = new System.Windows.Forms.Button();
            this.chkVideo = new System.Windows.Forms.CheckBox();
            this.colEntranceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVideoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.video)).BeginInit();
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
            this.colCarPlate,
            this.colEventDate});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // picPlate
            // 
            resources.ApplyResources(this.picPlate, "picPlate");
            this.picPlate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPlate.Name = "picPlate";
            this.picPlate.TabStop = false;
            // 
            // video
            // 
            resources.ApplyResources(this.video, "video");
            this.video.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.video.Name = "video";
            this.video.TabStop = false;
            // 
            // btnForceSnap
            // 
            resources.ApplyResources(this.btnForceSnap, "btnForceSnap");
            this.btnForceSnap.Name = "btnForceSnap";
            this.btnForceSnap.UseVisualStyleBackColor = true;
            this.btnForceSnap.Click += new System.EventHandler(this.btnForceSnap_Click);
            // 
            // chkVideo
            // 
            resources.ApplyResources(this.chkVideo, "chkVideo");
            this.chkVideo.Name = "chkVideo";
            this.chkVideo.UseVisualStyleBackColor = true;
            this.chkVideo.CheckedChanged += new System.EventHandler(this.chkVideo_CheckedChanged);
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
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colEventDate
            // 
            resources.ApplyResources(this.colEventDate, "colEventDate");
            this.colEventDate.Name = "colEventDate";
            this.colEventDate.ReadOnly = true;
            // 
            // FrmCarPlateOfXinLuTong
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkVideo);
            this.Controls.Add(this.btnForceSnap);
            this.Controls.Add(this.picPlate);
            this.Controls.Add(this.video);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmCarPlateOfXinLuTong";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCarPlateOfXinLuTong_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.video)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox picPlate;
        private System.Windows.Forms.PictureBox video;
        private System.Windows.Forms.Button btnForceSnap;
        private System.Windows.Forms.CheckBox chkVideo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVideoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDate;
    }
}