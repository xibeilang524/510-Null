using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls
{
    public partial class UCPictureListView : UserControl
    {
        public UCPictureListView()
        {
            InitializeComponent();
        }

        #region 私有变量
        private List<Image> _Pictures;
        private int _CurImageIndex = 0;
        #endregion

        #region 私有方法
        private void btnLeft_Click(object sender, EventArgs e)
        {
            _CurImageIndex--;
            ShowImage(_CurImageIndex);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            _CurImageIndex++;
            ShowImage(_CurImageIndex);
        }

        private void ShowImage(int index)
        {
            this.btnLeft.Enabled = (index > 0);
            this.btnRight.Enabled = (index >= 0 && index < _Pictures.Count - 1);
            if (index >= 0 && index < _Pictures.Count)
            {
                this.picPanel.Image = _Pictures[index];
            }
            else
            {
                this.picPanel.Image = Properties.Resources.NoImage;
            }
        }
        #endregion

        #region 公共方法和属性
        public void ShowSnapShots(List<SnapShot> shots)
        {
            if (shots != null && shots.Count > 0)
            {
                List<Image> imgs = new List<Image>();
                foreach (SnapShot shot in shots)
                {
                    imgs.Add(shot.Image);
                }
                if (imgs.Count > 0)
                {
                    ShowImages(imgs);
                }
            }
        }
        /// <summary>
        /// 在图片网格中显示一系列图片,最多可以显示16张图片,超过此数产生异常
        /// </summary>
        /// <param name="imgs"></param>
        public void ShowImages(List<Image> imgs)
        {
            this._Pictures = imgs;
            this.btnLeft.Visible = !(imgs == null || imgs.Count == 1);
            this.btnRight.Visible = !(imgs == null || imgs.Count == 1);
            if (imgs != null && imgs.Count > 0)
            {
                _CurImageIndex = 0;
                ShowImage(_CurImageIndex);
            }
            else
            {
                _CurImageIndex = -1;
                ShowImage(_CurImageIndex);
            }
        }

        /// <summary>
        /// 清空所有图片
        /// </summary>
        public void Clear()
        {
            ShowImages(null);
        }

        /// <summary>
        /// 导出所到的图片到指定目录
        /// </summary>
        /// <param name="folder"></param>
        public int ExportImagesTo(string folder)
        {
            int count = 0;
            if (_Pictures != null && _Pictures.Count > 0)
            {
                for (int i = 0; i < _Pictures.Count; i++)
                {
                    string path = System.IO.Path.Combine(folder, Guid.NewGuid().ToString() + ".jpg");
                    Ralid.GeneralLibrary.ImageHelper.SaveImage(_Pictures[i], path);
                    count++;
                }
            }
            return count;
        }

        #endregion
    }
}
