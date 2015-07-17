using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 表示报警类型的文字描述
    /// </summary>
    public class AlarmTypeDescription
    {
        /// <summary>
        /// 获取报警信息的文字描述
        /// </summary>
        /// <param name="alarmType"></param>
        /// <returns></returns>
        public static string GetDescription(AlarmType alarmType)
        {
            switch (alarmType )
            {
                case AlarmType .CancelCardPayment :
                    return Resource1.AlarmType_CancelCardPayment;
                case  AlarmType.Closedoor :
                    return Resource1 .AlarmType_Closedoor ;
                case AlarmType.InvalidCard :
                    return Resource1 .AlarmType_InvalidCard ;
                case AlarmType.ModifyCardPayment :
                    return Resource1 .AlarmType_ModifyCardPayment ;
                case AlarmType.Opendoor :
                    return Resource1 .AlarmType_Opendoor ;
                case AlarmType.PrinterStatus :
                    return Resource1 .AlarmType_PrinterStatus ;
                case  AlarmType.APMBillValidator :
                    return Resource1.AlarmType_APMBillValidator;
                case AlarmType.APMCardReader :
                    return Resource1.AlarmType_APMCardReader;
                case AlarmType.APMCoinChanger :
                    return Resource1.AlarmType_APMCoinCharger;
                case AlarmType.APMKeyboard :
                    return Resource1.AlarmType_APMKeyboard;
                case AlarmType.APMPrinter :
                    return Resource1.AlarmType_APMPrinter;
                case AlarmType.APMSystem :
                    return Resource1.AlarmType_APMSystem;
                case AlarmType .OperatorLogIn :
                    return Resource1.AlarmType_OperatorLogIn;
                case AlarmType .CarArrive :
                    return Resource1.AlarmType_CarArrive;
                case  AlarmType.CarLeave :
                    return Resource1.AlarmType_CarLeave;
                case AlarmType.GateAlarm:
                    return Resource1.AlarmType_GateAlarm;
                case AlarmType.OperatorLogOut:
                    return Resource1.AlarmType_OperatorLogOut;
                case AlarmType.CardOutAnomaly:
                    return Resource1.AlarmType_CardOutAnomaly;
                case AlarmType.BarcodeGunStatus:
                    return Resource1.AlarmType_BarcodeGunStatus;
                case AlarmType.ServerSwitching:
                    return Resource1.AlarmType_ServerSwitching;
                case AlarmType.OperatorCardWork:
                    return Resource1.AlarmType_OperatorCardWork;
                case AlarmType.CarPlateFail:
                    return Resource1.AlarmType_CarPlateFail;
                default :
                    return string.Empty ;
            }
        }
    }
}
