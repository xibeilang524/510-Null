using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Resouce
{
    public class EntranceStatusDescription
    {
        public static string GetDescription(EntranceStatus status)
        {
            switch (status)
            {
                case EntranceStatus.CardJam:
                    return Resource1.EnStatus_CardJam;
                case EntranceStatus.Error:
                    return Resource1.EnStatus_Error;
                case EntranceStatus.GateDown:
                    return Resource1.EnStatus_GateDown;
                case EntranceStatus.GateUp:
                    return Resource1.EnStatus_GateUp;
                case EntranceStatus.LessCard:
                    return Resource1.EnStatus_LessCard;
                case EntranceStatus.NoCard:
                    return Resource1.EnStatus_NoCard;
                case EntranceStatus.OffLine:
                    return Resource1.EnSatus_OffLine;
                case EntranceStatus.Ok:
                    return Resource1.EnStatus_OK;
                case EntranceStatus.StorageAlarm:
                    return Resource1.EnStatus_StorageAlarm;
                case EntranceStatus.StorageFull:
                    return Resource1.EnStatus_StorageFull;
                default:
                    return string.Empty;
            }
        }
    }
}
