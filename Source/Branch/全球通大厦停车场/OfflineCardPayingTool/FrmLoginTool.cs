using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;

namespace OfflineCardPayingTool
{
    public partial class FrmLoginTool : Form
    {
        public FrmLoginTool()
        {
            InitializeComponent();
        }

        #region 私有方法
        /// <summary>
        /// 验证停车场用户
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private bool Authentication(string logName, string pwd)
        {
            OperatorInfo info=LDB_ParkingDataBuffer.Current.Operators.FirstOrDefault(opt => opt.OperatorID == logName && opt.Password == pwd);
            if (info != null)
            {
                OperatorInfo.CurrentOperator = info;
                return true;
            }
            else
            {
                return false;
            }
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
                            items = new List<string>();
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
        private void FrmLoginTool_Load(object sender, EventArgs e)
        {
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string logName = this.txtLogName.Text.Trim();
            string pwd = this.txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(logName))
            {
                MessageBox.Show(Resources.Resource1.FrmLoginTool_InvalidUserName);
                return;
            }

            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show(Resources.Resource1.FrmLoginTool_InvalidPwd);
                return;
            }            

            if (Authentication(logName, pwd))
            {
                this.DialogResult = DialogResult.OK;
                if (AppSettings.CurrentSetting.RememberLogID) SaveHistoryOperators();
                this.Close();
            }
            else
            {
                MessageBox.Show(Resources.Resource1.FrmLoginTool_AuthenFail);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void chkRememberLogid_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.CurrentSetting.RememberLogID = chkRememberLogid.Checked;
        }
        #endregion

    }
}
