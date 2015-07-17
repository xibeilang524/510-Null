namespace Ralid.Park.UI
{
    partial class FrmVehicleLedSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVehicleLedSetting));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_InitLedMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.chkOnlyStationLed = new System.Windows.Forms.CheckBox();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShowTitle = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSubAddress1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubMessage1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubTitle1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubInterval1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubAddress2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubTitle2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubMessage2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubInterval2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubAddress3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubTitle3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubMessage3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubInterval3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGet
            // 
            resources.ApplyResources(this.btnGet, "btnGet");
            this.btnGet.Name = "btnGet";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colPark,
            this.colEntrance,
            this.colStation,
            this.colComPort,
            this.colShowTitle,
            this.colSubAddress1,
            this.colSubMessage1,
            this.colSubTitle1,
            this.colSubInterval1,
            this.colSubAddress2,
            this.colSubTitle2,
            this.colSubMessage2,
            this.colSubInterval2,
            this.colSubAddress3,
            this.colSubTitle3,
            this.colSubMessage3,
            this.colSubInterval3,
            this.colMemo});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Add,
            this.mnu_Update,
            this.mnu_Delete,
            this.mnu_InitLedMsg});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // mnu_Add
            // 
            resources.ApplyResources(this.mnu_Add, "mnu_Add");
            this.mnu_Add.Name = "mnu_Add";
            this.mnu_Add.Click += new System.EventHandler(this.mnu_Add_Click);
            // 
            // mnu_Update
            // 
            resources.ApplyResources(this.mnu_Update, "mnu_Update");
            this.mnu_Update.Name = "mnu_Update";
            this.mnu_Update.Click += new System.EventHandler(this.mnu_Update_Click);
            // 
            // mnu_Delete
            // 
            resources.ApplyResources(this.mnu_Delete, "mnu_Delete");
            this.mnu_Delete.Name = "mnu_Delete";
            this.mnu_Delete.Click += new System.EventHandler(this.mnu_Delete_Click);
            // 
            // mnu_InitLedMsg
            // 
            resources.ApplyResources(this.mnu_InitLedMsg, "mnu_InitLedMsg");
            this.mnu_InitLedMsg.Name = "mnu_InitLedMsg";
            this.mnu_InitLedMsg.Click += new System.EventHandler(this.mnu_InitLedMsg_Click);
            // 
            // chkOnlyStationLed
            // 
            resources.ApplyResources(this.chkOnlyStationLed, "chkOnlyStationLed");
            this.chkOnlyStationLed.Name = "chkOnlyStationLed";
            this.chkOnlyStationLed.UseVisualStyleBackColor = true;
            this.chkOnlyStationLed.CheckedChanged += new System.EventHandler(this.chkOnlyStationLed_CheckedChanged);
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colPark
            // 
            resources.ApplyResources(this.colPark, "colPark");
            this.colPark.Name = "colPark";
            this.colPark.ReadOnly = true;
            // 
            // colEntrance
            // 
            this.colEntrance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colEntrance, "colEntrance");
            this.colEntrance.Name = "colEntrance";
            this.colEntrance.ReadOnly = true;
            // 
            // colStation
            // 
            resources.ApplyResources(this.colStation, "colStation");
            this.colStation.Name = "colStation";
            this.colStation.ReadOnly = true;
            // 
            // colComPort
            // 
            resources.ApplyResources(this.colComPort, "colComPort");
            this.colComPort.Name = "colComPort";
            this.colComPort.ReadOnly = true;
            // 
            // colShowTitle
            // 
            resources.ApplyResources(this.colShowTitle, "colShowTitle");
            this.colShowTitle.Name = "colShowTitle";
            this.colShowTitle.ReadOnly = true;
            // 
            // colSubAddress1
            // 
            resources.ApplyResources(this.colSubAddress1, "colSubAddress1");
            this.colSubAddress1.Name = "colSubAddress1";
            this.colSubAddress1.ReadOnly = true;
            // 
            // colSubMessage1
            // 
            resources.ApplyResources(this.colSubMessage1, "colSubMessage1");
            this.colSubMessage1.Name = "colSubMessage1";
            this.colSubMessage1.ReadOnly = true;
            // 
            // colSubTitle1
            // 
            resources.ApplyResources(this.colSubTitle1, "colSubTitle1");
            this.colSubTitle1.Name = "colSubTitle1";
            this.colSubTitle1.ReadOnly = true;
            // 
            // colSubInterval1
            // 
            resources.ApplyResources(this.colSubInterval1, "colSubInterval1");
            this.colSubInterval1.Name = "colSubInterval1";
            this.colSubInterval1.ReadOnly = true;
            // 
            // colSubAddress2
            // 
            resources.ApplyResources(this.colSubAddress2, "colSubAddress2");
            this.colSubAddress2.Name = "colSubAddress2";
            this.colSubAddress2.ReadOnly = true;
            // 
            // colSubTitle2
            // 
            resources.ApplyResources(this.colSubTitle2, "colSubTitle2");
            this.colSubTitle2.Name = "colSubTitle2";
            this.colSubTitle2.ReadOnly = true;
            // 
            // colSubMessage2
            // 
            resources.ApplyResources(this.colSubMessage2, "colSubMessage2");
            this.colSubMessage2.Name = "colSubMessage2";
            this.colSubMessage2.ReadOnly = true;
            // 
            // colSubInterval2
            // 
            resources.ApplyResources(this.colSubInterval2, "colSubInterval2");
            this.colSubInterval2.Name = "colSubInterval2";
            this.colSubInterval2.ReadOnly = true;
            // 
            // colSubAddress3
            // 
            resources.ApplyResources(this.colSubAddress3, "colSubAddress3");
            this.colSubAddress3.Name = "colSubAddress3";
            this.colSubAddress3.ReadOnly = true;
            // 
            // colSubTitle3
            // 
            resources.ApplyResources(this.colSubTitle3, "colSubTitle3");
            this.colSubTitle3.Name = "colSubTitle3";
            this.colSubTitle3.ReadOnly = true;
            // 
            // colSubMessage3
            // 
            resources.ApplyResources(this.colSubMessage3, "colSubMessage3");
            this.colSubMessage3.Name = "colSubMessage3";
            this.colSubMessage3.ReadOnly = true;
            // 
            // colSubInterval3
            // 
            resources.ApplyResources(this.colSubInterval3, "colSubInterval3");
            this.colSubInterval3.Name = "colSubInterval3";
            this.colSubInterval3.ReadOnly = true;
            // 
            // colMemo
            // 
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // FrmVehicleLedSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkOnlyStationLed);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmVehicleLedSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVehicleLedSetting_FormClosing);
            this.Load += new System.EventHandler(this.FrmVehicleLedSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_Add;
        private System.Windows.Forms.ToolStripMenuItem mnu_Update;
        private System.Windows.Forms.ToolStripMenuItem mnu_Delete;
        private System.Windows.Forms.ToolStripMenuItem mnu_InitLedMsg;
        private System.Windows.Forms.CheckBox chkOnlyStationLed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComPort;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colShowTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubAddress1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubMessage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubTitle1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubInterval1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubAddress2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubTitle2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubMessage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubInterval2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubAddress3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubTitle3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubMessage3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubInterval3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
    }
}