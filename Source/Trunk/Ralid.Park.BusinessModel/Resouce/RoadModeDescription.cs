using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 通道模式描述类
    /// </summary>
    public class RoadModeDescription
    {
        public static string GetDescritption(RoadMode mode)
        {
            switch (mode)
            {
                case RoadMode.None:
                    return Resource1.RoadMode_None;
                case RoadMode.Exit:
                    return Resource1.RoadMode_Exit;
                case RoadMode.Entrance:
                    return Resource1.RoadMode_Entrance;
                default:
                    return string.Empty;
            }
        }
    }
}
