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
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;

namespace PreferentialSystem
{
    public partial class FrmPREWorkstations : FrmMasterBase
    {
        SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.AvailableParkConnect);

        public FrmPREWorkstations()
        {
            InitializeComponent();
        }

        #region 重写基类方法及事件处理
        protected override FrmDetailBase GetDetailForm()
        {
            return new FrmPREWorkstationDetail();
        }

        protected override bool DeletingItem(object item)
        {
            PREWorkstation info = item as PREWorkstation;
            PREWorkstationSetting pws = PREWorkstationSetting.Current;
            if(pws.WorkstationDictionary.ContainsKey(info.WorkstationID))
                pws.WorkstationDictionary.Remove(info.WorkstationID);
            CommandResult result = ssb.SaveSetting<PREWorkstationSetting>(pws);
            return result.Result == ResultCode.Successful;
        }

        protected override List<object> GetDataSource()
        {
            List<object> source = new List<object>();
            foreach (KeyValuePair<Guid,PREWorkstation> o in PREWorkstationSetting.Current.WorkstationDictionary)
            {
                source.Add(o.Value);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            PREWorkstation info = item as PREWorkstation;
            row.Tag = item;
            row.Cells["colWorkstationName"].Value = info.WorkstationName;
            row.Cells["colWorkstationDesc"].Value = info.WorkstationDesc;
        }

        protected override ContextMenuStrip GetContextMenuStrip()
        {
            PRERoleInfo role = PREOperatorInfo.CurrentOperator.Role;
            ContextMenuStrip menu = base.GetContextMenuStrip();
            menu.Items["mnu_Add"].Enabled = role.Permit(PREPermission.EditWorkstations);
            menu.Items["mnu_Delete"].Enabled = role.Permit(PREPermission.EditWorkstations);
            return menu;
        }
        #endregion
    }
}
