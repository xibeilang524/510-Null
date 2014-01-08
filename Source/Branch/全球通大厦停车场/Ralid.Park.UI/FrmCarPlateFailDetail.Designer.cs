namespace Ralid.Park.UI
{
    partial class FrmCarPlateFailDetail
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ucVideoes = new Ralid.Park.UserControls.VideoPanels.UCVideoListView();
            this.picIn = new Ralid.Park.UserControls.UCPictureListView();
            this.picOut = new Ralid.Park.UserControls.UCPictureListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblEnterCarPlate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblExitCarPlate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblEnterDateTime = new System.Windows.Forms.Label();
            this.lblExitDateTime = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(718, 226);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 36);
            this.btnClose.TabIndex = 70;
            this.btnClose.Text = "关闭窗口[F12]";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucVideoes
            // 
            this.ucVideoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucVideoes.Location = new System.Drawing.Point(12, 5);
            this.ucVideoes.Name = "ucVideoes";
            this.ucVideoes.Size = new System.Drawing.Size(450, 310);
            this.ucVideoes.TabIndex = 69;
            // 
            // picIn
            // 
            this.picIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIn.Location = new System.Drawing.Point(6, 19);
            this.picIn.Name = "picIn";
            this.picIn.Size = new System.Drawing.Size(450, 310);
            this.picIn.TabIndex = 68;
            // 
            // picOut
            // 
            this.picOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOut.Location = new System.Drawing.Point(6, 19);
            this.picOut.Name = "picOut";
            this.picOut.Size = new System.Drawing.Size(450, 310);
            this.picOut.TabIndex = 68;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.92683F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.07317F));
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblEnterCarPlate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblExitCarPlate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblEnterDateTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblExitDateTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtCardID, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(607, 81);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(206, 110);
            this.tableLayoutPanel1.TabIndex = 71;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(4, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "卡号:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(4, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 20);
            this.label10.TabIndex = 36;
            this.label10.Text = "入场车牌:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEnterCarPlate
            // 
            this.lblEnterCarPlate.AutoSize = true;
            this.lblEnterCarPlate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnterCarPlate.Font = new System.Drawing.Font("宋体", 9F);
            this.lblEnterCarPlate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEnterCarPlate.Location = new System.Drawing.Point(92, 22);
            this.lblEnterCarPlate.Name = "lblEnterCarPlate";
            this.lblEnterCarPlate.Size = new System.Drawing.Size(110, 20);
            this.lblEnterCarPlate.TabIndex = 38;
            this.lblEnterCarPlate.Text = "lblEnterCarPlate";
            this.lblEnterCarPlate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(4, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 20);
            this.label11.TabIndex = 37;
            this.label11.Text = "出场车牌:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExitCarPlate
            // 
            this.lblExitCarPlate.AutoSize = true;
            this.lblExitCarPlate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExitCarPlate.Font = new System.Drawing.Font("宋体", 9F);
            this.lblExitCarPlate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExitCarPlate.Location = new System.Drawing.Point(92, 43);
            this.lblExitCarPlate.Name = "lblExitCarPlate";
            this.lblExitCarPlate.Size = new System.Drawing.Size(110, 20);
            this.lblExitCarPlate.TabIndex = 40;
            this.lblExitCarPlate.Text = "lblExitCarPlate";
            this.lblExitCarPlate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(4, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "入场时间:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(4, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 24);
            this.label12.TabIndex = 48;
            this.label12.Text = "出场时间:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEnterDateTime
            // 
            this.lblEnterDateTime.AutoSize = true;
            this.lblEnterDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnterDateTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lblEnterDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEnterDateTime.Location = new System.Drawing.Point(92, 64);
            this.lblEnterDateTime.Name = "lblEnterDateTime";
            this.lblEnterDateTime.Size = new System.Drawing.Size(110, 20);
            this.lblEnterDateTime.TabIndex = 47;
            this.lblEnterDateTime.Text = "lblEnterDateTime";
            this.lblEnterDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExitDateTime
            // 
            this.lblExitDateTime.AutoSize = true;
            this.lblExitDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExitDateTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lblExitDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExitDateTime.Location = new System.Drawing.Point(92, 85);
            this.lblExitDateTime.Name = "lblExitDateTime";
            this.lblExitDateTime.Size = new System.Drawing.Size(110, 24);
            this.lblExitDateTime.TabIndex = 26;
            this.lblExitDateTime.Text = "lblExitDate";
            this.lblExitDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCardID
            // 
            this.txtCardID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCardID.Enabled = false;
            this.txtCardID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardID.Location = new System.Drawing.Point(92, 4);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(110, 14);
            this.txtCardID.TabIndex = 77;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picIn);
            this.groupBox1.Location = new System.Drawing.Point(12, 321);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 335);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入场图片";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picOut);
            this.groupBox2.Location = new System.Drawing.Point(479, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 335);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出场图片";
            // 
            // btnClear
            // 
            this.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClear.Location = new System.Drawing.Point(607, 226);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(95, 36);
            this.btnClear.TabIndex = 70;
            this.btnClear.Text = "清除事件[F11]";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FrmCarPlateFailDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 662);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucVideoes);
            this.Name = "FrmCarPlateFailDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车牌对比详细信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCarPlateFailDetail_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCarPlateFailDetail_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private UserControls.VideoPanels.UCVideoListView ucVideoes;
        private UserControls.UCPictureListView picIn;
        private UserControls.UCPictureListView picOut;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblEnterCarPlate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblExitCarPlate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblEnterDateTime;
        private System.Windows.Forms.Label lblExitDateTime;
        private GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClear;
    }
}