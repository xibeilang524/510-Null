using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel .Enum ;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls
{
    public partial class CarTypeComboBox : System.Windows.Forms.ComboBox
    {
        public CarTypeComboBox()
        {
            InitializeComponent();
        }

        public CarTypeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            this.Items.Add(string.Empty);
            foreach (CarType carType in CarTypeSetting.Current.CarTypes)
            {
                this.Items.Add(carType.Description);
            }
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Byte SelectedCarType
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    return (byte)(this.SelectedIndex - 1);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value + 2 <= this.Items.Count)
                {
                    this.SelectedIndex = value + 1;
                }
                else
                {
                    this.SelectedIndex = 0;
                }
            }
        }
    }
}

