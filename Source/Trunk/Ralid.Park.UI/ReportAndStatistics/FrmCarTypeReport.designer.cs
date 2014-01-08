namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCarTypeReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarTypeReport));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CollectGridView = new System.Windows.Forms.DataGridView();
            this.DetailGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCount = new Ralid.GeneralLibrary.WinformControl.DBCTextBox();
            this.txtPaid = new Ralid.GeneralLibrary.WinformControl.DBCTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cardTypeComboBox1 = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.workStationCombobox2 = new Ralid.Park.UserControls.StationNameComboBox(this.components);
            this.operatorComboBox2 = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucEntrance2 = new Ralid.Park.UserControls.UCEntrance();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval2 = new Ralid.Park.UserControls.UCDateTimeInterval();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CollectGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.CollectGridView);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.DetailGrid);
            // 
            // CollectGridView
            // 
            resources.ApplyResources(this.CollectGridView, "CollectGridView");
            this.CollectGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CollectGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CollectGridView.Name = "CollectGridView";
            this.CollectGridView.RowTemplate.Height = 23;
            // 
            // DetailGrid
            // 
            resources.ApplyResources(this.DetailGrid, "DetailGrid");
            this.DetailGrid.AllowUserToAddRows = false;
            this.DetailGrid.AllowUserToDeleteRows = false;
            this.DetailGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DetailGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DetailGrid.Name = "DetailGrid";
            this.DetailGrid.RowTemplate.Height = 23;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnQuery
            // 
            resources.ApplyResources(this.btnQuery, "btnQuery");
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Name = "panel1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.Name = "txtCount";
            // 
            // txtPaid
            // 
            resources.ApplyResources(this.txtPaid, "txtPaid");
            this.txtPaid.Name = "txtPaid";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cmbSort
            // 
            resources.ApplyResources(this.cmbSort, "cmbSort");
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            resources.GetString("cmbSort.Items"),
            resources.GetString("cmbSort.Items1"),
            resources.GetString("cmbSort.Items2")});
            this.cmbSort.Name = "cmbSort";
            // 
            // chkReverse
            // 
            resources.ApplyResources(this.chkReverse, "chkReverse");
            this.chkReverse.Checked = true;
            this.chkReverse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintPreview
            // 
            resources.ApplyResources(this.btnPrintPreview, "btnPrintPreview");
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.cardTypeComboBox1);
            this.groupBox1.Controls.Add(this.workStationCombobox2);
            this.groupBox1.Controls.Add(this.operatorComboBox2);
            this.groupBox1.Controls.Add(this.txtCarPlate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtCardID);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbSort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cardTypeComboBox1
            // 
            resources.ApplyResources(this.cardTypeComboBox1, "cardTypeComboBox1");
            this.cardTypeComboBox1.FormattingEnabled = true;
            this.cardTypeComboBox1.Name = "cardTypeComboBox1";
            // 
            // workStationCombobox2
            // 
            resources.ApplyResources(this.workStationCombobox2, "workStationCombobox2");
            this.workStationCombobox2.FormattingEnabled = true;
            this.workStationCombobox2.Name = "workStationCombobox2";
            // 
            // operatorComboBox2
            // 
            resources.ApplyResources(this.operatorComboBox2, "operatorComboBox2");
            this.operatorComboBox2.FormattingEnabled = true;
            this.operatorComboBox2.Name = "operatorComboBox2";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.ucEntrance2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // ucEntrance2
            // 
            resources.ApplyResources(this.ucEntrance2, "ucEntrance2");
            this.ucEntrance2.Name = "ucEntrance2";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.ucDateTimeInterval2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // ucDateTimeInterval2
            // 
            resources.ApplyResources(this.ucDateTimeInterval2, "ucDateTimeInterval2");
            this.ucDateTimeInterval2.EndDateTime = new System.DateTime(2012, 3, 7, 14, 53, 52, 399);
            this.ucDateTimeInterval2.Name = "ucDateTimeInterval2";
            this.ucDateTimeInterval2.ShowTime = true;
            this.ucDateTimeInterval2.StartDateTime = new System.DateTime(2012, 3, 7, 14, 53, 52, 402);
            // 
            // FrmCarTypeReport
            // 
            this.AcceptButton = this.btnQuery;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPrintPreview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.chkReverse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPaid);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnQuery);
            this.Name = "FrmCarTypeReport";
            this.Load += new System.EventHandler(this.FrmReportForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CollectGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private Ralid.GeneralLibrary .WinformControl .UCDateTimeInterval ucDateTimeInterval1;
        //private Ralid.Park.UserControls.OperatorComboBox operatorComboBox1;
        //private Ralid.Park.UserControls.WorkStationCombobox workStationCombobox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView DetailGrid;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCount;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtPaid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.CheckBox chkReverse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.DataGridView CollectGridView;
        //private Ralid.Park.UserControls.UCEntrance ucEntrance1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        //private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private Park.UserControls.UCEntrance ucEntrance2;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval2;
        private Park.UserControls.CardTypeComboBox cardTypeComboBox1;
        private Park.UserControls.StationNameComboBox workStationCombobox2;
        private Park.UserControls.OperatorComboBox operatorComboBox2;
        //private System.Windows.Forms.DataGridView dataGridView1;
    }
}