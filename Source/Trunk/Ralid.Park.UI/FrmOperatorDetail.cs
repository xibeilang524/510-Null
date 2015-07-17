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
    public partial class FrmOperatorDetail:FrmDetailBase 
    {
        private OperatorBll bll = new OperatorBll(AppSettings.CurrentSetting.ParkConnect); 
        private string _subPwd = new string('*', 10);

        public FrmOperatorDetail()
        {
            InitializeComponent();
        }

        #region 重写基类的方法
        protected override void InitControls()
        {
            comRoleList.Init();
            comDeptList.Init();

            //操作员编号已无用，可不设置
            this.label5.Visible = false;
            this.txtOperatorNum.Visible = false;
            this.label8.Visible = false;

            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
                this.btnChangePwd.Visible = false;
                this.txtPassword.Size = this.txtOperatorName.Size;
            }
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditOperator);
        }

        protected override void ItemShowing()
        {
            OperatorInfo info = (OperatorInfo)UpdatingItem;
            this.txtOperatorID.Text = info.OperatorID;
            this.txtOperatorID.Enabled = false;
            this.txtOperatorID.BackColor = Color.White;
            this.txtOperatorName.Text = info.OperatorName;
            this.txtPassword.Text = _subPwd;
            this.txtPassword.Enabled = false;
            this.txtPassword.BackColor = Color.White;
            this.comRoleList.SelectedRoleID = info.RoleID;
            this.txtOperatorNum.Text = info.OperatorNum.ToString();
            this.Text = info.OperatorID;
            this.comRoleList.Enabled = info.CanEdit;
            if (info.DeptID != null)
                this.comDeptList.SelectedDeptID = info.DeptID;
        }


        protected override object GetItemFromInput()
        {
            OperatorInfo info = null;
            if (CheckInput())
            {
                if (IsAdding)
                {
                    info = new OperatorInfo();
                    info.Password = txtPassword.Text.Trim();
                }
                else
                {
                    info = UpdatingItem as OperatorInfo;
                    if (txtPassword.Text.Trim() != _subPwd)
                    {
                        info.Password = txtPassword.Text.Trim();
                    }
                }
                info.OperatorID = txtOperatorID.Text.Trim();
                info.OperatorName = txtOperatorName.Text.Trim();
                info.Role = comRoleList.Role;
                info.RoleID = comRoleList.SelectedRoleID;
                if (comDeptList.Dept != null)
                {
                    info.Dept = comDeptList.Dept;
                    info.DeptID = comDeptList.SelectedDeptID;
                }
                else
                {
                    info.Dept = null;
                    info.DeptID = null;
                }
                if (this.txtOperatorNum.Visible)
                {
                    info.OperatorNum = byte.Parse(txtOperatorNum.Text);
                }
            }
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            OperatorInfo info = (OperatorInfo)addingItem;
            CommandResult result = bll.Insert(info);
            if (result.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                OperatorBll sobll = new OperatorBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                sobll.UpdateOrInsert(info);
            }
            return result;
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            OperatorInfo info = updatingItem as OperatorInfo;
            CommandResult result = bll.Update(info);
            if (result.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                OperatorBll sobll = new OperatorBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                sobll.UpdateOrInsert(info);
            }
            return result;
        }

        protected override bool CheckInput()
        {
            if (txtOperatorID.Text.Trim().Length == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmOperatorDetail_InvalidUserID);
                return false;
            }

            if (txtOperatorName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmOperatorDetail_InvalidUserName);
                return false;
            }

            if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmOperatorDetail_InvalidPwd);
                return false;
            }

            if (this.txtOperatorNum.Visible)
            {
                if (txtOperatorNum.IntergerValue > 255 || txtOperatorNum.IntergerValue < 1)
                {
                    MessageBox.Show(Resources.Resource1.FrmOperatorDetail_InvalidOperatorNum);
                    return false;
                }
            }
            return true;
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            FrmChangePwd frm = new FrmChangePwd();
            frm.Operator = UpdatingItem as OperatorInfo;
            frm.ShowDialog();
        }
        #endregion
    }
}
