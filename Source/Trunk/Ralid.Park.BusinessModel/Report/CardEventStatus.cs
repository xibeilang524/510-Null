using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 卡片事件状态
    /// </summary>
    public enum CardEventStatus
    {
        /// <summary>
        /// 等待处理
        /// </summary>
        Pending = 0,
        /// <summary>
        /// 车牌对比失败
        /// </summary>
        CarPlateFail = 2,
        /// <summary>
        /// 事件有效
        /// </summary>
        Valid = 1,
    }
}
