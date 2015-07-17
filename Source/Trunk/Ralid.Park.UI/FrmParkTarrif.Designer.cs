namespace Ralid.Park.UI
{
    partial class FrmParkTarrif
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
            this.tariffGrid = new System.Windows.Forms.DataGridView();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGeneral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoliday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInnerRoom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHolidayAndInnerRoom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TariffMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_ClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.label30 = new System.Windows.Forms.Label();
            this.btnDownLoad = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblHadSet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tariffGrid)).BeginInit();
            this.TariffMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tariffGrid
            // 
            this.tariffGrid.AllowUserToAddRows = false;
            this.tariffGrid.AllowUserToDeleteRows = false;
            this.tariffGrid.AllowUserToResizeColumns = false;
            this.tariffGrid.AllowUserToResizeRows = false;
            this.tariffGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tariffGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tariffGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardType,
            this.colCarType,
            this.colGeneral,
            this.colHoliday,
            this.colInnerRoom,
            this.colHolidayAndInnerRoom});
            this.tariffGrid.ContextMenuStrip = this.TariffMenu;
            this.tariffGrid.Location = new System.Drawing.Point(3, 3);
            this.tariffGrid.Name = "tariffGrid";
            this.tariffGrid.ReadOnly = true;
            this.tariffGrid.RowHeadersVisible = false;
            this.tariffGrid.RowTemplate.Height = 23;
            this.tariffGrid.Size = new System.Drawing.Size(604, 390);
            this.tariffGrid.TabIndex = 0;
            this.tariffGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tariffGrid_CellDoubleClick);
            // 
            // colCardType
            // 
            this.colCardType.HeaderText = "卡片类型";
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            this.colCardType.Width = 80;
            // 
            // colCarType
            // 
            this.colCarType.HeaderText = "车型";
            this.colCarType.Name = "colCarType";
            this.colCarType.ReadOnly = true;
            this.colCarType.Width = 80;
            // 
            // colGeneral
            // 
            this.colGeneral.HeaderText = "普通";
            this.colGeneral.Name = "colGeneral";
            this.colGeneral.ReadOnly = true;
            // 
            // colHoliday
            // 
            this.colHoliday.HeaderText = "节假日";
            this.colHoliday.Name = "colHoliday";
            this.colHoliday.ReadOnly = true;
            // 
            // colInnerRoom
            // 
            this.colInnerRoom.HeaderText = "室内";
            this.colInnerRoom.Name = "colInnerRoom";
            this.colInnerRoom.ReadOnly = true;
            // 
            // colHolidayAndInnerRoom
            // 
            this.colHolidayAndInnerRoom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHolidayAndInnerRoom.HeaderText = "室内节假日";
            this.colHolidayAndInnerRoom.Name = "colHolidayAndInnerRoom";
            this.colHolidayAndInnerRoom.ReadOnly = true;
            // 
            // TariffMenu
            // 
            this.TariffMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Clear,
            this.toolStripSeparator1,
            this.mnu_ClearAll});
            this.TariffMenu.Name = "TariffMenu";
            this.TariffMenu.Size = new System.Drawing.Size(153, 76);
            // 
            // mnu_Clear
            // 
            this.mnu_Clear.Name = "mnu_Clear";
            this.mnu_Clear.Size = new System.Drawing.Size(152, 22);
            this.mnu_Clear.Text = "清除";
            this.mnu_Clear.Click += new System.EventHandler(this.mnu_Clear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnu_ClearAll
            // 
            this.mnu_ClearAll.Name = "mnu_ClearAll";
            this.mnu_ClearAll.Size = new System.Drawing.Size(152, 22);
            this.mnu_ClearAll.Text = "清空费率";
            this.mnu_ClearAll.Click += new System.EventHandler(this.mnu_ClearAll_Click);
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("宋体", 9F);
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label30.Location = new System.Drawing.Point(1, 404);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(293, 12);
            this.label30.TabIndex = 6;
            this.label30.Text = "注意：脱机模式时，部分设置需要手动下发到控制器。";
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownLoad.Location = new System.Drawing.Point(370, 419);
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(75, 23);
            this.btnDownLoad.TabIndex = 7;
            this.btnDownLoad.Text = "下发[&D]";
            this.btnDownLoad.UseVisualStyleBackColor = true;
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(451, 419);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定[&O]";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(532, 419);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消[&C]";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblHadSet
            // 
            this.lblHadSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHadSet.AutoSize = true;
            this.lblHadSet.ForeColor = System.Drawing.Color.Blue;
            this.lblHadSet.Location = new System.Drawing.Point(36, 430);
            this.lblHadSet.Name = "lblHadSet";
            this.lblHadSet.Size = new System.Drawing.Size(59, 12);
            this.lblHadSet.TabIndex = 10;
            this.lblHadSet.Text = "lblHadSet";
            this.lblHadSet.Visible = false;
            // 
            // FrmParkTarrif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 454);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblHadSet);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDownLoad);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.tariffGrid);
            this.Name = "FrmParkTarrif";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "停车场单独费率设置";
            this.Load += new System.EventHandler(this.FrmParkTarrif_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tariffGrid)).EndInit();
            this.TariffMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView tariffGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGeneral;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoliday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInnerRoom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHolidayAndInnerRoom;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnDownLoad;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblHadSet;
        private System.Windows.Forms.ContextMenuStrip TariffMenu;
        private System.Windows.Forms.ToolStripMenuItem mnu_Clear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnu_ClearAll;
    }
}