using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 不分天按次收费费率
    /// </summary>
    [Serializable]
    public class TariffPerTime : TariffBase
    {
        #region 构造函数
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
        #endregion

        #region 公共属性
        public decimal FeePerTime { get; set; }
        #endregion

        #region 重写基类方法
        public override decimal CalculateFee(DateTime enter, DateTime exit)
        {
            TimeSpan ts = new TimeSpan(exit.Ticks - enter.Ticks);
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            if (FreeMinutes == 0 && ts.TotalMinutes == 0) return FeePerTime;
            return FeePerTime;
        }
        #endregion
    }
}
