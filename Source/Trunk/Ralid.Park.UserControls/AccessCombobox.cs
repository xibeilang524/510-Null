using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls
{
    public partial class AccessComboBox : ComboBox
    {
        public AccessComboBox()
        {
            InitializeComponent();
        }

        public AccessComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Init()
        {
            AccessSetting acsSetting = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<AccessSetting>();
            if (acsSetting != null && acsSetting.Accesses != null && acsSetting.Accesses.Count > 0)
            {
                AccessInfo defaultAcs = new AccessInfo { ID = 0, Name = string.Empty };
                List<AccessInfo> list = new List<AccessInfo>();
                list.Insert(0, defaultAcs);
                list.AddRange(acsSetting.Accesses);
                this.DataSource = list;
            }
            this.DisplayMember = "Name";
            this.ValueMember = "ID";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte AccesslevelID
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return 0;
                }
                else
                {
                    AccessInfo info = this.SelectedItem as AccessInfo;
                    return info.ID;
                }
            }
            set
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    AccessInfo info = this.Items[i] as AccessInfo;
                    if (info.ID == value)
                    {
                        this.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
