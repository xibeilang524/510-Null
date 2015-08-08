using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using Limilabs.FTP.Client;
using Ralid.OpenCard.OpenCardService.YCT;

namespace Ftptest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string zip = Path.Combine(Application.StartupPath, string.Format("{0}.zip", DateTime.Now.ToString("yyyyMMddHHmmss")));
                using (ZipFileWriter writer = new ZipFileWriter(zip))
                {
                    for (int i = 1; i < 11; i++)
                    {
                        string txt = string.Format("{0}.txt", i);
                        writer.WriteFile(txt, ASCIIEncoding.ASCII.GetBytes(txt));
                    }
                }
                MessageBox.Show("生成成功 ,文件名:" + zip);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成失败, 原因:" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
