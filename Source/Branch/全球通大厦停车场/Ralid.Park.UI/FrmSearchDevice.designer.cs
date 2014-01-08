namespace Ralid.Park.UI
{
    partial class FrmSearchDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchDevice));
            this.btnSearch = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colSerialNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_AddTo = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // grid
            // 
            resources.ApplyResources(this.grid, "grid");
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSerialNum,
            this.colIP,
            this.colMac});
            this.grid.ContextMenuStrip = this.contextMenuStrip1;
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 23;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_CellMouseDown);
            // 
            // colSerialNum
            // 
            resources.ApplyResources(this.colSerialNum, "colSerialNum");
            this.colSerialNum.Name = "colSerialNum";
            this.colSerialNum.ReadOnly = true;
            // 
            // colIP
            // 
            resources.ApplyResources(this.colIP, "colIP");
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            // 
            // colMac
            // 
            this.colMac.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colMac, "colMac");
            this.colMac.Name = "colMac";
            this.colMac.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Update,
            this.mnu_AddTo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // mnu_Update
            // 
            resources.ApplyResources(this.mnu_Update, "mnu_Update");
            this.mnu_Update.Name = "mnu_Update";
            this.mnu_Update.Click += new System.EventHandler(this.mnu_Update_Click);
            // 
            // mnu_AddTo
            // 
            resources.ApplyResources(this.mnu_AddTo, "mnu_AddTo");
            this.mnu_AddTo.Name = "mnu_AddTo";
            this.mnu_AddTo.Click += new System.EventHandler(this.mnu_AddTo_Click);
            // 
            // FrmSearchDevice
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid);
            this.Controls.Add(this.btnSearch);
            this.Name = "FrmSearchDevice";
            this.Load += new System.EventHandler(this.FrmSearchDevice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Update;
        private System.Windows.Forms.ToolStripMenuItem mnu_AddTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerialNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMac;
    }
}

