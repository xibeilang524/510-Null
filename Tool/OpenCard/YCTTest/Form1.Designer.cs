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
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(181, 20);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(129, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnReadCurCard
            // 
            this.btnReadCurCard.Location = new System.Drawing.Point(342, 331);
            this.btnReadCurCard.Name = "btnReadCurCard";
            this.btnReadCurCard.Size = new System.Drawing.Size(129, 23);
            this.btnReadCurCard.TabIndex = 1;
            this.btnReadCurCard.Text = "读  卡";
            this.btnReadCurCard.UseVisualStyleBackColor = true;
            this.btnReadCurCard.Click += new System.EventHandler(this.btnReadCurCard_Click);
            // 
            // btnReduceBalance
            // 
            this.btnReduceBalance.Location = new System.Drawing.Point(181, 331);
            this.btnReduceBalance.Name = "btnReduceBalance";
            this.btnReduceBalance.Size = new System.Drawing.Size(129, 23);
            this.btnReduceBalance.TabIndex = 8;
            this.btnReduceBalance.Text = "扣  款";
            this.btnReduceBalance.UseVisualStyleBackColor = true;
            this.btnReduceBalance.Click += new System.EventHandler(this.btnReduceBalance_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "串口";
            // 
            // txtBalance
            // 
            this.txtBalance.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtBalance.Location = new System.Drawing.Point(181, 101);
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
            this.txtBalance.Size = new System.Drawing.Size(132, 21);
            this.txtBalance.TabIndex = 15;
            this.txtBalance.Text = "0.00";
            // 
            // txtAmount
            // 
            this.txtAmount.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtAmount.Location = new System.Drawing.Point(181, 143);
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
            this.txtAmount.Size = new System.Drawing.Size(132, 21);
            this.txtAmount.TabIndex = 10;
            this.txtAmount.Text = "0.00";
            // 
            // comPort
            // 
            this.comPort.FormattingEnabled = true;
            this.comPort.Location = new System.Drawing.Point(64, 21);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(70, 20);
            this.comPort.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "类型";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 382);
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
    }
}

