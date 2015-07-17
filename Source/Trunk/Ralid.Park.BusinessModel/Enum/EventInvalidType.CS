using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    public enum EventInvalidType
    {
        /// <summary>
        /// 成功
        /// </summary>
        INV_Success = 0x00,
        /// <summary>
        /// 无效卡片
        /// </summary>
        INV_Invalid = 0x1,
        /// <summary>
        /// 卡片类型不符
        /// </summary>
        INV_Type = 0x2,
        /// <summary>
        /// 此卡已过期
        /// </summary>
        INV_OverDate = 0x3,
        /// <summary>
        /// 此卡已在场
        /// </summary>
        INV_HaveIn = 0x4,
        /// <summary>
        /// 此卡已出场
        /// </summary>
        INV_StillOut = 0x5,
        /// <summary>
        /// 停用临时卡
        /// </summary>
        INV_ForbidTempCard = 0x6,
        /// <summary>
        /// 车位已满
        /// </summary>
        INV_ParkFull = 0x7,
        /// <summary>
        /// 不允许进出嵌套车场
        /// </summary>
        INV_DisableNestedPark = 0x8,
        /// <summary>
        /// 车场编号不符
        /// </summary>
        INV_ParkNumError = 0x09,
        /// <summary>
        /// 车牌识别失败
        /// </summary>
        INV_CarPlateWrong = 0x11,
        /// <summary>
        /// 已缴费，但车牌不相符
        /// </summary>
        INV_CarPlateWrongWithPaid = 0x12,
        /// <summary>
        /// 没有缴费
        /// </summary>
        IVN_NotPaid = 0x13,
        /// <summary>
        /// 滞留超时，请重新缴费
        /// </summary>
        INV_OverTime = 0x14,
        /// <summary>
        /// 卡片数据校验错误
        /// </summary>
        INV_DataError = 0x15,
        /// <summary>
        /// 卡片版本不符
        /// </summary>
        INV_VersionError = 0x16,
        /// <summary>
        /// 未找到车型
        /// </summary>
        INV_NoCarType = 0x1B,
        /// <summary>
        /// 未设置停车费率
        /// </summary>
        INV_NoTariff = 0x1C,
        /// <summary>
        /// 缴费时间晚于出场时间
        /// </summary>
        INV_WrongPaidTime = 0x1E,
        /// <summary>
        /// 请插卡回收
        /// </summary>
        INV_InsertToRecovery = 0x1F,
        /// <summary>
        /// 此卡已注销
        /// </summary>
        INV_Cancelled = 0x21,
        /// <summary>
        /// 此卡待发行
        /// </summary>
        INV_Recycled = 256,
        /// <summary>
        /// 此卡已挂失
        /// </summary>
        INV_Loss = 257,
        /// <summary>
        /// 此卡已锁定
        /// </summary>
        INV_Lock = 258,
        /// <summary>
        /// 此卡未登记
        /// </summary>
        INV_UnRegister = 259,
        /// <summary>
        /// 图像对比失败
        /// </summary>
        INV_InvalidImg = 260,
        /// <summary>
        /// 入场时间晚于出场时间
        /// </summary>
        INV_WrongInTime = 261,
        /// <summary>
        /// 节假日不允许进入
        /// </summary>
        INV_HolidayDisabled = 262,
        /// <summary>
        /// 卡片未到生效期
        /// </summary>
        INV_NotActive = 263,
        /// <summary>
        /// 权限不足
        /// </summary>
        INV_NoAccessRight = 264,
        /// <summary>
        /// 卡片余额不足
        /// </summary>
        INV_Balance = 265,
        /// <summary>
        /// 请继续读卡，用于作读写器使用
        /// </summary>
        INV_ReadCard = 266,
        /// <summary>
        /// 超速行驶违章
        /// </summary>
        INV_Speeding = 267,
        /// <summary>
        /// 车牌识别失败
        /// </summary>
        INV_CarPlateFail = 0x26,
        /// <summary>
        /// 不在名单中
        /// </summary>
        INV_NotOnTheList = 0x27,
        /// <summary>
        /// 已过有效期
        /// </summary>
        INV_Expired = 0x28,
        /// <summary>
        /// 无进入权限
        /// </summary>
        INV_NoAccess = 0x29,
        /// <summary>
        /// 名单类型不符
        /// </summary>
        INV_ListType = 0x2A,
        /// <summary>
        /// 名单未启用
        /// </summary>
        INV_ListNotEnabled = 0x2B,
        /// <summary>
        /// 车辆已入场
        /// </summary>
        INV_CarIsIn = 0x2C,
        /// <summary>
        /// 车辆已出场
        /// </summary>
        INV_CarIsOut = 0x2D,
    }
}
