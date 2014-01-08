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

        #region 重写基类方法
        public override decimal CalculateFee(DateTime enter, DateTime exit)
        {
            TimeSpan ts = new TimeSpan(exit.Ticks - enter.Ticks);
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            if (FreeMinutes == 0 && ts.TotalMinutes == 0) return FeePerTime;
            return FeePerTime;
        }

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
        #endregion
    }
}
