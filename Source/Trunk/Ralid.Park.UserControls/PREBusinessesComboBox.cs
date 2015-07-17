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
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.UserControls
{
    public partial class PREBusinessesComboBox : ComboBox
    {
        public PREBusinessesComboBox()
        {
            InitializeComponent();
        }

        public PREBusinessesComboBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        ListBox listbox1 = new ListBox();

        public void Init()
        {
            PREBusinessesBll bll = new PREBusinessesBll(AppSettings.CurrentSetting.ParkConnect);
            List<PREBusinesses> items = bll.GetAllBusinesses().QueryObjects;

            //this.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            //this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            
            SetDataSource(items);

            this.TextChanged += new EventHandler(PREBusinessesComboBox_TextChanged);
            this.SelectedIndexChanged += new EventHandler(PREBusinessesComboBox_SelectedIndexChanged);
            //listbox1.SelectedIndexChanged += new EventHandler(listbox1_SelectedIndexChanged);
            listbox1.MouseDoubleClick += new MouseEventHandler(listbox1_MouseDoubleClick);
        }

        private void PREBusinessesComboBox_TextChanged(object sender, EventArgs e)
        {
            listbox1.Visible = false;
            if (!this.Parent.Controls.Contains(listbox1))
                this.Parent.Controls.Add(listbox1);
            listbox1.Width = this.Width;
            listbox1.Location = new System.Drawing.Point(this.Left, this.Top + this.Height);
            if (string.IsNullOrEmpty(this.Text))
                return;

            PreferentialReportSearchCondition con = new PreferentialReportSearchCondition();
            con.BusinessName = this.Text;
            PREBusinessesBll bll = new PREBusinessesBll(AppSettings.CurrentSetting.ParkConnect);
            List<PREBusinesses> items = bll.GetBusinesses(con).QueryObjects;
            if (items.Count > 0)
            {
                listbox1.DataSource = items;
                listbox1.ValueMember = "BusinessesID";
                listbox1.DisplayMember = "BusinessesName";
                listbox1.Visible = true;
                listbox1.BringToFront();
            }
        }

        private void PREBusinessesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {//如果是直接选择则不必显示listbox
            listbox1.Visible = false;
        }

        private void listbox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Text = (listbox1.SelectedItem as PREBusinesses).BusinessesName;
            //listbox1.Visible = false;
        }

        private void listbox1_MouseDoubleClick(object sender, EventArgs e)
        {
            this.Text = (listbox1.SelectedItem as PREBusinesses).BusinessesName;
            listbox1.Visible = false;
        }

        public void SetDataSource(List<PREBusinesses> items)
        {
            if (items != null)
            {
                List<PREBusinesses> data = new List<PREBusinesses>();
                data.Insert(0, new PREBusinesses());
                data.AddRange(items);
                this.DataSource = data;
                this.DisplayMember = "BusinessesName";
            }
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedBusinessesName
        {
            get
            {
                if (SelectedIndex > 0)
                {
                    PREBusinesses item = Items[SelectedIndex] as PREBusinesses;
                    return item.BusinessesName;
                }
                else
                {
                    return string.Empty;
                }
                
            }
        }

        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid SelectedBusinessesID
        {
            get
            {
                if (SelectedIndex > 0)
                {
                    PREBusinesses item = Items[SelectedIndex] as PREBusinesses;
                    return item.BusinessesID;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    PREBusinesses item = Items[i] as PREBusinesses;
                    if (item.BusinessesID == value)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
