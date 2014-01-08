using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;

namespace ECardInterface
{
    public partial class FrmParameter : Form
    {
        public FrmParameter()
        {
            InitializeComponent();
        }

        #region 私有方法
        private void ShowSetting(Mysetting ms)
        {
            if (ms.ParkfullCheckTimezones != null && ms.ParkfullCheckTimezones.Count > 0)
            {
                if (ms.ParkfullCheckTimezones.Count >= 1)
                {
                    chk1.Checked = true;
                    dtLimitationBegin1.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[0].Beginning.Hour, ms.ParkfullCheckTimezones[0].Beginning.Minute, 0);
                    dtLimitationEnd1.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[0].Ending.Hour, ms.ParkfullCheckTimezones[0].Ending.Minute, 0);
                }
                if (ms.ParkfullCheckTimezones.Count >= 2)
                {
                    chk2.Checked = true;
                    dtLimitationBegin2.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[1].Beginning.Hour, ms.ParkfullCheckTimezones[1].Beginning.Minute, 0);
                    dtLimitationEnd2.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[1].Ending.Hour, ms.ParkfullCheckTimezones[1].Ending.Minute, 0);
                }
                if (ms.ParkfullCheckTimezones.Count >= 3)
                {
                    chk3.Checked = true;
                    dtLimitationBegin3.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[2].Beginning.Hour, ms.ParkfullCheckTimezones[2].Beginning.Minute, 0);
                    dtLimitationEnd3.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[2].Ending.Hour, ms.ParkfullCheckTimezones[2].Ending.Minute, 0);
                }
                if (ms.ParkfullCheckTimezones.Count >= 4)
                {
                    chk4.Checked = true;
                    dtLimitationBegin4.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[3].Beginning.Hour, ms.ParkfullCheckTimezones[3].Beginning.Minute, 0);
                    dtLimitationEnd4.Value = new DateTime(2011, 1, 1, ms.ParkfullCheckTimezones[3].Ending.Hour, ms.ParkfullCheckTimezones[3].Ending.Minute, 0);
                }
            }
        }

        private Mysetting GetSettingFromInput()
        {
            Mysetting ms = Mysetting.Current;
            if (Mysetting.Current == null)
            {
                ms = new Mysetting();
            }
            if (ms.ParkfullCheckTimezones == null) ms.ParkfullCheckTimezones = new List<Ralid.Park.BusinessModel.Model.TimeZone>();
            ms.ParkfullCheckTimezones.Clear();
            if (chk1.Checked)
            {
                TimeEntity te1 = new TimeEntity(dtLimitationBegin1.Value.Hour, dtLimitationBegin1.Value.Minute);
                TimeEntity te2 = new TimeEntity(dtLimitationEnd1.Value.Hour, dtLimitationEnd1.Value.Minute);
                Ralid.Park.BusinessModel.Model.TimeZone tz = new Ralid.Park.BusinessModel.Model.TimeZone(te1, te2);
                ms.ParkfullCheckTimezones.Add(tz);
            }
            if (chk2.Checked)
            {
                TimeEntity te1 = new TimeEntity(dtLimitationBegin2.Value.Hour, dtLimitationBegin2.Value.Minute);
                TimeEntity te2 = new TimeEntity(dtLimitationEnd2.Value.Hour, dtLimitationEnd2.Value.Minute);
                Ralid.Park.BusinessModel.Model.TimeZone tz = new Ralid.Park.BusinessModel.Model.TimeZone(te1, te2);
                ms.ParkfullCheckTimezones.Add(tz);
            }
            if (chk3.Checked)
            {
                TimeEntity te1 = new TimeEntity(dtLimitationBegin3.Value.Hour, dtLimitationBegin3.Value.Minute);
                TimeEntity te2 = new TimeEntity(dtLimitationEnd3.Value.Hour, dtLimitationEnd3.Value.Minute);
                Ralid.Park.BusinessModel.Model.TimeZone tz = new Ralid.Park.BusinessModel.Model.TimeZone(te1, te2);
                ms.ParkfullCheckTimezones.Add(tz);
            }
            if (chk4.Checked)
            {
                TimeEntity te1 = new TimeEntity(dtLimitationBegin4.Value.Hour, dtLimitationBegin4.Value.Minute);
                TimeEntity te2 = new TimeEntity(dtLimitationEnd4.Value.Hour, dtLimitationEnd4.Value.Minute);
                Ralid.Park.BusinessModel.Model.TimeZone tz = new Ralid.Park.BusinessModel.Model.TimeZone(te1, te2);
                ms.ParkfullCheckTimezones.Add(tz);
            }
            return ms;
        }
        #endregion

        private void FrmParameter_Load(object sender, EventArgs e)
        {
            if (Mysetting.Current != null)
            {
                ShowSetting(Mysetting.Current);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Mysetting.Current = GetSettingFromInput();
            CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).SaveSetting<Mysetting>(Mysetting.Current);
            if (ret.Result != ResultCode.Successful)
            {
                MessageBox.Show(ret.Message);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
