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

namespace Ralid.Park.ParkService.CANParking
{
    public sealed class CANEntrance : EntranceBase
    {
        #region 构造函数
        public CANEntrance(CommComponent comPort, EntranceInfo info, ParkBase parent)
            : base(info, parent)
        {
            _CommComponent = comPort;
            _CommComponent.ReportReceviced += RecevicedPacketEventHandler;
            if (info.Address == CanAddress.HostEntrance)
            {
                SyncTime();
                SetEntranceMode(Parent.WorkMode);  //设置工作模式
                ////控制器复位 ,由于控制板在车压地感时复位会使其它地址加一，且厂家也推荐不要经常进行复位操作,但由于一号板偶尔会不正常，所以目前只复位一号板
                //Thread t1 = new Thread(ResetHardWare_Thread);
                //t1.IsBackground = true;
                //t1.Start();

                Thread t2 = new Thread(OnlineQuery_Thread);
                t2.IsBackground = true;
                t2.Start();
            }
        }
        #endregion

        #region 私有变量
        private PacketCreater _PacketCreater = new PacketCreater();
        private CommComponent _CommComponent;
        private object _StatusLock = new object();

        private System.Threading.AutoResetEvent _allSaved = new System.Threading.AutoResetEvent(true);

        private AutoResetEvent _CardValidResponseEvent = new AutoResetEvent(false); //卡片有效指令收到回复时的通知事件
        #endregion

        #region 私有方法
        private void ResetHardWare_Thread()
        {
            while (true)
            {
                Thread.Sleep(60000 * 60 * 2);
                Reset();
            }
        }

        private void OnlineQuery_Thread()
        {
            while (true)
            {
                Thread.Sleep(10 * 1000); //每
                OnlineQuery();
            }
        }

        private void ExecuteWaitingCommand_Thread()
        {
            CardBll cb = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
            while (true)
            {
                List<WaitingCommandInfo> wcs = wb.GetCommands(this.ParkID).QueryObjects;
                foreach (var wc in wcs)
                {
                    CardInfo card = cb.GetCardByID(wc.CardID).QueryObject;
                    if (card != null)
                    {
                        if (SaveCard(card, ActionType.Upate))
                        {
                            wb.Delete(wc);
                        }
                    }
                    else
                    {
                        wb.Delete(wc);
                    }
                }
                Thread.Sleep(60000);
            }
        }

        private void GetEvents_Thread()
        {
            //_EventIndexConf = string.Format("Park{0:D2}EventIndex", ParkID);
            //string temp = AppSettings.CurrentSetting.GetConfigContent(_EventIndexConf);
            //int.TryParse(temp, out _EventIndex);

            //while (_allSaved.WaitOne())
            //{
            //    FetchEventRequestNotify notify = new FetchEventRequestNotify(_EventIndex + 1, 5);
            //    this.ExecuteCommand<FetchEventRequestNotify>(notify);
            //}
        }

        private void SetEntranceMode(ParkWorkMode mode)
        {
            Packet packet = _PacketCreater.CreateSetWorkmodePacket(Address, (byte)(mode == ParkWorkMode.OffLine ? 0 : 3));
            _CommComponent.SendPacket(packet);
        }
        #endregion

        #region 事件处理程序
        private void RecevicedPacketEventHandler(object sender, ReportBase report)
        {
            if ((report is CardInvalidEventReport) ||
                (report is CardEventReport) ||
                (report is ParkVacantReport) ||
                (report is OfflineCardReadReport))  //如果是在线模式收到这些事件，则把控制器变成实时模式
            {
                if (Parent.WorkMode == ParkWorkMode.Fool)
                {
                    SetEntranceMode(ParkWorkMode.Fool);
                    return;
                }
            }

            //一号机的事件有时会以地址0出现
            if (report.Address == this.Address || (report.Address == 0 && this.Address == 1))
            {
                if (report is EntranceStatusReport)
                {
                    //如果是控制器状态报告,则不用进入事件处理流程,只要告诉系统硬件是活动的就可以了.
                }
                else
                {
                    if ((report is DeviceResetReport) && Parent.WorkMode == ParkWorkMode.Fool) //收到复位事件
                    {
                        SetEntranceMode(ParkWorkMode.Fool);
                    }
                    if (report is CommandEchoReport) _CardValidResponseEvent.Set();
                    AddToReportPool(report);  //消息入队
                }
                _LastEventDatetime = DateTime.Now;
                Status = EntranceStatus.Ok;
            }
        }

