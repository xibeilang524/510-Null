using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 描述一个特殊收费时段,特殊时段的计费优先级高于普通时段
    /// </summary>
    [Serializable]
    public class TariffSpecialTimezone
    {
        #region 公共属性
        /// <summary>
        /// 获取入场开始时间
        /// </summary>
        public TimeEntity EnterStart{get;set;}

        /// <summary>
        /// 获取入场结束时间
        /// </summary>
        public TimeEntity EnterEnd{get;set;}

        /// <summary>
        /// 获取出场开始时间
        /// </summary>
        public TimeEntity ExitStart{get;set;}

        /// <summary>
        /// 获取出场结束时间
        /// </summary>
        public TimeEntity ExitEnd{get;set;}

        /// <summary>
        /// 获取特殊时段内收费
        /// </summary>
        public decimal Fee{get;set;}
        #endregion

        public TariffSpecialTimezone()
        {
        }

        public TariffSpecialTimezone(TimeEntity enterBegin, TimeEntity enterEnd, TimeEntity exitBegin, TimeEntity exitEnd, Decimal fee)
        {
            EnterStart = enterBegin;
            EnterEnd = enterEnd;
            ExitStart = exitBegin;
            ExitEnd = exitEnd;
            Fee = fee;
        }



        #region ITariffTimezone 成员

        public decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            if (ending < beginning)
            {
                throw new InvalidOperationException("结束时间小于开始时间");
            }

            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            if (ts.TotalDays > 1)  //跨24小时后不再用特殊时段收费,收费时段在24小时之内有效
            {
                return 0;
            }

            //以开始时间为参照,建立收费时段的当天参照时间
            DateTime dt1 = new DateTime(beginning.Year, beginning.Month, beginning.Day, EnterStart.Hour, EnterStart.Minute, 0);
            DateTime dt2 = new DateTime(beginning.Year, beginning.Month, beginning.Day, EnterEnd.Hour, EnterEnd.Minute, 0);
            if (dt2 < dt1)
            {
                dt2.AddDays(1);
            }
            bool isEnterAtTimezone = (beginning >= dt1 && beginning <= dt2);

            dt1 = new DateTime(ending.Year, ending.Month, ending.Day, ExitStart.Hour, ExitStart.Minute, 0);
            dt2 = new DateTime(ending.Year, ending.Month, ending.Day, ExitEnd.Hour, ExitEnd.Minute, 0);
            if (dt2 < dt1)
            {
                dt2.AddDays(1);
            }
            bool isExitAtTimezone = (ending >= dt1 && ending <= dt2);

            if (isEnterAtTimezone && isExitAtTimezone)
            {
                return Fee;
            }
            else
            {
                return 0;
            }
        }


        #endregion
    }
}
