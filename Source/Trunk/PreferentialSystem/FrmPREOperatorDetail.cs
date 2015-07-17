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
using Ralid.Park.UI;
using Ralid.Park.UI.Resources;

namespace PreferentialSystem
{
    public partial class FrmPREOperatorDetail : FrmDetailBase
    {
        private PREOperatorBll bll = new PREOperatorBll(AppSettings.CurrentSetting.ParkConnect);
        private string _subPwd = new string('*', 10);

        public FrmPREOperatorDetail()
        {
            InitializeComponent();
        }

        #region 重写基类的方法
        protected override void InitControls()
        {
            comRoleList.Init();

            //操作员编号已无用，可不设置
            this.label5.Visible = false;
            this.txtOperatorNum.Visible = false;
            this.label8.Visible = false;

            if (IsAdding)
            {
                this.Text = "新增";
                this.btnChangePwd.Visible = false;
                this.txtPassword.Size = this.txtOperatorName.Size;
            }
            PRERoleInfo role = PREOperatorInfo.CurrentOperator.Role;
            //this.btnOk.Enabled = role.Permit(Permission.EditOperator);
        }

        protected override void ItemShowing()
        {
            PREOperatorInfo info = (PREOperatorInfo)UpdatingItem;
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
        }


        protected override object GetItemFromInput()
        {
            PREOperatorInfo info = null;
            if (CheckInput())
            {
                if (IsAdding)
                {
                    info = new PREOperatorInfo();
                    info.Password = txtPassword.Text.Trim();
                }
                else
                {
                    info = UpdatingItem as PREOperatorInfo;
                    if (txtPassword.Text.Trim() != _subPwd)
                    {
                        info.Password = txtPassword.Text.Trim();
                    }
                }
                info.OperatorID = txtOperatorID.Text.Trim();
                info.OperatorName = txtOperatorName.Text.Trim();
                info.Role = comRoleList.Role;
                info.RoleID = comRoleList.SelectedRoleID;
                if (this.txtOperatorNum.Visible)
                {
                    info.OperatorNum = byte.Parse(txtOperatorNum.Text);
                }
            }
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            PREOperatorInfo info = (PREOperatorInfo)addingItem;
            CommandResult result = bll.Insert(info);
            if (result.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                PREOperatorBll sobll = new PREOperatorBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
               sobll.UpdateOrInsert(info);
            }
            return result;
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            PREOperatorInfo info = updatingItem as PREOperatorInfo;
            CommandResult result = bll.Update(info);
            if (result.Result == ResultCode.Successful && DataBaseConnectionsManager.Current.StandbyConnected)
            {
                PREOperatorBll sobll = new PREOperatorBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                sobll.UpdateOrInsert(info);
            }
            return result;
        }

        protected override bool CheckInput()
        {
            if (txtOperatorID.Text.Trim().Length == 0)
            {
                //MessageBox.Show(Resource1.FrmOperatorDetail_InvalidUserID);
                return false;
            }

            if (txtOperatorName.Text.Trim().Length == 0)
            {
                //MessageBox.Show(Resource1.FrmOperatorDetail_InvalidUserName);
                return false;
            }

            if (txtPassword.Text.Trim().Length == 0)
            {
                //MessageBox.Show(Resource1.FrmOperatorDetail_InvalidPwd);
                return false;
            }

            if (this.txtOperatorNum.Visible)
            {
                if (txtOperatorNum.IntergerValue > 255 || txtOperatorNum.IntergerValue < 1)
                {
                    //MessageBox.Show(Resource1.FrmOperatorDetail_InvalidOperatorNum);
                    return false;
                }
            }
            return true;
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            //FrmChangePwd frm = new FrmChangePwd();
            //frm.Operator = UpdatingItem as PREOperatorInfo;
            //frm.ShowDialog();
        }
        #endregion

        private void btnChangePwd_Click_1(object sender, EventArgs e)
        {
            FrmPREChangePwd frm = new FrmPREChangePwd();
            frm.Operator = UpdatingItem as PREOperatorInfo;
            frm.ShowDialog();
        }
    }
}
