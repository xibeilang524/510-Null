namespace Ralid.Park.UI
{
    partial class FrmMasterBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMasterBase));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Fresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Fresh,
            this.mnu_Add,
            this.mnu_Delete,
            this.mnu_Property});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // mnu_Fresh
            // 
            resources.ApplyResources(this.mnu_Fresh, "mnu_Fresh");
            this.mnu_Fresh.Name = "mnu_Fresh";
            this.mnu_Fresh.Click += new System.EventHandler(this.mnu_Fresh_Click);
            // 
            // mnu_Add
            // 
            resources.ApplyResources(this.mnu_Add, "mnu_Add");
            this.mnu_Add.Name = "mnu_Add";
            this.mnu_Add.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // mnu_Delete
            // 
            resources.ApplyResources(this.mnu_Delete, "mnu_Delete");
            this.mnu_Delete.Name = "mnu_Delete";
            this.mnu_Delete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // mnu_Property
            // 
            resources.ApplyResources(this.mnu_Property, "mnu_Property");
            this.mnu_Property.Name = "mnu_Property";
            this.mnu_Property.Click += new System.EventHandler(this.mnu_Property_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Spring = true;
            // 
            // FrmMasterBase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmMasterBase";
            this.Load += new System.EventHandler(this.FrmMasterBase_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem mnu_Add;
        private System.Windows.Forms.ToolStripMenuItem mnu_Property;
        private System.Windows.Forms.ToolStripMenuItem mnu_Delete;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        protected System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Fresh;

    }
}