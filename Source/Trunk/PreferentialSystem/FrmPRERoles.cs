using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UI;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;

namespace PreferentialSystem
{
    public partial class FrmPRERoles : FrmMasterBase
    {
        private List<PRERoleInfo> roles;

        public FrmPRERoles()
        {
            InitializeComponent();
        }

        #region 重写基类方法及事件处理
        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmPRERoleDetail();
        }
        protected override bool DeletingItem(object item)
        {
            PREOperatorBll operatorBll = new PREOperatorBll(AppSettings.CurrentSetting.ParkConnect);
            OperatorSearchCondition con = new OperatorSearchCondition();
            con.RoleID = (item as PRERoleInfo).RoleID;
            List<PREOperatorInfo> list = operatorBll.GetOperators(con).QueryObjects;
            if (list != null && list.Count > 0)
            {
                if (MessageBox.Show("此角色下有操作员，建议先删除该角色的操作员。点击确定将继续，取消则会取消本次操作", "", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return false;
            }

            PRERoleBll bll = new PRERoleBll(AppSettings.CurrentSetting.ParkConnect);
            PRERoleInfo info = (PRERoleInfo)item;
            CommandResult result = bll.Delete(info);
            if (result.Result != ResultCode.Successful)
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return result.Result == ResultCode.Successful;
        }

        protected override List<object> GetDataSource()
        {
            PRERoleBll roleBll = new PRERoleBll(AppSettings.CurrentSetting.ParkConnect); 
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
            PRERoleInfo info = item as PRERoleInfo;
            row.Tag = item;
            row.Cells["colName"].Value = info.Name;
            row.Cells["colDescr"].Value = info.Description;
        }

        protected override ContextMenuStrip GetContextMenuStrip()
        {
            PRERoleInfo role = PREOperatorInfo.CurrentOperator.Role;
            ContextMenuStrip menu = base.GetContextMenuStrip();
            //menu.Items["mnu_Add"].Enabled = role.Permit(Permission.EditRole);
            //menu.Items["mnu_Delete"].Enabled = role.Permit(Permission.EditRole);
            return menu;
        }
        #endregion
    }
}
