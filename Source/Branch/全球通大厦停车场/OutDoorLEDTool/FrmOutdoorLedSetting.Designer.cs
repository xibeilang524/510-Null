namespace OutDoorLEDTool
{
    partial class FrmOutdoorLedSetting
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutdoorLedSetting));
            this.parkCombobox1 = new Ralid.Park.UserControls.ParkCombobox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnArea6 = new System.Windows.Forms.Button();
            this.btnArea5 = new System.Windows.Forms.Button();
            this.btnArea4 = new System.Windows.Forms.Button();
            this.btnArea3 = new System.Windows.Forms.Button();
            this.btnArea2 = new System.Windows.Forms.Button();
            this.btnArea1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.entranceGrid = new Ralid.GeneralLibrary.WinformControl.CustomDataGridView(this.components);
            this.colEntranceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntranceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ledGrid = new Ralid.GeneralLibrary.WinformControl.CustomDataGridView(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAutoFreshInterval = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.notify1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.systemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnu_System = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Exit1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Language = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_SimpleChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_TraditionalChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_English = new System.Windows.Forms.ToolStripMenuItem();
            this.colComport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBaud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMotorAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBrightness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entranceGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.systemMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // parkCombobox1
            // 
            this.parkCombobox1.FormattingEnabled = true;
            resources.ApplyResources(this.parkCombobox1, "parkCombobox1");
            this.parkCombobox1.Name = "parkCombobox1";
            this.parkCombobox1.SelectedIndexChanged += new System.EventHandler(this.parkCombobox1_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnArea6);
            this.groupBox1.Controls.Add(this.btnArea5);
            this.groupBox1.Controls.Add(this.btnArea4);
            this.groupBox1.Controls.Add(this.btnArea3);
            this.groupBox1.Controls.Add(this.btnArea2);
            this.groupBox1.Controls.Add(this.btnArea1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnArea6
            // 
            this.btnArea6.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnArea6, "btnArea6");
            this.btnArea6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnArea6.Name = "btnArea6";
            this.btnArea6.UseVisualStyleBackColor = false;
            this.btnArea6.Click += new System.EventHandler(this.btnArea_Click);
            this.btnArea6.MouseEnter += new System.EventHandler(this.btnArea_Enter);
            this.btnArea6.MouseLeave += new System.EventHandler(this.btnArea_Leave);
            // 
            // btnArea5
            // 
            this.btnArea5.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnArea5, "btnArea5");
            this.btnArea5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnArea5.Name = "btnArea5";
            this.btnArea5.UseVisualStyleBackColor = false;
            this.btnArea5.Click += new System.EventHandler(this.btnArea_Click);
            this.btnArea5.MouseEnter += new System.EventHandler(this.btnArea_Enter);
            this.btnArea5.MouseLeave += new System.EventHandler(this.btnArea_Leave);
            // 
            // btnArea4
            // 
            this.btnArea4.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnArea4, "btnArea4");
            this.btnArea4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnArea4.Name = "btnArea4";
            this.btnArea4.UseVisualStyleBackColor = false;
            this.btnArea4.Click += new System.EventHandler(this.btnArea_Click);
            this.btnArea4.MouseEnter += new System.EventHandler(this.btnArea_Enter);
            this.btnArea4.MouseLeave += new System.EventHandler(this.btnArea_Leave);
            // 
            // btnArea3
            // 
            this.btnArea3.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnArea3, "btnArea3");
            this.btnArea3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnArea3.Name = "btnArea3";
            this.btnArea3.UseVisualStyleBackColor = false;
            this.btnArea3.Click += new System.EventHandler(this.btnArea_Click);
            this.btnArea3.MouseEnter += new System.EventHandler(this.btnArea_Enter);
            this.btnArea3.MouseLeave += new System.EventHandler(this.btnArea_Leave);
            // 
            // btnArea2
            // 
            this.btnArea2.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnArea2, "btnArea2");
            this.btnArea2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnArea2.Name = "btnArea2";
            this.btnArea2.UseVisualStyleBackColor = false;
            this.btnArea2.Click += new System.EventHandler(this.btnArea_Click);
            this.btnArea2.MouseEnter += new System.EventHandler(this.btnArea_Enter);
            this.btnArea2.MouseLeave += new System.EventHandler(this.btnArea_Leave);
            // 
            // btnArea1
            // 
            this.btnArea1.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btnArea1, "btnArea1");
            this.btnArea1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnArea1.Name = "btnArea1";
            this.btnArea1.UseVisualStyleBackColor = false;
            this.btnArea1.Click += new System.EventHandler(this.btnArea_Click);
            this.btnArea1.MouseEnter += new System.EventHandler(this.btnArea_Enter);
            this.btnArea1.MouseLeave += new System.EventHandler(this.btnArea_Leave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.entranceGrid);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // entranceGrid
            // 
            this.entranceGrid.AllowUserToAddRows = false;
            this.entranceGrid.AllowUserToDeleteRows = false;
            this.entranceGrid.AllowUserToResizeRows = false;
            this.entranceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entranceGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEntranceName,
            this.colEntranceType,
            this.colCarType});
            resources.ApplyResources(this.entranceGrid, "entranceGrid");
            this.entranceGrid.Name = "entranceGrid";
            this.entranceGrid.RowHeadersVisible = false;
            this.entranceGrid.RowTemplate.Height = 23;
            this.entranceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entranceGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.entranceGrid_DataError);
            // 
            // colEntranceName
            // 
            this.colEntranceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colEntranceName, "colEntranceName");
            this.colEntranceName.Name = "colEntranceName";
            this.colEntranceName.ReadOnly = true;
            this.colEntranceName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colEntranceType
            // 
            resources.ApplyResources(this.colEntranceType, "colEntranceType");
            this.colEntranceType.Name = "colEntranceType";
            this.colEntranceType.ReadOnly = true;
            // 
            // colCarType
            // 
            resources.ApplyResources(this.colCarType, "colCarType");
            this.colCarType.Name = "colCarType";
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ledGrid);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // ledGrid
            // 
            this.ledGrid.AllowUserToAddRows = false;
            this.ledGrid.AllowUserToDeleteRows = false;
            this.ledGrid.AllowUserToResizeRows = false;
            this.ledGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ledGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colComport,
            this.colBaud,
            this.colCarAddress,
            this.colMotorAddress,
            this.colBrightness});
            this.ledGrid.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.ledGrid, "ledGrid");
            this.ledGrid.Name = "ledGrid";
            this.ledGrid.RowHeadersVisible = false;
            this.ledGrid.RowTemplate.Height = 23;
            this.ledGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Add,
            this.mnu_Update,
            this.mnu_Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // mnu_Add
            // 
            this.mnu_Add.Name = "mnu_Add";
            resources.ApplyResources(this.mnu_Add, "mnu_Add");
            this.mnu_Add.Click += new System.EventHandler(this.mnu_Add_Click);
            // 
            // mnu_Update
            // 
            this.mnu_Update.Name = "mnu_Update";
            resources.ApplyResources(this.mnu_Update, "mnu_Update");
            this.mnu_Update.Click += new System.EventHandler(this.mnu_Update_Click);
            // 
            // mnu_Delete
            // 
            this.mnu_Delete.Name = "mnu_Delete";
            resources.ApplyResources(this.mnu_Delete, "mnu_Delete");
            this.mnu_Delete.Click += new System.EventHandler(this.mnu_Delete_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtAutoFreshInterval
            // 
            resources.ApplyResources(this.txtAutoFreshInterval, "txtAutoFreshInterval");
            this.txtAutoFreshInterval.MaxValue = 10000;
            this.txtAutoFreshInterval.MinValue = 0;
            this.txtAutoFreshInterval.Name = "txtAutoFreshInterval";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // notify1
            // 
            this.notify1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notify1, "notify1");
            this.notify1.ContextMenuStrip = this.systemMenu;
            this.notify1.DoubleClick += new System.EventHandler(this.notify1_DoubleClick);
            // 
            // systemMenu
            // 
            this.systemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Exit});
            this.systemMenu.Name = "systemMenu";
            resources.ApplyResources(this.systemMenu, "systemMenu");
            // 
            // mnu_Exit
            // 
            this.mnu_Exit.Name = "mnu_Exit";
            resources.ApplyResources(this.mnu_Exit, "mnu_Exit");
            this.mnu_Exit.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_System,
            this.mnu_Language});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // mnu_System
            // 
            this.mnu_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Connect,
            this.mnu_Exit1});
            this.mnu_System.Name = "mnu_System";
            resources.ApplyResources(this.mnu_System, "mnu_System");
            // 
            // mnu_Connect
            // 
            this.mnu_Connect.Name = "mnu_Connect";
            resources.ApplyResources(this.mnu_Connect, "mnu_Connect");
            // 
            // mnu_Exit1
            // 
            this.mnu_Exit1.Name = "mnu_Exit1";
            resources.ApplyResources(this.mnu_Exit1, "mnu_Exit1");
            this.mnu_Exit1.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // mnu_Language
            // 
            this.mnu_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_SimpleChinese,
            this.mnu_TraditionalChinese,
            this.mnu_English});
            this.mnu_Language.Name = "mnu_Language";
            resources.ApplyResources(this.mnu_Language, "mnu_Language");
            // 
            // mnu_SimpleChinese
            // 
            this.mnu_SimpleChinese.Name = "mnu_SimpleChinese";
            resources.ApplyResources(this.mnu_SimpleChinese, "mnu_SimpleChinese");
            this.mnu_SimpleChinese.Click += new System.EventHandler(this.mnu_Language_Clicked);
            // 
            // mnu_TraditionalChinese
            // 
            this.mnu_TraditionalChinese.Name = "mnu_TraditionalChinese";
            resources.ApplyResources(this.mnu_TraditionalChinese, "mnu_TraditionalChinese");
            this.mnu_TraditionalChinese.Click += new System.EventHandler(this.mnu_Language_Clicked);
            // 
            // mnu_English
            // 
            this.mnu_English.Name = "mnu_English";
            resources.ApplyResources(this.mnu_English, "mnu_English");
            this.mnu_English.Click += new System.EventHandler(this.mnu_Language_Clicked);
            // 
            // colComport
            // 
            resources.ApplyResources(this.colComport, "colComport");
            this.colComport.Name = "colComport";
            this.colComport.ReadOnly = true;
            // 
            // colBaud
            // 
            resources.ApplyResources(this.colBaud, "colBaud");
            this.colBaud.Name = "colBaud";
            this.colBaud.ReadOnly = true;
            this.colBaud.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colBaud.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCarAddress
            // 
            resources.ApplyResources(this.colCarAddress, "colCarAddress");
            this.colCarAddress.Name = "colCarAddress";
            this.colCarAddress.ReadOnly = true;
            // 
            // colMotorAddress
            // 
            resources.ApplyResources(this.colMotorAddress, "colMotorAddress");
            this.colMotorAddress.Name = "colMotorAddress";
            this.colMotorAddress.ReadOnly = true;
            // 
            // colBrightness
            // 
            resources.ApplyResources(this.colBrightness, "colBrightness");
            this.colBrightness.Name = "colBrightness";
            this.colBrightness.ReadOnly = true;
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAutoFreshInterval);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.parkCombobox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entranceGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ledGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.systemMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.Park.UserControls.ParkCombobox parkCombobox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private Ralid.GeneralLibrary.WinformControl.CustomDataGridView entranceGrid;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox groupBox3;
        private Ralid.GeneralLibrary.WinformControl.CustomDataGridView ledGrid;
        private System.Windows.Forms.Button btnArea1;
        private System.Windows.Forms.Button btnArea6;
        private System.Windows.Forms.Button btnArea5;
        private System.Windows.Forms.Button btnArea4;
        private System.Windows.Forms.Button btnArea3;
        private System.Windows.Forms.Button btnArea2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Add;
        private System.Windows.Forms.ToolStripMenuItem mnu_Update;
        private System.Windows.Forms.ToolStripMenuItem mnu_Delete;
        private System.Windows.Forms.Label label6;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtAutoFreshInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NotifyIcon notify1;
        private System.Windows.Forms.ContextMenuStrip systemMenu;
        private System.Windows.Forms.ToolStripMenuItem mnu_Exit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_System;
        private System.Windows.Forms.ToolStripMenuItem mnu_Exit1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Language;
        private System.Windows.Forms.ToolStripMenuItem mnu_SimpleChinese;
        private System.Windows.Forms.ToolStripMenuItem mnu_TraditionalChinese;
        private System.Windows.Forms.ToolStripMenuItem mnu_English;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceType;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCarType;
        private System.Windows.Forms.ToolStripMenuItem mnu_Connect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBaud;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMotorAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBrightness;
    }
}

