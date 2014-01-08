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
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmWorkstationDetail : FrmDetailBase
    {
        private WorkstationBll bll = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect);

        public FrmWorkstationDetail()
        {
            InitializeComponent();
        }

        #region 处理基类事件
        protected override void InitControls()
        {
            this.EntranceTree.Init();
            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
            }
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            btnOk.Enabled = role.Permit(Permission.EditWorkstation);
        }


        protected override void ItemShowing()
        {
            WorkStationInfo info = (WorkStationInfo)UpdatingItem;
            this.txtName.Text = info.StationName;
            this.txtName.BackColor = Color.White;
            this.chkCenterCharge.Checked = info.IsCenterCharge;
            this.EntranceTree.SelectedEntranceIDs = info.EntranceList;
            this.Text = info.StationName;
        }

        protected override object GetItemFromInput()
        {
            WorkStationInfo info = null;
            if (IsAdding)
            {
                info = new WorkStationInfo();
                info.StationID = txtName.Text.Trim();
            }
            else
            {
                info = UpdatingItem as WorkStationInfo;
            }
            info.StationName = this.txtName.Text.Trim();
            info.IsCenterCharge = chkCenterCharge.Checked;
            info.EntranceList = this.EntranceTree.SelectedEntranceIDs;
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            return bll.Insert((WorkStationInfo)addingItem);
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            WorkStationInfo info = updatingItem as WorkStationInfo;
            CommandResult result = bll.Update(info);
            if (result.Result == ResultCode.Successful && WorkStationInfo.CurrentStation.StationID == info.StationID)
            {
                WorkStationInfo.CurrentStation = info;
            }
            return result;
        }

        protected override bool CheckInput()
        {
            string txt;
            txt = this.txtName.Text.Trim();
            if (txt.Length == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmWorkstationDetail_InvalidID);
                txtName.Focus();
                return false;
            }
            else
            {
                List<WorkStationInfo> stations = (new WorkstationBll(AppSettings.CurrentSetting.ParkConnect)).GetAllWorkstations().QueryObjects;
                if (UpdatingItem == null && stations.Exists(s => s.StationName == txt))
                {
                    MessageBox.Show(Resources.Resource1.FrmWorkstationDetail_SameName, Resources.Resource1.Form_Alert);
                    txtName.Focus();
                    return false;
                }
                else if (UpdatingItem != null && stations.Exists(s => s.StationID != (UpdatingItem as WorkStationInfo).StationID && s.StationName == txt))
                {
                    MessageBox.Show(Resources.Resource1.FrmWorkstationDetail_SameName, Resources.Resource1.Form_Alert);
                    txtName.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
