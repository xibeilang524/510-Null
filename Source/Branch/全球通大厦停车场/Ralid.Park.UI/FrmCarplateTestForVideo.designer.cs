namespace Ralid.Park.UI
{
    partial class FrmCarplateTestForVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarplateTestForVideo));
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtPercent = new System.Windows.Forms.Label();
            this.txtRegCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCarplate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.ucVideo = new Ralid.Park.UserControls.VideoPanels.ACTIVideoControl();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBackColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter2
            // 
            resources.ApplyResources(this.splitter2, "splitter2");
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.splitter2.Name = "splitter2";
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Controls.Add(this.txtPercent);
            this.panel2.Controls.Add(this.txtRegCount);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.resultGrid);
            this.panel2.Name = "panel2";
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtPercent
            // 
            resources.ApplyResources(this.txtPercent, "txtPercent");
            this.txtPercent.ForeColor = System.Drawing.Color.Blue;
            this.txtPercent.Name = "txtPercent";
            // 
            // txtRegCount
            // 
            resources.ApplyResources(this.txtRegCount, "txtRegCount");
            this.txtRegCount.ForeColor = System.Drawing.Color.Blue;
            this.txtRegCount.Name = "txtRegCount";
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
            // txtTotal
            // 
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.ForeColor = System.Drawing.Color.Blue;
            this.txtTotal.Name = "txtTotal";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // resultGrid
            // 
            resources.ApplyResources(this.resultGrid, "resultGrid");
            this.resultGrid.AllowUserToAddRows = false;
            this.resultGrid.AllowUserToDeleteRows = false;
            this.resultGrid.AllowUserToResizeRows = false;
            this.resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colCarPlate,
            this.colBackColor});
            this.resultGrid.Name = "resultGrid";
            this.resultGrid.RowHeadersVisible = false;
            this.resultGrid.RowTemplate.Height = 23;
            this.resultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCarplate);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.ucVideo);
            this.panel1.Name = "panel1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtCarplate
            // 
            resources.ApplyResources(this.txtCarplate, "txtCarplate");
            this.txtCarplate.Name = "txtCarplate";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // ucVideo
            // 
            resources.ApplyResources(this.ucVideo, "ucVideo");
            this.ucVideo.AllowDrop = true;
            this.ucVideo.Caption = "";
            this.ucVideo.Name = "ucVideo";
            this.ucVideo.ShowTitle = true;
            this.ucVideo.StretchToFit = true;
            this.ucVideo.VideoSource = null;
            // 
            // colFileName
            // 
            this.colFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colFileName, "colFileName");
            this.colFileName.Name = "colFileName";
            // 
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colBackColor
            // 
            resources.ApplyResources(this.colBackColor, "colBackColor");
            this.colBackColor.Name = "colBackColor";
            this.colBackColor.ReadOnly = true;
            // 
            // FrmCarplateTestForVideo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel2);
            this.Name = "FrmCarplateTestForVideo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCarplateTestForVideo_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label txtPercent;
        private System.Windows.Forms.Label txtRegCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarplate;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private UserControls.VideoPanels.ACTIVideoControl ucVideo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBackColor;
    }
}