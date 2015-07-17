using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 表示产生读卡事件的读头的枚举
    /// </summary>
    public enum EntranceReader
    {
        /// <summary>
        /// 读卡器一，网络版控制器时为月租卡读头，CAN总线控制器时为临时卡读头
        /// </summary>
        Reader1 = 0,
        /// <summary>
        /// 读卡器二，网络版控制器时为远距离卡读头，CAN总线控制器时为非临时卡读头
        /// </summary>
        Reader2 = 1,
        /// <summary>
        /// 读卡器三，网络版控制器时为临时卡读头
        /// </summary>
        Reader3 = 2,
        ///// <summary>
        ///// 读卡器四
        ///// </summary>
        //Reader4 = 3,
        ///// <summary>
        ///// 读卡器五
        ///// </summary>
        //Reader5=4,
        /// <summary>
        /// 地址1读卡器（非临时卡读头）
        /// </summary>
        Address1Reader = 3,
        /// <summary>
        /// 地址2读卡器（非临时卡读头）
        /// </summary>
        Address2Reader = 4,
        /// <summary>
        /// 地址3读卡器（非临时卡读头）
        /// </summary>
        Address3Reader = 5,
        /// <summary>
        /// 地址4读卡器（临时卡读头）
        /// </summary>
        Address4Reader = 6,
        /// <summary>
        /// 地址5读卡器（临时卡读头）
        /// </summary>
        Address5Reader = 7,
        /// <summary>
        /// 地址6读卡器（临时卡读头）
        /// </summary>
        Address6Reader = 8,
        /// <summary>
        /// 桌面读卡器
        /// </summary>
        DeskTopReader = 10
    }
}