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
            this.txtTelphone = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtMemo = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.dtActivationDate = new System.Windows.Forms.DateTimePicker();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtValidDate = new System.Windows.Forms.DateTimePicker();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkEnableLimitation = new System.Windows.Forms.CheckBox();
            this.chkOnlineHandleWhenOfflineMode = new System.Windows.Forms.CheckBox();
            this.chkEnableWhenExpired = new System.Windows.Forms.CheckBox();
            this.chkCanEnterWhenFull = new System.Windows.Forms.CheckBox();
            this.chkRepeatIn = new System.Windows.Forms.CheckBox();
            this.chkWithCount = new System.Windows.Forms.CheckBox();
            this.chkRepeatOut = new System.Windows.Forms.CheckBox();
            this.chkHoliday = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLimitationRemain = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.comAccessLevel = new Ralid.Park.UserControls.AccessComboBox(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLimitationRemain);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTelphone);
            this.groupBox2.Controls.Add(this.txtCertificate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtMemo);
            this.groupBox2.Controls.Add(this.dtActivationDate);
            this.groupBox2.Controls.Add(this.txtCarPlate);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtOwnerName);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.comCardType);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.comAccessLevel);
            this.groupBox2.Controls.Add(this.dtValidDate);
            this.groupBox2.Controls.Add(this.txtCardID);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label7);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtTelphone
            // 
            resources.ApplyResources(this.txtTelphone, "txtTelphone");
            this.txtTelphone.Name = "txtTelphone";
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
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label14.Name = "label14";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label12.Name = "label12";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label10.Name = "label10";
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
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label7.Name = "label7";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkEnableLimitation);
            this.groupBox3.Controls.Add(this.chkOnlineHandleWhenOfflineMode);
            this.groupBox3.Controls.Add(this.chkEnableWhenExpired);
            this.groupBox3.Controls.Add(this.chkCanEnterWhenFull);
            this.groupBox3.Controls.Add(this.chkRepeatIn);
            this.groupBox3.Controls.Add(this.chkWithCount);
            this.groupBox3.Controls.Add(this.chkRepeatOut);
            this.groupBox3.Controls.Add(this.chkHoliday);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // chkEnableLimitation
            // 
            resources.ApplyResources(this.chkEnableLimitation, "chkEnableLimitation");
            this.chkEnableLimitation.Checked = true;
            this.chkEnableLimitation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableLimitation.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.chkEnableLimitation.Name = "chkEnableLimitation";
            this.chkEnableLimitation.UseVisualStyleBackColor = true;
            this.chkEnableLimitation.CheckedChanged += new System.EventHandler(this.chkEnableLimitation_CheckedChanged);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label3.Name = "label3";
            // 
            // txtLimitationRemain
            // 
            resources.ApplyResources(this.txtLimitationRemain, "txtLimitationRemain");
            this.txtLimitationRemain.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtLimitationRemain.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLimitationRemain.Name = "txtLimitationRemain";
            this.txtLimitationRemain.PointCount = 2;
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.Name = "comCardType";
            // 
            // comAccessLevel
            // 
            this.comAccessLevel.FormattingEnabled = true;
            resources.ApplyResources(this.comAccessLevel, "comAccessLevel");
            this.comAccessLevel.Name = "comAccessLevel";
            // 
            // UCCard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "UCCard";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private AccessComboBox comAccessLevel;
        private System.Windows.Forms.DateTimePicker dtValidDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtMemo;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private CardTypeComboBox comCardType;
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
        private GeneralLibrary.WinformControl.DBCTextBox txtTelphone;
        private System.Windows.Forms.CheckBox chkEnableLimitation;
        private System.Windows.Forms.Label label3;
        private GeneralLibrary.WinformControl.DecimalTextBox txtLimitationRemain;

    }
}
