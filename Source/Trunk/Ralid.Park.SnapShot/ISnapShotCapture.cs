using System.ServiceModel;
using System.IO;

namespace Ralid.Park.SnapShotCapture
{
    /// <summary>
    /// 快照抓拍接口
    /// </summary>
    public interface ISnapShotCapture
    {
        /// <summary>
        /// 对通道进行图片抓拍,要求此通道有一个用于车牌识别的摄像机
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="entranceID"></param>
        /// <returns>抓拍图片的地址，返回空时表示抓拍失败</returns>
        string SnapShot(int parkID, int entranceID);
    }
}
