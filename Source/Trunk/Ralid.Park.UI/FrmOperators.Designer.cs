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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.chkRole = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comRole = new Ralid.Park.UserControls.RoleComboBox(this.components);
            this.txtOperaterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOperaterID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClosePanel = new System.Windows.Forms.Button();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.OperatorView)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.colDeptName,
            this.colOperatorNum,
            this.Column1});
            resources.ApplyResources(this.OperatorView, "OperatorView");
            this.OperatorView.Name = "OperatorView";
            this.OperatorView.RowHeadersVisible = false;
            this.OperatorView.RowTemplate.Height = 23;
            this.OperatorView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.chkRole);
            this.panelLeft.Controls.Add(this.btnSearch);
            this.panelLeft.Controls.Add(this.comRole);
            this.panelLeft.Controls.Add(this.txtOperaterName);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.txtOperaterID);
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.panel3);
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Name = "panelLeft";
            // 
            // chkRole
            // 
            resources.ApplyResources(this.chkRole, "chkRole");
            this.chkRole.Name = "chkRole";
            this.chkRole.UseVisualStyleBackColor = true;
            this.chkRole.CheckedChanged += new System.EventHandler(this.chkRole_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comRole
            // 
            this.comRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comRole.FormattingEnabled = true;
            resources.ApplyResources(this.comRole, "comRole");
            this.comRole.Name = "comRole";
            // 
            // txtOperaterName
            // 
            resources.ApplyResources(this.txtOperaterName, "txtOperaterName");
            this.txtOperaterName.Name = "txtOperaterName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtOperaterID
            // 
            resources.ApplyResources(this.txtOperaterID, "txtOperaterID");
            this.txtOperaterID.Name = "txtOperaterID";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.Controls.Add(this.btnClosePanel);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // btnClosePanel
            // 
            this.btnClosePanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnClosePanel.BackgroundImage = global::Ralid.Park.UI.Properties.Resources.button_grey_close;
            resources.ApplyResources(this.btnClosePanel, "btnClosePanel");
            this.btnClosePanel.FlatAppearance.BorderSize = 0;
            this.btnClosePanel.Name = "btnClosePanel";
            this.btnClosePanel.UseVisualStyleBackColor = false;
            this.btnClosePanel.Click += new System.EventHandler(this.btnClosePanel_Click);
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
            // colDeptName
            // 
            resources.ApplyResources(this.colDeptName, "colDeptName");
            this.colDeptName.Name = "colDeptName";
            this.colDeptName.ReadOnly = true;
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.OperatorView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelLeft);
            this.Name = "FrmOperators";
            this.Controls.SetChildIndex(this.panelLeft, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.Controls.SetChildIndex(this.OperatorView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.OperatorView)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomDataGridView OperatorView;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.CheckBox chkRole;
        private System.Windows.Forms.Button btnSearch;
        private RoleComboBox comRole;
        private System.Windows.Forms.TextBox txtOperaterName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOperaterID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClosePanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;



    }
}