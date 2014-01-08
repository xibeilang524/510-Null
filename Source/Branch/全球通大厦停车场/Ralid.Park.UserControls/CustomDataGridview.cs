using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Ralid.Park.UserControls
{
    public partial class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            InitializeComponent();
        }

        public CustomDataGridView(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        #region 重写基类方法
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > -1)
                {
                    if (!this.IsRowSelected(this.Rows[e.RowIndex]))
                    {
                        this.SelectNone();
                        this.Rows[e.RowIndex].Selected = true;
                    }
                }
            }
            base.OnCellMouseDown(e);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 选择行
        /// </summary>
        /// <param name="row"></param>
        public void SelectRow(DataGridViewRow row)
        {
            DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
            if (chk != null)
            {
                chk.Value = true;
            }
            else
            {
                row.Selected = true;
            }
        }
        /// <summary>
        /// 取消行选定
        /// </summary>
        /// <param name="row"></param>
        public void UnSelectRow(DataGridViewRow row)
        {
            DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
            if (chk != null)
            {
                chk.Value = false;
            }
            row.Selected = false;
        }
        /// <summary>
        /// 全选
        /// </summary>
        public void SelectAllRows()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                SelectRow(row);
            }
        }

        /// <summary>
        /// 全不选
        /// </summary>
        public void SelectNone()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                UnSelectRow(row);
            }
        }

        /// <summary>
        /// 反选
        /// </summary>
        public void Invert()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                if (IsRowSelected(row))
                {
                    UnSelectRow(row);
                }
                else
                {
                    SelectRow(row);
                }
            }
        }
        /// <summary>
        /// 行是否被选中
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsRowSelected(DataGridViewRow row)
        {
            DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
            if (chk != null)
            {
                return (chk.EditedFormattedValue != null && (bool)chk.EditedFormattedValue == true);
            }
            else
            {
                return row.Selected;
            }
        }

        public List<object> GetSelectedItems()
        {
            List<object> list = new List<object>();

            foreach (DataGridViewRow row in this.Rows)
            {
                if (IsRowSelected(row))
                {
                    list.Add(GetItem(row));
                }
            }
            return list;
        }

        public object GetItem(DataGridViewRow row)
        {
            if (row.DataBoundItem != null)
            {
                return row.DataBoundItem;
            }
            else if (row.Tag != null)
            {
                return row.Tag;
            }
            else
            {
                return null;
            }
        }

        public void SaveToFile(string file)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                using (fs = new FileStream(file, FileMode.Create))
                {
                    using (writer = new StreamWriter(fs, Encoding.Unicode))
                    {
                        StringBuilder header = new StringBuilder();
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            header.Append(this.Columns[i].HeaderText + "\t");
                        }
                        writer.WriteLine(header.ToString());
                        foreach (DataGridViewRow row in this.Rows)
                        {
                            StringBuilder rowText = new StringBuilder();
                            for (int i = 0; i < this.Columns.Count; i++)
                            {
                                if (row.Cells[i].Value != null)
                                {
                                    rowText.Append(row.Cells[i].Value.ToString() + "\t");
                                }
                                else
                                {
                                    rowText.Append(string.Empty + "\t");
                                }
                            }
                            writer.WriteLine(rowText.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error!");
            }
            finally
            {
                if (writer != null) { writer.Close(); }
                if (fs != null) { fs.Close(); }
            }
        }
        #endregion


    }
}

