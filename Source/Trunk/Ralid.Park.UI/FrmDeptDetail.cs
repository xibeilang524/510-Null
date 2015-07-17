using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UI
{
    public partial class FrmDeptDetail : FrmDetailBase
    {
        private DeptBll bll = new DeptBll(AppSettings.CurrentSetting.ParkConnect); 
        public FrmDeptDetail()
        {
            InitializeComponent();
        }

        #region 重写基类方法
        protected override bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtDeptName.Text.Trim()))
            {
                MessageBox.Show(Resources.Resource1.FrmVideosourceDetail_InvalidName);
                return false;
            }
            return true;
        }

        protected override void InitControls()
        {
            //this.funcTree.Init();
            if (IsAdding)
                this.Text = Resources.Resource1.Form_Add;
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditDept);
        }

        protected override void ItemShowing()
        {
            DeptInfo info = (DeptInfo)UpdatingItem;
            this.Text = info.DeptName;
            this.txtDeptName.Text = info.DeptName;
            this.txtDescription.Text = info.Descrption;
        }

        protected override object GetItemFromInput()
        {
            DeptInfo info;
            if (UpdatingItem == null)
            {
                info = new DeptInfo();
                info.DeptID = Guid.NewGuid();
            }
            else
            {
                info = UpdatingItem as DeptInfo;
            }
            info.DeptName = this.txtDeptName.Text.Trim();
            info.Descrption = this.txtDescription.Text.Trim();
            return info;
        }

        protected override CommandResult AddItem(object item)
        {
            DeptInfo info = item as DeptInfo;
            CommandResult reuslt = bll.Insert(info);
            if (reuslt.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                DeptBll srbll = new DeptBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                srbll.UpdateOrInsert(info);
            }
            return reuslt;
        }

        protected override CommandResult UpdateItem(object item)
        {
            DeptInfo info = item as DeptInfo;
            CommandResult reuslt = bll.Update(info);
            if (reuslt.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                DeptBll srbll = new DeptBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                srbll.UpdateOrInsert(info);
            }
            return reuslt;
        }
        #endregion
    }
}
