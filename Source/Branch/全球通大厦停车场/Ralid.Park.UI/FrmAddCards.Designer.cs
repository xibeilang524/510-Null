namespace Ralid.Park.UI
{
    partial class FrmAddCards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddCards));
            this.dtValidDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtBalance = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox();
            this.comAccessLevel = new Ralid.Park.UserControls.AccessComboBox();
            this.comChargeType = new Ralid.Park.UserControls.CarTypeComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkWriteCard = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSuffix = new Ralid.GeneralLibrary.WinformControl.DBCTextBox();
            this.txtAutoIncrement = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox();
            this.chkAutoIncrement = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPrefix = new Ralid.GeneralLibrary.WinformControl.DBCTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkOnlineHandleWhenOfflineMode = new System.Windows.Forms.CheckBox();
            this.chkEnableWhenExpired = new System.Windows.Forms.CheckBox();
            this.chkCanEnterWhenFull = new System.Windows.Forms.CheckBox();
            this.chkRepeatIn = new System.Windows.Forms.CheckBox();
            this.chkWithCount = new System.Windows.Forms.CheckBox();
            this.chkRepeatOut = new System.Windows.Forms.CheckBox();
            this.chkHoliday = new System.Windows.Forms.CheckBox();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblAlarm = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cardView = new Ralid.Park.UserControls.CardGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtValidDate
            // 
            resources.ApplyResources(this.dtValidDate, "dtValidDate");
            this.dtValidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtValidDate.Name = "dtValidDate";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label6.Name = "label6";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label7.Name = "label7";
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtBalance
            // 
            resources.ApplyResources(this.txtBalance, "txtBalance");
            this.txtBalance.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtBalance.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.PointCount = 2;
            // 
            // comAccessLevel
            // 
            resources.ApplyResources(this.comAccessLevel, "comAccessLevel");
            this.comAccessLevel.FormattingEnabled = true;
            this.comAccessLevel.Name = "comAccessLevel";
            // 
            // comChargeType
            // 
            resources.ApplyResources(this.comChargeType, "comChargeType");
            this.comChargeType.FormattingEnabled = true;
            this.comChargeType.Name = "comChargeType";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.ToolTipText = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label8.Name = "label8";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label1.Name = "label1";
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage1.Controls.Add(this.chkWriteCard);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.txtSuffix);
            this.tabPage1.Controls.Add(this.txtAutoIncrement);
            this.tabPage1.Controls.Add(this.chkAutoIncrement);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.txtPrefix);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.comCardType);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.comAccessLevel);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.comChargeType);
            this.tabPage1.Controls.Add(this.dtValidDate);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtBalance);
            this.tabPage1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.ToolTipText = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // chkWriteCard
            // 
            resources.ApplyResources(this.chkWriteCard, "chkWriteCard");
            this.chkWriteCard.Name = "chkWriteCard";
            this.chkWriteCard.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label10.Name = "label10";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label15.Name = "label15";
            // 
            // txtSuffix
            // 
            resources.ApplyResources(this.txtSuffix, "txtSuffix");
            this.txtSuffix.Name = "txtSuffix";
            // 
            // txtAutoIncrement
            // 
            resources.ApplyResources(this.txtAutoIncrement, "txtAutoIncrement");
            this.txtAutoIncrement.MaxValue = 100000000;
            this.txtAutoIncrement.MinValue = 0;
            this.txtAutoIncrement.Name = "txtAutoIncrement";
            // 
            // chkAutoIncrement
            // 
            resources.ApplyResources(this.chkAutoIncrement, "chkAutoIncrement");
            this.chkAutoIncrement.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkAutoIncrement.Name = "chkAutoIncrement";
            this.chkAutoIncrement.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label16.Name = "label16";
            // 
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.chkOnlineHandleWhenOfflineMode);
            this.groupBox3.Controls.Add(this.chkEnableWhenExpired);
            this.groupBox3.Controls.Add(this.chkCanEnterWhenFull);
            this.groupBox3.Controls.Add(this.chkRepeatIn);
            this.groupBox3.Controls.Add(this.chkWithCount);
            this.groupBox3.Controls.Add(this.chkRepeatOut);
            this.groupBox3.Controls.Add(this.chkHoliday);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // chkOnlineHandleWhenOfflineMode
            // 
            resources.ApplyResources(this.chkOnlineHandleWhenOfflineMode, "chkOnlineHandleWhenOfflineMode");
            this.chkOnlineHandleWhenOfflineMode.Name = "chkOnlineHandleWhenOfflineMode";
            this.chkOnlineHandleWhenOfflineMode.UseVisualStyleBackColor = true;
            // 
            // chkEnableWhenExpired
            // 
            resources.ApplyResources(this.chkEnableWhenExpired, "chkEnableWhenExpired");
            this.chkEnableWhenExpired.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkEnableWhenExpired.Name = "chkEnableWhenExpired";
            this.chkEnableWhenExpired.UseVisualStyleBackColor = true;
            // 
            // chkCanEnterWhenFull
            // 
            resources.ApplyResources(this.chkCanEnterWhenFull, "chkCanEnterWhenFull");
            this.chkCanEnterWhenFull.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkCanEnterWhenFull.Name = "chkCanEnterWhenFull";
            this.chkCanEnterWhenFull.UseVisualStyleBackColor = true;
            // 
            // chkRepeatIn
            // 
            resources.ApplyResources(this.chkRepeatIn, "chkRepeatIn");
            this.chkRepeatIn.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkRepeatIn.Name = "chkRepeatIn";
            this.chkRepeatIn.UseVisualStyleBackColor = true;
            // 
            // chkWithCount
            // 
            resources.ApplyResources(this.chkWithCount, "chkWithCount");
            this.chkWithCount.Checked = true;
            this.chkWithCount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWithCount.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkWithCount.Name = "chkWithCount";
            this.chkWithCount.UseVisualStyleBackColor = true;
            // 
            // chkRepeatOut
            // 
            resources.ApplyResources(this.chkRepeatOut, "chkRepeatOut");
            this.chkRepeatOut.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkRepeatOut.Name = "chkRepeatOut";
            this.chkRepeatOut.UseVisualStyleBackColor = true;
            // 
            // chkHoliday
            // 
            resources.ApplyResources(this.chkHoliday, "chkHoliday");
            this.chkHoliday.Checked = true;
            this.chkHoliday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHoliday.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkHoliday.Name = "chkHoliday";
            this.chkHoliday.UseVisualStyleBackColor = true;
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label9.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label9.Name = "label9";
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage2.Controls.Add(this.lblAlarm);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.cardView);
            this.tabPage2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.ToolTipText = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // lblAlarm
            // 
            resources.ApplyResources(this.lblAlarm, "lblAlarm");
            this.lblAlarm.ForeColor = System.Drawing.Color.Blue;
            this.lblAlarm.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.lblAlarm.Name = "lblAlarm";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::Ralid.Park.UI.Properties.Resources.CardReader;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label13.Name = "label13";
            // 
            // cardView
            // 
            resources.ApplyResources(this.cardView, "cardView");
            this.cardView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardView.Name = "cardView";
            this.cardView.RowTemplate.Height = 23;
            // 
            // tabPage4
            // 
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage4.Controls.Add(this.lblDownloadStatus);
            this.tabPage4.Controls.Add(this.lblStatus);
            this.tabPage4.Controls.Add(this.progressBar1);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.ToolTipText = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            // 
            // lblDownloadStatus
            // 
            resources.ApplyResources(this.lblDownloadStatus, "lblDownloadStatus");
            this.lblDownloadStatus.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.lblStatus.Name = "lblStatus";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label14.Name = "label14";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label11.Name = "label11";
            // 
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // FrmAddCards
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddCards";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAddCards_FormClosing);
            this.Load += new System.EventHandler(this.FrmAddCards_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Ralid.GeneralLibrary .WinformControl .DecimalTextBox txtBalance;
        private Ralid.Park.UserControls.AccessComboBox comAccessLevel;
        private Ralid.Park.UserControls.CarTypeComboBox comChargeType;
        private System.Windows.Forms.DateTimePicker dtValidDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkEnableWhenExpired;
        private System.Windows.Forms.CheckBox chkCanEnterWhenFull;
        private System.Windows.Forms.CheckBox chkRepeatIn;
        private System.Windows.Forms.CheckBox chkWithCount;
        private System.Windows.Forms.CheckBox chkRepeatOut;
        private System.Windows.Forms.CheckBox chkHoliday;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtSuffix;
        private GeneralLibrary.WinformControl.IntergerTextBox txtAutoIncrement;
        private System.Windows.Forms.CheckBox chkAutoIncrement;
        private System.Windows.Forms.Label label16;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPrefix;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label13;
        private UserControls.CardGridView cardView;
        private System.Windows.Forms.Label lblDownloadStatus;
        private System.Windows.Forms.CheckBox chkWriteCard;
        private System.Windows.Forms.CheckBox chkOnlineHandleWhenOfflineMode;
    }
}