using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.ParkAdapter;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI
{
    public partial class FrmDownLoadHolidaySetting : Ralid.Park.UI.FrmDownLoadBase
    {
        public FrmDownLoadHolidaySetting()
        {
            InitializeComponent();
        }


        #region 重写模板方法
        protected override HardwareTree GetHardwareTree()
        {
            return this.hardwareTree1;
        }

        protected override void InitControls()
        {
            this.hardwareTree1.ShowEntrance = true;
            this.hardwareTree1.Init();
            
        }

        protected override bool DownLoadHandler()
        {
            return Sync_HolidaySetting();
        }

        protected override void InitInput()
        {
            this.Text = Resources.Resource1.FrmDownLoadHolidaySetting_Text;
        }
        #endregion

        #region 私有方法
        private bool Sync_HolidaySetting()
        {
            bool success = true;
            Dictionary<int, List<EntranceInfo>> parksIDAndEntrances = this.hardwareTree1.GetSelectedParksIDAndEntrances();
            if (parksIDAndEntrances != null)
            {
                foreach (int parkID in parksIDAndEntrances.Keys)
                {
                    foreach (EntranceInfo entrance in parksIDAndEntrances[parkID])
                    {
                        IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                        bool ret = false;
                        if (pad != null)
                        {
                            NotifyMessage(string.Format(Resources.Resource1.FrmDownLoadHolidaySetting_Download, entrance.EntranceName));
                            ret = pad.DownloadHolidaySettingToEntrance(entrance.EntranceID, HolidaySetting.Current);
                        }
                        success = ret ? success : false;
                        NotifyHardwareTreeEntrance(entrance.EntranceID, ret);
                    }
                }
            }
            return success;
        }
        #endregion
    }
}
