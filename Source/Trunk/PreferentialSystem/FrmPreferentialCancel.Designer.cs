namespace PreferentialSystem
{
    partial class FrmPreferentialCancel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOwnerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCardWarning = new System.Windows.Forms.Label();
            this.txtCarPlate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCost3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBusiness3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCost2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBusiness2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCost1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBueiness1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCancelReason = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtEntranceTime = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEntranceTime);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtOwnerName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCardWarning);
            this.groupBox1.Controls.Add(this.txtCarPlate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCardID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 89);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "卡片信息";
            // 
            // txtOwnerName
            // 
            this.txtOwnerName.Enabled = false;
            this.txtOwnerName.Location = new System.Drawing.Point(394, 14);
            this.txtOwnerName.Name = "txtOwnerName";
            this.txtOwnerName.ReadOnly = true;
            this.txtOwnerName.Size = new System.Drawing.Size(100, 21);
            this.txtOwnerName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(341, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "持卡人:";
            // 
            // lblCardWarning
            // 
            this.lblCardWarning.AutoSize = true;
            this.lblCardWarning.ForeColor = System.Drawing.Color.Red;
            this.lblCardWarning.Location = new System.Drawing.Point(6, 64);
            this.lblCardWarning.Name = "lblCardWarning";
            this.lblCardWarning.Size = new System.Drawing.Size(0, 12);
            this.lblCardWarning.TabIndex = 4;
            // 
            // txtCarPlate
            // 
            this.txtCarPlate.Enabled = false;
            this.txtCarPlate.Location = new System.Drawing.Point(221, 14);
            this.txtCarPlate.Name = "txtCarPlate";
            this.txtCarPlate.ReadOnly = true;
            this.txtCarPlate.Size = new System.Drawing.Size(100, 21);
            this.txtCarPlate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "车牌号:";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(47, 14);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(100, 21);
            this.txtCardID.TabIndex = 1;
            this.txtCardID.TextChanged += new System.EventHandler(this.txtCardID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "卡号:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTime);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtNotes);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtCost3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtBusiness3);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtCost2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtBusiness2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCost1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtBueiness1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtHour);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(772, 247);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "最近一次优惠记录";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(307, 25);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(135, 21);
            this.txtTime.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(230, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 16;
            this.label12.Text = "优惠时间:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(71, 169);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ReadOnly = true;
            this.txtNotes.Size = new System.Drawing.Size(371, 72);
            this.txtNotes.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 172);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "备注:";
            // 
            // txtCost3
            // 
            this.txtCost3.Location = new System.Drawing.Point(307, 138);
            this.txtCost3.Name = "txtCost3";
            this.txtCost3.ReadOnly = true;
            this.txtCost3.Size = new System.Drawing.Size(135, 21);
            this.txtCost3.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(230, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "消费金额:";
            // 
            // txtBusiness3
            // 
            this.txtBusiness3.Location = new System.Drawing.Point(71, 138);
            this.txtBusiness3.Name = "txtBusiness3";
            this.txtBusiness3.ReadOnly = true;
            this.txtBusiness3.Size = new System.Drawing.Size(100, 21);
            this.txtBusiness3.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "商铺名称:";
            // 
            // txtCost2
            // 
            this.txtCost2.Location = new System.Drawing.Point(307, 101);
            this.txtCost2.Name = "txtCost2";
            this.txtCost2.ReadOnly = true;
            this.txtCost2.Size = new System.Drawing.Size(135, 21);
            this.txtCost2.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(230, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "消费金额:";
            // 
            // txtBusiness2
            // 
            this.txtBusiness2.Location = new System.Drawing.Point(71, 101);
            this.txtBusiness2.Name = "txtBusiness2";
            this.txtBusiness2.ReadOnly = true;
            this.txtBusiness2.Size = new System.Drawing.Size(100, 21);
            this.txtBusiness2.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "商铺名称:";
            // 
            // txtCost1
            // 
            this.txtCost1.Location = new System.Drawing.Point(307, 61);
            this.txtCost1.Name = "txtCost1";
            this.txtCost1.ReadOnly = true;
            this.txtCost1.Size = new System.Drawing.Size(135, 21);
            this.txtCost1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "消费金额:";
            // 
            // txtBueiness1
            // 
            this.txtBueiness1.Location = new System.Drawing.Point(71, 61);
            this.txtBueiness1.Name = "txtBueiness1";
            this.txtBueiness1.ReadOnly = true;
            this.txtBueiness1.Size = new System.Drawing.Size(100, 21);
            this.txtBueiness1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "商铺名称:";
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(71, 25);
            this.txtHour.Name = "txtHour";
            this.txtHour.ReadOnly = true;
            this.txtHour.Size = new System.Drawing.Size(76, 21);
            this.txtHour.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "优惠时数:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 381);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 3;
            this.label13.Text = "取消原因:";
            // 
            // txtCancelReason
            // 
            this.txtCancelReason.Location = new System.Drawing.Point(83, 378);
            this.txtCancelReason.Multiline = true;
            this.txtCancelReason.Name = "txtCancelReason";
            this.txtCancelReason.Size = new System.Drawing.Size(371, 55);
            this.txtCancelReason.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(460, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消优惠";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(551, 395);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtEntranceTime
            // 
            this.txtEntranceTime.Enabled = false;
            this.txtEntranceTime.Location = new System.Drawing.Point(577, 14);
            this.txtEntranceTime.Name = "txtEntranceTime";
            this.txtEntranceTime.ReadOnly = true;
            this.txtEntranceTime.Size = new System.Drawing.Size(182, 21);
            this.txtEntranceTime.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(512, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 9;
            this.label14.Text = "入场时间:";
            // 
            // FrmPreferentialCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 445);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtCancelReason);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPreferentialCancel";
            this.Text = "优惠取消";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPreferentialCancel_FormClosed);
            this.Load += new System.EventHandler(this.FrmPreferentialCancel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOwnerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCardWarning;
        private System.Windows.Forms.TextBox txtCarPlate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBueiness1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCost1;
        private System.Windows.Forms.TextBox txtCost2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBusiness2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCost3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBusiness3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCancelReason;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtEntranceTime;
        private System.Windows.Forms.Label label14;
    }
}