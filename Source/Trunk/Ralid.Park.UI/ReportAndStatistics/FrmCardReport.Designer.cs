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
            this.btnSearch.Location = new System.Drawing.Point(669, 13);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(669, 44);
            // 
            // accessComboBox1
            // 
            this.accessComboBox1.FormattingEnabled = true;
            this.accessComboBox1.Location = new System.Drawing.Point(52, 80);
            this.accessComboBox1.Name = "accessComboBox1";
            this.accessComboBox1.Size = new System.Drawing.Size(96, 20);
            this.accessComboBox1.TabIndex = 54;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(6, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 53;
            this.label9.Text = "权限组:";
            // 
            // txtCardCertificate
            // 
            this.txtCardCertificate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardCertificate.Location = new System.Drawing.Point(523, 13);
            this.txtCardCertificate.Name = "txtCardCertificate";
            this.txtCardCertificate.Size = new System.Drawing.Size(94, 21);
            this.txtCardCertificate.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(461, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 52;
            this.label8.Text = "卡片编号:";
            // 
            // txtCarPlate
            // 
            this.txtCarPlate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCarPlate.Location = new System.Drawing.Point(358, 13);
            this.txtCarPlate.Name = "txtCarPlate";
            this.txtCarPlate.Size = new System.Drawing.Size(94, 21);
            this.txtCarPlate.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(305, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 51;
            this.label4.Text = "车牌号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(461, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 48;
            this.label3.Text = "停车状态:";
            // 
            // cmbCarStatus
            // 
            this.cmbCarStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarStatus.FormattingEnabled = true;
            this.cmbCarStatus.Items.AddRange(new object[] {
            "所有",
            "在场",
            "不在场"});
            this.cmbCarStatus.Location = new System.Drawing.Point(523, 47);
            this.cmbCarStatus.Name = "cmbCarStatus";
            this.cmbCarStatus.Size = new System.Drawing.Size(96, 20);
            this.cmbCarStatus.TabIndex = 45;
            // 
            // txtOwnerName
            // 
            this.txtOwnerName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtOwnerName.Location = new System.Drawing.Point(200, 13);
            this.txtOwnerName.Name = "txtOwnerName";
            this.txtOwnerName.Size = new System.Drawing.Size(94, 21);
            this.txtOwnerName.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(153, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "持卡人:";
            // 
            // comCardStatus
            // 
            this.comCardStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardStatus.FormattingEnabled = true;
            this.comCardStatus.Location = new System.Drawing.Point(358, 47);
            this.comCardStatus.Name = "comCardStatus";
            this.comCardStatus.Size = new System.Drawing.Size(96, 20);
            this.comCardStatus.TabIndex = 42;
            // 
            // comChargeType
            // 
            this.comChargeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comChargeType.FormattingEnabled = true;
            this.comChargeType.Location = new System.Drawing.Point(52, 47);
            this.comChargeType.Name = "comChargeType";
            this.comChargeType.Size = new System.Drawing.Size(96, 20);
            this.comChargeType.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(305, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "卡状态:";
            // 
            // txtCardID
            // 
            this.txtCardID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardID.Location = new System.Drawing.Point(52, 13);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(96, 21);
            this.txtCardID.TabIndex = 37;
            // 
            // comCardType
            // 
            this.comCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Location = new System.Drawing.Point(200, 47);
            this.comCardType.Name = "comCardType";
            this.comCardType.Size = new System.Drawing.Size(96, 20);
            this.comCardType.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(18, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "车型:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(153, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "卡类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(18, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 55;
            this.label2.Text = "卡号:";
            // 
            // dtExpireDateBefore
            // 
            this.dtExpireDateBefore.CustomFormat = "yyyy-MM-dd";
            this.dtExpireDateBefore.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtExpireDateBefore.Location = new System.Drawing.Point(557, 80);
            this.dtExpireDateBefore.Name = "dtExpireDateBefore";
            this.dtExpireDateBefore.Size = new System.Drawing.Size(99, 21);
            this.dtExpireDateBefore.TabIndex = 59;
            // 
            // dtExpireDateAfter
            // 
            this.dtExpireDateAfter.CustomFormat = "yyyy-MM-dd";
            this.dtExpireDateAfter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtExpireDateAfter.Location = new System.Drawing.Point(296, 80);
            this.dtExpireDateAfter.Name = "dtExpireDateAfter";
            this.dtExpireDateAfter.Size = new System.Drawing.Size(99, 21);
            this.dtExpireDateAfter.TabIndex = 58;
            // 
            // chkExpireDateBefore
            // 
            this.chkExpireDateBefore.AutoSize = true;
            this.chkExpireDateBefore.Location = new System.Drawing.Point(417, 82);
            this.chkExpireDateBefore.Name = "chkExpireDateBefore";
            this.chkExpireDateBefore.Size = new System.Drawing.Size(144, 16);
            this.chkExpireDateBefore.TabIndex = 57;
            this.chkExpireDateBefore.Text = "卡片有效期小于或等于";
            this.chkExpireDateBefore.UseVisualStyleBackColor = true;
            // 
            // chkExpireDateAfter
            // 
            this.chkExpireDateAfter.AutoSize = true;
            this.chkExpireDateAfter.Location = new System.Drawing.Point(156, 82);
            this.chkExpireDateAfter.Name = "chkExpireDateAfter";
            this.chkExpireDateAfter.Size = new System.Drawing.Size(144, 16);
            this.chkExpireDateAfter.TabIndex = 56;
            this.chkExpireDateAfter.Text = "卡片有效期大于或等于";
            this.chkExpireDateAfter.UseVisualStyleBackColor = true;
            // 
            // cardView
            // 
            this.cardView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cardView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardView.Location = new System.Drawing.Point(8, 106);
            this.cardView.Name = "cardView";
            this.cardView.RowTemplate.Height = 23;
            this.cardView.Size = new System.Drawing.Size(786, 236);
            this.cardView.TabIndex = 60;
            // 
            // FrmCardReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 367);
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
            this.Text = "卡片查询";
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