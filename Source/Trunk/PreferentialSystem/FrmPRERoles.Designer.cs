namespace PreferentialSystem
{
    partial class FrmPRERoles
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
            this.RoleView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RoleView)).BeginInit();
            this.SuspendLayout();
            // 
            // RoleView
            // 
            this.RoleView.AllowUserToAddRows = false;
            this.RoleView.AllowUserToDeleteRows = false;
            this.RoleView.AllowUserToResizeRows = false;
            this.RoleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RoleView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDescr});
            this.RoleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoleView.Location = new System.Drawing.Point(0, 0);
            this.RoleView.Name = "RoleView";
            this.RoleView.RowHeadersVisible = false;
            this.RoleView.RowTemplate.Height = 23;
            this.RoleView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RoleView.Size = new System.Drawing.Size(547, 259);
            this.RoleView.TabIndex = 18;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "RoleID";
            this.colName.HeaderText = "角色";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDescr
            // 
            this.colDescr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescr.DataPropertyName = "RoleName";
            this.colDescr.HeaderText = "描述";
            this.colDescr.Name = "colDescr";
            this.colDescr.ReadOnly = true;
            // 
            // FrmPRERoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 281);
            this.Controls.Add(this.RoleView);
            this.Name = "FrmPRERoles";
            this.Text = "角色管理";
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