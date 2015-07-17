using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.ParkAdapter;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI
{
    public partial class FrmDownLoadKeySetting : Ralid.Park.UI.FrmDownLoadBase
    {
        public FrmDownLoadKeySetting()
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
            this.hardwareTree1.ExpandRootOnly();
        }

        protected override void InitInput()
        {
            this.Text = Resources.Resource1.FrmDownLoadKeySetting_Text;
        }

        protected override bool DownLoadHandler()
        {
            return Sync_KeySetting();
        }
        #endregion

        #region 私有方法
        private bool Sync_KeySetting()
        {
            bool success = true;
            List<EntranceInfo> entrances = this.hardwareTree1.GetSelectedEntrances();
            foreach (EntranceInfo entrance in entrances)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                bool ret = false;
                if (pad != null)
                {
                    NotifyMessage(string.Format(Resources.Resource1.FrmDownLoadKeySetting_Download, entrance.EntranceName));
                    ret = pad.DownloadKeySettingToEntrance(entrance.EntranceID, KeySetting.Current);
                    if (!ret)
                    {
                        WaitingCommandBLL wcBll = new WaitingCommandBLL(AppSettings.CurrentSetting.CurrentMasterConnect);
                        WaitingCommandInfo wcInfo = new WaitingCommandInfo();
                        wcInfo.EntranceID = entrance.EntranceID;
                        wcInfo.Command = BusinessModel.Enum.CommandType.DownloadKeySetting;
                        wcBll.DeleteAndInsert(wcInfo);
                    }
                }
                success = ret ? success : false;
                NotifyHardwareTreeEntrance(entrance.EntranceID, ret);
            }
            return success;
        }
        #endregion
    }
}
