namespace Ralid.Park.ThirdCommunication
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnu_System = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDeviceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStartFrom = new System.Windows.Forms.ToolStripStatusLabel();
            this.eventReportListBox1 = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_System});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(761, 25);
            this.menuStrip1.TabIndex = 78;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnu_System
            // 
            this.mnu_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Exit});
            this.mnu_System.Name = "mnu_System";
            this.mnu_System.Size = new System.Drawing.Size(44, 21);
            this.mnu_System.Text = "系统";
            // 
            // mnu_Exit
            // 
            this.mnu_Exit.Name = "mnu_Exit";
            this.mnu_Exit.Size = new System.Drawing.Size(100, 22);
            this.mnu_Exit.Text = "退出";
            this.mnu_Exit.Click += new System.EventHandler(this.mnu_Exit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDeviceStatus,
            this.lblStartFrom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(761, 22);
            this.statusStrip1.TabIndex = 80;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblDeviceStatus
            // 
            this.lblDeviceStatus.Name = "lblDeviceStatus";
            this.lblDeviceStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // lblStartFrom
            // 
            this.lblStartFrom.Name = "lblStartFrom";
            this.lblStartFrom.Size = new System.Drawing.Size(0, 17);
            // 
            // eventReportListBox1
            // 
            this.eventReportListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventReportListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventReportListBox1.FormattingEnabled = true;
            this.eventReportListBox1.ItemHeight = 12;
            this.eventReportListBox1.Location = new System.Drawing.Point(0, 25);
            this.eventReportListBox1.Name = "eventReportListBox1";
            this.eventReportListBox1.Size = new System.Drawing.Size(761, 215);
            this.eventReportListBox1.TabIndex = 81;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 262);
            this.Controls.Add(this.eventReportListBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FrmMain";
            this.Text = "交委接口";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_System;
        private System.Windows.Forms.ToolStripMenuItem mnu_Exit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblDeviceStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStartFrom;
        private UserControls.EventReportListBox eventReportListBox1;
    }
}