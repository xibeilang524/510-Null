using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 表示停车场工作模式的描述
    /// </summary>
    public class WorkModeDescription
    {
        public static string GetDescription(ParkWorkMode mode)
        {
            switch (mode)
            {
                case ParkWorkMode.Fool:
                    return "在线模式";
                case ParkWorkMode.OffLine:
                    return "写卡模式";
                default:
                    return string.Empty;
            }
        }
    }
}
