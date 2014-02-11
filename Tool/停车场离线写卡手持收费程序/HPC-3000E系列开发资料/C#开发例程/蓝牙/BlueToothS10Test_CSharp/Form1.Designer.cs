namespace BlueToothS10Test_CSharp
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bnRestore = new System.Windows.Forms.Button();
            this.bnSoftVersion = new System.Windows.Forms.Button();
            this.bnReboot = new System.Windows.Forms.Button();
            this.bnSetMode = new System.Windows.Forms.Button();
            this.bnClose = new System.Windows.Forms.Button();
            this.bnOpen = new System.Windows.Forms.Button();
            this.cbbMode = new System.Windows.Forms.ComboBox();
            this.cbbCom = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbRole = new System.Windows.Forms.ComboBox();
            this.bnGetPSW = new System.Windows.Forms.Button();
            this.bnGetRole = new System.Windows.Forms.Button();
            this.bnSetRole = new System.Windows.Forms.Button();
            this.bnSetPSW = new System.Windows.Forms.Button();
            this.bnGetIAC = new System.Windows.Forms.Button();
            this.bnSetIAC = new System.Windows.Forms.Button();
            this.bnGetClass = new System.Windows.Forms.Button();
            this.bnSetClass = new System.Windows.Forms.Button();
            this.bnGetName = new System.Windows.Forms.Button();
            this.bnSetName = new System.Windows.Forms.Button();
            this.bnAddr = new System.Windows.Forms.Button();
            this.txtPSW = new System.Windows.Forms.TextBox();
            this.txtIAC = new System.Windows.Forms.TextBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lstDevice = new System.Windows.Forms.ListView();
            this.bnDisconnect = new System.Windows.Forms.Button();
            this.bnConnect = new System.Windows.Forms.Button();
            this.bnMatch = new System.Windows.Forms.Button();
            this.bnBind = new System.Windows.Forms.Button();
            this.bnStopSearch = new System.Windows.Forms.Button();
            this.bnStartSearch = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.bnSend = new System.Windows.Forms.Button();
            this.bnOutClear = new System.Windows.Forms.Button();
            this.bnInClear = new System.Windows.Forms.Button();
            this.bnOutHex = new System.Windows.Forms.CheckBox();
            this.bnInHex = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(238, 295);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bnRestore);
            this.tabPage1.Controls.Add(this.bnSoftVersion);
            this.tabPage1.Controls.Add(this.bnReboot);
            this.tabPage1.Controls.Add(this.bnSetMode);
            this.tabPage1.Controls.Add(this.bnClose);
            this.tabPage1.Controls.Add(this.bnOpen);
            this.tabPage1.Controls.Add(this.cbbMode);
            this.tabPage1.Controls.Add(this.cbbCom);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(230, 266);
            this.tabPage1.Text = "Basic";
            // 
            // bnRestore
            // 
            this.bnRestore.Location = new System.Drawing.Point(45, 212);
            this.bnRestore.Name = "bnRestore";
            this.bnRestore.Size = new System.Drawing.Size(141, 20);
            this.bnRestore.TabIndex = 1;
            this.bnRestore.Text = "restore setting";
            // 
            // bnSoftVersion
            // 
            this.bnSoftVersion.Location = new System.Drawing.Point(45, 167);
            this.bnSoftVersion.Name = "bnSoftVersion";
            this.bnSoftVersion.Size = new System.Drawing.Size(141, 20);
            this.bnSoftVersion.TabIndex = 1;
            this.bnSoftVersion.Text = "software version";
            this.bnSoftVersion.Click += new System.EventHandler(this.bnSoftVersion_Click);
            // 
            // bnReboot
            // 
            this.bnReboot.Location = new System.Drawing.Point(45, 122);
            this.bnReboot.Name = "bnReboot";
            this.bnReboot.Size = new System.Drawing.Size(141, 20);
            this.bnReboot.TabIndex = 1;
            this.bnReboot.Text = "reboot device";
            this.bnReboot.Click += new System.EventHandler(this.bnReboot_Click);
            // 
            // bnSetMode
            // 
            this.bnSetMode.Location = new System.Drawing.Point(134, 68);
            this.bnSetMode.Name = "bnSetMode";
            this.bnSetMode.Size = new System.Drawing.Size(88, 20);
            this.bnSetMode.TabIndex = 1;
            this.bnSetMode.Text = "set mode";
            this.bnSetMode.Click += new System.EventHandler(this.bnSetMode_Click);
            // 
            // bnClose
            // 
            this.bnClose.Enabled = false;
            this.bnClose.Location = new System.Drawing.Point(167, 26);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(63, 20);
            this.bnClose.TabIndex = 1;
            this.bnClose.Text = "Close";
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // bnOpen
            // 
            this.bnOpen.Location = new System.Drawing.Point(98, 26);
            this.bnOpen.Name = "bnOpen";
            this.bnOpen.Size = new System.Drawing.Size(63, 20);
            this.bnOpen.TabIndex = 1;
            this.bnOpen.Text = "Open";
            this.bnOpen.Click += new System.EventHandler(this.bnOpen_Click);
            // 
            // cbbMode
            // 
            this.cbbMode.DisplayMember = "at;";
            this.cbbMode.Location = new System.Drawing.Point(14, 65);
            this.cbbMode.Name = "cbbMode";
            this.cbbMode.Size = new System.Drawing.Size(110, 23);
            this.cbbMode.TabIndex = 0;
            this.cbbMode.ValueMember = "at;";
            // 
            // cbbCom
            // 
            this.cbbCom.Location = new System.Drawing.Point(14, 23);
            this.cbbCom.Name = "cbbCom";
            this.cbbCom.Size = new System.Drawing.Size(83, 23);
            this.cbbCom.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cbbRole);
            this.tabPage2.Controls.Add(this.bnGetPSW);
            this.tabPage2.Controls.Add(this.bnGetRole);
            this.tabPage2.Controls.Add(this.bnSetRole);
            this.tabPage2.Controls.Add(this.bnSetPSW);
            this.tabPage2.Controls.Add(this.bnGetIAC);
            this.tabPage2.Controls.Add(this.bnSetIAC);
            this.tabPage2.Controls.Add(this.bnGetClass);
            this.tabPage2.Controls.Add(this.bnSetClass);
            this.tabPage2.Controls.Add(this.bnGetName);
            this.tabPage2.Controls.Add(this.bnSetName);
            this.tabPage2.Controls.Add(this.bnAddr);
            this.tabPage2.Controls.Add(this.txtPSW);
            this.tabPage2.Controls.Add(this.txtIAC);
            this.tabPage2.Controls.Add(this.txtClass);
            this.tabPage2.Controls.Add(this.txtName);
            this.tabPage2.Controls.Add(this.txtAddr);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(230, 266);
            this.tabPage2.Text = "Setting";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(98, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 20);
            this.label2.Text = "h";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(98, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 20);
            this.label1.Text = "h";
            // 
            // cbbRole
            // 
            this.cbbRole.Location = new System.Drawing.Point(12, 125);
            this.cbbRole.Name = "cbbRole";
            this.cbbRole.Size = new System.Drawing.Size(84, 23);
            this.cbbRole.TabIndex = 2;
            // 
            // bnGetPSW
            // 
            this.bnGetPSW.Location = new System.Drawing.Point(170, 227);
            this.bnGetPSW.Name = "bnGetPSW";
            this.bnGetPSW.Size = new System.Drawing.Size(59, 20);
            this.bnGetPSW.TabIndex = 1;
            this.bnGetPSW.Text = "GetPSW";
            // 
            // bnGetRole
            // 
            this.bnGetRole.Location = new System.Drawing.Point(170, 128);
            this.bnGetRole.Name = "bnGetRole";
            this.bnGetRole.Size = new System.Drawing.Size(59, 20);
            this.bnGetRole.TabIndex = 1;
            this.bnGetRole.Text = "GetRole";
            // 
            // bnSetRole
            // 
            this.bnSetRole.Location = new System.Drawing.Point(110, 128);
            this.bnSetRole.Name = "bnSetRole";
            this.bnSetRole.Size = new System.Drawing.Size(59, 20);
            this.bnSetRole.TabIndex = 1;
            this.bnSetRole.Text = "SetRole";
            // 
            // bnSetPSW
            // 
            this.bnSetPSW.Location = new System.Drawing.Point(110, 227);
            this.bnSetPSW.Name = "bnSetPSW";
            this.bnSetPSW.Size = new System.Drawing.Size(59, 20);
            this.bnSetPSW.TabIndex = 1;
            this.bnSetPSW.Text = "SetPSW";
            // 
            // bnGetIAC
            // 
            this.bnGetIAC.Location = new System.Drawing.Point(170, 194);
            this.bnGetIAC.Name = "bnGetIAC";
            this.bnGetIAC.Size = new System.Drawing.Size(59, 20);
            this.bnGetIAC.TabIndex = 1;
            this.bnGetIAC.Text = "GetIAC";
            // 
            // bnSetIAC
            // 
            this.bnSetIAC.Location = new System.Drawing.Point(110, 194);
            this.bnSetIAC.Name = "bnSetIAC";
            this.bnSetIAC.Size = new System.Drawing.Size(59, 20);
            this.bnSetIAC.TabIndex = 1;
            this.bnSetIAC.Text = "SetIAC";
            // 
            // bnGetClass
            // 
            this.bnGetClass.Location = new System.Drawing.Point(170, 161);
            this.bnGetClass.Name = "bnGetClass";
            this.bnGetClass.Size = new System.Drawing.Size(59, 20);
            this.bnGetClass.TabIndex = 1;
            this.bnGetClass.Text = "GetClass";
            // 
            // bnSetClass
            // 
            this.bnSetClass.Location = new System.Drawing.Point(110, 161);
            this.bnSetClass.Name = "bnSetClass";
            this.bnSetClass.Size = new System.Drawing.Size(59, 20);
            this.bnSetClass.TabIndex = 1;
            this.bnSetClass.Text = "SetClass";
            // 
            // bnGetName
            // 
            this.bnGetName.Location = new System.Drawing.Point(145, 90);
            this.bnGetName.Name = "bnGetName";
            this.bnGetName.Size = new System.Drawing.Size(63, 20);
            this.bnGetName.TabIndex = 1;
            this.bnGetName.Text = "bnGetName";
            // 
            // bnSetName
            // 
            this.bnSetName.Location = new System.Drawing.Point(22, 90);
            this.bnSetName.Name = "bnSetName";
            this.bnSetName.Size = new System.Drawing.Size(63, 20);
            this.bnSetName.TabIndex = 1;
            this.bnSetName.Text = "bnSetName";
            // 
            // bnAddr
            // 
            this.bnAddr.Location = new System.Drawing.Point(155, 22);
            this.bnAddr.Name = "bnAddr";
            this.bnAddr.Size = new System.Drawing.Size(61, 20);
            this.bnAddr.TabIndex = 1;
            this.bnAddr.Text = "GetAddr";
            // 
            // txtPSW
            // 
            this.txtPSW.Location = new System.Drawing.Point(12, 224);
            this.txtPSW.Name = "txtPSW";
            this.txtPSW.Size = new System.Drawing.Size(84, 23);
            this.txtPSW.TabIndex = 0;
            // 
            // txtIAC
            // 
            this.txtIAC.Location = new System.Drawing.Point(12, 191);
            this.txtIAC.Name = "txtIAC";
            this.txtIAC.Size = new System.Drawing.Size(84, 23);
            this.txtIAC.TabIndex = 0;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(12, 158);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(84, 23);
            this.txtClass.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 58);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(204, 23);
            this.txtName.TabIndex = 0;
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(12, 19);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(128, 23);
            this.txtAddr.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lstDevice);
            this.tabPage3.Controls.Add(this.bnDisconnect);
            this.tabPage3.Controls.Add(this.bnConnect);
            this.tabPage3.Controls.Add(this.bnMatch);
            this.tabPage3.Controls.Add(this.bnBind);
            this.tabPage3.Controls.Add(this.bnStopSearch);
            this.tabPage3.Controls.Add(this.bnStartSearch);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(230, 266);
            this.tabPage3.Text = "Search";
            // 
            // lstDevice
            // 
            this.lstDevice.Location = new System.Drawing.Point(24, 61);
            this.lstDevice.Name = "lstDevice";
            this.lstDevice.Size = new System.Drawing.Size(183, 104);
            this.lstDevice.TabIndex = 1;
            this.lstDevice.View = System.Windows.Forms.View.Details;
            // 
            // bnDisconnect
            // 
            this.bnDisconnect.Location = new System.Drawing.Point(129, 219);
            this.bnDisconnect.Name = "bnDisconnect";
            this.bnDisconnect.Size = new System.Drawing.Size(78, 20);
            this.bnDisconnect.TabIndex = 0;
            this.bnDisconnect.Text = "disconnect";
            // 
            // bnConnect
            // 
            this.bnConnect.Location = new System.Drawing.Point(24, 219);
            this.bnConnect.Name = "bnConnect";
            this.bnConnect.Size = new System.Drawing.Size(78, 20);
            this.bnConnect.TabIndex = 0;
            this.bnConnect.Text = "connect";
            // 
            // bnMatch
            // 
            this.bnMatch.Location = new System.Drawing.Point(129, 182);
            this.bnMatch.Name = "bnMatch";
            this.bnMatch.Size = new System.Drawing.Size(78, 20);
            this.bnMatch.TabIndex = 0;
            this.bnMatch.Text = "match";
            // 
            // bnBind
            // 
            this.bnBind.Location = new System.Drawing.Point(24, 182);
            this.bnBind.Name = "bnBind";
            this.bnBind.Size = new System.Drawing.Size(78, 20);
            this.bnBind.TabIndex = 0;
            this.bnBind.Text = "bind";
            // 
            // bnStopSearch
            // 
            this.bnStopSearch.Location = new System.Drawing.Point(129, 23);
            this.bnStopSearch.Name = "bnStopSearch";
            this.bnStopSearch.Size = new System.Drawing.Size(78, 20);
            this.bnStopSearch.TabIndex = 0;
            this.bnStopSearch.Text = "stop search";
            // 
            // bnStartSearch
            // 
            this.bnStartSearch.Location = new System.Drawing.Point(24, 23);
            this.bnStartSearch.Name = "bnStartSearch";
            this.bnStartSearch.Size = new System.Drawing.Size(78, 20);
            this.bnStartSearch.TabIndex = 0;
            this.bnStartSearch.Text = "start search";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.bnSend);
            this.tabPage4.Controls.Add(this.bnOutClear);
            this.tabPage4.Controls.Add(this.bnInClear);
            this.tabPage4.Controls.Add(this.bnOutHex);
            this.tabPage4.Controls.Add(this.bnInHex);
            this.tabPage4.Controls.Add(this.textBox2);
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(230, 266);
            this.tabPage4.Text = "Transport";
            // 
            // bnSend
            // 
            this.bnSend.Location = new System.Drawing.Point(146, 232);
            this.bnSend.Name = "bnSend";
            this.bnSend.Size = new System.Drawing.Size(59, 20);
            this.bnSend.TabIndex = 2;
            this.bnSend.Text = "send";
            // 
            // bnOutClear
            // 
            this.bnOutClear.Location = new System.Drawing.Point(81, 232);
            this.bnOutClear.Name = "bnOutClear";
            this.bnOutClear.Size = new System.Drawing.Size(59, 20);
            this.bnOutClear.TabIndex = 2;
            this.bnOutClear.Text = "clear";
            // 
            // bnInClear
            // 
            this.bnInClear.Location = new System.Drawing.Point(81, 102);
            this.bnInClear.Name = "bnInClear";
            this.bnInClear.Size = new System.Drawing.Size(59, 20);
            this.bnInClear.TabIndex = 2;
            this.bnInClear.Text = "clear";
            // 
            // bnOutHex
            // 
            this.bnOutHex.Location = new System.Drawing.Point(13, 232);
            this.bnOutHex.Name = "bnOutHex";
            this.bnOutHex.Size = new System.Drawing.Size(62, 20);
            this.bnOutHex.TabIndex = 1;
            this.bnOutHex.Text = "hex";
            // 
            // bnInHex
            // 
            this.bnInHex.Location = new System.Drawing.Point(13, 102);
            this.bnInHex.Name = "bnInHex";
            this.bnInHex.Size = new System.Drawing.Size(62, 20);
            this.bnInHex.TabIndex = 1;
            this.bnInHex.Text = "hex";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 136);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(205, 90);
            this.textBox2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 93);
            this.textBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button bnRestore;
        private System.Windows.Forms.Button bnSoftVersion;
        private System.Windows.Forms.Button bnReboot;
        private System.Windows.Forms.Button bnSetMode;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.Button bnOpen;
        private System.Windows.Forms.ComboBox cbbMode;
        private System.Windows.Forms.ComboBox cbbCom;
        private System.Windows.Forms.Button bnAddr;
        private System.Windows.Forms.TextBox txtPSW;
        private System.Windows.Forms.TextBox txtIAC;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.ComboBox cbbRole;
        private System.Windows.Forms.Button bnGetPSW;
        private System.Windows.Forms.Button bnGetRole;
        private System.Windows.Forms.Button bnSetRole;
        private System.Windows.Forms.Button bnSetPSW;
        private System.Windows.Forms.Button bnGetIAC;
        private System.Windows.Forms.Button bnSetIAC;
        private System.Windows.Forms.Button bnGetClass;
        private System.Windows.Forms.Button bnSetClass;
        private System.Windows.Forms.Button bnGetName;
        private System.Windows.Forms.Button bnSetName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnDisconnect;
        private System.Windows.Forms.Button bnConnect;
        private System.Windows.Forms.Button bnMatch;
        private System.Windows.Forms.Button bnBind;
        private System.Windows.Forms.Button bnStopSearch;
        private System.Windows.Forms.Button bnStartSearch;
        private System.Windows.Forms.ListView lstDevice;
        private System.Windows.Forms.CheckBox bnInHex;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bnSend;
        private System.Windows.Forms.Button bnOutClear;
        private System.Windows.Forms.Button bnInClear;
        private System.Windows.Forms.CheckBox bnOutHex;
        private System.Windows.Forms.TextBox textBox2;
    }
}

