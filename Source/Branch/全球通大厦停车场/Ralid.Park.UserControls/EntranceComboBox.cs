using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls
{
    public partial class EntranceComboBox :ComboBox 
    {
        public EntranceComboBox()
        {
            InitializeComponent();
        }

        public EntranceComboBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public void Init()
        {
            EntranceBll bll = new EntranceBll(Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.ParkConnect);
            List<EntranceInfo> items = bll.GetAllEntraces().QueryObjects;
            SetDataSource(items);
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        public void SetDataSource(List<EntranceInfo> items)
        {
            if (items != null)
            {
                List<EntranceInfo> data = new List<EntranceInfo>();
                data.Insert(0, new EntranceInfo());
                data.AddRange(items);
                this.DataSource = data;
                this.DisplayMember = "EntranceName";
            }
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string  SelectedEntranceName
        {
            get
            {
                if (SelectedIndex > 0)
                {
                    EntranceInfo item = Items[SelectedIndex] as EntranceInfo;
                    return item.EntranceName;
                }
                else
                {
                    return string.Empty;
                }
                
            }
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedEntranceID
        {
            get
            {
                if (SelectedIndex > 0)
                {
                    EntranceInfo item = Items[SelectedIndex] as EntranceInfo;
                    return item.EntranceID;
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
                    EntranceInfo item = Items[i] as EntranceInfo;
                    if (item.EntranceID == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
