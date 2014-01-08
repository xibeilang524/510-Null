using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.UI;
using Ralid.Park.DAL.IDAL;

namespace ECardInterface
{
    public partial class FrmEcardRecords : Ralid.Park.UI.FrmMasterBase
    {
        public FrmEcardRecords()
        {
            InitializeComponent();
        }

        #region 重写基类方法和处理事件
        protected override FrmDetailBase GetDetailForm()
        {
            return null;
        }

        protected override List<object> GetDataSource()
        {
            List<ECardRecord> records = ProviderFactory.Create<IECardRecordProvider>(AppSettings.CurrentSetting.ParkConnect).GetAll().QueryObjects;
            List<object> source = new List<object>();
            foreach (object o in records)
            {
                source.Add(o);
            }
            return source;
        }

        protected override void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            ECardRecord info = item as ECardRecord;
            row.Tag = info;
            row.Cells["colSheetID"].Value = info.SheetID;
            row.Cells["colCardID"].Value = info.CardID;
            row.Cells["colCarplate"].Value = info.Carplate;
            row.Cells["colEnterDt"].Value = info.EnterDt;
            row.Cells["colExitDt"].Value = info.EventDt;
            row.Cells["colLimitation"].Value = info.LimitationRemain;
        }

        protected override bool DeletingItem(object item)
        {
            ECardRecord info = item as ECardRecord;
            CommandResult ret = ProviderFactory.Create<IECardRecordProvider>(AppSettings.CurrentSetting.ParkConnect).Delete(info);
            if (ret.Result != ResultCode.Successful)
            {
                MessageBox.Show(ret.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ret.Result == ResultCode.Successful;
        }

        protected override ContextMenuStrip GetContextMenuStrip()
        {
            ContextMenuStrip menu = base.GetContextMenuStrip();
            menu.Items["mnu_Add"].Visible = false;
            return menu;
        }

        #endregion
    }
}
