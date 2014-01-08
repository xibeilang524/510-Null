namespace Ralid.Park.UI
{
    partial class FrmCardsDownloadTest
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cardView = new Ralid.Park.UserControls.CardGridView(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStart = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtEnd = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGet);
            this.panel1.Controls.Add(this.btnDownload);
            this.panel1.Controls.Add(this.txtEnd);
            this.panel1.Controls.Add(this.txtStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 85);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblMsg);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 448);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(846, 27);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cardView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(846, 363);
            this.panel3.TabIndex = 2;
            // 
            // cardView
            // 
            this.cardView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardView.Location = new System.Drawing.Point(0, 0);
            this.cardView.Name = "cardView";
            this.cardView.RowTemplate.Height = 23;
            this.cardView.Size = new System.Drawing.Size(846, 363);
            this.cardView.TabIndex = 24;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(663, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(672, 7);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(41, 12);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "lblMsg";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "结束编号：";
            // 
            // txtStart
            // 
            this.txtStart.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtStart.Location = new System.Drawing.Point(97, 22);
            this.txtStart.MaxValue = 2147483647;
            this.txtStart.MinValue = 1;
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(120, 21);
            this.txtStart.TabIndex = 1;
            // 
            // txtEnd
            // 
            this.txtEnd.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtEnd.Location = new System.Drawing.Point(328, 22);
            this.txtEnd.MaxValue = 2147483647;
            this.txtEnd.MinValue = 1;
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(123, 21);
            this.txtEnd.TabIndex = 1;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(521, 22);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(96, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "下发";
            this.btnDownload.UseVisualStyleBackColor = true;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(658, 22);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(96, 23);
            this.btnGet.TabIndex = 2;
            this.btnGet.Text = "获取";
            this.btnGet.UseVisualStyleBackColor = true;
            // 
            // FrmCardsDownloadTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 475);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCardsDownloadTest";
            this.Text = "卡片下载测试";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnDownload;
        private GeneralLibrary.WinformControl.IntergerTextBox txtEnd;
        private GeneralLibrary.WinformControl.IntergerTextBox txtStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel3;
        private UserControls.CardGridView cardView;
    }
}