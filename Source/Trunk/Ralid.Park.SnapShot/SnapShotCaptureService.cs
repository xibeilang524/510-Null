using System.ServiceModel;

namespace Ralid.Park.SnapShotCapture
{
    /// <summary>
    /// 快照抓拍的服务端
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SnapShotCaptureService : ISnapShotCapture
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置当前的快照抓拍服务实例
        /// </summary>
        public static SnapShotCaptureService CurrentInstance { get; set; }
        #endregion

        #region 私有变量
        private ISnapShotCapture _ssImp;
        #endregion

        #region 构造函数
        public SnapShotCaptureService()
        { 
        }
        public SnapShotCaptureService(ISnapShotCapture snapshot)
        {
            _ssImp = snapshot;
        }
        #endregion

        #region ISnapShot接口实现
        /// <summary>
        /// 对通道进行图片抓拍,要求此通道有一个用于车牌识别的摄像机
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="entranceID"></param>
        /// <returns>抓拍图片的地址，返回空时表示抓拍失败</returns>
        public string SnapShot(int parkID, int entranceID)
        {
            if (_ssImp != null)
            {
                return _ssImp.SnapShot(parkID, entranceID);
            }
            return string.Empty;
        }
        #endregion
    }
}
