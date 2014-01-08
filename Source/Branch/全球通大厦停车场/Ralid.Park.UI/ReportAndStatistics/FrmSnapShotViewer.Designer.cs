namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmSnapShotViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSnapShotViewer));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.plEvent = new Ralid.Park.UserControls.UCPictureListView();
            this.plLastEvent = new Ralid.Park.UserControls.UCPictureListView();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPre
            // 
            resources.ApplyResources(this.btnPre, "btnPre");
            this.btnPre.Name = "btnPre";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // plEvent
            // 
            resources.ApplyResources(this.plEvent, "plEvent");
            this.plEvent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plEvent.Name = "plEvent";
            // 
            // plLastEvent
            // 
            resources.ApplyResources(this.plLastEvent, "plLastEvent");
            this.plLastEvent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plLastEvent.Name = "plLastEvent";
            // 
            // FrmSnapShotViewer
            // 
            this.AcceptButton = this.btnNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.plLastEvent);
            this.Controls.Add(this.plEvent);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnPre);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSnapShotViewer";
            this.Load += new System.EventHandler(this.FrmCardEventImage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private UserControls.UCPictureListView plEvent;
        private UserControls.UCPictureListView plLastEvent;
    }
}