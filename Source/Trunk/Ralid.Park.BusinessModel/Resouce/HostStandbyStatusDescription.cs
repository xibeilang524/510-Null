using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 服务器主从状态送状态描述类
    /// </summary>
    public class HostStandbyStatusDescription
    {
        /// <summary>
        /// 获取服务器主从状态送状态的文字描述
        /// </summary>
        /// <param name="alarmType"></param>
        /// <returns></returns>
        public static string GetDescription(HostStandbyStatus status)
        {
            switch (status)
            {
                case HostStandbyStatus.Host:
                    return Resource1.HostStandbyStatus_Host;
                case HostStandbyStatus.Standby:
                    return Resource1.HostStandbyStatus_Standby;
                default:
                    return string.Empty;
            }
        }
    }
}
