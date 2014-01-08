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
    public partial class FrmOperators:FrmMasterBase
    {
        private List<OperatorInfo> operators;
        private OperatorBll bll = new OperatorBll(AppSettings.CurrentSetting.ParkConnect); 

        public FrmOperators()
        {
            InitializeComponent();
        }

        #region 重写基类方法和处理事件
        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmOperatorDetail();
        }

        protected override List<object> GetDataSource()
        {
            operators = bll.GetAllOperators().QueryObjects.ToList();
            List<object> source = new List<object>();
            foreach (object o in operators)
            {
                source.Add(o);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            OperatorInfo info = item as OperatorInfo;
            row.Tag = info;
            row.Cells["colOperatorID"].Value = info.OperatorID;
            row.Cells["colOperatorName"].Value = info.OperatorName;
            row.Cells["colRoleID"].Value = info.Role.Name;
            row.Cells["colOperatorNum"].Value = info.OperatorNum;
        }

        protected override bool DeletingItem(object item)
        {
            OperatorInfo info = (OperatorInfo)item;
            CommandResult ret = bll.Delete(info);
            if (ret.Result != ResultCode.Successful)
            {
                MessageBox.Show(ret.Message, Resources.Resource1.Form_Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret.Result == ResultCode.Successful;
        }
        protected override ContextMenuStrip GetContextMenuStrip()
        {
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            ContextMenuStrip menu = base.GetContextMenuStrip();
            menu.Items["mnu_Add"].Enabled = role.Permit(Permission.EditOperator);
            menu.Items["mnu_Delete"].Enabled = role.Permit(Permission.EditOperator);
            return menu;
        }

        #endregion
    }
}
