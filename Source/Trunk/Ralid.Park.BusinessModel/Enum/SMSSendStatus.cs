using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 短信发送状态
    /// </summary>
    public enum SMSSendStatus
    {
        /// <summary>
        /// 不发送
        /// </summary>
        NotSend = 0,
        /// <summary>
        /// 等待发送
        /// </summary>
        Waiting = 1,
        /// <summary>
        /// 发送成功
        /// </summary>
        Success = 2,
        /// <summary>
        /// 发送失败
        /// </summary>
        Fail = 3
    }
}
