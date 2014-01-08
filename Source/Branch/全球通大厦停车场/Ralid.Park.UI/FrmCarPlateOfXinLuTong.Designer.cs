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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colEntranceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVideoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picPlate = new System.Windows.Forms.PictureBox();
            this.video = new System.Windows.Forms.PictureBox();
            this.btnForceSnap = new System.Windows.Forms.Button();
            this.chkVideo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.video)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
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
            this.dataGridView1.Location = new System.Drawing.Point(7, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(638, 326);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // colEntranceName
            // 
            this.colEntranceName.HeaderText = "通道名称";
            this.colEntranceName.Name = "colEntranceName";
            this.colEntranceName.ReadOnly = true;
            // 
            // colIP
            // 
            this.colIP.HeaderText = "识别器IP";
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            // 
            // colVideoID
            // 
            this.colVideoID.HeaderText = "识别视频";
            this.colVideoID.Name = "colVideoID";
            this.colVideoID.ReadOnly = true;
            // 
            // colState
            // 
            this.colState.HeaderText = "状态";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            // 
            // colCarPlate
            // 
            this.colCarPlate.HeaderText = "上传车牌";
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colEventDate
            // 
            this.colEventDate.HeaderText = "上传时间";
            this.colEventDate.Name = "colEventDate";
            this.colEventDate.ReadOnly = true;
            this.colEventDate.Width = 130;
            // 
            // picPlate
            // 
            this.picPlate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPlate.Location = new System.Drawing.Point(652, 5);
            this.picPlate.Name = "picPlate";
            this.picPlate.Size = new System.Drawing.Size(264, 57);
            this.picPlate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPlate.TabIndex = 14;
            this.picPlate.TabStop = false;
            // 
            // video
            // 
            this.video.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.video.Location = new System.Drawing.Point(652, 68);
            this.video.Name = "video";
            this.video.Size = new System.Drawing.Size(450, 326);
            this.video.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.video.TabIndex = 13;
            this.video.TabStop = false;
            // 
            // btnForceSnap
            // 
            this.btnForceSnap.Location = new System.Drawing.Point(500, 26);
            this.btnForceSnap.Name = "btnForceSnap";
            this.btnForceSnap.Size = new System.Drawing.Size(145, 36);
            this.btnForceSnap.TabIndex = 15;
            this.btnForceSnap.Text = "手动识别";
            this.btnForceSnap.UseVisualStyleBackColor = true;
            this.btnForceSnap.Click += new System.EventHandler(this.btnForceSnap_Click);
            // 
            // chkVideo
            // 
            this.chkVideo.AutoSize = true;
            this.chkVideo.Location = new System.Drawing.Point(12, 37);
            this.chkVideo.Name = "chkVideo";
            this.chkVideo.Size = new System.Drawing.Size(72, 16);
            this.chkVideo.TabIndex = 16;
            this.chkVideo.Text = "输出视频";
            this.chkVideo.UseVisualStyleBackColor = true;
            this.chkVideo.CheckedChanged += new System.EventHandler(this.chkVideo_CheckedChanged);
            // 
            // FrmCarPlateOfXinLuTong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 399);
            this.Controls.Add(this.chkVideo);
            this.Controls.Add(this.btnForceSnap);
            this.Controls.Add(this.picPlate);
            this.Controls.Add(this.video);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmCarPlateOfXinLuTong";
            this.Text = "车牌识别";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVideoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDate;
        private System.Windows.Forms.Button btnForceSnap;
        private System.Windows.Forms.CheckBox chkVideo;
    }
}