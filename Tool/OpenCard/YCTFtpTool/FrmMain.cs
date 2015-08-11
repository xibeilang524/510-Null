using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Windows.Forms;
using Limilabs.FTP.Client;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.OpenCard.OpenCardService.YCT;

namespace Ralid.OpenCard.YCTFtpTool
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region 私有变量
        private FormWindowState _LastState = FormWindowState.Normal;
        #endregion

        #region 私有方法
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

        private void Download_Thread()
        {
            while (true)
            {
                YCTSetting yct = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).GetSetting<YCTSetting>();
                if (yct != null && !string.IsNullOrEmpty(yct.FTPServer) && yct.FTPPort > 0)
                {
                    using (Ftp ftp = new Ftp())
                    {
                        ftp.Connect(yct.FTPServer, yct.FTPPort);
                        if (ftp.Connected)
                        {
                            if (!string.IsNullOrEmpty(yct.FTPUser) && !string.IsNullOrEmpty(yct.FTPPassword))
                            {
                                ftp.Login(yct.FTPUser, yct.FTPPassword);
                            }
                            else
                            {
                                ftp.LoginAnonymous();
                            }
                            ftp.ChangeFolder("/"); //定位到文件下载目录
                            string localFolder = FTPFolderFactory.CreateDownloadFolder();
                            string[] files = Directory.GetFiles(localFolder);
                            foreach (FtpItem fi in ftp.GetList())
                            {
                                if (fi.IsFile && !files.Contains(Path.Combine(localFolder, fi.Name)))
                                {
                                    using (var fs = new FileStream(Path.Combine(localFolder, fi.Name), FileMode.Create, FileAccess.Write))
                                    {
                                        ftp.Download(fi.Name, fs);
                                    }
                                    if (Path.GetExtension(fi.Name).ToUpper() == ".ZIP")
                                    {
                                        string[] mds = ReadMD(Path.Combine(localFolder, fi.Name));
                                        if (mds != null && mds.Length > 0)
                                        {
                                            List<YCTBlacklist> bl = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(null).QueryObjects;
                                            Dictionary<string, YCTBlacklist> blacks = new Dictionary<string, YCTBlacklist>();
                                            bl.ForEach(it => blacks.Add(it.CardID, it));
                                            foreach (var md in mds)
                                            {
                                                string[] temp = md.Split('\t');
                                                if (!blacks.ContainsKey(temp[0]))
                                                {
                                                    YCTBlacklist yb = new YCTBlacklist();
                                                    yb.CardID = temp[0];
                                                    if (temp.Length >= 3) yb.Reason = temp[2];
                                                    yb.AddDateTime = DateTime.Now;
                                                    new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).Insert(yb);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(5000);
            }
        }

        private string[] ReadMD(string file)
        {
            using (var reader = new ZipFileReader(file))
            {
                byte[] data = reader.ReadFirst("MD");
                if (data != null && data.Length > 0)
                {
                    string temp = ASCIIEncoding.ASCII.GetString(data);
                    return temp.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            return null;
        }

        #endregion

        #region 事件处理程序
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

            Thread t = new Thread(new ThreadStart(Download_Thread));
            t.IsBackground = true;
            t.Start();
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
                lblFtpState.Text = "连接FTP服务器成功";
            }
            catch (FtpException ex)
            {
                MessageBox.Show(ex.Message);
                lblFtpState.Text = "连接FTP服务器失败";
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.ShowInTaskbar = false;
                _LastState = this.WindowState;
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = _LastState;
                this.ShowInTaskbar = true;
            }
            this.Activate();
        }

        private void mnu_Exit_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Environment.Exit(0);
        }
        #endregion

        private void btnBlack_Click(object sender, EventArgs e)
        {
            FrmBlackList frm = new FrmBlackList();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btnPayrecord_Click(object sender, EventArgs e)
        {
            FrmPayment frm = new FrmPayment();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
