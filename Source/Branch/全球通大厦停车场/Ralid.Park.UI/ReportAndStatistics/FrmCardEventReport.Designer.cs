namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardEventReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardEventReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.ucEntrance1 = new Ralid.Park.UserControls.UCEntrance();
            this.comEntrance = new Ralid.Park.UserControls.EntranceComboBox(this.components);
            this.comPark = new Ralid.Park.UserControls.ParkCombobox(this.components);
            this.carTypeComboBox1 = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.comOperator = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntranceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimitation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimitationRemain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.ucEntrance1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            // 
            // btnSaveAs
            // 
            resources.ApplyResources(this.btnSaveAs, "btnSaveAs");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ucDateTimeInterval1
            // 
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 9, 23, 59, 59, 0);
            resources.ApplyResources(this.ucDateTimeInterval1, "ucDateTimeInterval1");
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 9, 16, 56, 56, 625);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtOwnerName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtCertificate);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.ucEntrance1);
            this.groupBox3.Controls.Add(this.carTypeComboBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.comCardType);
            this.groupBox3.Controls.Add(this.comOperator);
            this.groupBox3.Controls.Add(this.txtCarPlate);
            this.groupBox3.Controls.Add(this.txtCardID);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtOwnerName
            // 
            resources.ApplyResources(this.txtOwnerName, "txtOwnerName");
            this.txtOwnerName.Name = "txtOwnerName";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtCertificate
            // 
            resources.ApplyResources(this.txtCertificate, "txtCertificate");
            this.txtCertificate.Name = "txtCertificate";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // ucEntrance1
            // 
            this.ucEntrance1.Controls.Add(this.comEntrance);
            this.ucEntrance1.Controls.Add(this.comPark);
            resources.ApplyResources(this.ucEntrance1, "ucEntrance1");
            this.ucEntrance1.Name = "ucEntrance1";
            // 
            // comEntrance
            // 
            this.comEntrance.FormattingEnabled = true;
            resources.ApplyResources(this.comEntrance, "comEntrance");
            this.comEntrance.Name = "comEntrance";
            // 
            // comPark
            // 
            this.comPark.FormattingEnabled = true;
            resources.ApplyResources(this.comPark, "comPark");
            this.comPark.Name = "comPark";
            // 
            // carTypeComboBox1
            // 
            this.carTypeComboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.carTypeComboBox1, "carTypeComboBox1");
            this.carTypeComboBox1.Name = "carTypeComboBox1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.Name = "comCardType";
            // 
            // comOperator
            // 
            this.comOperator.FormattingEnabled = true;
            resources.ApplyResources(this.comOperator, "comOperator");
            this.comOperator.Name = "comOperator";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colOwnerName,
            this.colCardCertificate,
            this.colCarPlate,
            this.colEventDateTime,
            this.colEntranceName,
            this.colLastDateTime,
            this.colLimitation,
            this.colLimitationRemain,
            this.colOperatorID,
            this.colEventType,
            this.colCardType,
            this.colCarType});
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridView1_CellDoubleClick);
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // colCardID
            // 
            this.colCardID.DataPropertyName = "CardID";
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
            // colCardCertificate
            // 
            resources.ApplyResources(this.colCardCertificate, "colCardCertificate");
            this.colCardCertificate.Name = "colCardCertificate";
            this.colCardCertificate.ReadOnly = true;
            // 
            // colCarPlate
            // 
            resources.ApplyResources(this.colCarPlate, "colCarPlate");
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colEventDateTime
            // 
            this.colEventDateTime.DataPropertyName = "EventDateTime";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.colEventDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.colEventDateTime, "colEventDateTime");
            this.colEventDateTime.Name = "colEventDateTime";
            this.colEventDateTime.ReadOnly = true;
            // 
            // colEntranceName
            // 
            this.colEntranceName.DataPropertyName = "EventAddress";
            resources.ApplyResources(this.colEntranceName, "colEntranceName");
            this.colEntranceName.Name = "colEntranceName";
            this.colEntranceName.ReadOnly = true;
            // 
            // colLastDateTime
            // 
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.colLastDateTime.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.colLastDateTime, "colLastDateTime");
            this.colLastDateTime.Name = "colLastDateTime";
            this.colLastDateTime.ReadOnly = true;
            // 
            // colLimitation
            // 
            resources.ApplyResources(this.colLimitation, "colLimitation");
            this.colLimitation.Name = "colLimitation";
            this.colLimitation.ReadOnly = true;
            // 
            // colLimitationRemain
            // 
            resources.ApplyResources(this.colLimitationRemain, "colLimitationRemain");
            this.colLimitationRemain.Name = "colLimitationRemain";
            this.colLimitationRemain.ReadOnly = true;
            // 
            // colOperatorID
            // 
            this.colOperatorID.DataPropertyName = "OperatorID";
            resources.ApplyResources(this.colOperatorID, "colOperatorID");
            this.colOperatorID.Name = "colOperatorID";
            this.colOperatorID.ReadOnly = true;
            // 
            // colEventType
            // 
            this.colEventType.DataPropertyName = "EventDescription";
            resources.ApplyResources(this.colEventType, "colEventType");
            this.colEventType.Name = "colEventType";
            this.colEventType.ReadOnly = true;
            // 
            // colCardType
            // 
            this.colCardType.DataPropertyName = "CardType";
            resources.ApplyResources(this.colCardType, "colCardType");
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            // 
            // colCarType
            // 
            resources.ApplyResources(this.colCarType, "colCarType");
            this.colCarType.Name = "colCarType";
            this.colCarType.ReadOnly = true;
            // 
            // FrmCardEventReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.customDataGridView1);
            this.Name = "FrmCardEventReport";
            this.Load += new System.EventHandler(this.FrmCardEventReport_Load);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ucEntrance1.ResumeLayout(false);
            this.ucEntrance1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Ralid.Park.UserControls.OperatorComboBox comOperator;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label5;
        private UserControls.CarTypeComboBox carTypeComboBox1;
        private UserControls.UCEntrance ucEntrance1;
        private UserControls.EntranceComboBox comEntrance;
        private UserControls.ParkCombobox comPark;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label6;
        private GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimitation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimitationRemain;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarType;
    }
}