using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.Park.UserControls
{
    public partial class UCPaging : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UCPaging()
        {
            InitializeComponent();
            PageIndex = 1;
        }

        #region 公共方法
        /// <summary>
        /// 初始化分页控件
        /// </summary>
        /// <param name="defultpagesizeindex">默认一页显示行数，0：10，1：30，2：50，3：100，4：1000，5：10000</param>
        public void Init(int defultpagesizeindex)
        {
            if (cmbpagesize.SelectedIndex != defultpagesizeindex)
            {
                cmbpagesize.SelectedIndex = defultpagesizeindex;
            }
            else
            {
                GetPageData(1, Convert.ToInt32(cmbpagesize.SelectedItem));
            }
            DefaultPageSizeIndex = defultpagesizeindex;
            PageIndex = 1;
        }

        /// <summary>
        /// 获取默认一页显示的行数
        /// </summary>
        public int DefaultPageSizeIndex { get; set; }

        /// <summary>
        /// 获取或设置每页显示的记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return Convert.ToInt32(this.cmbpagesize.SelectedItem);
            }
            set
            {
                this.cmbpagesize.SelectedItem = value;
            }
        }

        /// <summary>
        /// 获取或设置当前显示的页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                return Convert.ToInt32(tbpageindex.Text);
            }
            set
            {
                tbpageindex.Text = value.ToString();
            }
        }

        /// <summary>
        /// 获取或设置需要查看的页码
        /// </summary>
        public int GoPageIndex
        {
            get
            {
                return Convert.ToInt32(tbgotoindex.Text);
            }
            set
            {
                tbgotoindex.Text = value.ToString();
            }
        }

        /// <summary>
        /// 获取或设置总页数

        /// </summary>
        public int TotalPages
        {
            get
            {
                return Convert.ToInt32(labtotalpagecount.Text);
            }
            set
            {
                labtotalpagecount.Text = value.ToString();
                labtotalpages.Text = value.ToString();
            }
        }

        /// <summary>
        /// 获取或设置总记录数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return Convert.ToInt32(labtotalcount.Text);
            }
            set
            {
                labtotalcount.Text = value.ToString();
            }
        }

        #endregion



        #region 窗体事件
        /// <summary>
        /// 委托，查询指定页面的数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        public delegate void  HandleEventGetPageData(int index,int pagesize);
        public event HandleEventGetPageData GetPageData;

        //第一页

        private void labfristpage_Click(object sender, EventArgs e)
        {
            //if (cmbpagesize.SelectedItem == null)
            //{
            //    cmbpagesize.SelectedIndex = DefaultPageSizeIndex;
            //    return;
            //}
            if (PageIndex > 1)
            {
                GetPageData(1, PageSize);
                PageIndex = 1;
            }
        }

        //上一页

        private void labuppage_Click(object sender, EventArgs e)
        {
            //if (cmbpagesize.SelectedItem == null)
            //{
            //    cmbpagesize.SelectedIndex = DefaultPageSizeIndex;
            //    return;
            //}
            if (PageIndex > 1)
            {
                GetPageData(PageIndex - 1, PageSize);
                PageIndex--;
            }
        }

        //下一页

        private void labdownpage_Click(object sender, EventArgs e)
        {
            //if (cmbpagesize.SelectedItem == null)
            //{
            //    cmbpagesize.SelectedIndex = DefaultPageSizeIndex;
            //    return;
            //}
            if (PageIndex < TotalPages)
            {
                GetPageData(PageIndex + 1, PageSize);
                PageIndex++;
            }
        }

        //最后页
        private void lablastpage_Click(object sender, EventArgs e)
        {
            //if (cmbpagesize.SelectedItem == null)
            //{
            //    cmbpagesize.SelectedIndex = DefaultPageSizeIndex;
            //    return;
            //}
            if (PageIndex != TotalPages)
            {
                GetPageData(TotalPages, PageSize);
                PageIndex = TotalPages;
            }
        }

        //指定到那一页

        private void labgotopage_Click(object sender, EventArgs e)
        {
            int gotoindex = 0;
            if (int.TryParse(tbgotoindex.Text, out gotoindex) && Convert.ToInt32(tbgotoindex.Text) > 0 && Convert.ToInt32(tbgotoindex.Text) <= TotalPages)
            {
                GetPageData(gotoindex, PageSize);
                PageIndex = gotoindex;
            }
        }

        private void cmbpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpagesize.SelectedItem != null && PageSize != 0)
            {
                GetPageData(1, Convert.ToInt32(cmbpagesize.SelectedItem));
                TotalPages = (int)Math.Ceiling(TotalCount * 1.0M / PageSize);
                PageIndex = 1;
                tbgotoindex.Text = "";
            }

            DefaultPageSizeIndex = cmbpagesize.SelectedIndex;
        }


        #endregion
    }
}
