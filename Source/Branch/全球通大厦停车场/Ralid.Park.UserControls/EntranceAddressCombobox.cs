using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.Park.UserControls
{
    public partial class EntranceAddressComboBox :ComboBox 
    {
        public EntranceAddressComboBox()
        {
            InitializeComponent();
        }

        public EntranceAddressComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            Items.Clear();
            for (int i = 1; i < 63; i++)
            {
                this.Items.Add(i);
            }
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EntranceAddress
        {
            get
            {
                return (byte)(SelectedIndex == -1 ? 0 : SelectedIndex + 1);
            }
            set
            {
                if (value > 0 && value < 64)
                {
                    SelectedIndex = value - 1;
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }
    }
}
