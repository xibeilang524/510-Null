using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 表示收费代码
    /// </summary>
    public enum PaymentCode
    {
        /// <summary>
        /// 缴费机收费
        /// </summary>
        APM=0xA1,

        /// <summary>
        /// 电脑收费
        /// </summary>
        Computer = 0xB1,

        /// <summary>
        /// 功能卡收费
        /// </summary>
        FunctionCard = 0xB2
    }
}
