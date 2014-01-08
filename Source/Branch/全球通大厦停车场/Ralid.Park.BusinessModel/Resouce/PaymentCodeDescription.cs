using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 收费代码的文字描述类
    /// </summary>
    public class PaymentCodeDescription
    {
        /// <summary>
        /// 获取或设置收费类型的文字描述
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public static string GetDescription(PaymentCode pc)
        {
            switch (pc)
            {
                case PaymentCode.APM:
                    return "缴费机收费";
                case PaymentCode.Computer:
                    return "电脑收费";
                case PaymentCode.FunctionCard:
                    return "功能卡收费";
                default:
                    return string.Empty;
            }
        }
    }
}
