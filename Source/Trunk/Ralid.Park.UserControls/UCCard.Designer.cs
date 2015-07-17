namespace Ralid.Park.UserControls
{
    partial class UCCard
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCard));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkInPark = new System.Windows.Forms.CheckBox();
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtDepartment = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtMemo = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.dtActivationDate = new System.Windows.Forms.DateTimePicker();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txtDeposit = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.txtBalance = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.comAccessLevel = new Ralid.Park.UserControls.AccessComboBox(this.components);
            this.comChargeType = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.dtValidDate = new System.Windows.Forms.DateTimePicker();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkOnlineHandleWhenOfflineMode = new System.Windows.Forms.CheckBox();
            this.chkEnableWhenExpired = new System.Windows.Forms.CheckBox();
            this.chkCanEnterWhenFull = new System.Windows.Forms.CheckBox();
            this.chkRepeatIn = new System.Windows.Forms.CheckBox();
            this.chkWithCount = new System.Windows.Forms.CheckBox();
            this.chkRepeatOut = new System.Windows.Forms.CheckBox();
            this.chkHoliday = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbCarPlateList = new System.Windows.Forms.RadioButton();
            this.rdbCardList = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.chkInPark);
            this.groupBox2.Controls.Add(this.txtCertificate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDepartment);
            this.groupBox2.Controls.Add(this.txtMemo);
            this.groupBox2.Controls.Add(this.dtActivationDate);
            this.groupBox2.Controls.Add(this.txtCarPlate);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtOwnerName);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDeposit);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.comCardType);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtBalance);
            this.groupBox2.Controls.Add(this.comAccessLevel);
            this.groupBox2.Controls.Add(this.comChargeType);
            this.groupBox2.Controls.Add(this.dtValidDate);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // chkInPark
            // 
            resources.ApplyResources(this.chkInPark, "chkInPark");
            this.chkInPark.Name = "chkInPark";
            this.chkInPark.UseVisualStyleBackColor = true;
            // 
            // txtCertificate
            // 
            resources.ApplyResources(this.txtCertificate, "txtCertificate");
            this.txtCertificate.Name = "txtCertificate";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtDepartment
            // 
            resources.ApplyResources(this.txtDepartment, "txtDepartment");
            this.txtDepartment.Name = "txtDepartment";
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.Name = "txtMemo";
            // 
            // dtActivationDate
            // 
            resources.ApplyResources(this.dtActivationDate, "dtActivationDate");
            this.dtActivationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtActivationDate.Name = "dtActivationDate";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label16.Name = "label16";
            // 
            // txtOwnerName
            // 
            resources.ApplyResources(this.txtOwnerName, "txtOwnerName");
            this.txtOwnerName.Name = "txtOwnerName";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label9.Name = "label9";
            // 
            // txtDeposit
            // 
            resources.ApplyResources(this.txtDeposit, "txtDeposit");
            this.txtDeposit.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtDeposit.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDeposit.Name = "txtDeposit";
            this.txtDeposit.NumberWithCommas = true;
            this.txtDeposit.PointCount = 2;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label14.Name = "label14";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label3.Name = "label3";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label12.Name = "label12";
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label10.Name = "label10";
            // 
            // txtBalance
            // 
            resources.ApplyResources(this.txtBalance, "txtBalance");
            this.txtBalance.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtBalance.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.NumberWithCommas = true;
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
            // dtValidDate
            // 
            resources.ApplyResources(this.dtValidDate, "dtValidDate");
            this.dtValidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtValidDate.Name = "dtValidDate";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label6.Name = "label6";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label7.Name = "label7";
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
            this.chkOnlineHandleWhenOfflineMode.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkOnlineHandleWhenOfflineMode.Name = "chkOnlineHandleWhenOfflineMode";
            this.chkOnlineHandleWhenOfflineMode.UseVisualStyleBackColor = true;
            // 
            // chkEnableWhenExpired
            // 
            resources.ApplyResources(this.chkEnableWhenExpired, "chkEnableWhenExpired");
            this.chkEnableWhenExpired.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkEnableWhenExpired.Name = "chkEnableWhenExpired";
            this.chkEnableWhenExpired.UseVisualStyleBackColor = true;
            // 
            // chkCanEnterWhenFull
            // 
            resources.ApplyResources(this.chkCanEnterWhenFull, "chkCanEnterWhenFull");
            this.chkCanEnterWhenFull.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkCanEnterWhenFull.Name = "chkCanEnterWhenFull";
            this.chkCanEnterWhenFull.UseVisualStyleBackColor = true;
            // 
            // chkRepeatIn
            // 
            resources.ApplyResources(this.chkRepeatIn, "chkRepeatIn");
            this.chkRepeatIn.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkRepeatIn.Name = "chkRepeatIn";
            this.chkRepeatIn.UseVisualStyleBackColor = true;
            // 
            // chkWithCount
            // 
            resources.ApplyResources(this.chkWithCount, "chkWithCount");
            this.chkWithCount.Checked = true;
            this.chkWithCount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWithCount.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkWithCount.Name = "chkWithCount";
            this.chkWithCount.UseVisualStyleBackColor = true;
            // 
            // chkRepeatOut
            // 
            resources.ApplyResources(this.chkRepeatOut, "chkRepeatOut");
            this.chkRepeatOut.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkRepeatOut.Name = "chkRepeatOut";
            this.chkRepeatOut.UseVisualStyleBackColor = true;
            // 
            // chkHoliday
            // 
            resources.ApplyResources(this.chkHoliday, "chkHoliday");
            this.chkHoliday.Checked = true;
            this.chkHoliday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHoliday.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkHoliday.Name = "chkHoliday";
            this.chkHoliday.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.rdbCarPlateList);
            this.groupBox1.Controls.Add(this.rdbCardList);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // rdbCarPlateList
            // 
            resources.ApplyResources(this.rdbCarPlateList, "rdbCarPlateList");
            this.rdbCarPlateList.Name = "rdbCarPlateList";
            this.rdbCarPlateList.TabStop = true;
            this.rdbCarPlateList.UseVisualStyleBackColor = true;
            // 
            // rdbCardList
            // 
            resources.ApplyResources(this.rdbCardList, "rdbCardList");
            this.rdbCardList.Checked = true;
            this.rdbCardList.Name = "rdbCardList";
            this.rdbCardList.TabStop = true;
            this.rdbCardList.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // UCCard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "UCCard";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private AccessComboBox comAccessLevel;
        private CarTypeComboBox comChargeType;
        private System.Windows.Forms.DateTimePicker dtValidDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtMemo;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtBalance;
        private CardTypeComboBox comCardType;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtDeposit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtActivationDate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkRepeatIn;
        private System.Windows.Forms.CheckBox chkWithCount;
        private System.Windows.Forms.CheckBox chkRepeatOut;
        private System.Windows.Forms.CheckBox chkHoliday;
        private System.Windows.Forms.CheckBox chkEnableWhenExpired;
        private System.Windows.Forms.CheckBox chkCanEnterWhenFull;
        private GeneralLibrary.WinformControl.DBCTextBox txtCertificate;
        private System.Windows.Forms.Label label8;
        public GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.CheckBox chkOnlineHandleWhenOfflineMode;
        private System.Windows.Forms.CheckBox chkInPark;
        private GeneralLibrary.WinformControl.DBCTextBox txtDepartment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        public GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        public System.Windows.Forms.RadioButton rdbCarPlateList;
        public System.Windows.Forms.RadioButton rdbCardList;

    }
}
