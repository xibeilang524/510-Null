using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ServiceModel;
using Ralid.Park.BLL;
using Ralid.Park.ParkService;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.Park.ParkService.NETParking;
using Ralid.Park.ParkService.CANParking;

namespace Ralid.Park.ParkAdapter
{
    /// <summary>
    /// 表示在线停车场的通信接口
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed class ParkingAdapterServer : IParkingAdapter
    {
        #region 构造函数
        public ParkingAdapterServer(ParkInfo park)
        {
            _Parkinfo = park;

            if (park.DeviceType == EntranceDeviceType.CANEntrance)
            {
                _Park = new CANPark(park, null);
            }
            else
            {
                _Park = new NETPark(park, null);
            }

            ListenParkEvents(_Park);

            _ReportSendingThread = new Thread(ReportSending_Thread);
            _ReportSendingThread.Start();
        }
        #endregion 构造方法

        #region 成员变量
        private ParkInfo _Parkinfo;
        private ParkBase _Park;
        private List<IReportSinker> _reportSinkers = new List<IReportSinker>();
        private object _IReportSinkerListLock = new object();  //用于同步访问_reportSinkers

        private Queue<ReportBase> _ReportSendingQueue = new Queue<ReportBase>();
        private object _ReportSendingQueueLock = new object();
        private AutoResetEvent _ReportArriveEvent = new AutoResetEvent(false);
        private Thread _ReportSendingThread;
        #endregion 成员变量

        #region 私有方法
        private void RemoveSinkers(List<IReportSinker> sinkers)
        {
            lock (_IReportSinkerListLock)
            {
                foreach (IReportSinker faultsinker in sinkers)
                {
                    _reportSinkers.Remove(faultsinker);
                    if (AppSettings.CurrentSetting.Debug)
                    {
                        Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("通道出错，断开一个WCF连接，共有{0}个客户端连接", _reportSinkers.Count));
                    }
                }
            }
        }

        private void ListenParkEvents(ParkBase park)
        {
            park.CardCaptureReporting += new ReportingHandler<CardCaptureReport>(park_CardCaptureReporting);
            park.CardEventReporting += new ReportingHandler<CardEventReport>(park_CardEventReporting);
            park.CardInvalidReporting += new ReportingHandler<CardInvalidEventReport>(park_CardInvalidReporting);
            park.CarSenseReporting += new ReportingHandler<CarSenseReport>(park_CarSenseReporting);
            park.EntranceRemainTempCardReporting += new ReportingHandler<EntranceRemainTempCardReport>(park_EntranceRemainTempCardReporting);
            park.EntranceStatusReporting += new ReportingHandler<EntranceStatusReport>(park_EntranceStatusReporting);
            park.ParkVacantReporting += new ReportingHandler<ParkVacantReport>(park_ParkVacantReporting);
            park.DeviceResetReporting += new ReportingHandler<DeviceResetReport>(park_DeviceResetReporting);
            park.AlarmReporting += new ReportingHandler<AlarmReport>(park_AlarmReporting);
        }

