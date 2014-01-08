namespace Ralid.Park.UI
{
    partial class FrmSnapShoter
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
            this.videoGrid = new Ralid.Park.UserControls.VideoPanels.UCVideoPanelGrid();
            this.SuspendLayout();
            // 
            // videoGrid
            // 
            this.videoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoGrid.Enabled = false;
            this.videoGrid.Location = new System.Drawing.Point(0, 0);
            this.videoGrid.Name = "videoGrid";
            this.videoGrid.Size = new System.Drawing.Size(913, 674);
            this.videoGrid.TabIndex = 0;
            // 
            // FrmSnapShoter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(913, 674);
            this.Controls.Add(this.videoGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSnapShoter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmSnapShoter";
            this.ResumeLayout(false);

        }

        #endregion

        private Ralid.Park.UserControls.VideoPanels.UCVideoPanelGrid videoGrid;

    }
}