using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Resouce
{
    public class PaymentModeDescription
    {
        /// <summary>
        /// 获取或设置收费类型的文字描述
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static string GetDescription(PaymentMode pm)
        {
            switch (pm)
            {
                case PaymentMode.Cash :
                    return Resource1.PaymentMode_Cash;
                case PaymentMode.Pos :
                    return Resource1.PaymentMode_Pos;
                case PaymentMode.Prepay :
                    return Resource1.PaymentMode_Prepay;
                case PaymentMode.YangChengTong :
                    return Resource1.PaymentMode_YangChengTong;
                case PaymentMode .ZhongShanTong :
                    return Resource1.PaymentMode_ZhongShanTong;
                default :
                    return string.Empty;
            }
        }
    }
}
