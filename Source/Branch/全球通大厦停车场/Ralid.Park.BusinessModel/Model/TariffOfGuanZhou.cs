using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 广州分时段收费标准,两个时段,加一个特殊时段
    /// </summary>
    [DataContract]
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
        [DataMember]
        public bool SwitchWhenTimeZoneOverTime { get; set; }

        /// <summary>
        /// 获取或设置白天收费时段
        /// </summary>
        [DataMember]
        public TariffTimeZone DayTimezone { get; set; }

        /// <summary>
        /// 获取或设置晚上收费时段
        /// </summary>
        [DataMember]
        public TariffTimeZone NightTimezone { get; set; }

        //public override decimal CalculateFee(DateTime beginning, DateTime ending)
        //{
        //    decimal fee = 0;
        //    decimal daysfee = 0;
        //    double calMins = 0;//已计费的分钟数
        //    TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
        //    if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
        //    if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
        //    if (FreeMinutes == 0 && ts.TotalMinutes == 0) return GetChargeUnitFee(beginning);//返回收费单元的费用

        //    DateTime begin = beginning;
        //    DateTime end = begin.AddDays(1);

        //    TariffBase tariff = this;

        //    //收费都是按24小时为一周期
        //    while (end <= ending)
        //    {
        //        daysfee += tariff.CalcalateCycleFee(calMins, begin, end);
        //        begin = end;
        //        end = begin.AddDays(1);
        //        calMins += 1440;

        //        while (Card != null && begin < ending)
        //        {
        //            //开始一个收费周期
        //            tariff = TariffSetting.Current.GetIntradayTariff(Card, CarType, begin);//获取下一个收费周期的费率
        //            if (tariff != null || end >= ending) break;
        //            begin = end;
        //            end = begin.AddDays(1);
        //            calMins += 1440;
        //        }
        //    }

        //    if (tariff != null)
        //    {
        //        fee = tariff.CalcalateIntradayFee(calMins, begin, ending);
        //    }

        //    fee = daysfee + fee;

        //    if (FeeOfMax > 0)//有封顶费用
        //    {
        //        fee = fee > FeeOfMax ? FeeOfMax : fee;
        //    }

        //    return fee;
        //}

        public override decimal CalcalateIntradayFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal tail = CalculateFeeAlteration(beginning, ending);
            tail=(FeeOf24Hour > 0 && tail > FeeOf24Hour) ? FeeOf24Hour : tail;
            if (FeeOfMax > 0)//有封顶费用
            {
                tail = tail > FeeOfMax ? FeeOfMax : tail;
            }
            return tail;
        }

        public override decimal GetChargeUnitFee(DateTime beginning)
        {
            decimal fee = 0;
            if (DayTimezone.IsIn(beginning))
            {
                fee += DayTimezone.CalculateFee(DayTimezone.RegularCharge.Minutes);//收取一个单元分钟数费用
                if (DayTimezone.LimiteFee.HasValue && DayTimezone.LimiteFee > 0 && fee > DayTimezone.LimiteFee)
                    fee = DayTimezone.LimiteFee.Value;
            }
            else if (NightTimezone.IsIn(beginning))
            {
                fee += NightTimezone.CalculateFee(DayTimezone.RegularCharge.Minutes);//收取一个单元分钟数费用
                if (NightTimezone.LimiteFee.HasValue && NightTimezone.LimiteFee > 0 && fee > NightTimezone.LimiteFee)
                    fee = NightTimezone.LimiteFee.Value;
            }

            if (FeeOf24Hour > 0 && fee > FeeOf24Hour) fee = FeeOf24Hour;

            if (FeeOfMax > 0 && fee > FeeOfMax) fee = FeeOfMax;//有封顶费用

            return fee;
        }

        public override decimal CalcalateCycleFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            double minmutes = 0;
            minmutes = DayTimezone.TimeZoneTotalMinutes();
            fee += DayTimezone.CalculateFee(minmutes);

            minmutes = NightTimezone.TimeZoneTotalMinutes();
            fee += NightTimezone.CalculateFee(minmutes);

            if (FeeOf24Hour > 0 && fee > FeeOf24Hour) fee = FeeOf24Hour;

            if (FeeOfMax > 0 && fee > FeeOfMax) fee = FeeOfMax;//有封顶费用

            return fee;
        }



        //public override decimal CalculateFee(DateTime beginning, DateTime ending)
        //{
        //    decimal fee = 0;
        //    TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
        //    if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
        //    if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
        //    if (FeeOf24Hour > 0)  //每24小时有限额收费
        //    {
        //        int days = (int)Math.Floor(ts.TotalDays);   //收费都是按24小时为一周期,所以先将整24小时的费用收取完
        //        if (days > 0)
        //        {
        //            fee += FeeOf24Hour * days;
        //        }

        //        DateTime begin2 = beginning.AddDays(days);  //计算不足24小时的收费部分
        //        decimal tail = CalculateFeeAlteration(begin2, ending);
        //        fee += (tail > FeeOf24Hour) ? FeeOf24Hour : tail;
        //    }
        //    else   //无24小时限额收费,则各时间费用累加
        //    {
        //        fee = CalculateFeeAlteration(beginning, ending);
        //    }
        //    return fee;
        //}

        public override string ToString()
        {
            return Resouce.Resource1.Tariff_Guanzhou;
        }
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
    }
}
