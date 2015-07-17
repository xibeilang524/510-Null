using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 描述标准的收费时段
    /// </summary>
    [DataContract]
    public class TariffTimeZone
    {
        /// <summary>
        /// 获取或设置时段开始时间
        /// </summary>
        [DataMember]
        public TimeEntity Beginning { get; set; }

        /// <summary>
        /// 获取或设置时段结束时间
        /// </summary>
        [DataMember]
        public TimeEntity Ending { get; set; }

        /// <summary>
        /// 获取或设置时段正常收费
        /// </summary>
        [DataMember]
        public ChargeUnit RegularCharge { get; set; }

        /// <summary>
        /// 获取或设置时段最高限价(如果有的话)
        /// </summary>
        [DataMember]
        public Decimal? LimiteFee { get; set; }
        
        public TariffTimeZone()
        {
        }

        public TariffTimeZone(TimeEntity beginning, TimeEntity ending, ChargeUnit regularCharge)
        {
            Beginning = beginning;
            Ending = ending;
            RegularCharge = regularCharge;
        }

        /// <summary>
        /// 从连续时间段开始处获取属于本时段的分钟数,直到时段结束,
        /// 连续时间段内可能有多个时段交替出现,所以在另一个时段开始时停止获取.
        /// </summary>
        /// <param name="beginning">连续时间段的起始时间</param>
        /// <param name="ending">连续时间段的终止时间</param>
        public double SliceDuration(DateTime beginning, DateTime ending, bool switchWhenTimeZoneOverTime)
        {
            double minutes = 0;

            DateTime timezoneBegin=new DateTime ();
            DateTime timezoneEnd = new DateTime();
            //以开始时间为参照,建立收费时段的当天参照时间
            if (IsOverDay) //时段跨天
            {
                if (beginning.Hour >= Beginning.Hour)  //
                {
                    timezoneBegin = new DateTime(beginning.Year, beginning.Month, beginning.Day, Beginning.Hour, Beginning.Minute, 0);
                    timezoneEnd = new DateTime(beginning.Year, beginning.Month, beginning.Day, Ending.Hour, Ending.Minute, 0).AddDays(1);
                }
                else if (beginning .Hour <= Ending .Hour )
                {
                    timezoneBegin = new DateTime(beginning.Year, beginning.Month, beginning.Day, Beginning.Hour, Beginning.Minute, 0).AddDays(-1);
                    timezoneEnd = new DateTime(beginning.Year, beginning.Month, beginning.Day, Ending.Hour, Ending.Minute, 0);
                }
            }
            else
            {
                timezoneBegin=new DateTime(beginning.Year, beginning.Month, beginning.Day, Beginning.Hour, Beginning.Minute, 0);
                timezoneEnd = new DateTime(beginning.Year, beginning.Month, beginning.Day, Ending.Hour, Ending.Minute, 0);
            }
            if (timezoneBegin <= beginning && timezoneEnd >= beginning)  //开始时间落在范围内
            {
                if (timezoneEnd >= ending)
                {
                    TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
                    minutes = Math.Ceiling(ts.TotalMinutes);
                }
                else
                {
                    TimeSpan ts = new TimeSpan(timezoneEnd.Ticks - beginning.Ticks);
                    minutes = Math.Ceiling(ts.TotalMinutes);
                }
            }
            return minutes;
        }

        public decimal CalculateFee(double minutes)
        {
            decimal fee = 0;
            if (minutes > 0)
            {
                if (RegularCharge != null)  //只有正常收费
                {
                    int count = (int)Math.Ceiling(minutes / RegularCharge.Minutes);
                    fee = count * RegularCharge.Fee;
                }
            }
            if (LimiteFee != null && LimiteFee.Value > 0)  //如果时段有最高收费
            {
                fee = (LimiteFee.Value < fee) ? LimiteFee.Value : fee;
            }
            return fee;
        }

        /// <summary>
        /// 不考虑时段最高收费计算费用
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public decimal CalculateFeeWithOutLimiteFee(double minutes)
        {
            decimal fee = 0;
            if (minutes > 0)
            {
                if (RegularCharge != null)  //只有正常收费
                {
                    int count = (int)Math.Ceiling(minutes / RegularCharge.Minutes);
                    fee = count * RegularCharge.Fee;
                }
            }
            return fee;
        }

        /// <summary>
        /// 获取时段是否跨天(结束时间的小时数小于开始时间的小时数)
        /// </summary>
        public bool IsOverDay
        {
            get
            {
                return Ending.Hour < Beginning.Hour;
            }
        }

        /// <summary>
        /// 时间是否在时间段内
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool IsIn(DateTime dt)
        {
            if (IsOverDay)
            {
                return Beginning.TotalMinutes <= (dt.Minute + dt.Hour * 60) || Ending.TotalMinutes >= (dt.Minute + dt.Hour * 60);
            }
            else
            {
                return Beginning.TotalMinutes <= (dt.Minute + dt.Hour * 60) && (dt.Minute + dt.Hour * 60) <= Ending.TotalMinutes;
            }
        }

        /// <summary>
        /// 获取收费时间段的总分钟数
        /// </summary>
        /// <returns></returns>
        public double TimeZoneTotalMinutes()
        {
            double minutes = 0;
            if (IsOverDay)
            {
                minutes = 24 * 60 - Beginning.TotalMinutes + Ending.TotalMinutes;
            }
            else
            {
                minutes = Ending.TotalMinutes - Beginning.TotalMinutes;
            }
            return minutes;
        }
    }
}
