using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;

namespace PreferentialSystem
{
    public partial class FrmPRESysOptions : Form
    {
        public FrmPRESysOptions()
        {
            InitializeComponent();
        }

        private void FrmPRESysOptions_Load(object sender, EventArgs e)
        {
            ShowWorkstations();
            this.chkAllowDefine.Checked = PRESysOptionSetting.Current.PRESysOption.IsAllowDefineHour == 1 ? true : false;
            this.txtMaxHour.Text = PRESysOptionSetting.Current.PRESysOption.MaxHour == 0 ? "3" : PRESysOptionSetting.Current.PRESysOption.MaxHour.ToString();
            this.comTicketReader.Init();
            this.comTicketReader.ComPort = AppSettings.CurrentSetting.TicketReaderCOMPort;
        }

        private void ShowWorkstations()
        {
            PREWorkstationSetting pws = PREWorkstationSetting.Current;
            List<PREWorkstation> source = new List<PREWorkstation>();
            foreach (KeyValuePair<Guid, PREWorkstation> o in PREWorkstationSetting.Current.WorkstationDictionary)
            {
                source.Add(o.Value);
            }
            
            this.cmbWorkstations.DisplayMember = "WorkstationName";
            this.cmbWorkstations.ValueMember = "WorkstationID";
            this.cmbWorkstations.DataSource = source;
            
            for (int i = 0; i < this.cmbWorkstations.Items.Count; i++)
            {
                PREWorkstation item = this.cmbWorkstations.Items[i] as PREWorkstation;
                if (item.WorkstationName == PRESysOptionSetting.Current.PRESysOption.CurrentWorkstation)
                {
                    this.cmbWorkstations.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckInput()
        {
            byte maxhour = 0;
            if (string.IsNullOrEmpty(this.txtMaxHour.Text.Trim())
                || !byte.TryParse(this.txtMaxHour.Text, out maxhour))
            {
                MessageBox.Show("请输入0~99的最大优惠时数！");
                this.txtMaxHour.Focus();
                return false;
            }
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                AppSettings.CurrentSetting.TicketReaderCOMPort = this.comTicketReader.ComPort;

                PRESysOptionSetting pss = PRESysOptionSetting.Current;

                Guid currentWorkstationID = Guid.Empty;
                string currentWorkstation = string.Empty;
                if (this.cmbWorkstations.SelectedItem != null)
                {
                    currentWorkstationID = (this.cmbWorkstations.SelectedItem as PREWorkstation).WorkstationID;
                    currentWorkstation = (this.cmbWorkstations.SelectedItem as PREWorkstation).WorkstationName;
                }
                byte isAllowDeHour = this.chkAllowDefine.Checked == true ? (byte)1 : (byte)0;
                int maxHour = 0;
                try
                {
                    maxHour = Convert.ToInt32(this.txtMaxHour.Text);
                }
                catch { }
                //当前工作站保存到本地
                AppSettings.CurrentSetting.SaveConfig("CurrentWorkstationID", currentWorkstationID != null ? currentWorkstationID.ToString() : string.Empty);
                AppSettings.CurrentSetting.SaveConfig("CurrentWorkstation", currentWorkstation);

                pss.PRESysOption = new PRESysOption { CurrentWorkstationID = currentWorkstationID, CurrentWorkstation = currentWorkstation, IsAllowDefineHour = isAllowDeHour, MaxHour = maxHour };
                SysParaSettingsBll bll = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
                CommandResult result = bll.SaveSetting<PRESysOptionSetting>(pss, "PRESysOptionSetting");
                if (result.Result == ResultCode.Successful)
                    this.Close();
                else
                    MessageBox.Show(result.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                switch (rb.Name)
                {
                    case "radioButton1":
                        this.txtMaxHour.Text = "1";
                        break;
                    case "radioButton2":
                        this.txtMaxHour.Text = "2";
                        break;
                    case "radioButton3":
                        this.txtMaxHour.Text = "3";
                        break;
                }
            }
        }
    }
}
