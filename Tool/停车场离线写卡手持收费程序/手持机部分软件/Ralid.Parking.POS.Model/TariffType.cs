using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 收费类型
    /// </summary>
    public enum TariffType
    {
        /// <summary>
        /// 普通收费
        /// </summary>
        Normal=0,
        /// <summary>
        /// 节假日
        /// </summary>
        Holiday=1,
        /// <summary>
        /// 室内
        /// </summary>
        InnerRoom=2,
        /// <summary>
        /// 节假日加室内
        /// </summary>
        HolidayAndInnerRoom=3
    }
}
