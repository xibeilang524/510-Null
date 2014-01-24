using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 表示停车场收费信息
    /// </summary>
    [Serializable]
    public class ParkAccountsInfo
    {
        /// <summary>
        /// 获取或设置应收金额
        /// </summary>
        public decimal Accounts { get; set; }
        /// <summary>
        /// 获取或设置停车场由进场到计费时间所产生的停车费用
        /// </summary>
        public decimal ParkFee { get; set; }
        /// <summary>
        /// 获取或设置收费类型
        /// </summary>
        public TariffType TariffType { get; set; }
        /// <summary>
        /// 获取或设置收费车型
        /// </summary>
        public Byte CarType { get; set; }

        public ParkAccountsInfo()
        {
            TariffType = TariffType.Normal;
        }
    }
}
