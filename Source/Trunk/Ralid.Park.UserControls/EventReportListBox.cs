using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel .Report ;
using Ralid.Park.BusinessModel .Interface ;
using Ralid.Park.BLL;

namespace Ralid.Park.UserControls
{
    public partial class EventReportListBox : ListBox
    {
        #region 构造函数
        public EventReportListBox()
        {
            InitializeComponent();
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DrawItem += EventReportListBox_DrawItem;
        }

        public EventReportListBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DrawItem += EventReportListBox_DrawItem;
        }
        #endregion

        #region 私有变量
        #endregion

        #region 公共方法

        public void InsertMessage(string msg)
        {
            InsertMessage(msg, Color.Empty);
        }

        public void InsertMessage(string msg, Color color)
        {
            ColorString colorString = new ColorString(msg, color);

            if (this.Items.Count == 1000)
            {
                this.Items.Clear();
            }
            this.Items.Insert(0, colorString);
            this.SelectedIndex = -1;
        }

        public void InsertReport(ReportBase report)
        {
            Color color = Color.Empty;
            if (report is CardEventReport)
            {
                CardEventReport ce = report as CardEventReport;
                color = ce.ComparisonResult == CarPlateComparisonResult.Fail ? Color.Red : Color.Empty;
            }
            InsertMessage(report.Description, color);
        }
        #endregion

        #region 私有事件

        private void EventReportListBox_DoubleClick(object sender, EventArgs e)
        {
            this.Items.Clear();
            HorizontalExtent = 0;
        }

        private void EventReportListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listbox=(ListBox)sender;
            if (listbox.Items.Count > 0 && e.Index > -1)
            {
                ColorString colorItem = listbox.Items[e.Index] as ColorString;
                Color color = colorItem != null && colorItem.TextColor != Color.Empty ? colorItem.TextColor : e.ForeColor;
                string text = listbox.Items[e.Index].ToString();
                e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
                e.Graphics.DrawString(text, e.Font, new SolidBrush(color), e.Bounds);
                e.DrawFocusRectangle();
                SizeF size = e.Graphics.MeasureString(text, this.Font);
                if (size.Width > HorizontalExtent)
                {
                    HorizontalExtent = (int)Math.Ceiling(size.Width);
                }
            }
            else
            {
                HorizontalExtent = 0;
            }
        }
        #endregion
    }

    /// <summary>
    /// 表示一个又颜色的字符串
    /// </summary>
    public class ColorString
    {
        #region 构造函数
        public ColorString()
        {
        }
        public ColorString(string text, Color color)
        {
            Text = text;
            TextColor = color;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 字符串文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 字符串颜色
        /// </summary>
        public Color TextColor { get; set; }
        #endregion

        #region 重写函数
        public override string ToString()
        {
            return Text;
        }
        #endregion
    }
}
