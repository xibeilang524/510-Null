using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.ServiceModel.Channels;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.Park.BusinessModel.Model ;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park .BusinessModel .Report;

namespace Ralid.Park.ParkAdapter
{
    /// <summary>
    /// 表示一个停车场WCF服务的客户端
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ParkingAdapter : IParkingAdapter, IReportSinker, IReportSource, IDisposable
    {
        #region 构造函数
        public ParkingAdapter(string parkAdapterUri)
        {
            this._ParkAdapterUri = parkAdapterUri;
            _ConnectFailEvent = new AutoResetEvent(false);
            _ConnectServerThread = new Thread(ReconnectServer_Thread);
            _ConnectServerThread.Start();
            _ConnectedOKEvent = new AutoResetEvent(false);
            _CheckServerConnectedThread = new Thread(CheckConnected_Thread);
            _CheckServerConnectedThread.Start();

            ParkAdapterConnectFail += new EventHandler(ParkingAdapter_ParkAdapterConnectFail);
            ParkApaterReconnected += new EventHandler(ParkingAdapter_ParkApaterReconnected);

            Thread t = new Thread(ReportHandler_Thread);
            t.IsBackground = true;
            t.Start();
        }
        #endregion

        #region 私有变量
        private string _ParkAdapterUri;
        private IParkingAdapter _Channel;
        private DuplexChannelFactory<IParkingAdapter> _ChannelFactory;

        private AutoResetEvent _ConnectFailEvent = null;
        private Thread _ConnectServerThread;
        private AutoResetEvent _ConnectedOKEvent = new AutoResetEvent(false);
        private Thread _CheckServerConnectedThread;

        private Queue<Ralid.Park.BusinessModel.Report.ReportBase> _Reports = new Queue<BusinessModel.Report.ReportBase>();
        private object _ReportsLocker=new object ();
        private AutoResetEvent _ReportEvent = new AutoResetEvent(false);
        #endregion

        #region 私有方法
        private void ParkingAdapter_ParkAdapterConnectFail(object sender, EventArgs e)
        {
            _Channel = null;
            _ConnectFailEvent.Set();
            _ConnectedOKEvent.Reset();
            Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "断开与服务器连接");
        }

