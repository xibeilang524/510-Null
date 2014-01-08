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
    public partial class FrmDownLoadAccessSetting : Ralid.Park.UI.FrmDownLoadBase
    {
        public FrmDownLoadAccessSetting()
        {
            InitializeComponent();
        }

        #region 私有变量
        private bool _DownLoadAll;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取需下发的权限列表
        /// </summary>
        public List<AccessInfo> Accesses { get; set; }
        #endregion

        #region 重写模板方法
        protected override HardwareTree GetHardwareTree()
        {
            return this.hardwareTree1;
        }

        protected override void InitControls()
        {
            this.hardwareTree1.ShowEntrance = true;
            this.hardwareTree1.Init();

            _DownLoadAll = Accesses == null;

            if (_DownLoadAll)
            {
                Accesses = AccessSetting.Current.Accesses;
            }
        }

        protected override bool DownLoadHandler()
        {
            return Sync_AccessSetting();
        }

        protected override void InitInput()
        {
            this.Text = Resources.Resource1.FrmDownLoadAccessSetting_Text;
        }
        #endregion

        #region 私有方法
        private bool Sync_AccessSetting()
        {
            bool success = true;
            List<EntranceInfo> entrances = this.hardwareTree1.GetSelectedEntrances();
            foreach (EntranceInfo entrance in entrances)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                bool ret = false;
                if (pad != null)
                {
                    NotifyMessage(string.Format(Resources.Resource1.FrmDownLoadAccessSetting_Download, entrance.EntranceName));
                    ret = pad.DownloadAccessSettingToEntrance(entrance.EntranceID, AccessSetting.Current);
                }
                success = ret ? success : false;
                NotifyHardwareTreeEntrance(entrance.EntranceID, ret);
            }
            return success;
        }
        #endregion

    }
}
