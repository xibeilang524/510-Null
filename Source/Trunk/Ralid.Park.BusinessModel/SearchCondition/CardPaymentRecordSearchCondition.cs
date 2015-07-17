using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class CardPaymentRecordSearchCondition:RecordSearchCondition 
    {
        /// <summary>
        /// 获取或设置查询记录的入场时间
        /// </summary>
        public DateTime? EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置记录发生的时间范围
        /// </summary>
        public DateTimeRange EnterDateTimeRange { get; set; }
        // add by tom,2012-3-7
        public List<EntranceInfo> Source { get; set; }  //事件发生通道
        // end
        /// <summary>
        /// 获取或设置查询条件是否是中央收费产生的收费记录
        /// </summary>
        public bool? IsCenterCharge { get; set; }

        /// <summary>
        /// 获取或设置查询记录的缴费时间
        /// </summary>
        public DateTime? ChargeDateTime { get; set; }

        /// <summary>
        /// 获取或设置收费代码
        /// </summary>
        public PaymentCode? PaymentCode { get; set; }

        /// <summary>
        /// 获取或设置操作员卡号
        /// </summary>
        public string OperatorCardID { get; set; }
    }
}
