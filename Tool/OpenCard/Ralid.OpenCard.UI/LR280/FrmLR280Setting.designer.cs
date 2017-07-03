namespace Ralid.OpenCard.UI.LR280
{
    partial class FrmLR280Setting
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colComport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.txtTimeout = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colComport,
            this.colEntrance,
            this.colMemo});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(1, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(637, 341);
            this.dataGridView1.TabIndex = 0;
            // 
            // colComport
            // 
            this.colComport.HeaderText = "串口";
            this.colComport.Name = "colComport";
            this.colComport.ReadOnly = true;
            this.colComport.Width = 80;
            // 
            // colEntrance
            // 
            this.colEntrance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colEntrance.HeaderText = "所在通道";
            this.colEntrance.MinimumWidth = 200;
            this.colEntrance.Name = "colEntrance";
            this.colEntrance.ReadOnly = true;
            this.colEntrance.Width = 200;
            // 
            // colMemo
            // 
            this.colMemo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMemo.HeaderText = "说明";
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Add,
            this.mnu_Update,
            this.mnu_Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // mnu_Add
            // 
            this.mnu_Add.Name = "mnu_Add";
            this.mnu_Add.Size = new System.Drawing.Size(100, 22);
            this.mnu_Add.Text = "增加";
            this.mnu_Add.Click += new System.EventHandler(this.mnu_Add_Click);
            // 
            // mnu_Update
            // 
            this.mnu_Update.Name = "mnu_Update";
            this.mnu_Update.Size = new System.Drawing.Size(100, 22);
            this.mnu_Update.Text = "修改";
            this.mnu_Update.Click += new System.EventHandler(this.mnu_Update_Click);
            // 
            // mnu_Delete
            // 
            this.mnu_Delete.Name = "mnu_Delete";
            this.mnu_Delete.Size = new System.Drawing.Size(100, 22);
            this.mnu_Delete.Text = "删除";
            this.mnu_Delete.Click += new System.EventHandler(this.mnu_Delete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(524, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkEnable
            // 
            this.chkEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnable.AutoSize = true;
            this.chkEnable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEnable.ForeColor = System.Drawing.Color.Red;
            this.chkEnable.Location = new System.Drawing.Point(423, 356);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(95, 20);
            this.chkEnable.TabIndex = 16;
            this.chkEnable.Text = "启动服务";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // txtTimeout
            // 
            this.txtTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeout.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtTimeout.Location = new System.Drawing.Point(325, 356);
            this.txtTimeout.MaxValue = 999;
            this.txtTimeout.MinValue = 1;
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.NumberWithCommas = false;
            this.txtTimeout.Size = new System.Drawing.Size(69, 21);
            this.txtTimeout.TabIndex = 24;
            this.txtTimeout.Text = "30";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "读卡器超时时间(秒)";
            // 
            // FrmLR280Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 396);
            this.Controls.Add(this.txtTimeout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEnable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmLR280Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "银联闪付卡设置";
            this.Load += new System.EventHandler(this.FrmZSTSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Add;
        private System.Windows.Forms.ToolStripMenuItem mnu_Update;
        private System.Windows.Forms.ToolStripMenuItem mnu_Delete;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private GeneralLibrary.WinformControl.IntergerTextBox txtTimeout;
        private System.Windows.Forms.Label label1;

    }
}

