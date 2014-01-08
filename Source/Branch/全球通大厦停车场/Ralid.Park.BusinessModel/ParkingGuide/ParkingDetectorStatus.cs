using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    public enum ParkingDetectorStatus
    {
        /// <summary>
        /// 无车
        /// </summary>
        Empty=0,
        /// <summary>
        /// 有车
        /// </summary>
        Occupied=1,
        /// <summary>
        /// 无响应
        /// </summary>
        NoResponse=2,
        /// <summary>
        /// 所在区域控制器无响应
        /// </summary>
        RegionControllerNoResponse=3
    }
}
