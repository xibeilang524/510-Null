namespace Ralid.Park.DownloadCard
{
    partial class FrmDownloadCard
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
            this.btnOk = new System.Windows.Forms.Button();
            this.hardwareTree1 = new Ralid.Park.UserControls.HardwareTree(this.components);
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(298, 326);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(132, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "开始下发";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // hardwareTree1
            // 
            this.hardwareTree1.CheckBoxes = true;
            this.hardwareTree1.Dock = System.Windows.Forms.DockStyle.Left;
            this.hardwareTree1.Location = new System.Drawing.Point(0, 0);
            this.hardwareTree1.Name = "hardwareTree1";
            this.hardwareTree1.ShowVideoSource = false;
            this.hardwareTree1.Size = new System.Drawing.Size(292, 361);
            this.hardwareTree1.TabIndex = 3;
            // 
            // FrmDownloadCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 361);
            this.Controls.Add(this.hardwareTree1);
            this.Controls.Add(this.btnOk);
            this.Name = "FrmDownloadCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "下发卡片";
            this.Load += new System.EventHandler(this.FrmDownloadCard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private UserControls.HardwareTree hardwareTree1;
    }
}