using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 车辆信息LED屏显示信息描述类
    /// </summary>
    public class VehicleLEDMessageTypeDescription
    {
        /// <summary>
        /// 获取车辆信息LED屏显示信息的文字描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDescription(VehicleLEDMessageType type)
        {
            switch (type)
            {
                case VehicleLEDMessageType.Department:
                    return Resource1.VehicleLEDMessageType_Department;
                case VehicleLEDMessageType.OwnerName:
                    return Resource1.VehicleLEDMessageType_OwnerName;
                case VehicleLEDMessageType.CardCarPlate:
                    return Resource1.VehicleLEDMessageType_CardCarPlate;
                case VehicleLEDMessageType.RegCarPlate:
                    return Resource1.VehicleLEDMessageType_RegCarPlate;
                case VehicleLEDMessageType.CardCertificate:
                    return Resource1.VehicleLEDMessageType_CardCertificate;
                case VehicleLEDMessageType.LastCarPlate:
                    return Resource1.VehicleLEDMessageType_LastCarPlate;
                case VehicleLEDMessageType.LastDateTime:
                    return Resource1.VehicleLEDMessageType_LastDateTime;
                case VehicleLEDMessageType.LastEntrance:
                    return Resource1.VehicleLEDMessageType_LastEntrance;
                case VehicleLEDMessageType.ValidDate:
                    return Resource1.VehicleLEDMessageType_ValidDate;
                case VehicleLEDMessageType.Balance:
                    return Resource1.VehicleLEDMessageType_Balance;
                case VehicleLEDMessageType.TotalPosition:
                    return Resource1.VehicleLEDMessageType_TotalPosition;
                case VehicleLEDMessageType.Vacant:
                    return Resource1.VehicleLEDMessageType_Vacant;
                case VehicleLEDMessageType.Park:
                    return Resource1.VehicleLEDMessageType_Park;
                case VehicleLEDMessageType.Entrance:
                    return Resource1.VehicleLEDMessageType_Entrance;
                default:
                    return string.Empty;
            }
        }
    }
}
