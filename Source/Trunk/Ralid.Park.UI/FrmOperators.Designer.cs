using Ralid.Park.UserControls;

namespace Ralid.Park.UI
{
    partial class FrmOperators
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOperators));
            this.OperatorView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.OperatorView)).BeginInit();
            this.SuspendLayout();
            // 
            // OperatorView
            // 
            this.OperatorView.AllowUserToAddRows = false;
            this.OperatorView.AllowUserToOrderColumns = true;
            this.OperatorView.AllowUserToResizeRows = false;
            this.OperatorView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OperatorView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOperatorID,
            this.colOperatorName,
            this.colRoleID,
            this.colOperatorNum,
            this.Column1});
            resources.ApplyResources(this.OperatorView, "OperatorView");
            this.OperatorView.Name = "OperatorView";
            this.OperatorView.RowHeadersVisible = false;
            this.OperatorView.RowTemplate.Height = 23;
            this.OperatorView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colOperatorID
            // 
            this.colOperatorID.DataPropertyName = "OperatorID";
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            // 
            // colOperatorName
            // 
            this.colOperatorName.DataPropertyName = "OperatorName";
            resources.ApplyResources(this.colOperatorName, "colOperatorName");
            this.colOperatorName.Name = "colOperatorName";
            this.colOperatorName.ReadOnly = true;
            // 
            // colRoleID
            // 
            this.colRoleID.DataPropertyName = "RoleID";
            resources.ApplyResources(this.colRoleID, "colRoleID");
            this.colRoleID.Name = "colRoleID";
            this.colRoleID.ReadOnly = true;
            // 
            // colOperatorNum
            // 
            this.colOperatorNum.DataPropertyName = "OperatorNum";
            resources.ApplyResources(this.colOperatorNum, "colOperatorNum");
            this.colOperatorNum.Name = "colOperatorNum";
            this.colOperatorNum.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmOperators
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OperatorView);
            this.Name = "FrmOperators";
            this.Controls.SetChildIndex(this.OperatorView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.OperatorView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView OperatorView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;


    }
}