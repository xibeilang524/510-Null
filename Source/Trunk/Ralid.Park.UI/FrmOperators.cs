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
using Ralid.Park.BusinessModel.SearchCondition;

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
        protected override void InitControls()
        {
            this.comRole.Init();
            if (!this.chkRole.Checked)
            {
                this.comRole.Role = null;
                this.comRole.Enabled = false;
            }
        }

        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmOperatorDetail();
        }

        protected override List<object> GetDataSource()
        {
            OperatorSearchCondition search = new OperatorSearchCondition();
            search.OperatorID = this.txtOperaterID.Text.Trim();
            search.OperatorName = this.txtOperaterName.Text.Trim();
            RoleInfo role = this.comRole.Role;
            if (role != null)
            {
                search.RoleID = role.RoleID;
            }
            operators = bll.GetOperators(search).QueryObjects;
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
            if (info.Dept != null)
                row.Cells["colDeptName"].Value = info.Dept.DeptName;
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
            else if(DataBaseConnectionsManager.Current.StandbyConnected)
            {
                OperatorBll sobll = new OperatorBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                sobll.Delete(info);
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

        #region 事件处理
        private void chkRole_CheckedChanged(object sender, EventArgs e)
        {
            this.comRole.Enabled = this.chkRole.Checked;
            if (!this.comRole.Enabled) this.comRole.Role = null;
        }
        private void btnClosePanel_Click(object sender, EventArgs e)
        {
            this.panelLeft.Visible = false;
            this.splitter1.Visible = false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataToGridView();
        }
        #endregion


    }
}
