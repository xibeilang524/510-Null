using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.PlateRecognition;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.LOG;
using Ralid.GeneralLibrary.Printer;
using Ralid.GeneralLibrary.CardReader;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.Park.SnapShotCapture;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.ParkService
{
    public abstract class EntranceBase
    {
        #region 构造函数
        public EntranceBase(EntranceInfo info, ParkBase parent)
        {
            Parent = parent;
            _entrance = info;
            _status = EntranceStatus.UnKnown;  //初始状态设置为未知
            OptStatus = EntranceOperationStatus.CarLeave;//初始状态为车走状态
            
            if (info.TicketReaderCOMPort > 0)
            {
                _TicketReader = new BarCodeReader(info.TicketReaderCOMPort);
                _TicketReader.BarCodeRead += new BarCodeReadEventHandler(TicketReader_BarCodeRead);
                _TicketReader.Open();
                if (!_TicketReader.Opened)
                {
                    _ReaderPrinterOfflineEvent.Set();
                }
                else
                {
                    _CurText = UserSetting.Current.CompanyName;
                }
            }
            if (info.TicketReaderCOMPort2 != null && info.TicketReaderCOMPort2 > 0)
            {
                _TicketReader2 = new BarCodeReader(info.TicketReaderCOMPort2.Value);
                _TicketReader2.BarCodeRead += new BarCodeReadEventHandler(TicketReader_BarCodeRead);
                _TicketReader2.Open();
                if (!_TicketReader2.Opened)
                {
                    _ReaderPrinterOfflineEvent.Set();
                }
                else
                {
                    _CurText = UserSetting.Current.CompanyName;
                }
            }
            if (info.TicketPrinterCOMPort > 0)
            {
                _TicketPrinter = new KPM150BarCodePrinter(info.TicketPrinterCOMPort);
                _TicketPrinter.Open();
                _TicketPrinter.QueryStatus();
                if (_TicketPrinter.Status == PrinterStatus.OffLine || _TicketPrinter.Status == PrinterStatus.COMPortNotOpen)
                {
                    _ReaderPrinterOfflineEvent.Set();
                }
                else
                {
                    _CurText = UserSetting.Current.CompanyName;
                }
            }
            //初始化处理消息包的线程
            _PacketHandleThread = new Thread(ReportHandle_Thread);
            _PacketHandleThread.Start();

            Thread t = new Thread(new ThreadStart(HeartBeatCheck_Thread));
            t.IsBackground = true;
            t.Start();

            Thread t1 = new Thread(new ThreadStart(ReconnectReaderPrinter_Thread));
            t1.IsBackground = true;
            t1.Start();
        }
        #endregion

        #region 私有变量
        private EntranceInfo _entrance;
        private DateTime _lastCardReadTime = new DateTime(2011, 11, 11);//DateTime.Now;
        private EntranceStatus _status;

        protected KPM150BarCodePrinter _TicketPrinter;
        private PrinterStatus _LastTicketPrinterStatus = PrinterStatus.Ok;
        private BarCodeReader _TicketReader;
        private bool _LastTicketReaderOpened = true;
        private BarCodeReader _TicketReader2;
        private bool _LastTicketReader2Opened = true;

        private Queue<ReportBase> _PacketQueue = new Queue<ReportBase>();
        private object _PacketQueueLocker = new object();
        private AutoResetEvent _PacketRecieveEvent = new AutoResetEvent(false);
        private Thread _PacketHandleThread;

        private AutoResetEvent _ReaderPrinterOfflineEvent = new AutoResetEvent(false);//条码枪和打印机断线时间

        protected DateTime _LastEventDatetime = DateTime.Now;
        protected readonly int _OfflineTimeout = 15;  //断开超时时间，如果在超过此时间内没有收到控制器事件，则认为硬件已经离线

        private string _CurText = null;
        #endregion

        #region 私有方法
        private void ReportHandle_Thread()
        {
            while (_PacketRecieveEvent.WaitOne(int.MaxValue))
            {
                try
                {
                    ReportBase report = ObtainReportFromPool();
                    while (report != null)
                    {
                        report.ParkID = ParkID;
                        report.EntranceID = _entrance.EntranceID;
                        report.SourceName = _entrance.EntranceName;
                        DateTime dt1 = report.EventDateTime;
                        if (Parent.WorkMode == ParkWorkMode.Fool)
                        {
                            //在线模式时，以服务器的时间为准
                            DateTime dt = DateTime.Now;
                            report.EventDateTime = dt;
                        }
                        else
                        {
                            //脱机模式时，如果上传的是读卡事件或脱机读卡事件，事件时间手动增加1毫秒
                            //主要是因为脱机上传的事件时间是以秒为单位的，有可能同一秒里上传了地感事件和读卡事件，软件是以摄像机id和事件时间保存图片的
                            //当软件选上车压地感时抓拍时，会导致车压地感时能保存抓拍图片，读卡事件不能保存抓拍图片，因为事件的时间都是一样的
                            //所以这里手动为读卡事件或脱机读卡事件的事件时间增加1毫秒，以区分同一秒的地感事件时间
                            if ((report is CardReadReport)
                                || (report is OfflineCardReadReport))
                            {                                
                                report.EventDateTime = dt1.AddMilliseconds(1);
                            }
                        }
                        
                        if (AppSettings.CurrentSetting.Debug)
                        {
                            if (dt1 <= DateTime.MinValue)
                            {
                                Ralid.GeneralLibrary.LOG.FileLog.Log(_entrance.EntranceName, "正在处理事件 " + report.Description);
                            }
                            else
                            {
                                Ralid.GeneralLibrary.LOG.FileLog.Log(_entrance.EntranceName, "正在处理事件 " + report.Description + " 硬件时间:" + dt1.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                        ReportHandler(report);
                        report = ObtainReportFromPool();
                    }
                }
                catch (ThreadAbortException ex)
                {
                    Ralid.GeneralLibrary.LOG.FileLog.Log(_entrance.EntranceName, "事件循环线程被终止");
                }
            }
        }
        /// 从事件池中取出一个事件
        private ReportBase ObtainReportFromPool()
        {
            lock (_PacketQueueLocker)
            {
                if (_PacketQueue.Count > 0)
                {
                    return _PacketQueue.Dequeue();
                }
                return null;
            }
        }

        private void TicketReader_BarCodeRead(object sender, BarCodeReadEventArgs e)
        {
            string id = GetCardIDFromBarCode(e.BarCode);
            if (!string.IsNullOrEmpty(id))
            {
                if (!IsReadCardIntervalOver(DateTime.Now)) return; //如果未超过读卡间隔,不处理
                CardReadReport report = new CardReadReport();
                report.CardID = id;
                report.EntranceID = this.EntranceID;
                report.Reader = GetFirstTempReader();
                AddToReportPool(report);
            }
        }

        private string GetCardIDFromBarCode(string barcode)
        {
            if (!string.IsNullOrEmpty(barcode))
            {
                if (barcode.Length == 7)
                {
                    return barcode;
                }
                if (barcode.Length == 8)
                {
                    string ck = Ralid.GeneralLibrary.ITFCheckCreater.Create(barcode.Substring(0, barcode.Length - 1));
                    if (!string.IsNullOrEmpty(ck) && ck == barcode.Substring(barcode.Length - 1, 1))
                    {
                        return barcode.Substring(0, barcode.Length - 1);
                    }
                }
            }
            if (!string.IsNullOrEmpty(barcode) && AppSettings.CurrentSetting.Debug)
            {
                FileLog.Log(this.EntranceName + "丢弃条码", barcode);
            }
            return string.Empty;
        }

        /// <summary>
        /// 将report中的卡号转换成韦根26
        /// </summary>
        /// <param name="report"></param>
        private void ConvertReportCardIDToWengen26(ReportBase report)
        {
            long cardid = 0;
            if (report is CardReadReport)
            {
                CardReadReport report26 = report as CardReadReport;
                if (long.TryParse(report26.CardID, out cardid))
                {
                    report26.CardID = (cardid & 0xFFFFFF).ToString();
                }
            }
            else if (report is OfflineCardReadReport)
            {
                OfflineCardReadReport report26 = report as OfflineCardReadReport;
                if (long.TryParse(report26.CardID, out cardid))
                {
                    report26.CardID = (cardid & 0xFFFFFF).ToString();
                }
            }
            else if (report is CardInvalidEventReport)
            {
                CardInvalidEventReport report26 = report as CardInvalidEventReport;
                if (long.TryParse(report26.CardID, out cardid))
                {
                    report26.CardID = (cardid & 0xFFFFFF).ToString();
                }
            }
        }

        private EntranceReader GetFirstTempReader()
        {
            for (int i = 0; i < 10; i++)
            {
                if (IsTempReader((EntranceReader)i)) return (EntranceReader)i;
            }
            return (EntranceReader)0;
        }

        /// 创建并在数据库中保存纸票,成功后返回保存成功的纸票信息，失败返回null
        private CardInfo CreateATicket()
        {
            for (int i = 0; i < 3; i++) //生成一个卡号，然后保存，如果系统中已经存在此卡，则再次尝试，最多三次
            {
                CardInfo card = new CardInfo();
                card.CardID = TicketIDCreater.Create7CharCardID();
                card.CardType = Ralid.Park.BusinessModel.Enum.CardType.Ticket;
                card.Status = CardStatus.Enabled;
                card.OwnerName = card.CardID;
                card.HolidayEnabled = true;
                card.CanRepeatIn = false;
                card.CanRepeatOut = false;
                card.WithCount = true;
                card.CanEnterWhenFull = true;
                card.EnableWhenExpired = true;
                card.OnlineHandleWhenOfflineMode = true;
                Ralid.Park.BusinessModel.Result.CommandResult result = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).AddCard(card);
                if (result.Result == ResultCode.Successful)
                {
                    return card;
                }

            }
            return null;
        }
        /// <summary>
        /// 打印纸票
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        private bool PrintTicket(TicketInfo ticket)
        {
            //全使用SEVL模式指令打印，SEVL模式指令兼容使用黑标打印和不使用黑标打印
            return _TicketPrinter.PrintTicketBySVEL(ticket, false);

            //if (EntranceInfo.PrinterUseNotch)
            //{
            //    //使用黑标打印
            //    return _TicketPrinter.PrintTicketOfTykoWithNotch(ticket, false, 6);
            //}
            //else
            //{
            //    return _TicketPrinter.PrintTicketOfTyko(ticket, false);
            //}
        }
        /// 打印一张纸票
        private bool PrintATicket(string ticketID, DateTime eventDateTime)
        {
            if (_TicketPrinter != null)
            {
                //生成纸票
                TicketInfo ticket = new TicketInfo();
                ticket.EventDateTime = eventDateTime;
                ticket.Entrance = _entrance.EntranceName;
                ticket.CardID = ticketID;
                ticket.CompanyName = UserSetting.Current.CompanyName;
                ticket.Producter = "广州瑞立德";
                ticket.Reguard = "出场凭证,请勿折损!";
                //打印纸票
                if (PrintTicket(ticket))
                {
                    return true;
                }
                else
                {
                    if (AppSettings.CurrentSetting.Debug) FileLog.Log(_entrance.EntranceName, "纸票打印失败");
                }
            }
            return false;
        }

        private void HeartBeatCheck_Thread()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - _LastEventDatetime.Ticks);
                if (ts.TotalSeconds > _OfflineTimeout && Status != EntranceStatus.OffLine) //如果在超时时间内没有收到心跳包，则表示设备已经断线
                {
                    Status = EntranceStatus.OffLine;
                }
            }
        }

        /// <summary>
        /// 重连条码枪和打印机，由于同一块控制板不会同时存在打印和条码枪，所以这里只使用了一个线程去处理
        /// </summary>
        private void ReconnectReaderPrinter_Thread()
        {
            while (true)
            {
                if (_ReaderPrinterOfflineEvent.WaitOne(int.MaxValue))
                {
                    if (_TicketReader != null)
                    {
                        if (!_TicketReader.Opened)
                        {
                            try
                            {
                                FileLog.Log(this.EntranceName, "重新连接条码枪1");
                                _TicketReader.Close();
                                _TicketReader.Open();
                                if (!_TicketReader.Opened)
                                {
                                    _ReaderPrinterOfflineEvent.Set();
                                }
                                else
                                {
                                    //只要有一个条码枪可以使用就不需要在票箱上提示了
                                    DisplayMsg(UserSetting.Current.CompanyName, true);
                                    _CurText = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex);
                            }
                        }
                    }

                    if (_TicketReader2 != null)
                    {
                        if (!_TicketReader2.Opened)
                        {
                            try
                            {
                                FileLog.Log(this.EntranceName, "重新连接条码枪2");
                                _TicketReader2.Close();
                                _TicketReader2.Open();
                                if (!_TicketReader2.Opened)
                                {
                                    _ReaderPrinterOfflineEvent.Set();
                                }
                                else
                                {
                                    //只要有一个条码枪可以使用就不需要在票箱上提示了
                                    DisplayMsg(UserSetting.Current.CompanyName, true);
                                    _CurText = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex);
                            }
                        }
                    }

                    if (_TicketPrinter != null)
                    {
                        if (_TicketPrinter.Status == PrinterStatus.OffLine || _TicketPrinter.Status == PrinterStatus.COMPortNotOpen)
                        {
                            try
                            {
                                FileLog.Log(this.EntranceName, "重新连接串口打印机");
                                _TicketPrinter.Close();
                                _TicketPrinter.Open();
                                _TicketPrinter.QueryStatus();
                                if (_TicketPrinter.Status == PrinterStatus.OffLine || _TicketPrinter.Status == PrinterStatus.COMPortNotOpen)
                                {
                                    _ReaderPrinterOfflineEvent.Set();
                                }
                                else
                                {
                                    DisplayMsg(UserSetting.Current.CompanyName, true);
                                    _CurText = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex);
                            }
                        }
                    }

                    Thread.Sleep(5000);
                }
            }
        }

        /// <summary>
        /// 创建并在数据库中保存一张临时车牌名单,类型为纸票，成功后返回保存成功的临时车牌名单，失败返回null
        /// </summary>
        /// <param name="carPlate">车牌号码</param>
        /// <returns></returns>
        private CardInfo CreateATempCarPlateList(string carPlate)
        {
            //生成一个临时车牌名单，然后保存，如果系统中已经存在此卡，则再次尝试，最多十次
            //由于考虑到生成失败后，控制板不能再次生成车牌事件了，所以这里设置最多尝试十次
            for (int i = 0; i < 10; i++) 
            {
                CardInfo card = new CardInfo();
                card.CardID = ListCardIDCreater.CreateListCardID();
                card.ListType = CardListType.CarPlate;
                card.CarPlate = carPlate;
                card.CardType = Ralid.Park.BusinessModel.Enum.CardType.Ticket;
                card.Status = CardStatus.Enabled;
                card.OwnerName = card.CardID;
                card.HolidayEnabled = true;
                card.CanRepeatIn = false;
                card.CanRepeatOut = false;
                card.WithCount = true;
                card.CanEnterWhenFull = true;
                card.EnableWhenExpired = true;
                card.OnlineHandleWhenOfflineMode = true;
                Ralid.Park.BusinessModel.Result.CommandResult result = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).AddCard(card);
                if (result.Result == ResultCode.Successful)
                {
                    return card;
                }

            }
            return null;
        }

        /// <summary>
        /// 播放和显示票箱提示语
        /// </summary>
        private void SpeakAndShowPrompt()
        {
            if (IsExitDevice)
            {
                //出口时，播放请刷卡或交回临时卡
                SpeakAndShow(Hardware.DeviceVoiceAndMessage.ePROMPT_QSKHJHLSK);
            }
            else
            {
                //入口时，播放欢迎光临，请取卡语音并显示
                SpeakAndShow(Hardware.DeviceVoiceAndMessage.ePROMPT_HYGLQQK);
            }
        }
        #endregion

        #region 快照抓拍相关
        private string CarPlateSnapShot(int parkID, int entranceID)
        {
            string result = string.Empty;
            if (SnapShotCaptureService.CurrentInstance != null)
            {
                try
                {
                    result = SnapShotCaptureService.CurrentInstance.SnapShot(parkID, entranceID);
                }
                catch (Exception ex)
                {
                    ExceptionPolicy.HandleException(ex);
                }
            }
            return result;
        }
        #endregion

        #region 车牌识别有关
        private PlateRecognitionResult CarPalteRecognize(int parkID, int entranceID)
        {
            PlateRecognitionResult result = new PlateRecognitionResult();
            if (PlateRecognitionService.CurrentInstance != null)
            {
                try
                {
                    result = PlateRecognitionService.CurrentInstance.Recognize(parkID, entranceID);
                }
                catch (Exception ex)
                {
                    ExceptionPolicy.HandleException(ex);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取指定车牌号码的非临时性卡片名单
        /// </summary>
        /// <param name="carplate">车牌号码</param>
        /// <param name="maxCarPlateErrorChar">车牌号码允许错误的字符个数</param>
        /// <returns></returns>
        private List<CardInfo> GetCardHasCarplate(string carplate, int maxCarPlateErrorChar)
        {
            List<CardInfo> cards = null;
            if (maxCarPlateErrorChar == 0)
            {
                CardSearchCondition con = new CardSearchCondition();
                con.ListCarPlate = carplate;
                con.ListType = CardListType.Card;
                cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(con).QueryObjects;
                //只处理非临时卡
                cards = cards.Where(c => !c.IsTempCard).ToList();
            }
            else if (maxCarPlateErrorChar > 0)
            {
                List<CardInfo> temp = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetAllCards().QueryObjects;
                foreach (CardInfo card in temp)
                {
                    //只处理非临时卡
                    if (!card.IsTempCard && !string.IsNullOrEmpty(card.CarPlate))  //多个车牌号分开处理
                    {
                        //多个车牌号分开处理,车牌号可用中文或英文逗号分隔
                        string[] strs = card.CarPlate.Split(',', '，');
                        foreach (string str in strs)
                        {
                            if (CarPlateComparer.CarPlateComparison(carplate, str, maxCarPlateErrorChar))
                            {
                                if (cards == null) cards = new List<CardInfo>();
                                cards.Add(card);
                            }
                        }
                    }
                }
            }
            return cards;
        }
        #endregion

        #region 保护方法
        protected bool IsReadCardIntervalOver(DateTime eventDateTime)
        {
            TimeSpan ts = new TimeSpan(eventDateTime.Ticks - _lastCardReadTime.Ticks);
            if (Math.Abs(ts.TotalSeconds) < this.EntranceInfo.ReadCardInterval)
            {
                return false;
            }
            else
            {
                _lastCardReadTime = eventDateTime;
                return true;
            }
        }

        /// <summary>
        /// 生成警报报告
        /// </summary>
        /// <param name="type"></param>
        /// <param name="description"></param>
        protected void CreateAlarmReport(AlarmType type, string description, string operatorid)
        {
            AlarmReport alarm = new AlarmReport(
                                         this.Park.ParkID, this.EntranceID, DateTime.Now,
                                         this.EntranceName, type,
                                         description, operatorid);
            if (this.AlarmReporting != null) this.AlarmReporting(this, alarm);
        }
        #endregion

        #region 保护方法 产生事件
        protected virtual void OnDeviceResetReporting(DeviceResetReport report)
        {
            if (this.DeviceResetReporting != null) this.DeviceResetReporting(this, report);
        }

        protected virtual void OnCarSenseReporting(CarSenseReport report)
        {
            if (this.CarSenseReporting != null) this.CarSenseReporting(this, report);
        }

        protected virtual void OnCaptureACardReporting(CardCaptureReport report)
        {
            if (this.CaptureACardReporting != null) this.CaptureACardReporting(this, report);
        }

        protected virtual void OnButtonClickedReporting(ButtonClickedReport report)
        {
            if (this.ButtonClickedReporting != null) this.ButtonClickedReporting(this, report);
        }

        protected virtual void OnTakeoutCardReporting(CardTakeoutReport report)
        {
            if (this.TakeoutCardReporting != null) this.TakeoutCardReporting(this, report);
        }

        protected virtual void OnCardReadingReporting(CardReadReport report)
        {
            if (this.CardReadingReporting != null) this.CardReadingReporting(this, report);
        }

        protected virtual void OnCardWaitReporting(OfflineCardReadReport report)
        {
            if (this.CardWaitReporting != null) this.CardWaitReporting(this, report);
        }

        protected virtual void OnCardPermittedReporting(OfflineCardReadReport report)
        {
            if (this.CardPermittedReporting != null) this.CardPermittedReporting(this, report);
        }

        protected virtual void OnCardDeniedReporting(CardInvalidEventReport report)
        {
            if (this.CardDeniedReporting != null) this.CardDeniedReporting(this, report);
        }

        protected virtual void OnStatusChangedReporting(EntranceStatusReport report)
        {
            if (this.StatusChangedReporting != null) this.StatusChangedReporting(this, report);
        }

        protected virtual void OnAlarmReporting(AlarmReport report)
        {
            if (this.AlarmReporting != null) this.AlarmReporting(this, report);
        }

        protected virtual void OnParkVacantReporting(ParkVacantReport report)
        {
            if (this.ParkVacantReporting != null) this.ParkVacantReporting(this, report);
        }

        protected virtual void OnCommandEchoReporting(CommandEchoReport report)
        {
            if (this.CommandEchoReporting != null) this.CommandEchoReporting(this, report);
        }
        #endregion

        #region 控制板事件处理程序
        private void ReportHandler(ReportBase report)
        {
            try
            {
                if (report is EntranceStatusReport)
                {
                    OnStatusChangedReporting(report as EntranceStatusReport);
                }
                else if (report is DeviceResetReport)
                {
                    OnDeviceResetReporting(report as DeviceResetReport);
                }
                else if (report is CarSenseReport)
                {
                    CarSenseHandler(report as CarSenseReport);
                }
                else if (report is ButtonClickedReport)
                {
                    CardButtonHandler(report as ButtonClickedReport);
                }
                else if (report is CardTakeoutReport)
                {
                    OnTakeoutCardReporting(report as CardTakeoutReport);
                }
                else if (report is CardCaptureReport)
                {
                    CardCaptureHandler(report as CardCaptureReport);
                }
                else if (report is CardReadReport)
                {
                    CardReadReport crr = report as CardReadReport;
                    if (crr.IsCarPlateEventHandle)
                    {
                        CarPlateListHandler(report as CardReadReport);
                    }
                    else
                    {
                        CardReadReportHandler(report as CardReadReport);
                    }
                }
                else if (report is ParkVacantReport)
                {
                    ParkVacantHandler(report as ParkVacantReport);
                }
                else if (report is CarplateRecReport)
                {
                    CarplateRecHandler(report as CarplateRecReport);
                }
                else if (report is OfflineCardReadReport)
                {
                    OfflineCardReadReport cer = report as OfflineCardReadReport;
                    if (cer.IsCarPlateEventHandle)
                    {
                        OfflineCarPlateListHandler(cer);
                    }
                    else
                    {
                        if (cer.EventStatus == CardEventStatus.Valid)
                        {
                            CardPermitedHandler(cer);
                        }
                        else
                        {
                            CardWaitingHandler(cer);
                        }
                    }
                }
                else if (report is CardInvalidEventReport)
                {
                    CardInvalidHandler(report as CardInvalidEventReport);
                }
                else if (report is CommandEchoReport)
                {
                    CommandEchoHandler(report as CommandEchoReport);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void CarSenseHandler(CarSenseReport report)
        {
            if (report.InOrOutFlag == 1)
            {
                OptStatus = EntranceOperationStatus.CarArrival;

                //如果是在线模式的，并且设置了固定卡类自动识别车牌进出场的，生成处理车牌识别事件
                if (Parent.WorkMode == ParkWorkMode.Fool
                    && UserSetting.Current != null
                    && UserSetting.Current.FixCardAccessWhenRecognize)
                {
                    CarplateRecReport re = new CarplateRecReport(report.ParkID, report.EntranceID, report.EventDateTime, this.EntranceName);
                    AddToReportPool(re);
                }
                else if (AppSettings.CurrentSetting.SpeakPromptWhenCarArrival)
                {
                    SpeakAndShowPrompt();
                }
            }
            else
            {
                OptStatus = EntranceOperationStatus.CarLeave;
            }
            if (IsExitDevice)
            {
                StopCapture(); //出口在收到车到车走事件时取消收卡 

                
                if (_TicketReader != null)
                {
                    if (!_TicketReader.Opened)
                    {
                        //如果条码枪1串口打开失败，通知上位机
                        NotifyBarcodeReaderStatus(false);
                    }
                }

                if (_TicketReader2 != null)
                {
                    if (!_TicketReader2.Opened)
                    {
                        //如果条码枪2串口打开失败，通知上位机
                        NotifyBarcodeReader2Status(false);
                    }
                }
            }
            
            if (!string.IsNullOrEmpty(_CurText))
            {
                //如果票箱当前显示信息不为空，而条码枪1或条码枪2或纸票打印机连接正常时，将当前票箱显示信息设为公司名称
                if ((_TicketReader != null && _TicketReader.Opened)
                    || (_TicketReader2 != null && _TicketReader2.Opened)
                    || (_TicketPrinter != null && _TicketPrinter.Status == PrinterStatus.Ok))
                {
                    DisplayMsg(UserSetting.Current.CompanyName, true);
                    _CurText = null;
                }
            }

            OnCarSenseReporting(report);
        }

        private void CardButtonHandler(ButtonClickedReport report)
        {
            OnButtonClickedReporting(report);
        }

        private void CardCaptureHandler(CardCaptureReport report)
        {
            this.StopCapture(); //收到收卡一张事件后停止再收卡
            OnCaptureACardReporting(report);
        }

        private void CardReadReportHandler(CardReadReport report)
        {
            OnCardReadingReporting(report);
        }

        private void CardWaitingHandler(OfflineCardReadReport report)
        {
            OnCardWaitReporting(report);
        }

        private void CardPermitedHandler(OfflineCardReadReport report)
        {
            OnCardPermittedReporting(report);
        }

        private void CardInvalidHandler(CardInvalidEventReport report)
        {
            OnCardDeniedReporting(report);
        }

        private void ParkVacantHandler(ParkVacantReport report)
        {
            OnParkVacantReporting(report);
        }

        /// <summary>
        /// 处理卡片名单固定卡的车牌识别事件
        /// </summary>
        /// <param name="report"></param>
        private void CarplateRecHandler(CarplateRecReport report)
        {
            try
            {
                string carPlate = string.Empty;
                for (int i = 0; i < 3; i++)
                {
                    PlateRecognitionResult ret = CarPalteRecognize(this.Park.RootParkID, this.EntranceID);
                    //ret.CarPlate = "粤CLP681";
                    if (ret != null && !string.IsNullOrEmpty(ret.CarPlate) && ret.CarPlate != carPlate)
                    {
                        carPlate = ret.CarPlate;
                        if (AppSettings.CurrentSetting.Debug) FileLog.Log(this.EntranceName, "识别到车牌号 " + ret.CarPlate);
                        //先寻找完全匹配的车牌号，如果启用了允许车牌号有误差，再用较慢的方法寻找相匹配的车牌号
                        //这样的话只有车牌号不能完全匹配时才影响会多做一步费时的操作。
                        List<CardInfo> cards = GetCardHasCarplate(ret.CarPlate, 0);
                        if ((cards == null || cards.Count == 0) && UserSetting.Current.MaxCarPlateErrorChar > 0)
                        {
                            cards = GetCardHasCarplate(ret.CarPlate, UserSetting.Current.MaxCarPlateErrorChar);
                        }
                        if (cards != null && cards.Count == 1)
                        {
                            CardReadReport re = new CardReadReport();
                            re.CardID = cards[0].CardID;
                            re.ParkingData = null;
                            re.ParkID = this.Park.ParkID;
                            re.EntranceID = this.EntranceID;
                            re.EventDateTime = DateTime.Now;
                            re.CannotIgnored = true;
                            re.Reader = Ralid.Park.BusinessModel.Enum.EntranceReader.DeskTopReader;
                            re.LastCarPlate = string.Empty;
                            re.CarPlate = ret.CarPlate;
                            this.AddToReportPool(re);
                            return;//返回
                            //break; //退出循环
                        }
                        else
                        {
                            if (cards != null && cards.Count > 1)
                            {
                                //提示匹配到多个卡片名单
                                if (AppSettings.CurrentSetting.Debug) FileLog.Log(this.EntranceName, "多个卡片名单匹配到车牌:" + ret.CarPlate);
                                CreateAlarmReport(AlarmType.CarPlateFail,
                                     Resources.Resource1.EntranceBase_MultiCardList + ret.CarPlate + " " + Resources.Resource1.EntranceBase_NeedManualProcessing, string.Empty);
                            }
                        }
                    }
                    Thread.Sleep(200);
                } //end for

                if (AppSettings.CurrentSetting.SpeakPromptWhenCarArrival)
                {
                    SpeakAndShowPrompt();
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 车牌名单处理流程
        /// </summary>
        /// <param name="report"></param>
        private void CarPlateListHandler(CardReadReport report)
        {
            try
            {
                if (report.IsRecognitionFailure)
                {
                    //没有识别到车牌
                    if (report.IsCarNotPlate)
                    {
                        //如果是无车牌车辆，按无车牌车辆流程处理
                        NoCarPlateHander(report);
                    }
                    else
                    {
                        //提示进行人工处理
                        CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_NeedManualProcessing, string.Empty);
                    }
                }
                else
                {
                    CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                    List<CardInfo> cards = bll.GetCarPlateLists(report.CarPlate);
                    if (cards == null || cards.Count == 0)
                    {
                        //如果没有找到车牌名单，按临时车牌流程处理
                        TempCarPlateListHander(report);
                    }
                    else if (cards.Count == 1)
                    {
                        //将卡号重新赋值
                        report.CardID = cards[0].CardID;
                        report.IsCarPlateEventHandle = false;//标记按卡片事件处理
                        this.AddToReportPool(report);
                    }
                    else if (cards.Count > 1)
                    {
                        //匹配到多个车牌名单，提示人工介入
                        if (AppSettings.CurrentSetting.Debug) FileLog.Log(this.EntranceName, "多个车牌名单匹配到车牌:" + report.CarPlate + " 需要手动输入车牌号放行");
                        CreateAlarmReport(AlarmType.CarPlateFail,
                                 Resources.Resource1.EntranceBase_MultiCarPlateList + report.CarPlate + " " + Resources.Resource1.EntranceBase_NeedManualProcessing, string.Empty);
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 无车牌车辆流程处理
        /// </summary>
        /// <param name="report"></param>
        private void NoCarPlateHander(CardReadReport report)
        {
            try
            {
                if (!this.EntranceInfo.IsExitDevice)
                {
                    //入口时进行图片抓拍
                    string path = CarPlateSnapShot(this.Park.ParkID, this.EntranceID);
                    if (!string.IsNullOrEmpty(path))
                    {
                        //抓拍成功后,按临时车牌流程处理
                        TempCarPlateListHander(report);
                    }
                    else
                    {
                        //提示进行人工处理
                        CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_SnapShotFail, string.Empty);
                    }
                }
                else
                {
                    //提示进行人工处理
                    CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_NeedManualProcessing, string.Empty);
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 按临时车牌流程处理
        /// </summary>
        /// <param name="report"></param>
        private void TempCarPlateListHander(CardReadReport report)
        {
            try
            {
                if (!this.EntranceInfo.IsExitDevice)
                {
                    //生成临时车牌名单
                    CardInfo card = CreateATempCarPlateList(report.CarPlate);
                    if (card != null)
                    {
                        //生成临时车牌名后,将卡号重新赋值
                        report.CardID = card.CardID;
                        report.IsCarPlateEventHandle = false;//标记按卡片事件处理
                        this.AddToReportPool(report);
                    }
                    else
                    {
                        //提示进行人工处理
                        CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_NeedManualProcessing, string.Empty);
                    }
                }
                else
                {
                    //提示进行人工处理
                    //CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_NeedManualProcessing, string.Empty);
                    CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_FindNotCarPlateList + report.CarPlate, string.Empty);
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void OfflineCarPlateListHandler(OfflineCardReadReport report)
        {
            try
            {
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                List<CardInfo> cards = bll.GetCarPlateLists(report.CarPlate);
                if (cards == null || cards.Count == 0)
                {
                    //如果没有找到车牌名单，提示车牌识别失败
                    CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_FindNotCarPlateList + report.CarPlate, string.Empty);
                }
                else if (cards.Count == 1)
                {
                    //将卡号重新赋值
                    report.CardID = cards[0].CardID;
                    report.IsCarPlateEventHandle = false;//标记按卡片事件处理
                    this.AddToReportPool(report);
                }
                else if (cards.Count > 1)
                {
                    //匹配到多个车牌名单，提示
                    if (AppSettings.CurrentSetting.Debug) FileLog.Log(this.EntranceName, "多个车牌名单匹配到车牌:" + report.CarPlate);
                    CreateAlarmReport(AlarmType.CarPlateFail, Resources.Resource1.EntranceBase_MultiCarPlateList + report.CarPlate, string.Empty);
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void CommandEchoHandler(CommandEchoReport report)
        {
            OnCommandEchoReporting(report);
        }
        #endregion

        #region 公共事件
        /// <summary>
        /// 控制器复位时产生此事件
        /// </summary>
        public event ReportingHandler<DeviceResetReport> DeviceResetReporting;
        /// <summary>
        /// 地感状态变化时产生此事件
        /// </summary>
        public event ReportingHandler<CarSenseReport> CarSenseReporting;
        /// <summary>
        /// 收卡机收卡一张时产生此事件
        /// </summary>
        public event ReportingHandler<CardCaptureReport> CaptureACardReporting;
        /// <summary>
        /// 按下取卡按钮时产生此事件
        /// </summary>
        public event ReportingHandler<ButtonClickedReport> ButtonClickedReporting;
        /// <summary>
        /// 出卡机出卡一张时产生此事件
        /// </summary>
        public event ReportingHandler<CardTakeoutReport> TakeoutCardReporting;
        /// <summary>
        /// 在线模式读卡器刷卡或远程读卡时产生此事件
        /// </summary>
        public event ReportingHandler<CardReadReport> CardReadingReporting;
        /// <summary>
        /// 离线模式刷卡且卡片需等待用户确认时产生此事件
        /// </summary>
        public event ReportingHandler<OfflineCardReadReport> CardWaitReporting;
        /// <summary>
        /// 离线模式刷卡且卡片有效控制器抬闸放行后产生此事件
        /// </summary>
        public event ReportingHandler<OfflineCardReadReport> CardPermittedReporting;
        /// <summary>
        /// 离线模式刷卡卡片无效时产生此事件
        /// </summary>
        public event ReportingHandler<CardInvalidEventReport> CardDeniedReporting;
        /// <summary>
        /// 控制器状态发生变化时产生此事件
        /// </summary>
        public event ReportingHandler<EntranceStatusReport> StatusChangedReporting;
        /// <summary>
        /// 离线模式停车场车位数变化时产生此事件
        /// </summary>
        public event ReportingHandler<ParkVacantReport> ParkVacantReporting;
        /// <summary>
        /// 有报警时产生此事件
        /// </summary>
        public event ReportingHandler<AlarmReport> AlarmReporting;
        /// <summary>
        /// 控制器发卡机内临时卡数量变化时产生此事件
        /// </summary>
        public event ReportingHandler<EntranceRemainTempCardReport> EntranceRemainTempCardReporting;
        /// <summary>
        /// 在线模式上位机发送卡片有效指令，控制板执行成功时产生此事件
        /// </summary>
        public event ReportingHandler<CommandEchoReport> CommandEchoReporting;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置所属停车场
        /// </summary>
        public ParkBase Parent { get; set; }

        public EntranceInfo EntranceInfo
        {
            get { return _entrance; }
            set { _entrance = value; }
        }
        /// <summary>
        /// 获取停车场编号
        /// </summary>
        public int ParkID
        {
            get
            {
                return _entrance.ParkID;
            }
        }
        /// <summary>
        /// 获取停车场信息
        /// </summary>
        public ParkInfo Park
        {
            get
            {
                return Parent.Park;
            }
        }
        /// <summary>
        /// 获取控制器ID
        /// </summary>
        public int EntranceID
        {
            get
            {
                return _entrance.EntranceID;
            }
        }
        /// <summary>
        /// 获取通道名称
        /// </summary>
        public string EntranceName
        {
            get { return _entrance.EntranceName; }
        }
        /// <summary>
        /// 是否是出口控制器
        /// </summary>
        public bool IsExitDevice
        {
            get { return EntranceInfo.IsExitDevice; }
        }
        /// <summary>
        /// 获取或设置控制器运行状态
        /// </summary>
        public EntranceOperationStatus OptStatus { get; set; }
        /// <summary>
        /// 获取或设置目前正在控制器中待处理的卡片事件
        /// </summary>
        public CardEventReport ProcessingEvent { get; set; }
        /// <summary>
        /// 获取或设置控制器目前正在处理的卡片
        /// </summary>
        public CardInfo ProcessingCard { get; set; }
        /// <summary>
        /// 获取或设置通道发卡机还剩余的临时卡数量
        /// </summary>
        public int RemainTempCard
        {
            get
            {
                return _entrance.TempCard;
            }
            set
            {
                _entrance.TempCard = value;
                (new EntranceBll(AppSettings.CurrentSetting.ParkConnect)).Update(_entrance);
                EntranceRemainTempCardReport report = new EntranceRemainTempCardReport(ParkID, _entrance.EntranceID, DateTime.Now, _entrance.EntranceName, value);
                if (this.EntranceRemainTempCardReporting != null) this.EntranceRemainTempCardReporting(this, report);
            }
        }
        /// <summary>
        /// 获取或设置控制器状态
        /// </summary>
        public EntranceStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    EntranceStatusReport report = new EntranceStatusReport(ParkID, EntranceID, DateTime.Now, EntranceName, _status);
                    AddToReportPool(report);
                }
            }
        }
        /// <summary>
        /// 获取或设置当前卡片事件的确认操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 获取或设置当前卡片事件的确认工作站
        /// </summary>
        public string Station { get; set; }


        #endregion

        #region 抽象方法 具体子类需实现
        /// <summary>
        /// 获取控制板的临时卡读头
        /// </summary>
        public abstract bool IsTempReader(EntranceReader reader);
        /// <summary>
        /// 控制器复位
        /// </summary>
        public abstract void Reset();
        /// <summary>
        /// 同步时间
        /// </summary>
        public abstract void SyncTime();
        /// <summary>
        /// 发卡机出卡一张
        /// </summary>
        public abstract void TakeOutACard();
        /// <summary>
        /// 开始收卡
        /// </summary>
        public abstract void StartCapture();
        /// <summary>
        /// 结束收卡
        /// </summary>
        public abstract void StopCapture();
        /// <summary>
        /// 应用用户设置
        /// </summary>
        /// <param name="us"></param>
        public abstract bool ApplyUserSetting(UserSetting us);
        /// <summary>
        /// 应用费率设置
        /// </summary>
        /// <param name="tariffSetting"></param>
        public abstract bool ApplyTariffSetting(TariffSetting tariffSetting);
        /// <summary>
        /// 应用车位余数设置
        /// </summary>
        /// <param name="cps"></param>
        /// <returns></returns>
        public abstract bool ApplyCarPortSetting(CarPortSetting cps);
        /// <summary>
        /// 应用通道权限
        /// </summary>
        /// <param name="accessSetting"></param>
        public abstract bool ApplyAccessSetting(AccessSetting accessSetting);
        /// <summary>
        /// 应用节假日
        /// </summary>
        /// <param name="holidaySetting"></param>
        /// <returns></returns>
        public abstract bool ApplyHolidaySetting(HolidaySetting holidaySetting);
        /// <summary>
        /// 应用密钥设置
        /// </summary>
        /// <param name="keySetting"></param>
        /// <returns></returns>
        public abstract bool ApplyKeySetting(KeySetting keySetting);
        /// <summary>
        /// 显示消息
        /// </summary>
        public abstract void DisplayMsg(string msg, bool permanent);
        /// <summary>
        /// 显示车余数
        /// </summary>
        public abstract void ShowVacant();
        /// <summary>
        /// 操作道闸
        /// </summary>
        /// <param name="notify"></param>
        public abstract void OperateGate(GateOperationNotify notify);
        /// <summary>
        /// 上传卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool SaveCard(CardInfo card, ActionType action);
        ///// <summary>
        ///// 上传卡片到控制板
        ///// </summary>
        ///// <param name="card"></param>
        ///// <returns></returns>
        //public abstract bool SaveCard(CardInfo card, ActionType action,bool savefail);
        /// <summary>
        /// 上传多张卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool SaveCards(List<CardInfo> cards, ActionType action);
        ///// <summary>
        ///// 上传多张卡片到控制板
        ///// </summary>
        ///// <param name="card"></param>
        ///// <returns></returns>
        //public abstract bool SaveCards(List<CardInfo> cards, ActionType action, bool savefail);
        /// <summary>
        /// 删除卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool DeleteCard(CardInfo card);
        /// <summary>
        /// 清空控制制卡片
        /// </summary>
        /// <returns></returns>
        public abstract bool ClearCard();
        /// <summary>
        /// 向控制板发送卡片有效
        /// </summary>
        public abstract void CardValid();
        /// <summary>
        /// 向控制板发送卡片等待
        /// </summary>
        public abstract void CardWait();
        /// <summary>
        /// 向控制板发送卡片无效
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="cardType">卡类型（为空时使用硬件卡类型）</param>
        /// <param name="hcardType">硬件卡类型</param>
        /// <param name="reader">读头</param>
        /// <param name="invalidType">无效代码</param>
        /// <param name="param">参数</param>
        public abstract void CardInValid(string cardID, CardType cardType, byte hcardType, EntranceReader reader, EventInvalidType invalidType, object param);
        /// <summary>
        /// 向控制板获取识别到的车牌号码
        /// </summary>
        /// <returns></returns>
        public abstract string GetRecognizedCarPlate();
        /// <summary>
        /// 播放语音，同时把播放内容显示到屏上
        /// </summary>
        /// <param name="msg"></param>
        public abstract void SpeakAndShow(Ralid.Park.Hardware.DeviceVoiceAndMessage msg);
        #endregion

        #region 公共方法
        /// <summary>
        /// 把事件放入事件池中
        /// </summary>
        /// <param name="report"></param>
        public void AddToReportPool(ReportBase report)
        {
            lock (_PacketQueueLocker)
            {
                //如果处理队列中的消息超过最大数量,则丢弃最开始的一个事件,由于这里的事件都有实时性要求,队列中的事件积压的多,则表明前面的事件已经不用处理了
                if (_PacketQueue.Count == 10) _PacketQueue.Dequeue();
                _PacketQueue.Enqueue(report);
                _PacketRecieveEvent.Set();  //
            }
        }
        /// <summary>
        /// 清空事件队列
        /// </summary>
        public void ClearReportPool()
        {
            lock (_PacketQueueLocker)
            {
                _PacketQueue.Clear();
                _PacketRecieveEvent.Reset();
            }
        }
        /// <summary>
        /// 控制板出纸票一张
        /// </summary>
        public bool TakeoutATicket()
        {
            DisplayMsg(Resources.Resource1.EntranceBase_PrintingTicket, false);
            CardInfo card = CreateATicket();
            if (card != null)
            {
                DateTime eventDateTime = DateTime.Now;
                if (PrintATicket(card.CardID, eventDateTime))
                {
                    //转发消息
                    CardReadReport cardRead = new CardReadReport();
                    cardRead.ParkID = ParkID;
                    cardRead.EntranceID = this.EntranceID;
                    cardRead.CardID = card.CardID;
                    cardRead.CardType = (byte)card.CardType;
                    cardRead.CannotIgnored = true;
                    cardRead.EventDateTime = eventDateTime;
                    cardRead.Reader = GetFirstTempReader();
                    AddToReportPool(cardRead);
                    //纸票数量级减少一张
                    if (RemainTempCard > 0) RemainTempCard = RemainTempCard - 1;
                    if (!string.IsNullOrEmpty(_CurText))
                    {
                        DisplayMsg(UserSetting.Current.CompanyName, true);
                        _CurText = null;
                    }
                    return true;
                }
                else
                {
                    (new CardBll(AppSettings.CurrentSetting.ParkConnect)).DeleteCard(card);
                    NotifyTicketPrinterStatus(_TicketPrinter.Status);
                    if (_TicketPrinter.Status == PrinterStatus.OffLine || _TicketPrinter.Status == PrinterStatus.COMPortNotOpen)
                    {
                        _ReaderPrinterOfflineEvent.Set();
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 通知其打印机状态变化
        /// </summary>
        /// <param name="status"></param>
        public void NotifyTicketPrinterStatus(PrinterStatus status)
        {
            if (status != PrinterStatus.Ok || status != _LastTicketPrinterStatus) //如果打印机状态不正常或发生变化
            {
                _LastTicketPrinterStatus = status;
                _CurText = PrinterStatusDescription.GetDescription(status);
                DisplayMsg(_CurText, true);
                AlarmReport report = new AlarmReport(ParkID, _entrance.EntranceID, DateTime.Now, _entrance.EntranceName,
                    AlarmType.PrinterStatus, PrinterStatusDescription.GetDescription(status), string.Empty);
                if (this.AlarmReporting != null) this.AlarmReporting(this, report);
            }
        }
        /// <summary>
        /// 通知其条码枪状态变化
        /// </summary>
        /// <param name="status"></param>
        public void NotifyBarcodeReaderStatus(bool opened)
        {
            if (!opened || opened != _LastTicketReaderOpened) //如果条码枪串口未打开或发生变化
            {
                _LastTicketReaderOpened = opened;
                //如果条码枪1、2串口都未打开，并且没有在票箱上提示的，在票箱显示屏上提示
                if (!_LastTicketReaderOpened && !_LastTicketReader2Opened && string.IsNullOrEmpty(_CurText))
                {
                    _CurText = BarcodeStatusDescription.GetDescription(opened, 1);
                    DisplayMsg(_CurText, true);
                }

                AlarmReport report = new AlarmReport(ParkID, _entrance.EntranceID, DateTime.Now, _entrance.EntranceName,
                    AlarmType.BarcodeGunStatus, BarcodeStatusDescription.GetDescription(opened, 1), string.Empty);
                if (this.AlarmReporting != null) this.AlarmReporting(this, report);
            }
        }
        /// <summary>
        /// 通知其条码枪2状态变化
        /// </summary>
        /// <param name="status"></param>
        public void NotifyBarcodeReader2Status(bool opened)
        {
            if (!opened || opened != _LastTicketReader2Opened) //如果条码枪串口未打开或发生变化
            {
                _LastTicketReader2Opened = opened;
                //如果条码枪1、2串口都未打开，并且没有在票箱上提示的，在票箱显示屏上提示
                if (!_LastTicketReaderOpened && !_LastTicketReader2Opened && string.IsNullOrEmpty(_CurText))
                {
                    _CurText = BarcodeStatusDescription.GetDescription(opened, 2);
                    DisplayMsg(_CurText, true);
                }

                AlarmReport report = new AlarmReport(ParkID, _entrance.EntranceID, DateTime.Now, _entrance.EntranceName,
                    AlarmType.BarcodeGunStatus, BarcodeStatusDescription.GetDescription(opened, 2), string.Empty);
                if (this.AlarmReporting != null) this.AlarmReporting(this, report);
            }
        }
        /// <summary>
        /// 释放对象资源
        /// </summary>
        public virtual void Dispose()
        {
            if (_TicketPrinter != null) _TicketPrinter.Close();
            if (_TicketReader != null) _TicketReader.Close();
            _PacketHandleThread.Abort();
        }
        #endregion
    }
}
