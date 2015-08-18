namespace Ralid.Park.UI
{
    partial class FrmCardPaying
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardPaying));
            this.paymentPanel = new System.Windows.Forms.Panel();
            this.buttonPnl = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnCardOk = new System.Windows.Forms.Button();
            this.btnInvalidEvent = new System.Windows.Forms.Button();
            this.pnlCash = new System.Windows.Forms.Panel();
            this.btnPos = new System.Windows.Forms.Button();
            this.btnCoupon = new System.Windows.Forms.Button();
            this.btnCash = new System.Windows.Forms.Button();
            this.btnYCT = new System.Windows.Forms.Button();
            this.entrancePanel1 = new Ralid.Park.UserControls.EntrancePanel();
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
            this.label17 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.lblOwnerName = new System.Windows.Forms.Label();
            this.lblCarNum = new System.Windows.Forms.Label();
            this.lblEnterDateTime = new System.Windows.Forms.Label();
            this.lblExitDateTime = new System.Windows.Forms.Label();
            this.lblParkingTime = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblTariffType = new System.Windows.Forms.Label();
            this.lblLastTotalPaid = new System.Windows.Forms.Label();
            this.lblAccounts = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lblLastWorkstation = new System.Windows.Forms.Label();
            this.txtPaid = new Ralid.GeneralLibrary.WinformControl.DecimalTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblDiscountHour = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDiscountMemo = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.ucapmMonitor1 = new Ralid.Park.UserControls.UCAPMMonitor();
            this.eventList = new Ralid.Park.UserControls.EventReportListBox(this.components);
            this.splitTop = new System.Windows.Forms.Splitter();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.picIn = new Ralid.Park.UserControls.UCPictureListView();
            this.ucVideoes = new Ralid.Park.UserControls.VideoPanels.UCVideoListView();
            this.spliterLeft = new System.Windows.Forms.Splitter();
            this.tmr_YCT = new System.Windows.Forms.Timer(this.components);
            this.paymentPanel.SuspendLayout();
            this.buttonPnl.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlCash.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.videoPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // paymentPanel
            // 
            this.paymentPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.paymentPanel.Controls.Add(this.buttonPnl);
            this.paymentPanel.Controls.Add(this.tableLayoutPanel1);
            this.paymentPanel.Controls.Add(this.panel5);
            resources.ApplyResources(this.paymentPanel, "paymentPanel");
            this.paymentPanel.Name = "paymentPanel";
            // 
            // buttonPnl
            // 
            resources.ApplyResources(this.buttonPnl, "buttonPnl");
            this.buttonPnl.Controls.Add(this.panel6);
            this.buttonPnl.Controls.Add(this.pnlCash);
            this.buttonPnl.Controls.Add(this.entrancePanel1);
            this.buttonPnl.Controls.Add(this.carTypePanel1);
            this.buttonPnl.Name = "buttonPnl";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnCardOk);
            this.panel6.Controls.Add(this.btnInvalidEvent);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            this.panel6.Resize += new System.EventHandler(this.panel6_Resize);
            // 
            // btnCardOk
            // 
            this.btnCardOk.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnCardOk, "btnCardOk");
            this.btnCardOk.Name = "btnCardOk";
            this.btnCardOk.UseVisualStyleBackColor = false;
            this.btnCardOk.Click += new System.EventHandler(this.btnCardOk_Click);
            // 
            // btnInvalidEvent
            // 
            this.btnInvalidEvent.BackColor = System.Drawing.SystemColors.Control;
            this.btnInvalidEvent.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnInvalidEvent, "btnInvalidEvent");
            this.btnInvalidEvent.Name = "btnInvalidEvent";
            this.btnInvalidEvent.UseVisualStyleBackColor = false;
            this.btnInvalidEvent.Click += new System.EventHandler(this.btnInvalidEvent_Click);
            // 
            // pnlCash
            // 
            this.pnlCash.Controls.Add(this.btnPos);
            this.pnlCash.Controls.Add(this.btnCoupon);
            this.pnlCash.Controls.Add(this.btnCash);
            this.pnlCash.Controls.Add(this.btnYCT);
            resources.ApplyResources(this.pnlCash, "pnlCash");
            this.pnlCash.Name = "pnlCash";
            this.pnlCash.Resize += new System.EventHandler(this.pnlCash_Resize);
            // 
            // btnPos
            // 
            this.btnPos.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnPos, "btnPos");
            this.btnPos.Name = "btnPos";
            this.btnPos.UseVisualStyleBackColor = false;
            this.btnPos.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btnCoupon
            // 
            this.btnCoupon.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnCoupon, "btnCoupon");
            this.btnCoupon.Name = "btnCoupon";
            this.btnCoupon.UseVisualStyleBackColor = false;
            this.btnCoupon.Click += new System.EventHandler(this.btnCoupon_Click);
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnCash, "btnCash");
            this.btnCash.Name = "btnCash";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.EnabledChanged += new System.EventHandler(this.btnCash_EnabledChanged);
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // btnYCT
            // 
            this.btnYCT.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnYCT, "btnYCT");
            this.btnYCT.Name = "btnYCT";
            this.btnYCT.UseVisualStyleBackColor = false;
            this.btnYCT.Click += new System.EventHandler(this.btnYCT_Click);
            // 
            // entrancePanel1
            // 
            resources.ApplyResources(this.entrancePanel1, "entrancePanel1");
            this.entrancePanel1.Name = "entrancePanel1";
            this.entrancePanel1.EntranceSelectedChanged += new System.EventHandler(this.entrancePanel1_EntranceSelectedChanged);
            // 
            // carTypePanel1
            // 
            this.carTypePanel1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.carTypePanel1, "carTypePanel1");
            this.carTypePanel1.Name = "carTypePanel1";
            this.carTypePanel1.CarTypeSelectedChanged += new System.EventHandler(this.ChargeType_Selected);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonFace;
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
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 16);
            this.tableLayoutPanel1.Controls.Add(this.txtCardID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOwnerName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCarNum, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblEnterDateTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblExitDateTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblParkingTime, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblCardType, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblTariffType, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblLastTotalPaid, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblAccounts, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblDiscount, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtMemo, 1, 16);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblLastWorkstation, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtPaid, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.lblDiscountHour, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.lblDiscountMemo, 1, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // txtCardID
            // 
            this.txtCardID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Enter += new System.EventHandler(this.txt_Enter);
            this.txtCardID.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblOwnerName
            // 
            resources.ApplyResources(this.lblOwnerName, "lblOwnerName");
            this.lblOwnerName.Name = "lblOwnerName";
            // 
            // lblCarNum
            // 
            resources.ApplyResources(this.lblCarNum, "lblCarNum");
            this.lblCarNum.Name = "lblCarNum";
            // 
            // lblEnterDateTime
            // 
            resources.ApplyResources(this.lblEnterDateTime, "lblEnterDateTime");
            this.lblEnterDateTime.Name = "lblEnterDateTime";
            // 
            // lblExitDateTime
            // 
            resources.ApplyResources(this.lblExitDateTime, "lblExitDateTime");
            this.lblExitDateTime.Name = "lblExitDateTime";
            // 
            // lblParkingTime
            // 
            resources.ApplyResources(this.lblParkingTime, "lblParkingTime");
            this.lblParkingTime.ForeColor = System.Drawing.Color.Blue;
            this.lblParkingTime.Name = "lblParkingTime";
            // 
            // lblCardType
            // 
            resources.ApplyResources(this.lblCardType, "lblCardType");
            this.lblCardType.Name = "lblCardType";
            // 
            // lblTariffType
            // 
            resources.ApplyResources(this.lblTariffType, "lblTariffType");
            this.lblTariffType.Name = "lblTariffType";
            // 
            // lblLastTotalPaid
            // 
            resources.ApplyResources(this.lblLastTotalPaid, "lblLastTotalPaid");
            this.lblLastTotalPaid.Name = "lblLastTotalPaid";
            // 
            // lblAccounts
            // 
            resources.ApplyResources(this.lblAccounts, "lblAccounts");
            this.lblAccounts.ForeColor = System.Drawing.Color.Blue;
            this.lblAccounts.Name = "lblAccounts";
            // 
            // lblDiscount
            // 
            resources.ApplyResources(this.lblDiscount, "lblDiscount");
            this.lblDiscount.ForeColor = System.Drawing.Color.Red;
            this.lblDiscount.Name = "lblDiscount";
            // 
            // txtMemo
            // 
            resources.ApplyResources(this.txtMemo, "txtMemo");
            this.txtMemo.FormattingEnabled = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Enter += new System.EventHandler(this.txt_Enter);
            this.txtMemo.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // lblLastWorkstation
            // 
            resources.ApplyResources(this.lblLastWorkstation, "lblLastWorkstation");
            this.lblLastWorkstation.Name = "lblLastWorkstation";
            // 
            // txtPaid
            // 
            resources.ApplyResources(this.txtPaid, "txtPaid");
            this.txtPaid.ForeColor = System.Drawing.Color.Blue;
            this.txtPaid.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            131072});
            this.txtPaid.MinValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147352576});
            this.txtPaid.Name = "txtPaid";
            this.txtPaid.NumberWithCommas = true;
            this.txtPaid.PointCount = 2;
            this.txtPaid.TextChanged += new System.EventHandler(this.txtPaid_TextChanged);
            this.txtPaid.Enter += new System.EventHandler(this.txt_Enter);
            this.txtPaid.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblDiscountHour
            // 
            resources.ApplyResources(this.lblDiscountHour, "lblDiscountHour");
            this.lblDiscountHour.Name = "lblDiscountHour";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblDiscountMemo
            // 
            resources.ApplyResources(this.lblDiscountMemo, "lblDiscountMemo");
            this.lblDiscountMemo.Name = "lblDiscountMemo";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel5.Controls.Add(this.label15);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.splitTop);
            this.panel4.Controls.Add(this.videoPanel);
            this.panel4.Controls.Add(this.spliterLeft);
            this.panel4.Controls.Add(this.paymentPanel);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.ucapmMonitor1);
            this.panel3.Controls.Add(this.eventList);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.splitter2, "splitter2");
            this.splitter2.Name = "splitter2";
            this.splitter2.TabStop = false;
            // 
            // ucapmMonitor1
            // 
            this.ucapmMonitor1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.ucapmMonitor1, "ucapmMonitor1");
            this.ucapmMonitor1.Name = "ucapmMonitor1";
            // 
            // eventList
            // 
            resources.ApplyResources(this.eventList, "eventList");
            this.eventList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eventList.FormattingEnabled = true;
            this.eventList.Name = "eventList";
            // 
            // splitTop
            // 
            this.splitTop.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.splitTop, "splitTop");
            this.splitTop.Name = "splitTop";
            this.splitTop.TabStop = false;
            // 
            // videoPanel
            // 
            this.videoPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.videoPanel.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.videoPanel, "videoPanel");
            this.videoPanel.Name = "videoPanel";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.picIn, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ucVideoes, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // picIn
            // 
            this.picIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.picIn, "picIn");
            this.picIn.Name = "picIn";
            // 
            // ucVideoes
            // 
            this.ucVideoes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.ucVideoes, "ucVideoes");
            this.ucVideoes.Name = "ucVideoes";
            // 
            // spliterLeft
            // 
            this.spliterLeft.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.spliterLeft, "spliterLeft");
            this.spliterLeft.Name = "spliterLeft";
            this.spliterLeft.TabStop = false;
            // 
            // tmr_YCT
            // 
            this.tmr_YCT.Interval = 500;
            this.tmr_YCT.Tick += new System.EventHandler(this.tmr_YCT_Tick);
            // 
            // FrmCardPaying
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.CancelButton = this.btnInvalidEvent;
            this.Controls.Add(this.panel4);
            this.KeyPreview = true;
            this.Name = "FrmCardPaying";
            this.Activated += new System.EventHandler(this.FrmCardPaying_Activated);
            this.Deactivate += new System.EventHandler(this.FrmCardPaying_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCardPaying_FormClosed);
            this.Load += new System.EventHandler(this.FrmCardPaying_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCardPaying_KeyDown);
            this.paymentPanel.ResumeLayout(false);
            this.paymentPanel.PerformLayout();
            this.buttonPnl.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.pnlCash.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.videoPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paymentPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitTop;
        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.Splitter spliterLeft;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Ralid.Park.UserControls.EventReportListBox eventList;
        private Ralid.Park.UserControls.UCPictureListView picIn;
        private Ralid.Park.UserControls.VideoPanels.UCVideoListView ucVideoes;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Label label17;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private System.Windows.Forms.Label lblOwnerName;
        private System.Windows.Forms.Label lblCarNum;
        private System.Windows.Forms.Label lblEnterDateTime;
        private System.Windows.Forms.Label lblExitDateTime;
        private System.Windows.Forms.Label lblParkingTime;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblTariffType;
        private System.Windows.Forms.Label lblLastTotalPaid;
        private System.Windows.Forms.Label lblAccounts;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.ComboBox txtMemo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblLastWorkstation;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnCardOk;
        private System.Windows.Forms.Button btnInvalidEvent;
        private System.Windows.Forms.Panel pnlCash;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.Button btnYCT;
        private UserControls.CarTypePanel carTypePanel1;
        private GeneralLibrary.WinformControl.DecimalTextBox txtPaid;
        private System.Windows.Forms.Splitter splitter2;
        private UserControls.UCAPMMonitor ucapmMonitor1;
        private UserControls.EntrancePanel entrancePanel1;
        private System.Windows.Forms.Panel buttonPnl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDiscountHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDiscountMemo;
        private System.Windows.Forms.Button btnPos;
        private System.Windows.Forms.Button btnCoupon;
        private System.Windows.Forms.Timer tmr_YCT;

    }
}