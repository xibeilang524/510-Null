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
    public partial class WaitingCommandStatusComboBox : ComboBox
    {
        public WaitingCommandStatusComboBox()
        {
            InitializeComponent();
        }

        public WaitingCommandStatusComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            var items = new[] {
                new TextValueItem<WaitingCommandStatus?> (),
                new TextValueItem<WaitingCommandStatus?>(WaitingCommandStatus.Waiting ,WaitingCommandStatusDescription.GetDescription(WaitingCommandStatus.Waiting)),
                new TextValueItem<WaitingCommandStatus?>(WaitingCommandStatus.Fail,WaitingCommandStatusDescription.GetDescription(WaitingCommandStatus.Fail))
            };
            this.DataSource = items;
            this.DisplayMember = "Text";
            this.ValueMember = "Value";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WaitingCommandStatus? SelectedStatus
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    TextValueItem<WaitingCommandStatus?> item = (TextValueItem<WaitingCommandStatus?>)this.Items[this.SelectedIndex];
                    return item.Value;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                for (int i = 1; i < this.Items.Count; i++)
                {
                    TextValueItem<WaitingCommandStatus?> item = (TextValueItem<WaitingCommandStatus?>)this.Items[i];
                    if (item.Value == value)
                    {
                        this.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
