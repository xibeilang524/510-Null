namespace PreferentialSystem
{
    partial class FrmPREWorkstationDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtWorkstationName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWorkstationDesc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(46, 122);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(155, 122);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "工作站名称:";
            // 
            // txtWorkstationName
            // 
            this.txtWorkstationName.Location = new System.Drawing.Point(89, 24);
            this.txtWorkstationName.Name = "txtWorkstationName";
            this.txtWorkstationName.Size = new System.Drawing.Size(176, 21);
            this.txtWorkstationName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "工作站描述:";
            // 
            // txtWorkstationDesc
            // 
            this.txtWorkstationDesc.Location = new System.Drawing.Point(89, 49);
            this.txtWorkstationDesc.Multiline = true;
            this.txtWorkstationDesc.Name = "txtWorkstationDesc";
            this.txtWorkstationDesc.Size = new System.Drawing.Size(176, 67);
            this.txtWorkstationDesc.TabIndex = 5;
            // 
            // FrmPREWorkstationDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 157);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWorkstationName);
            this.Controls.Add(this.txtWorkstationDesc);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmPREWorkstationDetail";
            this.Text = "FrmPREWorkstationDetail";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtWorkstationDesc, 0);
            this.Controls.SetChildIndex(this.txtWorkstationName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWorkstationName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWorkstationDesc;
    }
}