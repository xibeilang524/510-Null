namespace Ralid.Park.UI
{
    partial class FrmCardIDConvert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardIDConvert));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdTo26 = new System.Windows.Forms.RadioButton();
            this.rdTo34 = new System.Windows.Forms.RadioButton();
            this.btnConvert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.txtCardCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.colNewCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParkingStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rdTo26);
            this.panel1.Controls.Add(this.rdTo34);
            this.panel1.Name = "panel1";
            // 
            // rdTo26
            // 
            resources.ApplyResources(this.rdTo26, "rdTo26");
            this.rdTo26.Name = "rdTo26";
            this.rdTo26.UseVisualStyleBackColor = true;
            // 
            // rdTo34
            // 
            resources.ApplyResources(this.rdTo34, "rdTo34");
            this.rdTo34.Checked = true;
            this.rdTo34.Name = "rdTo34";
            this.rdTo34.TabStop = true;
            this.rdTo34.UseVisualStyleBackColor = true;
            // 
            // btnConvert
            // 
            resources.ApplyResources(this.btnConvert, "btnConvert");
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Name = "label1";
            // 
            // btnFilter
            // 
            resources.ApplyResources(this.btnFilter, "btnFilter");
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilterCard_Click);
            // 
            // dataGridView1
            // 
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNewCardID,
            this.colCardID,
            this.colOwnerName,
            this.colCertificate,
            this.colCardType,
            this.colCarPlate,
            this.colParkingStatus,
            this.colFill});
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtCardCertificate
            // 
            resources.ApplyResources(this.txtCardCertificate, "txtCardCertificate");
            this.txtCardCertificate.Name = "txtCardCertificate";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtOwnerName
            // 
            resources.ApplyResources(this.txtOwnerName, "txtOwnerName");
            this.txtOwnerName.Name = "txtOwnerName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCount});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.Spring = true;
            // 
            // colNewCardID
            // 
            resources.ApplyResources(this.colNewCardID, "colNewCardID");
            this.colNewCardID.Name = "colNewCardID";
            this.colNewCardID.ReadOnly = true;
            // 
            // colCardID
            // 
            resources.ApplyResources(this.colCardID, "colCardID");
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            // 
            // colOwnerName
            // 
            resources.ApplyResources(this.colOwnerName, "colOwnerName");
            this.colOwnerName.Name = "colOwnerName";
            this.colOwnerName.ReadOnly = true;
            // 
            // colCertificate
            // 
            resources.ApplyResources(this.colCertificate, "colCertificate");
            this.colCertificate.Name = "colCertificate";
            this.colCertificate.ReadOnly = true;
            // 
            // colCardType
            // 
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colParkingStatus
            // 
            resources.ApplyResources(this.colParkingStatus, "colParkingStatus");
            this.colParkingStatus.Name = "colParkingStatus";
            this.colParkingStatus.ReadOnly = true;
            // 
            // colFill
            // 
            this.colFill.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colFill, "colFill");
            this.colFill.Name = "colFill";
            this.colFill.ReadOnly = true;
            // 
            // FrmCardIDConvert
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtCardCertificate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCarPlate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOwnerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comCardType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCardIDConvert";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCardIDConvert_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdTo26;
        private System.Windows.Forms.RadioButton rdTo34;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private System.Windows.Forms.Label label5;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardCertificate;
        private System.Windows.Forms.Label label8;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label4;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParkingStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFill;
    }
}

