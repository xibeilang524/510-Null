using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class YCTPaymentRecordSearchCondition : SearchCondition
    {
        public DateTimeRange PaymentDateTimeRange { get; set; }

        public string PID { get; set; }

        public string CardID { get; set; }

        public int? State { get; set; }
    }
}
