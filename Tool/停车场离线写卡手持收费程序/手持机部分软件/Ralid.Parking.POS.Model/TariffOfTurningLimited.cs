using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 表示限时过点收费
    /// </summary>
    [Serializable]
    public class TariffOfTurningLimited : TariffBase
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置入场收费
        /// </summary>
        public Decimal FirstFee { get; set; }

        /// <summary>
        /// 获取或设置改变收费费用的转折点
        /// </summary>
        public TimeEntity Turning { get; set; }

        /// <summary>
        /// 获取或设置过零点后的收费
        /// </summary>
        public decimal FeeOfTurning { get; set; }
        #endregion

        #region 重写基类方法
        public override decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间

            fee += FirstFee;
            if (ts.TotalHours < 24)
            {
                //小于24小时的，直接返回费用
                return fee;
            }

            DateTime turn = new DateTime(beginning.Year, beginning.Month, beginning.Day, Turning.Hour, Turning.Minute, 0);
            if (turn < beginning) turn.AddDays(1);

            if (ending >= turn)
            {
                while (ending >= turn)
                {
                    fee += FeeOfTurning;
                    turn = turn.AddDays(1);
                }
                return fee;
            }
            else
            {
                return fee;
            }
        }
        #endregion
    }
}
