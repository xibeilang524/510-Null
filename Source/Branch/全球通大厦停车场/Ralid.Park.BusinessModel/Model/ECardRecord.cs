using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示需要传递给全球通一车通的车辆进出记录
    /// </summary>
    public class ECardRecord
    {
        public int ID { get; set; }
        public string SheetID { get; set; }
        public string CardID { get; set; }
        public string Carplate { get; set; }
        public DateTime EventDt { get; set; }
        public DateTime? EnterDt { get; set; }
        public decimal? Limitation { get; set; }
        public decimal? LimitationRemain { get; set; }
    }
}
