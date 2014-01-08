namespace Ralid.Park.UI
{
    partial class FrmCarPlateOfVecon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarPlateOfVecon));
            this.rm = new AxRMCLILib.AxRMCli();
            this.btnInit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rm)).BeginInit();
            this.SuspendLayout();
            // 
            // rm
            // 
            resources.ApplyResources(this.rm, "rm");
            this.rm.Name = "rm";
            this.rm.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rm.OcxState")));
            // 
            // btnInit
            // 
            resources.ApplyResources(this.btnInit, "btnInit");
            this.btnInit.Name = "btnInit";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // FrmCarPlateOfVecon
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.rm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCarPlateOfVecon";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVeconCarPlateRecognization_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCarPlateRecognizationImp_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.rm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxRMCLILib.AxRMCli rm;
        private System.Windows.Forms.Button btnInit;
    }
}

