using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UserControls;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmWorkstations :FrmMasterBase 
    {
        private List<WorkStationInfo> stations;
        private WorkstationBll bll = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect); 

        public FrmWorkstations()
        {
            InitializeComponent();
        }

        #region 重写基类方法及事件处理
        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmWorkstationDetail();
        }
        protected override List<object> GetDataSource()
        {
            stations = bll.GetAllWorkstations().QueryObjects;
            List<object> source = new List<object>();
            foreach (object o in stations)
            {
                source.Add(o);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row,object item)
        {
            WorkStationInfo info = item as WorkStationInfo;
            row.Tag = info;
            row.Cells["colWorkstationID"].Value = info.StationName;
            DataGridViewCheckBoxCell c = row.Cells["colCenterCharge"] as DataGridViewCheckBoxCell;
            c.Value = info.IsCenterCharge;
            if (info.Dept != null)
                row.Cells["colDeptName"].Value = info.Dept.DeptName;
        }

        protected override bool DeletingItem(object item)
        {
            WorkStationInfo info = (WorkStationInfo)item;
            CommandResult result = bll.Delete(info);
            if (result.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                WorkstationBll swbll = new WorkstationBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                swbll.Delete(info);
            }
            return result.Result == ResultCode.Successful;
        }

        protected override ContextMenuStrip GetContextMenuStrip()
        {
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            ContextMenuStrip menu = base.GetContextMenuStrip();
            menu.Items["mnu_Add"].Enabled = role.Permit(Permission.EditWorkstation);
            menu.Items["mnu_Delete"].Enabled = role.Permit(Permission.EditWorkstation);
            return menu;
        }
        #endregion
    }
}
