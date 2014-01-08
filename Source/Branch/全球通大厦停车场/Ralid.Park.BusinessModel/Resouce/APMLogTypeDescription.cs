using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    public class APMLogTypeDescription
    {
        public static string GetDescritption(APMLogType logType)
        {
            switch (logType)
            {
                case APMLogType.Message :
                    return Resource1.APMLogType_Message;
                case APMLogType.Alarm :
                    return Resource1.APMLogType_Alarm;
                case APMLogType.Error :
                    return Resource1.APMLogType_Error;
                default :
                    return Resource1.APMLogType_Message;
            }
        }
    }
}
