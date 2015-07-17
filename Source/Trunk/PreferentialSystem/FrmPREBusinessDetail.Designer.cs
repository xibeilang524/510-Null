namespace PreferentialSystem
{
    partial class FrmPREBusinessDetail
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
            this.txtBusinessName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBusinessDesc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(58, 156);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(164, 156);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "商家名称:";
            // 
            // txtBusinessName
            // 
            this.txtBusinessName.Location = new System.Drawing.Point(77, 17);
            this.txtBusinessName.MaxLength = 50;
            this.txtBusinessName.Name = "txtBusinessName";
            this.txtBusinessName.Size = new System.Drawing.Size(207, 21);
            this.txtBusinessName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "商家描述:";
            // 
            // txtBusinessDesc
            // 
            this.txtBusinessDesc.Location = new System.Drawing.Point(77, 55);
            this.txtBusinessDesc.MaxLength = 200;
            this.txtBusinessDesc.Multiline = true;
            this.txtBusinessDesc.Name = "txtBusinessDesc";
            this.txtBusinessDesc.Size = new System.Drawing.Size(207, 95);
            this.txtBusinessDesc.TabIndex = 5;
            // 
            // FrmPREBusinessDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 191);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBusinessName);
            this.Controls.Add(this.txtBusinessDesc);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmPREBusinessDetail";
            this.Text = "FrmPREBusinessDetail";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtBusinessDesc, 0);
            this.Controls.SetChildIndex(this.txtBusinessName, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBusinessName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBusinessDesc;
    }
}