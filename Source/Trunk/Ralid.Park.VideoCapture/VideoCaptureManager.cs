using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.VideoCapture
{
    /// <summary>
    /// 用于管理摄像机抓拍实例
    /// </summary>
    public class VideoCaptureManager
    {
        #region 静态方法和属性
        private static VideoCaptureManager _instance;
        public static VideoCaptureManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new VideoCaptureManager();
                }
                return _instance;
            }
        }
        #endregion


        #region 构造函数
        private VideoCaptureManager()
        {
            _videoCaptures = new Dictionary<int, IVideoCapture>();
        }
        #endregion

        #region 私有变量
        private Dictionary<int, IVideoCapture> _videoCaptures;
        private object _ListLock = new object();
        #endregion

        #region 公共方法
        /// <summary>
        /// 增加一个摄像机抓拍实例
        /// </summary>
        /// <param name="videoType">摄像机类型</param>
        /// <param name="capture">摄像机抓拍实例</param>
        public void Add(int videoType, IVideoCapture capture)
        {
            lock (_ListLock)
            {
                _videoCaptures.Add(videoType, capture);
            }
        }

        /// <summary>
        /// 获取摄像机抓拍实例
        /// </summary>
        /// <param name="videoType">摄像机类型</param>
        /// <returns></returns>
        public IVideoCapture this[int videoType]
        {
            get
            {
                lock (_ListLock)
                {
                    if (_videoCaptures.ContainsKey(videoType))
                    {
                        return _videoCaptures[videoType];
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取所有的摄像机抓拍实例
        /// </summary>
        public IVideoCapture[] VideoCaptures
        {
            get
            {
                lock (_ListLock)
                {
                    return _videoCaptures.Values.ToArray();
                }
            }
        }

        #endregion
    }
}
