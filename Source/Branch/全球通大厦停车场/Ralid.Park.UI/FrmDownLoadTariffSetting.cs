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
    public partial class FrmDownLoadTariffSetting : Ralid.Park.UI.FrmDownLoadBase
    {
        public FrmDownLoadTariffSetting()
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

        protected override void InitInput()
        {
            this.Text = Resources.Resource1.FrmDownLoadTariffSetting_Text;
        }

        protected override bool DownLoadHandler()
        {
            return Sync_TarrifSetting();
        }
        #endregion

        #region 私有方法
        private bool Sync_TarrifSetting()
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
                            NotifyMessage(string.Format(Resources.Resource1.FrmDownLoadTariffSetting_Download, entrance.EntranceName));
                            ret = pad.DownloadTariffSettingToEntrance(entrance.EntranceID, TariffSetting.Current);
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
