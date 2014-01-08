namespace Ralid.Park.UI
{
    partial class FrmCarplateTestForFile
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarplateTestForFile));
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBackColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCarplate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPercent = new System.Windows.Forms.Label();
            this.txtRegCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.resultGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultGrid_CellClick);
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
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Delete});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            // 
            // mnu_Delete
            // 
            resources.ApplyResources(this.mnu_Delete, "mnu_Delete");
            this.mnu_Delete.Name = "mnu_Delete";
            this.mnu_Delete.Click += new System.EventHandler(this.mnu_Delete_Click);
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
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtCarplate);
            this.panel2.Controls.Add(this.btnBrowser);
            this.panel2.Controls.Add(this.txtDir);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.txtPercent);
            this.panel2.Controls.Add(this.txtRegCount);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.resultGrid);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Name = "panel2";
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
            // btnBrowser
            // 
            resources.ApplyResources(this.btnBrowser, "btnBrowser");
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtDir
            // 
            resources.ApplyResources(this.txtDir, "txtDir");
            this.txtDir.Name = "txtDir";
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.picImage);
            this.panel1.Name = "panel1";
            // 
            // picImage
            // 
            resources.ApplyResources(this.picImage, "picImage");
            this.picImage.Name = "picImage";
            this.picImage.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SaveAs});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // mnu_SaveAs
            // 
            resources.ApplyResources(this.mnu_SaveAs, "mnu_SaveAs");
            this.mnu_SaveAs.Name = "mnu_SaveAs";
            this.mnu_SaveAs.Click += new System.EventHandler(this.mnu_SaveAs_Click);
            // 
            // splitter2
            // 
            resources.ApplyResources(this.splitter2, "splitter2");
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.splitter2.Name = "splitter2";
            this.splitter2.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // FrmCarplateTestForFile
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmCarplateTestForFile";
            this.Load += new System.EventHandler(this.FrmCarplateTestForFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label txtPercent;
        private System.Windows.Forms.Label txtRegCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_SaveAs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem mnu_Delete;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label2;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarplate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBackColor;
    }
}

