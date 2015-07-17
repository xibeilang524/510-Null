using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.UserControls
{
    public partial class VehicleLEDMessageTypeCombobox : ComboBox
    {
        public VehicleLEDMessageTypeCombobox()
        {
            InitializeComponent();
        }

        public VehicleLEDMessageTypeCombobox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            var items = new[] {
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.None ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.None)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.Department ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.Department)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.OwnerName ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.OwnerName)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.CardCarPlate ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.CardCarPlate)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.RegCarPlate ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.RegCarPlate)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.CardCertificate ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.CardCertificate)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.LastCarPlate ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.LastCarPlate)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.LastDateTime ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.LastDateTime)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.LastEntrance ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.LastEntrance)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.ValidDate ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.ValidDate)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.Balance ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.Balance)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.TotalPosition ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.TotalPosition)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.Vacant ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.Vacant)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.Park ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.Park)),
                new TextValueItem<VehicleLEDMessageType>(VehicleLEDMessageType.Entrance ,VehicleLEDMessageTypeDescription.GetDescription(VehicleLEDMessageType.Entrance)),
            };
            this.DataSource = items;
            this.DisplayMember = "Text";
            this.ValueMember = "Value";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VehicleLEDMessageType SelectedVehicleLEDMessageType
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    TextValueItem<VehicleLEDMessageType> item = (TextValueItem<VehicleLEDMessageType>)this.Items[this.SelectedIndex];
                    return item.Value;
                }
                else
                {
                    return VehicleLEDMessageType.None;
                }
            }
            set
            {
                for (int i = 1; i < this.Items.Count; i++)
                {
                    TextValueItem<VehicleLEDMessageType> item = (TextValueItem<VehicleLEDMessageType>)this.Items[i];
                    if (item.Value == value)
                    {
                        this.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
