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

            if (info.TicketReaderCOMPort > 0)
            {
                _TicketReader = new BarCodeReader(info.TicketReaderCOMPort);
                _TicketReader.BarCodeRead += new BarCodeReadEventHandler(TicketReader_BarCodeRead);
                _TicketReader.Open();
            }
            if (info.TicketPrinterCOMPort > 0)
            {
                _TicketPrinter = new KPM150BarCodePrinter(info.TicketPrinterCOMPort);
                _TicketPrinter.Open();
            }
            //初始化处理消息包的线程
            _PacketHandleThread = new Thread(ReportHandle_Thread);
            _PacketHandleThread.Start();

            Thread t = new Thread(new ThreadStart(HeartBeatCheck_Thread));
            t.IsBackground = true;
            t.Start();
        }
        #endregion

        #region 私有变量
        private EntranceInfo _entrance;
        private DateTime _lastCardReadTime = new DateTime(2011, 11, 11);//DateTime.Now;
        private EntranceStatus _status;

        protected KPM150BarCodePrinter _TicketPrinter;
        private PrinterStatus _LastTicketPrinterStatus = PrinterStatus.Ok;
        private BarCodeReader _TicketReader;

        private Queue<ReportBase> _PacketQueue = new Queue<ReportBase>();
        private object _PacketQueueLocker = new object();
        private AutoResetEvent _PacketRecieveEvent = new AutoResetEvent(false);
        private Thread _PacketHandleThread;

        protected DateTime _LastEventDatetime = DateTime.Now;
        protected readonly int _OfflineTimeout = 15;  //断开超时时间，如果在超过此时间内没有收到控制器事件，则认为硬件已经离线

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
                        DateTime dt = DateTime.Now;
                        report.EventDateTime = dt;
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
            CardReadReport report = new CardReadReport();
            report.CardID = e.BarCode;
            report.EntranceID = this.EntranceID;
            report.Reader = GetFirstTempReader();
            AddToReportPool(report);
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
                Ralid.Park.BusinessModel.Result.CommandResult result = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).AddCard(card);
                if (result.Result == ResultCode.Successful)
                {
                    return card;
                }

            }
            return null;
        }
        /// 打印一张纸票
        private bool PrintATicket(string ticketID)
        {
            if (_TicketPrinter != null)
            {
                //生成纸票
                TicketInfo ticket = new TicketInfo();
                ticket.EventDateTime = DateTime.Now;
                ticket.Entrance = _entrance.EntranceName;
                ticket.CardID = ticketID;
                ticket.CompanyName = UserSetting.Current.CompanyName;
                ticket.Producter = "广州瑞立德";
                ticket.Reguard = "出场凭证,请勿折损!";
                //打印纸票
                if (_TicketPrinter.PrintTicketOfTyko(ticket))
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

        private List<CardInfo> GetCardHasCarplate(string carplate, int maxCarPlateErrorChar)
        {
            List<CardInfo> cards = null;
            if (maxCarPlateErrorChar == 0)
            {
                CardSearchCondition con = new CardSearchCondition();
                con.CarPlate = carplate;
                cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(con).QueryObjects;
            }
            else if (maxCarPlateErrorChar > 0)
            {
                List<CardInfo> temp = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetAllCards().QueryObjects;
                foreach (CardInfo card in temp)
                {
                    if (!string.IsNullOrEmpty(card.CarPlate))  //多个车牌号分开处理
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
                    CardReadReportHandler(report as CardReadReport);
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
                    if (cer.EventStatus == CardEventStatus.CarPlateFail)
                    {
                        CardWaitingHandler(cer);
                    }
                    else if (cer.EventStatus == CardEventStatus.Valid)
                    {
                        CardPermitedHandler(cer);
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
                CarplateRecReport re = new CarplateRecReport(report.ParkID, report.EntranceID, report.EventDateTime, this.EntranceName);
                AddToReportPool(re);
            }
            else
            {
                OptStatus = EntranceOperationStatus.CarLeave;
            }
            if (IsExitDevice) StopCapture(); //出口在收到车到车走事件时取消收卡 
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
            UserSetting us = UserSetting.Current;
            if (!IsExitDevice)
            {
                //按了取卡按钮后，只接受临时卡读头上的刷卡事件,防止远距离卡对取卡进行扰
                if (this.EntranceInfo.OnlyTempReaderAfterButtonClick && this.OptStatus == EntranceOperationStatus.CardTakeingOut && !IsTempReader(report.Reader)) return;
            }
            else
            {
                //防止无效远距离卡对出场进行干扰, 出口时如果收到刷卡事件时之前有未处理的卡片事件，且卡片事件由临时读头或远程读卡产生，
                //则此时忽略非临时读头或远程读卡的事件 
                if (report.Reader != EntranceReader.DeskTopReader && !IsTempReader(report.Reader))
                {
                    if (ProcessingEvent != null && (IsTempReader(ProcessingEvent.Reader) || ProcessingEvent.Reader == EntranceReader.DeskTopReader))
                    {
                        return;
                    }
                }
            }
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

        private void CarplateRecHandler(CarplateRecReport report)
        {
            try
            {
                string carPlate = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    PlateRecognitionResult ret = CarPalteRecognize(this.Park.RootParkID, this.EntranceID);
                    if (ret != null && !string.IsNullOrEmpty(ret.CarPlate) && ret.CarPlate != carPlate)
                    {
                        carPlate = ret.CarPlate;
                        FileLog.Log(this.EntranceName, "识别到车牌号 " + ret.CarPlate);
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
                            this.Carplate = ret.CarPlate;
                            this.AddToReportPool(re);
                            break; //退出循环
                        }
                        else if (cards == null || cards.Count == 0)
                        {
                            AlarmReport alarm = new AlarmReport(
                                     this.Park.ParkID, this.EntranceID, DateTime.Now,
                                     this.EntranceName, AlarmType.CarPlateFail,
                                     "未找到匹配的车牌，识别到的车牌号为:" + ret.CarPlate, string.Empty);
                            if (this.AlarmReporting != null) this.AlarmReporting(this, alarm);
                        }
                        else if (cards.Count > 1)
                        {
                            if (AppSettings.CurrentSetting.Debug) FileLog.Log(this.EntranceName, "多个人员匹配到车牌:" + ret.CarPlate + " 需要手动输入车牌号放行");
                            AlarmReport alarm = new AlarmReport(
                                     this.Park.ParkID, this.EntranceID, DateTime.Now,
                                     this.EntranceName, AlarmType.CarPlateFail,
                                     "多个人员匹配到车牌:" + ret.CarPlate + " 需要手动输入车牌号放行", string.Empty);
                            if (this.AlarmReporting != null) this.AlarmReporting(this, alarm);
                        }
                    }
                    Thread.Sleep(200);
                } //end for
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

        public string Carplate { get; set; }
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
        /// <summary>
        /// 上传卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool SaveCard(CardInfo card, ActionType action, bool savefail);
        /// <summary>
        /// 上传多张卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool SaveCards(List<CardInfo> cards, ActionType action);
        /// <summary>
        /// 上传多张卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public abstract bool SaveCards(List<CardInfo> cards, ActionType action, bool savefail);
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
        public abstract void CardInValid(EventInvalidType reason, object param);
        /// <summary>
        /// 向控制板获取识别到的车牌号码
        /// </summary>
        /// <returns></returns>
        public abstract string GetRecognizedCarPlate();
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
        public void TakeoutATicket()
        {
            DisplayMsg("正在出票", false);
            CardInfo card = CreateATicket();
            if (card != null)
            {
                if (PrintATicket(card.CardID))
                {
                    //转发消息
                    CardReadReport cardRead = new CardReadReport();
                    cardRead.ParkID = ParkID;
                    cardRead.EntranceID = this.EntranceID;
                    cardRead.CardID = card.CardID;
                    cardRead.CardType = (byte)card.CardType;
                    cardRead.CannotIgnored = true;
                    cardRead.EventDateTime = DateTime.Now;
                    AddToReportPool(cardRead);
                    //纸票数量级减少一张
                    if (RemainTempCard > 0) RemainTempCard = RemainTempCard - 1;
                }
                else
                {
                    (new CardBll(AppSettings.CurrentSetting.ParkConnect)).DeleteCard(card);
                    NotifyTicketPrinterStatus(_TicketPrinter.Status);
                }
            }
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
                DisplayMsg(PrinterStatusDescription.GetDescription(status), true);
                AlarmReport report = new AlarmReport(ParkID, _entrance.EntranceID, DateTime.Now, _entrance.EntranceName,
                    AlarmType.PrinterStatus, PrinterStatusDescription.GetDescription(status), string.Empty);
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
