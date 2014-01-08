namespace ECardInterface
{
    partial class FrmParameter
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk4 = new System.Windows.Forms.CheckBox();
            this.dtLimitationEnd4 = new System.Windows.Forms.DateTimePicker();
            this.dtLimitationBegin4 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chk2 = new System.Windows.Forms.CheckBox();
            this.dtLimitationEnd2 = new System.Windows.Forms.DateTimePicker();
            this.dtLimitationBegin2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.chk3 = new System.Windows.Forms.CheckBox();
            this.dtLimitationEnd3 = new System.Windows.Forms.DateTimePicker();
            this.dtLimitationBegin3 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.chk1 = new System.Windows.Forms.CheckBox();
            this.dtLimitationEnd1 = new System.Windows.Forms.DateTimePicker();
            this.dtLimitationBegin1 = new System.Windows.Forms.DateTimePicker();
            this.label43 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(231, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(322, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk4);
            this.groupBox1.Controls.Add(this.dtLimitationEnd4);
            this.groupBox1.Controls.Add(this.dtLimitationBegin4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chk2);
            this.groupBox1.Controls.Add(this.dtLimitationEnd2);
            this.groupBox1.Controls.Add(this.dtLimitationBegin2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chk3);
            this.groupBox1.Controls.Add(this.dtLimitationEnd3);
            this.groupBox1.Controls.Add(this.dtLimitationBegin3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chk1);
            this.groupBox1.Controls.Add(this.dtLimitationEnd1);
            this.groupBox1.Controls.Add(this.dtLimitationBegin1);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 78);
            this.groupBox1.TabIndex = 285;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "停车场满位时需要提醒用户的时间段";
            // 
            // chk4
            // 
            this.chk4.AutoSize = true;
            this.chk4.Location = new System.Drawing.Point(205, 50);
            this.chk4.Name = "chk4";
            this.chk4.Size = new System.Drawing.Size(15, 14);
            this.chk4.TabIndex = 300;
            this.chk4.UseVisualStyleBackColor = true;
            // 
            // dtLimitationEnd4
            // 
            this.dtLimitationEnd4.CustomFormat = "HH:mm";
            this.dtLimitationEnd4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationEnd4.Location = new System.Drawing.Point(301, 47);
            this.dtLimitationEnd4.Name = "dtLimitationEnd4";
            this.dtLimitationEnd4.ShowUpDown = true;
            this.dtLimitationEnd4.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationEnd4.TabIndex = 299;
            this.dtLimitationEnd4.Value = new System.DateTime(2010, 12, 7, 7, 0, 0, 0);
            // 
            // dtLimitationBegin4
            // 
            this.dtLimitationBegin4.CustomFormat = "HH:mm";
            this.dtLimitationBegin4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationBegin4.Location = new System.Drawing.Point(222, 47);
            this.dtLimitationBegin4.Name = "dtLimitationBegin4";
            this.dtLimitationBegin4.ShowUpDown = true;
            this.dtLimitationBegin4.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationBegin4.TabIndex = 298;
            this.dtLimitationBegin4.Value = new System.DateTime(2010, 12, 7, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(287, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 297;
            this.label4.Text = "-";
            // 
            // chk2
            // 
            this.chk2.AutoSize = true;
            this.chk2.Location = new System.Drawing.Point(205, 23);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(15, 14);
            this.chk2.TabIndex = 296;
            this.chk2.UseVisualStyleBackColor = true;
            // 
            // dtLimitationEnd2
            // 
            this.dtLimitationEnd2.CustomFormat = "HH:mm";
            this.dtLimitationEnd2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationEnd2.Location = new System.Drawing.Point(301, 20);
            this.dtLimitationEnd2.Name = "dtLimitationEnd2";
            this.dtLimitationEnd2.ShowUpDown = true;
            this.dtLimitationEnd2.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationEnd2.TabIndex = 295;
            this.dtLimitationEnd2.Value = new System.DateTime(2010, 12, 7, 7, 0, 0, 0);
            // 
            // dtLimitationBegin2
            // 
            this.dtLimitationBegin2.CustomFormat = "HH:mm";
            this.dtLimitationBegin2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationBegin2.Location = new System.Drawing.Point(222, 20);
            this.dtLimitationBegin2.Name = "dtLimitationBegin2";
            this.dtLimitationBegin2.ShowUpDown = true;
            this.dtLimitationBegin2.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationBegin2.TabIndex = 294;
            this.dtLimitationBegin2.Value = new System.DateTime(2010, 12, 7, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(287, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 293;
            this.label5.Text = "-";
            // 
            // chk3
            // 
            this.chk3.AutoSize = true;
            this.chk3.Location = new System.Drawing.Point(10, 50);
            this.chk3.Name = "chk3";
            this.chk3.Size = new System.Drawing.Size(15, 14);
            this.chk3.TabIndex = 292;
            this.chk3.UseVisualStyleBackColor = true;
            // 
            // dtLimitationEnd3
            // 
            this.dtLimitationEnd3.CustomFormat = "HH:mm";
            this.dtLimitationEnd3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationEnd3.Location = new System.Drawing.Point(107, 47);
            this.dtLimitationEnd3.Name = "dtLimitationEnd3";
            this.dtLimitationEnd3.ShowUpDown = true;
            this.dtLimitationEnd3.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationEnd3.TabIndex = 291;
            this.dtLimitationEnd3.Value = new System.DateTime(2010, 12, 7, 7, 0, 0, 0);
            // 
            // dtLimitationBegin3
            // 
            this.dtLimitationBegin3.CustomFormat = "HH:mm";
            this.dtLimitationBegin3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationBegin3.Location = new System.Drawing.Point(28, 47);
            this.dtLimitationBegin3.Name = "dtLimitationBegin3";
            this.dtLimitationBegin3.ShowUpDown = true;
            this.dtLimitationBegin3.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationBegin3.TabIndex = 290;
            this.dtLimitationBegin3.Value = new System.DateTime(2010, 12, 7, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(93, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 289;
            this.label1.Text = "-";
            // 
            // chk1
            // 
            this.chk1.AutoSize = true;
            this.chk1.Location = new System.Drawing.Point(10, 23);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(15, 14);
            this.chk1.TabIndex = 288;
            this.chk1.UseVisualStyleBackColor = true;
            // 
            // dtLimitationEnd1
            // 
            this.dtLimitationEnd1.CustomFormat = "HH:mm";
            this.dtLimitationEnd1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationEnd1.Location = new System.Drawing.Point(107, 20);
            this.dtLimitationEnd1.Name = "dtLimitationEnd1";
            this.dtLimitationEnd1.ShowUpDown = true;
            this.dtLimitationEnd1.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationEnd1.TabIndex = 287;
            this.dtLimitationEnd1.Value = new System.DateTime(2010, 12, 7, 7, 0, 0, 0);
            // 
            // dtLimitationBegin1
            // 
            this.dtLimitationBegin1.CustomFormat = "HH:mm";
            this.dtLimitationBegin1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLimitationBegin1.Location = new System.Drawing.Point(28, 20);
            this.dtLimitationBegin1.Name = "dtLimitationBegin1";
            this.dtLimitationBegin1.ShowUpDown = true;
            this.dtLimitationBegin1.Size = new System.Drawing.Size(62, 21);
            this.dtLimitationBegin1.TabIndex = 286;
            this.dtLimitationBegin1.Value = new System.DateTime(2010, 12, 7, 0, 0, 0, 0);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label43.Location = new System.Drawing.Point(93, 24);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(11, 12);
            this.label43.TabIndex = 285;
            this.label43.Text = "-";
            // 
            // FrmParameter
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(412, 145);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "FrmParameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.FrmParameter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk4;
        private System.Windows.Forms.DateTimePicker dtLimitationEnd4;
        private System.Windows.Forms.DateTimePicker dtLimitationBegin4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chk2;
        private System.Windows.Forms.DateTimePicker dtLimitationEnd2;
        private System.Windows.Forms.DateTimePicker dtLimitationBegin2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chk3;
        private System.Windows.Forms.DateTimePicker dtLimitationEnd3;
        private System.Windows.Forms.DateTimePicker dtLimitationBegin3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk1;
        private System.Windows.Forms.DateTimePicker dtLimitationEnd1;
        private System.Windows.Forms.DateTimePicker dtLimitationBegin1;
        private System.Windows.Forms.Label label43;
    }
}