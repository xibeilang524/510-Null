using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Enum;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 开关道闸通知
    /// </summary>
    [DataContract]
    public class GateOperationNotify
    {
        /// <summary>
        /// 控制器地址
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }

        /// <summary>
        /// 道闸上的操作
        /// </summary>
        [DataMember]
        public GateOperation Action{get;set;}

        /// <summary>
        /// 获取或设置操作员ID
        /// </summary>
        [DataMember]
        public string OperatorID { get; set; }


        public GateOperationNotify(int entranceID,GateOperation action,string operatorID)
        {
            this.EntranceID = entranceID;
            this.Action = action;
            this.OperatorID = operatorID;
        }

        public GateOperationNotify()
        {
        }
    }
}
