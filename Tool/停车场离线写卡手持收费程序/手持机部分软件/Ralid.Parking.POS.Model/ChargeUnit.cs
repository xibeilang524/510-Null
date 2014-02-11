using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 收费单元,即表示每隔多少分钟收多少钱
    /// </summary>
    [Serializable ]
    public class ChargeUnit
    {
        /// <summary>
        /// 设置或获取收费单元分钟数
        /// </summary>
        public short Minutes { get; set; }

        /// <summary>
        /// 设置或获取收费单元费用(元)
        /// </summary>
        public decimal Fee { get; set; }

        public ChargeUnit()
        {
        }

        public ChargeUnit(short minutes, decimal fee)
        {
            Minutes = minutes;
            Fee = fee;
        }

        public decimal CalculateFee(double minutes)
        {
            decimal fee = 0;
            if (minutes > 0)
            {
                int count = (int)Math.Ceiling(minutes / Minutes);
                fee = count * Fee;
            }
            return fee;
        }
    }
}
