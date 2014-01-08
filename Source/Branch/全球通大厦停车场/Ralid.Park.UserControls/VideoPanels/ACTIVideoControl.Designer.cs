namespace Ralid.Park.UserControls.VideoPanels
{
    partial class ACTIVideoControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACTIVideoControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.media = new AxnvUnifiedControlLib.AxnvUnifiedControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.media)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.media);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 253);
            this.panel1.TabIndex = 1;
            // 
            // media
            // 
            this.media.Dock = System.Windows.Forms.DockStyle.Fill;
            this.media.Enabled = true;
            this.media.Location = new System.Drawing.Point(0, 0);
            this.media.Name = "media";
            this.media.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("media.OcxState")));
            this.media.Size = new System.Drawing.Size(365, 253);
            this.media.TabIndex = 0;
            // 
            // ACTIVideoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ACTIVideoControl";
            this.SizeChanged += new System.EventHandler(this.ACTIVideoPanel_Resize);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.TitlePanel, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.media)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private AxnvUnifiedControlLib.AxnvUnifiedControl media;

    }
}
