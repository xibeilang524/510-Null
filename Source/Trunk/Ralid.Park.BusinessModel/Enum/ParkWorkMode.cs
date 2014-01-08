using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 工作模式
    /// </summary>
    public enum ParkWorkMode
    {
        /// <summary>
        /// 脱机模式（网络型停车场为写卡模式）
        /// </summary>
        OffLine = 0,
        /// <summary>
        /// 实时模式（傻瓜模式，逻辑处理交由上位机处理，本身只传递产生的事件)
        /// </summary>
        Fool = 1,
    }
}
