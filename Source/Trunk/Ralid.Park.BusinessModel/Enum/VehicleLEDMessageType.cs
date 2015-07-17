using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 车辆信息LED屏显示信息类型枚举
    /// </summary>
    [DataContract]
    public enum VehicleLEDMessageType
    {
        /// <summary>
        /// 不显示信息
        /// </summary>
        [EnumMember]
        None = 0,

        /// <summary>
        /// 部门名称
        /// </summary>
        [EnumMember]
        Department = 1,

        /// <summary>
        /// 车主姓名
        /// </summary>
        [EnumMember]
        OwnerName = 2,

        /// <summary>
        /// 登记车牌
        /// </summary>
        [EnumMember]
        CardCarPlate = 3,

        /// <summary>
        /// 识别车牌
        /// </summary>
        [EnumMember]
        RegCarPlate=4,

        /// <summary>
        /// 卡片编号
        /// </summary>
        [EnumMember]
        CardCertificate = 5,

        /// <summary>
        /// 入场车牌
        /// </summary>
        [EnumMember]
        LastCarPlate = 6,

        /// <summary>
        /// 入场时间
        /// </summary>
        [EnumMember]
        LastDateTime = 7,

        /// <summary>
        /// 入场通道
        /// </summary>
        [EnumMember]
        LastEntrance = 8,

        /// <summary>
        /// 有效期限
        /// </summary>
        [EnumMember]
        ValidDate = 9,

        /// <summary>
        /// 储值余额
        /// </summary>
        [EnumMember]
        Balance = 10,

        /// <summary>
        /// 总车位数
        /// </summary>
        [EnumMember]
        TotalPosition = 11,

        /// <summary>
        /// 空车位数
        /// </summary>
        [EnumMember]
        Vacant = 12,

        /// <summary>
        /// 停车场名
        /// </summary>
        [EnumMember]
        Park = 13,

        /// <summary>
        /// 通道名称
        /// </summary>
        [EnumMember]
        Entrance = 14,
    }
}
