namespace Ralid.Parking.POS.UI
{
    partial class FrmPaying
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCardID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lblHasPaid = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.lblEnterDT = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPaid = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(125, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 47);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "放弃处理(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(9, 195);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(106, 47);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "收费(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(35, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.Text = "卡号：";
            // 
            // lblCardID
            // 
            this.lblCardID.Location = new System.Drawing.Point(75, 17);
            this.lblCardID.Name = "lblCardID";
            this.lblCardID.Size = new System.Drawing.Size(156, 16);
            this.lblCardID.Text = "233955501";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 18);
            this.label3.Text = "入场时间：";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 18);
            this.label5.Text = "停车时长：";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.Text = "已收费用：";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 18);
            this.label7.Text = "本次应收：";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 18);
            this.label8.Text = "本次实收：";
            // 
            // lblInterval
            // 
            this.lblInterval.Location = new System.Drawing.Point(75, 63);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(156, 16);
            this.lblInterval.Text = "3 小时 24 分钟";
            // 
            // lblHasPaid
            // 
            this.lblHasPaid.Location = new System.Drawing.Point(75, 116);
            this.lblHasPaid.Name = "lblHasPaid";
            this.lblHasPaid.Size = new System.Drawing.Size(156, 16);
            this.lblHasPaid.Text = "30 元";
            // 
            // lblAccount
            // 
            this.lblAccount.Location = new System.Drawing.Point(75, 140);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(156, 16);
            this.lblAccount.Text = "4 元";
            // 
            // lblEnterDT
            // 
            this.lblEnterDT.Location = new System.Drawing.Point(75, 40);
            this.lblEnterDT.Name = "lblEnterDT";
            this.lblEnterDT.Size = new System.Drawing.Size(156, 16);
            this.lblEnterDT.Text = "2013-8-9 12:34:21";
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(75, 88);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(156, 16);
            this.lblTotal.Text = "34 元";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(9, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 18);
            this.label16.Text = "费用总额：";
            // 
            // txtPaid
            // 
            this.txtPaid.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtPaid.Location = new System.Drawing.Point(75, 158);
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.Size = new System.Drawing.Size(104, 26);
            this.txtPaid.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(184, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 18);
            this.label13.Text = "元";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(9, 252);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(106, 32);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "返回主界面(&R)";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FrmPaying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPaid);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lblEnterDT);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.lblHasPaid);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCardID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPaying";
            this.Text = "FrmPaying";
            this.Load += new System.EventHandler(this.FrmPaying_Load);
            this.Closed += new System.EventHandler(this.FrmPaying_Closed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label lblHasPaid;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label lblEnterDT;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPaid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnBack;
    }
}