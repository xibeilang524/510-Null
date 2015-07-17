using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UserControls;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;

namespace PreferentialSystem
{
    public partial class FrmPREWorksationSelect : Form
    {
        public FrmPREWorksationSelect()
        {
            InitializeComponent();
        }

        private CustomDataGridView GridView
        {
            get
            {
                return this.customGV;
            }
        }

        /// <summary>
        /// 获取或设置选择的多个工作站
        /// </summary>
        public List<string> SelectionItems { get; set; }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        private List<object> GetDataSource()
        {
            List<object> items = new List<object>();
            foreach (KeyValuePair<Guid, PREWorkstation> o in PREWorkstationSetting.Current.WorkstationDictionary)
            {
                items.Add(o.Value);
            }
            return items;
        }

        /// <summary>
        /// 绑定数据到表格
        /// </summary>
        private void BindDataToGridView()
        {
            if (GridView != null)
            {
                GridView.Rows.Clear();
                List<object> datasource = GetDataSource();
                if (datasource != null && datasource.Count > 0)
                {
                    foreach (object item in datasource)
                    {
                        Add_A_Row(item);
                    }
                }
                if (this.GridView.Rows.Count > 0)
                {
                    this.GridView.Rows[0].Selected = false;
                }
            }
        }
        private void Add_A_Row(object item)
        {
            int row = GridView.Rows.Add();
            ShowItemInGridViewRow(GridView.Rows[row], item);
            GridView.Rows[row].Tag = item;
        }
        private void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
            if (item is PREWorkstation)
            {
                ShowItemInGridViewRow1(row, item as PREWorkstation);
            }
        }
        private void ShowItemInGridViewRow1(DataGridViewRow row, PREWorkstation item)
        {
            PREWorkstation info = item;
            row.Tag = info;
            row.Cells["colWorkstationName"].Value = info.WorkstationName;
        }

        private string GetItemName(object item)
        {
            string name = string.Empty;
            if (item is PREWorkstation)
            {

                PREWorkstation info = item as PREWorkstation;
                name = info.WorkstationName;
            }
            return name;
        }


        /// <summary>
        /// 选择工作站
        /// </summary>
        private void SelectWorkStations()
        {
            if (this.SelectionItems != null)
            {
                foreach (string item in SelectionItems)
                {
                    foreach (DataGridViewRow row in GridView.Rows)
                    {
                        if (row.Visible)
                        {
                            string name = GetItemName(row.Tag);

                            if (name == item)
                            {
                                DataGridViewCheckBoxCell c = row.Cells["colCheck"] as DataGridViewCheckBoxCell;
                                c.Value = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取选择的工作站
        /// </summary>
        private List<string> GetSelectWorkStations()
        {
            List<string> items = new List<string>();
            foreach (DataGridViewRow row in GridView.Rows)
            {
                if (row.Visible)
                {
                    DataGridViewCheckBoxCell c = row.Cells["colCheck"] as DataGridViewCheckBoxCell;
                    if (c.Value != null && (bool)c.Value)
                    {
                        string name = GetItemName(row.Tag);
                        items.Add(name);
                    }
                }
            }
            return items;
        }

        /// <summary>
        /// 清除选择
        /// </summary>
        private void ClearSelection()
        {
            foreach (DataGridViewRow row in GridView.Rows)
            {
                PREWorkstation info = row.Tag as PREWorkstation;
                DataGridViewCheckBoxCell c = row.Cells["colCheck"] as DataGridViewCheckBoxCell;
                c.Value = false;
            }
        }

        /// <summary>
        /// 选择所有
        /// </summary>
        private void SelectAll()
        {
            foreach (DataGridViewRow row in GridView.Rows)
            {
                if (row.Visible)
                {
                    PREWorkstation info = row.Tag as PREWorkstation;
                    DataGridViewCheckBoxCell c = row.Cells["colCheck"] as DataGridViewCheckBoxCell;
                    c.Value = true;
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<string> items = GetSelectWorkStations();

            SelectionItems = items;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FrmPREWorksationSelect_Load(object sender, EventArgs e)
        {
            BindDataToGridView();
            SelectWorkStations();
        }
    }
}
