using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    ///车位探测器延时等级
    /// </summary>
    public enum DetectInterval
    {
        /// <summary>
        /// 0秒
        /// </summary>
        Zero = 0,
        /// <summary>
        /// 1秒
        /// </summary>
        One = 1,
        /// <summary>
        /// 2秒
        /// </summary>
        Two = 2,
        /// <summary>
        /// 3秒
        /// </summary>
        Three = 3,
        /// <summary>
        /// 4秒
        /// </summary>
        Four = 4,
        /// <summary>
        /// 6秒
        /// </summary>
        Six = 5,
        /// <summary>
        /// 8秒
        /// </summary>
        Eight = 6,
        /// <summary>
        /// 16秒
        /// </summary>
        Sixteen = 7
    }
}
