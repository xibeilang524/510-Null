using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BLL;

namespace Ralid.Park.UI
{
    public partial class FrmAPMDetail : FrmDetailBase
    {
        public FrmAPMDetail()
        {
            InitializeComponent();
        }

        #region 重写基类方法
        protected override bool CheckInput()
        {
            if (string.IsNullOrEmpty(txtSerialNum.Text))
            {
                MessageBox.Show(Resources.Resource1.FrmAPMDetail_InvalidSerialNum);
                txtSerialNum.Focus();
                return false;
            }
            return true;
        }

        protected override void InitControls()
        {
            
        }

        protected override void ItemShowing()
        {
            if (UpdatingItem != null)
            {
                APM info = UpdatingItem as APM;
                txtSerialNum.Text = info.SerialNum;
                this.Text = info.SerialNum;
                txtMemo.Text = info.Memo;
            }
        }

        protected override Object GetItemFromInput()
        {
            APM info;
            if (UpdatingItem != null)
            {
                info = UpdatingItem as APM;
            }
            else
            {
                info = new APM();
                info.CheckOutTime = new DateTime(2012, 1, 1);
            }
            info.SerialNum = txtSerialNum.Text.Trim();
            info.Memo = txtMemo.Text.Trim();
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            return (new APMBll(AppSettings.CurrentSetting.ParkConnect).Insert(addingItem as APM));
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            return (new APMBll(AppSettings.CurrentSetting.ParkConnect).Update(updatingItem as APM));
        }
        #endregion

        private void FrmAPMDetail_Load(object sender, EventArgs e)
        {
            btnOk.Enabled = OperatorInfo.CurrentOperator.Role.Permit(Permission.EditAPM);
            if (UpdatingItem != null)
            {
                this.Text = Resources.Resource1.Form_Add;
                this.txtSerialNum.Enabled = false ;
            }
            else
            {
                this.txtSerialNum.Enabled = true;
            }
        }
    }
}
