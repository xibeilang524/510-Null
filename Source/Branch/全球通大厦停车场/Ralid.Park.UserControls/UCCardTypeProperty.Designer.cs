namespace Ralid.Park.UserControls
{
    partial class UCCardTypeProperty
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
            this.comCardType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCompareFailOpenGate = new System.Windows.Forms.CheckBox();
            this.chkEnabledWiegand = new System.Windows.Forms.CheckBox();
            this.chkNotCompareCarPlate = new System.Windows.Forms.CheckBox();
            this.chkWriteCardHandle = new System.Windows.Forms.CheckBox();
            this.chkEnterNotWriteCarPlate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Location = new System.Drawing.Point(82, 9);
            this.comCardType.Name = "comCardType";
            this.comCardType.Size = new System.Drawing.Size(121, 20);
            this.comCardType.TabIndex = 0;
            this.comCardType.SelectedIndexChanged += new System.EventHandler(this.comCardType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "卡片类型:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCompareFailOpenGate);
            this.groupBox1.Controls.Add(this.chkEnabledWiegand);
            this.groupBox1.Controls.Add(this.chkNotCompareCarPlate);
            this.groupBox1.Controls.Add(this.chkWriteCardHandle);
            this.groupBox1.Controls.Add(this.chkEnterNotWriteCarPlate);
            this.groupBox1.Location = new System.Drawing.Point(3, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "卡片类型属性";
            // 
            // chkCompareFailOpenGate
            // 
            this.chkCompareFailOpenGate.AutoSize = true;
            this.chkCompareFailOpenGate.Checked = true;
            this.chkCompareFailOpenGate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCompareFailOpenGate.Location = new System.Drawing.Point(168, 44);
            this.chkCompareFailOpenGate.Name = "chkCompareFailOpenGate";
            this.chkCompareFailOpenGate.Size = new System.Drawing.Size(132, 16);
            this.chkCompareFailOpenGate.TabIndex = 0;
            this.chkCompareFailOpenGate.Text = "车牌对比失败时抬闸";
            this.chkCompareFailOpenGate.UseVisualStyleBackColor = true;
            // 
            // chkEnabledWiegand
            // 
            this.chkEnabledWiegand.AutoSize = true;
            this.chkEnabledWiegand.Checked = true;
            this.chkEnabledWiegand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabledWiegand.Location = new System.Drawing.Point(16, 66);
            this.chkEnabledWiegand.Name = "chkEnabledWiegand";
            this.chkEnabledWiegand.Size = new System.Drawing.Size(144, 16);
            this.chkEnabledWiegand.TabIndex = 0;
            this.chkEnabledWiegand.Text = "允许在韦根读卡器刷卡";
            this.chkEnabledWiegand.UseVisualStyleBackColor = true;
            // 
            // chkNotCompareCarPlate
            // 
            this.chkNotCompareCarPlate.AutoSize = true;
            this.chkNotCompareCarPlate.Checked = true;
            this.chkNotCompareCarPlate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotCompareCarPlate.Location = new System.Drawing.Point(16, 44);
            this.chkNotCompareCarPlate.Name = "chkNotCompareCarPlate";
            this.chkNotCompareCarPlate.Size = new System.Drawing.Size(84, 16);
            this.chkNotCompareCarPlate.TabIndex = 0;
            this.chkNotCompareCarPlate.Text = "不对比车牌";
            this.chkNotCompareCarPlate.UseVisualStyleBackColor = true;
            // 
            // chkWriteCardHandle
            // 
            this.chkWriteCardHandle.AutoSize = true;
            this.chkWriteCardHandle.Checked = true;
            this.chkWriteCardHandle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWriteCardHandle.Location = new System.Drawing.Point(168, 22);
            this.chkWriteCardHandle.Name = "chkWriteCardHandle";
            this.chkWriteCardHandle.Size = new System.Drawing.Size(144, 16);
            this.chkWriteCardHandle.TabIndex = 0;
            this.chkWriteCardHandle.Text = "此卡类型读写卡片内容";
            this.chkWriteCardHandle.UseVisualStyleBackColor = true;
            // 
            // chkEnterNotWriteCarPlate
            // 
            this.chkEnterNotWriteCarPlate.AutoSize = true;
            this.chkEnterNotWriteCarPlate.Checked = true;
            this.chkEnterNotWriteCarPlate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnterNotWriteCarPlate.Location = new System.Drawing.Point(16, 22);
            this.chkEnterNotWriteCarPlate.Name = "chkEnterNotWriteCarPlate";
            this.chkEnterNotWriteCarPlate.Size = new System.Drawing.Size(132, 16);
            this.chkEnterNotWriteCarPlate.TabIndex = 0;
            this.chkEnterNotWriteCarPlate.Text = "入口车牌不写入卡片";
            this.chkEnterNotWriteCarPlate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "（脱机模式有效）";
            // 
            // UCCardTypeProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comCardType);
            this.Name = "UCCardTypeProperty";
            this.Size = new System.Drawing.Size(360, 129);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comCardType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkCompareFailOpenGate;
        private System.Windows.Forms.CheckBox chkNotCompareCarPlate;
        private System.Windows.Forms.CheckBox chkWriteCardHandle;
        private System.Windows.Forms.CheckBox chkEnterNotWriteCarPlate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnabledWiegand;
    }
}
