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
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Enum;

namespace PreferentialSystem
{
    public partial class FrmPREBusinesses : FrmMasterBase
    {
        private List<PREBusinesses> businesses;
        public FrmPREBusinesses()
        {
            InitializeComponent();
        }

        #region 重写基类方法及事件处理
        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmPREBusinessDetail();
        }
        protected override bool DeletingItem(object item)
        {
            PREBusinessesBll bll = new PREBusinessesBll(AppSettings.CurrentSetting.ParkConnect);
            PREBusinesses info = (PREBusinesses)item;
            CommandResult result = bll.Delete(info);
            if (result.Result != ResultCode.Successful)
            {
                MessageBox.Show(result.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (DataBaseConnectionsManager.Current.StandbyConnected)
            {
                PREBusinessesBll srbll = new PREBusinessesBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                srbll.Delete(info);
            }
            return result.Result == ResultCode.Successful;
        }

        protected override List<object> GetDataSource()
        {
            PreferentialReportSearchCondition con = new PreferentialReportSearchCondition();
            con.BusinessName = this.txtBusinessesName.Text.Trim();
            PREBusinessesBll busBll = new PREBusinessesBll(AppSettings.CurrentSetting.ParkConnect);
            businesses = busBll.GetBusinesses(con).QueryObjects.ToList();
            List<object> source = new List<object>();
            foreach (object o in businesses)
            {
                source.Add(o);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            PREBusinesses info = item as PREBusinesses;
            row.Tag = item;
            row.Cells["colBusinessesName"].Value = info.BusinessesName;
            row.Cells["colBusinessesDesc"].Value = info.BusinessesDesc;
        }

        protected override ContextMenuStrip GetContextMenuStrip()
        {
            PRERoleInfo role = PREOperatorInfo.CurrentOperator.Role;
            ContextMenuStrip menu = base.GetContextMenuStrip();
            menu.Items["mnu_Add"].Enabled = role.Permit(PREPermission.EditBusiness);
            menu.Items["mnu_Delete"].Enabled = role.Permit(PREPermission.EditBusiness);
            return menu;
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataToGridView();
        }

    }
}
