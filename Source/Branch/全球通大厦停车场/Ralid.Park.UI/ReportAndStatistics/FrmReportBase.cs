using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI.ReportAndStatistics
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
        public CustomDataGridView GridView
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
                CustomDataGridView view = this.GridView;
                if (view != null)
                {
                    saveFileDialog1.Filter = Resources.Resource1.Form_ExcelFilter;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog1.FileName;
                        view.SaveToFile(path);
                        MessageBox.Show(Resources.Resource1.FrmReportBase_SaveSuccess);
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
            this.searchInfo.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, this.GridView.Rows.Count);
        }

        private void FrmReportBase_Load(object sender, EventArgs e)
        {
            if (GridView != null)
            {
                GridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }
        #endregion
    }
}
