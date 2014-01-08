using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 表示通道控制板类型的枚举
    /// </summary>
    public enum EntranceDeviceType
    {
        /// <summary>
        /// CAN总线停车场控制板
        /// </summary>
        CANEntrance = 0,
        /// <summary>
        /// 网络型停车场控制板
        /// </summary>
        NETEntrance = 1
    }
}
