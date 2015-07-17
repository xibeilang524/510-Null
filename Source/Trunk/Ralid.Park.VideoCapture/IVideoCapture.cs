using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.VideoCapture
{
    /// <summary>
    /// 摄像机抓拍接口
    /// </summary>
    public interface IVideoCapture
    {
        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <param name="info">摄像机</param>
        /// <param name="force">是否强制重新抓拍，不管之前有没有抓拍到图片</param>
        /// <returns>抓拍图片的地址，返回空时表示抓拍失败</returns>
        string CapturePicture(VideoSourceInfo info, bool force);
        /// <summary>
        /// 清除抓拍图片信息
        /// </summary>
        /// <param name="info">摄像机</param>
        void ClearCapture(VideoSourceInfo info);
    }
}
