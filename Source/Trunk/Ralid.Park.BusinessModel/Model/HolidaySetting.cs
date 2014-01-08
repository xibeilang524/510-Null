using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Model
{
    [DataContract()]
    public class HolidaySetting
    {
        /// <summary>
        /// 获取或设置系统当前的节假日选项
        /// </summary>
        public static HolidaySetting Current { get; set; }

        [DataMember]
        private List<HolidayInfo> _Holidays = new List<HolidayInfo>();

        /// <summary>
        ///获取或设置星期六是否为休息日
        /// </summary>
        [DataMember]
        public bool SaturdayIsRest { get; set; }

        /// <summary>
        /// 获取或设置星期日是否为休息日
        /// </summary>
        [DataMember]
        public bool SundayIsRest { get; set; }

        /// <summary>
        /// 获取节假日列表
        /// </summary>
        public List<HolidayInfo> Holidays
        {
            get
            {
                return _Holidays;
            }
        }
        /// <summary>
        /// 判断日期是否节假日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool IsHoliday(DateTime dt)
        {
            bool ret = false;
            ////如果周六或者周日是节假日，则在调整的节假日转工作日中查找，否则在节假日列表中找
            if ((dt.DayOfWeek == DayOfWeek.Saturday && SaturdayIsRest) ||
                (dt.DayOfWeek == DayOfWeek.Sunday && SundayIsRest))
            {
                ret = true;
                foreach (HolidayInfo holiday in _Holidays)
                {
                    if (holiday.WeekenToWorkDayInterval != null && holiday.WeekenToWorkDayInterval.Count > 0)
                    {
                        foreach (DatetimeInterval di in holiday.WeekenToWorkDayInterval)
                        {
                            if(di.IsInDatetimeInterval(dt))
                            {
                                ret=false;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (HolidayInfo holiday in _Holidays)
                {
                    if (dt.Date >= holiday.StartDate && dt.Date <= holiday.EndDate)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }
    }
}
