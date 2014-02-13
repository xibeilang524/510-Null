namespace Ralid.Park.POS
{
    partial class FrmMain
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExportSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportPayment = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDBSetting = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDBState = new System.Windows.Forms.ToolStripStatusLabel();
            this.eventList = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportSetting,
            this.toolStripSeparator3,
            this.btnImportPayment});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(557, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnExportSetting
            // 
            this.btnExportSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportSetting.Name = "btnExportSetting";
            this.btnExportSetting.Size = new System.Drawing.Size(84, 22);
            this.btnExportSetting.Text = "导出配置信息";
            this.btnExportSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportSetting.Click += new System.EventHandler(this.btnExportSetting_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImportPayment
            // 
            this.btnImportPayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportPayment.Name = "btnImportPayment";
            this.btnImportPayment.Size = new System.Drawing.Size(84, 22);
            this.btnImportPayment.Text = "导入收费记录";
            this.btnImportPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportPayment.Click += new System.EventHandler(this.btnCardEvent_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDBSetting,
            this.lblDBState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 237);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(557, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblDBSetting
            // 
            this.lblDBSetting.Name = "lblDBSetting";
            this.lblDBSetting.Size = new System.Drawing.Size(0, 17);
            // 
            // lblDBState
            // 
            this.lblDBState.Name = "lblDBState";
            this.lblDBState.Size = new System.Drawing.Size(0, 17);
            // 
            // eventList
            // 
            this.eventList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventList.Font = new System.Drawing.Font("宋体", 10F);
            this.eventList.FormattingEnabled = true;
            this.eventList.HorizontalScrollbar = true;
            this.eventList.ItemHeight = 12;
            this.eventList.Location = new System.Drawing.Point(0, 25);
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(557, 212);
            this.eventList.TabIndex = 84;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 259);
            this.Controls.Add(this.eventList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手持机同步工具";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnExportSetting;
        private System.Windows.Forms.ToolStripButton btnImportPayment;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblDBSetting;
        private System.Windows.Forms.ToolStripStatusLabel lblDBState;
        private Park.UserControls.EventReportListBox eventList;

    }
}

