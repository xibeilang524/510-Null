using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 广州分时段收费标准,两个时段,加一个特殊时段
    /// </summary>
    [Serializable]
    public class TariffOfGuanZhou : TariffBase
    {
        public TariffOfGuanZhou()
        {

        }

        #region 公共方法和属性
        ///<summary>
        /// 获取或设置过点是否切换时段
        /// </summary>
        
        public bool SwitchWhenTimeZoneOverTime { get; set; }

        /// <summary>
        /// 获取或设置白天收费时段
        /// </summary>
        
        public TariffTimeZone DayTimezone { get; set; }

        /// <summary>
        /// 获取或设置晚上收费时段
        /// </summary>
        
        public TariffTimeZone NightTimezone { get; set; }
        #endregion

        #region 私有方法
        /// <summary>
        /// 在连续时间段内计算各时段交替收费的总费用
        /// </summary>
        /// <param name="beginning"></param>
        /// <param name="ending"></param>
        /// <returns></returns>
        private decimal CalculateFeeAlteration(DateTime beginning, DateTime ending)
        {
            double minDay = 0;
            double minNight = 0;
            decimal fee = 0;
            //交替计算,直到所有时段都计算完
            do
            {
                minDay = DayTimezone.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (minDay > 0)
                {
                    fee += DayTimezone.CalculateFee(minDay);
                    beginning = beginning.AddMinutes(minDay);
                }
                minNight = NightTimezone.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (minNight > 0)
                {
                    fee += NightTimezone.CalculateFee(minNight);
                    beginning = beginning.AddMinutes(minNight);
                }
            }
            while (minDay > 0 || minNight > 0);
            return fee;
        }
        #endregion

        #region 重写基类方法
        public override decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            if (FeeOf24Hour > 0)  //每24小时有限额收费
            {
                int days = (int)Math.Floor(ts.TotalDays);   //收费都是按24小时为一周期,所以先将整24小时的费用收取完
                if (days > 0)
                {
                    fee += FeeOf24Hour * days;
                }

                DateTime begin2 = beginning.AddDays(days);  //计算不足24小时的收费部分
                decimal tail = CalculateFeeAlteration(begin2, ending);
                fee += (tail > FeeOf24Hour) ? FeeOf24Hour : tail;
            }
            else   //无24小时限额收费,则各时间费用累加
            {
                fee = CalculateFeeAlteration(beginning, ending);
            }
            return fee;
        }
        #endregion
    }
}
