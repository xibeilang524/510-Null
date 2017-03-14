using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public  class ETCPaymentRecordSearchCondition:SearchCondition 
    {
        /// <summary>
        /// 创建时间段
        /// </summary>
        public DateTimeRange AddTime { get; set; }
        /// <summary>
        /// 车道号
        /// </summary>
        public string LaneNo { get; set; }
        /// <summary>
        /// 是否等待上传
        /// </summary>
        public bool? WaitingUpload { get; set; }
    }
}
