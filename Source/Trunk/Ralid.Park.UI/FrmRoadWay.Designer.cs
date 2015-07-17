namespace Ralid.Park.UI
{
    partial class FrmRoadWay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRoadWay));
            this.RoadWayView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colRoadName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrances = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Entrance = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.RoadWayView)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoadWayView
            // 
            resources.ApplyResources(this.RoadWayView, "RoadWayView");
            this.RoadWayView.AllowUserToAddRows = false;
            this.RoadWayView.AllowUserToDeleteRows = false;
            this.RoadWayView.AllowUserToResizeRows = false;
            this.RoadWayView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RoadWayView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRoadName,
            this.colMode,
            this.colEntrances});
            this.RoadWayView.Name = "RoadWayView";
            this.RoadWayView.RowHeadersVisible = false;
            this.RoadWayView.RowTemplate.Height = 23;
            this.RoadWayView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colRoadName
            // 
            resources.ApplyResources(this.colRoadName, "colRoadName");
            this.colRoadName.Name = "colRoadName";
            this.colRoadName.ReadOnly = true;
            // 
            // colMode
            // 
            resources.ApplyResources(this.colMode, "colMode");
            this.colMode.Name = "colMode";
            this.colMode.ReadOnly = true;
            this.colMode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colEntrances
            // 
            this.colEntrances.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colEntrances, "colEntrances");
            this.colEntrances.Name = "colEntrances";
            this.colEntrances.ReadOnly = true;
            this.colEntrances.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Exit,
            this.mnu_Entrance});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            // 
            // mnu_Exit
            // 
            resources.ApplyResources(this.mnu_Exit, "mnu_Exit");
            this.mnu_Exit.Name = "mnu_Exit";
            this.mnu_Exit.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // mnu_Entrance
            // 
            resources.ApplyResources(this.mnu_Entrance, "mnu_Entrance");
            this.mnu_Entrance.Name = "mnu_Entrance";
            this.mnu_Entrance.Click += new System.EventHandler(this.mnu_Entrance_Click);
            // 
            // FrmRoadWay
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.RoadWayView);
            this.Name = "FrmRoadWay";
            this.Controls.SetChildIndex(this.RoadWayView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.RoadWayView)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.CustomDataGridView RoadWayView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem mnu_Exit;
        private System.Windows.Forms.ToolStripMenuItem mnu_Entrance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoadName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrances;
    }
}
