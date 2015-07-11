using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateLicenseFile();
            Environment.Exit(0);
        }

        //private void VL(Config cf)
        //{
        //    hardwareInfo lv = new hardwareInfo();
        //    StringDE stringDE = new StringDE();
        //    bool result;
        //    if (DateTime.Now < Convert.ToDateTime(cf.StartTime) || DateTime.Now > Convert.ToDateTime(cf.ExpireTime))
        //    {
        //        result = false;
        //    }
        //    else
        //    {
        //        if (cf.ComputeName.ToLower() != lv.GetHostName().ToLower())
        //        {
        //            result = false;
        //        }
        //        else
        //        {
        //            if (DateTime.Now < Convert.ToDateTime(stringDE.DecryptString(cf.StdTime)))
        //            {
        //                result = false;
        //            }
        //            else
        //            {
        //                string a = stringDE.EncryptString(string.Concat(new string[]
        //                {
        //                    lv.GetCpuID (),
        //                    "#",
        //                    lv.GetHostName(),
        //                    "#",
        //                    cf.StartTime ,
        //                    "#",
        //                    cf.ExpireTime 
        //                }));
        //                result = !(a != cf.Lic);
        //            }
        //        }
        //    }
        //}

        private void CreateLicenseFile()
        {
            hardwareInfo lv = new hardwareInfo();
            StringDE stringDE = new StringDE();
            Config cf = new Config();
            cf.ComputeName = new hardwareInfo().GetHostName();
            cf.StartTime = new DateTime(2000, 1, 1).ToString("yyyy-MM-dd HH:mm:ss");
            cf.ExpireTime = new DateTime(2099, 12, 31).ToString("yyyy-MM-dd HH:mm:ss");
            cf.StdTime = stringDE.EncryptString(new DateTime(2000, 1, 1).ToString("yyyy-MM-dd HH:mm:ss"));
            cf.Lic = stringDE.EncryptString(string.Format("{0}#{1}#{2}#{3}", lv.GetCpuID(), lv.GetHostName(), cf.StartTime, cf.ExpireTime));
            string path = Path.Combine(Application.StartupPath, cf.ComputeName + ".lic");
            try
            {
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Config));
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    ser.Serialize(fs, cf);
                    MessageBox.Show("创建license文件成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
