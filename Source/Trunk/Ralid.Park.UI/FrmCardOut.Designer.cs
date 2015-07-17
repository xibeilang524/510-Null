namespace Ralid.Park.UI
{
    partial class FrmCardOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardOut));
            this.label1 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.ComboBox();
            this.btnOut = new System.Windows.Forms.Button();
            this.listMsg = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.ucEntrance1 = new Ralid.Park.UserControls.UCEntrance();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.FormattingEnabled = true;
            this.txtMemo.Name = "txtMemo";
            // 
            // btnOut
            // 
            resources.ApplyResources(this.btnOut, "btnOut");
            this.btnOut.Name = "btnOut";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // listMsg
            // 
            resources.ApplyResources(this.listMsg, "listMsg");
            this.listMsg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listMsg.FormattingEnabled = true;
            this.listMsg.Name = "listMsg";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Name = "label2";
            // 
            // ucEntrance1
            // 
            resources.ApplyResources(this.ucEntrance1, "ucEntrance1");
            this.ucEntrance1.Name = "ucEntrance1";
            this.ucEntrance1.OnlyExit = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.ForeColor = System.Drawing.Color.Red;
            this.txtCount.Name = "txtCount";
            // 
            // FrmCardOut
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucEntrance1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listMsg);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCardOut";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardOut_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardOut_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtMemo;
        private System.Windows.Forms.Button btnOut;
        private UserControls.EventReportListBox listMsg;
        private System.Windows.Forms.Label label2;
        private UserControls.UCEntrance ucEntrance1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtCount;
    }
}