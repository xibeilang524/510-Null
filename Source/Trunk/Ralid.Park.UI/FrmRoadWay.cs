using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.UserControls;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.UI
{
    public partial class FrmRoadWay : Ralid.Park.UI.FrmMasterBase
    {

        public FrmRoadWay()
        {
            InitializeComponent();
        }

        private List<RoadWayInfo> roadWays;
        private bool _ForSwitchMode;
        private RoadWayBll bll = new RoadWayBll(AppSettings.CurrentSetting.ParkConnect);

        #region 公共属性
        /// <summary>
        /// 获取或设置是否用于切换通道模式
        /// </summary>
        public bool ForSwitchMode
        {
            get { return _ForSwitchMode; }
            set
            {
                _ForSwitchMode = value;
                if (_ForSwitchMode)
                {
                    this.Text = Resources.Resource1.FrmRoadWay_Switch;
                }
                else
                {
                    this.Text = Resources.Resource1.FrmRoadWay_RoadWays;
                }
            }
        }
        #endregion

        #region 公共事件
        /// <summary>
        /// 切换通道模式时产生此事件
        /// </summary>
        public EventHandler<SwitchRoadModeArgs> SwitchModeHandler;
        #endregion

        #region 私有方法
        private DataGridViewRow GetSelectRoadWayRow()
        {
            foreach (DataGridViewRow row in this.RoadWayView.Rows)
            {
                if (this.RoadWayView.IsRowSelected(row)
                    && row.Tag != null
                    && row.Tag is RoadWayInfo)
                {
                    return row;
                }
            }
            return null;
        }

        private bool SwitchMode(RoadWayInfo info, RoadMode mode)
        {
            if (info != null)
            {
                if (info.EntranceList.Count > 0)
                {
                    if (this.SwitchModeHandler != null)
                    {
                        this.SwitchModeHandler(this, new SwitchRoadModeArgs(mode, info));
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show(Resources.Resource1.FrmRoadWay_NoEntrance);
                }
            }
            return false;
        }
        #endregion

        #region 重写基类方法及事件处理

        protected override FrmDetailBase GetDetailForm()
        {
            if (_ForSwitchMode)
            {
                return null;
            }
            else
            {
                return new FrmRoadWayDetail();
            }
        }
        protected override List<object> GetDataSource()
        {
            roadWays = bll.GetAllRoadWays().QueryObjects;
            List<object> source = new List<object>();
            foreach (RoadWayInfo o in roadWays)
            {
                if (_ForSwitchMode && AppSettings.CurrentSetting != null && AppSettings.CurrentSetting.ShowOnlyListenedPark)
                {
                    //if (!WorkStationInfo.CurrentStation.IsInListenList(o.EntranceList))
                    if(!ParkBuffer.Current.IsInSameParkList(WorkStationInfo.CurrentStation.EntranceList,o.EntranceList))
                    {
                        continue;
                    }
                }
                source.Add(o);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            RoadWayInfo info = item as RoadWayInfo;
            row.Tag = info;
            row.Cells["colRoadName"].Value = info.RoadName;
            row.Cells["colMode"].Value = info.Mode == RoadMode.None ? string.Empty : RoadModeDescription.GetDescritption(info.Mode);
            row.Cells["colEntrances"].Value = ParkBuffer.Current == null ? string.Empty : ParkBuffer.Current.GetEntrancesName(info.EntranceList);
        }

        protected override bool DeletingItem(object item)
        {
            RoadWayInfo info = (RoadWayInfo)item;
            CommandResult result = bll.Delete(info);
            return result.Result == ResultCode.Successful;
        }

        protected override ContextMenuStrip GetContextMenuStrip()
        {
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            ContextMenuStrip menu = null;
            if (!ForSwitchMode)
            {
                menu = base.GetContextMenuStrip();
                menu.Items["mnu_Add"].Enabled = role.Permit(Permission.EditRoadWay);
                menu.Items["mnu_Delete"].Enabled = role.Permit(Permission.EditRoadWay);
            }
            else
            {
                menu = this.contextMenuStrip2;
                menu.Items["mnu_Exit"].Enabled = role.Permit(Permission.SwitchRoadWay);
                menu.Items["mnu_Entrance"].Enabled = role.Permit(Permission.SwitchRoadWay); 
            }
            return menu;
        }
        private void mnu_Exit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectRoadWayRow();
            if (row != null)
            {
                if (SwitchMode(row.Tag as RoadWayInfo, RoadMode.Exit))
                {
                    ShowItemInGridViewRow(row, row.Tag);
                }
            }
        }
        private void mnu_Entrance_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = GetSelectRoadWayRow();
            if (row != null)
            {
                if (SwitchMode(row.Tag as RoadWayInfo, RoadMode.Entrance))
                {
                    ShowItemInGridViewRow(row, row.Tag);
                }
            }
        }
        #endregion


    }
}
