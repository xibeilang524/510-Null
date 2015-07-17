namespace Ralid.Park.UI
{
    partial class FrmCarPlateManualExit
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
            this.txtCarplate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAdvance = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvCarPlateLists = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnterDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.searchInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.plEvent = new Ralid.Park.UserControls.UCPictureListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkNoCarPlate = new System.Windows.Forms.CheckBox();
            this.txtFindCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucEnterDateTime = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.btnCardIDOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSearch.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarPlateLists)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCarplate
            // 
            this.txtCarplate.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.txtCarplate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCarplate.Location = new System.Drawing.Point(164, 53);
            this.txtCarplate.Name = "txtCarplate";
            this.txtCarplate.Size = new System.Drawing.Size(305, 30);
            this.txtCarplate.TabIndex = 1;
            this.txtCarplate.TextChanged += new System.EventHandler(this.txtCarplate_TextChanged);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(-61, 35);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 84);
            this.listBox1.TabIndex = 2;
            this.listBox1.Visible = false;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(65, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "请输入车牌号码:";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btnOk.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point(475, 48);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(153, 37);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(24, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(845, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "手动输入 ----------------------------------------------------------------------------" +
                "-------------------------------------------------------";
            // 
            // btnAdvance
            // 
            this.btnAdvance.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btnAdvance.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnAdvance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdvance.Location = new System.Drawing.Point(659, 48);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(153, 37);
            this.btnAdvance.TabIndex = 4;
            this.btnAdvance.Text = "高级查找";
            this.btnAdvance.UseVisualStyleBackColor = true;
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.pnlGrid);
            this.pnlSearch.Controls.Add(this.groupBox3);
            this.pnlSearch.Controls.Add(this.groupBox2);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.groupBox1);
            this.pnlSearch.Controls.Add(this.btnCardIDOK);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Location = new System.Drawing.Point(18, 125);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(857, 508);
            this.pnlSearch.TabIndex = 32;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgvCarPlateLists);
            this.pnlGrid.Controls.Add(this.statusStrip1);
            this.pnlGrid.Location = new System.Drawing.Point(8, 123);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(418, 377);
            this.pnlGrid.TabIndex = 46;
            // 
            // dgvCarPlateLists
            // 
            this.dgvCarPlateLists.AllowUserToAddRows = false;
            this.dgvCarPlateLists.AllowUserToDeleteRows = false;
            this.dgvCarPlateLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarPlateLists.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colCarPlate,
            this.colEnterDateTime});
            this.dgvCarPlateLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarPlateLists.Location = new System.Drawing.Point(0, 0);
            this.dgvCarPlateLists.Name = "dgvCarPlateLists";
            this.dgvCarPlateLists.ReadOnly = true;
            this.dgvCarPlateLists.RowHeadersVisible = false;
            this.dgvCarPlateLists.RowTemplate.Height = 23;
            this.dgvCarPlateLists.Size = new System.Drawing.Size(418, 355);
            this.dgvCarPlateLists.TabIndex = 10;
            this.dgvCarPlateLists.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarPlateLists_RowEnter);
            // 
            // colCardID
            // 
            this.colCardID.HeaderText = "卡号";
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colCarPlate
            // 
            this.colCarPlate.HeaderText = "车牌号码";
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colEnterDateTime
            // 
            this.colEnterDateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEnterDateTime.HeaderText = "入场时间";
            this.colEnterDateTime.Name = "colEnterDateTime";
            this.colEnterDateTime.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 355);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(418, 22);
            this.statusStrip1.TabIndex = 38;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // searchInfo
            // 
            this.searchInfo.Name = "searchInfo";
            this.searchInfo.Size = new System.Drawing.Size(403, 17);
            this.searchInfo.Spring = true;
            this.searchInfo.Text = "总共 0 项";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.plEvent);
            this.groupBox3.Location = new System.Drawing.Point(432, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 385);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "入场图片";
            // 
            // plEvent
            // 
            this.plEvent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plEvent.Location = new System.Drawing.Point(3, 15);
            this.plEvent.Name = "plEvent";
            this.plEvent.Size = new System.Drawing.Size(412, 364);
            this.plEvent.TabIndex = 11;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chkNoCarPlate);
            this.groupBox2.Controls.Add(this.txtFindCarPlate);
            this.groupBox2.Location = new System.Drawing.Point(246, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 94);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "车牌号码：";
            // 
            // chkNoCarPlate
            // 
            this.chkNoCarPlate.AutoSize = true;
            this.chkNoCarPlate.Location = new System.Drawing.Point(8, 62);
            this.chkNoCarPlate.Name = "chkNoCarPlate";
            this.chkNoCarPlate.Size = new System.Drawing.Size(108, 16);
            this.chkNoCarPlate.TabIndex = 7;
            this.chkNoCarPlate.Text = "查找无车牌车辆";
            this.chkNoCarPlate.UseVisualStyleBackColor = true;
            this.chkNoCarPlate.CheckedChanged += new System.EventHandler(this.chkNoCarPlate_CheckedChanged);
            // 
            // txtFindCarPlate
            // 
            this.txtFindCarPlate.Font = new System.Drawing.Font("宋体", 9F);
            this.txtFindCarPlate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtFindCarPlate.Location = new System.Drawing.Point(71, 24);
            this.txtFindCarPlate.Name = "txtFindCarPlate";
            this.txtFindCarPlate.Size = new System.Drawing.Size(95, 21);
            this.txtFindCarPlate.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(457, 42);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(153, 37);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "模糊查找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucEnterDateTime);
            this.groupBox1.Location = new System.Drawing.Point(8, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 94);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入场时间";
            // 
            // ucEnterDateTime
            // 
            this.ucEnterDateTime.EndDateTime = new System.DateTime(2014, 11, 27, 15, 19, 44, 815);
            this.ucEnterDateTime.Location = new System.Drawing.Point(6, 14);
            this.ucEnterDateTime.Name = "ucEnterDateTime";
            this.ucEnterDateTime.ShowTime = true;
            this.ucEnterDateTime.Size = new System.Drawing.Size(221, 74);
            this.ucEnterDateTime.StartDateTime = new System.DateTime(2014, 11, 27, 15, 19, 44, 815);
            this.ucEnterDateTime.TabIndex = 5;
            // 
            // btnCardIDOK
            // 
            this.btnCardIDOK.Enabled = false;
            this.btnCardIDOK.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btnCardIDOK.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnCardIDOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCardIDOK.Location = new System.Drawing.Point(641, 42);
            this.btnCardIDOK.Name = "btnCardIDOK";
            this.btnCardIDOK.Size = new System.Drawing.Size(153, 37);
            this.btnCardIDOK.TabIndex = 9;
            this.btnCardIDOK.Text = "确定";
            this.btnCardIDOK.UseVisualStyleBackColor = true;
            this.btnCardIDOK.Click += new System.EventHandler(this.btnCardIDOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(845, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "车辆查找 ----------------------------------------------------------------------------" +
                "-------------------------------------------------------";
            // 
            // FrmCarPlateManualExit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 647);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.txtCarplate);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdvance);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmCarPlateManualExit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车辆出场";
            this.Load += new System.EventHandler(this.FrmCarPlateManualExit_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarPlateLists)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GeneralLibrary.WinformControl.DBCTextBox txtCarplate;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAdvance;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlGrid;
        private UserControls.CustomDataGridView dgvCarPlateLists;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnterDateTime;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel searchInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private UserControls.UCPictureListView plEvent;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkNoCarPlate;
        private GeneralLibrary.WinformControl.DBCTextBox txtFindCarPlate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.UCDateTimeInterval ucEnterDateTime;
        private System.Windows.Forms.Button btnCardIDOK;
        private System.Windows.Forms.Label label1;
    }
}