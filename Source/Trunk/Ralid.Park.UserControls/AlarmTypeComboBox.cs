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
    public partial class AlarmTypeComboBox :ComboBox 
    {
        public AlarmTypeComboBox()
        {
            InitializeComponent();
        }

        public AlarmTypeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            this.Items.Clear();
            this.Items.Add(string.Empty);
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.InvalidCard));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.Opendoor));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.Closedoor));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.ModifyCardPayment));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.CancelCardPayment));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.PrinterStatus));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.APMBillValidator));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.APMCoinChanger));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.APMPrinter));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.APMCardReader));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.APMKeyboard));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.APMSystem));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.OperatorLogIn));
            this.Items.Add(AlarmTypeDescription.GetDescription(AlarmType.CarArrive));
            this.SelectedIndex = 0;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AlarmType SelectedAlarmType
        {
            get
            {
                return (AlarmType)this.SelectedIndex;
            }
            set
            {
                this.SelectedIndex = (int)value;
            }
        }
    }
}
