using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 过零点收费方式
    /// </summary>
    [Serializable]
    public class TariffOfMidNight : TariffBase
    {
        /// <summary>
        /// 获取或设置入场收费
        /// </summary>
        
        public Decimal FirstFee { get; set; }

        /// <summary>
        /// 获取或设置过零点后的收费
        /// </summary>
        
        public decimal FeeOfMidNight { get; set; }

        public override decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            double days = Math.Ceiling(ts.TotalDays);
            if (days <= 1)
            {
                DateTime midnight = beginning.Date.AddDays(1).AddMinutes(1);  //过零点一分钟后
                if (ending >= midnight)
                {
                    fee += FeeOfMidNight;
                }
                else
                {
                    fee += FirstFee;
                }
            }
            else
            {
                fee += (decimal)days * FeeOf24Hour;
            }
            return fee;
        }
    }
}

