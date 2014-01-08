namespace Ralid.Park.UI
{
    partial class FrmDownLoadTariffSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDownLoadTariffSetting));
            this.hardwareTree1 = new Ralid.Park.UserControls.HardwareTree(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            // 
            // hardwareTree1
            // 
            resources.ApplyResources(this.hardwareTree1, "hardwareTree1");
            this.hardwareTree1.CheckBoxes = true;
            this.hardwareTree1.Name = "hardwareTree1";
            this.hardwareTree1.ShowEntrance = true;
            this.hardwareTree1.ShowVideoSource = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FrmDownLoadTariffSetting
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.hardwareTree1);
            this.Controls.Add(this.label1);
            this.Name = "FrmDownLoadTariffSetting";
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.hardwareTree1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.HardwareTree hardwareTree1;
        private System.Windows.Forms.Label label1;

    }
}
