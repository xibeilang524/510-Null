using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UserControls ;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI
{
    public partial class FrmMasterBase : Form
    {
        public FrmMasterBase()
        {
            InitializeComponent();
        }

        #region 私有方法
        private CustomDataGridView _gridView;

        private CustomDataGridView GridView
        {
            get
            {
                if (_gridView == null)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl is CustomDataGridView)
                        {
                            _gridView = ctrl as CustomDataGridView;
                        }
                    }
                }
                return _gridView;
            }
        }

        private void Add_A_Row(object item)
        {
            int row = GridView.Rows.Add();
            ShowItemInGridViewRow(GridView.Rows[row], item);
            GridView.Rows[row].Tag = item;
            this.toolStripStatusLabel1.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, GridView.Rows.Count);
        }

        private void Update_A_Row(object item)
        {
            foreach (DataGridViewRow row in GridView.Rows)
            {
                object pre = GridView.GetItem(row);
                if (pre != null && pre == item)
                {
                    ShowItemInGridViewRow(row, item);
                }
            }
        }

        #endregion

        #region 保护方法
        /// <summary>
        /// 绑定数据到表格
        /// </summary>
        protected void BindDataToGridView()
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
                    this.toolStripStatusLabel1.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, GridView.Rows.Count);
                }
            }
        }
        #endregion

        #region 子类要重写的方法
        protected virtual FrmDetailBase GetDetailForm()
        {
            return null;
        }

        protected virtual List<object> GetDataSource()
        {
            return new List<object>();
        }

        protected virtual void ShowItemInGridViewRow(DataGridViewRow row, object item)
        {
        }

        protected virtual bool DeletingItem(object item)
        {
            return false;
        }

        protected virtual ContextMenuStrip GetContextMenuStrip()
        {

            return this.contextMenuStrip1;
        }
        protected virtual void InitControls()
        { 
        }
        #endregion

        #region 事件处理
        private void FrmMasterBase_Load(object sender, EventArgs e)
        {
            InitControls();
            if (GridView != null)
            {
                GridView.BorderStyle = BorderStyle.FixedSingle;
                GridView.BackgroundColor = Color.White;
                BindDataToGridView();
                GridView.CellDoubleClick += GridView_DoubleClick;
                GridView.MouseDown += new MouseEventHandler(GridView_MouseDown);
            }
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            CustomDataGridView view = this.GridView;
            if (view != null)
            {
                view.SelectAllRows();
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            CustomDataGridView view = this.GridView;
            if (view != null)
            {
                view.Invert();
            }
        }

        private void btnSelNone_Click(object sender, EventArgs e)
        {
            CustomDataGridView view = this.GridView;
            if (view != null)
            {
                view.SelectNone();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> deletingItems = this.GridView.GetSelectedItems();
                List<DataGridViewRow> deletingRows = new List<DataGridViewRow>();
                if (deletingItems.Count > 0)
                {
                    DialogResult result = MessageBox.Show(Resources.Resource1.FrmMasterBase_DeleteQuery, Resources.Resource1.Form_Query, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        for (int i = GridView.Rows.Count - 1; i > -1; i--)
                        {
                            DataGridViewRow row = GridView.Rows[i];
                            if (GridView.IsRowSelected(row))
                            {
                                object deletingItem = GridView.GetItem(row);
                                if (DeletingItem(deletingItem))
                                {
                                    deletingRows.Add(row);
                                }
                            }
                        }
                        foreach (DataGridViewRow row in deletingRows)
                        {
                            GridView.Rows.Remove(row);
                        }
                        this.toolStripStatusLabel1.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, GridView.Rows.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FrmDetailBase detailForm = GetDetailForm();
                if (detailForm != null)
                {
                    detailForm.IsAdding = true;
                    detailForm.ItemAdded += delegate(object obj, ItemAddedEventArgs args)
                    {
                        Add_A_Row(args.AddedItem);
                    };
                    detailForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void GridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                object pre = this.GridView.GetItem(this.GridView.Rows[e.RowIndex]);
                if (pre != null)
                {
                    FrmDetailBase detailForm = GetDetailForm();
                    if (detailForm != null)
                    {
                        detailForm.IsAdding = false;
                        detailForm.UpdatingItem = pre;

                        detailForm.ItemUpdated += delegate(object obj, ItemUpdatedEventArgs args)
                        {
                            Update_A_Row(args.UpdatedItem);
                        };
                        detailForm.ShowDialog();
                    }
                }
            }
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridView.ContextMenuStrip = GetContextMenuStrip();
            }
        }

        private void mnu_Property_Click(object sender, EventArgs e)
        {
            List<object> updateItems = this.GridView.GetSelectedItems();
            if (updateItems.Count > 0)
            {
                object pre = updateItems[0];
                if (pre != null)
                {
                    FrmDetailBase detailForm = GetDetailForm();
                    if (detailForm != null)
                    {
                        detailForm.IsAdding = false;
                        detailForm.UpdatingItem = pre;
                        detailForm.ItemUpdated += delegate(object obj, ItemUpdatedEventArgs args)
                        {
                            Update_A_Row(args.UpdatedItem);
                        };
                        detailForm.ShowDialog();
                    }
                }
            }
        }

        private void mnu_Fresh_Click(object sender, EventArgs e)
        {
            BindDataToGridView();
        }
        #endregion

    }
}
