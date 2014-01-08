using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 卡片选项
    /// </summary>
    [Flags]
    [DataContract]
    public enum CardOptions
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// 脱机模式时按脱机模式处理
        /// </summary>
        [EnumMember]
        OfflineHandleWhenOfflineMode = 1,

        [EnumMember]
        Reserved=2,
        /// <summary>
        /// 禁止重复入场
        /// </summary>
        [EnumMember]
        ForbidRepeatIn = 4,
        /// <summary>
        /// 禁止重复出场
        /// </summary>
        [EnumMember]
        ForbidRepeatOut = 8,
        /// <summary>
        /// 出入场时参加车位计数
        /// </summary>
        [EnumMember]
        WithCount = 16,
        /// <summary>
        /// 车场满位时禁止入场
        /// </summary>
        [EnumMember]
        ForbidWhenFull = 32,
        /// <summary>
        /// 禁止进出内嵌套车场
        /// </summary>
        [EnumMember]
        ForbidNestPark = 64,
        /// <summary>
        /// 卡片过期时禁止进出场
        /// </summary>
        [EnumMember]
        ForbidWhenExpired = 128,
        /// <summary>
        /// 节假日有效
        /// </summary>
        [EnumMember]
        HolidayEnable = 256,
    }
}
