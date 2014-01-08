using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Resouce
{
    public  class CardStatusDescription
    {
        /// <summary>
        /// 获取卡片状态的文字描述
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetDescription(CardStatus status)
        {
            switch (status)
            {
                case  CardStatus.Enabled :
                    return Resource1.CardStatus_Enabled;
                case  CardStatus.Deleted :
                    return Resource1.CardStatus_Deleted;
                case  CardStatus.Recycled :
                    return Resource1.CardStatus_Recycled;
                case CardStatus.Disabled :
                    return Resource1.CardStatus_Disabled;
                case  CardStatus.Loss :
                    return Resource1.CardStatus_Loss;
                default:
                    return string.Empty ;
            }
        }
    }
}
