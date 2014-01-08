using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.BusinessModel.Model
{
    public class SnapShot
    {
        #region 构造函数
        public SnapShot()
        {
        }

        public SnapShot(DateTime shotAt, int videoSourceID, string cardID, string imgPath)
        {
            this.ShotAt = shotAt;
            this.VideoSourceID = videoSourceID;
            this.CardID = cardID;
            try
            {
                this._ImageData = GetBytesFromPhoto(imgPath);
                this._Img = Image.FromFile(imgPath);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion
        #region 私有变量
        private byte[] _ImageData;
        private Image _Img;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置抓拍时间
        /// </summary>
        public DateTime ShotAt { get; set; }
        /// <summary>
        /// 获取或设置抓拍摄像机ID
        /// </summary>
        public int VideoSourceID { get; set; }
        /// <summary>
        /// 获取或设置抓拍时的卡片号码
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置抓拍的图片
        /// </summary>
        public Image Image
        {
            get
            {
                if (_Img == null)
                {
                    if (_ImageData != null)
                    {
                        _Img = GetImageFromBytes(_ImageData);
                    }
                }
                return _Img;
            }
        }
        #endregion

        #region 私有方法
        private Image GetImageFromBytes(byte[] photo)
        {
            try
            {
                if (photo != null)
                {
                    string path = Path.Combine(TempFolderManager.GetCurrentFolder(),
                                        string.Format("{0}_{1}.jpg", "History", Guid.NewGuid().ToString()));
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(photo, 0, photo.Length);
                    }
                    Image img = Image.FromFile(path);
                    return img;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private byte[] GetBytesFromPhoto(string path)
        {
            byte[] bs = null;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bs = new byte[fs.Length];
                fs.Position = 0;
                fs.Read(bs, 0, (int)fs.Length);
            }
            return bs;
        }
        #endregion
    }
}
