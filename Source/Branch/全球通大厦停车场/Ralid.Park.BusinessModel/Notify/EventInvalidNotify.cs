using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 事件无效拒绝放行指令
    /// </summary>
    [DataContract]
    public class EventInvalidNotify
    {
        /// <summary>
        /// 获取或设置要通知的无效事件
        /// </summary>
        [DataMember]
        public CardEventReport CardEvent{get;set;}

        /// <summary>
        /// 值班操作员编号
        /// </summary>
        [DataMember]
        public byte OperatorNum { get; set; }

        /// <summary>
        /// 事件无效类型
        /// </summary>
        [DataMember]
        public EventInvalidType  InvalidType{get;set;}

        /// <summary>
        /// 获取或设置卡片过期日期，如果是卡片过期无效指令的话
        /// </summary>
        [DataMember]
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// 获取或设置卡片余额，如果是余额不足无效指令的话
        /// </summary>
        [DataMember]
        public decimal Balance { get; set; }

        public EventInvalidNotify()
        {
        }
    }
}