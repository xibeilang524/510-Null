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
                    return Resource1.PaymentCode_APM;
                case PaymentCode.Computer:
                    return Resource1.PaymentCode_Computer;
                case PaymentCode.FunctionCard:
                    return Resource1.PaymentCode_FunctionCard;
                case PaymentCode.POS:
                    return Resource1.PaymentCode_POS;
                case PaymentCode.Internet:
                    return Resource1.PaymentCode_Internet;
                default:
                    return string.Empty;
            }
        }
    }
}
