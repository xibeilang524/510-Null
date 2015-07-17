using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// WaitingCommand命令下发状态
    /// </summary>
    public enum WaitingCommandStatus
    {
        /// <summary>
        /// 等待下发
        /// </summary>
        Waiting = 0,
        /// <summary>
        /// 下发失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 下发成功
        /// </summary>
        Success = 2
    }
}
