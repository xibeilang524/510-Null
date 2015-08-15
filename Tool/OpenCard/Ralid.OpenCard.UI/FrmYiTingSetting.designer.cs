namespace Ralid.OpenCard.UI
{
    partial class FrmYiTingSetting
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
            this.colReaderIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtIP = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.ip2 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip4 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip3 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip1 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.intergerTextBox1 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.intergerTextBox2 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.intergerTextBox3 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.intergerTextBox4 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.txtIP.SuspendLayout();
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
            this.colReaderIP,
            this.colEntrance,
            this.colMemo});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(1, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(625, 219);
            this.dataGridView1.TabIndex = 0;
            // 
            // colReaderIP
            // 
            this.colReaderIP.HeaderText = "读卡器ID";
            this.colReaderIP.Name = "colReaderIP";
            this.colReaderIP.ReadOnly = true;
            this.colReaderIP.Width = 150;
            // 
            // colEntrance
            // 
            this.colEntrance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEntrance.HeaderText = "通道";
            this.colEntrance.Name = "colEntrance";
            this.colEntrance.ReadOnly = true;
            // 
            // colMemo
            // 
            this.colMemo.HeaderText = "说明";
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            this.colMemo.Width = 200;
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
            this.btnSave.Location = new System.Drawing.Point(512, 274);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPort
            // 
            this.txtPort.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPort.Location = new System.Drawing.Point(345, 16);
            this.txtPort.MaxValue = 2147483647;
            this.txtPort.MinValue = -2147483648;
            this.txtPort.Name = "txtPort";
            this.txtPort.NumberWithCommas = false;
            this.txtPort.Size = new System.Drawing.Size(38, 21);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "0";
            // 
            // txtIP
            // 
            this.txtIP.Controls.Add(this.ip2);
            this.txtIP.Controls.Add(this.ip4);
            this.txtIP.Controls.Add(this.ip3);
            this.txtIP.Controls.Add(this.ip1);
            this.txtIP.Controls.Add(this.intergerTextBox1);
            this.txtIP.Controls.Add(this.intergerTextBox2);
            this.txtIP.Controls.Add(this.intergerTextBox3);
            this.txtIP.Controls.Add(this.intergerTextBox4);
            this.txtIP.Location = new System.Drawing.Point(65, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(226, 28);
            this.txtIP.TabIndex = 6;
            // 
            // ip2
            // 
            this.ip2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip2.Location = new System.Drawing.Point(61, 3);
            this.ip2.MaxValue = 255;
            this.ip2.MinValue = 0;
            this.ip2.Name = "ip2";
            this.ip2.NumberWithCommas = false;
            this.ip2.Size = new System.Drawing.Size(39, 21);
            this.ip2.TabIndex = 147;
            this.ip2.Text = "0";
            // 
            // ip4
            // 
            this.ip4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip4.Location = new System.Drawing.Point(177, 3);
            this.ip4.MaxValue = 255;
            this.ip4.MinValue = 0;
            this.ip4.Name = "ip4";
            this.ip4.NumberWithCommas = false;
            this.ip4.Size = new System.Drawing.Size(39, 21);
            this.ip4.TabIndex = 149;
            this.ip4.Text = "0";
            // 
            // ip3
            // 
            this.ip3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip3.Location = new System.Drawing.Point(119, 3);
            this.ip3.MaxValue = 255;
            this.ip3.MinValue = 0;
            this.ip3.Name = "ip3";
            this.ip3.NumberWithCommas = false;
            this.ip3.Size = new System.Drawing.Size(39, 21);
            this.ip3.TabIndex = 148;
            this.ip3.Text = "0";
            // 
            // ip1
            // 
            this.ip1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip1.Location = new System.Drawing.Point(3, 3);
            this.ip1.MaxValue = 255;
            this.ip1.MinValue = 0;
            this.ip1.Name = "ip1";
            this.ip1.NumberWithCommas = false;
            this.ip1.Size = new System.Drawing.Size(39, 21);
            this.ip1.TabIndex = 146;
            this.ip1.Text = "0";
            // 
            // intergerTextBox1
            // 
            this.intergerTextBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.intergerTextBox1.Location = new System.Drawing.Point(61, 3);
            this.intergerTextBox1.MaxValue = 255;
            this.intergerTextBox1.MinValue = 0;
            this.intergerTextBox1.Name = "intergerTextBox1";
            this.intergerTextBox1.NumberWithCommas = false;
            this.intergerTextBox1.Size = new System.Drawing.Size(39, 21);
            this.intergerTextBox1.TabIndex = 147;
            this.intergerTextBox1.Text = "0";
            // 
            // intergerTextBox2
            // 
            this.intergerTextBox2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.intergerTextBox2.Location = new System.Drawing.Point(177, 3);
            this.intergerTextBox2.MaxValue = 255;
            this.intergerTextBox2.MinValue = 0;
            this.intergerTextBox2.Name = "intergerTextBox2";
            this.intergerTextBox2.NumberWithCommas = false;
            this.intergerTextBox2.Size = new System.Drawing.Size(39, 21);
            this.intergerTextBox2.TabIndex = 149;
            this.intergerTextBox2.Text = "0";
            // 
            // intergerTextBox3
            // 
            this.intergerTextBox3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.intergerTextBox3.Location = new System.Drawing.Point(119, 3);
            this.intergerTextBox3.MaxValue = 255;
            this.intergerTextBox3.MinValue = 0;
            this.intergerTextBox3.Name = "intergerTextBox3";
            this.intergerTextBox3.NumberWithCommas = false;
            this.intergerTextBox3.Size = new System.Drawing.Size(39, 21);
            this.intergerTextBox3.TabIndex = 148;
            this.intergerTextBox3.Text = "0";
            // 
            // intergerTextBox4
            // 
            this.intergerTextBox4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.intergerTextBox4.Location = new System.Drawing.Point(3, 3);
            this.intergerTextBox4.MaxValue = 255;
            this.intergerTextBox4.MinValue = 0;
            this.intergerTextBox4.Name = "intergerTextBox4";
            this.intergerTextBox4.NumberWithCommas = false;
            this.intergerTextBox4.Size = new System.Drawing.Size(39, 21);
            this.intergerTextBox4.TabIndex = 146;
            this.intergerTextBox4.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "控制机IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "端口";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(5, 273);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 34);
            this.button3.TabIndex = 14;
            this.button3.Text = "增加";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.mnu_Add_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(134, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 34);
            this.button2.TabIndex = 13;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.mnu_Update_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(254, 273);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 34);
            this.button1.TabIndex = 12;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.mnu_Delete_Click);
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEnable.ForeColor = System.Drawing.Color.Red;
            this.chkEnable.Location = new System.Drawing.Point(411, 281);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(95, 20);
            this.chkEnable.TabIndex = 15;
            this.chkEnable.Text = "启动服务";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // FrmYiTingSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 319);
            this.Controls.Add(this.chkEnable);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmYiTingSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "驿停闪付设置";
            this.Load += new System.EventHandler(this.FrmZSTSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.txtIP.ResumeLayout(false);
            this.txtIP.PerformLayout();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colReaderIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private GeneralLibrary.WinformControl.IntergerTextBox txtPort;
        private GeneralLibrary.WinformControl.UCIPTextBox txtIP;
        private GeneralLibrary.WinformControl.IntergerTextBox ip2;
        private GeneralLibrary.WinformControl.IntergerTextBox ip4;
        private GeneralLibrary.WinformControl.IntergerTextBox ip3;
        private GeneralLibrary.WinformControl.IntergerTextBox ip1;
        private GeneralLibrary.WinformControl.IntergerTextBox intergerTextBox1;
        private GeneralLibrary.WinformControl.IntergerTextBox intergerTextBox2;
        private GeneralLibrary.WinformControl.IntergerTextBox intergerTextBox3;
        private GeneralLibrary.WinformControl.IntergerTextBox intergerTextBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkEnable;

    }
}

