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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExportSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportPayment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportAuthen = new System.Windows.Forms.ToolStripButton();
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
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportSetting,
            this.toolStripSeparator3,
            this.btnImportPayment,
            this.toolStripSeparator1,
            this.btnImportAuthen});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnExportSetting
            // 
            resources.ApplyResources(this.btnExportSetting, "btnExportSetting");
            this.btnExportSetting.Name = "btnExportSetting";
            this.btnExportSetting.Click += new System.EventHandler(this.btnExportSetting_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // btnImportPayment
            // 
            resources.ApplyResources(this.btnImportPayment, "btnImportPayment");
            this.btnImportPayment.Name = "btnImportPayment";
            this.btnImportPayment.Click += new System.EventHandler(this.btnCardEvent_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // btnImportAuthen
            // 
            resources.ApplyResources(this.btnImportAuthen, "btnImportAuthen");
            this.btnImportAuthen.Name = "btnImportAuthen";
            this.btnImportAuthen.Click += new System.EventHandler(this.btnImportAuthen_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDBSetting,
            this.lblDBState});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lblDBSetting
            // 
            resources.ApplyResources(this.lblDBSetting, "lblDBSetting");
            this.lblDBSetting.Name = "lblDBSetting";
            // 
            // lblDBState
            // 
            resources.ApplyResources(this.lblDBState, "lblDBState");
            this.lblDBState.Name = "lblDBState";
            // 
            // eventList
            // 
            resources.ApplyResources(this.eventList, "eventList");
            this.eventList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventList.FormattingEnabled = true;
            this.eventList.Name = "eventList";
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eventList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMain";
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImportAuthen;

    }
}

