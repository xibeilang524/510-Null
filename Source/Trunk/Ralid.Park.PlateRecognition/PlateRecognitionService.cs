using System.ServiceModel ;
using System.Threading;
using System;
using System.Collections.Generic;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.PlateRecognition
{
    /// <summary>
    /// 车牌识别的服务端
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PlateRecognitionService : IPlateRecognition
    {
        #region 静态属性
        private static PlateRecognitionService _Instance;
        /// <summary>
        /// 获取当前的车牌识别服务实例
        /// </summary>
        public static PlateRecognitionService CurrentInstance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PlateRecognitionService();
                }
                return _Instance;
            }
        }
        #endregion

        #region 私有变量
        //private IPlateRecognition _plnImp;
        /// <summary>
        /// 车牌识别实例集合
        /// </summary>
        private Dictionary<int, IPlateRecognition> _plnImps = new Dictionary<int, IPlateRecognition>();
        #endregion

        #region 构造函数
        public PlateRecognitionService()
        {
        }

        public PlateRecognitionService(IPlateRecognition recognition)
        {
            //_plnImp = recognition;
        }
        #endregion


        #region 公共方法
        /// <summary>
        /// 添加车牌识别实例
        /// </summary>
        /// <param name="recognitionType">车牌识别类型</param>
        /// <param name="plnImp">实例</param>
        public void Add(int recognitionType, IPlateRecognition plnImp)
        {
            if (_plnImps.ContainsKey(recognitionType))
            {
                _plnImps[recognitionType] = plnImp;
            }
            else
            {
                _plnImps.Add(recognitionType, plnImp);
            }
        }
        #endregion

        #region IPlateRecognition接口实现
        public PlateRecognitionResult Recognize(int parkID, int entranceID)
        {
            //if (_plnImp != null)
            //{
            //    return _plnImp.Recognize(parkID, entranceID);
            //}

            IPlateRecognition plnImp = null;
            //默认车牌识别类型
            CarPlateRecognizationType reconizationType = AppSettings.CurrentSetting.CarPlateRecognization;

            try
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(parkID, entranceID);
                if (entrance != null)
                {
                    //如果是使用信路通识别的
                    if (entrance.CarPlateIP != "0.0.0.0"
                        && !string.IsNullOrEmpty(entrance.CarPlateIP))
                    {
                        reconizationType = CarPlateRecognizationType.XinLuTong;
                    }
                    else
                    {
                        foreach (VideoSourceInfo video in entrance.VideoSources)
                        {
                            if (video.IsForCarPlate)
                            {
                                //使用大华摄像机的
                                if (video.VideoSourceType == (int)VideoServerType.DaHua)
                                {
                                    reconizationType = CarPlateRecognizationType.DaHua;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }

            if (_plnImps.ContainsKey((int)reconizationType))
            {
                plnImp = _plnImps[(int)reconizationType];
            }

            if (plnImp != null)
            {
                return plnImp.Recognize(parkID, entranceID);
            }

            return new PlateRecognitionResult();
        }


        public PlateRecognitionResult Recognize(string path)
        {
            //if (_plnImp != null)
            //{
            //    return _plnImp.Recognize(path);
            //}

            IPlateRecognition plnImp = null;

            //只有文通和亚视才能图片识别车牌
            if (_plnImps.ContainsKey((int)CarPlateRecognizationType.WINTONE))
            {
                plnImp = _plnImps[(int)CarPlateRecognizationType.WINTONE];
            }
            else if (_plnImps.ContainsKey((int)CarPlateRecognizationType.VECON))
            {
                plnImp = _plnImps[(int)CarPlateRecognizationType.VECON];
            }

            if (plnImp != null)
            {
                return plnImp.Recognize(path);
            }

            return new PlateRecognitionResult();
        }
        #endregion
    }
}
