namespace BuzzerDemo_CSharp
{
    partial class Buzzer
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
            this.bnBuzzerOn = new System.Windows.Forms.Button();
            this.bnBeepFive = new System.Windows.Forms.Button();
            this.bnBuzzerOff = new System.Windows.Forms.Button();
            this.bnBuzzerState = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bnBuzzerOn
            // 
            this.bnBuzzerOn.Location = new System.Drawing.Point(6, 66);
            this.bnBuzzerOn.Name = "bnBuzzerOn";
            this.bnBuzzerOn.Size = new System.Drawing.Size(105, 30);
            this.bnBuzzerOn.TabIndex = 0;
            this.bnBuzzerOn.Text = "蜂鸣器蜂鸣";
            this.bnBuzzerOn.Click += new System.EventHandler(this.bnBuzzerOn_Click);
            // 
            // bnBeepFive
            // 
            this.bnBeepFive.Location = new System.Drawing.Point(122, 66);
            this.bnBeepFive.Name = "bnBeepFive";
            this.bnBeepFive.Size = new System.Drawing.Size(105, 30);
            this.bnBeepFive.TabIndex = 0;
            this.bnBeepFive.Text = "蜂鸣器鸣叫5次";
            this.bnBeepFive.Click += new System.EventHandler(this.bnBeepFive_Click);
            // 
            // bnBuzzerOff
            // 
            this.bnBuzzerOff.Location = new System.Drawing.Point(6, 122);
            this.bnBuzzerOff.Name = "bnBuzzerOff";
            this.bnBuzzerOff.Size = new System.Drawing.Size(105, 30);
            this.bnBuzzerOff.TabIndex = 0;
            this.bnBuzzerOff.Text = "蜂鸣器禁止";
            this.bnBuzzerOff.Click += new System.EventHandler(this.bnBuzzerOff_Click);
            // 
            // bnBuzzerState
            // 
            this.bnBuzzerState.Location = new System.Drawing.Point(122, 122);
            this.bnBuzzerState.Name = "bnBuzzerState";
            this.bnBuzzerState.Size = new System.Drawing.Size(105, 30);
            this.bnBuzzerState.TabIndex = 0;
            this.bnBuzzerState.Text = "读蜂鸣器状态";
            this.bnBuzzerState.Click += new System.EventHandler(this.bnBuzzerState_Click);
            // 
            // Buzzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.bnBuzzerState);
            this.Controls.Add(this.bnBuzzerOff);
            this.Controls.Add(this.bnBeepFive);
            this.Controls.Add(this.bnBuzzerOn);
            this.Name = "Buzzer";
            this.Text = "Buzzer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bnBuzzerOn;
        private System.Windows.Forms.Button bnBeepFive;
        private System.Windows.Forms.Button bnBuzzerOff;
        private System.Windows.Forms.Button bnBuzzerState;
    }
}

