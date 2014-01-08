using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 操作员的权限枚举
    /// </summary>
    public enum Permission
    {
        #region 卡片
        /// <summary>
        /// 查看卡片信息
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "查看卡片信息")]
        ReadCards = 0,
        /// <summary>
        /// 编辑卡片信息
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "修改卡片信息")]
        EditCard = 1,
        /// <summary>
        /// 发行卡片
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片发行")]
        ReleaseCard = 2,
        /// <summary>
        /// 卡片充值
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片充值")]
        ChargeCard = 3,
        /// <summary>
        /// 卡片延期
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片延期")]
        DeferCard = 4,
        /// <summary>
        /// 卡片禁用
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片禁用")]
        DisableCard = 5,
        /// <summary>
        /// 卡片启用
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片启用")]
        EnableCard = 6,
        /// <summary>
        /// 卡片挂失
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片挂失")]
        LostCard = 7,
        /// <summary>
        /// 卡片恢复
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片恢复")]
        RestoreCard = 8,
        /// <summary>
        /// 卡片回收
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡片回收")]
        RecycleCard = 9,
        /// <summary>
        /// 批量加卡
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "批量加卡")]
        AddCards = 11,
        /// <summary>
        /// 下载全部卡片
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "下载全部卡片")]
        DownloadAllCards = 14,

        /// <summary>
        /// 导出卡片
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "导出卡片")]
        ExportCards = 67,

        /// <summary>
        /// 批量修改
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "批量修改")]
        CardBulkChange = 68,

        /// <summary>
        /// 卡号转换
        /// </summary>
        [OperatorRight(Catalog = "Card", Description = "卡号转换")]
        CardIDConvert = 69,
        #endregion

        #region 停车场及其硬件
        /// <summary>
        /// 编辑停车场信息
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "编辑停车场信息")]
        EditPark = 15,
        ///// <summary>
        ///// 同步数据库
        ///// </summary>
        //[OperatorRight(Catalog = "Hardware", Description = "同步数据库")]
        //SyncDataBase = 16,
        /// <summary>
        /// 编辑停车区域信息
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "编辑停车区域信息")]
        EditDivision = 17,
        /// <summary>
        /// 编辑区域控制器信息
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "编辑控制器信息")]
        EditEntrance = 18,
        /// <summary>
        /// 临时卡数量设置
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "临时卡数量设置")]
        TempCardSetting = 49,
        /// <summary>
        /// 抬闸
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "抬闸")]
        OpenDoor = 19,
        /// <summary>
        /// 落闸
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "落闸")]
        CloseDoor = 20,
        /// <summary>
        /// 控制器复位
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "控制器复位")]
        ResetEntrance = 21,
        /// <summary>
        ///远程入场 
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "远程读卡")]
        RemoteReadCard = 22,
        /// <summary>
        /// 编辑摄像机信息
        /// </summary>
        [OperatorRight(Catalog = "Hardware", Description = "编辑摄像机信息")]
        EditVideo = 24,
        #endregion

        #region 系统数据
        /// <summary>
        /// 卡片进出收费
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "操作员结算")]
        OperatorSettle = 58,
        /// <summary>
        /// 对其它操作员结算
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "对其它操作员结算")]
        OtherOperatorSettle = 59,
        /// <summary>
        /// 中山通设置
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "中山通设置")]
        ZSTSetting = 70,
        /// <summary>
        /// 查看系统设置
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "查看系统设置")]
        ReadSysSetting = 25,
        /// <summary>
        /// 编辑系统设置
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "编辑系统设置")]
        EditSysSetting = 26,
        /// <summary>
        /// 查看工作站信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "查看工作站信息")]
        ReadWorkstation = 27,
        /// <summary>
        /// 编辑工作站信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "编辑工作站信息")]
        EditWorkstation = 28,
        /// <summary>
        /// 当前工作站设置
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "当前工作站设置")]
        SystemIDSetting = 46,
        /// <summary>
        /// 实时监控
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "实时监控")]
        VideoMonitor = 47,
        /// <summary>
        /// 实时监控
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "实时事件")]
        RealEvent =62,
        /// <summary>
        /// 卡片进出收费
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "卡片进出收费")]
        CardPaying = 48,
        /// <summary>
        /// 读取自助缴费机信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "读取自助缴费机信息")]
        ReadAPM=55,

        /// <summary>
        /// 编辑自助缴费机信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "编辑自助缴费机信息")]
        EditAPM=56,

        /// <summary>
        /// 查看操作员信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "查看操作员信息")]
        ReadOperaotor = 30,
        /// <summary>
        /// 编辑操作员信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "编辑操作员信息")]
        EditOperator = 31,
        /// <summary>
        /// 查看角色信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "查看角色信息")]
        ReadRole = 32,
        /// <summary>
        /// 编辑角色信息
        /// </summary>
        [OperatorRight(Catalog = "Data", Description = "编辑角色信息")]
        EditRole = 33,
        #endregion

        #region 报表
        /// <summary>
        /// 卡片充值报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "卡片充值报表")]
        CardChargeReport = 34,
        /// <summary>
        /// 卡片延期报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "卡片延期报表")]
        CardDeferReport = 35,
        /// <summary>
        /// 月租卡收费统计
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "月租卡收费统计")]
        CardDeferStatistics =60 ,
        /// <summary>
        /// 卡片发行报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "卡片发行报表")]
        CardReleaseReport = 36,
        /// <summary>
        /// 停车收费报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "停车收费报表")]
        CardPayingReport = 37,
        /// <summary>
        /// 停车收费统计
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "停车收费统计")]
        CardPayingStatistics = 61,
        /// <summary>
        /// 卡片挂失/恢复报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "卡片挂失/恢复报表")]
        CardLossRestoreReport = 38,
        /// <summary>
        /// 卡片禁用/启用报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "卡片禁用/启用报表")]
        CardDisableEnableReport = 39,
        /// <summary>
        /// 卡片回收报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "卡片回收报表")]
        CardRecycleReport = 40,
        /// <summary>
        /// 卡片删除报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "卡片删除报表")]
        CardDeleteReport = 64,
        /// <summary>
        /// 车流量统计
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "车流量统计")]
        CarFlowStatistics = 42,
        /// <summary>
        /// 系统报警报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "系统报警报表")]
        AlarmReport = 43,
        /// <summary>
        /// 车辆进出报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "车辆进出报表")]
        CardEventReport = 44,
        /// <summary>
        /// 在场卡片报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "车辆进出报表")]
        CardInparkReport=57,
        /// <summary>
        /// 操作员交接班报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "操作员交接班报表")]
        OperatorShiftStatistics = 45,

        /// <summary>
        /// 导出车辆收费报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "导出车辆收费报表")]
        ExportCardPayment = 50,

        /// <summary>
        /// 导出月卡收费报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "导出月卡收费报表")]
        ExportMonthCardPaymentReport = 51,

        /// <summary>
        /// 打印车辆收费报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "打印车辆收费报表")]
        PrintCardPayment = 65,

        /// <summary>
        /// 打印月卡收费报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "打印月卡收费报表")]
        PrintMonthCardPaymentReport = 66,

        /// <summary>
        /// 导出车辆收费报表
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "打印操作员结算单")]
        PrintOperatorSettleLog = 63,

        /// <summary>
        /// 修改卡片收费记录
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "修改卡片收费记录")]
        ModifyCardPaymentRecord = 52,

        ///// <summary>
        ///// 车类型汇总报表
        ///// </summary>
        //[OperatorRight(Catalog = "Report", Description = "车类型汇总查询")]
        CarTypeReportDetail = 53,

        /// <summary>
        /// 自助缴费机日志查询
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "自助缴费机日志查询")]
        PayOperationLogReport = 54,

        /// <summary>
        /// 羊城通扣费记录查询
        /// </summary>
        [OperatorRight(Catalog = "Report", Description = "羊城通扣费记录查询")]
        YangChenTongLogReport = 71,
        #endregion
    }

    public class OperatorRightAttribute : Attribute
    {
        /// <summary>
        /// 权限的类别
        /// </summary>
        public string Catalog { get; set; }
        /// <summary>
        /// 权限的说明
        /// </summary>
        public string Description { get; set; }

        public OperatorRightAttribute()
        {
        }
    }
}
