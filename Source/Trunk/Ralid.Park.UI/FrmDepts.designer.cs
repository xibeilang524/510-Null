namespace Ralid.Park.UI
{
	partial class FrmDepts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDepts));
            this.DeptView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DeptView)).BeginInit();
            this.SuspendLayout();
            // 
            // DeptView
            // 
            resources.ApplyResources(this.DeptView, "DeptView");
            this.DeptView.AllowUserToAddRows = false;
            this.DeptView.AllowUserToDeleteRows = false;
            this.DeptView.AllowUserToResizeRows = false;
            this.DeptView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeptView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDescr});
            this.DeptView.Name = "DeptView";
            this.DeptView.RowHeadersVisible = false;
            this.DeptView.RowTemplate.Height = 23;
            this.DeptView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "DeptName";
            resources.ApplyResources(this.colName, "colName");
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDescr
            // 
            this.colDescr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescr.DataPropertyName = "Descrption";
            resources.ApplyResources(this.colDescr, "colDescr");
            this.colDescr.Name = "colDescr";
            this.colDescr.ReadOnly = true;
            // 
            // FrmDepts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeptView);
            this.Name = "FrmDepts";
            this.Controls.SetChildIndex(this.DeptView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DeptView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private UserControls.CustomDataGridView DeptView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescr;
	}
}