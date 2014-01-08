namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardInParkReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardInParkReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.gridCard = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardCertificate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnterDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHasPaid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.carTypeComboBox1 = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCard)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 6, 15, 23, 28, 975);
            resources.ApplyResources(this.ucDateTimeInterval1, "ucDateTimeInterval1");
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 5, 17, 12, 37, 562);
            // 
            // gridCard
            // 
            this.gridCard.AllowUserToAddRows = false;
            this.gridCard.AllowUserToDeleteRows = false;
            this.gridCard.AllowUserToResizeRows = false;
            resources.ApplyResources(this.gridCard, "gridCard");
            this.gridCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colOwnerName,
            this.colCardCertificate,
            this.colLastCarPlate,
            this.colCardType,
            this.colCarType,
            this.colEnterDateTime,
            this.colEntrance,
            this.colHasPaid});
            this.gridCard.Name = "gridCard";
            this.gridCard.RowHeadersVisible = false;
            this.gridCard.RowTemplate.Height = 23;
            this.gridCard.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCard_CellContentDoubleClick);
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
            // colCardCertificate
            // 
            resources.ApplyResources(this.colCardCertificate, "colCardCertificate");
            this.colCardCertificate.Name = "colCardCertificate";
            this.colCardCertificate.ReadOnly = true;
            // 
            // colLastCarPlate
            // 
            resources.ApplyResources(this.colLastCarPlate, "colLastCarPlate");
            this.colLastCarPlate.Name = "colLastCarPlate";
            this.colLastCarPlate.ReadOnly = true;
            // 
            // colCardType
            // 
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
            // colEnterDateTime
            // 
            resources.ApplyResources(this.colEnterDateTime, "colEnterDateTime");
            this.colEnterDateTime.Name = "colEnterDateTime";
            this.colEnterDateTime.ReadOnly = true;
            // 
            // colEntrance
            // 
            resources.ApplyResources(this.colEntrance, "colEntrance");
            this.colEntrance.Name = "colEntrance";
            this.colEntrance.ReadOnly = true;
            // 
            // colHasPaid
            // 
            resources.ApplyResources(this.colHasPaid, "colHasPaid");
            this.colHasPaid.Name = "colHasPaid";
            this.colHasPaid.ReadOnly = true;
            this.colHasPaid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colHasPaid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.carTypeComboBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comCardType);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtOwnerName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCertificate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCarPlate);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // FrmCardInParkReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gridCard);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCardInParkReport";
            this.Load += new System.EventHandler(this.FrmCardInParkReport_Load);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.gridCard, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCard)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private UserControls.CustomDataGridView gridCard;
        private System.Windows.Forms.GroupBox groupBox2;
        private GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label7;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label9;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private UserControls.CardTypeComboBox comCardType;
        private System.Windows.Forms.Label label10;
        private UserControls.CarTypeComboBox carTypeComboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnterDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrance;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHasPaid;
    }
}