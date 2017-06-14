namespace LR280Test
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
            this.label1 = new System.Windows.Forms.Label();
            this.comPort = new Ralid.GeneralLibrary.WinformControl.ComPortComboBox(this.components);
            this.btnConnect = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAmount = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.btn扣款 = new System.Windows.Forms.Button();
            this.btn读卡 = new System.Windows.Forms.Button();
            this.btn签到 = new System.Windows.Forms.Button();
            this.btn结算 = new System.Windows.Forms.Button();
            this.txtCardType = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtBalance = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.txtBank = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.RichTextBox();
            this.btn查余额 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "串口";
            // 
            // comPort
            // 
            this.comPort.FormattingEnabled = true;
            this.comPort.Location = new System.Drawing.Point(45, 23);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(137, 20);
            this.comPort.TabIndex = 15;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(207, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(98, 23);
            this.btnConnect.TabIndex = 14;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(160, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "元";
            // 
            // txtAmount
            // 
            this.txtAmount.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAmount.Location = new System.Drawing.Point(68, 205);
            this.txtAmount.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtAmount.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.NumberWithCommas = false;
            this.txtAmount.PointCount = 2;
            this.txtAmount.Size = new System.Drawing.Size(85, 21);
            this.txtAmount.TabIndex = 32;
            this.txtAmount.Text = "0.01";
            // 
            // btn扣款
            // 
            this.btn扣款.Enabled = false;
            this.btn扣款.Location = new System.Drawing.Point(186, 197);
            this.btn扣款.Name = "btn扣款";
            this.btn扣款.Size = new System.Drawing.Size(129, 36);
            this.btn扣款.TabIndex = 31;
            this.btn扣款.Text = "扣  款";
            this.btn扣款.UseVisualStyleBackColor = true;
            this.btn扣款.Click += new System.EventHandler(this.btn扣款_Click);
            // 
            // btn读卡
            // 
            this.btn读卡.Enabled = false;
            this.btn读卡.Location = new System.Drawing.Point(12, 76);
            this.btn读卡.Name = "btn读卡";
            this.btn读卡.Size = new System.Drawing.Size(85, 25);
            this.btn读卡.TabIndex = 30;
            this.btn读卡.Text = "读  卡";
            this.btn读卡.UseVisualStyleBackColor = true;
            this.btn读卡.Click += new System.EventHandler(this.btn读卡_Click);
            // 
            // btn签到
            // 
            this.btn签到.Enabled = false;
            this.btn签到.Location = new System.Drawing.Point(219, 76);
            this.btn签到.Name = "btn签到";
            this.btn签到.Size = new System.Drawing.Size(85, 25);
            this.btn签到.TabIndex = 34;
            this.btn签到.Text = "签    到";
            this.btn签到.UseVisualStyleBackColor = true;
            this.btn签到.Click += new System.EventHandler(this.btn签到_Click);
            // 
            // btn结算
            // 
            this.btn结算.Enabled = false;
            this.btn结算.Location = new System.Drawing.Point(332, 76);
            this.btn结算.Name = "btn结算";
            this.btn结算.Size = new System.Drawing.Size(85, 25);
            this.btn结算.TabIndex = 35;
            this.btn结算.Text = "结  算";
            this.btn结算.UseVisualStyleBackColor = true;
            this.btn结算.Click += new System.EventHandler(this.btn结算_Click);
            // 
            // txtCardType
            // 
            this.txtCardType.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardType.Location = new System.Drawing.Point(67, 163);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.ReadOnly = true;
            this.txtCardType.Size = new System.Drawing.Size(139, 21);
            this.txtCardType.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "卡片余额";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "卡片类型";
            // 
            // txtCardID
            // 
            this.txtCardID.Enabled = false;
            this.txtCardID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardID.Location = new System.Drawing.Point(67, 128);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.ReadOnly = true;
            this.txtCardID.Size = new System.Drawing.Size(139, 21);
            this.txtCardID.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 37;
            this.label3.Text = "卡号";
            // 
            // txtBalance
            // 
            this.txtBalance.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtBalance.Location = new System.Drawing.Point(297, 163);
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
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(126, 21);
            this.txtBalance.TabIndex = 36;
            this.txtBalance.Text = "0.00";
            // 
            // txtBank
            // 
            this.txtBank.Enabled = false;
            this.txtBank.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtBank.Location = new System.Drawing.Point(297, 128);
            this.txtBank.Name = "txtBank";
            this.txtBank.ReadOnly = true;
            this.txtBank.Size = new System.Drawing.Size(126, 21);
            this.txtBank.TabIndex = 43;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(240, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 42;
            this.label11.Text = "银行行号";
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(12, 256);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(411, 144);
            this.txtResponse.TabIndex = 44;
            this.txtResponse.Text = "";
            // 
            // btn查余额
            // 
            this.btn查余额.Enabled = false;
            this.btn查余额.Location = new System.Drawing.Point(116, 76);
            this.btn查余额.Name = "btn查余额";
            this.btn查余额.Size = new System.Drawing.Size(85, 25);
            this.btn查余额.TabIndex = 45;
            this.btn查余额.Text = "查余额";
            this.btn查余额.UseVisualStyleBackColor = true;
            this.btn查余额.Click += new System.EventHandler(this.btn查余额_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 401);
            this.Controls.Add(this.btn查余额);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCardType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.btn结算);
            this.Controls.Add(this.btn签到);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btn扣款);
            this.Controls.Add(this.btn读卡);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comPort);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "LR280测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Ralid.GeneralLibrary.WinformControl.ComPortComboBox comPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label9;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtAmount;
        private System.Windows.Forms.Button btn扣款;
        private System.Windows.Forms.Button btn读卡;
        private System.Windows.Forms.Button btn签到;
        private System.Windows.Forms.Button btn结算;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtBalance;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtBank;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox txtResponse;
        private System.Windows.Forms.Button btn查余额;
    }
}

