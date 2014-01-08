using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 控制器状态
    /// </summary>
    public enum EntranceStatus
    {
        /// <summary>
        /// 设备正常
        /// </summary>
        Ok = 0,
        /// <summary>
        /// 故障
        /// </summary>
        Error = 0x01,
        /// <summary>
        /// 存储空间满
        /// </summary>
        StorageFull = 0x02,
        /// <summary>
        /// 存储空间达到状态
        /// </summary>
        StorageAlarm = 0x04,
        /// <summary>
        /// 发卡机缺卡/打印机缺纸
        /// </summary>
        NoCard = 0x08,
        /// <summary>
        /// 卡数量少
        /// </summary>
        LessCard = 0x10,
        /// <summary>
        /// 卡纸/塞卡
        /// </summary>
        CardJam = 0x20,
        /// <summary>
        /// 落闸到位
        /// </summary>
        GateDown=0x2000,
        /// <summary>
        /// 抬闸到位
        /// </summary>
        GateUp = 0x4000,
        /// <summary>
        /// 不在线
        /// </summary>
        OffLine = 0x8000,
        /// <summary>
        /// 未知状态
        /// </summary>
        UnKnown=0x10000,
    }
}
