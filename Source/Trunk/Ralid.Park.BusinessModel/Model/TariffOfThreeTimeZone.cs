using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 三个时段的收费标准
    /// </summary>
    [DataContract]
    [Serializable]
    public class TariffOfThreeTimeZone : TariffBase
    {
        #region 构造函数
        public TariffOfThreeTimeZone()
        {
        }
        #endregion

        #region 公共属性
        ///<summary>
        /// 获取或设置过点是否切换时段
        /// </summary>
        [DataMember]
        public bool SwitchWhenTimeZoneOverTime { get; set; }

        /// <summary>
        /// 获取或设置收费时段1（白天时段）
        /// </summary>
        [DataMember]
        public TariffTimeZone Timezone1 { get; set; }

        /// <summary>
        /// 获取或设置收费时段2（夜晚时段）
        /// </summary>
        [DataMember]
        public TariffTimeZone Timezone2 { get; set; }
        
        /// <summary>
        /// 获取或设置收费时段3（凌晨时段）
        /// </summary>
        [DataMember]
        public TariffTimeZone Timezone3 { get; set; }
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
            double min1 = 0;
            double min2 = 0;
            double min3 = 0;
            decimal fee = 0;
            //交替计算,直到所有时段都计算完
            do
            {
                min1 = Timezone1.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (min1 > 0)
                {
                    fee += Timezone1.CalculateFee(min1);
                    beginning = beginning.AddMinutes(min1);
                }
                min2 = Timezone2.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (min2 > 0)
                {
                    fee += Timezone2.CalculateFee(min2);
                    beginning = beginning.AddMinutes(min2);
                }
                min3 = Timezone3.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (min3 > 0)
                {
                    fee += Timezone3.CalculateFee(min3);
                    beginning = beginning.AddMinutes(min3);
                }
            }
            while (min1 > 0 || min2 > 0 || min3 > 0);
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

        public override string ToString()
        {
            return Resouce.Resource1.Tariff_ThreeTimeZone;
        }
        #endregion
    }
}
