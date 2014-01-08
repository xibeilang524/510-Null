using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示过点转换收费
    /// </summary>
    [DataContract]
    public class TariffOfTurning:TariffBase
    {
        /// <summary>
        /// 获取或设置入场收费
        /// </summary>
        [DataMember]
        public Decimal FirstFee { get; set; }

        /// <summary>
        /// 获取或设置改变收费费用的转折点
        /// </summary>
        [DataMember]
        public TimeEntity Turning { get; set; }

        /// <summary>
        /// 获取或设置过零点后的收费
        /// </summary>
        [DataMember]
        public decimal FeeOfTurning { get; set; }

        //public override decimal CalculateFee(DateTime beginning, DateTime ending)
        //{
        //    decimal fee = 0;
        //    decimal daysfee = 0;
        //    double calMins = 0;//已计费的分钟数
        //    TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
        //    if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
        //    if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
        //    if (FreeMinutes == 0 && ts.TotalMinutes == 0) return GetChargeUnitFee(beginning);//返回最小收费

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

        //    //fee += FirstFee;
        //    //DateTime turn = new DateTime(beginning.Year, beginning.Month, beginning.Day, Turning.Hour, Turning.Minute, 0).AddDays(1);
        //    //if (ending > turn)
        //    //{
        //    //    while (ending > turn)
        //    //    {
        //    //        fee += FeeOfTurning;
        //    //        turn = turn.AddDays(1);
        //    //    }
        //    //}

        //    //if (FeeOfMax > 0)//有封顶费用
        //    //{
        //    //    fee = fee > FeeOfMax ? FeeOfMax : fee;
        //    //}
        //    //return fee;
        //}

        public override decimal CalcalateCycleFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = calMins > 0 ? 0 : FirstFee; 
            DateTime turn = new DateTime(beginning.Year, beginning.Month, beginning.Day, Turning.Hour, Turning.Minute, 0).AddDays(1);
            if (ending >= turn)
            {
                fee += FeeOfTurning;
            }
            if (FeeOfMax > 0)//有封顶费用
            {
                fee = fee > FeeOfMax ? FeeOfMax : fee;
            }
            return fee;
        }

        public override decimal CalcalateIntradayFee(double calMins, DateTime beginning, DateTime ending)
        {
            decimal fee = calMins > 0 ? 0 : FirstFee;
            DateTime turn = new DateTime(beginning.Year, beginning.Month, beginning.Day, Turning.Hour, Turning.Minute, 0).AddDays(1);
            if (ending > turn)
            {
                fee += FeeOfTurning;
            }
            if (FeeOfMax > 0)//有封顶费用
            {
                fee = fee > FeeOfMax ? FeeOfMax : fee;
            }
            return fee;
        }

        public override decimal GetChargeUnitFee(DateTime beginning)
        {
            decimal fee = FirstFee;
            if (FeeOfMax > 0)//有封顶费用
            {
                fee = fee > FeeOfMax ? FeeOfMax : fee;
            }
            return fee;
        }

        #region 重写基类方法
        public override string ToString()
        {
            return Resouce.Resource1.Tariff_Turning;
        }
        #endregion
    }
}
