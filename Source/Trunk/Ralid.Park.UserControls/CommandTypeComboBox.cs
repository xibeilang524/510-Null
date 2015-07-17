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
    public partial class CommandTypeComboBox : ComboBox
    {
        public CommandTypeComboBox()
        {
            InitializeComponent();
        }

        public CommandTypeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            var items = new[] {
                new TextValueItem<CommandType?> (),
                new TextValueItem<CommandType?>(CommandType.AddCard ,CommandTypeDescription.GetDescription(CommandType.AddCard)),
                new TextValueItem<CommandType?>(CommandType.UpateCard,CommandTypeDescription.GetDescription(CommandType.UpateCard)),
                new TextValueItem<CommandType?>(CommandType.DeleteCard ,CommandTypeDescription.GetDescription(CommandType.DeleteCard)),
                new TextValueItem<CommandType?>(CommandType.DownloadAccesses,CommandTypeDescription.GetDescription(CommandType.DownloadAccesses)),
                new TextValueItem<CommandType?>(CommandType.DownloadHolidays ,CommandTypeDescription.GetDescription(CommandType.DownloadHolidays)),
                new TextValueItem<CommandType?>(CommandType.DownloadKeySetting,CommandTypeDescription.GetDescription(CommandType.DownloadKeySetting)),
                new TextValueItem<CommandType?>(CommandType.DownloadTariffs ,CommandTypeDescription.GetDescription(CommandType.DownloadTariffs))
            };
            this.DataSource = items;
            this.DisplayMember = "Text";
            this.ValueMember = "Value";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CommandType? SelectedCommandType
        {
            get
            {
                if (this.SelectedIndex > 0)
                {
                    TextValueItem<CommandType?> item = (TextValueItem<CommandType?>)this.Items[this.SelectedIndex];
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
                    TextValueItem<CommandType?> item = (TextValueItem<CommandType?>)this.Items[i];
                    if (item.Value == value)
                    {
                        this.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
