using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls
{
    public partial class StationIDCombobox : ComboBox 
    {
        public StationIDCombobox()
        {
            InitializeComponent();
        }

        public StationIDCombobox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            WorkstationBll bll = new WorkstationBll(Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.ParkConnect);
            List<WorkStationInfo> items = bll.GetAllWorkstations().QueryObjects;
            this.Items.Clear();
            items.Insert(0, new WorkStationInfo());
            this.DataSource = items;
            this.DisplayMember = "StationName";
            this.ValueMember = "StationID";
            this.SelectedIndex = 0;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StationID
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    return this.SelectedValue.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.SelectedIndex = -1;
                if (value != null)
                {
                    for(int i=0;i<this.Items .Count ;i++)
                    {
                        if ((this.Items[i] as WorkStationInfo).StationID == value)
                        {
                            this.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }
    }
}
