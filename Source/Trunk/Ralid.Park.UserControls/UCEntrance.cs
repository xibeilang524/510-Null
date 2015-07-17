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

        /// <summary>
        /// 获取或设置是否只显示出口通道，需在Init前调用
        /// </summary>
        public bool OnlyExit { get; set; }

        public void Init()
        {
            ParkBll parkbll = new ParkBll(AppSettings.CurrentSetting.ParkConnect);
            parkList = parkbll.GetAllParks().QueryObjects;
            EntranceBll entranceBll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);
            entrances = entranceBll.GetAllEntraces().QueryObjects;
            if (OnlyExit)
            {
                entrances = (from e in entrances
                             where e.IsExitDevice
                             select e).ToList();
            }
            this.comPark.Init();
            this.comEntrance.DropDownStyle = ComboBoxStyle.DropDownList;
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
        
        /// <summary>
        /// 获取所有选择的控制器
        /// </summary>
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

        /// <summary>
        /// 获取选择的停车场
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ParkInfo SelectedPark
        {
            get
            {
                if (this.comPark.SelectedIndex > 0)
                {
                    var item = this.comPark.Items[this.comPark.SelectedIndex] as ParkInfo;
                    return item;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取选择的控制器
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EntranceInfo SelectedEntrance
        {
            get
            {
                if (this.comEntrance.SelectedIndex > 0)
                {
                    var item = this.comEntrance.Items[this.comEntrance.SelectedIndex] as EntranceInfo;
                    return item;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取或设置选择的停车场ID
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedParkID
        {
            get
            {
                return this.comPark.SelectedParkID;
            }
            set
            {
                this.comPark.SelectedParkID = value;
            }
        }

        /// <summary>
        /// 获取或设置选择的控制器ID
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedEntranceID
        {
            get
            {
                return this.comEntrance.SelectedEntranceID;
            }
            set
            {
                this.comEntrance.SelectedEntranceID = value;
            }
        }
    }
}
