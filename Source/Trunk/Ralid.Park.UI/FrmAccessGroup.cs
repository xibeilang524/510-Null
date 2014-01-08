using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmAccessGroup : Form
    {
        public FrmAccessGroup()
        {
            InitializeComponent();
        }

        public event GetAccessesHandler GetAccesses;
        public event GetAccessTimeZonesHandler GetAccessTimeZones;

        public AccessTimeZone TimeZone { get; set; }

        private void FrmAccessGroup_Load(object sender, EventArgs e)
        {
            entranceTree.Init();
            this.dtStart.Value = new DateTime(2010, 1, 1, 0, 0, 0);
            this.dtEnd.Value = new DateTime(2010, 1, 1, 23, 59, 0);
            if (TimeZone != null)
            {
                ShowItem(TimeZone);
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                TimeZone  = GetItemFromInput();
                this.DialogResult = DialogResult.OK;
            }
        }

        private AccessTimeZone GetItemFromInput()
        {
            AccessTimeZone timezone = new AccessTimeZone();
            timezone.BeginTime = new TimeEntity(this.dtStart.Value.Hour, this.dtStart.Value.Minute);
            timezone.EndTime = new TimeEntity(this.dtEnd.Value.Hour, this.dtEnd.Value.Minute);
            timezone.IncludeHoliday = this.chkHoliday.Checked;
            timezone.AccessEntrances = entranceTree.SelectedEntranceIDs;
            return timezone;
        }

        private void ShowItem(AccessTimeZone timezone)
        {
            this.dtStart.Value = new DateTime(2010, 1, 1,(int) timezone.BeginTime.Hour,(int) timezone.BeginTime.Minute, 0);
            this.dtEnd.Value = new DateTime(2010, 1, 1, (int)timezone.EndTime.Hour, (int)timezone.EndTime.Minute, 0);
            this.chkHoliday.Checked = timezone.IncludeHoliday;
            entranceTree.SelectedEntranceIDs = timezone.AccessEntrances;
        }

        private string GetEntranceGroupValue(int startIndex, int endIndex,int[] _array)
        {
            string strResult="";
            for (int i = endIndex; i >= startIndex; i--)
            {
                strResult = strResult + _array[i].ToString();
            }
            return strResult;
        }

        private bool CheckInput()
        {
            if (AppSettings.CurrentSetting.EnableWriteCard && GetAccesses != null && GetAccessTimeZones != null)
            {
                //写卡模式每个通道最大支持8个权限组
                List<EntranceInfo> entrances = entranceTree.GetSelectedEntrances();
                foreach (EntranceInfo entrance in entrances)
                {
                    EntranceEventArgs args = new EntranceEventArgs { EntranceID = entrance.EntranceID };
                    //List<AccessInfo> accesses = AccessSetting.Current.GetAccesses(entrance.EntranceID);
                    List<AccessInfo> accesses = GetAccesses(this, args);
                    if (accesses.Count > 7)
                    {
                        MessageBox.Show(string.Format(Resources.Resource1.FrmAccessGroup_AccessMax, entrance.EntranceName));
                        return false;
                    }
                    List<AccessTimeZone> accessTimeZones = GetAccessTimeZones(this, args);
                    if (TimeZone != null && accessTimeZones.Any(item => item == TimeZone)) accessTimeZones.Remove(TimeZone);
                    if (accessTimeZones.Count > 3)
                    {

                        MessageBox.Show(string.Format(Resources.Resource1.FrmAccessGroup_IntervalMax, entrance.EntranceName));
                        return false;
                    }
                }
            }
            return true;
        }

        
    }
}
