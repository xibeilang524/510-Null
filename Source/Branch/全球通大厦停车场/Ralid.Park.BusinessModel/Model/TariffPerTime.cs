using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 不分天按次收费费率
    /// </summary>
    [DataContract]
    public class TariffPerTime : TariffBase
    {
        public TariffPerTime()
        {
        }

        public TariffPerTime(byte freeTime, decimal feePerTime)
        {
            FreeMinutes = freeTime;
            FeePerTime = feePerTime;
            ChargePerTimeWithoutEnter = true;
            FeeWithoutEnter = feePerTime;
        }

        [DataMember]
        public decimal FeePerTime { get; set; }

        //public override decimal CalculateFee(DateTime enter, DateTime exit)
        //{
        //    decimal fee = 0;
        //    decimal daysfee = 0;
        //    double calMins = 0;//已计费的分钟数
        //    TimeSpan ts = new TimeSpan(exit.Ticks - enter.Ticks);
        //    if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
        //    if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
        //    if (FreeMinutes == 0 && ts.TotalMinutes == 0) return GetChargeUnitFee(enter);//返回收费单元的费用

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
        //}

        public override decimal CalcalateCycleFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = calMins > 0 ? 0 : FeePerTime;
            return fee;
        }

        public override decimal CalcalateIntradayFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = calMins > 0 ? 0 : FeePerTime;
            return fee;
        }

        public override decimal GetChargeUnitFee(DateTime beginning)
        {
            decimal fee = FeePerTime;
            if (FeeOfMax > 0)//有封顶费用
            {
                fee = fee > FeeOfMax ? FeeOfMax : fee;
            }
            return fee;
        }

        public override string ToString()
        {
            return Resouce.Resource1.Tariff_PerTime;
        }
    }
}
