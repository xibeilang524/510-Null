using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.Hardware;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park .PlateRecognition;
using Ralid.GeneralLibrary.ExceptionHandling;

namespace Ralid.Park.ParkService.NETParking
{
    /// <summary>
    /// 表示一个在线停车场
    /// </summary>
    public class NETPark : ParkBase
    {
        #region 构造函数
        public NETPark(ParkInfo park, ParkBase parent)
            : base(park, parent)
        {
            foreach (EntranceInfo en in park.Entrances)  //初始化主控制器
            {
                if (en.IsMaster && _Master == null)
                {
                    AddEntrance(en);
                }
            }

            foreach (EntranceInfo en in park.Entrances)  //初始化所有其它的控制器,都设置成非主控，且其主控IP设置为_Master的IP
            {
                if (GetEntrance(en.EntranceID) == null)
                {
                    AddEntrance(en);
                }
            }

            if (park.SubParks != null && park.SubParks.Count > 0)
            {
                foreach (ParkInfo subPark in park.SubParks)
                {
                    NETPark sub = new NETPark(subPark, this);
                    AddSubPark(sub);
                }
            }
            BeginBackGroudWork();
        }
        #endregion

        #region 私有变量
        private NetEntrance _Master = null; //用于保存停车场的主控制器引用,主控制器也在_Entrances列表中。
        private CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
        private CardEventBll _CardEventBll = new CardEventBll(AppSettings.CurrentSetting.ParkConnect);
        private ParkBll _ParkBll = new ParkBll(AppSettings.CurrentSetting.ParkConnect);
        #endregion

        #region 私有方法
        private void BeginBackGroudWork()
        {
            Thread t = new Thread(SyncTime_Thread);
            t.IsBackground = true;
            t.Start();

            Thread t3 = new Thread(ExecuteWaitingCommand_Thread);
            t3.IsBackground = true;
            t3.Start();

            //写卡停车场时，才启动读取设备收费记录线程
            if (this.Park.IsWriteCardMode)
            {
                Thread t4 = new Thread(UpdateDevicePaymentRecord_Thread);
                t4.IsBackground = true;
                t4.Start();
            }
        }

        private void SyncTime_Thread()
        {
            while (true)
            {
                Thread.Sleep(5 * 60 * 1000);
                foreach (EntranceBase entrance in _Entrances)
                {
                    entrance.SyncTime();
                }
            }
        }

        private void ExecuteWaitingCommand_Thread()
        {
            ClearInvalidWaitingCommand();//清除已删除的控制器的等待命令
            WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
            while (true)
            {
                List<WaitingCommandInfo> wcs = wb.GetAllCommands().QueryObjects;
                EntranceBase entrance = null;
                int entranceID = -1;
                foreach (var wc in wcs)
                {
                    if (entranceID != wc.EntranceID)
                    {
                        entrance = GetEntrance(wc.EntranceID);
                        entranceID = wc.EntranceID;
                    }

                    if (entrance != null)
                    {
                        bool ret = false;

                        if (wc.Command == CommandType.DownloadAccesses)
                        {
                            ret = entrance.ApplyAccessSetting(AccessSetting.Current);
                        }
                        else if (wc.Command == CommandType.DownloadHolidays)
                        {
                            ret = entrance.ApplyHolidaySetting(HolidaySetting.Current);
                        }
                        else if (wc.Command == CommandType.DownloadTariffs)
                        {
                            ret = entrance.ApplyTariffSetting(TariffSetting.Current);
                        }
                        else if (wc.Command == CommandType.ClearCard)
                        {
                            ret = entrance.ClearCard();
                        }
                        else if (wc.Command == CommandType.DeleteCard)
                        {
                            CardInfo card = new CardInfo();
                            card.CardID = wc.CardID;
                            ret = entrance.DeleteCard(card);
                        }
                        else if (wc.Command == CommandType.AddCard || wc.Command == CommandType.UpateCard)
                        {
                            CardBll cb = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                            CardInfo card = cb.GetCardByID(wc.CardID).QueryObject;
                            if (card != null)
                            {
                                ret = entrance.SaveCard(card, wc.Command == CommandType.AddCard ? ActionType.Add : ActionType.Upate);
                            }
                            else
                            {
                                ret = true;
                            }
                        }
                        if (ret)
                        {
                            wb.Delete(wc);
                        }
                    }
                }
                Thread.Sleep(60 * 1000);
            }
        }

