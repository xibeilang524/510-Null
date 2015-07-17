using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    /// <summary>
    /// 自助缴费机结账记录查询条件
    /// </summary>
    public class APMCheckOutRecordSearchCondition:RecordSearchCondition
    {
        /// <summary>
        /// 获取或设置要查询日志的自助缴费机编号
        /// </summary>
        public string MID { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的结账操作员
        /// </summary>
        public string APMOperator { get; set; }
    }
}
