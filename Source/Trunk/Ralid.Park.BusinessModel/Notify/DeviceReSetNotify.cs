using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Interface;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 控制器复位、在线查询指令,
    /// </summary>
    [DataContract]
    public class DeviceReSetNotify
    {
        /// <summary>
        /// 复位主机或要查询状态的主机的地址,
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }

        /// <summary>
        /// 1:主机、附机复位； 5：在线状态查询
        /// </summary>
        [DataMember]
        public byte ActionCode { get; set; }

        public DeviceReSetNotify(int entranceID,byte reset)
        {
            this.EntranceID = entranceID;
            this.ActionCode = reset;
        }

        public DeviceReSetNotify():this(1,1)
        {
        }
    }
}
