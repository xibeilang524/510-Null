using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示一个带入场收费的日夜差异收费,目前用于珠江新城地下空间
    /// 停放3个小时内(含三个小时)收取2.5元/半小时,超过3个小时的部分按5元/半小时收取,每天22:00--8:00最高限价为10元,24小时限价65元
    /// </summary>
    [DataContract]
    public class TariffOfDixiakongjian : TariffBase
    {
        #region 构造函数
        public TariffOfDixiakongjian()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 表示入场预定分钟数
        /// </summary>
        [DataMember]
        public int FirstMinutes { get; set; }
        /// <summary>
        /// 获取或设置入场预定分钟数内的收费规则
        /// </summary>
        [DataMember]
        public ChargeUnit FirstFee { get; set; }
        /// <summary>
        /// 获取或设置超过入场预定分钟数后的收费规则
        /// </summary>
        [DataMember]
        public ChargeUnit RegularFee { get; set; }
        /// <summary>
        /// 获取或设置限价时段
        /// </summary>
        [DataMember]
        public TimeZone LimitationTimezone { get; set; }
        /// <summary>
        /// 获取或设置限价时段的收费规则
        /// </summary>
        [DataMember]
        public ChargeUnit LimitationRegularFee { get; set; }
        /// <summary>
        /// 获取或设置限价时段的最高限价
        /// </summary>
        [DataMember]
        public decimal Limitation { get; set; }
        #endregion

        #region 重写基类方法
        public override decimal CalcalateIntradayFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = 0;

            //把预定时间的费用收完
            double sliceAll = 0;
            TimeZone other = new TimeZone(LimitationTimezone.Ending, LimitationTimezone.Beginning); //如果有限价时段,那么其它的时间就组成另一个时段
            while (beginning < ending)
            {
                sliceAll = other.Slice(beginning, ending, FirstFee.Minutes, false);
                if (sliceAll > 0)
                {
                    if (sliceAll <= FirstMinutes) //如果还在入场时间内
                    {
                        fee += (decimal)Math.Ceiling(sliceAll / FirstFee.Minutes) * FirstFee.Fee;
                    }
                    else
                    {
                        fee += ((decimal)Math.Ceiling((decimal)FirstMinutes / FirstFee.Minutes)) * FirstFee.Fee;
                        //计算超过3小时部分阶段
                        double fuck = sliceAll - FirstMinutes;
                        fee += ((decimal)Math.Ceiling(fuck / RegularFee.Minutes)) * RegularFee.Fee;
                    }
                    calMins += sliceAll;
                    beginning = beginning.AddMinutes(sliceAll);
                }
                sliceAll = LimitationTimezone.Slice(beginning, ending, FirstFee.Minutes, false);
                if (sliceAll > 0)
                {
                    decimal tempFee = 0;
                    tempFee += (decimal)(Math.Ceiling(sliceAll / LimitationRegularFee.Minutes)) * LimitationRegularFee.Fee;
                    fee += tempFee > Limitation ? Limitation : tempFee;
                    calMins += sliceAll;
                    beginning = beginning.AddMinutes(sliceAll);
                }
            }
            if (FeeOf24Hour > 0 && FeeOf24Hour < fee) fee = FeeOf24Hour;

            return fee;
        }

        public override decimal GetChargeUnitFee(DateTime beginning)
        {
            decimal fee = 0;
            TimeZone other = new TimeZone(LimitationTimezone.Ending, LimitationTimezone.Beginning); //如果有限价时段,那么其它的时间就组成另一个时段
            if (other.IsIn(beginning))
            {
                fee += FirstFee.Fee;
            }
            else if (LimitationTimezone.IsIn(beginning))
            {
                fee += LimitationRegularFee.Fee;
                if (Limitation > 0 && fee > Limitation) fee = Limitation;
            }

            if (FeeOf24Hour > 0 && fee > FeeOf24Hour) fee = FeeOf24Hour;

            if (FeeOfMax > 0 && fee > FeeOfMax) fee = FeeOfMax;//有封顶费用

            return fee;
        }

        public override decimal CalcalateCycleFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            double minmutes = 0;
            double firstMinutes = FirstMinutes;//入场预定时间内
            TimeZone other = new TimeZone(LimitationTimezone.Ending, LimitationTimezone.Beginning); //如果有限价时段,那么其它的时间就组成另一个时段
            
            //先计算没有限价的时段
            minmutes = other.TimeZoneTotalMinutes();
            firstMinutes = calMins > FirstMinutes ? 0 : FirstMinutes - calMins;//预定时间的费用是否已收取
            firstMinutes = minmutes > firstMinutes ? firstMinutes : minmutes;//还在入场预定时间的分钟数
            fee += ((decimal)Math.Ceiling(firstMinutes / FirstFee.Minutes)) * FirstFee.Fee;//先计算入场预定时间的费用
            minmutes -= firstMinutes;
            if (minmutes > 0)
            {
                fee += (decimal)Math.Ceiling(minmutes / RegularFee.Minutes) * RegularFee.Fee;//再计算入场预定时间后的费用
            }

            //再计算限价时段的
            minmutes = LimitationTimezone.TimeZoneTotalMinutes();
            decimal tempFee = 0;
            tempFee += (decimal)(Math.Ceiling(minmutes / LimitationRegularFee.Minutes)) * LimitationRegularFee.Fee;
            fee += Limitation > 0 && tempFee > Limitation ? Limitation : tempFee;
            
            if (FeeOf24Hour > 0 && fee > FeeOf24Hour) fee = FeeOf24Hour;

            if (FeeOfMax > 0 && fee > FeeOfMax) fee = FeeOfMax;//有封顶费用

            return fee;
        }

        public override string ToString()
        {
            return Resouce.Resource1.Tariff_Dixiakongjian;
        }

        public override decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            decimal daysfee = 0;
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            double totalMins = Math.Ceiling(ts.TotalMinutes);
            double calMins = 0; //已经计费的分钟数
            if (totalMins < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && totalMins <= FreeMinutes) return 0;//小于免费停车时间
            if (ts.TotalDays >= 1 && FeeOf24Hour > 0)  //如果有每天最高收费，先计算出整天数的费用
            {
                double days = Math.Floor(ts.TotalDays);
                daysfee += ((decimal)days) * FeeOf24Hour;
                calMins += days * 1440;
                beginning = beginning.AddDays(days);
            }

            //把预定时间的费用收完
            double sliceAll = 0;
            TimeZone other = new TimeZone(LimitationTimezone.Ending, LimitationTimezone.Beginning); //如果有限价时段,那么其它的时间就组成另一个时段
            while (beginning < ending)
            {
                sliceAll = other.Slice(beginning, ending, FirstFee.Minutes, false);
                if (sliceAll > 0)
                {
                    if (sliceAll <= FirstMinutes) //如果还在入场时间内
                    {
                        fee += (decimal)Math.Ceiling(sliceAll / FirstFee.Minutes) * FirstFee.Fee;
                    }
                    else
                    {
                        fee += ((decimal)Math.Ceiling((decimal)FirstMinutes / FirstFee.Minutes)) * FirstFee.Fee;
                        //计算超过3小时部分阶段
                        double fuck = sliceAll - FirstMinutes;
                        fee += ((decimal)Math.Ceiling(fuck / RegularFee.Minutes)) * RegularFee.Fee;
                    }
                    calMins += sliceAll;
                    beginning = beginning.AddMinutes(sliceAll);
                }
                sliceAll = LimitationTimezone.Slice(beginning, ending, FirstFee.Minutes, false);
                if (sliceAll > 0)
                {
                    decimal tempFee = 0;
                    tempFee += (decimal)(Math.Ceiling(sliceAll / LimitationRegularFee.Minutes)) * LimitationRegularFee.Fee;
                    fee += tempFee > Limitation ? Limitation : tempFee;
                    calMins += sliceAll;
                    beginning = beginning.AddMinutes(sliceAll);
                }
            }
            if (FeeOf24Hour > 0 && FeeOf24Hour < fee) fee = FeeOf24Hour;
            return fee + daysfee;
        }
        #endregion
    }
}
