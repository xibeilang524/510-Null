using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UI;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;

namespace PreferentialSystem
{
    public partial class FrmPREOperators : FrmMasterBase
    {
        private List<PREOperatorInfo> operators;
        private PREOperatorBll bll = new PREOperatorBll(AppSettings.CurrentSetting.ParkConnect); 

        public FrmPREOperators()
        {
            InitializeComponent();
        }

        protected override void InitControls()
        {
            this.comRole.Init();
            if (!this.chkRole.Checked)
            {
                this.comRole.Role = null;
                this.comRole.Enabled = false;
            }
        }

        protected override List<object> GetDataSource()
        {
            OperatorSearchCondition search = new OperatorSearchCondition();
            search.OperatorID = this.txtOperaterID.Text.Trim();
            search.OperatorName = this.txtOperaterName.Text.Trim();
            PRERoleInfo role = this.comRole.Role;
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
            PREOperatorInfo info = item as PREOperatorInfo;
            row.Tag = info;
            row.Cells["colOperatorID"].Value = info.OperatorID;
            row.Cells["colOperatorName"].Value = info.OperatorName;
            if(info.Role != null)
                row.Cells["colRoleID"].Value = info.Role.Name;
            row.Cells["colOperatorNum"].Value = info.OperatorNum;
        }

        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmPREOperatorDetail();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataToGridView();
        }

        private void chkRole_CheckedChanged(object sender, EventArgs e)
        {
            this.comRole.Enabled = this.chkRole.Checked;
            if (!this.comRole.Enabled) this.comRole.Role = null;
        }

        protected override bool DeletingItem(object item)
        {
            PREOperatorInfo info = (PREOperatorInfo)item;
            if (info.OperatorID == "admin")
            {
                MessageBox.Show("不能删除管理员！");
                return false;
            }
            CommandResult ret = bll.Delete(info);
            if (ret.Result != ResultCode.Successful)
            {
                MessageBox.Show(ret.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret.Result == ResultCode.Successful;
        }
    }
}
