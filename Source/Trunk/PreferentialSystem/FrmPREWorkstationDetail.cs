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
    public partial class FrmPREWorkstationDetail : FrmDetailBase
    {
        private SysParaSettingsBll bll = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
        public FrmPREWorkstationDetail()
        {
            InitializeComponent();
        }

        protected override void InitControls()
        {
            base.InitControls();
            if (IsAdding)
                this.Text = "新增";
            else
                this.Text = (UpdatingItem as PREWorkstation).WorkstationName;
            PRERoleInfo role = PREOperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(PREPermission.EditWorkstations);
        }

        protected override bool CheckInput()
        {
            if (txtWorkstationName.Text.Trim().Length == 0)
            {
                MessageBox.Show("工作站名称不能为空！");
                return false;
            }
            return true;
        }

        protected override object GetItemFromInput()
        {
            PREWorkstation info = null;
            if (CheckInput())
            {
                if (IsAdding)
                {
                    info = new PREWorkstation();
                    info.WorkstationID = Guid.NewGuid();
                }
                else
                {
                    info = UpdatingItem as PREWorkstation;
                }
                info.WorkstationName = txtWorkstationName.Text.Trim();
                info.WorkstationDesc = txtWorkstationDesc.Text.Trim();
            }
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            PREWorkstationSetting pws = PREWorkstationSetting.Current;
            if (pws.WorkstationDictionary == null)
                pws.WorkstationDictionary = new Dictionary<Guid,PREWorkstation>();
            PREWorkstation info = (PREWorkstation)addingItem;
            pws.WorkstationDictionary.Add(info.WorkstationID, new PREWorkstation { WorkstationID = info.WorkstationID,WorkstationName=info.WorkstationName,WorkstationDesc=info.WorkstationDesc });
            CommandResult result = bll.SaveSetting<PREWorkstationSetting>(pws, "PREWorkstationSetting");
            return result;
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            PREWorkstation info = updatingItem as PREWorkstation;
            PREWorkstationSetting pws = PREWorkstationSetting.Current;
            if (pws.WorkstationDictionary.ContainsKey(info.WorkstationID))
                pws.WorkstationDictionary.Remove(info.WorkstationID);
            pws.WorkstationDictionary.Add(info.WorkstationID, new PREWorkstation { WorkstationID = info.WorkstationID, WorkstationName = info.WorkstationName, WorkstationDesc = info.WorkstationDesc });
            CommandResult result = bll.SaveSetting<PREWorkstationSetting>(pws, "PREWorkstationSetting");
            return result;
        }

        protected override void ItemShowing()
        {
            PREWorkstation info = (PREWorkstation)UpdatingItem;
            this.txtWorkstationName.Text = info.WorkstationName;
            this.txtWorkstationDesc.Text = info.WorkstationDesc;
        }
    }
}
