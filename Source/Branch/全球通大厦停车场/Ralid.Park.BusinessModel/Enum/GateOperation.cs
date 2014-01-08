using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 道闸动作枚举
    /// </summary>
    public enum GateOperation
    {
        /// <summary>
        /// 停闸
        /// </summary>
        Stop = 0x00,
        /// <summary>
        /// 抬闸
        /// </summary>
        Open = 0x01,
        /// <summary>
        /// 落闸
        /// </summary>
        Close = 0x02,
    }
}
