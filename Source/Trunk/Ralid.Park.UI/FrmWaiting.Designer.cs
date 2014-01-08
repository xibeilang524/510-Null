namespace Ralid.Park.UI
{
    partial class FrmWaiting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWaiting));
            this.panelResult = new System.Windows.Forms.Panel();
            this.dataResult = new System.Windows.Forms.DataGridView();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbtips = new System.Windows.Forms.Label();
            this.panelResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataResult)).BeginInit();
            this.panelProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelResult
            // 
            resources.ApplyResources(this.panelResult, "panelResult");
            this.panelResult.Controls.Add(this.dataResult);
            this.panelResult.Name = "panelResult";
            // 
            // dataResult
            // 
            resources.ApplyResources(this.dataResult, "dataResult");
            this.dataResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataResult.Name = "dataResult";
            this.dataResult.RowTemplate.Height = 23;
            // 
            // panelProgress
            // 
            resources.ApplyResources(this.panelProgress, "panelProgress");
            this.panelProgress.Controls.Add(this.progressBar1);
            this.panelProgress.Controls.Add(this.lbtips);
            this.panelProgress.Name = "panelProgress";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // lbtips
            // 
            resources.ApplyResources(this.lbtips, "lbtips");
            this.lbtips.Name = "lbtips";
            // 
            // FrmWaiting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panelResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmWaiting";
            this.panelResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataResult)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.DataGridView dataResult;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbtips;


    }
}