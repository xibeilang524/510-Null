namespace OfflineCardPayingTool
{
    partial class FrmUpdatePaymentRecord
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAccounts = new System.Windows.Forms.Label();
            this.txtPaid = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.carTypeComboBox1 = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdUpdated = new System.Windows.Forms.RadioButton();
            this.rdBoth = new System.Windows.Forms.RadioButton();
            this.rdUnupdated = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdPaid = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdFree = new System.Windows.Forms.RadioButton();
            this.chkPaymentMode = new System.Windows.Forms.CheckBox();
            this.comPaymentMode = new Ralid.Park.UserControls.PaymentModeComboBox(this.components);
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.comOperator = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCarType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExitDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnterDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeSpan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAccounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTariffType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHandled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTotalPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperatorCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1028, 12);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(1028, 41);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtAccounts);
            this.groupBox2.Controls.Add(this.txtPaid);
            this.groupBox2.Location = new System.Drawing.Point(803, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 90);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果统计";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(15, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "实收:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(15, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "折扣:";
            // 
            // txtAccounts
            // 
            this.txtAccounts.AutoSize = true;
            this.txtAccounts.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.txtAccounts.ForeColor = System.Drawing.Color.Red;
            this.txtAccounts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtAccounts.Location = new System.Drawing.Point(48, 36);
            this.txtAccounts.Name = "txtAccounts";
            this.txtAccounts.Size = new System.Drawing.Size(16, 15);
            this.txtAccounts.TabIndex = 7;
            this.txtAccounts.Text = "0";
            this.txtAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPaid
            // 
            this.txtPaid.AutoSize = true;
            this.txtPaid.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.txtPaid.ForeColor = System.Drawing.Color.Blue;
            this.txtPaid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtPaid.Location = new System.Drawing.Point(48, 16);
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.Size = new System.Drawing.Size(16, 15);
            this.txtPaid.TabIndex = 8;
            this.txtPaid.Text = "0";
            this.txtPaid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.carTypeComboBox1);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.chkPaymentMode);
            this.groupBox3.Controls.Add(this.comPaymentMode);
            this.groupBox3.Controls.Add(this.txtCarPlate);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comCardType);
            this.groupBox3.Controls.Add(this.comOperator);
            this.groupBox3.Controls.Add(this.txtCardID);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(236, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(561, 91);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // carTypeComboBox1
            // 
            this.carTypeComboBox1.FormattingEnabled = true;
            this.carTypeComboBox1.Location = new System.Drawing.Point(60, 64);
            this.carTypeComboBox1.Name = "carTypeComboBox1";
            this.carTypeComboBox1.Size = new System.Drawing.Size(109, 20);
            this.carTypeComboBox1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rdUpdated);
            this.panel2.Controls.Add(this.rdBoth);
            this.panel2.Controls.Add(this.rdUnupdated);
            this.panel2.Location = new System.Drawing.Point(351, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 22);
            this.panel2.TabIndex = 12;
            // 
            // rdUpdated
            // 
            this.rdUpdated.AutoSize = true;
            this.rdUpdated.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdUpdated.Location = new System.Drawing.Point(132, 3);
            this.rdUpdated.Name = "rdUpdated";
            this.rdUpdated.Size = new System.Drawing.Size(59, 16);
            this.rdUpdated.TabIndex = 2;
            this.rdUpdated.Text = "已上传";
            this.rdUpdated.UseVisualStyleBackColor = true;
            // 
            // rdBoth
            // 
            this.rdBoth.AutoSize = true;
            this.rdBoth.Checked = true;
            this.rdBoth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdBoth.Location = new System.Drawing.Point(4, 3);
            this.rdBoth.Name = "rdBoth";
            this.rdBoth.Size = new System.Drawing.Size(47, 16);
            this.rdBoth.TabIndex = 0;
            this.rdBoth.TabStop = true;
            this.rdBoth.Text = "全部";
            this.rdBoth.UseVisualStyleBackColor = true;
            // 
            // rdUnupdated
            // 
            this.rdUnupdated.AutoSize = true;
            this.rdUnupdated.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdUnupdated.Location = new System.Drawing.Point(68, 3);
            this.rdUnupdated.Name = "rdUnupdated";
            this.rdUnupdated.Size = new System.Drawing.Size(59, 16);
            this.rdUnupdated.TabIndex = 1;
            this.rdUnupdated.Text = "未上传";
            this.rdUnupdated.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(26, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "车型:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rdPaid);
            this.panel1.Controls.Add(this.rdAll);
            this.panel1.Controls.Add(this.rdFree);
            this.panel1.Location = new System.Drawing.Point(351, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 22);
            this.panel1.TabIndex = 11;
            // 
            // rdPaid
            // 
            this.rdPaid.AutoSize = true;
            this.rdPaid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdPaid.Location = new System.Drawing.Point(68, 2);
            this.rdPaid.Name = "rdPaid";
            this.rdPaid.Size = new System.Drawing.Size(47, 16);
            this.rdPaid.TabIndex = 2;
            this.rdPaid.Text = "收费";
            this.rdPaid.UseVisualStyleBackColor = true;
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Checked = true;
            this.rdAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdAll.Location = new System.Drawing.Point(4, 2);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(47, 16);
            this.rdAll.TabIndex = 0;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "全部";
            this.rdAll.UseVisualStyleBackColor = true;
            // 
            // rdFree
            // 
            this.rdFree.AutoSize = true;
            this.rdFree.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdFree.Location = new System.Drawing.Point(134, 2);
            this.rdFree.Name = "rdFree";
            this.rdFree.Size = new System.Drawing.Size(47, 16);
            this.rdFree.TabIndex = 1;
            this.rdFree.Text = "免费";
            this.rdFree.UseVisualStyleBackColor = true;
            // 
            // chkPaymentMode
            // 
            this.chkPaymentMode.AutoSize = true;
            this.chkPaymentMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkPaymentMode.Location = new System.Drawing.Point(351, 66);
            this.chkPaymentMode.Name = "chkPaymentMode";
            this.chkPaymentMode.Size = new System.Drawing.Size(78, 16);
            this.chkPaymentMode.TabIndex = 13;
            this.chkPaymentMode.Text = "收费类型:";
            this.chkPaymentMode.UseVisualStyleBackColor = true;
            // 
            // comPaymentMode
            // 
            this.comPaymentMode.FormattingEnabled = true;
            this.comPaymentMode.Location = new System.Drawing.Point(435, 64);
            this.comPaymentMode.Name = "comPaymentMode";
            this.comPaymentMode.Size = new System.Drawing.Size(109, 20);
            this.comPaymentMode.TabIndex = 14;
            // 
            // txtCarPlate
            // 
            this.txtCarPlate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCarPlate.Location = new System.Drawing.Point(231, 13);
            this.txtCarPlate.Name = "txtCarPlate";
            this.txtCarPlate.Size = new System.Drawing.Size(109, 21);
            this.txtCarPlate.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(186, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "车牌号:";
            // 
            // comCardType
            // 
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Location = new System.Drawing.Point(60, 39);
            this.comCardType.Name = "comCardType";
            this.comCardType.Size = new System.Drawing.Size(109, 20);
            this.comCardType.TabIndex = 5;
            // 
            // comOperator
            // 
            this.comOperator.FormattingEnabled = true;
            this.comOperator.Location = new System.Drawing.Point(231, 39);
            this.comOperator.Name = "comOperator";
            this.comOperator.Size = new System.Drawing.Size(109, 20);
            this.comOperator.TabIndex = 9;
            // 
            // txtCardID
            // 
            this.txtCardID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardID.Location = new System.Drawing.Point(60, 13);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(109, 21);
            this.txtCardID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(186, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "操作员:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(2, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "卡片类型:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(26, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "卡号:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 91);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "收费时间";
            // 
            // ucDateTimeInterval1
            // 
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2010, 1, 9, 23, 59, 59, 0);
            this.ucDateTimeInterval1.Location = new System.Drawing.Point(3, 12);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.Size = new System.Drawing.Size(221, 74);
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2010, 1, 9, 16, 56, 56, 625);
            this.ucDateTimeInterval1.TabIndex = 0;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(1028, 68);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(111, 23);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "上传记录(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
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
            this.colCarPlate,
            this.colCardType,
            this.colCarType,
            this.colExitDateTime,
            this.colEnterDateTime,
            this.colTimeSpan,
            this.colTotalAccounts,
            this.colPaid,
            this.colDiscount,
            this.colTariffType,
            this.colPaymentMode,
            this.colHandled,
            this.colTotalPaid,
            this.colTotalDiscount,
            this.colMemo,
            this.colOperator,
            this.colOperatorCardID,
            this.colPaymentCode,
            this.colStation});
            this.customDataGridView1.Location = new System.Drawing.Point(4, 102);
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersWidth = 20;
            this.customDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.Size = new System.Drawing.Size(1163, 348);
            this.customDataGridView1.TabIndex = 20;
            // 
            // colCardID
            // 
            this.colCardID.HeaderText = "卡号";
            this.colCardID.Name = "colCardID";
            this.colCardID.ReadOnly = true;
            this.colCardID.Width = 70;
            // 
            // colCarPlate
            // 
            this.colCarPlate.HeaderText = "车牌号";
            this.colCarPlate.Name = "colCarPlate";
            this.colCarPlate.ReadOnly = true;
            // 
            // colCardType
            // 
            this.colCardType.HeaderText = "卡片类型";
            this.colCardType.Name = "colCardType";
            this.colCardType.ReadOnly = true;
            this.colCardType.Width = 80;
            // 
            // colCarType
            // 
            this.colCarType.HeaderText = "收费车型";
            this.colCarType.Name = "colCarType";
            this.colCarType.ReadOnly = true;
            this.colCarType.Width = 80;
            // 
            // colExitDateTime
            // 
            this.colExitDateTime.HeaderText = "收费时间";
            this.colExitDateTime.Name = "colExitDateTime";
            this.colExitDateTime.ReadOnly = true;
            this.colExitDateTime.Width = 130;
            // 
            // colEnterDateTime
            // 
            this.colEnterDateTime.HeaderText = "入场时间";
            this.colEnterDateTime.Name = "colEnterDateTime";
            this.colEnterDateTime.ReadOnly = true;
            this.colEnterDateTime.Width = 130;
            // 
            // colTimeSpan
            // 
            this.colTimeSpan.HeaderText = "停车时长";
            this.colTimeSpan.Name = "colTimeSpan";
            this.colTimeSpan.ReadOnly = true;
            this.colTimeSpan.Width = 90;
            // 
            // colTotalAccounts
            // 
            this.colTotalAccounts.HeaderText = "应收";
            this.colTotalAccounts.Name = "colTotalAccounts";
            this.colTotalAccounts.ReadOnly = true;
            this.colTotalAccounts.Width = 80;
            // 
            // colPaid
            // 
            this.colPaid.HeaderText = "实收";
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            this.colPaid.Width = 80;
            // 
            // colDiscount
            // 
            this.colDiscount.HeaderText = "折扣";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            this.colDiscount.Width = 80;
            // 
            // colTariffType
            // 
            this.colTariffType.HeaderText = "计费方式";
            this.colTariffType.Name = "colTariffType";
            this.colTariffType.ReadOnly = true;
            this.colTariffType.Width = 80;
            // 
            // colPaymentMode
            // 
            this.colPaymentMode.HeaderText = "收费方式";
            this.colPaymentMode.Name = "colPaymentMode";
            this.colPaymentMode.ReadOnly = true;
            this.colPaymentMode.Width = 80;
            // 
            // colHandled
            // 
            this.colHandled.HeaderText = "已上传";
            this.colHandled.Name = "colHandled";
            this.colHandled.ReadOnly = true;
            this.colHandled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colHandled.Width = 80;
            // 
            // colTotalPaid
            // 
            this.colTotalPaid.HeaderText = "累计实收";
            this.colTotalPaid.Name = "colTotalPaid";
            this.colTotalPaid.ReadOnly = true;
            this.colTotalPaid.Width = 80;
            // 
            // colTotalDiscount
            // 
            this.colTotalDiscount.HeaderText = "累计折扣";
            this.colTotalDiscount.Name = "colTotalDiscount";
            this.colTotalDiscount.ReadOnly = true;
            this.colTotalDiscount.Width = 80;
            // 
            // colMemo
            // 
            this.colMemo.HeaderText = "说明";
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // colOperator
            // 
            this.colOperator.HeaderText = "操作员";
            this.colOperator.Name = "colOperator";
            this.colOperator.ReadOnly = true;
            this.colOperator.Width = 80;
            // 
            // colOperatorCardID
            // 
            this.colOperatorCardID.HeaderText = "操作员卡号";
            this.colOperatorCardID.Name = "colOperatorCardID";
            this.colOperatorCardID.ReadOnly = true;
            this.colOperatorCardID.Width = 90;
            // 
            // colPaymentCode
            // 
            this.colPaymentCode.HeaderText = "收费代码";
            this.colPaymentCode.Name = "colPaymentCode";
            this.colPaymentCode.ReadOnly = true;
            this.colPaymentCode.Width = 80;
            // 
            // colStation
            // 
            this.colStation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStation.HeaderText = "工作站";
            this.colStation.MinimumWidth = 100;
            this.colStation.Name = "colStation";
            this.colStation.ReadOnly = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(4, 451);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1163, 23);
            this.progressBar1.TabIndex = 21;
            // 
            // FrmUpdatePaymentRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1170, 497);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmUpdatePaymentRecord";
            this.Text = "脱机收费记录查询";
            this.Load += new System.EventHandler(this.FrmUpdatePaymentRecord_Load);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnUpdate, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtAccounts;
        private System.Windows.Forms.Label txtPaid;
        private System.Windows.Forms.GroupBox groupBox3;
        private Ralid.Park.UserControls.CarTypeComboBox carTypeComboBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdUpdated;
        private System.Windows.Forms.RadioButton rdBoth;
        private System.Windows.Forms.RadioButton rdUnupdated;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdPaid;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.RadioButton rdFree;
        private System.Windows.Forms.CheckBox chkPaymentMode;
        private Ralid.Park.UserControls.PaymentModeComboBox comPaymentMode;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label7;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private Ralid.Park.UserControls.OperatorComboBox comOperator;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.Button btnUpdate;
        private Ralid.Park.UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExitDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEnterDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeSpan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTariffType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHandled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperatorCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStation;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
