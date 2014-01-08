namespace ParkingDebugTool
{
    partial class UCSection
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
            this.ckbblock0 = new System.Windows.Forms.CheckBox();
            this.txtBlock0 = new Ralid.GeneralLibrary.WinformControl.HexTextBox(this.components);
            this.ckbblock1 = new System.Windows.Forms.CheckBox();
            this.txtBlock1 = new Ralid.GeneralLibrary.WinformControl.HexTextBox(this.components);
            this.ckbblock2 = new System.Windows.Forms.CheckBox();
            this.txtBlock2 = new Ralid.GeneralLibrary.WinformControl.HexTextBox(this.components);
            this.lblText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbblock0
            // 
            this.ckbblock0.AutoSize = true;
            this.ckbblock0.Checked = true;
            this.ckbblock0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbblock0.Location = new System.Drawing.Point(14, 18);
            this.ckbblock0.Name = "ckbblock0";
            this.ckbblock0.Size = new System.Drawing.Size(42, 16);
            this.ckbblock0.TabIndex = 0;
            this.ckbblock0.Text = "块0";
            this.ckbblock0.UseVisualStyleBackColor = true;
            // 
            // txtBlock0
            // 
            this.txtBlock0.InputSpace = true;
            this.txtBlock0.Location = new System.Drawing.Point(62, 16);
            this.txtBlock0.MaxLength = 47;
            this.txtBlock0.Name = "txtBlock0";
            this.txtBlock0.Size = new System.Drawing.Size(289, 21);
            this.txtBlock0.TabIndex = 1;
            this.txtBlock0.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            // 
            // ckbblock1
            // 
            this.ckbblock1.AutoSize = true;
            this.ckbblock1.Checked = true;
            this.ckbblock1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbblock1.Location = new System.Drawing.Point(14, 57);
            this.ckbblock1.Name = "ckbblock1";
            this.ckbblock1.Size = new System.Drawing.Size(42, 16);
            this.ckbblock1.TabIndex = 0;
            this.ckbblock1.Text = "块1";
            this.ckbblock1.UseVisualStyleBackColor = true;
            // 
            // txtBlock1
            // 
            this.txtBlock1.InputSpace = true;
            this.txtBlock1.Location = new System.Drawing.Point(62, 55);
            this.txtBlock1.MaxLength = 47;
            this.txtBlock1.Name = "txtBlock1";
            this.txtBlock1.Size = new System.Drawing.Size(289, 21);
            this.txtBlock1.TabIndex = 1;
            this.txtBlock1.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            // 
            // ckbblock2
            // 
            this.ckbblock2.AutoSize = true;
            this.ckbblock2.Checked = true;
            this.ckbblock2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbblock2.Location = new System.Drawing.Point(14, 96);
            this.ckbblock2.Name = "ckbblock2";
            this.ckbblock2.Size = new System.Drawing.Size(42, 16);
            this.ckbblock2.TabIndex = 0;
            this.ckbblock2.Text = "块2";
            this.ckbblock2.UseVisualStyleBackColor = true;
            // 
            // txtBlock2
            // 
            this.txtBlock2.InputSpace = true;
            this.txtBlock2.Location = new System.Drawing.Point(62, 94);
            this.txtBlock2.MaxLength = 47;
            this.txtBlock2.Name = "txtBlock2";
            this.txtBlock2.Size = new System.Drawing.Size(289, 21);
            this.txtBlock2.TabIndex = 1;
            this.txtBlock2.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblText.Location = new System.Drawing.Point(3, 3);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(45, 12);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "扇区 0";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ckbblock0);
            this.panel1.Controls.Add(this.txtBlock0);
            this.panel1.Controls.Add(this.txtBlock2);
            this.panel1.Controls.Add(this.ckbblock1);
            this.panel1.Controls.Add(this.ckbblock2);
            this.panel1.Controls.Add(this.txtBlock1);
            this.panel1.Location = new System.Drawing.Point(5, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 135);
            this.panel1.TabIndex = 3;
            // 
            // UCSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblText);
            this.Name = "UCSection";
            this.Size = new System.Drawing.Size(375, 158);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbblock0;
        private Ralid.GeneralLibrary.WinformControl.HexTextBox txtBlock0;
        private System.Windows.Forms.CheckBox ckbblock1;
        private Ralid.GeneralLibrary.WinformControl.HexTextBox txtBlock1;
        private System.Windows.Forms.CheckBox ckbblock2;
        private Ralid.GeneralLibrary.WinformControl.HexTextBox txtBlock2;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Panel panel1;
    }
}
