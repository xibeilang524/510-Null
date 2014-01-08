using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 数据库连接状态
    /// </summary>
    public enum DataBaseConnectionStatus
    {
        /// <summary>
        /// 未连接
        /// </summary>
        Unconnected = 0,
        /// <summary>
        /// 已连接
        /// </summary>
        Connected = 1,
        /// <summary>
        /// 连接断开
        /// </summary>
        Disconnect = 2
    }
}
