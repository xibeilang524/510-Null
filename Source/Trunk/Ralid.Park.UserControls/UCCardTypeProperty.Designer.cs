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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCCardTypeProperty));
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
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.Name = "comCardType";
            this.comCardType.SelectedIndexChanged += new System.EventHandler(this.comCardType_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkCompareFailOpenGate);
            this.groupBox1.Controls.Add(this.chkEnabledWiegand);
            this.groupBox1.Controls.Add(this.chkNotCompareCarPlate);
            this.groupBox1.Controls.Add(this.chkWriteCardHandle);
            this.groupBox1.Controls.Add(this.chkEnterNotWriteCarPlate);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkCompareFailOpenGate
            // 
            resources.ApplyResources(this.chkCompareFailOpenGate, "chkCompareFailOpenGate");
            this.chkCompareFailOpenGate.Checked = true;
            this.chkCompareFailOpenGate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCompareFailOpenGate.Name = "chkCompareFailOpenGate";
            this.chkCompareFailOpenGate.UseVisualStyleBackColor = true;
            // 
            // chkEnabledWiegand
            // 
            resources.ApplyResources(this.chkEnabledWiegand, "chkEnabledWiegand");
            this.chkEnabledWiegand.Checked = true;
            this.chkEnabledWiegand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabledWiegand.Name = "chkEnabledWiegand";
            this.chkEnabledWiegand.UseVisualStyleBackColor = true;
            // 
            // chkNotCompareCarPlate
            // 
            resources.ApplyResources(this.chkNotCompareCarPlate, "chkNotCompareCarPlate");
            this.chkNotCompareCarPlate.Checked = true;
            this.chkNotCompareCarPlate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotCompareCarPlate.Name = "chkNotCompareCarPlate";
            this.chkNotCompareCarPlate.UseVisualStyleBackColor = true;
            // 
            // chkWriteCardHandle
            // 
            resources.ApplyResources(this.chkWriteCardHandle, "chkWriteCardHandle");
            this.chkWriteCardHandle.Checked = true;
            this.chkWriteCardHandle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWriteCardHandle.Name = "chkWriteCardHandle";
            this.chkWriteCardHandle.UseVisualStyleBackColor = true;
            // 
            // chkEnterNotWriteCarPlate
            // 
            resources.ApplyResources(this.chkEnterNotWriteCarPlate, "chkEnterNotWriteCarPlate");
            this.chkEnterNotWriteCarPlate.Checked = true;
            this.chkEnterNotWriteCarPlate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnterNotWriteCarPlate.Name = "chkEnterNotWriteCarPlate";
            this.chkEnterNotWriteCarPlate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // UCCardTypeProperty
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comCardType);
            this.Name = "UCCardTypeProperty";
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
