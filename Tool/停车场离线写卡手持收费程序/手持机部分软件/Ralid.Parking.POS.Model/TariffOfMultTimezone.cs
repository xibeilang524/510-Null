using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 多时段收费标准
    /// </summary>
    [Serializable]
    public class TariffOfMultTimezone : TariffBase
    {
        private List<TariffTimeZone> _TariffTimezones = new List<TariffTimeZone>();

        public override decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            return fee;
        }

        public override string ToString()
        {
            return "多时段收费";
        }
    }
}
