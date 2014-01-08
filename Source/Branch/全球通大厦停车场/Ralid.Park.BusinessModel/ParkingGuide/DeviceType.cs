using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    /// 指定车位引导系统中的设备类型
    /// </summary>
    public enum  DeviceType
    {
        /// <summary>
        /// 中央控制器
        /// </summary>
        CentralControl=0,
        /// <summary>
        /// 区域控制器
        /// </summary>
        RegionController=1,
        /// <summary>
        /// 车位探测器
        /// </summary>
        ParkingDetector=2,
        /// <summary>
        /// 显示屏
        /// </summary>
        Sreen=3
    }
}
