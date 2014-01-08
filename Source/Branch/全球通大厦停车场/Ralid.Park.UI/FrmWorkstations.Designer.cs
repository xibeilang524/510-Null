namespace Ralid.Park.UI
{
    partial class FrmWorkstations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWorkstations));
            this.StationView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colWorkstationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCenterCharge = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.StationView)).BeginInit();
            this.SuspendLayout();
            // 
            // StationView
            // 
            resources.ApplyResources(this.StationView, "StationView");
            this.StationView.AllowUserToAddRows = false;
            this.StationView.AllowUserToDeleteRows = false;
            this.StationView.AllowUserToResizeRows = false;
            this.StationView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StationView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colWorkstationID,
            this.colCenterCharge,
            this.Column1});
            this.StationView.Name = "StationView";
            this.StationView.RowHeadersVisible = false;
            this.StationView.RowTemplate.Height = 23;
            this.StationView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colWorkstationID
            // 
            resources.ApplyResources(this.colWorkstationID, "colWorkstationID");
            this.colWorkstationID.Name = "colWorkstationID";
            this.colWorkstationID.ReadOnly = true;
            // 
            // colCenterCharge
            // 
            resources.ApplyResources(this.colCenterCharge, "colCenterCharge");
            this.colCenterCharge.Name = "colCenterCharge";
            this.colCenterCharge.ReadOnly = true;
            this.colCenterCharge.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCenterCharge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmWorkstations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StationView);
            this.Name = "FrmWorkstations";
            this.Controls.SetChildIndex(this.StationView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.StationView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView StationView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkstationID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCenterCharge;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}