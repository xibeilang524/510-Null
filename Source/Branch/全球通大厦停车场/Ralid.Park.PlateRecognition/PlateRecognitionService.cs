using System.ServiceModel ;
using System.Threading;

namespace Ralid.Park.PlateRecognition
{
    /// <summary>
    /// 车牌识别的服务端
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PlateRecognitionService : IPlateRecognition
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置当前的车牌识别服务实例
        /// </summary>
        public static  PlateRecognitionService CurrentInstance { get; set; }
        #endregion

        private IPlateRecognition _plnImp;

        public PlateRecognitionService()
        {
        }

        public PlateRecognitionService(IPlateRecognition recognition)
        {
            _plnImp = recognition;
        }

        public PlateRecognitionResult Recognize(int parkID, int entranceID)
        {
            if (_plnImp != null)
            {
                return _plnImp.Recognize(parkID, entranceID);
            }
            return new PlateRecognitionResult();
        }


        public PlateRecognitionResult Recognize(string path)
        {
            if (_plnImp != null)
            {
                return _plnImp.Recognize(path);
            }
            return new PlateRecognitionResult();
        }
    }
}