        private void UpdateDevicePaymentRecord_Thread()
        {
            while (true)
            {
                List<EntranceBase> entrances = new List<EntranceBase>();
                entrances.AddRange(_Entrances);
                foreach (ParkBase park in _SubParks)
                {
                    entrances.AddRange(park.Entrances);
                }
                foreach (NetEntrance entrance in entrances)
                {
                    int capacity = 0;
                    int latestIndex = 0;
                    int beginIndex = entrance.EntranceInfo.PaymentEventIndex + 1;
                    if (entrance.GetPaymentStorageInfo(out capacity, out latestIndex))
                    {
                        int count = latestIndex - beginIndex + 1;
                        count = count < 0 ? count + 0xFFFFFF : count;
                        if (count > 0)
                        {
                            List<DevicePaymentRecord> deviceRecords = entrance.GetPaymentRecords(beginIndex, count);
                            List<CardPaymentInfo> records = GetCardPaymentRecordsFromDeviceRecords(deviceRecords, entrance.EntranceName);
                            if (records != null)
                            {
                                CardPaymentRecordBll crb = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
                                bool success = true;
                                foreach (CardPaymentInfo record in records)
                                {
                                    success = crb.InsertRecordWithCheck(record).Result == ResultCode.Successful ? success : false;
                                    if (success) entrance.EntranceInfo.PaymentEventIndex++;
                                }
                                if (entrance.EntranceInfo.PaymentEventIndex > 0xFFFFFF)//流水号最大3个字节0xFFFFFF
                                {
                                    entrance.EntranceInfo.PaymentEventIndex -= 0xFFFFFF;
                                }

                                EntranceBll ebll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);
                                ebll.Update(entrance.EntranceInfo);
                            }
                        }
                    }
                }
                //每30分钟，获取一次
                Thread.Sleep(30 * 60 * 1000);
            }
        }

        private List<CardPaymentInfo> GetCardPaymentRecordsFromDeviceRecords(List<DevicePaymentRecord> deviceRecords,string entranceName)
        {
            if (deviceRecords != null)
            {
                List<CardPaymentInfo> records = new List<CardPaymentInfo>();
                CardInfo card = null;
                CardInfo operatorCard = null;
                foreach (DevicePaymentRecord deviceRecord in deviceRecords)
                {
                    CardPaymentInfo record = new CardPaymentInfo();
                    record.PaymentCode = deviceRecord.PaymentCode == DevicePaymentCode.Computer ? PaymentCode.Computer : PaymentCode.FunctionCard;
                    record.ChargeDateTime = deviceRecord.PaymentDateTime;
                    record.CardID = deviceRecord.CardID;
                    record.Accounts = deviceRecord.Amount;
                    record.Paid = deviceRecord.Amount;
                    record.OperatorCardID = deviceRecord.FunctionCardID;
                    record.StationID = entranceName;
                    //缴费卡片相关信息
                    if (card == null || card.CardID != record.CardID)
                        card = _CardBll.GetCardByID(record.CardID).QueryObject;
                    if (card != null)
                    {
                        record.OwnerName = card.OwnerName;
                        record.CarPlate = card.CarPlate;
                        record.CardType = card.CardType;
                        record.CarType = card.CarType;
                    }
                    //收费功能卡相关信息
                    if (operatorCard == null || operatorCard.CardID != record.OperatorCardID)
                    {
                        operatorCard = _CardBll.GetCardByID(record.OperatorCardID).QueryObject;
                    }
                    if (operatorCard != null)
                    {
                        record.OperatorID = operatorCard.OwnerName;
                    }

                    records.Add(record);
                }

                return records;
            }
            return null;
        }


        private void SetParkVacantLed()
        {
            if (_Master == null || _Master.Status == EntranceStatus.OffLine) return;
            foreach (NetEntrance entrance in _Entrances)
            {
                if (entrance.Status != EntranceStatus.OffLine)
                {
                    entrance.ParkDevice.SetParkVacantLedMessage(Park.VacantText);
                    entrance.ParkDevice.SetParkVacantFullLedMessage(Park.ParkFullText);
                }
            }
        }

