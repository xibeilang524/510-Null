using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.OpenCard.OpenCardService;

namespace Ralid.OpenCard.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 私有变量
        private LJHSocket _Socket = null;
        #endregion

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_Socket == null)
            {
                _Socket = new LJHSocket();
                _Socket.OnDataArrivedEvent += new GeneralLibrary.DataArrivedDelegate(_Socket_OnDataArrivedEvent);
            }
            _Socket.IP = txtIP.IP;
            _Socket.Port = txtPort.IntergerValue;
            _Socket.Open();
            eventList.Text += string.Format("连接 {0}", _Socket.IsConnected ? "成功" : "失败") + "\r\n";
        }

        private void _Socket_OnDataArrivedEvent(object sender, byte[] data)
        {
            this.Invoke((Action)(() =>
            {
                string msg = UTF32Encoding.Default.GetString(data);
                if (!string.IsNullOrEmpty(msg))
                {
                    eventList.Text += msg +"\r\n";
                }
                else
                {
                    eventList.Text += Ralid.GeneralLibrary.HexStringConverter.HexToString(data, " ") +"\r\n";
                }
            }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_Socket != null) _Socket.Close();
            eventList.Text += "断开连接" + "\r\n";
        }
    }
}
