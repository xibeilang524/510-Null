using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 出入口工作模式选项枚举
    /// </summary>
    [DataContract]
    [Flags]
    public enum EntranceWorkmodeOption
    {
        /// <summary>
        /// 无
        /// </summary>
        [EnumMember]
        OPT_None = 0,
        /// <summary>
        /// 主控制器
        /// </summary>
        [EnumMember]
        OPT_IsMaster = 0x02,
        /// <summary>
        /// 出口控制器
        /// </summary>
        [EnumMember]
        OPT_IsExitDevice = 0x04,
        /// <summary>
        /// 取/读卡要求压地感
        /// </summary>
        [EnumMember]
        OPT_TakeCardNeedCarSense = 0x20,
        /// <summary>
        /// 允许取卡后刷卡,即发卡机吐卡后如果未读到卡片，允许用户自行刷卡，而不用再收回
        /// </summary>
        [EnumMember]
        OPT_AllowEjectCardWhithoutRead = 0x40,
        /// <summary>
        /// 压地感开补光灯
        /// </summary>
        [EnumMember]
        OPT_LightEnable = 0x80,
        /// <summary>
        /// 不允许过期卡片出场
        /// </summary>
        [EnumMember]
        OPT_ForbidExitWhenCardExpired = 0x100,
        /// <summary>
        /// 出口收费
        /// </summary>
        [EnumMember]
        OPT_ExportCharge = 0x200,
        /// <summary>
        /// 卡不在名单中不转在线处理
        /// </summary>
        [EnumMember]
        OPT_NotOnlineHandleWhenNotOnList=0x400,
        /// <summary>
        /// 车场满时禁止进场
        /// </summary>
        [EnumMember]
        OPT_ForbidEnterWhenFull = 0x800,
        /// <summary>
        /// 禁用临时卡
        /// </summary>
        [EnumMember]
        OPT_DisableTempCard = 0x1000,
        /// <summary>
        /// 使用韦根34
        /// </summary>
        [EnumMember]
        OPT_Wiegand34 = 0x2000,
        /// <summary>
        /// 不进行车位计数
        /// </summary>
        [EnumMember]
        OPT_NoParkingCount = 0x4000,
        /// <summary>
        /// 控制器有效
        /// </summary>
        [EnumMember]
        OPT_Valid = 0x8000,


        #region 以下选项不用下发到控制器
        /// <summary>
        /// 保留字段1
        /// </summary>
        [EnumMember]
        Reserve1 = 0x800000,

        //#region 2013-5-10 增加的几个选项,这几个选项不用下发到控制器
        /// <summary>
        /// 出口收卡机内没有安装读卡器
        /// </summary>
        [EnumMember]
        NoReaderOnCardCaptuer = 0x1000000,
        /// <summary>
        /// 当按下取卡按钮时只响应临时卡读头的读卡事件
        /// </summary>
        [EnumMember]
        OnlyTempReaderAfterButtonClick = 0x2000000,
        /// <summary>
        /// 卡片有效命令需等待控制板返回执行结果
        /// </summary>
        [EnumMember]
        CardValidNeedResponse = 0x4000000,
        /// <summary>
        /// 月卡类出场需要确认
        /// </summary>
        [EnumMember]
        MonthCardWaitWhenOut = 0x8000000,
        /// <summary>
        ///储值卡类出场需要确认
        /// </summary>
        [EnumMember]
        PrepayCardWaitWhenOut = 0x10000000,
        /// <summary>
        /// 车场有余位显示屏
        /// </summary>
        [EnumMember]
        EnableParkvacantLed = 0x20000000,
        #endregion
        /// <summary>
        /// 将此通道当成门禁方式来用，启用这一选项时，卡片进出场不会改变卡片的状态，只记录卡片的进出记录
        /// </summary>
        [EnumMember]
        UseAsACS = 0x40000000,
    }
}
