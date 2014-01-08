using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace OutDoorLEDTool
{
    internal class ResourceUtil
    {
        #region 公共方法
        /// <summary>
        /// 窗体引用资源文件
        /// </summary>
        /// <param name="form">要应用的窗体</param>
        public static void ApplyResource(Form form)
        {
            ComponentResourceManager res = new ComponentResourceManager(form.GetType());
            form.Text = res.GetString("$this.Text");

            FieldInfo[] fields = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public |BindingFlags .Instance );
            foreach (FieldInfo fi  in fields)
            {
                object obj = fi.GetValue(form);
                if (obj is ToolStrip )
                {
                    ApplyResource(obj as ToolStrip, res);
                }
                else if (obj is DataGridView)
                {
                    ApplyResource(obj as DataGridView, res);
                }
                else if (obj is Control)
                {
                    ApplyResource(obj as Control, res);
                }
            }
            if (form.IsMdiContainer)
            {
                foreach (Form frm in form.MdiChildren)
                {
                    ApplyResource(frm);
                }
            }
        }
        /// <summary>
        /// 控件引用资源文件
        /// </summary>
        /// <param name="ctrl"></param>
        public static void ApplyResource(Control ctrl, ComponentResourceManager res)
        {
            if (ctrl is Ralid.Park.UserControls.CarTypePanel)  //这个控件的内容是通过配置文件生成的
            {
                return;
            }
            ctrl.Text = res.GetString(ctrl.Name + ".Text");
            foreach (Control child in ctrl.Controls)
            {
                child.Text = res.GetString(child.Name + ".Text");
                ApplyResource(child, res);
            }
        }

        public static void ApplyResource(ToolStrip ts, ComponentResourceManager res)
        {
            foreach (ToolStripItem tsi in ts.Items)
            {
                tsi.Text = res.GetString(tsi.Name + ".Text");
                if (tsi is ToolStripMenuItem)
                {
                    ApplyResource(tsi as ToolStripMenuItem, res);
                }
            }
        }

        public static void ApplyResource(DataGridView grid, ComponentResourceManager res)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.HeaderText = res.GetString(string.Format("{0}.HeaderText", column.Name));
            }
        }
        #endregion

        #region 私有方法
        private static void ApplyResource(ToolStripMenuItem tsi, ComponentResourceManager res)
        {
            foreach (ToolStripItem item in tsi.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    item.Text = res.GetString(item.Name + ".Text");
                    ApplyResource(item as ToolStripMenuItem, res);
                }
            }
        }
        #endregion
    }
}