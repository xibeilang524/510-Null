using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 卡片的停车状态
    /// </summary>
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
        In = 0x80,

        /// <summary>
        /// 64-酒店未退房（当酒店应用启用时有效）
        /// </summary>
        NotCheckOut = 0x40,

        /// <summary>
        /// 32-酒店应用
        /// </summary>
        HotelApp = 0x20,

        /// <summary>
        /// 16-固定/月租/储值等过期欠费卡用作临时卡,入场或出场事件中标定为用作临时卡
        /// </summary>
        AsTempCard = 0x10,

        /// <summary>
        /// 8-入场或出场事件中标定已停过室内停车场
        /// </summary>
        NestedParkMarked = 0x08,

        /// <summary>
        /// 4-已入内车场
        /// </summary>
        IndoorIn = 0x04,

        /// <summary>
        /// 2-中央收费已交费(网络型停车场用于第1层车场即外车场收费标识，包括中央收费和出口收费)
        /// </summary>
        PaidBill = 0x02,

        /// <summary>
        /// 1-区内消费优惠
        /// </summary>
        Consume = 0x01,

        /// <summary>
        /// 0-已出场
        /// </summary>
        Out = 0
    }
}
