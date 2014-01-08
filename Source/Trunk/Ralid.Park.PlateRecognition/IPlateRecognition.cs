using System.ServiceModel;
using System.IO;

namespace Ralid.Park.PlateRecognition
{
    /// <summary>
    /// 车牌识别接口
    /// </summary>
    public interface IPlateRecognition
    {
        /// <summary>
        /// 对通道进行车牌识别,要求此通道有一个用于车牌识别的摄像机
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        PlateRecognitionResult Recognize(int parkID, int entranceID);

        /// <summary>
        /// 通过文件名识别
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        PlateRecognitionResult Recognize(string path);
    }
}
