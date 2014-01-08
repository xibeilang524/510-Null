using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Resouce
{
    public class TariffTypeDescription
    {
        /// <summary>
        /// 获取计费类型的文字描述
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public static string GetDescription(TariffType tt)
        {
            switch (tt)
            {
                case TariffType.Normal:
                    return Resource1.TariffType_Normal;
                case TariffType.Holiday:
                    return Resource1.TariffType_Holiday;
                case TariffType.InnerRoom:
                    return Resource1.TariffType_InnerRoom;
                case TariffType.HolidayAndInnerRoom:
                    return Resource1.TariffType_HolidayAndInnerRoom;
                default:
                    return string.Empty;
            }
        }
    }
}
