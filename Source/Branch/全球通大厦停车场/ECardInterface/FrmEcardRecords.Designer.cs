namespace ECardInterface
{
    partial class FrmEcardRecords
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.OperatorView = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colSheetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarplate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnterDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExitDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimitation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.colSheetID,
            this.colCardID,
            this.colCarplate,
            this.colEnterDt,
            this.colExitDt,
            this.colLimitation,
            this.colMemo});
            this.OperatorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperatorView.Location = new System.Drawing.Point(0, 0);
            this.OperatorView.Name = "OperatorView";
            this.OperatorView.RowHeadersVisible = false;
            this.OperatorView.RowHeadersWidth = 20;
            this.OperatorView.RowTemplate.Height = 23;
            this.OperatorView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OperatorView.Size = new System.Drawing.Size(719, 259);
            this.OperatorView.TabIndex = 18;
            // 
            // colSheetID
            // 
            this.colSheetID.HeaderText = "车单号";
            this.colSheetID.Name = "colSheetID";
            this.colSheetID.ReadOnly = true;
            // 
            // colCardID
            // 
            this.colCardID.HeaderText = "员工编号";
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colCarplate
            // 
            this.colCarplate.HeaderText = "车牌";
            this.colCarplate.Name = "colCarplate";
            this.colCarplate.ReadOnly = true;
            // 
            // colEnterDt
            // 
            dataGridViewCellStyle4.Format = "yyyy-MM-dd HH:mm:ss";
            this.colEnterDt.DefaultCellStyle = dataGridViewCellStyle4;
            this.colEnterDt.HeaderText = "入场时间";
            this.colEnterDt.Name = "colEnterDt";
            this.colEnterDt.ReadOnly = true;
            this.colEnterDt.Width = 130;
            // 
            // colExitDt
            // 
            dataGridViewCellStyle5.Format = "yyyy-MM-dd HH:mm:ss";
            this.colExitDt.DefaultCellStyle = dataGridViewCellStyle5;
            this.colExitDt.HeaderText = "出场时间";
            this.colExitDt.Name = "colExitDt";
            this.colExitDt.ReadOnly = true;
            this.colExitDt.Width = 130;
            // 
            // colLimitation
            // 
            dataGridViewCellStyle6.Format = "N1";
            dataGridViewCellStyle6.NullValue = null;
            this.colLimitation.DefaultCellStyle = dataGridViewCellStyle6;
            this.colLimitation.HeaderText = "剩余时长";
            this.colLimitation.Name = "colLimitation";
            this.colLimitation.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMemo.HeaderText = "";
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            this.colMemo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmEcardRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 281);
            this.Controls.Add(this.OperatorView);
            this.MinimizeBox = false;
            this.Name = "FrmEcardRecords";
            this.Text = "未上传记录";
            this.Controls.SetChildIndex(this.OperatorView, 0);
            ((System.ComponentModel.ISupportInitialize)(this.OperatorView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.CustomDataGridView OperatorView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSheetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarplate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnterDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExitDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimitation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
    }
}