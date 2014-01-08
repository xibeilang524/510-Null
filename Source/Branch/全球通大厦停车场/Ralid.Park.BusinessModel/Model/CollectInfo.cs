using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 报表类，Add By Tom,2012-3-7
    /// </summary>
    public class CollectInfo
    {
        public string Name { get; set; }
        public decimal Car { get; set; }
        public decimal Truck { get; set; }
        public decimal SuperTruck { get; set; }
        public decimal MotorBike { get; set; }
        public decimal FreeCar { get; set; }
        public decimal TotalMomey { get; set; }
    }
}
