using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 收费类型
    /// </summary>
    public enum PaymentMode
    {
        /// <summary>
        /// 现金
        /// </summary>
        Cash=0,
        /// <summary>
        /// 储值卡扣款
        /// </summary>
        Prepay=1,
        /// <summary>
        /// 羊城通
        /// </summary>
        YangChengTong=2,
        /// <summary>
        /// POS收费
        /// </summary>
        Pos=3,
        /// <summary>
        /// 中山通
        /// </summary>
        ZhongShanTong=4,
    }
}
