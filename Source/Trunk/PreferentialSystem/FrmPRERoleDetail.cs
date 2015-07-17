using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UI;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace PreferentialSystem
{
    public partial class FrmPRERoleDetail : FrmDetailBase
    {
        public FrmPRERoleDetail()
        {
            InitializeComponent();
        }

        private PRERoleBll bll = new PRERoleBll(AppSettings.CurrentSetting.ParkConnect); 

        #region 重写基类方法
        protected override bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show("角色名称不能为空！");
                return false;
            }
            return true;
        }

        protected override void InitControls()
        {
            this.funcTree.Init();
            if (IsAdding)
            {
                this.Text = "增加";
            }
            PRERoleInfo role = PREOperatorInfo.CurrentOperator.Role;
            //this.btnOk.Enabled = role.Permit(PREPermission.EditRole);
        }

        protected override void ItemShowing()
        {
            PRERoleInfo info = (PRERoleInfo)UpdatingItem;
            this.txtName.Text = info.Name;
            this.txtName.BackColor = Color.White;
            this.txtDescription.Text = info.Description;
            this.Text = info.Name;
            this.funcTree.SelectedRights = info.Permission;
            if (!info.CanEdit) //角色不可编辑,用于系统管理员,用户不可以更改
            {
                this.funcTree.Enabled = false;
            }
        }

        protected override object GetItemFromInput()
        {
            PRERoleInfo info;
            if (UpdatingItem == null)
            {
                info = new PRERoleInfo();
                info.RoleID = this.txtName.Text.Trim();
            }
            else
            {
                info = UpdatingItem as PRERoleInfo;
            }
            info.Name = this.txtName.Text.Trim();
            info.Description = this.txtDescription.Text.Trim();
            info.Permission = this.funcTree.SelectedRights;
            return info;
        }

        protected override CommandResult AddItem(object item)
        {
            PRERoleInfo info = item as PRERoleInfo;
            CommandResult reuslt = bll.Insert(info);
            return reuslt;
        }

        protected override CommandResult UpdateItem(object item)
        {
            PRERoleInfo info = item as PRERoleInfo;
            CommandResult reuslt = bll.Update(info);
            return reuslt;
        }
        #endregion

    }
}
