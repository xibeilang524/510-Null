using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls
{
    public partial class UCEntrance : UserControl
    {
        List<ParkInfo> parkList;
        List<EntranceInfo> entrances;

        public UCEntrance()
        {
            InitializeComponent();
        }

        public void Init()
        {
            ParkBll parkbll = new ParkBll(AppSettings.CurrentSetting.ParkConnect);
            parkList = parkbll.GetAllParks().QueryObjects;
            EntranceBll entranceBll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);
            entrances = entranceBll.GetAllEntraces().QueryObjects;
            this.comPark.Init();
        }

        private void comPark_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parkID=this.comPark .SelectedParkID ;
            if (parkID > 0)
            {
                if (entrances != null)
                {
                    List<EntranceInfo> ens = (from en in entrances
                                              where en.ParkID == parkID
                                              select en).ToList();
                    this.comEntrance.SetDataSource(ens);
                }
            }
            else
            {
                this.comEntrance.SetDataSource(entrances);
            }
        }
        
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<EntranceInfo> SelectedEntrances
        {
            get
            {
                List<EntranceInfo> items;
                if (comEntrance.SelectedEntranceID > 0)
                {
                    items = entrances.Where(e => e.EntranceID == this.comEntrance.SelectedEntranceID).ToList();
                }
                else
                {
                    if (comPark.SelectedParkID > 0)
                    {
                        items =entrances.Where (en=>en.ParkID ==comPark.SelectedParkID).ToList();
                    }
                    else
                    {
                        items = null;
                    }
                }
                return items;
            }
        }
    }
}
