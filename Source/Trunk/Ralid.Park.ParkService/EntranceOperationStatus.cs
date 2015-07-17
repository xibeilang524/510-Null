using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.ParkService
{
    /// <summary>
    /// 表示控制器操作状态的枚举
    /// </summary>
    public enum EntranceOperationStatus
    {
        /// <summary>
        /// 车走
        /// </summary>
        CarLeave = 1,
        /// <summary>
        /// 车到
        /// </summary>
        CarArrival = 2,
        /// <summary>
        /// 正在出卡(按了取卡按钮)
        /// </summary>
        CardTakeingOut = 3,
        /// <summary>
        /// 车位已满，并且有车辆等待入场
        /// </summary>
        FullAndWait = 4,
        /// <summary>
        /// 已抬闸放行
        /// </summary>
        LiftGate = 5,
    }
}