        protected override void OnButtonClickedReporting(ButtonClickedReport report)
        {
            if (this.IsExitDevice) return;
            lock (_StatusLock)
            {
                if (this.OptStatus != EntranceOperationStatus.CarArrival) return;
                this.OptStatus = EntranceOperationStatus.CardTakeingOut;
            }
            if (this._TicketPrinter == null && this.RemainTempCard > 0) this.RemainTempCard--;

            if (_TicketPrinter != null)
            {
                TakeoutATicket();
            }
            base.OnButtonClickedReporting(report);
        }

        protected override void OnCardReadingReporting(CardReadReport report)
        {
            UserSetting us = UserSetting.Current;
            if (!report.CannotIgnored)
            {
                if (EntranceInfo.ReadAndTakeCardNeedCarSense && (OptStatus != EntranceOperationStatus.CarArrival && OptStatus != EntranceOperationStatus.CardTakeingOut)) return; //要求地感读卡时,如果没有车压地感,则不处理
                if (!IsReadCardIntervalOver(DateTime.Now)) return; //如果未超过读卡间隔,不处理
            }
            base.OnCardReadingReporting(report);
        }

        protected override void OnTakeoutCardReporting(CardTakeoutReport report)
        {
            if (!this.IsExitDevice && this.ProcessingEvent != null && IsTempReader(this.ProcessingEvent.Reader) && this.ProcessingEvent.CardType.IsTempCard)
            {
                for (int i = 0; i < 2; i++)
                {
                    _CardValidResponseEvent.Reset();
                    if (this.EntranceInfo.CardValidNeedResponse && !_CardValidResponseEvent.WaitOne(3000))
                    {
                        //如果启用了卡片有效需要下位机回复选项,则如果在2秒钟内没有收到回复，则再发送一次卡片有效指令。
                    }
                    else
                    {
                        break;
                    }
                    if (AppSettings.CurrentSetting.Debug) FileLog.Log(EntranceName, "发送卡片有效指令 " + ProcessingEvent.CardID);
                    TempCardEnterValid(ProcessingEvent.EventDateTime);
                }
            }
            base.OnTakeoutCardReporting(report);
        }
        #endregion

