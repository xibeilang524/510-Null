namespace Ralid.Park.UI
{
    partial class FrmParkingCouponInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParkingCouponInput));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblCouponUnit = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.gridParkingCoupon = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colCouponName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCouponValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCouponCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridParkingCoupon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCouponUnit
            // 
            resources.ApplyResources(this.lblCouponUnit, "lblCouponUnit");
            this.lblCouponUnit.ForeColor = System.Drawing.Color.Blue;
            this.lblCouponUnit.Name = "lblCouponUnit";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.ForeColor = System.Drawing.Color.Blue;
            this.label34.Name = "label34";
            // 
            // gridParkingCoupon
            // 
            resources.ApplyResources(this.gridParkingCoupon, "gridParkingCoupon");
            this.gridParkingCoupon.AllowUserToAddRows = false;
            this.gridParkingCoupon.AllowUserToDeleteRows = false;
            this.gridParkingCoupon.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridParkingCoupon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridParkingCoupon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCouponName,
            this.colCouponValue,
            this.colCouponCount});
            this.gridParkingCoupon.Name = "gridParkingCoupon";
            this.gridParkingCoupon.RowTemplate.Height = 23;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // colCouponName
            // 
            this.colCouponName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colCouponName, "colCouponName");
            this.colCouponName.Name = "colCouponName";
            this.colCouponName.ReadOnly = true;
            // 
            // colCouponValue
            // 
            resources.ApplyResources(this.colCouponValue, "colCouponValue");
            this.colCouponValue.Name = "colCouponValue";
            this.colCouponValue.ReadOnly = true;
            // 
            // colCouponCount
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            this.colCouponCount.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.colCouponCount, "colCouponCount");
            this.colCouponCount.Name = "colCouponCount";
            // 
            // FrmParkingCouponInput
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblCouponUnit);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.gridParkingCoupon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmParkingCouponInput";
            this.Load += new System.EventHandler(this.FrmParkingCouponInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridParkingCoupon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCouponUnit;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.DataGridView gridParkingCoupon;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCouponName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCouponValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCouponCount;
    }
}