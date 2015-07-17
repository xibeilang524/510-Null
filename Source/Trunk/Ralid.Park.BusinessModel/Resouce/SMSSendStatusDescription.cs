using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 短信发送状态描述类
    /// </summary>
    public class SMSSendStatusDescription
    {
        /// <summary>
        /// 获取短信发送状态的文字描述
        /// </summary>
        /// <param name="alarmType"></param>
        /// <returns></returns>
        public static string GetDescription(SMSSendStatus status)
        {
            switch (status)
            {
                case SMSSendStatus.NotSend:
                    return Resource1.SMSSendStatus_NotSend;
                case SMSSendStatus.Waiting:
                    return Resource1.SMSSendStatus_Waiting;
                case SMSSendStatus.Success:
                    return Resource1.SMSSendStatus_Success;
                case SMSSendStatus.Fail:
                    return Resource1.SMSSendStatus_Fail;
                default:
                    return string.Empty;
            }
        }
    }
}
