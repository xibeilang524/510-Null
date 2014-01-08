using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 自助缴费机状态
    /// </summary>
    [Flags]
    public enum APMStatus
    {
        /// <summary>
        /// 状态正常
        /// </summary>
        Normal = 0x00,

        /// <summary>
        /// 停车场连接故障
        /// </summary>
        ParkingConnectFault = 0x02,

        /// <summary>
        /// 本地数据库故障
        /// </summary>
        LocalDBFault = 0x04,

        /// <summary>
        /// 纸币识别机故障?
        /// </summary>
        BillValidatorFault = 0x08,

        /// <summary>
        /// 硬币找零机故障
        /// </summary>
        CoinChangerFault = 0x10,

        /// <summary>
        /// 打印机缺纸
        /// </summary>
        PrinterPaperAbsent = 0x20,

        /// <summary>
        /// 打印机故障
        /// </summary>
        PrinterFault = 0x40,

        /// <summary>
        /// 读卡器故障
        /// </summary>
        ReaderFault = 0x80,

        /// <summary>
        /// 金属小键盘故障
        /// </summary>
        MetalKeyboardFault = 0x100,

        /// <summary>
        /// 硬币数量不足
        /// </summary>
        CoinShortage = 0x200,

        /// <summary>
        /// 钱箱已满
        /// </summary>
        CashBoxFull = 0x400,

        /// <summary>
        ///  登录停车场服务器失败 
        /// </summary>
        LoginParkingFault = 0x800

    }
}