        private void ParkingAdapter_ParkApaterReconnected(object sender, EventArgs e)
        {
            _ConnectFailEvent.Reset();
            _ConnectedOKEvent.Set();
            Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "与服务器连接成功");
        }

        private void ReconnectServer_Thread()
        {
            while (_ConnectFailEvent.WaitOne(int.MaxValue))
            {
                ConnectServer();
                Thread.Sleep(1000 * 5);
            }
        }

        private void CheckConnected_Thread()
        {

            while (_ConnectedOKEvent.WaitOne(int.MaxValue))
            {
                try
                {
                    if (_Channel.Echo("hello") == "hello")  //如果返回的与发送的不一样,就说明通道已经出错
                    {
                        _ConnectedOKEvent.Set();
                        Thread.Sleep(1000 * 5);
                    }
                    else
                    {
                        ParkingAdapter_ParkAdapterConnectFail(this, EventArgs.Empty);
                        return;
                    }
                }
                catch
                {
                    ParkingAdapter_ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
        }

        private void EnQueue(ReportBase report)
        {
            lock (_ReportsLocker)
            {
                _Reports.Enqueue(report);
                _ReportEvent.Set();
            }
        }

        private ReportBase Dequeue()
        {
            if (_Reports.Count > 0)
            {
                lock (_ReportsLocker)
                {
                    return _Reports.Dequeue();
                }
            }
            return null;
        }

        private void ReportHandler_Thread()
        {
            while (true)
            {
                if (_ReportEvent.WaitOne(int.MaxValue))
                {
                    ReportBase report = Dequeue();
                    while (report != null)
                    {
                        try
                        {
                            if (this.Reporting != null) this.Reporting(this,report);
                        }
                        catch (Exception ex)
                        {
                            ExceptionPolicy.HandleException(ex);
                        }
                        report = Dequeue();
                    }
                }
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 停车场适配器断开
        /// </summary>
        public event EventHandler ParkAdapterConnectFail;
        public event EventHandler ParkApaterReconnected;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置停车场ID
        /// </summary>
        public int ParkID{get;set;}
        #endregion

        #region 公共方法
        /// <summary>
        /// 建立WCF连接
        /// </summary>
        /// <returns></returns>
        public bool ConnectServer()
        {
            try
            {
                if (!string.IsNullOrEmpty(_ParkAdapterUri))
                {
                    Binding binding = Ralid.GeneralLibrary.WCF.BindingFactory.CreateDualBinding(_ParkAdapterUri);
                    if (binding != null)
                    {
                        if (_ChannelFactory != null)
                        {
                            _ChannelFactory.Close();
                        }
                        _ChannelFactory = new DuplexChannelFactory<IParkingAdapter>(this, binding, new EndpointAddress(_ParkAdapterUri));
                        IParkingAdapter channel = _ChannelFactory.CreateChannel();
                        channel.Subscription();
                        _Channel = channel;
                        if (ParkApaterReconnected != null)
                        {
                            ParkApaterReconnected(this, EventArgs.Empty);
                        }
                        return true;
                    }
                }
                else
                {
                    if (ParkAdapterConnectFail != null)
                    {
                        ParkAdapterConnectFail(this, EventArgs.Empty);
                    }
                }
            }
            catch (CommunicationException ex)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return false;
        }
        /// <summary>
        /// 关闭WCF连接
        /// </summary>
        public void Close()
        {
            Dispose();
        }
        #endregion

        #region IParkingAdapter 成员
        public bool AddEntrance(EntranceInfo info)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.AddEntrance(info);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool UpdateEntrance(EntranceInfo info)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.UpdateEntrance(info);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DeleteEntrance(EntranceInfo info)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DeleteEntrance(info);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool SynEntranceData(int entranceID)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.SynEntranceData(entranceID);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool ClearEntranceData(int entranceID)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.ClearEntranceData(entranceID);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool SaveCard(CardInfo card,ActionType action)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.SaveCard(card,action);
                }
            }
            catch (CommunicationException ex)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool SaveCardToEntrance(int entranceID, CardInfo card, ActionType action)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.SaveCardToEntrance(entranceID, card, action);
                }
            }
            catch (CommunicationException ex)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool SaveCardsToEntrance(int entranceID, List<CardInfo> cards, ActionType action)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.SaveCardsToEntrance(entranceID, cards, action);
                }
            }
            catch (CommunicationException ex)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DeleteCard(CardInfo card)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DeleteCard(card);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DeleteCardToEntrance(int entranceID,CardInfo card)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DeleteCardToEntrance(entranceID, card);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool ClearCards()
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.ClearCards();
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool ClearCardsToEntrance(int entranceID)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.ClearCardsToEntrance(entranceID);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadVacantSetting(CarPortSetting notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadVacantSetting(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadAccessSetting(AccessSetting ascLevel)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadAccessSetting(ascLevel);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadAccessSettingToEntrance(int entranceID, AccessSetting ascLevel)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadAccessSettingToEntrance(entranceID, ascLevel);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownLoadUserSetting(UserSetting us)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownLoadUserSetting(us);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadTariffSetting(TariffSetting tariffSetting)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadTariffSetting(tariffSetting);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadTariffSettingToEntrance(int entranceID,TariffSetting tariffSetting)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadTariffSettingToEntrance(entranceID, tariffSetting);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadHolidaySetting(HolidaySetting holidaySetting)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadHolidaySetting(holidaySetting);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadHolidaySettingToEntrance(int entranceID, HolidaySetting holidaySetting)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadHolidaySettingToEntrance(entranceID, holidaySetting);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadKeySetting(KeySetting keySetting)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadKeySetting(keySetting);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadKeySettingToEntrance(int entranceID, KeySetting keySetting)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadKeySettingToEntrance(entranceID, keySetting);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool DownloadCarTypeSetting(CarTypeSetting carTypeSetting)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.DownloadCarTypeSetting(carTypeSetting);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool EventInvalid(EventInvalidNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.EventInvalid(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool EventValid(EventValidNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.EventValid(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool GateOperation(GateOperationNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.GateOperation(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public void LedDisplay(SetLedDisplayNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    _Channel.LedDisplay(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        public bool ResetDevice(DeviceReSetNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.ResetDevice(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool SetEntranceRemainTempCard(EntranceRemainTempCardNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.SetEntranceRemainTempCard(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool SwitchCarType(CarTypeSwitchNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.SwitchCarType(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool RemoteReadCard(RemoteReadCardNotify notify)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.RemoteReadCard(notify);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool Subscription()
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.Subscription();
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public bool UnSubscription()
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.UnSubscription();
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public string Echo(string echo)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.Echo(echo);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        public EntranceStatus GetParkStatus()
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.GetParkStatus();
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return EntranceStatus.UnKnown;
        }

        public EntranceStatus GetEntranceStatus(int entranceID)
        {
            try
            {
                if (_Channel != null)
                {
                    return _Channel.GetEntranceStatus(entranceID);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return EntranceStatus.UnKnown;
        }


        public void WaitingCommandServiceEnable(bool enable)
        {
            try
            {
                if (_Channel != null)
                {
                    _Channel.WaitingCommandServiceEnable(enable);
                }
            }
            catch (CommunicationException)
            {
                if (ParkAdapterConnectFail != null)
                {
                    ParkAdapterConnectFail(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        //public byte GetServerWorkMode()
        //{
        //    try
        //    {
        //        if (_Channel != null)
        //        {
        //            return _Channel.GetServerWorkMode();
        //        }
        //    }
        //    catch (CommunicationException)
        //    {
        //        if (ParkAdapterConnectFail != null)
        //        {
        //            ParkAdapterConnectFail(this, EventArgs.Empty);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
        //    }
        //    return 0xFF;
        //}

        //public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType)
        //{
        //    try
        //    {
        //        if (_Channel != null)
        //        {
        //            return _Channel.CreateCardPaymentRecord(card, carType);
        //        }
        //    }
        //    catch (CommunicationException)
        //    {
        //        if (ParkAdapterConnectFail != null)
        //        {
        //            ParkAdapterConnectFail(this, EventArgs.Empty);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
        //    }
        //    return null;
        //}

        //public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType, System.DateTime datetime)
        //{
        //    try
        //    {
        //        if (_Channel != null)
        //        {
        //            return _Channel.CreateCardPaymentRecord(card, carType, datetime);
        //        }
        //    }
        //    catch (CommunicationException)
        //    {
        //        if (ParkAdapterConnectFail != null)
        //        {
        //            ParkAdapterConnectFail(this, EventArgs.Empty);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
        //    }
        //    return null;
        //}
        #endregion

        #region IDisposable 成员
        public void Dispose()
        {
            if (_Channel != null)
            {
                _Channel.UnSubscription();
            }
        }
        #endregion

        #region IReportSinker 成员
        public void DeviceResetSink(Ralid.Park.BusinessModel.Report.DeviceResetReport report)
        {
            EnQueue(report);
        }

        public void EntranceStatusSink(Ralid.Park.BusinessModel.Report.EntranceStatusReport report)
        {
            EnQueue(report);
        }

        public void CardEventReportSink(Ralid.Park.BusinessModel.Report.CardEventReport report)
        {
            EnQueue(report);
        }

        public void CardRealTimeEventSink(Ralid.Park.BusinessModel.Report.CardEventReport report)
        {
            EnQueue(report);
        }

        public void CardInvalidSink(Ralid.Park.BusinessModel.Report.CardInvalidEventReport report)
        {
            EnQueue(report);
        }

        public void CarSenseSink(Ralid.Park.BusinessModel.Report.CarSenseReport report)
        {
            EnQueue(report);
        }

        public void CardCaptureSink(Ralid.Park.BusinessModel.Report.CardCaptureReport report)
        {
            EnQueue(report);
        }

        public void ParkVacantSink(Ralid.Park.BusinessModel.Report.ParkVacantReport report)
        {
            EnQueue(report);
        }

        public void EntranceRemainTempCardSink(Ralid.Park.BusinessModel.Report.EntranceRemainTempCardReport report)
        {
            EnQueue(report);
        }

        public void AlarmSink(BusinessModel.Report.AlarmReport report)
        {
            EnQueue(report);
        }
        #endregion

        #region IReportSource 事件成员
        public event ReportHandler<ReportBase> Reporting;
        #endregion

    }
}
