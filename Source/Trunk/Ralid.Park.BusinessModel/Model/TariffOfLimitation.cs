using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 限额收费方式
    /// </summary>
    [DataContract]
    public class TariffOfLimitation : TariffBase
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置入场收费
        /// </summary>
        [DataMember]
        public ChargeUnit FirstCharge { get; set; }

        /// <summary>
        /// 获取或设置正常收费
        /// </summary>
        [DataMember]
        public ChargeUnit RegularCharge { get; set; }

        /// <summary>
        /// 获取或设置12小时最高限价，等于0时表示没有设置12小时最高收费
        /// </summary>
        [DataMember]
        public decimal FeeOf12Hour { get; set; }
        #endregion

        #region 重写基类方法
        public override decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            decimal max = 0;  //表示停车费用可以增加的最大尾数
            decimal reg = 0;
            double minutes = Math.Ceiling(ts.TotalMinutes); //总停车分钟数
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            if (FirstCharge != null && minutes <= FirstCharge.Minutes) return FirstCharge.Fee; //停车时间小于首次入场收费时间
            if (FeeOf24Hour > 0)  //设置了24小时限价
            {
                int days = (int)Math.Floor(minutes / (24 * 60));   //收费都是按24小时为一周期,所以先将整24小时的费用收取完
                if (days > 0)
                {
                    fee += FeeOf24Hour * days;
                    minutes -= days * 24 * 60;
                }
                max = FeeOf24Hour;
            }

            if (FeeOf12Hour > 0) //设置了12小时限价
            {
                int c12 = (int)Math.Floor(minutes / (12 * 60));
                if (c12 > 0)
                {
                    fee += FeeOf12Hour * c12;
                    minutes -= c12 * 12 * 60;
                    max = max > 0 ? max - FeeOf12Hour : FeeOf12Hour;
                }
                else
                {
                    max = FeeOf12Hour;
                }
            }

            if (fee == 0 && FirstCharge != null) //表示没有超过最限价的小时数或没有设置限价
            {
                reg += FirstCharge.Fee;
                minutes -= FirstCharge.Minutes;
            }
            reg += ((int)Math.Ceiling(minutes / RegularCharge.Minutes)) * RegularCharge.Fee;
            fee += (max > 0 && reg > max) ? max : reg;
            return fee;
        }

        public override string ToString()
        {
            return Resouce.Resource1.Tariff_Limitation;
        }
        #endregion
    }
}
