namespace ParkingDebugTool
{
    partial class FrmParkingDebugTool
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCard = new System.Windows.Forms.TabPage();
            this.btnWriteSection = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPaidFee = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.txtFee = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.dtPaidTime = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtEnterTime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.chkIn2Mark = new System.Windows.Forms.CheckBox();
            this.chkIn3 = new System.Windows.Forms.CheckBox();
            this.chkPaid1 = new System.Windows.Forms.CheckBox();
            this.chkIn2 = new System.Windows.Forms.CheckBox();
            this.chkIn1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkEnableWhenExpired = new System.Windows.Forms.CheckBox();
            this.chkCanEnterWhenFull = new System.Windows.Forms.CheckBox();
            this.chkRepeatIn = new System.Windows.Forms.CheckBox();
            this.chkWithCount = new System.Windows.Forms.CheckBox();
            this.chkRepeatOut = new System.Windows.Forms.CheckBox();
            this.chkHoliday = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCardVersion = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtAccessLevel = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtActivationDate = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBalance = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.dtValidDate = new System.Windows.Forms.DateTimePicker();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSection = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.chkWeigand34 = new System.Windows.Forms.CheckBox();
            this.txtKey = new Ralid.GeneralLibrary.WinformControl.HexTextBox(this.components);
            this.tabEntrance = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colSerialNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ucSection1 = new ParkingDebugTool.UCSection();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.comChargeType = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.eventReportListBox1 = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.chkOnlineHandleWhenOfflineMode = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabCard.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.tabEntrance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCard);
            this.tabControl1.Controls.Add(this.tabSetting);
            this.tabControl1.Controls.Add(this.tabEntrance);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 649);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabCard
            // 
            this.tabCard.Controls.Add(this.ucSection1);
            this.tabCard.Controls.Add(this.btnWriteSection);
            this.tabCard.Controls.Add(this.btnWrite);
            this.tabCard.Controls.Add(this.btnRead);
            this.tabCard.Controls.Add(this.groupBox2);
            this.tabCard.Controls.Add(this.groupBox3);
            this.tabCard.Controls.Add(this.groupBox1);
            this.tabCard.Location = new System.Drawing.Point(4, 22);
            this.tabCard.Name = "tabCard";
            this.tabCard.Padding = new System.Windows.Forms.Padding(3);
            this.tabCard.Size = new System.Drawing.Size(484, 623);
            this.tabCard.TabIndex = 0;
            this.tabCard.Text = "卡片信息";
            this.tabCard.UseVisualStyleBackColor = true;
            // 
            // btnWriteSection
            // 
            this.btnWriteSection.Location = new System.Drawing.Point(390, 58);
            this.btnWriteSection.Name = "btnWriteSection";
            this.btnWriteSection.Size = new System.Drawing.Size(75, 23);
            this.btnWriteSection.TabIndex = 6;
            this.btnWriteSection.Text = "写入扇区";
            this.btnWriteSection.UseVisualStyleBackColor = true;
            this.btnWriteSection.Click += new System.EventHandler(this.btnWriteSection_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWrite.Location = new System.Drawing.Point(390, 581);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 4;
            this.btnWrite.Text = "写入(&W)";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(276, 581);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 4;
            this.btnRead.Text = "读取(&R)";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtPaidFee);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtFee);
            this.groupBox2.Controls.Add(this.dtPaidTime);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.dtEnterTime);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.chkIn2Mark);
            this.groupBox2.Controls.Add(this.chkIn3);
            this.groupBox2.Controls.Add(this.chkPaid1);
            this.groupBox2.Controls.Add(this.chkIn2);
            this.groupBox2.Controls.Add(this.chkIn1);
            this.groupBox2.Location = new System.Drawing.Point(8, 430);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(468, 145);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "车场信息";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(242, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 35;
            this.label11.Text = "已缴费用:";
            // 
            // txtPaidFee
            // 
            this.txtPaidFee.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPaidFee.Location = new System.Drawing.Point(302, 105);
            this.txtPaidFee.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtPaidFee.MinValue = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.txtPaidFee.Name = "txtPaidFee";
            this.txtPaidFee.PointCount = 2;
            this.txtPaidFee.Size = new System.Drawing.Size(155, 21);
            this.txtPaidFee.TabIndex = 34;
            this.txtPaidFee.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(5, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 33;
            this.label10.Text = "停车费用:";
            // 
            // txtFee
            // 
            this.txtFee.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtFee.Location = new System.Drawing.Point(69, 105);
            this.txtFee.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtFee.MinValue = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.txtFee.Name = "txtFee";
            this.txtFee.PointCount = 2;
            this.txtFee.Size = new System.Drawing.Size(155, 21);
            this.txtFee.TabIndex = 32;
            this.txtFee.Text = "0.00";
            // 
            // dtPaidTime
            // 
            this.dtPaidTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtPaidTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPaidTime.Location = new System.Drawing.Point(302, 69);
            this.dtPaidTime.Name = "dtPaidTime";
            this.dtPaidTime.Size = new System.Drawing.Size(155, 21);
            this.dtPaidTime.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(242, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 31;
            this.label9.Text = "缴费时间:";
            // 
            // dtEnterTime
            // 
            this.dtEnterTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnterTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnterTime.Location = new System.Drawing.Point(69, 69);
            this.dtEnterTime.Name = "dtEnterTime";
            this.dtEnterTime.Size = new System.Drawing.Size(155, 21);
            this.dtEnterTime.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(6, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "入场时间:";
            // 
            // chkIn2Mark
            // 
            this.chkIn2Mark.AutoSize = true;
            this.chkIn2Mark.Location = new System.Drawing.Point(147, 42);
            this.chkIn2Mark.Name = "chkIn2Mark";
            this.chkIn2Mark.Size = new System.Drawing.Size(96, 16);
            this.chkIn2Mark.TabIndex = 0;
            this.chkIn2Mark.Text = "进入过内车场";
            this.chkIn2Mark.UseVisualStyleBackColor = true;
            // 
            // chkIn3
            // 
            this.chkIn3.AutoSize = true;
            this.chkIn3.Enabled = false;
            this.chkIn3.Location = new System.Drawing.Point(292, 20);
            this.chkIn3.Name = "chkIn3";
            this.chkIn3.Size = new System.Drawing.Size(102, 16);
            this.chkIn3.TabIndex = 0;
            this.chkIn3.Text = "已入第3层车场";
            this.chkIn3.UseVisualStyleBackColor = true;
            // 
            // chkPaid1
            // 
            this.chkPaid1.AutoSize = true;
            this.chkPaid1.Location = new System.Drawing.Point(12, 42);
            this.chkPaid1.Name = "chkPaid1";
            this.chkPaid1.Size = new System.Drawing.Size(96, 16);
            this.chkPaid1.TabIndex = 0;
            this.chkPaid1.Text = "外车场已缴费";
            this.chkPaid1.UseVisualStyleBackColor = true;
            // 
            // chkIn2
            // 
            this.chkIn2.AutoSize = true;
            this.chkIn2.Location = new System.Drawing.Point(147, 20);
            this.chkIn2.Name = "chkIn2";
            this.chkIn2.Size = new System.Drawing.Size(84, 16);
            this.chkIn2.TabIndex = 0;
            this.chkIn2.Text = "已入内车场";
            this.chkIn2.UseVisualStyleBackColor = true;
            // 
            // chkIn1
            // 
            this.chkIn1.AutoSize = true;
            this.chkIn1.Location = new System.Drawing.Point(12, 20);
            this.chkIn1.Name = "chkIn1";
            this.chkIn1.Size = new System.Drawing.Size(84, 16);
            this.chkIn1.TabIndex = 0;
            this.chkIn1.Text = "已入外车场";
            this.chkIn1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkOnlineHandleWhenOfflineMode);
            this.groupBox3.Controls.Add(this.chkEnableWhenExpired);
            this.groupBox3.Controls.Add(this.chkCanEnterWhenFull);
            this.groupBox3.Controls.Add(this.chkRepeatIn);
            this.groupBox3.Controls.Add(this.chkWithCount);
            this.groupBox3.Controls.Add(this.chkRepeatOut);
            this.groupBox3.Controls.Add(this.chkHoliday);
            this.groupBox3.Location = new System.Drawing.Point(8, 340);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(468, 84);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "属性";
            // 
            // chkEnableWhenExpired
            // 
            this.chkEnableWhenExpired.AutoSize = true;
            this.chkEnableWhenExpired.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkEnableWhenExpired.Location = new System.Drawing.Point(292, 38);
            this.chkEnableWhenExpired.Name = "chkEnableWhenExpired";
            this.chkEnableWhenExpired.Size = new System.Drawing.Size(156, 16);
            this.chkEnableWhenExpired.TabIndex = 5;
            this.chkEnableWhenExpired.Text = "卡片过期后仍允许进出场";
            this.chkEnableWhenExpired.UseVisualStyleBackColor = true;
            // 
            // chkCanEnterWhenFull
            // 
            this.chkCanEnterWhenFull.AutoSize = true;
            this.chkCanEnterWhenFull.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkCanEnterWhenFull.Location = new System.Drawing.Point(292, 16);
            this.chkCanEnterWhenFull.Name = "chkCanEnterWhenFull";
            this.chkCanEnterWhenFull.Size = new System.Drawing.Size(132, 16);
            this.chkCanEnterWhenFull.TabIndex = 4;
            this.chkCanEnterWhenFull.Text = "车场满位时允许入场";
            this.chkCanEnterWhenFull.UseVisualStyleBackColor = true;
            // 
            // chkRepeatIn
            // 
            this.chkRepeatIn.AutoSize = true;
            this.chkRepeatIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRepeatIn.Location = new System.Drawing.Point(12, 16);
            this.chkRepeatIn.Name = "chkRepeatIn";
            this.chkRepeatIn.Size = new System.Drawing.Size(96, 16);
            this.chkRepeatIn.TabIndex = 0;
            this.chkRepeatIn.Text = "允许重复入场";
            this.chkRepeatIn.UseVisualStyleBackColor = true;
            // 
            // chkWithCount
            // 
            this.chkWithCount.AutoSize = true;
            this.chkWithCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkWithCount.Location = new System.Drawing.Point(147, 38);
            this.chkWithCount.Name = "chkWithCount";
            this.chkWithCount.Size = new System.Drawing.Size(96, 16);
            this.chkWithCount.TabIndex = 3;
            this.chkWithCount.Text = "参加车位计数";
            this.chkWithCount.UseVisualStyleBackColor = true;
            // 
            // chkRepeatOut
            // 
            this.chkRepeatOut.AutoSize = true;
            this.chkRepeatOut.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkRepeatOut.Location = new System.Drawing.Point(12, 38);
            this.chkRepeatOut.Name = "chkRepeatOut";
            this.chkRepeatOut.Size = new System.Drawing.Size(96, 16);
            this.chkRepeatOut.TabIndex = 1;
            this.chkRepeatOut.Text = "允许重复出场";
            this.chkRepeatOut.UseVisualStyleBackColor = true;
            // 
            // chkHoliday
            // 
            this.chkHoliday.AutoSize = true;
            this.chkHoliday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkHoliday.Location = new System.Drawing.Point(147, 16);
            this.chkHoliday.Name = "chkHoliday";
            this.chkHoliday.Size = new System.Drawing.Size(108, 16);
            this.chkHoliday.TabIndex = 2;
            this.chkHoliday.Text = "节假日允许进出";
            this.chkHoliday.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comCardType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCardVersion);
            this.groupBox1.Controls.Add(this.txtAccessLevel);
            this.groupBox1.Controls.Add(this.txtCarPlate);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtActivationDate);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtBalance);
            this.groupBox1.Controls.Add(this.comChargeType);
            this.groupBox1.Controls.Add(this.dtValidDate);
            this.groupBox1.Controls.Add(this.txtCardID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(8, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(230, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "卡格式版本:";
            // 
            // txtCardVersion
            // 
            this.txtCardVersion.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardVersion.Location = new System.Drawing.Point(302, 14);
            this.txtCardVersion.MaxValue = 255;
            this.txtCardVersion.MinValue = 1;
            this.txtCardVersion.Name = "txtCardVersion";
            this.txtCardVersion.Size = new System.Drawing.Size(155, 21);
            this.txtCardVersion.TabIndex = 34;
            this.txtCardVersion.Text = "0";
            // 
            // txtAccessLevel
            // 
            this.txtAccessLevel.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAccessLevel.Location = new System.Drawing.Point(70, 70);
            this.txtAccessLevel.MaxValue = 255;
            this.txtAccessLevel.MinValue = 0;
            this.txtAccessLevel.Name = "txtAccessLevel";
            this.txtAccessLevel.Size = new System.Drawing.Size(155, 21);
            this.txtAccessLevel.TabIndex = 33;
            this.txtAccessLevel.Text = "0";
            // 
            // txtCarPlate
            // 
            this.txtCarPlate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCarPlate.Location = new System.Drawing.Point(302, 70);
            this.txtCarPlate.MaxLength = 30;
            this.txtCarPlate.Name = "txtCarPlate";
            this.txtCarPlate.Size = new System.Drawing.Size(155, 21);
            this.txtCarPlate.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(242, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 32;
            this.label12.Text = "车牌号码:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(6, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "储值余额:";
            // 
            // dtActivationDate
            // 
            this.dtActivationDate.CustomFormat = "yyyy-MM-dd";
            this.dtActivationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtActivationDate.Location = new System.Drawing.Point(70, 99);
            this.dtActivationDate.Name = "dtActivationDate";
            this.dtActivationDate.Size = new System.Drawing.Size(155, 21);
            this.dtActivationDate.TabIndex = 23;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(5, 103);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 12);
            this.label16.TabIndex = 29;
            this.label16.Text = "生效日期:";
            // 
            // txtBalance
            // 
            this.txtBalance.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtBalance.Location = new System.Drawing.Point(70, 128);
            this.txtBalance.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtBalance.MinValue = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.PointCount = 2;
            this.txtBalance.Size = new System.Drawing.Size(155, 21);
            this.txtBalance.TabIndex = 26;
            this.txtBalance.Text = "0.00";
            // 
            // dtValidDate
            // 
            this.dtValidDate.CustomFormat = "yyyy-MM-dd";
            this.dtValidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtValidDate.Location = new System.Drawing.Point(302, 97);
            this.dtValidDate.Name = "dtValidDate";
            this.dtValidDate.Size = new System.Drawing.Size(155, 21);
            this.dtValidDate.TabIndex = 25;
            // 
            // txtCardID
            // 
            this.txtCardID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardID.Location = new System.Drawing.Point(69, 14);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.ReadOnly = true;
            this.txtCardID.Size = new System.Drawing.Size(155, 21);
            this.txtCardID.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(242, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "有效期限:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(29, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "卡号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(17, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "卡类型:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(242, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "收费车型:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(5, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "通行权限:";
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.eventReportListBox1);
            this.tabSetting.Controls.Add(this.button1);
            this.tabSetting.Controls.Add(this.txtSection);
            this.tabSetting.Controls.Add(this.label14);
            this.tabSetting.Controls.Add(this.btnSave);
            this.tabSetting.Controls.Add(this.label13);
            this.tabSetting.Controls.Add(this.chkWeigand34);
            this.tabSetting.Controls.Add(this.txtKey);
            this.tabSetting.Location = new System.Drawing.Point(4, 22);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(484, 610);
            this.tabSetting.TabIndex = 1;
            this.tabSetting.Text = "读卡器设置";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(241, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSection
            // 
            this.txtSection.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSection.Location = new System.Drawing.Point(70, 61);
            this.txtSection.MaxLength = 2;
            this.txtSection.MaxValue = 15;
            this.txtSection.MinValue = 0;
            this.txtSection.Name = "txtSection";
            this.txtSection.Size = new System.Drawing.Size(27, 21);
            this.txtSection.TabIndex = 9;
            this.txtSection.Text = "2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 8;
            this.label14.Text = "扇区：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(382, 565);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(115, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "密钥：";
            // 
            // chkWeigand34
            // 
            this.chkWeigand34.AutoSize = true;
            this.chkWeigand34.Checked = true;
            this.chkWeigand34.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeigand34.Location = new System.Drawing.Point(25, 21);
            this.chkWeigand34.Name = "chkWeigand34";
            this.chkWeigand34.Size = new System.Drawing.Size(84, 16);
            this.chkWeigand34.TabIndex = 0;
            this.chkWeigand34.Text = "启用韦根34";
            this.chkWeigand34.UseVisualStyleBackColor = true;
            // 
            // txtKey
            // 
            this.txtKey.InputSpace = true;
            this.txtKey.Location = new System.Drawing.Point(162, 61);
            this.txtKey.MaxLength = 17;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(121, 21);
            this.txtKey.TabIndex = 6;
            // 
            // tabEntrance
            // 
            this.tabEntrance.Controls.Add(this.btnSearch);
            this.tabEntrance.Controls.Add(this.grid);
            this.tabEntrance.Location = new System.Drawing.Point(4, 22);
            this.tabEntrance.Name = "tabEntrance";
            this.tabEntrance.Size = new System.Drawing.Size(484, 610);
            this.tabEntrance.TabIndex = 2;
            this.tabEntrance.Text = "控制板信息";
            this.tabEntrance.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(363, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 23);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSerialNum,
            this.colIP,
            this.colMac});
            this.grid.Location = new System.Drawing.Point(8, 67);
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 23;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(468, 544);
            this.grid.TabIndex = 20;
            // 
            // colSerialNum
            // 
            this.colSerialNum.HeaderText = "序列号";
            this.colSerialNum.Name = "colSerialNum";
            this.colSerialNum.ReadOnly = true;
            this.colSerialNum.Width = 120;
            // 
            // colIP
            // 
            this.colIP.HeaderText = "IP";
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            this.colIP.Width = 120;
            // 
            // colMac
            // 
            this.colMac.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMac.HeaderText = "MAC";
            this.colMac.Name = "colMac";
            this.colMac.ReadOnly = true;
            // 
            // ucSection1
            // 
            this.ucSection1.Location = new System.Drawing.Point(8, 6);
            this.ucSection1.Name = "ucSection1";
            this.ucSection1.Section = 2;
            this.ucSection1.Size = new System.Drawing.Size(375, 158);
            this.ucSection1.TabIndex = 7;
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Location = new System.Drawing.Point(69, 44);
            this.comCardType.Name = "comCardType";
            this.comCardType.Size = new System.Drawing.Size(155, 20);
            this.comCardType.TabIndex = 36;
            // 
            // comChargeType
            // 
            this.comChargeType.FormattingEnabled = true;
            this.comChargeType.Location = new System.Drawing.Point(302, 44);
            this.comChargeType.Name = "comChargeType";
            this.comChargeType.Size = new System.Drawing.Size(155, 20);
            this.comChargeType.TabIndex = 19;
            // 
            // eventReportListBox1
            // 
            this.eventReportListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventReportListBox1.FormattingEnabled = true;
            this.eventReportListBox1.ItemHeight = 12;
            this.eventReportListBox1.Location = new System.Drawing.Point(25, 141);
            this.eventReportListBox1.Name = "eventReportListBox1";
            this.eventReportListBox1.Size = new System.Drawing.Size(120, 88);
            this.eventReportListBox1.TabIndex = 12;
            // 
            // chkOnlineHandleWhenOfflineMode
            // 
            this.chkOnlineHandleWhenOfflineMode.AutoSize = true;
            this.chkOnlineHandleWhenOfflineMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkOnlineHandleWhenOfflineMode.Location = new System.Drawing.Point(12, 60);
            this.chkOnlineHandleWhenOfflineMode.Name = "chkOnlineHandleWhenOfflineMode";
            this.chkOnlineHandleWhenOfflineMode.Size = new System.Drawing.Size(168, 16);
            this.chkOnlineHandleWhenOfflineMode.TabIndex = 6;
            this.chkOnlineHandleWhenOfflineMode.Text = "脱机模式时按在线模式处理";
            this.chkOnlineHandleWhenOfflineMode.UseVisualStyleBackColor = true;
            // 
            // FrmParkingDebugTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 649);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmParkingDebugTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "写卡停车场调试工具";
            this.Load += new System.EventHandler(this.FrmParkingDebugTool_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabCard.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabSetting.ResumeLayout(false);
            this.tabSetting.PerformLayout();
            this.tabEntrance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCard;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtActivationDate;
        private System.Windows.Forms.Label label16;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtBalance;
        private Ralid.Park.UserControls.CarTypeComboBox comChargeType;
        private System.Windows.Forms.DateTimePicker dtValidDate;
        public Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkEnableWhenExpired;
        private System.Windows.Forms.CheckBox chkCanEnterWhenFull;
        private System.Windows.Forms.CheckBox chkRepeatIn;
        private System.Windows.Forms.CheckBox chkWithCount;
        private System.Windows.Forms.CheckBox chkRepeatOut;
        private System.Windows.Forms.CheckBox chkHoliday;
        public Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtAccessLevel;
        private System.Windows.Forms.Label label3;
        public Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtCardVersion;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtPaidFee;
        private System.Windows.Forms.Label label10;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtFee;
        private System.Windows.Forms.DateTimePicker dtPaidTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtEnterTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkIn2Mark;
        private System.Windows.Forms.CheckBox chkIn3;
        private System.Windows.Forms.CheckBox chkPaid1;
        private System.Windows.Forms.CheckBox chkIn2;
        private System.Windows.Forms.CheckBox chkIn1;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private System.Windows.Forms.CheckBox chkWeigand34;
        private System.Windows.Forms.TabPage tabEntrance;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerialNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMac;
        private System.Windows.Forms.Button btnSearch;
        private Ralid.GeneralLibrary.WinformControl.HexTextBox txtKey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSave;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtSection;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnWriteSection;
        private UCSection ucSection1;
        private System.Windows.Forms.Button button1;
        private Ralid.Park.UserControls.EventReportListBox eventReportListBox1;
        private System.Windows.Forms.CheckBox chkOnlineHandleWhenOfflineMode;
    }
}

