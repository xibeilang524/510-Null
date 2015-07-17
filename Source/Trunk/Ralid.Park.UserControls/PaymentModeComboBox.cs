using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Ralid.Park.BusinessModel .Enum ;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.UserControls
{
    public partial class PaymentModeComboBox : System.Windows.Forms.ComboBox
    {
        public PaymentModeComboBox()
        {
            InitializeComponent();
        }

        public PaymentModeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            this.Items.Clear();
            TextValueItem<PaymentMode>[] items = new TextValueItem<PaymentMode>[]{
            new TextValueItem<PaymentMode>(PaymentMode.Cash, PaymentModeDescription .GetDescription (PaymentMode .Cash )),
            new TextValueItem <PaymentMode>(PaymentMode .Prepay ,PaymentModeDescription.GetDescription (PaymentMode .Prepay )),
            new TextValueItem<PaymentMode>(PaymentMode.YangChengTong,PaymentModeDescription .GetDescription (PaymentMode.YangChengTong )),
            new TextValueItem<PaymentMode>(PaymentMode.Pos, PaymentModeDescription .GetDescription (PaymentMode.Pos )),
            new TextValueItem <PaymentMode >(PaymentMode.ZhongShanTong ,PaymentModeDescription .GetDescription (PaymentMode.ZhongShanTong )),
            new TextValueItem <PaymentMode >(PaymentMode.WeChat ,PaymentModeDescription .GetDescription (PaymentMode.WeChat )),
            };
            this.DataSource = items;
            this.DisplayMember = "Text";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaymentMode SelectedPaymentMode
        {
            get
            {
                PaymentMode mode = PaymentMode.Cash;
                if (this.SelectedItem != null)
                {
                    TextValueItem<PaymentMode> item = SelectedItem as TextValueItem<PaymentMode>;
                    if (item != null)
                    {
                        mode = item.Value;
                    }
                }
                return mode;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    TextValueItem<PaymentMode> item = Items[i] as TextValueItem<PaymentMode>;
                    if (item != null && item.Value == value)
                    {
                        SelectedIndex = i;
                    }
                }
            }
        }
    }
}
