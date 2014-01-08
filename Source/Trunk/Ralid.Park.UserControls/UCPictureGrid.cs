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
    public partial class UCPictureGrid : UserControl
    {
        #region 构造函数
        public UCPictureGrid()
        {
            InitializeComponent();
            SetShowMode(1, 1);
            this.Resize += new EventHandler(UCPictureGrid_Resize);
        }
        #endregion

        #region 私有变量和方法
        private int _Rows;
        private int _Columns;
        private readonly int _gap = 0;  //各个视频之间的空隙
        private List<PictureBox> _pictures = new List<PictureBox>();

        private void UCPictureGrid_Resize(object sender, EventArgs e)
        {
            LayoutPictures();
        }

        private void LayoutPictures()
        {
            int width = 0;
            int height = 0;
            if (_Rows > 0 && _Columns > 0)
            {
                this.SuspendLayout();
                width = (this.Width - _gap * (_Columns - 1)) / _Columns;
                height = (this.Height - _gap * (_Rows - 1)) / _Rows;

                for (int i = 0; i < _Rows; i++)
                {
                    for (int j = 0; j < _Columns; j++)
                    {
                        _pictures[i * _Columns + j].Left = (width + _gap) * j;
                        _pictures[i * _Columns + j].Top = (height + _gap) * i;
                        _pictures[i * _Columns + j].Width = width;
                        _pictures[i * _Columns + j].Height = height;
                    }
                }
                this.ResumeLayout(false);
            }
        }

        private void SetShowMode(int rows, int columns)
        {
            if (rows > 0 && columns > 0)
            {
                _Rows = rows;
                _Columns = columns;
                for (int i = _pictures.Count; i < rows * columns; i++)
                {
                    PictureBox pic = new PictureBox();
                    pic.AllowDrop = true;
                    pic.Name = "pic" + i.ToString();
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    ClearPictureBox(pic);
                    this.Controls.Add(pic);
                    pic.Visible = true;
                    _pictures.Add(pic);
                }
                LayoutPictures();
            }
        }

        private static void ClearPictureBox(PictureBox pic)
        {
            pic.Image = Ralid.Park.UserControls.Properties.Resources.NoImage;
            pic.Tag = null;
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
            if (imgs != null)
            {
                switch (imgs.Count)
                {
                    case 0:
                    case 1:
                        SetShowMode(1, 1);
                        break;
                    case 2:
                    case 3:
                    case 4:
                        SetShowMode(2, 2);
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        SetShowMode(3, 3);
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        SetShowMode(4, 4);
                        break;
                    default:
                        SetShowMode(4, 4);  //最多显示其中16张
                        break;
                }
                foreach (PictureBox pic in _pictures)
                {
                    ClearPictureBox(pic);
                }

                for (int i = 0; i < imgs.Count; i++)
                {
                    _pictures[i].Image = imgs[i];
                    _pictures[i].Tag = imgs[i];
                }
            }
        }

        /// <summary>
        /// 清空所有图片
        /// </summary>
        public void Clear()
        {
            SetShowMode(1, 1);
            ClearPictureBox(this._pictures[0]);
        }

        /// <summary>
        /// 导出所到的图片到指定目录
        /// </summary>
        /// <param name="folder"></param>
        public int ExportImagesTo(string folder)
        {
            int count = 0;
            if (_pictures != null)
            {
                for (int i = 0; i < _pictures.Count; i++)
                {
                    if (_pictures[i].Tag != null)
                    {
                        string path = System.IO.Path.Combine(folder, Guid.NewGuid().ToString() + ".jpg");
                        Ralid.GeneralLibrary.ImageHelper.SaveImage(_pictures[i].Image, path);
                        count++;
                    }
                }
            }
            return count;
        }
        #endregion
    }
}