        private void RemoveFaultSinkers(List<IReportSinker> faultSinkers)
        {
            lock (_IReportSinkerListLock)
            {
                foreach (IReportSinker faultsinker in faultSinkers)
                {
                    _reportSinkers.Remove(faultsinker);
                    if (AppSettings.CurrentSetting.Debug)
                    {
                        Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("通道出错，断开一个WCF连接，共有{0}个客户端连接", _reportSinkers.Count));
                    }
                }
            }
        }

        private void ReportSending_Thread()
        {
            while (_ReportArriveEvent.WaitOne(int.MaxValue))
            {
                ReportBase report = ReportDequeue();
                while (report != null)
                {
                    SendReport(report);
                    report = ReportDequeue();
                }
            }
        }

        private void ReportEnqueue(ReportBase report)
        {
            lock (_ReportSendingQueueLock)
            {
                _ReportSendingQueue.Enqueue(report);
                _ReportArriveEvent.Set();
            }
        }

        private ReportBase ReportDequeue()
        {
            lock (_ReportSendingQueueLock)
            {
                if (_ReportSendingQueue.Count > 0)
                {
                    return _ReportSendingQueue.Dequeue();
                }
                return null;
            }
        }

        private void SendReport(ReportBase report)
        {
            try
            {
                if (report is CardEventReport) ReportSink(report as CardEventReport);
                else if (report is CarSenseReport) ReportSink(report as CarSenseReport);
                else if (report is CardInvalidEventReport) ReportSink(report as CardInvalidEventReport);
                else if (report is DeviceResetReport) ReportSink(report as DeviceResetReport);
                else if (report is ParkVacantReport) ReportSink(report as ParkVacantReport);
                else if (report is CardCaptureReport) ReportSink(report as CardCaptureReport);
                else if (report is EntranceStatusReport) ReportSink(report as EntranceStatusReport);
                else if (report is EntranceRemainTempCardReport) ReportSink(report as EntranceRemainTempCardReport);
                else if (report is AlarmReport) ReportSink(report as AlarmReport);
                else if (report is UpdateSystemParamSettingReport) ReportSink(report as UpdateSystemParamSettingReport);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }

        private void ReportSink(EntranceStatusReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.EntranceStatusSink(report);
                }

                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(DeviceResetReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.DeviceResetSink(report);
                }
                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(CardEventReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.CardEventReportSink(report);
                }
                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(CardInvalidEventReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.CardInvalidSink(report);
                }

                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(CarSenseReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.CarSenseSink(report);
                }
                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(ParkVacantReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.ParkVacantSink(report);
                }
                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(CardCaptureReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.CardCaptureSink(report);
                }

                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(EntranceRemainTempCardReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.EntranceRemainTempCardSink(report);
                }
                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveFaultSinkers(faultSinkers);
            }
        }

        private void ReportSink(AlarmReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.AlarmSink(report);
                }

                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveSinkers(faultSinkers);
            }
        }

        private void ReportSink(UpdateSystemParamSettingReport report)
        {
            List<IReportSinker> faultSinkers = new List<IReportSinker>();
            foreach (IReportSinker reportSinker in _reportSinkers)
            {
                try
                {
                    reportSinker.UpdateSystemParamSettingSink(report);
                }

                catch (Exception ex)
                {
                    faultSinkers.Add(reportSinker);
                    ExceptionPolicy.HandleException(ex);
                }
            }
            if (faultSinkers.Count > 0)
            {
                RemoveSinkers(faultSinkers);
            }
        }
        #endregion

        #region IParkingAdapter 成员
        public ParkInfo Park
        {
            get { return _Parkinfo; }
        }

        public bool AddEntrance(EntranceInfo info)
        {
            return _Park.AddEntrance(info);
        }

        public bool UpdateEntrance(EntranceInfo info)
        {
            return _Park.UpdateEntrance(info);
        }

        public bool DeleteEntrance(EntranceInfo info)
        {
            return _Park.DeleteEntrance(info);
        }

        public bool SynEntranceData(int entranceID)
        {
            return false;
        }

        public bool ClearEntranceData(int entranceID)
        {
            return false;
        }

        public bool SaveCard(CardInfo card,ActionType action)
        {
            return _Park.SaveCard(card,action);
        }

        public bool SaveCardToEntrance(int entranceID,CardInfo card, ActionType action)
        {
            return _Park.SaveCardToEntrance(entranceID, card, action);
        }

        public bool SaveCardsToEntrance(int entranceID, List<CardInfo> cards, ActionType action)
        {
            return _Park.SaveCardsToEntrance(entranceID, cards, action);
        }

        public bool DeleteCard(CardInfo card)
        {
            return _Park.DeleteCard(card);
        }

        public bool DeleteCardToEntrance(int entranceID, CardInfo card)
        {
            return _Park.DeleteCardToEntrance(entranceID, card);
        }

        public bool ClearCards()
        {
            return _Park.ClearCard();
        }

        public bool ClearCardsToEntrance(int entranceID)
        {
            return _Park.ClearCardToEntrance(entranceID);
        }

        public bool DownloadVacantSetting(CarPortSetting vacantInfo)
        {
            return _Park.DownloadVacantSetting(vacantInfo);
        }

        public bool ModifiedVacant(short vacant)
        {
            return _Park.ModifiedVacant(vacant);
        }

        public bool DownloadAccessSetting(AccessSetting ascLevel)
        {
            return _Park.DownloadAccessSetting(ascLevel);
        }

        public bool DownloadAccessSettingToEntrance(int entranceID, AccessSetting ascLevel)
        {
            return _Park.DownloadAccessSettingToEntrance(entranceID, ascLevel);
        }
        

        public bool DownLoadUserSetting(UserSetting us)
        {
            return _Park.DownLoadUserSetting(us);
        }

        public bool DownloadHolidaySetting(HolidaySetting holidaySetting)
        {
            return _Park.DownloadHolidaySetting(holidaySetting);
        }

        public bool DownloadHolidaySettingToEntrance(int entranceID, HolidaySetting holidaySetting)
        {
            return _Park.DownloadHolidaySettingToEntrance(entranceID, holidaySetting);
        }
        

        public bool DownloadTariffSetting(TariffSetting tariffSetting)
        {
            return _Park.DownloadTariffSetting(tariffSetting);
        }

        public bool DownloadTariffSettingToEntrance(int entranceID, TariffSetting tariffSetting)
        {
            return _Park.DownloadTariffSettingToEntrance(entranceID, tariffSetting);
        }

        public bool DownloadKeySetting(KeySetting keySetting)
        {
            return _Park.DownloadKeySetting(keySetting);
        }

        public bool DownloadKeySettingToEntrance(int entranceID, KeySetting keySetting)
        {
            return _Park.DownloadKeySettingToEntrance(entranceID, keySetting);
        }

        public bool DownloadCarTypeSetting(CarTypeSetting carTypeSetting)
        {
            return _Park.DownloadCarTypeSetting(carTypeSetting);
        }

        public bool EventValid(Ralid.Park.BusinessModel.Notify.EventValidNotify notify)
        {
            return _Park.EventValid(notify);
        }

        public bool EventInvalid(Ralid.Park.BusinessModel.Notify.EventInvalidNotify notify)
        {
            return _Park.EventInvalid(notify);
        }

        public bool ResetDevice(Ralid.Park.BusinessModel.Notify.DeviceReSetNotify notify)
        {
            return _Park.ResetDevice(notify);
        }

        public void LedDisplay(Ralid.Park.BusinessModel.Notify.SetLedDisplayNotify notify)
        {
            _Park.LedDisplay(notify);
        }

        public bool GateOperation(GateOperationNotify notify)
        {
            return _Park.GateOperate(notify);
        }

        public bool SwitchCarType(CarTypeSwitchNotify notify)
        {
            _Park.SwitchCarType(notify);
            return true;
        }

        public bool SwitchEntrance(EntranceSwitchNotify notify)
        {
            _Park.SwitchEntrance(notify);
            return true;
        }

        public bool SetEntranceRemainTempCard(EntranceRemainTempCardNotify notify)
        {
            return _Park.SetEntranceRemainTempCard(notify);
        }

        public bool RemoteReadCard(RemoteReadCardNotify notify)
        {
            return _Park.RemoteReadCard(notify);
        }

        public bool UpdateSystemParamSetting(UpdateSystemParamSettingNotity notify)
        {
            UpdateSystemParamSettingReport report = new UpdateSystemParamSettingReport();
            report.EventDateTime = DateTime.Now;
            report.OperatorID = notify.Operator.OperatorName;
            report.StationID = notify.StationID;
            report.SourceName = notify.StationName;
            report.ParamTypeName = notify.ParamTypeName;

            ReportEnqueue(report);

            return true;
        }

        public bool Subscription()
        {
            lock (_IReportSinkerListLock)
            {
                IReportSinker reportSinker = OperationContext.Current.GetCallbackChannel<IReportSinker>();
                if (_reportSinkers.Contains(reportSinker) == false)
                {
                    _reportSinkers.Add(reportSinker);
                    if (AppSettings.CurrentSetting.Debug)
                    {
                        Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("建立一个WCF连接，共有{0}个客户端连接", _reportSinkers.Count));
                    }
                }
            }
            return true;
        }

        public bool UnSubscription()
        {
            lock (_IReportSinkerListLock)
            {
                IReportSinker reportSinker = OperationContext.Current.GetCallbackChannel<IReportSinker>();
                _reportSinkers.Remove(reportSinker);
                if (AppSettings.CurrentSetting.Debug)
                {
                    Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("断开一个WCF连接，共有{0}个客户端连接", _reportSinkers.Count));
                }
            }
            return true;
        }

        public string Echo(string echo)
        {
            //由于发心跳包的都应该是需要长连接的情景,客户端的IReportSinker有可能在服务器发送包时会失败,被从列表中移除,
            //通过发心跳包重新加回列表中.
            lock (_IReportSinkerListLock)
            {
                IReportSinker reportSinker = OperationContext.Current.GetCallbackChannel<IReportSinker>();
                if (_reportSinkers.Contains(reportSinker) == false)
                {
                    _reportSinkers.Add(reportSinker);
                    if (AppSettings.CurrentSetting.Debug)
                    {
                        Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("建立一个WCF连接，共有{0}个客户端连接", _reportSinkers.Count));
                    }
                }
            }
            return echo;
        }

        #region 写卡模式新增

        public EntranceStatus GetParkStatus()
        {
            return _Park.GetParkStatus();
        }

        public EntranceStatus GetEntranceStatus(int entranceID)
        {
            return _Park.GetEntranceStatus(entranceID);
        }
        //public byte GetServerWorkMode()
        //{
        //    return _Park.GetServerWorkMode();
        //}

        public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType)
        {
            return _Park.CreateCardPaymentRecord(card, carType);
        }

        public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType, DateTime datetime)
        {
            return _Park.CreateCardPaymentRecord(card, carType, datetime);
        }
        #endregion



        public void WaitingCommandServiceEnable(bool enable)
        {
            _Park.WaitingCommandServiceEnable(enable);
        }
        #endregion

        #region 事件处理程序
        private void park_ParkVacantReporting(object sender, ParkVacantReport report)
        {
            ReportEnqueue(report);
        }

        private void park_EntranceStatusReporting(object sender, EntranceStatusReport report)
        {
            ReportEnqueue(report);
        }

        private void park_EntranceRemainTempCardReporting(object sender, EntranceRemainTempCardReport report)
        {
            ReportEnqueue(report);
        }

        private void park_CarSenseReporting(object sender, CarSenseReport report)
        {
            ReportEnqueue(report);
        }

        private void park_CardInvalidReporting(object sender, CardInvalidEventReport report)
        {
            ReportEnqueue(report);
        }

        private void park_CardEventReporting(object sender, CardEventReport report)
        {
            ReportEnqueue(report);
        }

        private void park_CardCaptureReporting(object sender, CardCaptureReport report)
        {
            ReportEnqueue(report);
        }

        private void park_DeviceResetReporting(object sender, DeviceResetReport report)
        {
            ReportEnqueue(report);
        }

        private void park_AlarmReporting(object sender, AlarmReport report)
        {
            ReportEnqueue(report);
        }
        #endregion
    }
}
