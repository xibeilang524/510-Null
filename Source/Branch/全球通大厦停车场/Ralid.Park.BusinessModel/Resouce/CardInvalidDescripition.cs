using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    public class CardInvalidDescripition
    {
        public static string GetDescription(Ralid.Park.BusinessModel.Enum.EventInvalidType invalidType)
        {
            switch (invalidType)
            {
                case EventInvalidType.INV_Invalid:
                    return Resource1.INV_Invalid;
                case EventInvalidType.INV_Type:
                    return Resource1.INV_Type;
                case EventInvalidType.INV_OverDate:
                    return Resource1.INV_OverDate;
                case EventInvalidType.INV_HaveIn:
                    return Resource1.INV_HaveIn;
                case EventInvalidType.INV_StillOut:
                    return Resource1.INV_StillOut;
                case EventInvalidType.INV_ForbidTempCard:
                    return Resource1.INV_ForbidTempCard;
                case EventInvalidType.INV_ParkFull:
                    return Resource1.INV_ParkFull;
                case EventInvalidType.INV_DisableNestedPark:
                    return Resource1.INV_DisableNestedPark;
                case EventInvalidType.INV_ParkNumError:
                    return Resource1.INV_ParkNumError;
                case EventInvalidType.INV_CarPlateWrong:
                    return Resource1.INV_CarPlateWrong;
                case EventInvalidType.INV_CarPlateWrongWithPaid:
                    return Resource1.INV_CarPlateWrongWithPaid;
                case EventInvalidType.IVN_NotPaid:
                    return Resource1.IVN_NotPaid;
                case EventInvalidType.INV_OverTime:
                    return Resource1.INV_OverTime;
                case EventInvalidType.INV_DataError:
                    return Resource1.INV_DataError;
                case EventInvalidType.INV_VersionError:
                    return Resource1.INV_VersionError;
                case EventInvalidType.INV_NoCarType:
                    return Resource1.INV_NoCarType;
                case EventInvalidType.INV_NoTariff:
                    return Resource1.INV_NoTariff;
                case EventInvalidType.INV_WrongPaidTime:
                    return Resource1.INV_WrongPaidTime;
                case EventInvalidType.INV_InsertToRecovery:
                    return Resource1.INV_InsertToRecovery;
                case EventInvalidType.INV_Cancelled:
                    return Resource1.INV_Cancelled;
                case EventInvalidType.INV_UnRegister:
                    return Resource1.INV_UnRegister;
                case EventInvalidType.INV_Recycled:
                    return Resource1.INV_Recycled;
                case EventInvalidType.INV_Loss:
                    return Resource1.INV_Loss;
                case EventInvalidType.INV_Lock:
                    return Resource1.INV_Lock;
                case EventInvalidType.INV_WrongInTime:
                    return Resource1.INV_WrongInTime;
                case EventInvalidType.INV_InvalidImg:
                    return Resource1.INV_InvalidImg;
                case EventInvalidType.INV_HolidayDisabled:
                    return Resource1.INV_HolidayDisabled;
                case EventInvalidType.INV_NotActive:
                    return Resource1.INV_NotActive;
                case EventInvalidType.INV_NoAccessRight:
                    return Resource1.INV_NoAccessRight;
                case EventInvalidType .INV_Balance :
                    return Resource1.INV_Balance;
                case EventInvalidType .INV_ReadCard :
                    return Resource1.INV_ReadCard;
                default:
                    return Resource1.INV_Unknow;
            }
        }
    }
}