        private bool SetParkVacant(int totalPosition, int vacant)
        {
            Hardware.ParkParams pp = new ParkParams();
            pp.ParkSpace = totalPosition;
            pp.ParkVacant = vacant;
            pp.Index = 0;
            pp.StrParkNum = ParkNum;
            if (_Master != null && _Master.Status != EntranceStatus.OffLine)
            {
                return _Master.ParkDevice.SetParkLots(pp);
            }
            return false;
        }

        private void ClearInvalidWaitingCommand()
        {
            if (ParkBuffer.Current != null)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                List<WaitingCommandInfo> wcs = wb.GetAllCommands().QueryObjects;
                List<EntranceInfo> entrances=ParkBuffer.Current.GetEntrances();

                int entranceID = 0;//当前控制器id
                bool invalidEntrance = false;//当前控制器是否无效控制器
                foreach (var wc in wcs)
                {
                    if (wc.EntranceID != entranceID)
                    {
                        EntranceInfo entrance = entrances.FirstOrDefault(e => e.EntranceID == wc.EntranceID);
                        entranceID = wc.EntranceID;
                        invalidEntrance = entrance == null;
                    }
                    if (invalidEntrance)
                    {
                        wb.Delete(wc);
                    }
                }

            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取停车场的停车场编号
        /// </summary>
        public string ParkNum
        {
            //3个字节，第一层车场编号（1byte）+第二层车场编号（1byte）+第三层车场编号（1byte)
            //目前只使用第一层车场编号和第二层车场编号，每个车场编号最大255，超过了直接返回 "000000"
            get
            {
                byte[] num = new byte[3];
                List<ParkInfo> rootParkList = (from p in ParkBuffer.Current.Parks where !p.IsNested orderby p.ParkID ascending select p).ToList();
                List<ParkInfo> oneNestedParkList = (from p in ParkBuffer.Current.Parks where p.IsNested orderby p.ParkID ascending select p).ToList();

                if (Park.IsRootPark)
                {
                    int root = rootParkList.FindIndex(p => p.ParkID == Park.ParkID);
                    root = root > -1 ? root : rootParkList.Count;
                    root += 1;
                    if (root > 0xFF) return "000000";
                    else num[0] = (byte)root;
                }
                else
                {
                    int root = rootParkList.FindIndex(p => p.ParkID == Park.ParentID.Value);
                    root = root > -1 ? root : rootParkList.Count;
                    root += 1;
                    if (root > 0xFF) return "000000";
                    else num[0] = (byte)root;
                    int one = oneNestedParkList.FindIndex(p => p.ParkID == Park.ParkID);
                    one = one > -1 ? one : oneNestedParkList.Count;
                    one += 1;
                    if (one > 0xFF) return "000000";
                    else num[1] = (byte)one;
                }

                return string.Format("{0:x2}{1:x2}{2:x2}", num[0], num[1], num[2]);
            }
        }
        /// <summary>
        /// 获取停车场的主控制通道
        /// </summary>
        public override EntranceBase Master
        {
            get { return _Master; }
        }
        #endregion

        #region 离线模式卡片进出处理
        protected override void OnCardWaiting(OfflineCardReadReport report)
        {
            //EntranceBase entrance = GetEntrance(report.EntranceID);
            //if (entrance == null) return;
            //CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDetail(report.CardID);
            //if (card != null)
            //{
            //    //离线模式下，如果硬件记录的时间大于系统记录的时间，表明卡片在软件退出时进出过停车场，所以此时以硬件时间为准
            //    if (report.LastDateTime != null && report.LastDateTime.Value.Ticks > card.LastDateTime.Ticks) card.LastDateTime = report.LastDateTime.Value;
            //    ProcessCard(entrance, report.Reader, card, report.EventDateTime);
            //}
            //else
            //{
            //    DenyCard(report.CardID, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_UnRegister, entrance,null);
            //}

            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            //收到卡片等待确认事件时，如果之前没有收到此卡的事件等待事件，则生成一个卡片事件
            if (entrance.ProcessingEvent == null || entrance.ProcessingEvent.CardID != report.CardID)
            {
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDetail(report.CardID);
                if (card != null)
                {
                    if (report.LastDateTime != null) card.LastDateTime = report.LastDateTime.Value;
                    if (entrance.IsExitDevice)
                    {
                        CreateCardExitEvent(card, entrance, report.EventDateTime);
                    }
                    else
                    {
                        CreateCardEnterEvent(card, entrance, report.EventDateTime);
                    }

                    if (entrance.ProcessingEvent != null)
                    {
                        entrance.ProcessingEvent.CarPlate = report.CarPlate;
                        entrance.ProcessingEvent.LastCarPlate = report.LastCarPlate;
                        entrance.ProcessingEvent.ComparisonResult = report.CarPlateComparisonResult;
                        entrance.ProcessingEvent.EventStatus = report.EventStatus;
                        if (entrance.ProcessingEvent.CardPaymentInfo != null)
                        {
                            //这里要将费用设为0，因为收到卡片车牌对比确认事件时，控制板肯定会先判断是否已收费的，所以收到该事件时，费用应为0
                            entrance.ProcessingEvent.CardPaymentInfo.Accounts = 0;
                        }
                    }
                }
            }
            if (entrance.ProcessingEvent != null && entrance.ProcessingEvent.CardID == report.CardID)
            {
                RaiseCardEventReporting(entrance.ProcessingEvent);
            }
        }

        protected override void OnCardPermitted(OfflineCardReadReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            //收到卡片有效事件时，如果之前没有收到此卡的事件等待事件，则生成一个卡片事件
            if (entrance.ProcessingEvent == null || entrance.ProcessingEvent.CardID != report.CardID)
            {
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDetail(report.CardID);
                if (card != null)
                {
                    if (report.LastDateTime != null) card.LastDateTime = report.LastDateTime.Value;
                    if (entrance.IsExitDevice)
                    {
                        CreateCardExitEvent(card, entrance, report.EventDateTime);
                    }
                    else
                    {
                        CreateCardEnterEvent(card, entrance, report.EventDateTime);
                    }

                    if (entrance.ProcessingEvent != null)
                    {
                        entrance.ProcessingEvent.CarPlate = report.CarPlate;
                        entrance.ProcessingEvent.LastCarPlate = report.LastCarPlate;
                        entrance.ProcessingEvent.ComparisonResult = report.CarPlateComparisonResult;
                    }
                }
            }
            if (entrance.ProcessingEvent != null && entrance.ProcessingEvent.CardID == report.CardID)
            {
                PermitCard(entrance, entrance.Operator, entrance.Station);
            }
        }
        #endregion

        #region 重写基类方法
        /// <summary>
        /// 增加一个控制板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public override bool AddEntrance(EntranceInfo info)
        {
            if (info.ParkID == this.Park.ParkID)
            {
                if (info.IsMaster)  //新增一个主控制器
                {
                    foreach (NetEntrance entrance in _Entrances)  //其它的所有控制器都要改成从机
                    {
                        if (entrance.Status != EntranceStatus.OffLine)
                        {
                            entrance.EntranceInfo.MasterIP = info.IPAddress;
                            entrance.EntranceInfo.IsMaster = false;
                            entrance.SyncToHardware();
                        }
                    }
                    info.MasterIP = info.IPAddress;
                    _Master = new NetEntrance(info, this);
                    _Entrances.Add(_Master);
                    ListenEntranceEvents(_Master);
                    return true;
                }
                else
                {
                    info.MasterIP = _Master != null ? _Master.EntranceInfo.IPAddress : string.Empty;
                    NetEntrance entrance = new NetEntrance(info, this);
                    ListenEntranceEvents(entrance);
                    _Entrances.Add(entrance);
                    return true;
                }
            }
            else
            {
                foreach (NETPark subPark in _SubParks)
                {
                    if (info.ParkID == subPark.Park.ParkID)
                    {
                        return subPark.AddEntrance(info);
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 更新一个控制板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public override bool UpdateEntrance(EntranceInfo info)
        {
            NetEntrance entrance = GetEntrance(info.EntranceID) as NetEntrance;
            if (entrance != null)
            {
                if (info.IsMaster) //更新主控制器
                {
                    entrance.EntranceInfo = info;
                    entrance.EntranceInfo.MasterIP = info.IPAddress;
                    _Master = entrance;
                    _Master.SyncToHardware();
                    foreach (NetEntrance en in _Entrances)
                    {
                        if (!en.Equals(entrance))
                        {
                            en.EntranceInfo.MasterIP = info.IPAddress;
                            if (en.Status != EntranceStatus.OffLine)
                            {
                                en.SyncToHardware();
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    if (_Master != null && _Master.EntranceInfo == info) //info由主控制器转为非主控制器,将停车场的主控制器设置为空
                    {
                        _Master = null;
                    }
                    info.MasterIP = _Master != null ? _Master.EntranceInfo.IPAddress : string.Empty;
                    entrance.EntranceInfo = info;
                    entrance.SyncToHardware();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 更新停车场车位余数
        /// </summary>
        /// <param name="entrance"></param>
        protected override void UpdateVacant(EntranceBase entrance)
        {
            //if (WorkMode == ParkWorkMode.OffLine)//离线模式从主控制板获取车位信息
            //{
            //    if (_Master != null && _Master.Status == EntranceStatus.Ok)
            //    {
            //        ParkParams pp;
            //        if (_Master.ParkDevice.GetParkLots(0, out pp))
            //        {
            //            unchecked
            //            {
            //                Park.Vacant = (short)pp.ParkVacant;
            //            }
            //        }
            //    }
            //}
            //else
            //{
                base.UpdateVacant(entrance);
            //}
        }
        /// <summary>
        /// 下载车位信息设置
        /// </summary>
        /// <param name="vacantInfo"></param>
        /// <returns></returns>
        public override bool DownloadVacantSetting(CarPortSetting vacantInfo)
        {
            bool ret = false;
            Park.Vacant = vacantInfo.VacantPort;
            Park.VacantText = vacantInfo.VacantText;
            Park.TotalPosition = vacantInfo.CarPortUpLimit;
            Park.MinPosition = vacantInfo.CarPortDownLimit;
            Park.ParkFullText = vacantInfo.ParkFullText;
            SetParkVacantLed();
            //if (Park.WorkMode == ParkWorkMode.OffLine)
            //{
                ret = SetParkVacant(vacantInfo.CarPortUpLimit, vacantInfo.VacantPort);
            //}
            //else
            //{
            //    ret = true;
            //}
            if (ret)
            {
                ParkVacantReport report = new ParkVacantReport();
                report.ParkID = Park.ParkID;
                report.ParkVacant = Park.Vacant;
                OnParkVacantReporting(report);
            }
            return ret;
        }

        public override bool EventValid(EventValidNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.EntranceID);
            if (entrance != null && entrance.ProcessingEvent != null)
            {
                entrance.Operator = notify.Operator.OperatorName;
                entrance.Station = notify.Station;
                if (entrance.ProcessingEvent.IsExitEvent && entrance.ProcessingCard.CardType.IsPrepayCard) //出口事件
                {
                    entrance.ProcessingEvent.Balance = entrance.ProcessingEvent.Balance >= notify.Paid ?
                        entrance.ProcessingEvent.Balance - notify.Paid : entrance.ProcessingEvent.Balance;
                }
                entrance.CardValid();
            }
            return true;
        }

        protected override void OnEntranceStatusReporting(EntranceStatusReport report)
        {
            if (report.Status != EntranceStatus.OffLine)
            {
                NetEntrance entrance = GetEntrance(report.EntranceID) as NetEntrance;
                if (entrance != null) //如果主控制器重新连接
                {
                    entrance.SyncToHardware();
                    if (entrance == Master && WorkMode == ParkWorkMode.OffLine)
                    {
                        Hardware.ParkParams pp;
                        if (_Master.ParkDevice.GetParkLots(0, out pp))  //如果成功，则以硬件保存的车位数为准
                        {
                            Park.Vacant = (short)pp.ParkVacant;
                            _ParkBll.UpdateVacant(Park.ParkID, Park.Vacant);
                            RaiseParkVacantReporting(new ParkVacantReport(Park.ParkID, 0, DateTime.Now, Park.ParkName, Park.Vacant));
                        }
                    }
                }
            }
            RaiseEntranceStatusReporting(report);
        }

        public override bool DownloadTariffSetting(TariffSetting tariffSetting)
        {
            TariffSetting.Current = tariffSetting;
            return true;
        }

        protected override void OnCardCaptureReporting(CardCaptureReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            RaiseCardCaptureReporting(report);

            if (entrance.ProcessingEvent != null && entrance.ProcessingEvent.CardType.IsTempCard)
            {
                if (UserSetting.Current.EnableCarPlateRecognize) CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
                //如果启用车牌识别,且出入口车牌对比失败,
                if (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)
                {
                    entrance.DisplayMsg(CardInvalidDescripition.GetDescription(Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_CarPlateWrong), false);
                    //(entrance as NetEntrance).ParkDevice.Speak(DeviceVoice.eAUDIO_CPBF);
                    //(entrance as NetEntrance).ParkDevice.Speak(DeviceVoice.eAUDIO_ZZQRQSH);
                    entrance.CardWait();
                    RaiseCardEventReporting(entrance.ProcessingEvent);
                    return;
                }
                else
                {
                    entrance.CardValid();
                    if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station);
                }
            }
        }

        public override bool SaveCard(CardInfo card, ActionType action)
        {
            bool result = true;
            foreach (EntranceBase entrance in _Entrances)
            {
                result = entrance.SaveCard(card, action) ? result : false;
            }
            return result;
        }

        public override bool DeleteCard(CardInfo card)
        {
            bool result = true;
            foreach (EntranceBase entrance in _Entrances)
            {
                result = entrance.DeleteCard(card) ? result : false;
            }
            return result;
        }

        public override bool ClearCard()
        {
            bool result = true;
            foreach (EntranceBase entrance in _Entrances)
            {
                result = entrance.ClearCard() ? result : false;
            }
            return result;
        }

        #region 写卡模式

        //public override bool DownloadAccessSetting(AccessSetting ascLevel)
        //{
        //    bool ret = base.DownloadAccessSetting(ascLevel);
        //    if (ret && WorkMode == WorkMode.OffLine)//写卡模式
        //    {
        //        foreach (EntranceBase entrance in _Entrances)
        //        {
        //            if (!entrance.ApplyAccessSetting(ascLevel))
        //            {
        //                ret = false;
        //                Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("控制板 {0} 设置通道权限失败！", entrance.EntranceName));
        //            }
        //        }
        //    }
        //    return ret;
        //}

        //public override bool DownloadHolidaySetting(HolidaySetting holidaySetting)
        //{
        //    if (WorkMode == WorkMode.OffLine)//写卡模式
        //    {
        //        bool ret = true;
        //        HolidaySetting.Current = holidaySetting;
        //        //下载到所有控制器
        //        foreach (EntranceBase entrance in _Entrances)
        //        {
        //            if (!entrance.ApplyHolidaySetting(holidaySetting))
        //            {
        //                ret = false;
        //                Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("控制板 {0} 设置节假日失败！", entrance.EntranceName));
        //            }
        //        }
        //        return ret;
        //    }
        //    else
        //    {
        //        return base.DownloadHolidaySetting(holidaySetting);
        //    }
        //}

        //public override bool DownloadTariffSetting(TariffSetting tariffSetting)
        //{
        //    if (WorkMode == WorkMode.OffLine)//写卡模式
        //    {
        //        bool ret = true;
        //        TariffSetting.Current = tariffSetting;
        //        //下载到所有控制器
        //        foreach (EntranceBase entrance in _Entrances)
        //        {
        //            if (!entrance.ApplyTariffSetting(tariffSetting))
        //            {
        //                ret = false;
        //                Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("控制板 {0} 设置费率失败！",  entrance.EntranceName));
        //            }
        //        }
        //        return ret;
        //    }
        //    else
        //    {
        //        return base.DownloadTariffSetting(tariffSetting);
        //    }
        //}

        //public override bool SaveCard(CardInfo card, ActionType action)
        //{
        //    if (WorkMode == WorkMode.OffLine)//写卡模式
        //    {
        //        bool ret = true;
        //        foreach (EntranceBase entrance in _Entrances)
        //        {
        //            if (!entrance.SaveCard(card, action))
        //            {
        //                ret = false;
        //                Ralid.GeneralLibrary.LOG.FileLog.Log("系统", string.Format("上传卡片 {0} 到控制板 {1} 失败！",card.CardID,entrance.EntranceName));
        //            }
        //        }
        //        return ret;
        //    }
        //    else
        //    {
        //        return base.SaveCard(card, action);
        //    }
        //}
        #endregion
        #endregion
    }
}
