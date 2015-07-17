using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class PreferentialReportSearchCondition : RecordSearchCondition
    {
        public DateTimeRange EnterDateTimeRange { get; set; }
        public string CardID { get; set; }
        public string BusinessName { get; set; }
        public DateTime OperatorTime { get; set; }

        /// <summary>
        /// 获取或设置查询条件中的多个操作员名称
        /// </summary>
        public List<string> OperatorNames { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
        /// <summary>
        /// 优惠时数
        /// </summary>
        public int? Hour { get; set; }
    }
}
