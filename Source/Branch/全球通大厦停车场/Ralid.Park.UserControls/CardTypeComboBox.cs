using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms ;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls
{
    public partial class CardTypeComboBox:ComboBox 
    {
        public CardTypeComboBox()
        {
            InitializeComponent();
        }

        public CardTypeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public virtual void Init()
        {
            this.Items.Add(string.Empty);
            this.Items.AddRange(
                new CardType[]{
                CardType.OwnerCard ,
                CardType.MonthRentCard ,
                CardType.PrePayCard ,
                CardType.TempCard ,
                CardType.VipCard,
                CardType.Ticket 
            });
            if (AppSettings.CurrentSetting.EnableWriteCard)
            {
                //写卡模式屏蔽纸票选择
                this.Items.Remove(CardType.Ticket);
            }
            if (CustomCardTypeSetting.Current != null)
            {
                CardType[] customs = CustomCardTypeSetting.Current.CardTypes;
                if (customs != null) this.Items.AddRange(customs);
            }
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CardType SelectedCardType
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    CardType item = (CardType)this.Items[this.SelectedIndex];
                    return item;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    this.SelectedIndex = -1;
                    for (int i = 1; i < this.Items.Count; i++)
                    {
                        CardType item = (CardType)this.Items[i];
                        if (item == value)
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
