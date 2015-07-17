namespace PreferentialSystem
{
    partial class FrmPREBusinesses
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBusinessesName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClosePanel = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.BusinessesView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colBusinessesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBusinessesDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelLeft.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BusinessesView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.btnSearch);
            this.panelLeft.Controls.Add(this.txtBusinessesName);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(138, 271);
            this.panelLeft.TabIndex = 20;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(8, 70);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(122, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查 询(&Q)";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtBusinessesName
            // 
            this.txtBusinessesName.Location = new System.Drawing.Point(8, 43);
            this.txtBusinessesName.Name = "txtBusinessesName";
            this.txtBusinessesName.Size = new System.Drawing.Size(124, 21);
            this.txtBusinessesName.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "商家名称:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.Controls.Add(this.btnClosePanel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(138, 25);
            this.panel3.TabIndex = 7;
            // 
            // btnClosePanel
            // 
            this.btnClosePanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClosePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClosePanel.FlatAppearance.BorderSize = 0;
            this.btnClosePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClosePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClosePanel.Location = new System.Drawing.Point(113, 0);
            this.btnClosePanel.Name = "btnClosePanel";
            this.btnClosePanel.Size = new System.Drawing.Size(25, 25);
            this.btnClosePanel.TabIndex = 7;
            this.btnClosePanel.UseVisualStyleBackColor = false;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.splitter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitter1.Location = new System.Drawing.Point(138, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(6, 271);
            this.splitter1.TabIndex = 23;
            this.splitter1.TabStop = false;
            // 
            // BusinessesView
            // 
            this.BusinessesView.AllowUserToAddRows = false;
            this.BusinessesView.AllowUserToOrderColumns = true;
            this.BusinessesView.AllowUserToResizeRows = false;
            this.BusinessesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BusinessesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBusinessesName,
            this.colBusinessesDesc});
            this.BusinessesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BusinessesView.Location = new System.Drawing.Point(144, 0);
            this.BusinessesView.Name = "BusinessesView";
            this.BusinessesView.RowHeadersVisible = false;
            this.BusinessesView.RowHeadersWidth = 20;
            this.BusinessesView.RowTemplate.Height = 23;
            this.BusinessesView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BusinessesView.Size = new System.Drawing.Size(428, 271);
            this.BusinessesView.TabIndex = 24;
            // 
            // colBusinessesName
            // 
            this.colBusinessesName.DataPropertyName = "BusinessesName";
            this.colBusinessesName.HeaderText = "商家名称";
            this.colBusinessesName.Name = "colBusinessesName";
            this.colBusinessesName.ReadOnly = true;
            this.colBusinessesName.Width = 150;
            // 
            // colBusinessesDesc
            // 
            this.colBusinessesDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBusinessesDesc.DataPropertyName = "BusinessesDesc";
            this.colBusinessesDesc.HeaderText = "描述";
            this.colBusinessesDesc.Name = "colBusinessesDesc";
            this.colBusinessesDesc.ReadOnly = true;
            // 
            // FrmPREBusinesses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 293);
            this.Controls.Add(this.BusinessesView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelLeft);
            this.Name = "FrmPREBusinesses";
            this.Text = "商家信息";
            this.Controls.SetChildIndex(this.panelLeft, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.Controls.SetChildIndex(this.BusinessesView, 0);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BusinessesView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtBusinessesName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClosePanel;
        private System.Windows.Forms.Splitter splitter1;
        private Ralid.Park.UserControls.CustomDataGridView BusinessesView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBusinessesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBusinessesDesc;
    }
}