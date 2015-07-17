using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace Ralid.Park.UI
{
    public class ResourceUtil
    {
        #region 公共方法
        /// <summary>
        /// 窗体引用资源文件
        /// </summary>
        /// <param name="form">要应用的窗体</param>
        public static void ApplyResource(Form form)
        {
            List<ComponentResourceManager> ress = new List<ComponentResourceManager>();
            ComponentResourceManager res = new ComponentResourceManager(form.GetType());
            ress.Add(res);
            Type basetype = form.GetType().BaseType;
            //窗体的父窗体不是Form时，需要将父窗体的资源文件也要查找，最多10个资源文件
            while (basetype.Name != "Form" && ress.Count < 10)
            {
                ComponentResourceManager res1 = new ComponentResourceManager(basetype);
                ress.Add(res1);
                basetype = basetype.BaseType;
            }

            form.Text = res.GetString("$this.Text");

            FieldInfo[] fields = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo fi in fields)
            {
                object obj = fi.GetValue(form);
                if (obj is ToolStrip)
                {
                    ApplyResource(obj as ToolStrip, ress);
                }
                else if (obj is DataGridView)
                {
                    ApplyResource(obj as DataGridView, ress);
                }
                else if (obj is Control)
                {
                    ApplyResource(obj as Control, ress);
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
        public static void ApplyResource(Control ctrl, List<ComponentResourceManager> ress)
        {
            if (ctrl is Ralid.Park.UserControls.CarTypePanel)  //这个控件的内容是通过配置文件生成的
            {
                return;
            }
            List<ComponentResourceManager> cress = ress;
            if (ctrl is UserControl)
            {
                //如果是自定义控制，从自定义控件的资源文件用查找
                cress = new List<ComponentResourceManager>();
                ComponentResourceManager res = new ComponentResourceManager(ctrl.GetType());
                cress.Add(res);
            }
            ctrl.Text = GetString(ctrl.Name + ".Text", cress);
            foreach (Control child in ctrl.Controls)
            {
                child.Text = GetString(child.Name + ".Text", cress);
                ApplyResource(child, cress);
            }
        }

        public static void ApplyResource(ToolStrip ts, List<ComponentResourceManager> ress)
        {
            foreach (ToolStripItem tsi in ts.Items)
            {
                tsi.Text = GetString(tsi.Name + ".Text",ress);
                if (tsi is ToolStripMenuItem)
                {
                    ApplyResource(tsi as ToolStripMenuItem, ress);
                }
            }
        }

        public static void ApplyResource(DataGridView grid, List<ComponentResourceManager> ress)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.HeaderText = GetString(string.Format("{0}.HeaderText", column.Name),ress);
            }
        }
        #endregion

        #region 私有方法
        private static void ApplyResource(ToolStripMenuItem tsi, List<ComponentResourceManager> ress)
        {
            foreach (ToolStripItem item in tsi.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    item.Text = GetString(item.Name + ".Text",ress);
                    ApplyResource(item as ToolStripMenuItem, ress);
                }
            }
        }

        private static string GetString(string name, List<ComponentResourceManager> ress)
        {
            string str = string.Empty;
            foreach (ComponentResourceManager res in ress)
            {
                str = res.GetString(name);
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
            }
            return str;
        }
        #endregion

        //#region 公共方法
        ///// <summary>
        ///// 窗体引用资源文件
        ///// </summary>
        ///// <param name="form">要应用的窗体</param>
        //public static void ApplyResource(Form form)
        //{
        //    ComponentResourceManager res = new ComponentResourceManager(form.GetType());

        //    form.Text = res.GetString("$this.Text");

        //    FieldInfo[] fields = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public |BindingFlags .Instance );
        //    foreach (FieldInfo fi  in fields)
        //    {
        //        object obj = fi.GetValue(form);
        //        if (obj is ToolStrip )
        //        {
        //            ApplyResource(obj as ToolStrip, res);
        //        }
        //        else if (obj is DataGridView)
        //        {
        //            ApplyResource(obj as DataGridView, res);
        //        }
        //        else if (obj is Control)
        //        {
        //            ApplyResource(obj as Control, res);
        //        }
        //    }
        //    if (form.IsMdiContainer)
        //    {
        //        foreach (Form frm in form.MdiChildren)
        //        {
        //            ApplyResource(frm);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 控件引用资源文件
        ///// </summary>
        ///// <param name="ctrl"></param>
        //public static void ApplyResource(Control ctrl, ComponentResourceManager res)
        //{
        //    if (ctrl is Ralid.Park.UserControls.CarTypePanel)  //这个控件的内容是通过配置文件生成的
        //    {
        //        return;
        //    }
        //    ctrl.Text = res.GetString(ctrl.Name + ".Text");
        //    foreach (Control child in ctrl.Controls)
        //    {
        //        child.Text = res.GetString(child.Name + ".Text");
        //        ApplyResource(child, res);
        //    }
        //}

        ///// <summary>
        ///// 控件引用资源文件
        ///// </summary>
        ///// <param name="ctrl"></param>
        //public static void ApplyResource(Control ctrl, List<ComponentResourceManager> ress)
        //{
        //    if (ctrl is Ralid.Park.UserControls.CarTypePanel)  //这个控件的内容是通过配置文件生成的
        //    {
        //        return;
        //    }
        //    ctrl.Text = GetString(ctrl.Name + ".Text",ress);
        //    foreach (Control child in ctrl.Controls)
        //    {
        //        child.Text = GetString(child.Name + ".Text",ress);
        //        ApplyResource(child, ress);
        //    }
        //}

        //public static void ApplyResource(ToolStrip ts, ComponentResourceManager res)
        //{
        //    foreach (ToolStripItem tsi in ts.Items)
        //    {
        //        tsi.Text = res.GetString(tsi.Name + ".Text");
        //        if (tsi is ToolStripMenuItem)
        //        {
        //            ApplyResource(tsi as ToolStripMenuItem, res);
        //        }
        //    }
        //}

        //public static void ApplyResource(DataGridView grid, ComponentResourceManager res)
        //{
        //    foreach (DataGridViewColumn column in grid.Columns)
        //    {
        //        column.HeaderText = res.GetString(string.Format("{0}.HeaderText", column.Name));
        //    }
        //}
        //#endregion

        //#region 私有方法
        //private static void ApplyResource(ToolStripMenuItem tsi, ComponentResourceManager res)
        //{
        //    foreach (ToolStripItem item in tsi.DropDownItems)
        //    {
        //        if (item is ToolStripMenuItem)
        //        {
        //            item.Text = res.GetString(item.Name + ".Text");
        //            ApplyResource(item as ToolStripMenuItem, res);
        //        }
        //    }
        //}
        //#endregion
    }
}