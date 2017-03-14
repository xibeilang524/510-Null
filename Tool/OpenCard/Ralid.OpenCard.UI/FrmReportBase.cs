using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UserControls;

namespace Ralid.OpenCard.UI
{
    public partial class FrmReportBase : Form
    {
        public FrmReportBase()
        {
            InitializeComponent();
        }

        #region 事件
        public event EventHandler ItemSearching;

        protected virtual void OnItemSearching(EventArgs e)
        {
            if (this.ItemSearching != null) this.ItemSearching(this, EventArgs.Empty);
        }
        #endregion

        #region 公共属性
        public virtual CustomDataGridView GridView
        {
            get
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is CustomDataGridView)
                    {
                        return ctrl as CustomDataGridView;
                    }
                }
                return null;
            }
        }
        public virtual List<CustomDataGridView> GridViews
        {
            get
            {
                List<CustomDataGridView> views = new List<CustomDataGridView>();
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is CustomDataGridView)
                    {
                        views.Add(ctrl as CustomDataGridView);
                    }
                }
                return views;
            }
        }
        #endregion

        #region 事件处理程序
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                List<CustomDataGridView> views = this.GridViews;
                if (views != null)
                {
                    foreach (CustomDataGridView view in views)
                    {
                        saveFileDialog1.Title = view.AccessibleDescription;
                        saveFileDialog1.Filter = "Excel文档|*.xls;*.xlsx|所有文件(*.*)|*.*";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string path = saveFileDialog1.FileName;
                            view.SaveToFile(path);
                            MessageBox.Show("保存成功!");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OnItemSearching(EventArgs.Empty);
            this.searchInfo.Text = string.Format("共有 {0} 项", this.GridView.Rows.Count);
        }

        private void FrmReportBase_Load(object sender, EventArgs e)
        {
            if (GridViews != null)
            {
                foreach (CustomDataGridView view in GridViews)
                {
                    view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
        }
        #endregion
    }
}
