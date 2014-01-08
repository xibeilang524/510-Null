namespace OfflineCardPayingTool
{
    partial class FrmPaying
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
            this.paymentPanel = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnCash = new System.Windows.Forms.Button();
            this.btnYCT = new System.Windows.Forms.Button();
            this.carTypePanel1 = new Ralid.Park.UserControls.CarTypePanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.lblOwnerName = new System.Windows.Forms.Label();
            this.lblCarNum = new System.Windows.Forms.Label();
            this.lblEnterDateTime = new System.Windows.Forms.Label();
            this.lblExitDateTime = new System.Windows.Forms.Label();
            this.lblParkingTime = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblLastTotalPaid = new System.Windows.Forms.Label();
            this.lblAccounts = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lblLastWorkstation = new System.Windows.Forms.Label();
            this.txtPaid = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.eventList = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.paymentPanel.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // paymentPanel
            // 
            this.paymentPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.paymentPanel.Controls.Add(this.panel7);
            this.paymentPanel.Controls.Add(this.panel6);
            this.paymentPanel.Controls.Add(this.carTypePanel1);
            this.paymentPanel.Controls.Add(this.tableLayoutPanel1);
            this.paymentPanel.Controls.Add(this.panel5);
            this.paymentPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.paymentPanel.Location = new System.Drawing.Point(680, 0);
            this.paymentPanel.Name = "paymentPanel";
            this.paymentPanel.Size = new System.Drawing.Size(219, 573);
            this.paymentPanel.TabIndex = 74;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnCancel);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 420);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(219, 43);
            this.panel7.TabIndex = 68;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "放弃处理[F12]";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnCash);
            this.panel6.Controls.Add(this.btnYCT);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 364);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(219, 56);
            this.panel6.TabIndex = 67;
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.SystemColors.Control;
            this.btnCash.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnCash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCash.Location = new System.Drawing.Point(2, 7);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(100, 40);
            this.btnCash.TabIndex = 7;
            this.btnCash.Text = "现金收费[F9]";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // btnYCT
            // 
            this.btnYCT.BackColor = System.Drawing.SystemColors.Control;
            this.btnYCT.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnYCT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnYCT.Location = new System.Drawing.Point(114, 7);
            this.btnYCT.Name = "btnYCT";
            this.btnYCT.Size = new System.Drawing.Size(100, 40);
            this.btnYCT.TabIndex = 8;
            this.btnYCT.Text = "羊城通[F10]";
            this.btnYCT.UseVisualStyleBackColor = false;
            this.btnYCT.Click += new System.EventHandler(this.btnYCT_Click);
            // 
            // carTypePanel1
            // 
            this.carTypePanel1.BackColor = System.Drawing.SystemColors.Control;
            this.carTypePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.carTypePanel1.Location = new System.Drawing.Point(0, 361);
            this.carTypePanel1.Name = "carTypePanel1";
            this.carTypePanel1.Size = new System.Drawing.Size(219, 3);
            this.carTypePanel1.TabIndex = 66;
            this.carTypePanel1.CarTypeSelectedChanged += new System.EventHandler(this.carTypePanel1_CarTypeSelectedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.90826F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.09174F));
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtCardID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOwnerName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCarNum, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblEnterDateTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblExitDateTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblParkingTime, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblCardType, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblBalance, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblLastTotalPaid, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblAccounts, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblDiscount, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtMemo, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblLastWorkstation, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtPaid, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 14);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(219, 336);
            this.tableLayoutPanel1.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(4, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "卡号:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(4, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 36;
            this.label10.Text = "车主姓名:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(4, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 20);
            this.label11.TabIndex = 37;
            this.label11.Text = "车牌号码:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(4, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "入场时间:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(4, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 20);
            this.label12.TabIndex = 48;
            this.label12.Text = "计费时间:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(4, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "停车时长:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(4, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 51;
            this.label8.Text = "卡片类型:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(4, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 75;
            this.label2.Text = "卡片余额:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(4, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.TabIndex = 80;
            this.label13.Text = "累计已收:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(4, 212);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 20);
            this.label14.TabIndex = 82;
            this.label14.Text = "本次应收:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(4, 284);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 24);
            this.label16.TabIndex = 83;
            this.label16.Text = "折扣金额:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(4, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 50);
            this.label7.TabIndex = 11;
            this.label7.Text = "实收费用:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCardID
            // 
            this.txtCardID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCardID.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtCardID.Location = new System.Drawing.Point(91, 4);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Size = new System.Drawing.Size(124, 14);
            this.txtCardID.TabIndex = 0;
            // 
            // lblOwnerName
            // 
            this.lblOwnerName.AutoSize = true;
            this.lblOwnerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOwnerName.Font = new System.Drawing.Font("宋体", 9F);
            this.lblOwnerName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOwnerName.Location = new System.Drawing.Point(91, 22);
            this.lblOwnerName.Name = "lblOwnerName";
            this.lblOwnerName.Size = new System.Drawing.Size(124, 20);
            this.lblOwnerName.TabIndex = 38;
            this.lblOwnerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCarNum
            // 
            this.lblCarNum.AutoSize = true;
            this.lblCarNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCarNum.Font = new System.Drawing.Font("宋体", 9F);
            this.lblCarNum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCarNum.Location = new System.Drawing.Point(91, 43);
            this.lblCarNum.Name = "lblCarNum";
            this.lblCarNum.Size = new System.Drawing.Size(124, 20);
            this.lblCarNum.TabIndex = 40;
            this.lblCarNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEnterDateTime
            // 
            this.lblEnterDateTime.AutoSize = true;
            this.lblEnterDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnterDateTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lblEnterDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEnterDateTime.Location = new System.Drawing.Point(91, 64);
            this.lblEnterDateTime.Name = "lblEnterDateTime";
            this.lblEnterDateTime.Size = new System.Drawing.Size(124, 20);
            this.lblEnterDateTime.TabIndex = 47;
            this.lblEnterDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExitDateTime
            // 
            this.lblExitDateTime.AutoSize = true;
            this.lblExitDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExitDateTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lblExitDateTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExitDateTime.Location = new System.Drawing.Point(91, 85);
            this.lblExitDateTime.Name = "lblExitDateTime";
            this.lblExitDateTime.Size = new System.Drawing.Size(124, 20);
            this.lblExitDateTime.TabIndex = 26;
            this.lblExitDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblParkingTime
            // 
            this.lblParkingTime.AutoSize = true;
            this.lblParkingTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParkingTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lblParkingTime.ForeColor = System.Drawing.Color.Blue;
            this.lblParkingTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblParkingTime.Location = new System.Drawing.Point(91, 106);
            this.lblParkingTime.Name = "lblParkingTime";
            this.lblParkingTime.Size = new System.Drawing.Size(124, 20);
            this.lblParkingTime.TabIndex = 18;
            this.lblParkingTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardType.Font = new System.Drawing.Font("宋体", 9F);
            this.lblCardType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardType.Location = new System.Drawing.Point(91, 127);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(124, 20);
            this.lblCardType.TabIndex = 50;
            this.lblCardType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBalance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBalance.Location = new System.Drawing.Point(91, 148);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(124, 20);
            this.lblBalance.TabIndex = 76;
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastTotalPaid
            // 
            this.lblLastTotalPaid.AutoSize = true;
            this.lblLastTotalPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastTotalPaid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLastTotalPaid.Location = new System.Drawing.Point(91, 170);
            this.lblLastTotalPaid.Name = "lblLastTotalPaid";
            this.lblLastTotalPaid.Size = new System.Drawing.Size(124, 20);
            this.lblLastTotalPaid.TabIndex = 81;
            this.lblLastTotalPaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAccounts
            // 
            this.lblAccounts.AutoSize = true;
            this.lblAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAccounts.ForeColor = System.Drawing.Color.Blue;
            this.lblAccounts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAccounts.Location = new System.Drawing.Point(91, 212);
            this.lblAccounts.Name = "lblAccounts";
            this.lblAccounts.Size = new System.Drawing.Size(124, 20);
            this.lblAccounts.TabIndex = 84;
            this.lblAccounts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiscount.ForeColor = System.Drawing.Color.Red;
            this.lblDiscount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDiscount.Location = new System.Drawing.Point(91, 284);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(124, 24);
            this.lblDiscount.TabIndex = 85;
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMemo
            // 
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemo.FormattingEnabled = true;
            this.txtMemo.Location = new System.Drawing.Point(91, 312);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(124, 20);
            this.txtMemo.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(4, 191);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 20);
            this.label19.TabIndex = 87;
            this.label19.Text = "上次收费:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLastWorkstation
            // 
            this.lblLastWorkstation.AutoSize = true;
            this.lblLastWorkstation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastWorkstation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLastWorkstation.Location = new System.Drawing.Point(91, 191);
            this.lblLastWorkstation.Name = "lblLastWorkstation";
            this.lblLastWorkstation.Size = new System.Drawing.Size(124, 20);
            this.lblLastWorkstation.TabIndex = 88;
            this.lblLastWorkstation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPaid
            // 
            this.txtPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPaid.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold);
            this.txtPaid.ForeColor = System.Drawing.Color.Blue;
            this.txtPaid.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPaid.Location = new System.Drawing.Point(91, 236);
            this.txtPaid.MaxValue = new decimal(new int[] {
            16777215,
            0,
            0,
            131072});
            this.txtPaid.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.PointCount = 2;
            this.txtPaid.Size = new System.Drawing.Size(124, 44);
            this.txtPaid.TabIndex = 1;
            this.txtPaid.Text = "0.00";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(4, 309);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 26);
            this.label17.TabIndex = 78;
            this.label17.Text = "优惠说明:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel5.Controls.Add(this.label15);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(219, 25);
            this.panel5.TabIndex = 56;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(5, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "卡片收费明细";
            // 
            // eventList
            // 
            this.eventList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventList.Font = new System.Drawing.Font("宋体", 10F);
            this.eventList.FormattingEnabled = true;
            this.eventList.ItemHeight = 12;
            this.eventList.Location = new System.Drawing.Point(0, 0);
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(680, 573);
            this.eventList.TabIndex = 84;
            // 
            // FrmPaying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 573);
            this.Controls.Add(this.eventList);
            this.Controls.Add(this.paymentPanel);
            this.Name = "FrmPaying";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "脱机收费";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPaying_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPaying_FormClosed);
            this.Load += new System.EventHandler(this.FrmPaying_Load);
            this.paymentPanel.ResumeLayout(false);
            this.paymentPanel.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paymentPanel;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.Button btnYCT;
        private Ralid.Park.UserControls.CarTypePanel carTypePanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label7;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label lblOwnerName;
        private System.Windows.Forms.Label lblCarNum;
        private System.Windows.Forms.Label lblEnterDateTime;
        private System.Windows.Forms.Label lblExitDateTime;
        private System.Windows.Forms.Label lblParkingTime;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblLastTotalPaid;
        private System.Windows.Forms.Label lblAccounts;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.ComboBox txtMemo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblLastWorkstation;
        private Ralid.GeneralLibrary.WinformControl.DecimalTextBox txtPaid;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private Ralid.Park.UserControls.EventReportListBox eventList;

    }
}