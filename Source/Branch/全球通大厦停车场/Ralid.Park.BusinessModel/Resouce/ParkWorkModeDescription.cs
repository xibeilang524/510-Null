using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum;
namespace Ralid.Park.BusinessModel.Resouce
{
    public class ParkWorkModeDescription
    {
        public static string GetDescription(ParkWorkMode workMode)
        {
            switch (workMode)
            {
                case ParkWorkMode.OffLine:
                    return Resource1.ParkWorkmode_Offline;
                case ParkWorkMode.Fool:
                    return Resource1.ParkWorkmode_Online;
                default:
                    return string.Empty;
            }
        }
    }
}
