namespace YCTTest
{
    partial class Form1
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnReadCurCard = new System.Windows.Forms.Button();
            this.btnReduceBalance = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBalance = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txtAmount = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.comPort = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhysicalID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtLogicID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCardType = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDeposit = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txtCount = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtVersion = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.txtSN = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(270, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(129, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnReadCurCard
            // 
            this.btnReadCurCard.Enabled = false;
            this.btnReadCurCard.Location = new System.Drawing.Point(207, 252);
            this.btnReadCurCard.Name = "btnReadCurCard";
            this.btnReadCurCard.Size = new System.Drawing.Size(129, 36);
            this.btnReadCurCard.TabIndex = 1;
            this.btnReadCurCard.Text = "读  卡";
            this.btnReadCurCard.UseVisualStyleBackColor = true;
            this.btnReadCurCard.Click += new System.EventHandler(this.btnReadCurCard_Click);
            // 
            // btnReduceBalance
            // 
            this.btnReduceBalance.Enabled = false;
            this.btnReduceBalance.Location = new System.Drawing.Point(278, 324);
            this.btnReduceBalance.Name = "btnReduceBalance";
            this.btnReduceBalance.Size = new System.Drawing.Size(129, 36);
            this.btnReduceBalance.TabIndex = 8;
            this.btnReduceBalance.Text = "扣  款";
            this.btnReduceBalance.UseVisualStyleBackColor = true;
            this.btnReduceBalance.Click += new System.EventHandler(this.btnReduceBalance_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "串口";
            // 
            // txtBalance
            // 
            this.txtBalance.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtBalance.Location = new System.Drawing.Point(364, 166);
            this.txtBalance.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtBalance.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.NumberWithCommas = false;
            this.txtBalance.PointCount = 2;
            this.txtBalance.Size = new System.Drawing.Size(139, 21);
            this.txtBalance.TabIndex = 15;
            this.txtBalance.Text = "0.00";
            // 
            // txtAmount
            // 
            this.txtAmount.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAmount.Location = new System.Drawing.Point(160, 332);
            this.txtAmount.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtAmount.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.NumberWithCommas = false;
            this.txtAmount.PointCount = 2;
            this.txtAmount.Size = new System.Drawing.Size(85, 21);
            this.txtAmount.TabIndex = 10;
            this.txtAmount.Text = "0.01";
            // 
            // comPort
            // 
            this.comPort.FormattingEnabled = true;
            this.comPort.Location = new System.Drawing.Point(108, 21);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(137, 20);
            this.comPort.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "类型";
            // 
            // cmbWType
            // 
            this.cmbWType.FormattingEnabled = true;
            this.cmbWType.Items.AddRange(new object[] {
            "",
            "1-M1钱包",
            "2-CPU钱包"});
            this.cmbWType.Location = new System.Drawing.Point(113, 97);
            this.cmbWType.Name = "cmbWType";
            this.cmbWType.Size = new System.Drawing.Size(139, 20);
            this.cmbWType.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "物理卡号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "逻辑卡号";
            // 
            // txtPhysicalID
            // 
            this.txtPhysicalID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPhysicalID.Location = new System.Drawing.Point(113, 131);
            this.txtPhysicalID.Name = "txtPhysicalID";
            this.txtPhysicalID.Size = new System.Drawing.Size(139, 21);
            this.txtPhysicalID.TabIndex = 20;
            // 
            // txtLogicID
            // 
            this.txtLogicID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtLogicID.Location = new System.Drawing.Point(364, 131);
            this.txtLogicID.Name = "txtLogicID";
            this.txtLogicID.Size = new System.Drawing.Size(139, 21);
            this.txtLogicID.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "卡片类型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(302, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "卡片余额";
            // 
            // txtCardType
            // 
            this.txtCardType.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardType.Location = new System.Drawing.Point(113, 166);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.Size = new System.Drawing.Size(139, 21);
            this.txtCardType.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "卡计数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "押金";
            // 
            // txtDeposit
            // 
            this.txtDeposit.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtDeposit.Location = new System.Drawing.Point(364, 203);
            this.txtDeposit.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtDeposit.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txtDeposit.Name = "txtDeposit";
            this.txtDeposit.NumberWithCommas = false;
            this.txtDeposit.PointCount = 2;
            this.txtDeposit.Size = new System.Drawing.Size(139, 21);
            this.txtDeposit.TabIndex = 27;
            this.txtDeposit.Text = "0.00";
            // 
            // txtCount
            // 
            this.txtCount.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCount.Location = new System.Drawing.Point(113, 203);
            this.txtCount.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtCount.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txtCount.Name = "txtCount";
            this.txtCount.NumberWithCommas = false;
            this.txtCount.PointCount = 0;
            this.txtCount.Size = new System.Drawing.Size(139, 21);
            this.txtCount.TabIndex = 28;
            this.txtCount.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(252, 336);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "元";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(364, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 36);
            this.button1.TabIndex = 30;
            this.button1.Text = "测试命令";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "读卡器版本号";
            // 
            // txtVersion
            // 
            this.txtVersion.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtVersion.Location = new System.Drawing.Point(108, 55);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(395, 21);
            this.txtVersion.TabIndex = 32;
            // 
            // txtSN
            // 
            this.txtSN.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtSN.Location = new System.Drawing.Point(364, 97);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(139, 21);
            this.txtSN.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(300, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 33;
            this.label11.Text = "序列号";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 382);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.txtDeposit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCardType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLogicID);
            this.Controls.Add(this.txtPhysicalID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbWType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnReduceBalance);
            this.Controls.Add(this.comPort);
            this.Controls.Add(this.btnReadCurCard);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnReadCurCard;
        private Ralid.GeneralLibrary.WinformControl.ComPortComboBox comPort;
        private System.Windows.Forms.Button btnReduceBalance;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtBalance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbWType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPhysicalID;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtLogicID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtDeposit;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtVersion;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtSN;
        private System.Windows.Forms.Label label11;
    }
}

