using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 表示停车场控制板类型的文字描述
    /// </summary>
    public class EntranceDeviceTypeDescription
    {
        public static string GetDescription(EntranceDeviceType et)
        {
            switch (et)
            {
                case EntranceDeviceType.CANEntrance:
                    return Resource1.EntranceDeviceType_CAN;
                case EntranceDeviceType.NETEntrance:
                    return Resource1.EntranceDeviceType_NET;
                default:
                    return string.Empty;
            }
        }
    }
}
