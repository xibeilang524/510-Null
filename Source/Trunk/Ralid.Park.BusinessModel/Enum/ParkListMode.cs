using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 停车场进出凭证名单模式枚举
    /// </summary>
    public enum ParkListMode
    {
        /// <summary>
        /// 卡片名单模式
        /// </summary>
        Card = 0x00,
        /// <summary>
        /// 车牌名单模式
        /// </summary>
        CarPlate = 0x01,
        /// <summary>
        /// 车牌名单+卡片名单模式（车牌名单优先）
        /// </summary>
        CarPlateAndCard = 0x02
    }
}
