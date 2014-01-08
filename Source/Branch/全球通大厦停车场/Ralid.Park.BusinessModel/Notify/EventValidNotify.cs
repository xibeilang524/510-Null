using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 事件有效放行指令
    /// </summary>
    [DataContract]
    public class EventValidNotify
    {
        /// <summary>
        /// 获取或设置发生事件控制器的地址
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }

        /// <summary>
        /// 获取或设置值班操作员编号
        /// </summary>
        [DataMember]
        public OperatorInfo Operator { get; set; }

        /// <summary>
        /// 获取或设置发出事件有效指令的工作站
        /// </summary>
        [DataMember]
        public string  Station { get; set; }

        /// <summary>
        /// 获取或设置实收(扣)停车费用
        /// </summary>
        [DataMember]
        public decimal Paid { get; set; }

        public EventValidNotify(int entranceID, OperatorInfo opt, string stationID, decimal paid)
        {
            this.EntranceID = entranceID;
            this.Operator = opt;
            this.Station = stationID;
            this.Paid = paid;
        }

        public EventValidNotify()
        {
        }
    }
}
