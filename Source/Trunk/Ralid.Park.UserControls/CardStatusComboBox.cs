using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls
{
    public partial class CardStatusComboBox :System .Windows .Forms .ComboBox 
    {
        public CardStatusComboBox()
        {
            InitializeComponent();
        }

        public CardStatusComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            var items = new[] {
                new TextValueItem<CardStatus> (),
                new TextValueItem<CardStatus>(CardStatus .Enabled ,Ralid.Park .BusinessModel .Resouce .CardStatusDescription .GetDescription(CardStatus.Enabled )),
                new TextValueItem<CardStatus>(CardStatus .Disabled,Ralid.Park .BusinessModel .Resouce .CardStatusDescription.GetDescription(CardStatus .Disabled )),
                new TextValueItem<CardStatus>(CardStatus .Loss,Ralid.Park .BusinessModel .Resouce .CardStatusDescription.GetDescription(CardStatus .Loss)),
                new TextValueItem<CardStatus>(CardStatus.Recycled,Ralid.Park .BusinessModel .Resouce .CardStatusDescription.GetDescription(CardStatus .Recycled ) )
            };
            this.DataSource = items;
            this.DisplayMember = "Text";
            this.ValueMember = "Value";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CardStatus CardStatus
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    TextValueItem<CardStatus> item = (TextValueItem<CardStatus>)this.Items[this.SelectedIndex];
                    return item.Value;
                }
                else
                {
                    return CardStatus.Disabled;
                }
            }
            set
            {
                for (int i=1 ;i<this.Items .Count ;i++)
                {
                    TextValueItem<CardStatus> item = (TextValueItem<CardStatus>)this.Items[i];
                    if (item.Value == value)
                    {
                        this.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