        #region 向控制器硬件发送卡片有效,卡片等待,卡片无效等消息
        private void CardEnterWait(CardType cardType)
        {
            Packet p = _PacketCreater.CreateCardEnterWaitPacket(Address, cardType);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送月卡入场有效指令
        /// </summary>
        /// <param name="expiredDate"></param>
        private void MonthCardEnterValid(DateTime expiredDate)
        {
            Packet p = _PacketCreater.CreateMonthCardEnterValidPacket(Address, expiredDate);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送储值卡入场有效指令
        /// </summary>
        /// <param name="eventDt"></param>
        /// <param name="balance"></param>
        private void PrepayCardEnterValid(DateTime eventDt, decimal balance)
        {
            Packet p = _PacketCreater.CreatePrepayCardEnterValidPacket(Address, eventDt, balance);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送临时卡入场有效指令
        /// </summary>
        /// <param name="eventDt"></param>
        private void TempCardEnterValid(DateTime eventDt)
        {
            Packet p = _PacketCreater.CreateTempCardEnterValidPacket(Address, eventDt);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送月卡出场等待指令
        /// </summary>
        /// <param name="cardEvent"></param>
        private void MonthCardExitWait(CardEventReport cardEvent)
        {
            Packet p = _PacketCreater.CreateMonthCardExitWaitPacket(Address, cardEvent);
            _CommComponent.SendPacket(p);
            DisplayMsg("免收费放行", false);
        }
        /// <summary>
        /// 发送储值卡出场等待指令
        /// </summary>
        /// <param name="report"></param>
        private void PrepayCardExitWait(CardEventReport report)
        {
            Packet p = _PacketCreater.CreatePrepayCardExitWaitPacket(Address, report);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送临时卡出场等待指令
        /// </summary>
        /// <param name="report"></param>
        private void TempCardExitWait(CardEventReport report)
        {
            Packet p = _PacketCreater.CreateTempCardExitWaitPacket(Address, report);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送月卡出场有效指令
        /// </summary>
        /// <param name="expireDT"></param>
        private void MonthCardExitValid(DateTime expireDT)
        {
            Packet p = _PacketCreater.CreateMonthCardExitValidPacket(Address, expireDT);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送储值卡出场有效指令
        /// </summary>
        /// <param name="lastDT"></param>
        /// <param name="eventDT"></param>
        /// <param name="carType"></param>
        /// <param name="balance"></param>
        private void PrepayCardExitValid(DateTime lastDT, DateTime eventDT, byte carType, decimal balance)
        {
            Packet p = _PacketCreater.CreatePrepayCardExitValid(Address, lastDT, eventDT, carType, balance);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 发送临时卡出场有效指令
        /// </summary>
        /// <param name="lastDT"></param>
        /// <param name="eventDT"></param>
        /// <param name="carType"></param>
        private void TempCardExitValid(DateTime lastDT, DateTime eventDT, byte carType)
        {
            Packet p = _PacketCreater.CreateTempCardExitValidPacket(Address, lastDT, eventDT, (byte)CarTypeSetting.Current.GetHardwareCarType(carType));
            _CommComponent.SendPacket(p);
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取控制器地址
        /// </summary>
        public byte Address
        {
            get
            {
                return (byte)EntranceInfo.Address;
            }
        }

        /// <summary>
        /// 获取控制板的临时卡读头
        /// </summary>
        public override bool IsTempReader(EntranceReader reader)
        {
            return reader == EntranceReader.Reader1;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 控制器复位
        /// </summary>
        public override void Reset()
        {
            Packet packet = _PacketCreater.CreateDeviceResetPacket(Address);
            _CommComponent.SendPacket(packet);
        }

        /// <summary>
        /// 在线状态查询,如果设置在线返回true，否则返回false
        /// </summary>
        /// <returns></returns>
        public void OnlineQuery()
        {
            Packet packet = _PacketCreater.CreateQueryStatusPacket(Address);
            _CommComponent.SendPacket(packet);
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        public override void SyncTime()
        {
            Packet packet = _PacketCreater.CreateSyncTimePacket(Address);
            _CommComponent.SendPacket(packet);
        }

        /// <summary>
        /// 发卡机出卡一张
        /// </summary>
        public override void TakeOutACard()
        {
            Packet p = new Packet();
            p.Address = Address;
            p.Order = OrderCode.CPMR_Cardout;
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 开始收卡
        /// </summary>
        public override void StartCapture()
        {
            Packet p = new Packet();
            p.Address = Address;
            p.Order = 0xA3;  //收卡机收卡指令
            p.AddByte(1);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 结束收卡
        /// </summary>
        public override void StopCapture()
        {
            Packet p = new Packet();
            p.Address = Address;
            p.Order = 0xA3;  //收卡机收卡指令
            p.AddByte(0);
            _CommComponent.SendPacket(p);
        }
        /// <summary>
        /// 下载用户参数
        /// </summary>
        /// <param name="us"></param>
        public override bool ApplyUserSetting(UserSetting us)
        {
            //显示公司名称
            Packet p3 = _PacketCreater.CreateLEDDisplayPacket(Address, CanAddress.TicketBoxLed, us.CompanyName, true);
            _CommComponent.SendPacket(p3);

            Packet p4 = _PacketCreater.CreateLEDDisplayPacket(Address, CanAddress.ChargeLed, us.CompanyName, true);
            _CommComponent.SendPacket(p4);
            Thread.Sleep(1000);
            return true;
        }

        public override bool ApplyTariffSetting(TariffSetting tariffSetting)
        {
            if (Parent.WorkMode == ParkWorkMode.OffLine)
            {
                List<Packet> packets = _PacketCreater.CreateDownloadTariffSettingPacket(Address, tariffSetting);
                foreach (Packet packet in packets)
                {
                    _CommComponent.SendPacket(packet);
                    Thread.Sleep(1000);
                }
            }
            return true;
        }

        public override bool ApplyCarPortSetting(CarPortSetting cps)
        {
            if (Parent.WorkMode == ParkWorkMode.OffLine)
            {
                List<Packet> packets = _PacketCreater.CreateDownloadCarPortSettingPacket(Address, cps);
                foreach (Packet packet in packets)
                {
                    _CommComponent.SendPacket(packet);
                    Thread.Sleep(1000);
                }
            }
            return true;
        }

        public override bool ApplyAccessSetting(AccessSetting accessSetting)
        {
            return false;
        }

        public override bool ApplyHolidaySetting(HolidaySetting holidaySetting)
        {
            return false;
        }

        public override bool ApplyKeySetting(KeySetting keySetting)
        {
            return false;
        }

        //设置车位数
        public void SetVacant(short vacant)
        {
            Packet packet = _PacketCreater.CreateSetVacantPacket(Address, vacant);
            _CommComponent.SendPacket(packet);
        }

        /// <summary>
        /// 在LED上保存信息
        /// </summary>
        public override void DisplayMsg(string msg, bool permanent)
        {
            Packet p = _PacketCreater.CreateLEDDisplayPacket(Address, CanAddress.TicketBoxLed, msg, permanent);
            _CommComponent.SendPacket(p);
            p = _PacketCreater.CreateLEDDisplayPacket(Address, CanAddress.ChargeLed, msg, permanent);
            _CommComponent.SendPacket(p);
        }

        /// <summary>
        /// 在车位屏上显示车余数
        /// </summary>
        public override void ShowVacant()
        {
            //满位显示屏
            string vmsg = Park.Vacant > 0 ? Park.VacantText + Park.Vacant : Park.ParkFullText;
            Packet p = _PacketCreater.CreateLEDDisplayPacket(Address, CanAddress.VacantLed, vmsg, false);
            _CommComponent.SendPacket(p);
        }
        public override void OperateGate(GateOperationNotify notify)
        {
            Packet p = _PacketCreater.CreateGateOperatePacket(Address, notify.Action);
            _CommComponent.SendPacket(p);
        }

        /// <summary>
        /// 上传卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCard(CardInfo card, ActionType action)
        {
            return true;
        }
        /// <summary>
        /// 上传卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCard(CardInfo card, ActionType action, bool savefail)
        {
            return true;
        }
        /// <summary>
        /// 上传多张卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCards(List<CardInfo> cards, ActionType action)
        {
            return true;
        }

        /// <summary>
        /// 上传多张卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCards(List<CardInfo> cards, ActionType action, bool savefail)
        {
            return true;
        }
        /// <summary>
        /// 删除卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool DeleteCard(CardInfo card)
        {
            return false;
        }
        /// <summary>
        /// 清空控制器卡片
        /// </summary>
        /// <returns></returns>
        public override bool ClearCard()
        {
            return true;
        }
        /// <summary>
        /// 释放对象资源
        /// </summar
        public override void Dispose()
        {
            base.Dispose();
            this._CommComponent.ReportReceviced -= RecevicedPacketEventHandler;
        }
        /// <summary>
        /// 向控制板发送卡片有效
        /// </summary>
        public override void CardValid()
        {
            if (ProcessingEvent != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (AppSettings.CurrentSetting.Debug) FileLog.Log(EntranceName, "发送卡片有效指令 " + ProcessingEvent.CardID);
                    if (ProcessingEvent.IsExitEvent)
                    {
                        if (ProcessingCard.CardType.IsMonthCard)
                        {
                            MonthCardExitValid(ProcessingCard.ValidDate.Date.AddDays(1));  //过期日期比有效日期多一天
                        }
                        else if (ProcessingCard.CardType.IsPrepayCard)
                        {
                            byte carType = (byte)(CarTypeSetting.Current[ProcessingEvent.CarType].HardwareCarType);
                            PrepayCardExitValid(ProcessingEvent.LastDateTime.Value, ProcessingEvent.EventDateTime, carType, ProcessingCard.Balance);
                        }
                        else if (ProcessingCard.CardType.IsTempCard)
                        {
                            byte carType = (byte)(CarTypeSetting.Current[ProcessingEvent.CarType].HardwareCarType);
                            TempCardExitValid(ProcessingEvent.LastDateTime.Value, ProcessingEvent.EventDateTime, carType);
                        }
                    }
                    else
                    {
                        //控制控制板播放语音及显示
                        if (ProcessingCard.CardType.IsMonthCard)
                        {
                            MonthCardEnterValid(ProcessingCard.ValidDate.Date.AddDays(1)); //过期日期比有效日期多一天
                        }
                        else if (ProcessingCard.CardType.IsPrepayCard)
                        {
                            PrepayCardEnterValid(ProcessingEvent.EventDateTime, ProcessingCard.Balance);
                        }
                        else if (ProcessingCard.CardType.IsTempCard)
                        {
                            TempCardEnterValid(ProcessingEvent.EventDateTime);
                            //如果在临时卡读头上读卡，说明是临时卡取卡入场，控制板要等用户取卡后才发抬闸回复，所以这里不用重复发事件有效指令
                            if (IsTempReader(ProcessingEvent.Reader))
                            {
                                break;
                            }
                        }
                    }

                    _CardValidResponseEvent.Reset();
                    if (this.EntranceInfo.CardValidNeedResponse && !_CardValidResponseEvent.WaitOne(3000))
                    {
                        //如果启用了卡片有效需要下位机回复选项,则如果在1秒钟内没有收到回复，则再发送一次卡片有效指令。
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 向控制板发送卡片等待
        /// </summary>
        /// <param name="card"></param>
        public override void CardWait()
        {
            if (ProcessingEvent != null)
            {
                if (AppSettings.CurrentSetting.Debug) FileLog.Log(EntranceName, string.Format("发送卡片等待指令 卡号{0} {1} 应收{2} ",
                    ProcessingEvent.CardID, ProcessingEvent.CardType, ProcessingEvent.CardPaymentInfo != null ? ProcessingEvent.CardPaymentInfo.Accounts : 0));
                if (ProcessingEvent.IsExitEvent)
                {
                    if (ProcessingEvent.CardType.IsTempCard || ProcessingEvent.ChargeAsTempCard)
                    {
                        TempCardExitWait(this.ProcessingEvent);
                    }
                    else if (ProcessingEvent.CardType.IsPrepayCard)
                    {
                        PrepayCardExitWait(this.ProcessingEvent);
                    }
                    else if (ProcessingEvent.CardType.IsMonthCard)
                    {
                        MonthCardExitWait(this.ProcessingEvent);
                    }
                }
                else
                {
                    CardEnterWait(ProcessingEvent.CardType);
                }
            }
        }
        /// <summary>
        /// 向控制板发送卡片无效
        /// </summary>
        /// <param name="cardevent"></param>
        public override void CardInValid(EventInvalidType invalidType,object param)
        {
            if (AppSettings.CurrentSetting.Debug) FileLog.Log(EntranceName, "发送卡片无效" + CardInvalidDescripition.GetDescription(invalidType));
            Packet p = _PacketCreater.CreateCardInvalidPacket(Address, invalidType, param);
            _CommComponent.SendPacket(p);
        }

        /// <summary>
        /// 与硬件同步信息
        /// </summary>
        public void SyncToHardware()
        {
            Packet p1 = _PacketCreater.CreateBordSettingPacket(Address, EntranceInfo);
            _CommComponent.SendPacket(p1);
        }

        /// <summary>
        /// 向控制板获取识别到的车牌号码
        /// </summary>
        /// <returns></returns>
        public override string GetRecognizedCarPlate()
        {
            return string.Empty;
        }
        #endregion
    }
}