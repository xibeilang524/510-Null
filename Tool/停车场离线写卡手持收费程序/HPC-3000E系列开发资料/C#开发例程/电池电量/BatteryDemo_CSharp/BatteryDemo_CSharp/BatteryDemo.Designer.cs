namespace BatteryDemo_CSharp
{
    partial class BatteryDemo
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
            this.txtVoltage = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtPercent = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblCharge = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtVoltage
            // 
            this.txtVoltage.Location = new System.Drawing.Point(36, 44);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.ReadOnly = true;
            this.txtVoltage.Size = new System.Drawing.Size(181, 23);
            this.txtVoltage.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(36, 143);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(127, 20);
            this.lblStatus.Text = "label1";
            // 
            // txtPercent
            // 
            this.txtPercent.Location = new System.Drawing.Point(36, 93);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.ReadOnly = true;
            this.txtPercent.Size = new System.Drawing.Size(181, 23);
            this.txtPercent.TabIndex = 0;
            // 
            // lblLevel
            // 
            this.lblLevel.Location = new System.Drawing.Point(36, 183);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(127, 20);
            this.lblLevel.Text = "label1";
            // 
            // lblCharge
            // 
            this.lblCharge.Location = new System.Drawing.Point(36, 221);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(127, 20);
            this.lblCharge.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.lblCharge);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtPercent);
            this.Controls.Add(this.txtVoltage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtVoltage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtPercent;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblCharge;
    }
}

