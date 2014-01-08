namespace Ralid.Park.UserControls.VideoPanels
{
    partial class UCVideoListView
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
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnRight
            // 
            this.btnRight.BackColor = System.Drawing.Color.White;
            this.btnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRight.Image = global::Ralid.Park.UserControls.Properties.Resources.arrow_sans_right_32;
            this.btnRight.Location = new System.Drawing.Point(440, 0);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(19, 325);
            this.btnRight.TabIndex = 11;
            this.btnRight.UseVisualStyleBackColor = false;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.Color.White;
            this.btnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLeft.Image = global::Ralid.Park.UserControls.Properties.Resources.controls_play_back_32;
            this.btnLeft.Location = new System.Drawing.Point(0, 0);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(19, 325);
            this.btnLeft.TabIndex = 10;
            this.btnLeft.UseVisualStyleBackColor = false;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // videoPanel
            // 
            this.videoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPanel.Location = new System.Drawing.Point(19, 0);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(421, 325);
            this.videoPanel.TabIndex = 12;
            // 
            // UCVideoListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Name = "UCVideoListView";
            this.Size = new System.Drawing.Size(459, 325);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Panel videoPanel;
    }
}
