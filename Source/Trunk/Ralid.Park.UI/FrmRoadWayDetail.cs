using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmRoadWayDetail : Ralid.Park.UI.FrmDetailBase
    {
        public FrmRoadWayDetail()
        {
            InitializeComponent();
        }

        private RoadWayBll bll = new RoadWayBll(AppSettings.CurrentSetting.ParkConnect);

        #region 处理基类事件
        protected override void InitControls()
        {
            this.EntranceTree.Init();
            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
            }
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            btnOk.Enabled = role.Permit(Permission.EditRoadWay);
        }


        protected override void ItemShowing()
        {
            RoadWayInfo info = (RoadWayInfo)UpdatingItem;
            this.txtName.Text = info.RoadName;
            this.txtName.BackColor = Color.White;
            this.EntranceTree.SelectedEntranceIDs = info.EntranceList;
            this.Text = info.RoadName;
        }

        protected override object GetItemFromInput()
        {
            RoadWayInfo info = null;
            if (IsAdding)
            {
                info = new RoadWayInfo();
            }
            else
            {
                info = UpdatingItem as RoadWayInfo;
            }
            info.RoadName = this.txtName.Text.Trim();
            info.EntranceList = this.EntranceTree.SelectedEntranceIDs;
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            RoadWayInfo info = (RoadWayInfo)addingItem;
            return bll.Insert((RoadWayInfo)addingItem);
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            RoadWayInfo info = updatingItem as RoadWayInfo;
            return bll.Update(info);
        }

        protected override bool CheckInput()
        {
            string txt;
            txt = this.txtName.Text.Trim();
            if (txt.Length == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmRoadWayDetail_InvalidName);
                txtName.Focus();
                return false;
            }
            if (this.EntranceTree.SelectedEntranceIDs.Count == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmRoadWayDetail_InvalidEntranceIDs);
                return false;
            }
            return true;
        }
        #endregion
    }
}
