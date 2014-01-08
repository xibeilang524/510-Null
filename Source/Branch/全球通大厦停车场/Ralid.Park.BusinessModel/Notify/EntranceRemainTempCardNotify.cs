using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Notify
{

    [Serializable]
    [DataContract]
    public class EntranceRemainTempCardNotify
    {
        public EntranceRemainTempCardNotify()
        {
        }

        public EntranceRemainTempCardNotify(int parkID,int entranceID, int remainTempCard)
        {
            ParkID = parkID;
            EntranceID = entranceID;
            RemainTempCard = remainTempCard;
        }

        /// <summary>
        /// 获取或设置停车场ID
        /// </summary>
        [DataMember]
        public int ParkID{get;set;}

        /// <summary>
        /// 获取或设置通道地址
        /// </summary>
        [DataMember]
        public int EntranceID{get;set;}

        /// <summary>
        /// 获取或设置临时卡数量
        /// </summary>
        [DataMember]
        public int RemainTempCard{get;set;}
    }
}
