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
    public partial class PaymentCodeComboBox : ComboBox
    {
        public PaymentCodeComboBox()
        {
            InitializeComponent();
        }

        public PaymentCodeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            this.Items.Clear();

            TextValueItem<PaymentCode?>[] items = new TextValueItem<PaymentCode?>[]{
                new TextValueItem<PaymentCode?>(null,string.Empty),
                new TextValueItem<PaymentCode?>(PaymentCode.Computer,PaymentCodeDescription.GetDescription(PaymentCode.Computer)),
                new TextValueItem<PaymentCode?>(PaymentCode.APM,PaymentCodeDescription.GetDescription(PaymentCode.APM)),
                new TextValueItem<PaymentCode?>(PaymentCode.FunctionCard,PaymentCodeDescription.GetDescription(PaymentCode.FunctionCard)),
                new TextValueItem<PaymentCode?>(PaymentCode.POS,PaymentCodeDescription .GetDescription (PaymentCode.POS)),
                new TextValueItem<PaymentCode?>(PaymentCode.Internet,PaymentCodeDescription .GetDescription (PaymentCode.Internet)),
            };
            this.DataSource = items;
            this.DisplayMember = "Text";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 获取或设置选择的收费代码
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaymentCode? SelectedPaymentCode
        {
            get
            {
                TextValueItem<PaymentCode?> item = this.SelectedItem as TextValueItem<PaymentCode?>;
                return item == null ? null : item.Value;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    TextValueItem<PaymentCode?> item = this.Items[i] as TextValueItem<PaymentCode?>;
                    if (item.Value == value)
                    {
                        this.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
