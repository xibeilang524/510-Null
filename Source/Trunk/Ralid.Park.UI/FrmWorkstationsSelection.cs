using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI
{
    public partial class FrmWorkstationsSelection : Form
    {
        #region 构造函数
        public FrmWorkstationsSelection()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有属性
        private CustomDataGridView GridView
        {
            get
            {
                return this.StationView;
            }
        }

        /// <summary>
        /// 是否只显示中央收费站
        /// </summary>
        private bool _ShowCenterOnly;
        /// <summary>
        /// 是否只显示缴费机
        /// </summary>
        private bool _ShowAPMOnly;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置选择的多个工作站
        /// </summary>
        public List<string> SelectionItems { get; set; }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        private List<object> GetDataSource()
        {
            List<object> items = new List<object>();

            WorkstationBll wsBll = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect);
            List<WorkStationInfo> stations = wsBll.GetAllWorkstations().QueryObjects;
            if (stations != null) items.AddRange(stations);

            APMBll apmBll = new APMBll(AppSettings.CurrentSetting.ParkConnect);
            List<APM> apms = apmBll.GetAllItems().QueryObjects;
            if (apms != null) items.AddRange(apms);

            return items;
        }

        /// <summary>
        /// 是否显示行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool ShowGridViewRow(DataGridViewRow row)
        {
            object item = row.Tag;
            if (_ShowAPMOnly)
            {
                if (!(item is APM))
                {
                    return false;
                }
            }
            if (_ShowCenterOnly)
            {
                if (!(item is WorkStationInfo))
                {
                    return false;
                }

                WorkStationInfo info = item as WorkStationInfo;
                if (!info.IsCenterCharge)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 显示表格的行
        /// </summary>
        private void ShowDataGridRows()
        {
            foreach (DataGridViewRow row in GridView.Rows)
            {
                row.Visible = ShowGridViewRow(row);
            }
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
            if (item is WorkStationInfo)
            {
                ShowItemInGridViewRow(row, item as WorkStationInfo);
            }
            else if (item is APM)
            {
                ShowItemInGridViewRow(row, item as APM);
            }
        }

        private void ShowItemInGridViewRow(DataGridViewRow row, WorkStationInfo item)
        {
            WorkStationInfo info = item;
            row.Tag = info;
            row.Cells["colWorkstationID"].Value = info.StationName;
            DataGridViewCheckBoxCell c = row.Cells["colCenterCharge"] as DataGridViewCheckBoxCell;
            c.Value = info.IsCenterCharge;
        }

        private void ShowItemInGridViewRow(DataGridViewRow row, APM item)
        {
            APM info = item;
            row.Tag = info;
            row.Cells["colWorkstationID"].Value = info.SerialNum;
            DataGridViewCheckBoxCell c = row.Cells["colAPM"] as DataGridViewCheckBoxCell;
            c.Value = true;
        }

        private string GetItemName(object item)
        {
            string name = string.Empty;
            if (item is WorkStationInfo)
            {

                WorkStationInfo info = item as WorkStationInfo;
                name = info.StationName;
            }
            else if (item is APM)
            {
                APM info = item as APM;
                name = info.SerialNum;
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
                WorkStationInfo info = row.Tag as WorkStationInfo;
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
                    WorkStationInfo info = row.Tag as WorkStationInfo;
                    DataGridViewCheckBoxCell c = row.Cells["colCheck"] as DataGridViewCheckBoxCell;
                    c.Value = true;
                }
            }
        }
        #endregion


        #region 窗体事件
        private void FrmWorkstationsSelection_Load(object sender, EventArgs e)
        {
            BindDataToGridView();
            SelectWorkStations();
        }
        private void chkCenterOnly_CheckedChanged(object sender, EventArgs e)
        {
            _ShowCenterOnly = this.chkCenterOnly.Checked;
            if (_ShowCenterOnly && this.chkAPMOnly.Checked)
            {
                this.chkAPMOnly.Checked = false;
            }
            else
            {
                ShowDataGridRows();
            }
        }
        private void chkAPMOnly_CheckedChanged(object sender, EventArgs e)
        {
            _ShowAPMOnly = this.chkAPMOnly.Checked;
            if (_ShowAPMOnly && this.chkCenterOnly.Checked)
            {
                this.chkCenterOnly.Checked = false;
            }
            else
            {
                ShowDataGridRows();
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            List<string> items = GetSelectWorkStations();

            SelectionItems = items;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }
        private void btnAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }
        #endregion





    }
}
