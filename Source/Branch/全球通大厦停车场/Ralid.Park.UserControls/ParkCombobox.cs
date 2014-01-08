using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls
{
    public partial class ParkCombobox :System.Windows .Forms .ComboBox 
    {
        public ParkCombobox()
        {
            InitializeComponent();
        }

        public ParkCombobox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void SetDataSource(List<ParkInfo> items)
        {
            if (items != null)
            {
                this.DataSource = items;
                this.DisplayMember = "ParkName";
            }
        }

        public void Init()
        {
            Init(Resources.Resource1.HardwareTree_Root);
        }

        public void Init(string emptyPark)
        {
            ParkBll bll = new ParkBll(AppSettings.CurrentSetting.ParkConnect);
            this.Items.Clear();
            List<ParkInfo> parks = bll.GetAllParks().QueryObjects;

            ParkInfo info = new ParkInfo();
            info.ParkName = emptyPark; // Resources.Resource1.HardwareTree_Root;
            parks.Insert(0, info);

            SetDataSource(parks);
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedParkID
        {
            get
            {
                if (SelectedIndex > 0)
                {
                    var item = (ParkInfo)this.Items[SelectedIndex];
                    return item.ParkID;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    var item = (ParkInfo)this.Items[i];
                    if (item.ParkID == value)
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
