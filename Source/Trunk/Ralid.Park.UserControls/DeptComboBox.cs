using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls
{
    public partial class DeptComboBox : ComboBox
    {
        public DeptComboBox()
        {
            InitializeComponent();
        }

        public DeptComboBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public void Init()
        {
            DeptBll bll = new DeptBll(Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.ParkConnect);
            //this.DataSource = bll.GetAllDepts().QueryObjects;

            List<DeptInfo> items = bll.GetAllDepts().QueryObjects;
            this.Items.Clear();
            this.Items.Add("");
            foreach (var item in items)
            {
                this.Items.Add(item);
            }

            this.DisplayMember = "DeptName";
            this.ValueMember = "DeptID";
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 获取或设置选择的部门
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DeptInfo Dept
        {
            get
            {
                if (this.SelectedIndex < 1)
                {
                    return null;
                }
                else
                {
                    return ((DeptInfo)this.Items[SelectedIndex]);
                }
            }
            set
            {
                if (value == null)
                {
                    this.SelectedIndex = -1;
                }
                else
                {
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        if (this.Items[i] == null || string.IsNullOrEmpty(this.Items[i].ToString()))
                            continue;
                        DeptInfo info = (DeptInfo)this.Items[i];
                        if (info.DeptID == value.DeptID)
                        {
                            this.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置选择的部门ID的字符串形式
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedDeptIDString
        {
            get
            {
                if (this.SelectedIndex < 1)
                {
                    return string.Empty;
                }
                else
                {
                    DeptInfo dept = (DeptInfo)this.Items[SelectedIndex];
                    return dept.DeptID.ToString();
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        //if (this.Items[i] == null || string.IsNullOrEmpty(this.Items[i].ToString()))
                        //    continue;
                        if (this.Items[i] is DeptInfo)
                        {
                            DeptInfo info = (DeptInfo)this.Items[i];
                            if (info.DeptID.ToString() == value)
                            {
                                this.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    this.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 获取或设置选择的部门ID
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid? SelectedDeptID
        {
            get
            {
                if (this.SelectedIndex < 1)
                {
                    return null;
                }
                else
                {
                    DeptInfo dept = (DeptInfo)this.Items[SelectedIndex];
                    return dept.DeptID;
                }
            }
            set
            {
                if (value.HasValue)
                {
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        //if (this.Items[i] == null || string.IsNullOrEmpty(this.Items[i].ToString()))
                        //    continue;
                        if (this.Items[i] is DeptInfo)
                        {
                            DeptInfo info = (DeptInfo)this.Items[i];
                            if (info.DeptID == value.Value)
                            {
                                this.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    this.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 获取选择的部门
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DeptInfo SelectedDept
        {
            get
            {
                if (this.SelectedIndex < 1)
                {
                    return null;
                }
                return this.Items[SelectedIndex] as DeptInfo;
            }
        }

    }
}
