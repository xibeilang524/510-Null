using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 按天收费费率(每过24小时收取一天费用)
    /// </summary>
    [Serializable]
    public class TariffPerDay : TariffBase
    {
        #region 构造函数
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
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置每天收取的费用
        /// </summary>
        public decimal FeePerDay { get; set; }

        /// <summary>
        /// 获取或设置超过多少天后，超过的每天按超过指定天数后每天收取的费用收费，等于0时表示没有设置
        /// </summary>
        public short OverDay { get; set; }

        /// <summary>
        /// 获取或设置超过指定天数后每天收取的费用
        /// </summary>
        public decimal FeePerOverDay { get; set; }
        #endregion

        #region 重写基类方法
        public override decimal CalculateFee(DateTime enter, DateTime exit)
        {
            decimal fee = 0;
            TimeSpan ts = new TimeSpan(exit.Ticks - enter.Ticks);
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            if (FreeMinutes == 0 && ts.TotalMinutes == 0) return FeePerDay;

            double totalDays = System.Math.Ceiling(ts.TotalDays);
            if (FeePerOverDay > 0 && totalDays > OverDay)//有设置超过多少天后，超过的每天按超过指定天数后每天收取的费用收费
            {
                fee = OverDay * FeePerDay;
                fee += ((decimal)totalDays - OverDay) * FeePerOverDay;
            }
            else
            {
                fee = (int)Math.Ceiling(totalDays) * FeePerDay;
            }

            if (FeeOfMax > 0)//有封顶费用
            {
                fee = fee > FeeOfMax ? FeeOfMax : fee;
            }
            return fee;
        }
        #endregion
    }
}
