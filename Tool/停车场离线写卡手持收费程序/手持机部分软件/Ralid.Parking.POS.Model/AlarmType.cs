using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 报警类型
    /// </summary>
    public enum AlarmType
    {
        /// <summary>
        /// 无效卡片
        /// </summary>
        InvalidCard=1,
        /// <summary>
        /// 人工抬闸
        /// </summary>
        Opendoor=2,
        /// <summary>
        /// 人工落闸
        /// </summary>
        Closedoor=3,
        /// <summary>
        /// 修改收费记录
        /// </summary>
        ModifyCardPayment=4,
        /// <summary>
        /// 取消收费
        /// </summary>
        CancelCardPayment=5,
        /// <summary>
        /// 纸票打印机状态
        /// </summary>
        PrinterStatus=6,
        /// <summary>
        /// 自助缴费机纸币识别机警报
        /// </summary>
        APMBillValidator = 7,
        /// <summary>
        /// 自助缴费机硬币找零机警报
        /// </summary>
        APMCoinChanger = 8,
        /// <summary>
        /// 自助缴费机打印机警报
        /// </summary>
        APMPrinter = 9,
        /// <summary>
        /// 自助缴费机读卡器警报
        /// </summary>
        APMCardReader = 10,
        /// <summary>
        /// 自助缴费机密码键盘警报
        /// </summary>
        APMKeyboard = 11,
        /// <summary>
        /// 自助缴费机系统警报
        /// </summary>
        APMSystem = 12,
        /// <summary>
        /// 操作员登录
        /// </summary>
        OperatorLogIn=13,
        /// <summary>
        /// 车辆到达
        /// </summary>
        CarArrive=14,
        /// <summary>
        /// 车辆离开
        /// </summary>
        CarLeave=15
    }
}
