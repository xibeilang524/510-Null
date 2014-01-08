namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCarFlowStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarFlowStatistics));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.carTypeComboBox1 = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.ucEntrance1 = new Ralid.Park.UserControls.UCEntrance();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtPerMonth = new System.Windows.Forms.RadioButton();
            this.rdPerDay = new System.Windows.Forms.RadioButton();
            this.rdPerHour = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            // 
            // btnSaveAs
            // 
            resources.ApplyResources(this.btnSaveAs, "btnSaveAs");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ucDateTimeInterval1
            // 
            resources.ApplyResources(this.ucDateTimeInterval1, "ucDateTimeInterval1");
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 9, 23, 59, 59, 0);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 9, 16, 56, 56, 625);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.carTypeComboBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ucEntrance1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // carTypeComboBox1
            // 
            resources.ApplyResources(this.carTypeComboBox1, "carTypeComboBox1");
            this.carTypeComboBox1.FormattingEnabled = true;
            this.carTypeComboBox1.Name = "carTypeComboBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ucEntrance1
            // 
            resources.ApplyResources(this.ucEntrance1, "ucEntrance1");
            this.ucEntrance1.Name = "ucEntrance1";
            // 
            // customDataGridView1
            // 
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.rtPerMonth);
            this.groupBox3.Controls.Add(this.rdPerDay);
            this.groupBox3.Controls.Add(this.rdPerHour);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // rtPerMonth
            // 
            resources.ApplyResources(this.rtPerMonth, "rtPerMonth");
            this.rtPerMonth.Name = "rtPerMonth";
            this.rtPerMonth.UseVisualStyleBackColor = true;
            // 
            // rdPerDay
            // 
            resources.ApplyResources(this.rdPerDay, "rdPerDay");
            this.rdPerDay.Checked = true;
            this.rdPerDay.Name = "rdPerDay";
            this.rdPerDay.TabStop = true;
            this.rdPerDay.UseVisualStyleBackColor = true;
            // 
            // rdPerHour
            // 
            resources.ApplyResources(this.rdPerHour, "rdPerHour");
            this.rdPerHour.Name = "rdPerHour";
            this.rdPerHour.UseVisualStyleBackColor = true;
            // 
            // FrmCarFlowStatistics
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmCarFlowStatistics";
            this.Load += new System.EventHandler(this.FrmCarFlowStatistics_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rtPerMonth;
        private System.Windows.Forms.RadioButton rdPerDay;
        private System.Windows.Forms.RadioButton rdPerHour;
        private Ralid.Park.UserControls.UCEntrance ucEntrance1;
        private UserControls.CarTypeComboBox carTypeComboBox1;
        private System.Windows.Forms.Label label1;
    }
}