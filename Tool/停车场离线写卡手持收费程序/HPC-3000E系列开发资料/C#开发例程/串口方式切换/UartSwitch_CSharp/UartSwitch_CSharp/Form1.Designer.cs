namespace UartSwitch_CSharp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.UART_TTL = new System.Windows.Forms.Button();
            this.UART_RS232 = new System.Windows.Forms.Button();
            this.UART_RS485 = new System.Windows.Forms.Button();
            this.UART_NC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UART_TTL
            // 
            this.UART_TTL.Location = new System.Drawing.Point(53, 25);
            this.UART_TTL.Name = "UART_TTL";
            this.UART_TTL.Size = new System.Drawing.Size(76, 31);
            this.UART_TTL.TabIndex = 0;
            this.UART_TTL.Text = "TTL方式";
            this.UART_TTL.Click += new System.EventHandler(this.UART_TTL_Click);
            // 
            // UART_RS232
            // 
            this.UART_RS232.Location = new System.Drawing.Point(53, 73);
            this.UART_RS232.Name = "UART_RS232";
            this.UART_RS232.Size = new System.Drawing.Size(76, 31);
            this.UART_RS232.TabIndex = 1;
            this.UART_RS232.Text = "RS232方式";
            this.UART_RS232.Click += new System.EventHandler(this.UART_RS232_Click);
            // 
            // UART_RS485
            // 
            this.UART_RS485.Location = new System.Drawing.Point(53, 123);
            this.UART_RS485.Name = "UART_RS485";
            this.UART_RS485.Size = new System.Drawing.Size(76, 31);
            this.UART_RS485.TabIndex = 2;
            this.UART_RS485.Text = "RS485方式";
            this.UART_RS485.Click += new System.EventHandler(this.UART_RS485_Click);
            // 
            // UART_NC
            // 
            this.UART_NC.Location = new System.Drawing.Point(53, 170);
            this.UART_NC.Name = "UART_NC";
            this.UART_NC.Size = new System.Drawing.Size(76, 31);
            this.UART_NC.TabIndex = 3;
            this.UART_NC.Text = "悬空方式";
            this.UART_NC.Click += new System.EventHandler(this.UART_NC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(190, 219);
            this.Controls.Add(this.UART_NC);
            this.Controls.Add(this.UART_RS485);
            this.Controls.Add(this.UART_RS232);
            this.Controls.Add(this.UART_TTL);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "串口方式切换控制";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UART_TTL;
        private System.Windows.Forms.Button UART_RS232;
        private System.Windows.Forms.Button UART_RS485;
        private System.Windows.Forms.Button UART_NC;
    }
}

