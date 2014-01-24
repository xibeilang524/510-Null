using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 停车费率的基类
    /// </summary>
    [Serializable]
    public abstract class TariffBase
    {
        #region 公共属性
        /// <summary>
        /// 收费卡类型
        /// </summary>
        public Byte CardType { get; set; }

        /// <summary>
        /// 收费车型
        /// </summary>
        public Byte CarType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>
        public TariffType TariffType { get; set; }

        /// <summary>
        /// 获取或设置免费时间(分钟)
        /// </summary>
        public byte FreeMinutes { get; set; }

        /// <summary>
        /// 获取或设置无入场记录按次收费
        /// </summary>
        public bool ChargePerTimeWithoutEnter { get; set; }

        /// <summary>
        ///获取或设置无入场记录按次收费的收费金额
        /// </summary>
        public decimal FeeWithoutEnter { get; set; }

        /// <summary>
        /// 获取或设置每24小时最高收费,等于0时表示没有设置每24小时最高收费
        /// </summary>
        public decimal FeeOf24Hour { get; set; }

        /// <summary>
        /// 获取或设置封顶费用，即收费的最大费用，等于0时表示没有设置封顶费用
        /// </summary>
        public decimal FeeOfMax { get; set; }
        #endregion

        #region 虚拟方法
        /// <summary>
        /// 计算停车费用
        /// </summary>
        /// <param name="enter">入场时间</param>
        /// <param name="exit">出场时间</param>
        /// <returns></returns>
        public abstract  decimal CalculateFee(DateTime beginning, DateTime ending);
        #endregion

        #region 重写基类方法
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is TariffBase)) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            else
            {
                TariffBase tb = obj as TariffBase;
                return this.CalculateFee(new DateTime(2011, 1, 1), new DateTime(2011, 1, 2)) == tb.CalculateFee(new DateTime(2011, 1, 1), new DateTime(2011, 1, 2));
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
