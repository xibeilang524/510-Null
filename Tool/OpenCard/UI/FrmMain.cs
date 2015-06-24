using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.OpenCard.UI
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private DataBaseConnectionChecker _DBChecker = null;

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
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AppSettings.CurrentSetting.MasterParkConnect) || !CheckConnect(AppSettings.CurrentSetting.MasterParkConnect))
            {
                FrmConnect frm = new FrmConnect();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
            }
        }
        #endregion
    }
}
