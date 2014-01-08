using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmRoleDetail :FrmDetailBase  
    {
        private RoleBll bll = new RoleBll(AppSettings.CurrentSetting.ParkConnect); 

        public FrmRoleDetail()
        {
            InitializeComponent();
        }

        #region 重写基类方法
        protected override bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show(Resources.Resource1.FrmRoleDetail_InvalidRoleID);
                return false;
            }
            return true;
        }

        protected override void InitControls()
        {
            this.funcTree.Init();
            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
            }
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditRole);
        }

        protected override void ItemShowing()
        {
            RoleInfo info = (RoleInfo)UpdatingItem;
            this.txtName.Text = info.Name;
            this.txtName.BackColor = Color.White;
            this.txtDescription.Text = info.Description;
            this.Text = info.RoleID;
            this.funcTree.SelectedRights = info.Permission;
            if (!info.CanEdit) //角色不可编辑,用于系统管理员,用户不可以更改
            {
                this.funcTree.Enabled = false;
            }
        }

        protected override object GetItemFromInput()
        {
            RoleInfo info;
            if (UpdatingItem == null)
            {
                info = new RoleInfo();
                info.RoleID = this.txtName.Text.Trim();
            }
            else
            {
                info = UpdatingItem as RoleInfo;
            }
            info.Name = this.txtName.Text.Trim();
            info.Description = this.txtDescription.Text.Trim();
            info.Permission = this.funcTree.SelectedRights;
            return info;
        }

        protected override CommandResult  AddItem(object item)
        {
            RoleInfo info = item as RoleInfo;
            CommandResult reuslt = bll.Insert(info);
            if (reuslt.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                RoleBll srbll = new RoleBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                srbll.UpdateOrInsert(info);
            }
            return reuslt;
        }

        protected override CommandResult UpdateItem(object item)
        {
            RoleInfo info = item as RoleInfo;
            CommandResult reuslt = bll.Update(info);
            if (reuslt.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                RoleBll srbll = new RoleBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                srbll.UpdateOrInsert(info);
            }
            return reuslt;
        }
        #endregion
    }
}
