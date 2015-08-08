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

        public int? PSN { get; set; }

        public string LCN { get; set; }

        public DateTime? TIM { get; set; }

        public int? WalletType { get; set; }

        public int? State { get; set; }

        public string UploadFile { get; set; }

        public bool UnUploaded { get; set; }
    }
}
