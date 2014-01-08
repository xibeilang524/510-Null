using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls
{
    public partial class OperatorComboBox :ComboBox 
    {
        public OperatorComboBox()
        {
            InitializeComponent();
        }

        public OperatorComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        public void Init()
        {
            OperatorBll bll = new OperatorBll(AppSettings .CurrentSetting .ParkConnect );
            List<OperatorInfo> items = bll.GetAllOperators().QueryObjects;
            this.Items.Clear();
            this.Items.Add("");
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
            this.DisplayMember = "OperatorName";
            this.SelectedIndex = 0;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OperatorID
        {
            get
            {
                if (this.SelectedIndex <=0)
                {
                    return string.Empty;
                }
                else
                {
                    OperatorInfo info = this.Items[SelectedIndex] as OperatorInfo;
                    return info.OperatorID;
                }
            }
            set
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    OperatorInfo info = this.Items[i] as OperatorInfo;
                    if (info!=null && info.OperatorID  == value)
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OperatorName
        {
            get
            {
                if (this.SelectedIndex <= 0)
                {
                    return string.Empty;
                }
                else
                {
                    OperatorInfo info = this.Items[SelectedIndex] as OperatorInfo;
                    return info.OperatorName;
                }
            }
            set
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    OperatorInfo info = this.Items[i] as OperatorInfo;
                    if (info != null && info.OperatorName == value)
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperatorInfo SelectecOperator
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return null;
                }
                return this.Items[SelectedIndex] as OperatorInfo;
            }
        }
    }
}
