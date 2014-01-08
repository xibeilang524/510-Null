namespace Ralid.Park.UI
{
    partial class FrmZSTParameter
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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetUploadTime = new System.Windows.Forms.Button();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.dateTimePicker8 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker7 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btn_ConsumptionReport = new System.Windows.Forms.Button();
            this.group2 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox1.SuspendLayout();
            this.group2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(317, 391);
            this.txtMessage.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(9, 20);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker1.TabIndex = 4;
            this.dateTimePicker1.Value = new System.DateTime(2013, 3, 14, 1, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetUploadTime);
            this.groupBox1.Controls.Add(this.btn_Upload);
            this.groupBox1.Controls.Add(this.dateTimePicker8);
            this.groupBox1.Controls.Add(this.dateTimePicker7);
            this.groupBox1.Controls.Add(this.dateTimePicker6);
            this.groupBox1.Controls.Add(this.dateTimePicker5);
            this.groupBox1.Controls.Add(this.dateTimePicker4);
            this.groupBox1.Controls.Add(this.dateTimePicker3);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Location = new System.Drawing.Point(323, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 214);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自动上传时间设置";
            // 
            // btnSetUploadTime
            // 
            this.btnSetUploadTime.Location = new System.Drawing.Point(25, 129);
            this.btnSetUploadTime.Name = "btnSetUploadTime";
            this.btnSetUploadTime.Size = new System.Drawing.Size(183, 33);
            this.btnSetUploadTime.TabIndex = 12;
            this.btnSetUploadTime.Text = "设置(&S)";
            this.btnSetUploadTime.UseVisualStyleBackColor = true;
            this.btnSetUploadTime.Click += new System.EventHandler(this.btnSetUploadTime_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Location = new System.Drawing.Point(25, 171);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(183, 33);
            this.btn_Upload.TabIndex = 13;
            this.btn_Upload.Text = "手动上传消费记录(&M)";
            this.btn_Upload.UseVisualStyleBackColor = true;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // dateTimePicker8
            // 
            this.dateTimePicker8.Checked = false;
            this.dateTimePicker8.CustomFormat = "HH:mm";
            this.dateTimePicker8.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker8.Location = new System.Drawing.Point(133, 101);
            this.dateTimePicker8.Name = "dateTimePicker8";
            this.dateTimePicker8.ShowCheckBox = true;
            this.dateTimePicker8.ShowUpDown = true;
            this.dateTimePicker8.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker8.TabIndex = 11;
            this.dateTimePicker8.Value = new System.DateTime(2013, 3, 14, 22, 0, 0, 0);
            // 
            // dateTimePicker7
            // 
            this.dateTimePicker7.Checked = false;
            this.dateTimePicker7.CustomFormat = "HH:mm";
            this.dateTimePicker7.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker7.Location = new System.Drawing.Point(9, 101);
            this.dateTimePicker7.Name = "dateTimePicker7";
            this.dateTimePicker7.ShowCheckBox = true;
            this.dateTimePicker7.ShowUpDown = true;
            this.dateTimePicker7.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker7.TabIndex = 10;
            this.dateTimePicker7.Value = new System.DateTime(2013, 3, 14, 20, 0, 0, 0);
            // 
            // dateTimePicker6
            // 
            this.dateTimePicker6.Checked = false;
            this.dateTimePicker6.CustomFormat = "HH:mm";
            this.dateTimePicker6.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker6.Location = new System.Drawing.Point(133, 74);
            this.dateTimePicker6.Name = "dateTimePicker6";
            this.dateTimePicker6.ShowCheckBox = true;
            this.dateTimePicker6.ShowUpDown = true;
            this.dateTimePicker6.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker6.TabIndex = 9;
            this.dateTimePicker6.Value = new System.DateTime(2013, 3, 14, 15, 0, 0, 0);
            // 
            // dateTimePicker5
            // 
            this.dateTimePicker5.Checked = false;
            this.dateTimePicker5.CustomFormat = "HH:mm";
            this.dateTimePicker5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker5.Location = new System.Drawing.Point(9, 74);
            this.dateTimePicker5.Name = "dateTimePicker5";
            this.dateTimePicker5.ShowCheckBox = true;
            this.dateTimePicker5.ShowUpDown = true;
            this.dateTimePicker5.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker5.TabIndex = 8;
            this.dateTimePicker5.Value = new System.DateTime(2013, 3, 14, 12, 0, 0, 0);
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Checked = false;
            this.dateTimePicker4.CustomFormat = "HH:mm";
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker4.Location = new System.Drawing.Point(133, 47);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.ShowCheckBox = true;
            this.dateTimePicker4.ShowUpDown = true;
            this.dateTimePicker4.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker4.TabIndex = 7;
            this.dateTimePicker4.Value = new System.DateTime(2013, 3, 14, 9, 0, 0, 0);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Checked = false;
            this.dateTimePicker3.CustomFormat = "HH:mm";
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker3.Location = new System.Drawing.Point(9, 47);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.ShowCheckBox = true;
            this.dateTimePicker3.ShowUpDown = true;
            this.dateTimePicker3.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker3.TabIndex = 6;
            this.dateTimePicker3.Value = new System.DateTime(2013, 3, 14, 5, 0, 0, 0);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(133, 20);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowCheckBox = true;
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(83, 21);
            this.dateTimePicker2.TabIndex = 5;
            this.dateTimePicker2.Value = new System.DateTime(2013, 3, 14, 3, 0, 0, 0);
            // 
            // btn_ConsumptionReport
            // 
            this.btn_ConsumptionReport.Location = new System.Drawing.Point(22, 109);
            this.btn_ConsumptionReport.Name = "btn_ConsumptionReport";
            this.btn_ConsumptionReport.Size = new System.Drawing.Size(183, 33);
            this.btn_ConsumptionReport.TabIndex = 14;
            this.btn_ConsumptionReport.Text = "脱机对账统计(&M)";
            this.btn_ConsumptionReport.UseVisualStyleBackColor = true;
            this.btn_ConsumptionReport.Click += new System.EventHandler(this.btn_ConsumptionReport_Click);
            // 
            // group2
            // 
            this.group2.Controls.Add(this.ucDateTimeInterval1);
            this.group2.Controls.Add(this.btn_ConsumptionReport);
            this.group2.Location = new System.Drawing.Point(324, 227);
            this.group2.Name = "group2";
            this.group2.Size = new System.Drawing.Size(236, 152);
            this.group2.TabIndex = 15;
            this.group2.TabStop = false;
            this.group2.Text = "脱机对账统计";
            // 
            // ucDateTimeInterval1
            // 
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2013, 3, 14, 10, 1, 42, 146);
            this.ucDateTimeInterval1.Location = new System.Drawing.Point(6, 21);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = false;
            this.ucDateTimeInterval1.Size = new System.Drawing.Size(221, 74);
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2013, 3, 14, 10, 1, 42, 146);
            this.ucDateTimeInterval1.TabIndex = 15;
            // 
            // FrmZSTParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 391);
            this.Controls.Add(this.group2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtMessage);
            this.Name = "FrmZSTParameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "中山通消费记录同步";
            this.Load += new System.EventHandler(this.FrmZSTParameter_Load);
            this.groupBox1.ResumeLayout(false);
            this.group2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSetUploadTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker8;
        private System.Windows.Forms.DateTimePicker dateTimePicker7;
        private System.Windows.Forms.DateTimePicker dateTimePicker6;
        private System.Windows.Forms.DateTimePicker dateTimePicker5;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.Button btn_ConsumptionReport;
        private System.Windows.Forms.GroupBox group2;
        private UserControls.UCDateTimeInterval ucDateTimeInterval1;
    }
}

