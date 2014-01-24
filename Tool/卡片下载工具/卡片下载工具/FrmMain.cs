using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.SoftDog;

namespace Ralid.Park.DownloadCard
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region 私有变量
        private List<Form> _openedForms = new List<Form>();
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
            catch
            {
                return false;
            }
        }

        private void InitSystemParameters()
        {
            //初始化系统设置
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.SQLConnect);
            UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            HolidaySetting.Current = ssb.GetOrCreateSetting<HolidaySetting>();
            AccessSetting.Current = ssb.GetOrCreateSetting<AccessSetting>();
            TariffSetting.Current = ssb.GetOrCreateSetting<TariffSetting>();
            CarTypeSetting.Current = ssb.GetOrCreateSetting<CarTypeSetting>();
            CustomCardTypeSetting.Current = ssb.GetOrCreateSetting<CustomCardTypeSetting>();
            BaseCardTypeSetting.Current = ssb.GetOrCreateSetting<BaseCardTypeSetting>();
            KeySetting.Current = ssb.GetOrCreateSetting<KeySetting>();
        }
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmConnect frm = new FrmConnect();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.SQLConnect))
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(AppSettings.CurrentSetting.SQLConnect);
                lblDBSetting.Text = string.Format("数据库服务器：[{0}]   数据库名：[{1}]   连接{2}",
                                                   !string.IsNullOrEmpty(sb.DataSource) ? sb.DataSource : "无",
                                                   !string.IsNullOrEmpty(sb.InitialCatalog) ? sb.InitialCatalog : "无",
                                                    CheckConnect(AppSettings.CurrentSetting.SQLConnect) ? "成功" : "失败");
                if (CheckConnect(AppSettings.CurrentSetting.SQLConnect))
                {
                    InitSystemParameters();
                    ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.SQLConnect);
                    ParkBuffer.Current.InValid();
                }
            }
            else
            {
                lblDBSetting.Text = "没有设置数据库连接";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出系统?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void mnu_Connect_Click(object sender, EventArgs e)
        {
            FrmConnect frm = new FrmConnect();
            frm.IsForSetting = true;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.SQLConnect))
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(AppSettings.CurrentSetting.SQLConnect);
                lblDBSetting.Text = string.Format("数据库服务器：[{0}]   数据库名：[{1}]   连接{2}",
                                                   !string.IsNullOrEmpty(sb.DataSource) ? sb.DataSource : "无",
                                                   !string.IsNullOrEmpty(sb.InitialCatalog) ? sb.InitialCatalog : "无",
                                                    CheckConnect(AppSettings.CurrentSetting.SQLConnect) ? "成功" : "失败");
            }
            else
            {
                lblDBSetting.Text = "没有设置数据库连接";
            }
        }
        #endregion

        private void mnu_DownloadCards_Click(object sender, EventArgs e)
        {
            FrmDownloadCard frm = new FrmDownloadCard();
            frm.ShowDialog();
        }
    }
}
