namespace PreferentialSystem
{
    partial class FrmPREWorkstations
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
            this.WorkstationView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colWorkstationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWorkstationDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.WorkstationView)).BeginInit();
            this.SuspendLayout();
            // 
            // WorkstationView
            // 
            this.WorkstationView.AllowUserToAddRows = false;
            this.WorkstationView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkstationView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WorkstationView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colWorkstationName,
            this.colWorkstationDesc});
            this.WorkstationView.Location = new System.Drawing.Point(0, 2);
            this.WorkstationView.Name = "WorkstationView";
            this.WorkstationView.RowTemplate.Height = 23;
            this.WorkstationView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.WorkstationView.Size = new System.Drawing.Size(547, 254);
            this.WorkstationView.TabIndex = 18;
            // 
            // colWorkstationName
            // 
            this.colWorkstationName.HeaderText = "工作站名称";
            this.colWorkstationName.Name = "colWorkstationName";
            this.colWorkstationName.ReadOnly = true;
            // 
            // colWorkstationDesc
            // 
            this.colWorkstationDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colWorkstationDesc.HeaderText = "工作站描述";
            this.colWorkstationDesc.Name = "colWorkstationDesc";
            this.colWorkstationDesc.ReadOnly = true;
            // 
            // FrmPREWorkstations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 281);
            this.Controls.Add(this.WorkstationView);
            this.Name = "FrmPREWorkstations";
            this.Text = "工作站设置";
            this.Controls.SetChildIndex(this.WorkstationView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.WorkstationView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView WorkstationView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkstationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkstationDesc;
    }
}