namespace Ralid.Park.UI
{
    partial class FrmCarPlateManualEnter
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
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVisitor = new System.Windows.Forms.Button();
            this.comVisitor = new System.Windows.Forms.ComboBox();
            this.txtCarplate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVisitor = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkNotPlate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(14, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(527, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "已登记车辆 ---------------------------------------------------------------------------" +
                "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(14, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(527, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "临时车辆 ----------------------------------------------------------------------------" +
                "--";
            // 
            // btnVisitor
            // 
            this.btnVisitor.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btnVisitor.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnVisitor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVisitor.Location = new System.Drawing.Point(378, 190);
            this.btnVisitor.Name = "btnVisitor";
            this.btnVisitor.Size = new System.Drawing.Size(153, 37);
            this.btnVisitor.TabIndex = 7;
            this.btnVisitor.Text = "确认";
            this.btnVisitor.UseVisualStyleBackColor = true;
            this.btnVisitor.Click += new System.EventHandler(this.btnVisitor_Click);
            // 
            // comVisitor
            // 
            this.comVisitor.Font = new System.Drawing.Font("宋体", 16.75F, System.Drawing.FontStyle.Bold);
            this.comVisitor.FormattingEnabled = true;
            this.comVisitor.Items.AddRange(new object[] {
            "",
            "粤",
            "京",
            "津",
            "渝",
            "沪",
            "冀",
            "豫",
            "鲁",
            "晋",
            "湘",
            "鄂",
            "桂",
            "贵",
            "赣",
            "云",
            "川",
            "闽",
            "藏",
            "陕",
            "甘",
            "宁",
            "青",
            "蒙",
            "辽",
            "吉",
            "黑",
            "新",
            "浙",
            "苏",
            "皖",
            "琼",
            "台"});
            this.comVisitor.Location = new System.Drawing.Point(156, 193);
            this.comVisitor.Name = "comVisitor";
            this.comVisitor.Size = new System.Drawing.Size(49, 30);
            this.comVisitor.TabIndex = 5;
            // 
            // txtCarplate
            // 
            this.txtCarplate.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.txtCarplate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCarplate.Location = new System.Drawing.Point(154, 66);
            this.txtCarplate.Name = "txtCarplate";
            this.txtCarplate.Size = new System.Drawing.Size(218, 30);
            this.txtCarplate.TabIndex = 1;
            this.txtCarplate.TextChanged += new System.EventHandler(this.txtCarplate_TextChanged);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(14, 251);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 84);
            this.listBox1.TabIndex = 2;
            this.listBox1.Visible = false;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(55, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "请输入车牌号码:";
            // 
            // txtVisitor
            // 
            this.txtVisitor.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.txtVisitor.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtVisitor.Location = new System.Drawing.Point(209, 193);
            this.txtVisitor.Name = "txtVisitor";
            this.txtVisitor.Size = new System.Drawing.Size(163, 30);
            this.txtVisitor.TabIndex = 6;
            this.txtVisitor.Enter += new System.EventHandler(this.txtVisitor_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(55, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "请输入临时车牌:";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btnOk.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point(378, 63);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(153, 37);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkNotPlate
            // 
            this.chkNotPlate.AutoSize = true;
            this.chkNotPlate.Location = new System.Drawing.Point(57, 163);
            this.chkNotPlate.Name = "chkNotPlate";
            this.chkNotPlate.Size = new System.Drawing.Size(84, 16);
            this.chkNotPlate.TabIndex = 4;
            this.chkNotPlate.Text = "车辆无车牌";
            this.chkNotPlate.UseVisualStyleBackColor = true;
            this.chkNotPlate.CheckedChanged += new System.EventHandler(this.chkNotPlate_CheckedChanged);
            // 
            // FrmCarPlateManualEnter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 286);
            this.Controls.Add(this.chkNotPlate);
            this.Controls.Add(this.btnVisitor);
            this.Controls.Add(this.comVisitor);
            this.Controls.Add(this.txtCarplate);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVisitor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmCarPlateManualEnter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车辆入场登记";
            this.Load += new System.EventHandler(this.FrmCarPlateManualEnter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVisitor;
        private System.Windows.Forms.ComboBox comVisitor;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarplate;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private GeneralLibrary.WinformControl.DBCTextBox txtVisitor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkNotPlate;
    }
}