using System;
using System.Collections.Generic;
using System.Text;

namespace Ralid.GeneralLibrary.CardReader.YCT
{
    internal enum YCTCommandType : byte
    {
        /// <summary>
        /// 读取序列号
        /// </summary>
        ReadSerialNumber=0x0E,
        /// <summary>
        /// 蜂鸣
        /// </summary>
        Beep=0x42,
        /// <summary>
        /// 获取版本号
        /// </summary>
        GetVersion=0x60,
        /// <summary>
        /// 设置服务商代码
        /// </summary>
        SetServiceCode=0x61,
        /// <summary>
        /// 设置消费参数
        /// </summary>
        InitPaidPara=0xB3,
        /// <summary>
        /// 询卡
        /// </summary>
        Poll=0xB4,
        /// <summary>
        /// 预消费
        /// </summary>
        Prepaid=0xB5,
        /// <summary>
        /// 消费
        /// </summary>
        CompletePaid=0xB6,
        /// <summary>
        /// 恢复未完成交易处理
        /// </summary>
        RestorePay=0xB7,
        /// <summary>
        /// 捕捉黑名单
        /// </summary>
        CatchBlack=0xB8,
        /// <summary>
        /// 进入BOOTLOAD状态
        /// </summary>
        Bootload=0xDF,    
    }
}
