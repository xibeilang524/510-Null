using System;
using System.Collections.Generic;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public enum YCTCommandType : byte
    {
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
        Paid=0xB6,
        /// <summary>
        /// 未完成交易处理
        /// </summary>
        Repay=0xB7,
        /// <summary>
        /// 捕捉黑名单
        /// </summary>
        CatchBlack=0xB8,
    }
}
