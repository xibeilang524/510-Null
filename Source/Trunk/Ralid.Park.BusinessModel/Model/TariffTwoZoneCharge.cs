using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 描述两个标准收费时段的收费费用
    /// </summary>
    public class TariffTwoZoneCharge : IComparable<TariffTwoZoneCharge>
    {

        #region 构造函数
        public TariffTwoZoneCharge()
        {
        }

        public TariffTwoZoneCharge(DateTime begin, DateTime end)
        {
            Beginning = begin;
            Ending = end;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 设置或获取收费的开始时间
        /// </summary>
        public DateTime Beginning { get; set; }

        /// <summary>
        /// 设置或获取收费的结算时间
        /// </summary>
        public DateTime Ending { get; set; }

        /// <summary>
        /// 获取或设置第一个收费时段收取的费用（日夜差异收费时为日间时段）
        /// </summary>
        public decimal FirstCharge { get; set; }

        /// <summary>
        /// 获取或设置第二个收费时段收取的费用（日夜差异收费时为夜间时段）
        /// </summary>
        public decimal SecondCharge { get; set; }

        /// <summary>
        /// 获取两个收费时段收取的费用
        /// </summary>
        public decimal ChargeFee
        {
            get
            {
                return FirstCharge + SecondCharge;
            }
        }
        #endregion

        #region 实现IComparable接口
        public int CompareTo(TariffTwoZoneCharge other)
        {
            return ChargeFee.CompareTo(other.ChargeFee);
        }
        #endregion
    }
}
