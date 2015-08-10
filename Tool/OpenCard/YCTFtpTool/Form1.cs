using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using Limilabs.FTP.Client;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.OpenCard.OpenCardService.YCT;

namespace Ralid.OpenCard.YCTFtpTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool CheckConnect(string conStr)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = conStr;
                    con.Open();
                    con.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += string.Format(" [{0}]", Application.ProductVersion);
            if (string.IsNullOrEmpty(AppSettings.CurrentSetting.MasterParkConnect) || !CheckConnect(AppSettings.CurrentSetting.MasterParkConnect))
            {
                FrmConnect frm = new FrmConnect();
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog() != DialogResult.OK) Environment.Exit(0);
            }

            YCTSetting yct = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).GetSetting<YCTSetting>();
            if (yct != null)
            {
                txtFTPServer.Text = yct.FTPServer;
                txtFTPPort.IntergerValue = yct.FTPPort;
                txtFTPUser.Text = yct.FTPUser;
                txtFTPPwd.Text = yct.FTPPassword;
            }
            string ftpPath = AppSettings.CurrentSetting.GetConfigContent("YCTFtpPath");
            if (string.IsNullOrEmpty(ftpPath)) ftpPath = System.IO.Path.Combine(Application.StartupPath, "羊城通FTP");
            txtFTPPath.Text = ftpPath;
            AppSettings.CurrentSetting.SaveConfig("YCTFtpPath", txtFTPPath.Text);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            YCTSetting yct = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).GetOrCreateSetting<YCTSetting>();
            yct.FTPServer = txtFTPServer.Text;
            yct.FTPPort = txtFTPPort.IntergerValue;
            yct.FTPUser = txtFTPUser.Text;
            yct.FTPPassword = txtFTPPwd.Text;
            (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).SaveSetting<YCTSetting>(yct);

            try
            {
                using (Ftp ftp = new Ftp())
                {
                    ftp.Connect(txtFTPServer.Text, txtFTPPort.IntergerValue);
                    if (ftp.Connected)
                    {
                        if (string.IsNullOrEmpty(txtFTPUser.Text))
                        {
                            ftp.LoginAnonymous();
                        }
                        else
                        {
                            ftp.Login(txtFTPUser.Text, txtFTPPwd.Text);
                        }
                        //string pwd = ftp.GetCurrentFolder();
                        //ftp.Download(file, System.IO.Path.Combine(@"f:\yct", file));
                    }
                }
                lblFtpState .Text ="连接FTP服务器成功";
            }
            catch (FtpException ex)
            {
                MessageBox.Show(ex.Message);
                lblFtpState .Text ="连接FTP服务器失败";
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dig = new FolderBrowserDialog();
            if (dig.ShowDialog() == DialogResult.OK)
            {
                txtFTPPath.Text = dig.SelectedPath;
                AppSettings.CurrentSetting.SaveConfig("YCTFtpPath", txtFTPPath.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string zip = YCTUploadFileFactory.CreateM1UploadFile();
            if (!string.IsNullOrEmpty(zip))
            {

            }
        }
    }
}
