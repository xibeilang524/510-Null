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
using System.Diagnostics;
using System.Net.FtpClient;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.OpenCard.OpenCardService.YCT;
using Ralid.GeneralLibrary.SQLHelper;

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
        private bool _Dosyncing = false;
        private string _ReadFolder = "/output";
        private string _WriteFolder = "/input";
        private TraceListener _EventListBoxTrace = null;
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

        private bool UpGradeDataBase()
        {
            bool result = false;
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "DbUpdate.sql");
            if (System.IO.File.Exists(path))
            {
                SqlClientHelper.SqlClient client = new SqlClientHelper.SqlClient(AppSettings.CurrentSetting.MasterParkConnect);
                client.Connect();
                client.ExecuteSQLFile(path);
            }
            return result;
        }

        private void ExtraFile(string file)
        {
            if (Path.GetExtension(file).ToUpper() == ".ZIP")
            {
                string[] mds = ReadMD(file);
                if (mds != null && mds.Length > 0)
                {
                    InsertMsg("解析黑名单...");
                    YCTBlacklistBll bll = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect);
                    List<YCTBlacklist> bl = bll.GetItems(null).QueryObjects;
                    Dictionary<string, YCTBlacklist> blacks = new Dictionary<string, YCTBlacklist>();
                    bl.ForEach(it => blacks.Add(it.LCN, it));
                    List<YCTBlacklist> addings = new List<YCTBlacklist>();
                    foreach (var md in mds)
                    {
                        string[] temp = md.Split('\t');
                        if (temp.Length >= 3)
                        {
                            string lcn = temp[1].Length == 16 ? temp[1] : temp[0]; //CPU名单逻辑卡号在第二个位置, M1卡名单逻辑卡号在第一个位置
                            string fcn = temp[1].Length == 16 ? temp[0] : temp[1]; //物理卡号位置与逻辑卡号刚好相反
                            if (!blacks.ContainsKey(lcn))
                            {
                                YCTBlacklist yb = new YCTBlacklist();
                                yb.LCN = lcn;
                                yb.FCN = fcn;
                                yb.Reason = temp[2];
                                yb.AddDateTime = DateTime.Now;
                                addings.Add(yb);
                            }
                        }
                    }
                    if (addings.Count > 0)
                    {
                        int count = 0;
                        List<YCTBlacklist> temp = new List<YCTBlacklist>();
                        foreach (YCTBlacklist it in addings)
                        {
                            if (temp.Count < 1000)
                            {
                                temp.Add(it);
                                count++;
                            }
                            if (temp.Count == 1000)
                            {
                                bll.BatchInsert(temp);
                                temp.Clear();
                                InsertMsg(string.Format("导入黑名单完成 {0:P}", (decimal)count / addings.Count));
                            }
                        }
                        if (temp.Count > 0)
                        {
                            bll.BatchInsert(temp);
                            temp.Clear();
                        }
                    }
                    InsertMsg("完成黑名单解析");
                }
            }
        }

        private void SyncDownloadFiles(FtpClient ftp)
        {
            //原理： 将本地目录中不存在的ZIP文件从远程目录中下载回来
            string localFolder = FTPFolderFactory.CreateDownloadFolder();
            string[] files = Directory.GetFiles(localFolder);

            InsertMsg("定位到: " + _ReadFolder);
            ftp.SetWorkingDirectory(_ReadFolder);
            var remoteFiles = ftp.GetListing(_ReadFolder, FtpListOption.NoPath);
            if (remoteFiles == null || remoteFiles.Length == 0) return;
            foreach (var fi in remoteFiles)
            {
                if (fi.Type == FtpFileSystemObjectType.File && Path.GetExtension(fi.Name).ToUpper() == ".ZIP" && !files.Contains(Path.Combine(localFolder, fi.Name)))
                {
                    InsertMsg("下载文件 " + fi.Name);
                    string file = Path.Combine(localFolder, fi.Name); //本地文件
                    string tempFile = Path.Combine(localFolder, fi.Name + ".temp.ZIP");
                    using (var fs = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                    {
                        ftp.Download(fi.Name, fs);
                    }
                    if (File.Exists(tempFile))
                    {
                        InsertMsg("解析文件 " + fi.Name);
                        ExtraFile(tempFile);
                        File.Copy(tempFile, file);
                        File.Delete(tempFile);
                    }
                }
            }
        }

        private void SyncUploadFiles(FtpClient ftp, YCTSetting yct)
        {
            DateTime dt = DateTime.Now;
            string m1Zip = string.Format("XF{0}{1}{2}.ZIP", yct.ServiceCode.ToString().PadLeft(4, '0'), yct.ReaderCode.ToString().PadLeft(4, '0'), DateTime.Today.ToString("yyyyMMdd"));
            string cpuZip = string.Format("CX{0}{1}{2}.ZIP", yct.ServiceCode.ToString().PadLeft(4, '0'), yct.ReaderCode.ToString().PadLeft(4, '0'), DateTime.Today.ToString("yyyyMMddHH"));
            
            InsertMsg("定位到: " + _WriteFolder );
            ftp.SetWorkingDirectory(_WriteFolder);
            var items = ftp.GetListing(_WriteFolder, FtpListOption.NoPath);
            if (items == null && items.Length == 0 || items.Count(it => it.Name == m1Zip) == 0)
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
                    YCTBlacklistSearchCondition ycon = new YCTBlacklistSearchCondition();
                    ycon.WalletType = 1; //M
                    ycon.OnlyCatched = true;
                    ycon.OnlyUnUploaded = true;
                    List<YCTBlacklist> blacks = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(ycon).QueryObjects;
                    string zip = YCTUploadFileFactory.CreateM1UploadFile(dt, m1Zip, records, blacks);
                    if (!string.IsNullOrEmpty(zip))
                    {
                        InsertMsg("上传文件" + m1Zip);
                        using (FileStream fs = new FileStream(zip, FileMode.Open, FileAccess.Read))
                        {
                            ftp.Upload(m1Zip, fs);
                            new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect).BatchChangeUploadFile(records, m1Zip);
                            if (blacks != null && blacks.Count > 0) new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).BatchChangeUploadFile(blacks, m1Zip);
                        }
                    }
                }
            }

            if (items == null && items.Length == 0 || items.Count(it => it.Name == cpuZip) == 0)
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
                    YCTBlacklistSearchCondition ycon = new YCTBlacklistSearchCondition();
                    ycon.WalletType = 2; //cpu
                    ycon.OnlyCatched = true;
                    ycon.OnlyUnUploaded = true;
                    List<YCTBlacklist> blacks = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(ycon).QueryObjects;
                    string zip = YCTUploadFileFactory.CreateCPUUploadFile(dt, cpuZip, records, blacks);
                    if (!string.IsNullOrEmpty(zip))
                    {
                        InsertMsg("上传文件" + cpuZip);
                        using (FileStream fs = new FileStream(zip, FileMode.Open, FileAccess.Read))
                        {
                            ftp.Upload(cpuZip, fs);
                            new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect).BatchChangeUploadFile(records, cpuZip);
                            if (blacks != null && blacks.Count > 0) new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).BatchChangeUploadFile(blacks, cpuZip);
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
                    Thread.Sleep(1000 * 10);
                    if (!_Dosyncing) DoSync();
                    Thread.Sleep(1000 * 60 * 10);
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
                _Dosyncing = true;
                InsertMsg("开始进行同步...");
                YCTSetting yct = (new SysParaSettingsBll(AppSettings.CurrentSetting.MasterParkConnect)).GetSetting<YCTSetting>();
                if (yct != null && !string.IsNullOrEmpty(yct.FTPServer) && yct.FTPPort > 0)
                {
                    using (FtpClient ftp = new FtpClient())
                    {
                        ftp.Host = yct.FTPServer;
                        ftp.Port = yct.FTPPort;
                        ftp.DataConnectionType = FtpDataConnectionType.PORT;  //数据传输设置成主动   
                        ftp.Credentials = new System.Net.NetworkCredential(string.IsNullOrEmpty(yct.FTPUser) ? "anonymous" : yct.FTPUser, string.IsNullOrEmpty(yct.FTPPassword) ? "huihai.com" : yct.FTPPassword);
                        var temp = ftp.SystemType; //获取系统类型
                        SyncUploadFiles(ftp, yct);  //同频上传目录
                        SyncDownloadFiles(ftp); //同步下载目录，
                    }
                }
            }
            catch (Exception ex)
            {
                InsertMsg(ex.Message);
            }
            finally
            {
                _Dosyncing = false;
                InsertMsg(" >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
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

        public void InsertMsg(string msg)
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
            UpGradeDataBase(); //生成需要的一些表

            string temp = AppSettings.CurrentSetting.GetConfigContent("WriteFolder");
            if (!string.IsNullOrEmpty(temp)) _WriteFolder = temp;
            temp = AppSettings.CurrentSetting.GetConfigContent("ReadFolder");
            if (!string.IsNullOrEmpty(temp)) _ReadFolder = temp;

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InsertMsg("连接FTP服务器失败:" + ex.Message);
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
            if (!this.Visible)
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
            if (!_Dosyncing)
            {
                Thread t = new Thread(new ThreadStart(DoSync));
                t.Start();
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void chkFTPTrace_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFTPTrace.Checked)
            {
                if (_EventListBoxTrace == null) _EventListBoxTrace = new EventListTraceLog(this);
                FtpTrace.AddListener(_EventListBoxTrace);
            }
            else
            {
                if (_EventListBoxTrace != null) FtpTrace.RemoveListener(_EventListBoxTrace);
            }
        }
    }
}
