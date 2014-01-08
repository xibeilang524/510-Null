using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class APMLogSearchCondition : RecordSearchCondition
    {
        /// <summary>
        /// 获取或设置查询条件中的日志流水号
        /// </summary>
        public string SerialNum { get; set; }
        /// <summary>
        /// 获取或设置要查询日志的自助缴费机编号
        /// </summary>
        public string MID { get; set; }
        /// <summary>
        /// 获取或设置要查询的日志类型
        /// </summary>
        public List<APMLogType> Types { get; set; }
        /// <summary>
        /// 获取或设置要查询日志的部分描述
        /// </summary>
        public string Description { get; set; }
    }
}
