using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class ServerSwitchRecordSearchCondition : SearchCondition
    {
        /// <summary>
        /// 获取或设置记录的停车场ID
        /// </summary>
        public int ParkID { get; set; }
        /// <summary>
        /// 获取或设置记录发生的时间范围
        /// </summary>
        public DateTimeRange RecordDateTimeRange { get; set; }
        /// <summary>
        /// 获取或设置记录的短信发送状态
        /// </summary>
        public SMSSendStatus? SMSStatus { get; set; }
    }
}
