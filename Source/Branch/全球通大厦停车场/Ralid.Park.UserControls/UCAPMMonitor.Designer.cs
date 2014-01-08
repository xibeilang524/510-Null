namespace Ralid.Park.UserControls
{
    partial class UCAPMMonitor
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
            this.apmPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // apmPanel
            // 
            this.apmPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apmPanel.Location = new System.Drawing.Point(0, 0);
            this.apmPanel.Name = "apmPanel";
            this.apmPanel.Size = new System.Drawing.Size(239, 57);
            this.apmPanel.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UCAPMMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.apmPanel);
            this.Name = "UCAPMMonitor";
            this.Size = new System.Drawing.Size(239, 57);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel apmPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;


    }
}
