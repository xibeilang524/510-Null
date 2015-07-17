using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 下发状态描述类
    /// </summary>
    public class WaitingCommandStatusDescription
    {
        public static string GetDescription(WaitingCommandStatus status)
        {
            switch (status)
            {
                case WaitingCommandStatus.Waiting:
                    return Resource1.WaitingCommandStatus_Wait;
                case WaitingCommandStatus.Fail:
                    return Resource1.WaitingCommandStatus_Fail;
                case WaitingCommandStatus.Success:
                    return Resource1.WaitingCommandStatus_Success;
                default:
                    return string.Empty;
            }
        }
    }
}
