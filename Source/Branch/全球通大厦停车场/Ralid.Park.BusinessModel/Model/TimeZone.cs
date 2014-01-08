using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示一个时间段
    /// </summary>
    [DataContract]
    public class TimeZone
    {
        #region 构造函数
        public TimeZone()
        {
        }

        public TimeZone(TimeEntity beginning, TimeEntity ending)
        {
            Beginning = beginning;
            Ending = ending;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 从连续时间段开始处获取属于本时段的分钟数,直到时段结束,
        /// 连续时间段内可能有多个时段交替出现,所以在另一个时段开始时停止获取.
        /// </summary>
        /// <param name="beginning">连续时间段的起始时间</param>
        /// <param name="ending">连续时间段的终止时间</param>
        private double SliceDuration(DateTime beginning, DateTime ending)
        {
            double minutes = 0;

            DateTime timezoneBegin = new DateTime();
            DateTime timezoneEnd = new DateTime();
            //以开始时间为参照,建立收费时段的当天参照时间
            if (IsOverDay) //时段跨天
            {
                if (beginning.Hour >= Beginning.Hour)  //
                {
                    timezoneBegin = new DateTime(beginning.Year, beginning.Month, beginning.Day, Beginning.Hour, Beginning.Minute, 0);
                    timezoneEnd = new DateTime(beginning.Year, beginning.Month, beginning.Day, Ending.Hour, Ending.Minute, 0).AddDays(1);
                }
                else if (beginning.Hour <= Ending.Hour)
                {
                    timezoneBegin = new DateTime(beginning.Year, beginning.Month, beginning.Day, Beginning.Hour, Beginning.Minute, 0).AddDays(-1);
                    timezoneEnd = new DateTime(beginning.Year, beginning.Month, beginning.Day, Ending.Hour, Ending.Minute, 0);
                }
            }
            else
            {
                timezoneBegin = new DateTime(beginning.Year, beginning.Month, beginning.Day, Beginning.Hour, Beginning.Minute, 0);
                timezoneEnd = new DateTime(beginning.Year, beginning.Month, beginning.Day, Ending.Hour, Ending.Minute, 0);
            }
            //计算重合字段,开始时间,两个开始时间两者取大,两个结束时间两者取小
            if (timezoneBegin < beginning) timezoneBegin = beginning;
            if (timezoneEnd > ending) timezoneEnd = ending;

            if (timezoneEnd > timezoneBegin)
            {
                TimeSpan ts = new TimeSpan(timezoneEnd.Ticks - timezoneBegin.Ticks);
                minutes = Math.Ceiling(ts.TotalMinutes);
            }
            return minutes;
        }

        #endregion

        #region 公共属性
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
        /// 获取时段是否跨天(结束时间的小时数小于开始时间的小时数)
        /// </summary>
        public bool IsOverDay
        {
            get
            {
                return Ending.Hour < Beginning.Hour;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取从开始时间和结束时间之间以(如30分钟为单位)位于此时段内的分钟数
        /// </summary>
        /// <param name="beginning">开始时间</param>
        /// <param name="ending">结束时间</param>
        /// <param name="unit">单位分钟数</param>
        /// <param name="overlap">不足单位分钟数的部分是否跨时段补齐</param>
        /// <returns></returns>
        public double Slice(DateTime beginning, DateTime ending, double unit, bool overlap)
        {
            if (beginning >= ending) return 0;
            double minutes = 0;
            if (overlap)
            {
                while (IsIn(beginning))
                {
                    if (beginning.AddMinutes(unit) > ending)
                    {
                        TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
                        minutes += ts.TotalMinutes;
                        beginning = ending;
                        break;
                    }
                    else
                    {
                        minutes += unit;
                        beginning = beginning.AddMinutes(unit);
                    }
                }
                return minutes;
            }
            else
            {
                return SliceDuration(beginning, ending);
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
        /// 获取时间段的总分钟数
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

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (obj == null) return false;
            TimeZone tz = obj as TimeZone;
            if (tz != null)
            {
                if (
                    (this.Beginning != null && tz.Beginning != null && this.Beginning.Hour == tz.Beginning.Hour && this.Beginning.Minute == tz.Beginning.Minute) &&
                    (this.Ending != null && tz.Ending != null && this.Ending.Hour == tz.Ending.Hour && this.Ending.Minute == this.Ending.Minute))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
