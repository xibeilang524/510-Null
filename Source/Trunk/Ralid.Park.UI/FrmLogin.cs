using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Configuration ;
using System.Windows.Forms;
using System.Data.SqlClient;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary;

namespace Ralid.Park.UI
{
    public partial class FrmLogin : Form
    {
        private bool _Adance = false;
        private SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();

        public FrmLogin()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置是否禁止编辑数据库
        /// </summary>
        public bool ForbidEditDataBase { get; set; }
        #endregion

        #region 私有方法
        private void SaveConnectString()
        {
            sb.DataSource = this.txtServer.Text;
            sb.InitialCatalog = this.txtDataBase.Text;
            sb.IntegratedSecurity = rdSystem.Checked;
            sb.UserID = this.txtUserID.Text;
            sb.Password = this.txtPasswd.Text;
            
            sb.PersistSecurityInfo = true;

            sb.ConnectTimeout = 5;

            AppSettings.CurrentSetting.MasterParkConnect = sb.ConnectionString;

            AppSettings.CurrentSetting.StandbyParkConnect = this.UCStandbyDB.ConnectString;

            AppSettings.CurrentSetting.SelectedPark = this.rdbMasterDB.Checked ? DataBaseType.Master : DataBaseType.Standby;
        }

        private bool UpGradeDataBase()
        {
            bool result = false;
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "DbUpdate.sql");
            if (System.IO.File.Exists(path))
            {
                result = DatabaseUpgrader.ExeSQLFile(AppSettings.CurrentSetting.MasterParkConnect, path);
                if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect)
                    && !DatabaseUpgrader.ExeSQLFile(AppSettings.CurrentSetting.StandbyParkConnect, path))
                {
                    result = false;
                }
            }
            return result;
        }

        private List<string> GetHistoryOperators()
        {
            List<string> items = null;
            string file = Path.Combine(Application.StartupPath, "HistoryOperators.txt");
            if (File.Exists(file))
            {
                try
                {
                    using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            items= new List<string>();
                            while (!reader.EndOfStream)
                            {
                                items.Add(reader.ReadLine());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
            return items;
        }

        private void SaveHistoryOperators()
        {
            List<string> items = new List<string>();
            if (txtLogName.AutoCompleteCustomSource != null && txtLogName.AutoCompleteCustomSource.Count > 0)
            {
                string[] history = new string[txtLogName.AutoCompleteCustomSource.Count];
                txtLogName.AutoCompleteCustomSource.CopyTo(history, 0);
                items.AddRange(history);

            }
            if (!items.Contains(txtLogName.Text)) items.Add(txtLogName.Text);
            try
            {
                string file = Path.Combine(Application.StartupPath, "HistoryOperators.txt");
                using (FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        foreach (string item in items)
                        {
                            writer.WriteLine(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }        
        #endregion

        #region 窗体事件
        private void Login_Load(object sender, EventArgs e)
        {
            this.gpDB.Visible = false;
            this.gpDB.Enabled = !ForbidEditDataBase;
            this.gpSDB.Visible = false;
            this.gpSDB.Enabled = !ForbidEditDataBase;
            this.gpDBSelect.Visible = false;
            this.Height = 148;


            if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.MasterParkConnect))
            {
                try
                {
                    sb = new SqlConnectionStringBuilder(AppSettings.CurrentSetting.MasterParkConnect);
                    txtServer.Text = sb.DataSource;
                    txtDataBase.Text = sb.InitialCatalog;
                    if (sb.IntegratedSecurity)
                    {
                        this.rdSystem.Checked = true;
                    }
                    else
                    {
                        this.rdUser.Checked = true;
                        this.txtUserID.Text = sb.UserID;
                        this.txtPasswd.Text = sb.Password;
                    }
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect))
            {
                this.UCStandbyDB.ConnectString = AppSettings.CurrentSetting.StandbyParkConnect;
            }

            if (!string.IsNullOrEmpty(CommandLineArgs.UserName))
            {
                this.txtLogName.Text = CommandLineArgs.UserName;
                this.txtPassword.Text = CommandLineArgs.Password;
                CommandLineArgs.UserName = string.Empty;
                CommandLineArgs.Password = string.Empty;
                this.btnLogin_Click(this.btnLogin, EventArgs.Empty);
                return;
            }

            this.chkRememberLogid.Checked = AppSettings.CurrentSetting.RememberLogID;
            if (AppSettings.CurrentSetting.RememberLogID)
            {
                List<string> history = GetHistoryOperators();
                if (history != null && history.Count > 0)
                {
                    this.txtLogName.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                    foreach (string item in history)
                    {
                        this.txtLogName.AutoCompleteCustomSource.Add(item);
                        this.txtLogName.Items.Add(item);
                    }
                }
            }
        }

        private void chkRememberLogid_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.CurrentSetting.RememberLogID = chkRememberLogid.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string logName = this.txtLogName.Text.Trim();
            string pwd = this.txtPassword.Text.Trim();
            string server = this.txtServer.Text.Trim();
            string database = this.txtDataBase.Text.Trim();

            if (string.IsNullOrEmpty(logName))
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_InvalidUserName);
                return;
            }

            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_InvalidPwd);
                return;
            }
            if (string.IsNullOrEmpty(server))
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_InvalidServer);
                this.txtServer.Focus();
                return;
            }
            if (string.IsNullOrEmpty(database))
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_InvalidDataBase);
                this.txtDataBase.Focus();
                return;
            }
            if (this.UCStandbyDB.Server == server
                && this.UCStandbyDB.DataBase == database)
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_MasterStandbySame);
                this.UCStandbyDB.Focus();
                return;
            }
            if (this.rdbStandbyDB.Checked
                || (!string.IsNullOrEmpty(this.UCStandbyDB.Server) || !string.IsNullOrEmpty(this.UCStandbyDB.DataBase)))
            {
                if (!this.UCStandbyDB.CheckInput())
                {
                    MessageBox.Show(Resources.Resource1.FrmLogin_StandbyInvalid);
                    this.UCStandbyDB.Focus();
                    return;
                }
            }

            SaveConnectString();
            if (AppSettings.CurrentSetting.DatabaseNeedUpgrade)
            {
                if (UpGradeDataBase()) //升级数据库
                {
                    AppSettings.CurrentSetting.DatabaseNeedUpgrade = false;
                }
            }

            OperatorBll authen = null;
            if (this.rdbMasterDB.Checked)
            {
                authen = new OperatorBll(AppSettings.CurrentSetting.MasterParkConnect);
            }
            else
            {
                authen = new OperatorBll(AppSettings.CurrentSetting.StandbyParkConnect);
            }
            if (authen.Authentication(logName, pwd))
            {
                this.DialogResult = DialogResult.OK;
                if (AppSettings.CurrentSetting.RememberLogID) SaveHistoryOperators();
                this.Close();
            }
            else
            {
                MessageBox.Show(Resources.Resource1.FrmLogin_AuthenFail);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rdSystem_CheckedChanged(object sender, EventArgs e)
        {
            this.txtUserID.Enabled = !rdSystem.Checked;
            this.txtPasswd.Enabled = !rdSystem.Checked;
        }

        private void rdUser_CheckedChanged(object sender, EventArgs e)
        {
            this.txtUserID.Enabled = rdUser.Checked;
            this.txtPasswd.Enabled = rdUser.Checked;
        }

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            _Adance = !_Adance;
            if (_Adance)
            {
                this.gpDB.Visible = _Adance;
                this.gpSDB.Visible = _Adance;
                this.gpDBSelect.Visible = _Adance;
                this.btnAdvance.Text = this.btnAdvance.Text.Replace(">>>", "<<<");
                this.Height = 409;
            }
            else
            {
                this.gpDB.Visible = false;
                this.gpSDB.Visible = false;
                this.gpDBSelect.Visible = false;
                this.btnAdvance.Text = this.btnAdvance.Text.Replace("<<<", ">>>");
                this.Height = 148;
            }
        }
        #endregion
    }
}
