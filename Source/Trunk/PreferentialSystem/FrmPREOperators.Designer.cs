namespace PreferentialSystem
{
    partial class FrmPREOperators
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
            this.comRole = new Ralid.Park.UserControls.PRERoleComboBox(this.components);
            this.chkRole = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtOperaterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOperaterID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClosePanel = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.OperatorView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colBlank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelLeft.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OperatorView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.comRole);
            this.panelLeft.Controls.Add(this.chkRole);
            this.panelLeft.Controls.Add(this.btnSearch);
            this.panelLeft.Controls.Add(this.txtOperaterName);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.txtOperaterID);
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(138, 271);
            this.panelLeft.TabIndex = 19;
            // 
            // comRole
            // 
            this.comRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comRole.FormattingEnabled = true;
            this.comRole.Location = new System.Drawing.Point(10, 141);
            this.comRole.Name = "comRole";
            this.comRole.Size = new System.Drawing.Size(121, 20);
            this.comRole.TabIndex = 23;
            // 
            // chkRole
            // 
            this.chkRole.AutoSize = true;
            this.chkRole.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRole.Location = new System.Drawing.Point(10, 119);
            this.chkRole.Name = "chkRole";
            this.chkRole.Size = new System.Drawing.Size(54, 16);
            this.chkRole.TabIndex = 15;
            this.chkRole.Text = "角色:";
            this.chkRole.UseVisualStyleBackColor = true;
            this.chkRole.CheckedChanged += new System.EventHandler(this.chkRole_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(7, 176);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(122, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查 询(&Q)";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtOperaterName
            // 
            this.txtOperaterName.Location = new System.Drawing.Point(8, 92);
            this.txtOperaterName.Name = "txtOperaterName";
            this.txtOperaterName.Size = new System.Drawing.Size(124, 21);
            this.txtOperaterName.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(8, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "操作员名:";
            // 
            // txtOperaterID
            // 
            this.txtOperaterID.Location = new System.Drawing.Point(8, 53);
            this.txtOperaterID.MaxLength = 50;
            this.txtOperaterID.Name = "txtOperaterID";
            this.txtOperaterID.Size = new System.Drawing.Size(124, 21);
            this.txtOperaterID.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(8, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "登录ID:";
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
            this.splitter1.TabIndex = 22;
            this.splitter1.TabStop = false;
            // 
            // OperatorView
            // 
            this.OperatorView.AllowUserToAddRows = false;
            this.OperatorView.AllowUserToOrderColumns = true;
            this.OperatorView.AllowUserToResizeRows = false;
            this.OperatorView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OperatorView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBlank,
            this.colOperatorID,
            this.colOperatorName,
            this.colRoleID,
            this.colOperatorNum,
            this.Column1});
            this.OperatorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperatorView.Location = new System.Drawing.Point(138, 0);
            this.OperatorView.Name = "OperatorView";
            this.OperatorView.RowHeadersVisible = false;
            this.OperatorView.RowHeadersWidth = 20;
            this.OperatorView.RowTemplate.Height = 23;
            this.OperatorView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OperatorView.Size = new System.Drawing.Size(434, 271);
            this.OperatorView.TabIndex = 21;
            // 
            // colBlank
            // 
            this.colBlank.HeaderText = "";
            this.colBlank.MinimumWidth = 50;
            this.colBlank.Name = "colBlank";
            this.colBlank.ReadOnly = true;
            this.colBlank.Width = 50;
            // 
            // colOperatorID
            // 
            this.colOperatorID.DataPropertyName = "OperatorID";
            this.colOperatorID.HeaderText = "登录ID";
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            // 
            // colOperatorName
            // 
            this.colOperatorName.DataPropertyName = "OperatorName";
            this.colOperatorName.HeaderText = "操作员名";
            this.colOperatorName.Name = "colOperatorName";
            this.colOperatorName.ReadOnly = true;
            // 
            // colRoleID
            // 
            this.colRoleID.DataPropertyName = "RoleID";
            this.colRoleID.HeaderText = "角色";
            this.colRoleID.Name = "colRoleID";
            this.colRoleID.ReadOnly = true;
            // 
            // colOperatorNum
            // 
            this.colOperatorNum.DataPropertyName = "OperatorNum";
            this.colOperatorNum.HeaderText = "操作员编号";
            this.colOperatorNum.Name = "colOperatorNum";
            this.colOperatorNum.ReadOnly = true;
            this.colOperatorNum.Visible = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmPREOperators
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 293);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.OperatorView);
            this.Controls.Add(this.panelLeft);
            this.Name = "FrmPREOperators";
            this.Text = "操作员管理";
            this.Controls.SetChildIndex(this.panelLeft, 0);
            this.Controls.SetChildIndex(this.OperatorView, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OperatorView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.CheckBox chkRole;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtOperaterName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOperaterID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClosePanel;
        private Ralid.Park.UserControls.CustomDataGridView OperatorView;
        private System.Windows.Forms.Splitter splitter1;
        private Ralid.Park.UserControls.PRERoleComboBox comRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBlank;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}