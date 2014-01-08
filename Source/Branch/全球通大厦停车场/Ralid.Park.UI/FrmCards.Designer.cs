namespace Ralid.Park.UI
{
    partial class FrmCards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCards));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.CardOperatorMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnu_CardRelease = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardCharge = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardDefer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_CardLost = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardRecycle = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_CardClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Property = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cardView = new Ralid.Park.UserControls.CardGridView(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.accessComboBox1 = new Ralid.Park.UserControls.AccessComboBox(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.txtCardCertificate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.txtCarPlate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCarStatus = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtOwnerName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClosePanel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comCardStatus = new Ralid.Park.UserControls.CardStatusComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.comChargeType = new Ralid.Park.UserControls.CarTypeComboBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtCardID = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.comCardType = new Ralid.Park.UserControls.CardTypeComboBox(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_CardRelease = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_CardCharge = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_CardDefer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_CardRecycle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_FindCard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Refresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_AddCards = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_DownloadCards = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportCards = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_BulkChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_CardIDConvert = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_AddManageCard = new System.Windows.Forms.ToolStripButton();
            this.btn_ChangeCardKey = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.CardOperatorMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CardOperatorMenuStrip
            // 
            resources.ApplyResources(this.CardOperatorMenuStrip, "CardOperatorMenuStrip");
            this.CardOperatorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_CardRelease,
            this.mnu_CardCharge,
            this.mnu_CardDefer,
            this.toolStripSeparator13,
            this.mnu_CardLost,
            this.mnu_CardRestore,
            this.mnu_CardDisable,
            this.mnu_CardEnable,
            this.mnu_CardRecycle,
            this.mnu_CardDownload,
            this.mnu_CardClear,
            this.toolStripSeparator15,
            this.mnu_Delete,
            this.mnu_Property});
            this.CardOperatorMenuStrip.Name = "contextMenuStrip1";
            // 
            // mnu_CardRelease
            // 
            resources.ApplyResources(this.mnu_CardRelease, "mnu_CardRelease");
            this.mnu_CardRelease.Name = "mnu_CardRelease";
            this.mnu_CardRelease.Click += new System.EventHandler(this.mnu_CardRelease_Click);
            // 
            // mnu_CardCharge
            // 
            resources.ApplyResources(this.mnu_CardCharge, "mnu_CardCharge");
            this.mnu_CardCharge.Name = "mnu_CardCharge";
            this.mnu_CardCharge.Click += new System.EventHandler(this.mnu_CardCharge_Click);
            // 
            // mnu_CardDefer
            // 
            resources.ApplyResources(this.mnu_CardDefer, "mnu_CardDefer");
            this.mnu_CardDefer.Name = "mnu_CardDefer";
            this.mnu_CardDefer.Click += new System.EventHandler(this.mnu_CardDefer_Click);
            // 
            // toolStripSeparator13
            // 
            resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            // 
            // mnu_CardLost
            // 
            resources.ApplyResources(this.mnu_CardLost, "mnu_CardLost");
            this.mnu_CardLost.Name = "mnu_CardLost";
            this.mnu_CardLost.Click += new System.EventHandler(this.mnu_CardLostRestore_Click);
            // 
            // mnu_CardRestore
            // 
            resources.ApplyResources(this.mnu_CardRestore, "mnu_CardRestore");
            this.mnu_CardRestore.Name = "mnu_CardRestore";
            this.mnu_CardRestore.Click += new System.EventHandler(this.mnu_CardLostRestore_Click);
            // 
            // mnu_CardDisable
            // 
            resources.ApplyResources(this.mnu_CardDisable, "mnu_CardDisable");
            this.mnu_CardDisable.Name = "mnu_CardDisable";
            this.mnu_CardDisable.Click += new System.EventHandler(this.mnu_CardDisableEnable_Click);
            // 
            // mnu_CardEnable
            // 
            resources.ApplyResources(this.mnu_CardEnable, "mnu_CardEnable");
            this.mnu_CardEnable.Name = "mnu_CardEnable";
            this.mnu_CardEnable.Click += new System.EventHandler(this.mnu_CardDisableEnable_Click);
            // 
            // mnu_CardRecycle
            // 
            resources.ApplyResources(this.mnu_CardRecycle, "mnu_CardRecycle");
            this.mnu_CardRecycle.Name = "mnu_CardRecycle";
            this.mnu_CardRecycle.Click += new System.EventHandler(this.mnu_CardRecycle_Click);
            // 
            // mnu_CardDownload
            // 
            resources.ApplyResources(this.mnu_CardDownload, "mnu_CardDownload");
            this.mnu_CardDownload.Name = "mnu_CardDownload";
            this.mnu_CardDownload.Click += new System.EventHandler(this.mnu_CardDownload_Click);
            // 
            // mnu_CardClear
            // 
            resources.ApplyResources(this.mnu_CardClear, "mnu_CardClear");
            this.mnu_CardClear.Name = "mnu_CardClear";
            this.mnu_CardClear.Click += new System.EventHandler(this.mnu_CardClear_Click);
            // 
            // toolStripSeparator15
            // 
            resources.ApplyResources(this.toolStripSeparator15, "toolStripSeparator15");
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            // 
            // mnu_Delete
            // 
            resources.ApplyResources(this.mnu_Delete, "mnu_Delete");
            this.mnu_Delete.Name = "mnu_Delete";
            this.mnu_Delete.Click += new System.EventHandler(this.mnu_Delete_Click);
            // 
            // mnu_Property
            // 
            resources.ApplyResources(this.mnu_Property, "mnu_Property");
            this.mnu_Property.Name = "mnu_Property";
            this.mnu_Property.Click += new System.EventHandler(this.mnu_Property_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.cardView);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panelLeft);
            this.panel1.Name = "panel1";
            // 
            // cardView
            // 
            resources.ApplyResources(this.cardView, "cardView");
            this.cardView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardView.ContextMenuStrip = this.CardOperatorMenuStrip;
            this.cardView.Name = "cardView";
            this.cardView.RowTemplate.Height = 23;
            this.cardView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDoubleClick);
            this.cardView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDown);
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panelLeft
            // 
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.accessComboBox1);
            this.panelLeft.Controls.Add(this.label9);
            this.panelLeft.Controls.Add(this.txtCardCertificate);
            this.panelLeft.Controls.Add(this.label8);
            this.panelLeft.Controls.Add(this.txtCarPlate);
            this.panelLeft.Controls.Add(this.label4);
            this.panelLeft.Controls.Add(this.label3);
            this.panelLeft.Controls.Add(this.cmbCarStatus);
            this.panelLeft.Controls.Add(this.pictureBox1);
            this.panelLeft.Controls.Add(this.txtOwnerName);
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Controls.Add(this.btnSearch);
            this.panelLeft.Controls.Add(this.comCardStatus);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Controls.Add(this.comChargeType);
            this.panelLeft.Controls.Add(this.label6);
            this.panelLeft.Controls.Add(this.txtCardID);
            this.panelLeft.Controls.Add(this.comCardType);
            this.panelLeft.Controls.Add(this.label7);
            this.panelLeft.Controls.Add(this.label5);
            this.panelLeft.Name = "panelLeft";
            // 
            // accessComboBox1
            // 
            resources.ApplyResources(this.accessComboBox1, "accessComboBox1");
            this.accessComboBox1.FormattingEnabled = true;
            this.accessComboBox1.Name = "accessComboBox1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtCardCertificate
            // 
            resources.ApplyResources(this.txtCardCertificate, "txtCardCertificate");
            this.txtCardCertificate.Name = "txtCardCertificate";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtCarPlate
            // 
            resources.ApplyResources(this.txtCarPlate, "txtCarPlate");
            this.txtCarPlate.Name = "txtCarPlate";
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
            // cmbCarStatus
            // 
            resources.ApplyResources(this.cmbCarStatus, "cmbCarStatus");
            this.cmbCarStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarStatus.FormattingEnabled = true;
            this.cmbCarStatus.Items.AddRange(new object[] {
            resources.GetString("cmbCarStatus.Items"),
            resources.GetString("cmbCarStatus.Items1"),
            resources.GetString("cmbCarStatus.Items2")});
            this.cmbCarStatus.Name = "cmbCarStatus";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::Ralid.Park.UI.Properties.Resources.CardReader;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // txtOwnerName
            // 
            resources.ApplyResources(this.txtOwnerName, "txtOwnerName");
            this.txtOwnerName.Name = "txtOwnerName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.Controls.Add(this.btnClosePanel);
            this.panel3.Name = "panel3";
            // 
            // btnClosePanel
            // 
            resources.ApplyResources(this.btnClosePanel, "btnClosePanel");
            this.btnClosePanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnClosePanel.BackgroundImage = global::Ralid.Park.UI.Properties.Resources.button_grey_close;
            this.btnClosePanel.FlatAppearance.BorderSize = 0;
            this.btnClosePanel.Name = "btnClosePanel";
            this.btnClosePanel.UseVisualStyleBackColor = false;
            this.btnClosePanel.Click += new System.EventHandler(this.btnClosePanel_Click);
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comCardStatus
            // 
            resources.ApplyResources(this.comCardStatus, "comCardStatus");
            this.comCardStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardStatus.FormattingEnabled = true;
            this.comCardStatus.Name = "comCardStatus";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comChargeType
            // 
            resources.ApplyResources(this.comChargeType, "comChargeType");
            this.comChargeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comChargeType.FormattingEnabled = true;
            this.comChargeType.Name = "comChargeType";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.TextChanged += new System.EventHandler(this.txtCardID_TextChanged);
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCount});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.Spring = true;
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_CardRelease,
            this.toolStripSeparator4,
            this.btn_CardCharge,
            this.toolStripSeparator5,
            this.btn_CardDefer,
            this.toolStripSeparator6,
            this.btn_CardRecycle,
            this.toolStripSeparator7,
            this.btn_FindCard,
            this.toolStripSeparator8,
            this.btn_Refresh,
            this.toolStripSeparator12,
            this.btn_AddCards,
            this.toolStripSeparator9,
            this.btn_DownloadCards,
            this.toolStripSeparator10,
            this.btnExportCards,
            this.toolStripSeparator11,
            this.btn_BulkChange,
            this.toolStripSeparator14,
            this.btn_CardIDConvert,
            this.toolStripSeparator16,
            this.btn_AddManageCard,
            this.btn_ChangeCardKey});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btn_CardRelease
            // 
            resources.ApplyResources(this.btn_CardRelease, "btn_CardRelease");
            this.btn_CardRelease.Image = global::Ralid.Park.UI.Properties.Resources.CardRelease;
            this.btn_CardRelease.Name = "btn_CardRelease";
            this.btn_CardRelease.Click += new System.EventHandler(this.mnu_CardRelease_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // btn_CardCharge
            // 
            resources.ApplyResources(this.btn_CardCharge, "btn_CardCharge");
            this.btn_CardCharge.Image = global::Ralid.Park.UI.Properties.Resources.CardCharge;
            this.btn_CardCharge.Name = "btn_CardCharge";
            this.btn_CardCharge.Click += new System.EventHandler(this.mnu_CardCharge_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // btn_CardDefer
            // 
            resources.ApplyResources(this.btn_CardDefer, "btn_CardDefer");
            this.btn_CardDefer.Image = global::Ralid.Park.UI.Properties.Resources.CardDefer;
            this.btn_CardDefer.Name = "btn_CardDefer";
            this.btn_CardDefer.Click += new System.EventHandler(this.mnu_CardDefer_Click);
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // btn_CardRecycle
            // 
            resources.ApplyResources(this.btn_CardRecycle, "btn_CardRecycle");
            this.btn_CardRecycle.Image = global::Ralid.Park.UI.Properties.Resources.recycle;
            this.btn_CardRecycle.Name = "btn_CardRecycle";
            this.btn_CardRecycle.Click += new System.EventHandler(this.mnu_CardRecycle_Click);
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // btn_FindCard
            // 
            resources.ApplyResources(this.btn_FindCard, "btn_FindCard");
            this.btn_FindCard.Image = global::Ralid.Park.UI.Properties.Resources.FindCard;
            this.btn_FindCard.Name = "btn_FindCard";
            this.btn_FindCard.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // btn_Refresh
            // 
            resources.ApplyResources(this.btn_Refresh, "btn_Refresh");
            this.btn_Refresh.Image = global::Ralid.Park.UI.Properties.Resources.Refresh;
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // toolStripSeparator12
            // 
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            // 
            // btn_AddCards
            // 
            resources.ApplyResources(this.btn_AddCards, "btn_AddCards");
            this.btn_AddCards.Image = global::Ralid.Park.UI.Properties.Resources.AddCards;
            this.btn_AddCards.Name = "btn_AddCards";
            this.btn_AddCards.Click += new System.EventHandler(this.btn_AddCards_Click);
            // 
            // toolStripSeparator9
            // 
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // btn_DownloadCards
            // 
            resources.ApplyResources(this.btn_DownloadCards, "btn_DownloadCards");
            this.btn_DownloadCards.Image = global::Ralid.Park.UI.Properties.Resources.DownloadCards;
            this.btn_DownloadCards.Name = "btn_DownloadCards";
            this.btn_DownloadCards.Click += new System.EventHandler(this.btn_DownLoadAllCards_Click);
            // 
            // toolStripSeparator10
            // 
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            // 
            // btnExportCards
            // 
            resources.ApplyResources(this.btnExportCards, "btnExportCards");
            this.btnExportCards.Image = global::Ralid.Park.UI.Properties.Resources.save;
            this.btnExportCards.Name = "btnExportCards";
            this.btnExportCards.Click += new System.EventHandler(this.btnExportCards_Click);
            // 
            // toolStripSeparator11
            // 
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            // 
            // btn_BulkChange
            // 
            resources.ApplyResources(this.btn_BulkChange, "btn_BulkChange");
            this.btn_BulkChange.Image = global::Ralid.Park.UI.Properties.Resources.Transfer;
            this.btn_BulkChange.Name = "btn_BulkChange";
            this.btn_BulkChange.Click += new System.EventHandler(this.btn_BulkChange_Click);
            // 
            // toolStripSeparator14
            // 
            resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            // 
            // btn_CardIDConvert
            // 
            resources.ApplyResources(this.btn_CardIDConvert, "btn_CardIDConvert");
            this.btn_CardIDConvert.Image = global::Ralid.Park.UI.Properties.Resources.Transfer1;
            this.btn_CardIDConvert.Name = "btn_CardIDConvert";
            this.btn_CardIDConvert.Click += new System.EventHandler(this.btn_CardIDConvert_Click);
            // 
            // toolStripSeparator16
            // 
            resources.ApplyResources(this.toolStripSeparator16, "toolStripSeparator16");
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            // 
            // btn_AddManageCard
            // 
            resources.ApplyResources(this.btn_AddManageCard, "btn_AddManageCard");
            this.btn_AddManageCard.Image = global::Ralid.Park.UI.Properties.Resources.ManageCard;
            this.btn_AddManageCard.Name = "btn_AddManageCard";
            this.btn_AddManageCard.Click += new System.EventHandler(this.btn_AddManageCard_Click);
            // 
            // btn_ChangeCardKey
            // 
            resources.ApplyResources(this.btn_ChangeCardKey, "btn_ChangeCardKey");
            this.btn_ChangeCardKey.Image = global::Ralid.Park.UI.Properties.Resources.ChangeKey;
            this.btn_ChangeCardKey.Name = "btn_ChangeCardKey";
            this.btn_ChangeCardKey.Click += new System.EventHandler(this.btn_ChangeCardKey_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "Record.xls";
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // FrmCards
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCards";
            this.Activated += new System.EventHandler(this.FrmCards_Activated);
            this.Deactivate += new System.EventHandler(this.FrmCards_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCards_FormClosing);
            this.Load += new System.EventHandler(this.FrmCards_Load);
            this.CardOperatorMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cardView)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip CardOperatorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardCharge;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardDefer;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardLost;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardDisable;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardEnable;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardRecycle;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardRelease;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnu_Delete;
        private System.Windows.Forms.ToolStripMenuItem mnu_Property;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnSearch;
        private Ralid.Park.UserControls.CardStatusComboBox comCardStatus;
        private System.Windows.Forms.Label label2;
        private Ralid.Park.UserControls.CarTypeComboBox comChargeType;
        private System.Windows.Forms.Label label6;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardID;
        private Ralid.Park.UserControls.CardTypeComboBox comCardType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Ralid.Park.UserControls.CardGridView cardView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClosePanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_CardRelease;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btn_CardCharge;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btn_CardDefer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btn_CardRecycle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btn_FindCard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btn_DownloadCards;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btn_AddCards;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtOwnerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel lblCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCarStatus;
        private System.Windows.Forms.ToolStripButton btn_Refresh;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCardCertificate;
        private System.Windows.Forms.Label label8;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtCarPlate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnExportCards;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private UserControls.AccessComboBox accessComboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton btn_BulkChange;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton btn_CardIDConvert;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardDownload;
        private System.Windows.Forms.ToolStripMenuItem mnu_CardClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton btn_AddManageCard;
        private System.Windows.Forms.ToolStripButton btn_ChangeCardKey;
    }
}