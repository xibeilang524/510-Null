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
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;

namespace Ralid.Park.UI
{
    public partial class FrmAPMMaster : FrmMasterBase
    {
        #region 构造函数
        public FrmAPMMaster()
        {
            InitializeComponent();
        }
        #endregion

        #region 重写基类方法
        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmAPMDetail();
        }

        protected override List<object> GetDataSource()
        {
            List<object> data = new List<object>();
            APMBll bll = new APMBll(AppSettings.CurrentSetting.ParkConnect);
            List<APM> items = bll.GetAllItems().QueryObjects;
            foreach (APM item in items)
            {
                data.Add(item);
            }
            return data;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            APM info = item as APM;
            if (info != null)
            {
                row.Cells["colSerialNum"].Value = info.SerialNum;
                row.Cells["colIP"].Value = info.IP;
                row.Cells["colMAC"].Value = info.MAC;
                row.Cells["colStatus"].Value = Ralid.Park.BusinessModel.Resouce.APMStatusDescription.GetDescription(info.Status);
                row.Cells["colActiveDateTime"].Value = info.ActiveDateTime != null ? info.ActiveDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                row.Cells["colMemo"].Value = info.Memo;
            }
        }

        protected override bool DeletingItem(object item)
        {
            CommandResult ret = (new APMBll(AppSettings.CurrentSetting.ParkConnect)).Delete(item as APM);
            return ret.Result == ResultCode.Successful;
        }
        #endregion
    }
}
