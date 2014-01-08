using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{

    public class APMStatusDescription
    {
        public static string GetDescription(APMStatus status)
        {
            string temp = string.Empty;
            if (status == APMStatus.Normal) return temp += Resource1.APMStatus_Normal; //如果正常就不往下判断了
            if ((status & APMStatus.ParkingConnectFault) == APMStatus.ParkingConnectFault) temp += Resource1.APMStatus_ParkingConnectedFault + ",";
            if ((status & APMStatus.LocalDBFault) == APMStatus.LocalDBFault) temp += Resource1.APMStatus_LocalDBFault + ",";
            if ((status & APMStatus.MetalKeyboardFault) == APMStatus.MetalKeyboardFault) temp += Resource1.APMStatus_MetalKeyboardFault + ",";
            if ((status & APMStatus.CashBoxFull) == APMStatus.CashBoxFull) temp += Resource1.APMStatus_CashBoxFull + ",";
            if ((status & APMStatus.BillValidatorFault) == APMStatus.BillValidatorFault) temp += Resource1.APMStatus_BillValidatorFault + ",";
            if ((status & APMStatus.CoinChangerFault) == APMStatus.CoinChangerFault) temp += Resource1.APMStatus_CoinChangerFault + ",";
            if ((status & APMStatus.CoinShortage) == APMStatus.CoinShortage) temp += Resource1.APMStatus_CoinShortage + ",";
            if ((status & APMStatus.PrinterFault) == APMStatus.PrinterFault) temp += Resource1.APMStatus_PrinterFault + ",";
            if ((status & APMStatus.PrinterPaperAbsent)==APMStatus .PrinterPaperAbsent )temp+=Resource1 .APMStatus_PrinterPaperAbsent +",";
            if ((status & APMStatus.ReaderFault) == APMStatus.ReaderFault) temp += Resource1.APMStatus_ReaderFault + ",";
            if ((status & APMStatus.LoginParkingFault) == APMStatus.LoginParkingFault) temp += Resource1.APMStatus_LoginParkingFault + ",";
            if (!string.IsNullOrEmpty(temp)) temp = temp.Substring(0, temp.Length - 1);
            return temp;
        }
    }
}
