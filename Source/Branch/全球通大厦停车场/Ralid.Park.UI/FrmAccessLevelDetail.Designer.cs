namespace Ralid.Park.UI
{
    partial class FrmAccessLevelDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccessLevelDetail));
            this.txtName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gridView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colBeginTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccessGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToResizeRows = false;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBeginTime,
            this.colEndTime,
            this.colAccessGroup,
            this.colFill});
            resources.ApplyResources(this.gridView, "gridView");
            this.gridView.Name = "gridView";
            this.gridView.RowHeadersVisible = false;
            this.gridView.RowTemplate.Height = 23;
            this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellDoubleClick);
            // 
            // colBeginTime
            // 
            resources.ApplyResources(this.colBeginTime, "colBeginTime");
            this.colBeginTime.Name = "colBeginTime";
            this.colBeginTime.ReadOnly = true;
            // 
            // colEndTime
            // 
            resources.ApplyResources(this.colEndTime, "colEndTime");
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.ReadOnly = true;
            // 
            // colAccessGroup
            // 
            this.colAccessGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colAccessGroup, "colAccessGroup");
            this.colAccessGroup.Name = "colAccessGroup";
            this.colAccessGroup.ReadOnly = true;
            // 
            // colFill
            // 
            this.colFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colFill, "colFill");
            this.colFill.Name = "colFill";
            this.colFill.ReadOnly = true;
            // 
            // FrmAccessLevelDetail
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAccessLevelDetail";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmAccessLevelDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtName;
        private System.Windows.Forms.Label label2;
        private Ralid.Park.UserControls.CustomDataGridView gridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeginTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccessGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFill;
    }
}