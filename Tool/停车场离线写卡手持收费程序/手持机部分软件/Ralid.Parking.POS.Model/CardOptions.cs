using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 卡片选项
    /// </summary>
    [Flags]
    public enum CardOptions
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None = 0,
        /// <summary>
        /// 脱机模式时按脱机模式处理
        /// </summary>
        OfflineHandleWhenOfflineMode = 1,

        Reserved=2,
        /// <summary>
        /// 禁止重复入场
        /// </summary>
        ForbidRepeatIn = 4,
        /// <summary>
        /// 禁止重复出场
        /// </summary>
        ForbidRepeatOut = 8,
        /// <summary>
        /// 出入场时参加车位计数
        /// </summary>
        WithCount = 16,
        /// <summary>
        /// 车场满位时禁止入场
        /// </summary>
        ForbidWhenFull = 32,
        /// <summary>
        /// 禁止进出内嵌套车场
        /// </summary>
        ForbidNestPark = 64,
        /// <summary>
        /// 卡片过期时禁止进出场
        /// </summary>
        ForbidWhenExpired = 128,
        /// <summary>
        /// 节假日有效
        /// </summary>
        HolidayEnable = 256,
    }
}
