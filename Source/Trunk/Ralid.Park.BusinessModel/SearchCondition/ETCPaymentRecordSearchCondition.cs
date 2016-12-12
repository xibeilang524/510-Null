using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public  class ETCPaymentRecordSearchCondition:SearchCondition 
    {
        /// <summary>
        /// 流水号模糊查询条件
        /// </summary>
        public string ListNoLike { get; set; }
        /// <summary>
        /// 创建时间段
        /// </summary>
        public DateTimeRange CreateTime { get; set; }
        /// <summary>
        /// 是否等待上传
        /// </summary>
        public bool? WaitingUpload { get; set; }
    }
}
