using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel.Enum ;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 表示用户权限模块的文字描述
    /// </summary>
    public class PermissionDescription
    {
        public static string GetDescription(string catalog)
        {
            if (catalog == "Card") return Resource1.Permission_Catalog_Card;
            else if (catalog == "Data") return Resource1.Permission_Catalog_Data;
            else if (catalog == "Hardware") return Resource1.Permission_Catalog_Hardware;
            else if (catalog == "Report") return Resource1.Permission_Catalog_Report;
            return string.Empty;
        }

        public static string GetPREDescription(string catalog)
        {
            if (catalog == "System") return "系统";
            else if (catalog == "Data") return "数据";
            else if (catalog == "Safe") return "安全";
            else if (catalog == "Reprot") return Resource1.Permission_Catalog_Report;
            return string.Empty;
        }

        public static string GetDescription(Permission p)
        {
            switch (p)
            {
                case Permission.AddCards:
                    return Resource1.Permission_AddCard;
                case Permission.AlarmReport:
                    return Resource1.Permission_AlarmReport;
                case Permission.CardChargeReport:
                    return Resource1.Permission_CardChargeReport;
                case Permission.CardDeferReport:
                    return Resource1.Permission_CardDeferReport;
                case Permission.CardDisableEnableReport:
                    return Resource1.Permission_CardDisableEnableReport;
                case Permission.CardEventReport:
                    return Resource1.Permission_CardEventReport;
                case Permission.CardLossRestoreReport:
                    return Resource1.Permission_CardLossRestoreReport;
                case Permission.CardPaying:
                    return Resource1.Permission_CardPaying;
                case Permission.CardPayingReport:
                    return Resource1.Permission_CardPayingReport;
                case Permission.CardRecycleReport:
                    return Resource1.Permission_CardRecycleReport;
                case Permission.CardReleaseReport:
                    return Resource1.Permission_CardReleaseReport;
                case Permission.CarFlowStatistics:
                    return Resource1.Permission_CarFlowStatistics;
                case Permission.ChargeCard:
                    return Resource1.Permission_ChargeCard;
                case Permission.CloseDoor:
                    return Resource1.Permission_CloseDoor;
                case Permission.DeferCard:
                    return Resource1.Permission_DeferCard;
                case Permission.DisableCard:
                    return Resource1.Permission_DisableCard;
                case Permission.DownloadAllCards:
                    return Resource1.Permission_DownloadAllCards;
                case Permission.EditAPM:
                    return Resource1.Permission_EditAPM;
                case Permission.EditCard:
                    return Resource1.Permission_EditCard;
                case Permission.DeleteAtAll:
                    return Resource1.Permission_DeleteAtAll;
                case Permission.EditDivision:
                    return Resource1.Permission_EditDivision;
                case Permission.EditEntrance:
                    return Resource1.Permission_EditEntrance;
                case Permission.EditOperator:
                    return Resource1.Permission_EditOperator;
                case Permission.EditPark:
                    return Resource1.Permission_EditPark;
                case Permission.EditRole:
                    return Resource1.Permission_EditRole;
                case Permission.EditSysSetting:
                    return Resource1.Permission_EditSysSetting;
                case Permission.EditVideo:
                    return Resource1.Permission_EditVideo;
                case Permission.EditWorkstation:
                    return Resource1.Permission_EditWorkstation;
                case Permission.EnableCard:
                    return Resource1.Permission_EnableCard;
                case Permission.ExportCardPayment:
                    return Resource1.Permission_ExportCardPayment;
                case Permission.ExportMonthCardPaymentReport:
                    return Resource1.Permission_ExportMonthCardPaymentReport;
                case Permission.LostCard:
                    return Resource1.Permission_LostCard;
                case Permission.ModifyCardPaymentRecord:
                    return Resource1.Permission_ModifyCardPaymentRecord;
                case Permission.OpenDoor:
                    return Resource1.Permission_OpenDoor;
                case Permission.OperatorShiftStatistics:
                    return Resource1.Permission_OperatorShiftStatistics;
                case Permission.PayOperationLogReport:
                    return Resource1.Permission_PayOperationLogReport;
                case Permission.CardInparkReport:
                    return Resource1.Permission_CardInParkReport;
                case Permission.ReadAPM:
                    return Resource1.Permission_ReadAPM;
                case Permission.ReadCards:
                    return Resource1.Permission_ReadCard;
                case Permission.ReadOperaotor:
                    return Resource1.Permission_ReadOperator;
                case Permission.ReadRole:
                    return Resource1.Permission_ReadRole;
                case Permission.ReadSysSetting:
                    return Resource1.Permission_ReadSysSetting;
                case Permission.ReadWorkstation:
                    return Resource1.Permission_ReadWorkstation;
                case Permission.RecycleCard:
                    return Resource1.Permission_RecycleCard;
                case Permission.ReleaseCard:
                    return Resource1.Permission_ReleaseCard;
                case Permission.RemoteReadCard:
                    return Resource1.Permission_RemoteReadCard;
                case Permission.ResetEntrance:
                    return Resource1.Permission_ResetDevice;
                case Permission.RestoreCard:
                    return Resource1.Permission_RestoreCard;
                case Permission.SystemIDSetting:
                    return Resource1.Permission_SystemIDSetting;
                case Permission.TempCardSetting:
                    return Resource1.Permission_TempCardSetting;
                case Permission.VideoMonitor:
                    return Resource1.Permission_VideoMonitor;
                case Permission.OperatorSettle:
                    return Resource1.Permission_OperatorSettle;
                case Permission.OtherOperatorSettle:
                    return Resource1.Permission_OtherOperatorSettle;
                case Permission.CardDeferStatistics:
                    return Resource1.Permission_CardDeferStatistic;
                case Permission.CardPayingStatistics:
                    return Resource1.Permission_CardPayingStatistic;
                case Permission.RealEvent:
                    return Resource1.Permission_RealEvent;
                case Permission.PrintOperatorSettleLog:
                    return Resource1.Permission_PrintOperatorSettleLog;
                case Permission.CardDeleteReport:
                    return Resource1.Permission_CardDeleteReport;
                case Permission.PrintCardPayment:
                    return Resource1.Permission_PrintCardPayment;
                case Permission.PrintMonthCardPaymentReport:
                    return Resource1.Permission_PrintMonthCardPaymentReport;
                case Permission.CardIDConvert:
                    return Resource1.Permission_CardIDConvert;
                case Permission.CardBulkChange:
                    return Resource1.Permission_BulkChangeCards;
                case Permission.ExportCards:
                    return Resource1.Permission_ExportCards;
                case Permission.ZSTSetting:
                    return Resource1.Permission_ZSTSeting;
                case Permission.YangChenTongLogReport:
                    return Resource1.Permission_YangChenTongLogReport;
                case Permission.SyncDataToStandby:
                    return Resource1.Permission_SyncDataToStandby;
                case Permission.AddManageCard:
                    return Resource1.Permission_AddManageCard;
                case Permission.ChangeCardKey:
                    return Resource1.Permission_ChangeCardKey;
                case Permission.CardDataConvert:
                    return Resource1.Permission_CardDataConvert;
                case Permission.ExportParameter:
                    return Resource1.Permission_ExportParameter;
                case Permission.ImportRecord:
                    return Resource1.Permission_ImportRecord;
                case Permission.HasPaidCardReport:
                    return Resource1.Permission_HasPaidCardReport;
                case Permission.CardReport:
                    return Resource1.Permission_CardReport;
                case Permission.ReadRoadWay:
                    return Resource1.Permission_ReadRoadWay;
                case Permission.EditRoadWay:
                    return Resource1.Permission_EditRoadWay;
                case Permission.FreeAuthorization:
                    return Resource1.Permissions_FreeAuthorization;
                case Permission.EditFreeDays:
                    return Resource1.Permission_EditFreeDays;
                case Permission.SwitchRoadWay:
                    return Resource1.Permission_SwitchRoadWay;
                case Permission.POSSyncTool:
                    return Resource1.Permission_POSSyncTool;
                case Permission.FreeAuthorizationLogReport:
                    return Resource1.Permission_FreeAuthorizationLogReport;
                case Permission.CardOut:
                    return Resource1.Permission_CardOut;
                case Permission.WaitingCommandReport:
                    return Resource1.Permission_WaitingCommandReport;
                case Permission.ReadLocalSetting:
                    return Resource1.Permission_ReadLocalSetting;
                case Permission.EditLocalSetting:
                    return Resource1.Permission_EditLocalSetting;
                case Permission.APMCheckOutRecordReport:
                    return Resource1.Permission_APMCheckOutRecordReport;
                case Permission.APMRefundRecordReport:
                    return Resource1.Permission_APMRefundRecordReport;
                case Permission.APMRefund:
                    return Resource1.Permission_APMRefund;
                case Permission.VehicleLedSetting:
                    return Resource1.Permission_VehicleLedSetting;
                case Permission.HostStandbySetting:
                    return Resource1.Permission_HostStandbySetting;
                case Permission.ServerSwitchReport:
                    return Resource1.Permission_ServerSwitchReport;
                case Permission.ReadDept:
                    return Resource1.Permission_ReadDept;
                case Permission.EditDept:
                    return Resource1.Permission_EditDept;
                case Permission.NoCardLost:
                    return Resource1.Permission_NoCardLost;
                case Permission.SpeedingProcess:
                    return Resource1.Permission_SpeedingProcess;
                case Permission.SpeedingReport:
                    return Resource1.Permission_SpeedingReport;
                default:
                    return string.Empty;
            }
        }

        public static string GetDescription(PREPermission p)
        {
            switch (p)
            {
                case PREPermission.SystemSetting:
                    return "系统设置";
                case PREPermission.PreferentialCore:
                    return "优惠录入";
                case PREPermission.PreferentialCancel:
                    return "优惠取消";
                case PREPermission.ReadWorkstations:
                    return "查看工作站信息";
                case PREPermission.EditWorkstations:
                    return "修改工作站信息";
                case PREPermission.ReadBusiness:
                    return "查看商家信息";
                case PREPermission.EditBusiness:
                    return "修改商家信息";
                case PREPermission.OperatorManager:
                    return "操作员管理";
                case PREPermission.RoleManager:
                    return "角色管理";
                case PREPermission.PreferentialReport:
                    return "优惠记录";
                default:
                    return string.Empty;
            }
        }
    }
}
