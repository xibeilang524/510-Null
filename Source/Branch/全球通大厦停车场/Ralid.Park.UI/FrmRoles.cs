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
    public partial class FrmRoles :FrmMasterBase 
    {
        private List<RoleInfo> roles;

        public FrmRoles()
        {
            InitializeComponent();
        }

        #region 重写基类方法及事件处理
        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmRoleDetail();
        }
        protected override bool DeletingItem(object item)
        {
            RoleBll bll = new RoleBll(AppSettings.CurrentSetting.ParkConnect);
            RoleInfo info = (RoleInfo)item;
            CommandResult result = bll.Delete(info);
            if (result.Result != ResultCode.Successful)
            {
                MessageBox.Show(result.Message, Resources.Resource1.Form_Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return result.Result == ResultCode.Successful;
        }

        protected override List<object> GetDataSource()
        {
            RoleBll roleBll = new RoleBll(AppSettings.CurrentSetting.ParkConnect); 
            roles = roleBll.GetAllRoles().QueryObjects.ToList();
            List<object> source = new List<object>();
            foreach (object o in roles)
            {
                source.Add(o);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            RoleInfo info = item as RoleInfo;
            row.Tag = item;
            row.Cells["colName"].Value = info.Name;
            row.Cells["colDescr"].Value = info.Description;
        }

        protected override ContextMenuStrip GetContextMenuStrip()
        {
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            ContextMenuStrip menu = base.GetContextMenuStrip();
            menu.Items["mnu_Add"].Enabled = role.Permit(Permission.EditRole);
            menu.Items["mnu_Delete"].Enabled = role.Permit(Permission.EditRole);
            return menu;
        }
        #endregion
    }
}
