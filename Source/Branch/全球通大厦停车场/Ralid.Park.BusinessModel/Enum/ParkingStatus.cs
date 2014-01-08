using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 卡片的停车状态
    /// </summary>
    [DataContract]
    [Flags]
    public enum ParkingStatus
    {
        ///// <summary>
        ///// 256-室内停车场已缴费（用于网络型停车场第2层车场收费标识）
        ///// </summary>
        //[EnumMember]
        //IndoorPaid = 0x100,

        /// <summary>
        /// 128-已入场
        /// </summary> 
        [EnumMember]
        In = 0x80,

        /// <summary>
        /// 64-重复入场
        /// </summary>
        [EnumMember]
        RepeatIn = 0x40,

        /// <summary>
        /// 32-重复出场
        /// </summary>
        [EnumMember]
        RepeatOut = 0x20,

        /// <summary>
        /// 16-固定/月租/储值等过期欠费卡用作临时卡,入场或出场事件中标定为用作临时卡
        /// </summary>
        [EnumMember]
        AsTempCard = 0x10,

        /// <summary>
        /// 8-入场或出场事件中标定已停过室内停车场
        /// </summary>
        [EnumMember]
        NestedParkMarked = 0x08,

        /// <summary>
        /// 4-已入内车场
        /// </summary>
        [EnumMember]
        IndoorIn = 0x04,

        /// <summary>
        /// 2-中央收费已交费(网络型停车场用于第1层车场即外车场收费标识，包括中央收费和出口收费)
        /// </summary>
        [EnumMember]
        PaidBill = 0x02,

        /// <summary>
        /// 1-区内消费优惠
        /// </summary>
        [EnumMember]
        Consume = 0x01,

        /// <summary>
        /// 0-已出场
        /// </summary>
        [EnumMember]
        Out = 0
    }
}
