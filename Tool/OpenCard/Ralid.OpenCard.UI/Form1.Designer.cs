namespace Ralid.OpenCard.UI
{
    partial class Form1
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
            this.txtIP = new Ralid.GeneralLibrary.WinformControl.UCIPTextBox();
            this.ip2 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip4 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip3 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.ip1 = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new Ralid.GeneralLibrary.WinformControl.IntergerTextBox(this.components);
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.eventList = new System.Windows.Forms.TextBox();
            this.txtIP.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Controls.Add(this.ip2);
            this.txtIP.Controls.Add(this.ip4);
            this.txtIP.Controls.Add(this.ip3);
            this.txtIP.Controls.Add(this.ip1);
            this.txtIP.Location = new System.Drawing.Point(27, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(226, 28);
            this.txtIP.TabIndex = 3;
            // 
            // ip2
            // 
            this.ip2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip2.Location = new System.Drawing.Point(61, 3);
            this.ip2.MaxValue = 255;
            this.ip2.MinValue = 0;
            this.ip2.Name = "ip2";
            this.ip2.NumberWithCommas = false;
            this.ip2.Size = new System.Drawing.Size(39, 21);
            this.ip2.TabIndex = 147;
            this.ip2.Text = "0";
            // 
            // ip4
            // 
            this.ip4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip4.Location = new System.Drawing.Point(177, 3);
            this.ip4.MaxValue = 255;
            this.ip4.MinValue = 0;
            this.ip4.Name = "ip4";
            this.ip4.NumberWithCommas = false;
            this.ip4.Size = new System.Drawing.Size(39, 21);
            this.ip4.TabIndex = 149;
            this.ip4.Text = "0";
            // 
            // ip3
            // 
            this.ip3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip3.Location = new System.Drawing.Point(119, 3);
            this.ip3.MaxValue = 255;
            this.ip3.MinValue = 0;
            this.ip3.Name = "ip3";
            this.ip3.NumberWithCommas = false;
            this.ip3.Size = new System.Drawing.Size(39, 21);
            this.ip3.TabIndex = 148;
            this.ip3.Text = "0";
            // 
            // ip1
            // 
            this.ip1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ip1.Location = new System.Drawing.Point(3, 3);
            this.ip1.MaxValue = 255;
            this.ip1.MinValue = 0;
            this.ip1.Name = "ip1";
            this.ip1.NumberWithCommas = false;
            this.ip1.Size = new System.Drawing.Size(39, 21);
            this.ip1.TabIndex = 146;
            this.ip1.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            // 
            // txtPort
            // 
            this.txtPort.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPort.Location = new System.Drawing.Point(266, 15);
            this.txtPort.MaxValue = 2147483647;
            this.txtPort.MinValue = -2147483648;
            this.txtPort.Name = "txtPort";
            this.txtPort.NumberWithCommas = false;
            this.txtPort.Size = new System.Drawing.Size(38, 21);
            this.txtPort.TabIndex = 5;
            this.txtPort.Text = "0";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(322, 18);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(415, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "断开";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // eventList
            // 
            this.eventList.Location = new System.Drawing.Point(13, 51);
            this.eventList.Multiline = true;
            this.eventList.Name = "eventList";
            this.eventList.Size = new System.Drawing.Size(538, 334);
            this.eventList.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 397);
            this.Controls.Add(this.eventList);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.txtIP.ResumeLayout(false);
            this.txtIP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GeneralLibrary.WinformControl.UCIPTextBox txtIP;
        private GeneralLibrary.WinformControl.IntergerTextBox ip2;
        private GeneralLibrary.WinformControl.IntergerTextBox ip4;
        private GeneralLibrary.WinformControl.IntergerTextBox ip3;
        private GeneralLibrary.WinformControl.IntergerTextBox ip1;
        private System.Windows.Forms.Label label1;
        private GeneralLibrary.WinformControl.IntergerTextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox eventList;
    }
}