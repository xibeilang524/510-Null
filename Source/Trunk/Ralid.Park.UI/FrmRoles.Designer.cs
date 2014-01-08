namespace Ralid.Park.UI
{
    partial class FrmRoles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRoles));
            this.RoleView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RoleView)).BeginInit();
            this.SuspendLayout();
            // 
            // RoleView
            // 
            resources.ApplyResources(this.RoleView, "RoleView");
            this.RoleView.AllowUserToAddRows = false;
            this.RoleView.AllowUserToDeleteRows = false;
            this.RoleView.AllowUserToResizeRows = false;
            this.RoleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RoleView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDescr});
            this.RoleView.Name = "RoleView";
            this.RoleView.RowHeadersVisible = false;
            this.RoleView.RowTemplate.Height = 23;
            this.RoleView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "RoleID";
            resources.ApplyResources(this.colName, "colName");
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDescr
            // 
            this.colDescr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescr.DataPropertyName = "RoleName";
            resources.ApplyResources(this.colDescr, "colDescr");
            this.colDescr.Name = "colDescr";
            this.colDescr.ReadOnly = true;
            // 
            // FrmRoles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RoleView);
            this.Name = "FrmRoles";
            this.Controls.SetChildIndex(this.RoleView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.RoleView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView RoleView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescr;
    }
}