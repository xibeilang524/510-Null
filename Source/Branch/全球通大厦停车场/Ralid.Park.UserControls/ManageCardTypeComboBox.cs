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
    public partial class ManageCardTypeComboBox :CardTypeComboBox 
    {
        public ManageCardTypeComboBox()
        {
            InitializeComponent();
        }

        public ManageCardTypeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public override void Init()
        {
            this.Items.Add(string.Empty);
            this.Items.AddRange(
                new CardType[]{
                //CardType.MonitorCard,
                //CardType.ManagerCard,
                CardType.OperatorCard,
            });
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
