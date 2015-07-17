using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Report;
using Ralid.GeneralLibrary.LED;
using Ralid.GeneralLibrary.ExceptionHandling;

namespace Ralid.Park.UI
{
    /// <summary>
    /// 表示一个车辆信息显示处理器
    /// </summary>
    public class VehicleLedRender : Ralid.Park.BusinessModel.Interface.IReportHandler
    {
        #region 构造函数
        public VehicleLedRender()
        { 
        }
        #endregion

        #region 私有变量
        /// <summary>
        /// 车辆信息显示屏
        /// </summary>
        private VehicleLed _VehicleLed;
        #endregion

        #region 私有方法
        private void ReportRender(CardEventReport report)
        {
            try
            {
                if (_VehicleLed == null)
                {
                    if (AppSettings.CurrentSetting.VehicleLedCOMPort > 0)
                    {
                        _VehicleLed = new VehicleLed(AppSettings.CurrentSetting.VehicleLedCOMPort);
                        _VehicleLed.Open();
                    }
                }
                if (_VehicleLed != null)
                {
                    _VehicleLed.DisplayVehicleInfo(report.Department, report.OwnerName, report.CardCarPlate, report.IsExitEvent);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 实现 IReportHandler 接口
        public void ProcessReport(ReportBase report)
        {
            if (AppSettings.CurrentSetting.VehicleLedCOMPort > 0)
            {
                if (report is CardEventReport)
                {
                    EntranceInfo entrance = ParkBuffer.Current.GetEntrance(report.EntranceID);
                    if (entrance != null && WorkStationInfo.CurrentStation.EntranceList.Exists(e => e == entrance.EntranceID))
                    {
                        CardEventReport r = report as CardEventReport;
                        ReportRender(r);
                    }
                }
            }
        }
        #endregion
    }
}
