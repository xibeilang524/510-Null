using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 控制器的卡片类型属性枚举
    /// </summary>
    [Flags]
    public enum EntranceCardTypeProperty
    {
        /// <summary>
        /// 入口车牌不写卡，默认；
        /// </summary>
        EnterNotWriteCarPlate = 0x01,

        /// <summary>
        /// 不对比车牌，默认；
        /// </summary>
        NotCompareCarPlate = 0x02,

        /// <summary>
        /// 车牌对比失败时抬闸，默认；
        /// </summary>
        CompareFailOpenGate = 0x04,

        /// <summary>
        /// =1操作卡内容(按写卡方式处理)，默认；=0读取序列号（按序列号方式处理，相当于在韦根读卡器刷卡，不判断进出场状态，直接放行）。(脱机模式使用)
        /// </summary>
        WriteCardHandle=0x08,

        /// <summary>
        /// 允许在韦根读卡器刷卡，默认；
        /// （临时卡、储值卡默认不允许在韦根读卡器刷卡，其余卡类型默认允许在韦根读卡器刷卡。）
        /// </summary>
        EnabledWiegandReader=0x10,

        /// <summary>
        /// 默认，默认全为1
        /// </summary>
        Default=0xFFFF
    }
}
