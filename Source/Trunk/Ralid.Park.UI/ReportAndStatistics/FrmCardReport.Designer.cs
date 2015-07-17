namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCardReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardReport));
            this.accessComboBox1 = new Ralid.Park.UserControls.AccessComboBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txtCardCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCarStatus = new System.Windows.Forms.ComboBox();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.comCardStatus = new Ralid.Park.UserControls.CardStatusComboBox(this.components);
            this.comChargeType = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtExpireDateBefore = new System.Windows.Forms.DateTimePicker();
            this.dtExpireDateAfter = new System.Windows.Forms.DateTimePicker();
            this.chkExpireDateBefore = new System.Windows.Forms.CheckBox();
            this.chkExpireDateAfter = new System.Windows.Forms.CheckBox();
            this.cardView = new Ralid.Park.UserControls.CardGridView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).BeginInit();
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
            // accessComboBox1
            // 
            resources.ApplyResources(this.accessComboBox1, "accessComboBox1");
            this.accessComboBox1.FormattingEnabled = true;
            this.accessComboBox1.Name = "accessComboBox1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbCarStatus
            // 
            resources.ApplyResources(this.cmbCarStatus, "cmbCarStatus");
            this.cmbCarStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarStatus.FormattingEnabled = true;
            this.cmbCarStatus.Items.AddRange(new object[] {
            resources.GetString("cmbCarStatus.Items"),
            resources.GetString("cmbCarStatus.Items1"),
            resources.GetString("cmbCarStatus.Items2")});
            this.cmbCarStatus.Name = "cmbCarStatus";
            // 
            // txtOwnerName
            // 
            resources.ApplyResources(this.txtOwnerName, "txtOwnerName");
            this.txtOwnerName.Name = "txtOwnerName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comCardStatus
            // 
            resources.ApplyResources(this.comCardStatus, "comCardStatus");
            this.comCardStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardStatus.FormattingEnabled = true;
            this.comCardStatus.Name = "comCardStatus";
            // 
            // comChargeType
            // 
            resources.ApplyResources(this.comChargeType, "comChargeType");
            this.comChargeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comChargeType.FormattingEnabled = true;
            this.comChargeType.Name = "comChargeType";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dtExpireDateBefore
            // 
            resources.ApplyResources(this.dtExpireDateBefore, "dtExpireDateBefore");
            this.dtExpireDateBefore.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtExpireDateBefore.Name = "dtExpireDateBefore";
            // 
            // dtExpireDateAfter
            // 
            resources.ApplyResources(this.dtExpireDateAfter, "dtExpireDateAfter");
            this.dtExpireDateAfter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtExpireDateAfter.Name = "dtExpireDateAfter";
            // 
            // chkExpireDateBefore
            // 
            resources.ApplyResources(this.chkExpireDateBefore, "chkExpireDateBefore");
            this.chkExpireDateBefore.Name = "chkExpireDateBefore";
            this.chkExpireDateBefore.UseVisualStyleBackColor = true;
            // 
            // chkExpireDateAfter
            // 
            resources.ApplyResources(this.chkExpireDateAfter, "chkExpireDateAfter");
            this.chkExpireDateAfter.Name = "chkExpireDateAfter";
            this.chkExpireDateAfter.UseVisualStyleBackColor = true;
            // 
            // cardView
            // 
            resources.ApplyResources(this.cardView, "cardView");
            this.cardView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardView.Name = "cardView";
            this.cardView.RowTemplate.Height = 23;
            // 
            // FrmCardReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cardView);
            this.Controls.Add(this.dtExpireDateBefore);
            this.Controls.Add(this.dtExpireDateAfter);
            this.Controls.Add(this.chkExpireDateBefore);
            this.Controls.Add(this.chkExpireDateAfter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.accessComboBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCardCertificate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCarPlate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCarStatus);
            this.Controls.Add(this.txtOwnerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comCardStatus);
            this.Controls.Add(this.comChargeType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.comCardType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Name = "FrmCardReport";
            this.Load += new System.EventHandler(this.FrmCardReport_Load);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.comCardType, 0);
            this.Controls.SetChildIndex(this.txtCardID, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.comChargeType, 0);
            this.Controls.SetChildIndex(this.comCardStatus, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtOwnerName, 0);
            this.Controls.SetChildIndex(this.cmbCarStatus, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtCarPlate, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtCardCertificate, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.accessComboBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.chkExpireDateAfter, 0);
            this.Controls.SetChildIndex(this.chkExpireDateBefore, 0);
            this.Controls.SetChildIndex(this.dtExpireDateAfter, 0);
            this.Controls.SetChildIndex(this.dtExpireDateBefore, 0);
            this.Controls.SetChildIndex(this.cardView, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.AccessComboBox accessComboBox1;
        private System.Windows.Forms.Label label9;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardCertificate;
        private System.Windows.Forms.Label label8;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCarStatus;
        private GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label1;
        private UserControls.CardStatusComboBox comCardStatus;
        private UserControls.CarTypeComboBox comChargeType;
        private System.Windows.Forms.Label label6;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private UserControls.CardTypeComboBox comCardType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtExpireDateBefore;
        private System.Windows.Forms.DateTimePicker dtExpireDateAfter;
        private System.Windows.Forms.CheckBox chkExpireDateBefore;
        private System.Windows.Forms.CheckBox chkExpireDateAfter;
        private UserControls.CardGridView cardView;
    }
}