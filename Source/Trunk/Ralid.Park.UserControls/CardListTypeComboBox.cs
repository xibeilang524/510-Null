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
    public partial class CardListTypeComboBox : ComboBox
    {
        public CardListTypeComboBox()
        {
            InitializeComponent();
        }

        public CardListTypeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            var items = new[] {
                new TextValueItem<CardListType> (),
                new TextValueItem<CardListType>(CardListType.Card ,Ralid.Park.BusinessModel.Resouce.CardListTypeDescription.GetDescription(CardListType.Card)),
                new TextValueItem<CardListType>(CardListType.CarPlate,Ralid.Park.BusinessModel.Resouce.CardListTypeDescription.GetDescription(CardListType.CarPlate))
            };
            this.DataSource = items;
            this.DisplayMember = "Text";
            this.ValueMember = "Value";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CardListType ListType
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    TextValueItem<CardListType> item = (TextValueItem<CardListType>)this.Items[this.SelectedIndex];
                    return item.Value;
                }
                else
                {
                    return CardListType.Card;
                }
            }
            set
            {
                for (int i = 1; i < this.Items.Count; i++)
                {
                    TextValueItem<CardListType> item = (TextValueItem<CardListType>)this.Items[i];
                    if (item.Value == value)
                    {
                        this.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
