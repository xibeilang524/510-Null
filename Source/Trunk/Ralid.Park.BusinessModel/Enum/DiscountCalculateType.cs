using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    //add by Jan 2012-3-29
    public enum DiscountCalculateType
    {
        /// <summary>
        /// 按小时计算计算方式
        /// </summary>
        Hour = 0,


        /// <summary>
        /// 折合成优惠劵计算方式
        /// </summary>
        Coupon = 1,

        /// <summary>
        /// 按累计差额计算方式
        /// </summary>
        Balance = 2
    }
    //end
}
