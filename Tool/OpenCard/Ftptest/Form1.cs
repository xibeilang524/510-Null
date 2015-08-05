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
using ICSharpCode.SharpZipLib.Zip;

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
            string file = "123.zip";
            using (Ftp ftp = new Ftp())
            {
                ftp.Connect("192.168.0.102", 10021);
                if (ftp.Connected)
                {
                    ftp.LoginAnonymous();
                    if (ftp.FileExists(file))
                    {
                        ftp.Download(file, System.IO.Path.Combine(@"f:\yct", file));
                        ZipFile zip = new ZipFile(System.IO.Path.Combine(@"f:\yct", file));
                        foreach (ZipEntry item in zip)
                        {
                            var stream = zip.GetInputStream(item);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dig = new OpenFileDialog();
            if (dig.ShowDialog() == DialogResult.OK)
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(dig.FileName)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (theEntry.IsFile && Path.GetFileName(theEntry.Name).IndexOf("MD") == 0)
                        {
                            StreamReader r = new StreamReader(s);
                            int count = 0;
                            while (!r.EndOfStream)
                            {
                                //Console.WriteLine(r.ReadLine());
                                count++;
                            }
                            Console.WriteLine("共读到 {0} 行数据", count);
                        }
                        Console.WriteLine(theEntry.Name);
                    }
                }
            }
        }
    }
}
