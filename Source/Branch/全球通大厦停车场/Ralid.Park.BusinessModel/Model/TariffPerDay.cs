using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 按天收费费率(每过24小时收取一天费用)
    /// </summary>
    [DataContract]
    [Serializable]
    public class TariffPerDay:TariffBase 
    {
        public TariffPerDay()
        {
        }

        public TariffPerDay(byte freeTime, decimal feePerDay)
        {
            FreeMinutes = freeTime;
            FeePerDay = feePerDay;
            ChargePerTimeWithoutEnter = true;
            FeeWithoutEnter = feePerDay;
        }

        /// <summary>
        /// 获取或设置每天收取的费用
        /// </summary>
        [DataMember]
        public decimal FeePerDay { get; set; }

        /// <summary>
        /// 获取或设置超过多少天后，超过的每天按超过指定天数后每天收取的费用收费，等于0时表示没有设置
        /// </summary>
        [DataMember]
        public short OverDay { get; set; }

        /// <summary>
        /// 获取或设置超过指定天数后每天收取的费用
        /// </summary>
        [DataMember]
        public decimal FeePerOverDay { get; set; }

        //public override decimal CalculateFee(DateTime enter, DateTime exit)
        //{
        //    decimal fee = 0;
        //    decimal daysfee = 0;
        //    double calMins = 0;//已计费的分钟数
        //    TimeSpan ts = new TimeSpan(exit.Ticks - enter.Ticks);
        //    if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
        //    if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
        //    if (FreeMinutes == 0 && ts.TotalMinutes == 0) return GetChargeUnitFee(enter);//返回最小收费

        //    DateTime begin = enter;
        //    DateTime end = begin.AddDays(1);

        //    TariffBase tariff = this;

        //    //收费都是按24小时为一周期
        //    while (end <= exit)
        //    {
        //        daysfee += tariff.CalcalateCycleFee(calMins, begin, end);
        //        begin = end;
        //        end = begin.AddDays(1);
        //        calMins += 1440;

        //        while (Card != null && begin < exit)
        //        {
        //            //开始一个收费周期
        //            tariff = TariffSetting.Current.GetIntradayTariff(Card, CarType, begin);//获取下一个收费周期的费率
        //            if (tariff != null || end >= exit) break;
        //            begin = end;
        //            end = begin.AddDays(1);
        //            calMins += 1440;
        //        }
        //    }



        //    if (tariff != null)
        //    {
        //        fee = tariff.CalcalateIntradayFee(calMins, begin, exit);
        //    }

        //    fee = daysfee + fee;

        //    if (FeeOfMax > 0)//有封顶费用
        //    {
        //        fee = fee > FeeOfMax ? FeeOfMax : fee;
        //    }

        //    return fee;

        //    //int days = (int)Math.Ceiling(ts.TotalDays);

        //    //if (OverDay > 0)//有设置超过多少天后，超过的每天按超过指定天数后每天收取的费用收费
        //    //{
        //    //    int firstdays = days > OverDay ? OverDay : days;
        //    //    fee += firstdays * FeePerDay;
        //    //    days -= firstdays;
        //    //    fee += days * FeePerOverDay;
        //    //}
        //    //else
        //    //{
        //    //    fee = (int)Math.Ceiling(ts.TotalDays) * FeePerDay;
        //    //}

        //    //if (FeeOfMax > 0)//有封顶费用
        //    //{
        //    //    fee = fee > FeeOfMax ? FeeOfMax : fee;
        //    //}

        //    //return fee;
        //}

        public override decimal CalcalateCycleFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            if (OverDay > 0)//有设置超过多少天后，超过的每天按超过指定天数后每天收取的费用收费
            {
                int calDays = (int)Math.Ceiling(calMins / (24 * 60));//已缴费天数
                fee = calDays >= OverDay ? FeePerOverDay : FeePerDay;
            }
            else
            {
                fee = FeePerDay;
            }

            if (FeeOfMax > 0)//有封顶费用
            {
                fee = fee > FeeOfMax ? FeeOfMax : fee;
            }
            return fee;
        }

        public override decimal CalcalateIntradayFee(double calMins, DateTime beginning, DateTime ending)
        {
            return CalcalateCycleFee(calMins, beginning, ending);
        }

        public override decimal GetChargeUnitFee(DateTime beginning)
        {
            decimal fee = FeePerDay;
            if (FeeOfMax > 0)//有封顶费用
            {
                fee = fee > FeeOfMax ? FeeOfMax : fee;
            }
            return fee;
        }

        public override string ToString()
        {
            return Resouce.Resource1.Tariff_PerDay;
        }
    }
}
