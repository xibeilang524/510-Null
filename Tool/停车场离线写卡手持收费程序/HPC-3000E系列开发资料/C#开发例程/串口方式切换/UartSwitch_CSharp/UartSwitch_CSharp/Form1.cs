using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UartSwicthSpace;
 

namespace UartSwitch_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //初始化串口方式选择
            if (true != UartSwichSet.UartSwitchInit())
            {
                MessageBox.Show("初始化串口方式选择失败！");
                Application.Exit();
            }

        }

        private void UART_TTL_Click(object sender, EventArgs e)
        {
            if (true != UartSwichSet.UartSwitchSetting(UartSwich_State.UART_TTL))
            {
                MessageBox.Show("串口方式选择TTL失败！");
                Application.Exit();
            }
            MessageBox.Show("串口方式选择TTL成功！");
        }

        private void UART_RS232_Click(object sender, EventArgs e)
        {
            if (true != UartSwichSet.UartSwitchSetting(UartSwich_State.UART_RS232))
            {
                MessageBox.Show("串口方式选择RS232失败！");
                Application.Exit();
            }
            MessageBox.Show("串口方式选择RS232成功！");
        }

        private void UART_RS485_Click(object sender, EventArgs e)
        {
            if (true != UartSwichSet.UartSwitchSetting(UartSwich_State.UART_RS485))
            {
                MessageBox.Show("串口方式选择RS485失败！");
                Application.Exit();
            }
            MessageBox.Show("串口方式选择RS485成功！");
        }

        private void UART_NC_Click(object sender, EventArgs e)
        {
            if (true != UartSwichSet.UartSwitchSetting(UartSwich_State.UART_NC))
            {
                MessageBox.Show("串口方式选择无连接方式失败！");
                Application.Exit();
            }
            MessageBox.Show("串口方式选择无连接方式成功！");
        }


    }
}