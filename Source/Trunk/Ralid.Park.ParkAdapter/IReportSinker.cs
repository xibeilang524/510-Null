using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.ParkAdapter
{
    /// <summary>
    /// 传递报告链中的报告传递者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ServiceContract]
    public interface IReportSinker
    {
        /// <summary>
        ///传递出入口设备状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void DeviceResetSink(DeviceResetReport report);
        /// <summary>
        ///传递出入口设备状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void EntranceStatusSink(EntranceStatusReport report);

        /// <summary>
        /// 传递事件
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void CardEventReportSink(CardEventReport report);

        /// <summary>
        /// 传递实时事件
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void CardRealTimeEventSink(CardEventReport report);

        /// <summary>
        /// 传递无效刷卡事件
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void CardInvalidSink(CardInvalidEventReport report);

        /// <summary>
        /// 传递地感检测到车辆的消息
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void CarSenseSink(CarSenseReport report);

        /// <summary>
        /// 收卡机收卡一张
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void CardCaptureSink(CardCaptureReport report);

        /// <summary>
        /// 传递停车场车位余数
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void ParkVacantSink(ParkVacantReport report);
        /// <summary>
        /// 通道临时卡剩余数量通知
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void EntranceRemainTempCardSink(EntranceRemainTempCardReport report);
        /// <summary>
        /// 传递报警信息通知
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void AlarmSink(AlarmReport report);
        /// <summary>
        /// 传递更新系统参数设置通知
        /// </summary>
        /// <param name="report"></param>
        [OperationContract(IsOneWay = true)]
        void UpdateSystemParamSettingSink(UpdateSystemParamSettingReport report);
    }
}
