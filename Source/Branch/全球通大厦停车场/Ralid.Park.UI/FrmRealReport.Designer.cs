namespace Ralid.Park.UI
{
    partial class FrmRealReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRealReport));
            this.eventList = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.SuspendLayout();
            // 
            // eventList
            // 
            resources.ApplyResources(this.eventList, "eventList");
            this.eventList.FormattingEnabled = true;
            this.eventList.Name = "eventList";
            this.eventList.DoubleClick += new System.EventHandler(this.eventList_DoubleClick);
            // 
            // FrmRealReport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eventList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmRealReport";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.EventReportListBox eventList;
    }
}