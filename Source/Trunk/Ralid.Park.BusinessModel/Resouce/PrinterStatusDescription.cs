using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary .Printer ;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 表示打印机状态的文字描述
    /// </summary>
    public class PrinterStatusDescription
    {
        public static string GetDescription(PrinterStatus status)
        {
            switch (status)
            {
                case  PrinterStatus.Unknown :
                    return Resource1.PrinterStatus_Unknown;
                case PrinterStatus.COMPortNotOpen :
                    return Resource1.PrinterStatus_COMPortNotOpen;
                case PrinterStatus.OffLine :
                    return Resource1.PrinterStatus_OffLine;
                case PrinterStatus.Ok :
                    return Resource1.PrinterStatus_Ok;
                case PrinterStatus .PaperAbsent :
                    return Resource1.PrinterStatus_PaperAbsent;
                default :
                    return Resource1.PrinterStatus_Unknown;
            }
        }
    }
}
