using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.UI
{
    public partial class FrmDepts : FrmMasterBase
    {
        private List<DeptInfo> depts;

        public FrmDepts()
        {
            InitializeComponent();
        }

        protected override List<object> GetDataSource()
        {
            DeptBll deptBll = new DeptBll(AppSettings.CurrentSetting.ParkConnect);
            depts = deptBll.GetAllDepts().QueryObjects.ToList();
            List<object> source = new List<object>();
            foreach (object o in depts)
            {
                source.Add(o);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            DeptInfo info = item as DeptInfo;
            row.Tag = item;
            row.Cells["colName"].Value = info.DeptName;
            row.Cells["colDescr"].Value = info.Descrption;
        }

        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmDeptDetail();
        }

        protected override bool DeletingItem(object item)
        {
            DeptBll bll = new DeptBll(AppSettings.CurrentSetting.ParkConnect);
            DeptInfo info = (DeptInfo)item;
            CommandResult result = bll.Delete(info);
            if (result.Result != ResultCode.Successful)
            {
                MessageBox.Show(result.Message, Resources.Resource1.Form_Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (DataBaseConnectionsManager.Current.StandbyConnected)
            {
                DeptBll srbll = new DeptBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                srbll.Delete(info);
            }
            return result.Result == ResultCode.Successful;
        }

    }
}
