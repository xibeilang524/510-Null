using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
namespace Ralid.Park.UserControls
{
    public partial class StationNameComboBox : ComboBox
    {
        #region 构造函数
        public StationNameComboBox()
        {
            InitializeComponent();
        }

        public StationNameComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region 公共方法
        public void Init()
        {
            WorkstationBll bll = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect);
            List<WorkStationInfo> items = bll.GetAllWorkstations().QueryObjects;
            this.Items.Clear();
            this.Items.Add(string.Empty);
            if (items != null && items.Count > 0)
            {
                foreach (WorkStationInfo station in items)
                {
                    this.Items.Add(station.StationName);
                }
            }
            List<APM> apms = (new APMBll(AppSettings.CurrentSetting.ParkConnect)).GetAllItems().QueryObjects;
            if (apms != null && apms.Count > 0)
            {
                foreach (APM apm in apms)
                {
                    this.Items.Add(apm.SerialNum);
                }
            }
        }
        #endregion
    }
}
