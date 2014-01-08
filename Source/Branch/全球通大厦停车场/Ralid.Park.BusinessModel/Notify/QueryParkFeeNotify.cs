using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 查询卡片停车费用通知类
    /// </summary>
    [DataContract]
    public class QueryParkFeeNotify
    {
        /// <summary>
        /// 获取或设置要查询停车费用的卡片ID
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置要查询停车费用的卡片类型
        /// </summary>
        [DataMember]
        public CardType CardType { get; set; }

        /// <summary>
        /// 获取或设置要查询的通道号
        /// </summary>
        [DataMember]
        public byte Address { get; set; }

        public QueryParkFeeNotify()
        {
            Address = 1;
        }
    }
}
