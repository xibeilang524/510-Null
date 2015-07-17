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
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;

namespace PreferentialSystem
{
    public partial class FrmPREBusinessDetail : FrmDetailBase
    {
        private PREBusinessesBll bll = new PREBusinessesBll(AppSettings.CurrentSetting.ParkConnect); 
        public FrmPREBusinessDetail()
        {
            InitializeComponent();
        }

        protected override void InitControls()
        {
            PRERoleInfo role = PREOperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(PREPermission.EditBusiness);
            if (IsAdding)
                this.Text = "商家信息";
        }

        protected override void ItemShowing()
        {
            PREBusinesses info = (PREBusinesses)UpdatingItem;
            this.txtBusinessName.Text = info.BusinessesName;
            this.txtBusinessDesc.Text = info.BusinessesDesc;
            this.Text = info.BusinessesName;
        }

        protected override bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtBusinessName.Text.Trim()))
            {
                MessageBox.Show("商家名称不能为空！");
                return false;
            }
            return true;
        }

        protected override object GetItemFromInput()
        {
            PREBusinesses info;
            if (UpdatingItem == null)
            {
                info = new PREBusinesses();
                info.BusinessesID = Guid.NewGuid();
            }
            else
            {
                info = UpdatingItem as PREBusinesses;
            }
            info.BusinessesName = this.txtBusinessName.Text;
            info.BusinessesDesc = this.txtBusinessDesc.Text;
            return info;
        }

        protected override CommandResult AddItem(object item)
        {
            PREBusinesses info = item as PREBusinesses;
            CommandResult reuslt = bll.Insert(info);
            return reuslt;
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            PREBusinesses info = updatingItem as PREBusinesses;
            CommandResult result = bll.Update(info);
            return result;
        }
    }
}
