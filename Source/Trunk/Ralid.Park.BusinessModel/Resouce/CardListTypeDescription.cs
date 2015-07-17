using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 表示名单类型的描述类
    /// </summary>
    public class CardListTypeDescription
    {
        /// <summary>
        /// 获取名单类型的文字描述
        /// </summary>
        /// <param name="alarmType"></param>
        /// <returns></returns>
        public static string GetDescription(CardListType listType)
        {
            switch (listType)
            {
                case CardListType.Card:
                    return Resource1.CardListType_Card;
                case CardListType.CarPlate:
                    return Resource1.CardListType_CarPlate;
                default:
                    return string.Empty;
            }
        }
    }
}
