namespace Ralid.Park.UI
{
    partial class FrmAccessGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccessGroup));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHoliday = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.entranceTree = new Ralid.Park.UserControls.HardwareTree(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkHoliday);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.entranceTree);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dtEnd);
            this.groupBox1.Controls.Add(this.dtStart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkHoliday
            // 
            resources.ApplyResources(this.chkHoliday, "chkHoliday");
            this.chkHoliday.Checked = true;
            this.chkHoliday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHoliday.Name = "chkHoliday";
            this.chkHoliday.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // entranceTree
            // 
            this.entranceTree.CheckBoxes = true;
            resources.ApplyResources(this.entranceTree, "entranceTree");
            this.entranceTree.Name = "entranceTree";
            this.entranceTree.ShowEntrance = true;
            this.entranceTree.ShowVideoSource = false;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // dtEnd
            // 
            resources.ApplyResources(this.dtEnd, "dtEnd");
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.ShowUpDown = true;
            // 
            // dtStart
            // 
            resources.ApplyResources(this.dtStart, "dtStart");
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Name = "dtStart";
            this.dtStart.ShowUpDown = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // butOK
            // 
            resources.ApplyResources(this.butOK, "butOK");
            this.butOK.Name = "butOK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.butCancel, "butCancel");
            this.butCancel.Name = "butCancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // FrmAccessGroup
            // 
            this.AcceptButton = this.butOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAccessGroup";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmAccessGroup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butCancel;
        private Ralid.Park.UserControls.HardwareTree entranceTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkHoliday;
    }
}