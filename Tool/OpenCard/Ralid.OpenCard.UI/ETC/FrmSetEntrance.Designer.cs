namespace Ralid.OpenCard.UI.ETC
{
    partial class FrmSetEntrance
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.comEntrance = new Ralid.Park.UserControls.EntranceComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.chkDisableRSU = new System.Windows.Forms.CheckBox();
            this.chkDisableReader = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 32);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(89, 131);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 32);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // comEntrance
            // 
            this.comEntrance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comEntrance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comEntrance.FormattingEnabled = true;
            this.comEntrance.Location = new System.Drawing.Point(24, 46);
            this.comEntrance.Name = "comEntrance";
            this.comEntrance.Size = new System.Drawing.Size(316, 24);
            this.comEntrance.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "停车场通道";
            // 
            // chkDisableRSU
            // 
            this.chkDisableRSU.AutoSize = true;
            this.chkDisableRSU.Location = new System.Drawing.Point(121, 83);
            this.chkDisableRSU.Name = "chkDisableRSU";
            this.chkDisableRSU.Size = new System.Drawing.Size(72, 16);
            this.chkDisableRSU.TabIndex = 10;
            this.chkDisableRSU.Text = "禁用天线";
            this.chkDisableRSU.UseVisualStyleBackColor = true;
            // 
            // chkDisableReader
            // 
            this.chkDisableReader.AutoSize = true;
            this.chkDisableReader.Checked = true;
            this.chkDisableReader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisableReader.Location = new System.Drawing.Point(225, 83);
            this.chkDisableReader.Name = "chkDisableReader";
            this.chkDisableReader.Size = new System.Drawing.Size(84, 16);
            this.chkDisableReader.TabIndex = 11;
            this.chkDisableReader.Text = "禁用读卡器";
            this.chkDisableReader.UseVisualStyleBackColor = true;
            // 
            // FrmSetEntrance
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 190);
            this.Controls.Add(this.chkDisableReader);
            this.Controls.Add(this.chkDisableRSU);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.comEntrance);
            this.Controls.Add(this.label2);
            this.Name = "FrmSetEntrance";
            this.Text = "设置停车场通道";
            this.Load += new System.EventHandler(this.FrmSetEntrance_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Park.UserControls.EntranceComboBox comEntrance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDisableRSU;
        private System.Windows.Forms.CheckBox chkDisableReader;
    }
}