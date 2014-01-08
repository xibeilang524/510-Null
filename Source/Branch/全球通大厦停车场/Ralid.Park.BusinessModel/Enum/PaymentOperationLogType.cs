using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 表示APM操作日志的类型
    /// </summary>
    public enum APMLogType
    {
        /// <summary>
        /// 信息
        /// </summary>
        Message=0,
        /// <summary>
        /// 警告
        /// </summary>
        Alarm=1,
        /// <summary>
        /// 错误
        /// </summary>
        Error=2
    }
}
