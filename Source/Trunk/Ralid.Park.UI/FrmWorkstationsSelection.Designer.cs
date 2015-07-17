namespace Ralid.Park.UI
{
    partial class FrmWorkstationsSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWorkstationsSelection));
            this.StationView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWorkstationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCenterCharge = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAPM = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkCenterOnly = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkAPMOnly = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.StationView)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.colCheck,
            this.colWorkstationID,
            this.colCenterCharge,
            this.colAPM});
            this.StationView.Name = "StationView";
            this.StationView.RowHeadersVisible = false;
            this.StationView.RowTemplate.Height = 23;
            this.StationView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colCheck
            // 
            resources.ApplyResources(this.colCheck, "colCheck");
            this.colCheck.Name = "colCheck";
            // 
            // colWorkstationID
            // 
            this.colWorkstationID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
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
            // colAPM
            // 
            resources.ApplyResources(this.colAPM, "colAPM");
            this.colAPM.Name = "colAPM";
            this.colAPM.ReadOnly = true;
            // 
            // chkCenterOnly
            // 
            resources.ApplyResources(this.chkCenterOnly, "chkCenterOnly");
            this.chkCenterOnly.Name = "chkCenterOnly";
            this.chkCenterOnly.UseVisualStyleBackColor = true;
            this.chkCenterOnly.CheckedChanged += new System.EventHandler(this.chkCenterOnly_CheckedChanged);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkAPMOnly
            // 
            resources.ApplyResources(this.chkAPMOnly, "chkAPMOnly");
            this.chkAPMOnly.Name = "chkAPMOnly";
            this.chkAPMOnly.UseVisualStyleBackColor = true;
            this.chkAPMOnly.CheckedChanged += new System.EventHandler(this.chkAPMOnly_CheckedChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.chkCenterOnly);
            this.groupBox1.Controls.Add(this.chkAPMOnly);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnAll
            // 
            resources.ApplyResources(this.btnAll, "btnAll");
            this.btnAll.Name = "btnAll";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // FrmWorkstationsSelection
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.StationView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWorkstationsSelection";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FrmWorkstationsSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StationView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.CustomDataGridView StationView;
        private System.Windows.Forms.CheckBox chkCenterOnly;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkAPMOnly;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkstationID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCenterCharge;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAPM;
        private System.Windows.Forms.Button btnAll;
    }
}