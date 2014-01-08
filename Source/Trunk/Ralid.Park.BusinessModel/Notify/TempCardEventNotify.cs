using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.BusinessModel.Notify
{
    [DataContract]
    public class CarTypeSwitchNotify
    {
        /// <summary>
        /// 获取或设置控制器ID
        /// </summary>
        [DataMember]
        public int EntranceID{get;set;}

        /// <summary>
        /// 获取或设置车型
        /// </summary>
        [DataMember]
        public byte CarType{get;set;}

        public CarTypeSwitchNotify(int entranceID,byte carType)
        {
            this.EntranceID =entranceID ; 
            this.CarType = carType;
        }

        public CarTypeSwitchNotify()
        {
        }
    }
}
