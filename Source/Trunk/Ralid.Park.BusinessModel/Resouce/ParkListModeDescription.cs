using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 表示控制板进出凭证名单模式的描述类
    /// </summary>
    public class ParkListModeDescription
    {
        /// <summary>
        /// 获取控制板进出凭证名单模式的文字描述
        /// </summary>
        /// <param name="alarmType"></param>
        /// <returns></returns>
        public static string GetDescription(ParkListMode listType)
        {
            switch (listType)
            {
                case ParkListMode.Card:
                    return Resource1.ParkListMode_Card;
                case ParkListMode.CarPlate:
                    return Resource1.ParkListMode_CarPlate;
                case ParkListMode.CarPlateAndCard:
                    return Resource1.ParkListMode_CarPlateAndCard;
                default:
                    return string.Empty;
            }
        }
    }
}
