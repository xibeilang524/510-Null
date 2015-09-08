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
using System.Net.FtpClient;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
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

        private void ExtraFile(string file)
        {
            if (Path.GetExtension(file).ToUpper() == ".ZIP")
            {
                string[] mds = ReadMD(file);
                if (mds != null && mds.Length > 0)
                {
                    InsertMsg("解析黑名单...");
                    int count = 0;
                    YCTBlacklistBll bll = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect);
                    List<YCTBlacklist> bl = bll.GetItems(null).QueryObjects;
                    Dictionary<string, YCTBlacklist> blacks = new Dictionary<string, YCTBlacklist>();
                    bl.ForEach(it => blacks.Add(it.CardID, it));
                    foreach (var md in mds)
                    {
                        string[] temp = md.Split('\t');
                        if (temp.Length >= 3)
                        {
                            string cardid = temp[1].Length == 16 ? temp[1] : temp[0]; //CPU名单逻辑卡号在第二个位置, M1卡名单逻辑卡号在第一个位置
                            if (!blacks.ContainsKey(cardid))
                            {
                                YCTBlacklist yb = new YCTBlacklist();
                                yb.CardID = cardid;
                                yb.Reason = temp[2];
                                yb.AddDateTime = DateTime.Now;
                                bll.Insert(yb);
                                count++;
                            }
                            else
                            {
                                blacks.Remove(temp[0]); //如果存在则从字典中删除,字典中最后剩余的是不在当前黑名单的信息,要将这些卡号从黑名单列表中删除掉
                            }
                        }
                    }
                    foreach (var item in blacks) //删除不在当前黑名单中的数据
                    {
                        bll.Delete(item.Value);
                    }
                    InsertMsg("完成黑名单解析");
                }
            }
        }

        private void SyncDownloadFiles(FtpClient ftp)
        {
            //原理： 将本地目录中不存在的ZIP文件从远程目录中下载回来
            if (!ftp.DirectoryExists("/READ")) return;
            ftp.SetWorkingDirectory("/READ"); //定位到文件下载目录
            InsertMsg("定位到: " + ftp.GetWorkingDirectory());
            string localFolder = FTPFolderFactory.CreateDownloadFolder();
            string[] files = Directory.GetFiles(localFolder);
            foreach (var fi in ftp.GetListing())
            {
                if (fi.Type == FtpFileSystemObjectType.File && !files.Contains(Path.Combine(localFolder, fi.Name)))
                {
                    InsertMsg("下载文件 " + fi.Name);
                    string file = Path.Combine(localFolder, fi.Name); //本地文件
                    using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                    {
                        ftp.Download(fi.Name, fs);
                    }
                    if (File.Exists(file))
                    {
                        InsertMsg("解析文件 " + fi.Name);
                        ExtraFile(file);
                    }
                }
            }
        }

        private void SyncUploadFiles(FtpClient ftp, YCTSetting yct)
        {
            if (!ftp.DirectoryExists("WRITE")) return;
            ftp.SetWorkingDirectory("/WRITE"); //定位到文件下载目录
            InsertMsg("定位到: " + ftp.GetWorkingDirectory());
            DateTime dt = DateTime.Now;
            string m1Zip = string.Format("XF{0}{1}{2}.ZIP", yct.ServiceCode.ToString().PadLeft(4, '0'), yct.ReaderCode.ToString().PadLeft(4, '0'), DateTime.Today.ToString("yyyyMMdd"));
            string cpuZip = string.Format("CX{0}{1}{2}.ZIP", yct.ServiceCode.ToString().PadLeft(4, '0'), yct.ReaderCode.ToString().PadLeft(4, '0'), DateTime.Today.ToString("yyyyMMddHH"));
            var item = ftp.GetListing();
            if (item == null && item.Length == 0 || item.Count(it => it.Name == m1Zip) == 0)
            {
                YCTPaymentRecordSearchCondition con = new YCTPaymentRecordSearchCondition() //获取所有钱包类型为M1钱包且未上传的记录
                {
                    WalletType = 1,
                    State = (int)YCTPaymentRecordState.PaidOk,
                    UnUploaded = true
                };
                List<YCTPaymentRecord> records = new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(con).QueryObjects;
                if (records != null && records.Count > 0)
                {
                    string zip = YCTUploadFileFactory.CreateM1UploadFile(dt, m1Zip, records);
                    if (!string.IsNullOrEmpty(zip))
                    {
                        InsertMsg("上传文件" + m1Zip);
                        using (FileStream fs = new FileStream(zip, FileMode.Open, FileAccess.Read))
                        {
                            ftp.Upload(m1Zip, fs);
                            new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect).BatchChangeUploadFile(records, m1Zip);
                        }
                    }
                }
            }

            if (item == null && item.Length == 0 || item.Count(it => it.Name == cpuZip) == 0)
            {
                YCTPaymentRecordSearchCondition con = new YCTPaymentRecordSearchCondition() //获取所有钱包类型为CPU钱包且未上传的记录
                {
                    WalletType = 2,
                    State = (int)YCTPaymentRecordState.PaidOk,
                    UnUploaded = true
                };
                List<YCTPaymentRecord> records = new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(con).QueryObjects;
                if (records != null && records.Count > 0)
                {
                    string zip = YCTUploadFileFactory.CreateCPUUploadFile(dt, cpuZip, records);
                    if (!string.IsNullOrEmpty(zip))
                    {
                        InsertMsg("上传文件" + cpuZip);
                        using (FileStream fs = new FileStream(zip, FileMode.Open, FileAccess.Read))
                        {
                            ftp.Upload(cpuZip, fs);
                            new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect).BatchChangeUploadFile(records, cpuZip);
                        }
                    }
                }
            }
        }

        private void SyncFile_Thread()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(1000 * 60);
                    DoSync();
                    Thread.Sleep(1000 * 60 * 5);
                }
                catch (ThreadAbortException)
                {
                }
            }
        }

        private void DoSync()
        {
            try
            {
                YCTSetting yct = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).GetSetting<YCTSetting>();
                if (yct != null && !string.IsNullOrEmpty(yct.FTPServer) && yct.FTPPort > 0)
                {
                    using (FtpClient ftp = new FtpClient())
                    {
                        ftp.Host = yct.FTPServer;
                        ftp.Port = yct.FTPPort;
                        ftp.Credentials = new System.Net.NetworkCredential(string.IsNullOrEmpty(yct.FTPUser) ? "anonymous" : yct.FTPUser, string.IsNullOrEmpty(yct.FTPPassword) ? "huihai.com" : yct.FTPPassword);
                        SyncDownloadFiles(ftp); //同步下载目录，
                        ftp.SetWorkingDirectory("/"); //退回到根目录
                        SyncUploadFiles(ftp, yct);  //同频上传目录
                        InsertMsg(" >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    }
                }
            }
            catch (Exception ex)
            {
                InsertMsg(ex.Message);
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

        private void InsertMsg(string msg)
        {
            Action action = delegate()
            {
                this.eventList.Items.Add(string.Format("【{0}】 {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
                this.eventList.SelectedIndex = this.eventList.Items.Count - 1;
                this.eventList.Refresh();
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
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

            Thread t = new Thread(new ThreadStart(SyncFile_Thread));
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
                using (FtpClient ftp = new FtpClient())
                {
                    ftp.Host = yct.FTPServer;
                    ftp.Port = yct.FTPPort;
                    ftp.Credentials = new System.Net.NetworkCredential(string.IsNullOrEmpty(yct.FTPUser) ? "anonymous" : yct.FTPUser, string.IsNullOrEmpty(yct.FTPPassword) ? "huihai.com" : yct.FTPPassword);
                    ftp.Connect();
                    InsertMsg("连接FTP服务器" + (ftp.IsConnected ? "成功" : "失败"));
                }
            }
            catch (System.Net.FtpClient.FtpException ex)
            {
                MessageBox.Show(ex.Message);
                InsertMsg("连接FTP服务器失败");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.ShowInTaskbar = false;
                _LastState = this.WindowState;
                this.Hide();
                e.Cancel = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (!this.Visible )
            {
                this.WindowState = _LastState;
                this.ShowInTaskbar = true;
                this.Visible = true;
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

        private void btnSync_Click(object sender, EventArgs e)
        {
            DoSync();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }
    }
}
