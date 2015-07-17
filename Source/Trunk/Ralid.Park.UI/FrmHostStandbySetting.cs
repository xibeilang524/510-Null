using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI
{
    public partial class FrmHostStandbySetting : Form
    {
        public FrmHostStandbySetting()
        {
            InitializeComponent();
        }

        #region 私有变量
        /// <summary>
        /// 当前双机热备设置
        /// </summary>
        private HostStandbySetting _Current;
        #endregion

        #region 私有方法
        private void ClearSetting()
        {
            this.ucipHost.IP = "0.0.0.0";
            this.ucipStandby.IP = "0.0.0.0";
            this.gvSMSList.Rows.Clear();
            _Current = null;
        }

        private void ShowSetting(HostStandbySetting setting)
        {
            if (setting != null)
            {
                this.ucipHost.IP = setting.HostIP;
                this.ucipStandby.IP = setting.StandbyIP;
                this.chkSendSMS.Checked = setting.SendSMS;

                ShowSMSListOnGrid(setting.SMSItems);
            }
            else
            {
                ClearSetting();
            }
        }

        private void ShowSMSListOnGrid(List<SMSItem> items)
        {
            this.gvSMSList.Rows.Clear();
            if (items != null)
            {
                foreach (SMSItem item in items)
                {
                    int row = this.gvSMSList.Rows.Add();
                    ShowSMSItemOnGrid(this.gvSMSList.Rows[row], item);
                }
            }
        }

        private void ShowSMSItemOnGrid(DataGridViewRow row, SMSItem item)
        {
            row.Tag = item;
            row.Cells["colName"].Value = item.Name;
            row.Cells["colTelephone"].Value = item.Telephone;
        }

        private HostStandbySetting GetSettingFromInput(HostStandbySetting setting)
        {
            if (setting == null) setting = new HostStandbySetting();

            setting.ParkID = this.comPark.SelectedParkID;
            setting.HostIP = this.ucipHost.IP;
            setting.StandbyIP = this.ucipStandby.IP;
            setting.SendSMS = this.chkSendSMS.Checked;

            setting.SMSItems = new List<SMSItem>();
            foreach (DataGridViewRow row in this.gvSMSList.Rows)
            {
                if (!row.IsNewRow)
                {
                    SMSItem item = new SMSItem();
                    item.Name = Convert.ToString(row.Cells["colName"].Value).Trim();
                    item.Telephone = Convert.ToString(row.Cells["colTelephone"].Value).Trim();

                    setting.SMSItems.Add(item);
                }
            }

            return setting;
        }

        private bool CheckInput()
        {
            if (this.comPark.SelectedParkID <= 0)
            {
                this.comPark.Focus();
                MessageBox.Show("请选择停车场");
                return false;
            }
            if (this.ucipHost.IP == "0.0.0.0"
                || string.IsNullOrEmpty(this.ucipHost.IP))
            {
                this.ucipHost.Focus();
                MessageBox.Show("请输入主机IP");
                return false;
            }
            if (this.ucipStandby.IP == "0.0.0.0"
                || string.IsNullOrEmpty(this.ucipStandby.IP))
            {
                this.ucipStandby.Focus();
                MessageBox.Show("请输入从机IP");
                return false;
            }
            foreach (DataGridViewRow row in this.gvSMSList.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(row.Cells["colName"].Value).Trim()))
                    {
                        this.gvSMSList.CurrentCell = row.Cells["colName"];
                        MessageBox.Show("请输发送短信名单姓名");
                        return false;
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(row.Cells["colTelephone"].Value).Trim()))
                    {
                        this.gvSMSList.CurrentCell = row.Cells["colTelephone"];
                        MessageBox.Show("请输发送短信名单电话号码");
                        return false;
                    }

                }
            }
            return true;
        }
        
        #endregion

        #region 窗体事件
        private void FrmHostStandbySetting_Load(object sender, EventArgs e)
        {
            this.comPark.Init();
        }
        private void comPark_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parkID = this.comPark.SelectedParkID;
            if (parkID > 0)
            {
                HostStandbySettingBll bll = new HostStandbySettingBll(AppSettings.CurrentSetting.ParkConnect);
                _Current = bll.Get(parkID);
            }
            else
            {
                _Current = null;
            }
            ShowSetting(_Current);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                HostStandbySettingBll bll = new HostStandbySettingBll(AppSettings.CurrentSetting.ParkConnect);
                _Current = GetSettingFromInput(_Current);
                if (bll.Save(_Current))
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                }

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion




    }
}
