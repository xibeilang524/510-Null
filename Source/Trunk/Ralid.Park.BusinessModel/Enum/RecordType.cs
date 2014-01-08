using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 记录类型
    /// </summary>
    public enum RecordType
    {
        /// <summary>
        /// 卡片收费记录
        /// </summary>
        CardPaymentRecord=0x00,

        /// <summary>
        /// 卡片充值记录
        /// </summary>
        CardChargeRecord=0x01,

        /// <summary>
        /// 未知记录
        /// </summary>
        Unknow=0xFF
    }
}
