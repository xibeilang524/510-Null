namespace PreferentialSystem
{
    partial class FrmPreferentialReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbBusiness = new Ralid.Park.UserControls.PREBusinessesComboBox(this.components);
            this.lblOperators = new System.Windows.Forms.LinkLabel();
            this.txtWorkstations = new System.Windows.Forms.TextBox();
            this.lblWorkstations = new System.Windows.Forms.LinkLabel();
            this.txtOperators = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntranceTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreferentialHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBusiness1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBusiness2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBusiness3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWorkstationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsCancel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCancelReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelectColumns = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_PrintRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveAs2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHour = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.txtCancelReason = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(640, 81);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(640, 108);
            this.btnSaveAs.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbBusiness);
            this.groupBox1.Controls.Add(this.lblOperators);
            this.groupBox1.Controls.Add(this.txtWorkstations);
            this.groupBox1.Controls.Add(this.lblWorkstations);
            this.groupBox1.Controls.Add(this.txtOperators);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCardID);
            this.groupBox1.Location = new System.Drawing.Point(248, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 107);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // cmbBusiness
            // 
            this.cmbBusiness.FormattingEnabled = true;
            this.cmbBusiness.Location = new System.Drawing.Point(253, 17);
            this.cmbBusiness.Name = "cmbBusiness";
            this.cmbBusiness.Size = new System.Drawing.Size(121, 20);
            this.cmbBusiness.TabIndex = 8;
            // 
            // lblOperators
            // 
            this.lblOperators.AutoSize = true;
            this.lblOperators.Location = new System.Drawing.Point(6, 49);
            this.lblOperators.Name = "lblOperators";
            this.lblOperators.Size = new System.Drawing.Size(59, 12);
            this.lblOperators.TabIndex = 6;
            this.lblOperators.TabStop = true;
            this.lblOperators.Text = "多操作员:";
            this.lblOperators.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOperators_LinkClicked);
            // 
            // txtWorkstations
            // 
            this.txtWorkstations.Location = new System.Drawing.Point(71, 77);
            this.txtWorkstations.Name = "txtWorkstations";
            this.txtWorkstations.ReadOnly = true;
            this.txtWorkstations.Size = new System.Drawing.Size(303, 21);
            this.txtWorkstations.TabIndex = 6;
            // 
            // lblWorkstations
            // 
            this.lblWorkstations.AutoSize = true;
            this.lblWorkstations.Location = new System.Drawing.Point(6, 80);
            this.lblWorkstations.Name = "lblWorkstations";
            this.lblWorkstations.Size = new System.Drawing.Size(59, 12);
            this.lblWorkstations.TabIndex = 6;
            this.lblWorkstations.TabStop = true;
            this.lblWorkstations.Text = "多工作站:";
            this.lblWorkstations.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblWorkstations_LinkClicked);
            // 
            // txtOperators
            // 
            this.txtOperators.Location = new System.Drawing.Point(71, 46);
            this.txtOperators.Name = "txtOperators";
            this.txtOperators.ReadOnly = true;
            this.txtOperators.Size = new System.Drawing.Size(303, 21);
            this.txtOperators.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "商铺名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "卡号:";
            // 
            // txtCardID
            // 
            this.txtCardID.Location = new System.Drawing.Point(71, 16);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(100, 21);
            this.txtCardID.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucDateTimeInterval1);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 107);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "优惠时间:";
            // 
            // ucDateTimeInterval1
            // 
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2014, 9, 1, 17, 0, 21, 139);
            this.ucDateTimeInterval1.Location = new System.Drawing.Point(6, 20);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.Size = new System.Drawing.Size(221, 74);
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2014, 9, 1, 17, 0, 21, 139);
            this.ucDateTimeInterval1.TabIndex = 7;
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardID,
            this.colEntranceTime,
            this.colPreferentialHour,
            this.colBusiness1,
            this.colCost1,
            this.colBusiness2,
            this.colCost2,
            this.colBusiness3,
            this.colCost3,
            this.colNotes,
            this.colWorkstationName,
            this.colOperator,
            this.colOperatorTime,
            this.colIsCancel,
            this.colCancelReason});
            this.customDataGridView1.Location = new System.Drawing.Point(9, 176);
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.Size = new System.Drawing.Size(909, 272);
            this.customDataGridView1.TabIndex = 7;
            this.customDataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.customDataGridView1_MouseDown);
            // 
            // colCardID
            // 
            this.colCardID.HeaderText = "卡号";
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            this.colCardID.Width = 80;
            // 
            // colEntranceTime
            // 
            this.colEntranceTime.HeaderText = "入场时间";
            this.colEntranceTime.Name = "colEntranceTime";
            this.colEntranceTime.ReadOnly = true;
            this.colEntranceTime.Width = 130;
            // 
            // colPreferentialHour
            // 
            this.colPreferentialHour.HeaderText = "优惠时数";
            this.colPreferentialHour.Name = "colPreferentialHour";
            this.colPreferentialHour.ReadOnly = true;
            this.colPreferentialHour.Width = 80;
            // 
            // colBusiness1
            // 
            this.colBusiness1.HeaderText = "商铺1";
            this.colBusiness1.Name = "colBusiness1";
            this.colBusiness1.ReadOnly = true;
            // 
            // colCost1
            // 
            this.colCost1.HeaderText = "商铺1消费";
            this.colCost1.Name = "colCost1";
            this.colCost1.ReadOnly = true;
            // 
            // colBusiness2
            // 
            this.colBusiness2.HeaderText = "商铺2";
            this.colBusiness2.Name = "colBusiness2";
            this.colBusiness2.ReadOnly = true;
            // 
            // colCost2
            // 
            this.colCost2.HeaderText = "商铺2消费";
            this.colCost2.Name = "colCost2";
            this.colCost2.ReadOnly = true;
            // 
            // colBusiness3
            // 
            this.colBusiness3.HeaderText = "商铺3";
            this.colBusiness3.Name = "colBusiness3";
            this.colBusiness3.ReadOnly = true;
            // 
            // colCost3
            // 
            this.colCost3.HeaderText = "商铺3消费";
            this.colCost3.Name = "colCost3";
            this.colCost3.ReadOnly = true;
            // 
            // colNotes
            // 
            this.colNotes.HeaderText = "备注";
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            // 
            // colWorkstationName
            // 
            this.colWorkstationName.HeaderText = "工作站名称";
            this.colWorkstationName.Name = "colWorkstationName";
            this.colWorkstationName.ReadOnly = true;
            // 
            // colOperator
            // 
            this.colOperator.HeaderText = "操作员名称";
            this.colOperator.Name = "colOperator";
            this.colOperator.ReadOnly = true;
            // 
            // colOperatorTime
            // 
            this.colOperatorTime.HeaderText = "操作时间";
            this.colOperatorTime.Name = "colOperatorTime";
            this.colOperatorTime.ReadOnly = true;
            this.colOperatorTime.Width = 130;
            // 
            // colIsCancel
            // 
            this.colIsCancel.HeaderText = "是否取消";
            this.colIsCancel.Name = "colIsCancel";
            this.colIsCancel.ReadOnly = true;
            this.colIsCancel.Width = 80;
            // 
            // colCancelReason
            // 
            this.colCancelReason.HeaderText = "取消原因";
            this.colCancelReason.MinimumWidth = 120;
            this.colCancelReason.Name = "colCancelReason";
            this.colCancelReason.ReadOnly = true;
            this.colCancelReason.Width = 120;
            // 
            // btnSelectColumns
            // 
            this.btnSelectColumns.Location = new System.Drawing.Point(640, 137);
            this.btnSelectColumns.Name = "btnSelectColumns";
            this.btnSelectColumns.Size = new System.Drawing.Size(111, 23);
            this.btnSelectColumns.TabIndex = 8;
            this.btnSelectColumns.Text = "选择列";
            this.btnSelectColumns.UseVisualStyleBackColor = true;
            this.btnSelectColumns.Click += new System.EventHandler(this.btnSelectColumns_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_PrintRecord});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            // 
            // mnu_PrintRecord
            // 
            this.mnu_PrintRecord.Name = "mnu_PrintRecord";
            this.mnu_PrintRecord.Size = new System.Drawing.Size(148, 22);
            this.mnu_PrintRecord.Text = "打印优惠记录";
            this.mnu_PrintRecord.Click += new System.EventHandler(this.mnu_PrintRecord_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTotal);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(640, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(141, 54);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "统计信息";
            // 
            // txtTotal
            // 
            this.txtTotal.AutoSize = true;
            this.txtTotal.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.txtTotal.ForeColor = System.Drawing.Color.Black;
            this.txtTotal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtTotal.Location = new System.Drawing.Point(71, 31);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(16, 15);
            this.txtTotal.TabIndex = 11;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "优惠时数:";
            // 
            // btnSaveAs2
            // 
            this.btnSaveAs2.Location = new System.Drawing.Point(640, 108);
            this.btnSaveAs2.Name = "btnSaveAs2";
            this.btnSaveAs2.Size = new System.Drawing.Size(111, 23);
            this.btnSaveAs2.TabIndex = 12;
            this.btnSaveAs2.Text = "另存为(&S)";
            this.btnSaveAs2.UseVisualStyleBackColor = true;
            this.btnSaveAs2.Click += new System.EventHandler(this.btnSaveAs2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtHour);
            this.groupBox4.Controls.Add(this.txtCancelReason);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(9, 125);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(429, 45);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "附加查询条件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "优惠时数:";
            // 
            // txtHour
            // 
            this.txtHour.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtHour.Location = new System.Drawing.Point(310, 14);
            this.txtHour.MaxValue = 100000;
            this.txtHour.MinValue = 0;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(100, 21);
            this.txtHour.TabIndex = 2;
            // 
            // txtCancelReason
            // 
            this.txtCancelReason.Location = new System.Drawing.Point(71, 14);
            this.txtCancelReason.Name = "txtCancelReason";
            this.txtCancelReason.Size = new System.Drawing.Size(156, 21);
            this.txtCancelReason.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "取消原因:";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(523, 137);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(111, 23);
            this.btnPrint.TabIndex = 14;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmPreferentialReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 473);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnSaveAs2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSelectColumns);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmPreferentialReport";
            this.Text = "优惠记录查询统计";
            this.Load += new System.EventHandler(this.FrmPreferentialReport_Load);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.btnSelectColumns, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.btnSaveAs2, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.btnPrint, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label label2;
        private Ralid.Park.UserControls.PREBusinessesComboBox cmbBusiness;
        private System.Windows.Forms.TextBox txtOperators;
        private System.Windows.Forms.LinkLabel lblOperators;
        private System.Windows.Forms.LinkLabel lblWorkstations;
        private System.Windows.Forms.TextBox txtWorkstations;
        private System.Windows.Forms.GroupBox groupBox2;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.Button btnSelectColumns;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_PrintRecord;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.Button btnSaveAs2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtCancelReason;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Ralid.GeneralLibrary.WinformControl.IntergerTextBox txtHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntranceTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreferentialHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBusiness1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBusiness2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBusiness3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkstationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCancelReason;
    }
}