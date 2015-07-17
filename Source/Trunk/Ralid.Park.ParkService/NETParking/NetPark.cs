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
using Ralid.Park.PlateRecognition;
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
        private bool _WaitingCommandPause;//WaitingCommand服务暂停
        #endregion

        #region 私有方法
        private void BeginBackGroudWork()
        {
            //同步时间已交由控制器的心跳包来同步了，这里就不需要使用单独线程来同步控制器时间了
            //Thread t = new Thread(SyncTime_Thread);
            //t.IsBackground = true;
            //t.Start();

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

        //private void SyncTime_Thread()
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(5 * 60 * 1000);
        //        try
        //        {
        //            foreach (EntranceBase entrance in _Entrances)
        //            {
        //                entrance.SyncTime();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
        //        }

        //    }
        //}


        //private void ExecuteWaitingCommand_Thread()
        //{
        //    try
        //    {
        //        ClearInvalidWaitingCommand();//清除已删除的控制器的等待命令
        //        Ralid.GeneralLibrary.LOG.FileLog.Log("WaitingCommand", "开始WaitingCommand服务");
        //        while (true)
        //        {
        //            try
        //            {
        //                if (!_WaitingCommandPause && DataBaseConnectionsManager.Current.MasterConnected)
        //                {
        //                    WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
        //                    List<WaitingCommandInfo> wcs = wb.GetAllCommands().QueryObjects;
        //                    EntranceBase entrance = null;
        //                    int entranceID = -1;
        //                    foreach (var wc in wcs)
        //                    {
        //                        if (entranceID != wc.EntranceID)
        //                        {
        //                            entrance = GetEntrance(wc.EntranceID);
        //                            entranceID = wc.EntranceID;
        //                        }

        //                        if (entrance != null)
        //                        {
        //                            bool ret = false;

        //                            if (wc.Command == CommandType.DownloadAccesses)
        //                            {
        //                                ret = entrance.ApplyAccessSetting(AccessSetting.Current);
        //                            }
        //                            else if (wc.Command == CommandType.DownloadHolidays)
        //                            {
        //                                ret = entrance.ApplyHolidaySetting(HolidaySetting.Current);
        //                            }
        //                            else if (wc.Command == CommandType.DownloadTariffs)
        //                            {
        //                                ret = entrance.ApplyTariffSetting(TariffSetting.Current);
        //                            }
        //                            else if (wc.Command == CommandType.ClearCard)
        //                            {
        //                                ret = entrance.ClearCard();
        //                            }
        //                            else if (wc.Command == CommandType.DeleteCard)
        //                            {
        //                                CardInfo card = new CardInfo();
        //                                card.CardID = wc.CardID;
        //                                ret = entrance.SaveCard(card, ActionType.Delete);
        //                            }
        //                            else if (wc.Command == CommandType.AddCard || wc.Command == CommandType.UpateCard)
        //                            {
        //                                CardBll cb = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
        //                                CardInfo card = cb.GetCardByID(wc.CardID).QueryObject;
        //                                if (card != null)
        //                                {
        //                                    ret = entrance.SaveCard(card, wc.Command == CommandType.AddCard ? ActionType.Add : ActionType.Upate, false);
        //                                }
        //                                else
        //                                {
        //                                    ret = true;
        //                                }
        //                            }
        //                            if (ret)
        //                            {
        //                                CommandResult dresult = new CommandResult(ResultCode.CannotConnectServer);
        //                                if (DataBaseConnectionsManager.Current.MasterConnected)
        //                                {
        //                                    dresult = wb.Delete(wc);
        //                                }
        //                                if (AppSettings.CurrentSetting.Debug)
        //                                {
        //                                    if (dresult.Result == ResultCode.Successful)
        //                                    {
        //                                        string msg = string.Format("控制器{0}[{1}] 命令 {2} {3} 成功,删除命令 {4}", entrance.EntranceName, entrance.EntranceID, wc.Command, wc.CardID, dresult.Result == ResultCode.Successful ? "成功" : dresult.Result.ToString());
        //                                        Ralid.GeneralLibrary.LOG.FileLog.Log("WaitingCommand", msg);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
        //                Ralid.GeneralLibrary.LOG.FileLog.Log("WaitingCommand", "开始WaitingCommand服务");
        //            }
        //            Thread.Sleep(60 * 1000);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
        //        Ralid.GeneralLibrary.LOG.FileLog.Log("WaitingCommand", "开始WaitingCommand服务");
        //    }
        //}

        private void ExecuteWaitingCommand_Thread()
        {
            try
            {
                //开始前先等待10分钟，用于等待硬件设备连接状态检测完成
                Thread.Sleep(10 * 60 * 1000);
                Ralid.GeneralLibrary.LOG.FileLog.Log("WaitingCommand", "开始WaitingCommand服务");
                while (true)
                {
                    try
                    {
                        if (!_WaitingCommandPause && DataBaseConnectionsManager.Current.MasterConnected)
                        {
                            WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                            List<WaitingCommandInfo> wcs = wb.GetWaitingCommands().QueryObjects;
                            EntranceBase entrance = null;
                            int entranceID = -1;
                            foreach (var wc in wcs)
                            {
                                if (entranceID != wc.EntranceID)
                                {
                                    entrance = GetEntrance(wc.EntranceID);
                                    entranceID = wc.EntranceID;
                                }

                                if (entrance != null && entrance.Status != EntranceStatus.OffLine)
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
                                    else if (wc.Command == CommandType.DownloadKeySetting)
                                    {
                                        ret = entrance.ApplyKeySetting(KeySetting.Current);
                                    }
                                    else if (wc.Command == CommandType.ClearCard)
                                    {
                                        ret = entrance.ClearCard();
                                    }
                                    else if (wc.Command == CommandType.DeleteCard)
                                    {
                                        //由于命令执行前已删除了卡片信息，所以如果是车牌名单时，cardid保存的是车牌名单的车牌号
                                        CardInfo card = new CardInfo();
                                        card.ListType = wc.CardIDType.HasValue && wc.CardIDType.Value == 1 ? CardListType.CarPlate : CardListType.Card;
                                        if (card.IsCardList)
                                        {
                                            card.CardID = wc.CardID;
                                        }
                                        else
                                        {
                                            card.CarPlate = wc.CardID;
                                        }
                                        ret = entrance.SaveCard(card, ActionType.Delete);
                                    }
                                    else if (wc.Command == CommandType.AddCard || wc.Command == CommandType.UpateCard)
                                    {
                                        CardBll cb = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
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
                                        CommandResult dresult = new CommandResult(ResultCode.CannotConnectServer);
                                        if (DataBaseConnectionsManager.Current.MasterConnected)
                                        {
                                            dresult = wb.Delete(wc);
                                        }
                                        if (AppSettings.CurrentSetting.Debug)
                                        {
                                            if (dresult.Result == ResultCode.Successful)
                                            {
                                                string msg = string.Format("控制器{0}[{1}] 命令 {2} {3} 成功,删除命令 {4}", entrance.EntranceName, entrance.EntranceID, wc.Command, wc.CardID, dresult.Result == ResultCode.Successful ? "成功" : dresult.Result.ToString());
                                                Ralid.GeneralLibrary.LOG.FileLog.Log("WaitingCommand", msg);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        CommandResult dresult = new CommandResult(ResultCode.CannotConnectServer);
                                        if (DataBaseConnectionsManager.Current.MasterConnected)
                                        {
                                            wc.Status = WaitingCommandStatus.Fail;
                                            dresult = wb.Update(wc);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                    Thread.Sleep(5 * 60 * 1000);
                }

            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                Ralid.GeneralLibrary.LOG.FileLog.Log("WaitingCommand", "WaitingCommand服务异常终止");
            }
        }

        private void UpdateDevicePaymentRecord_Thread()
        {
            try
            {
                //开始前先等待10分钟，用于等待硬件设备连接状态检测完成
                Thread.Sleep(10 * 60 * 1000);
                Ralid.GeneralLibrary.LOG.FileLog.Log("UpdateDevicePaymentRecord", "开始UpdateDevicePaymentRecord服务");
                while (true)
                {
                    try
                    {
                        if (DataBaseConnectionsManager.Current.MasterConnected)
                        {
                            List<EntranceBase> entrances = new List<EntranceBase>();
                            entrances.AddRange(_Entrances);
                            foreach (ParkBase park in _SubParks)
                            {
                                entrances.AddRange(park.Entrances);
                            }

                            Ralid.GeneralLibrary.LOG.FileLog.Log("UpdateDevicePaymentRecord", string.Format("控制器数量: {0} ", entrances.Count));

                            foreach (NetEntrance entrance in entrances)
                            {
                                int capacity = 0;
                                int latestIndex = 0;
                                int currentIndex = entrance.EntranceInfo.PaymentEventIndex;
                                int beginIndex = entrance.EntranceInfo.PaymentEventIndex + 1;

                                string logDesc = string.Format("【{0}】 当前索引：{1} ", entrance.EntranceName, currentIndex);

                                if (entrance.GetPaymentStorageInfo(out capacity, out latestIndex))
                                {
                                    logDesc += string.Format(" 容量：{0} 最后索引：{1} ", capacity, latestIndex);

                                    int count = latestIndex - beginIndex + 1;
                                    count = count < 0 ? count + 0xFFFFFF : count;
                                    if (count > 0)
                                    {
                                        List<DevicePaymentRecord> deviceRecords = entrance.GetPaymentRecords(beginIndex, count);
                                        List<CardPaymentInfo> records = GetCardPaymentRecordsFromDeviceRecords(deviceRecords, entrance.EntranceName);
                                        if (records != null)
                                        {
                                            logDesc += string.Format(" 获取到记录：{0}  ", records.Count);

                                            CardPaymentRecordBll crb = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
                                            bool success = true;
                                            foreach (CardPaymentInfo record in records)
                                            {
                                                success = crb.InsertRecordWithCheck(record).Result == ResultCode.Successful ? success : false;
                                                if (success) currentIndex += 1;
                                            }
                                            if (currentIndex > 0xFFFFFF)//流水号最大3个字节0xFFFFFF
                                            {
                                                currentIndex -= 0xFFFFFF;
                                            }
                                            if (currentIndex != entrance.EntranceInfo.PaymentEventIndex)
                                            {
                                                entrance.EntranceInfo.PaymentEventIndex = currentIndex;
                                                EntranceBll ebll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);
                                                ebll.Update(entrance.EntranceInfo);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    logDesc += string.Format(" 获取容量信息失败 "); 
                                }

                                Ralid.GeneralLibrary.LOG.FileLog.Log("UpdateDevicePaymentRecord", logDesc);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                    //每30分钟，获取一次
                    Thread.Sleep(30 * 60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                Ralid.GeneralLibrary.LOG.FileLog.Log("UpdateDevicePaymentRecord", "UpdateDevicePaymentRecord服务异常终止");
            }
        }

        private List<CardPaymentInfo> GetCardPaymentRecordsFromDeviceRecords(List<DevicePaymentRecord> deviceRecords, string entranceName)
        {
            if (deviceRecords != null)
            {
                List<CardPaymentInfo> records = new List<CardPaymentInfo>();
                if (deviceRecords.Count > 0)
                {
                    CardInfo card = null;
                    CardInfo operatorCard = null;
                    OperatorInfo operatorInfo = null;
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
                        CardBll _CardBll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
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

                            //获取操作员卡所属的操作员
                            if (operatorInfo == null || operatorInfo.OperatorName != operatorCard.OwnerName)
                            {
                                Ralid.Park.BusinessModel.SearchCondition.OperatorSearchCondition condition = new BusinessModel.SearchCondition.OperatorSearchCondition();
                                condition.OperatorName = operatorCard.OwnerName;
                                List<OperatorInfo> operators = (new OperatorBll(AppSettings.CurrentSetting.ParkConnect)).GetOperators(condition).QueryObjects;
                                if (operators != null && operators.Count > 0)
                                {
                                    operatorInfo = operators[0];
                                }
                                else
                                {
                                    operatorInfo = null;
                                }
                            }

                            if (operatorInfo != null)
                            {
                                record.OperatorDeptID = operatorInfo.DeptID;
                            }

                            OperatorSettleBLL osbll = new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect);
                            //查找与收费时间最近的结算时间为记录结算时间，如没有找到，则该记录还没结算
                            record.SettleDateTime = osbll.GetRecentSettleDateTime(operatorCard.OwnerName, deviceRecord.PaymentDateTime);
                        }

                        record.Memo = string.Format("上传时间：{0} ；硬件记录索引：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), deviceRecord.RecordIndex);

                        records.Add(record);
                    }
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
                List<EntranceInfo> entrances = ParkBuffer.Current.GetEntrances();

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
            //收到卡片等待事件时，如果之前没有收到此卡的事件等待事件，则生成一个卡片事件
            if (entrance.ProcessingEvent == null || entrance.ProcessingEvent.CardID != report.CardID)
            {
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDetail(report.CardID, AppSettings.CurrentSetting.CurrentStandbyConnect);
                if (card != null)
                {
                    //卡片信息以硬件上传信息为准
                    //不是远距离读卡的，如果有上一次读卡时间，以硬件上传的上一次读卡时间为准
                    if (report.Reader != EntranceReader.Reader2 && report.LastDateTime != null) card.LastDateTime = report.LastDateTime.Value;

                    //modify by Jan 2014-08-19 当启用硬件识别时，以硬件上传的为准
                    if (UserSetting.Current.EnableCarPlateRecognize
                    && UserSetting.Current.HardWareCarPlateRecognize)
                    {
                        card.RegCarPlate = report.CarPlate;
                        card.LastCarPlate = report.LastCarPlate;
                    }

                    if (entrance.IsExitDevice)
                    {
                        CreateCardExitEvent(card, entrance, report.EventDateTime);
                    }
                    else
                    {
                        CreateCardEnterEvent(card, entrance, report.EventDateTime);
                    }
                }
                else
                {
                    string description = string.Format("未找到脱机卡片{0}", report.CardID);
                    //提示找不到脱机卡片
                    AlarmReport alarm = new AlarmReport(
                                         this.Park.ParkID, entrance.EntranceID, DateTime.Now,
                                         entrance.EntranceName, AlarmType.InvalidCard,
                                         description, string.Empty);
                    RaiseAlarmReporting(alarm);
                }
            }

            if (entrance.ProcessingEvent != null && entrance.ProcessingEvent.CardID == report.CardID)
            {
                //如果正在处理的事件的读头与上传事件的读头不一致，将正在处理的事件的读头设置为上传事件的读头
                if (entrance.ProcessingEvent.Reader != report.Reader) entrance.ProcessingEvent.Reader = report.Reader;
                entrance.ProcessingEvent.ComparisonResult = report.CarPlateComparisonResult;
                entrance.ProcessingEvent.EventStatus = report.EventStatus;

                ////当事件车牌对比结果为车牌对比失败，而软件又启用了软件车牌识别的，使用软件识别重新进行车牌识别对比
                ////这是因为当控制板没有安装硬件车牌识别一体机时，默认返回对比结果为对比失败
                //if (((report.EventStatus == CardEventStatus.CarPlateFail)||(report.CarPlateComparisonResult== BusinessModel.Enum.CarPlateComparisonResult.Noncontrastive))
                //    && UserSetting.Current.EnableCarPlateRecognize
                //    && UserSetting.Current.SoftWareCarPlateRecognize)
                //modify by Jan 2014-08-19 当启用车牌识别时
                if (UserSetting.Current.EnableCarPlateRecognize)
                {
                    //当启用软件识别时，忽略控制板上传的识别结果，使用软件识别重新进行车牌识别对比
                    if (UserSetting.Current.SoftWareCarPlateRecognize)
                    {
                        //add by Jan 2014-09-01 如果当前状态为车牌对比失败，对比前需设置为等待处理状态和对比成功
                        if (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)
                        {
                            entrance.ProcessingEvent.ComparisonResult = BusinessModel.Enum.CarPlateComparisonResult.Success;
                            entrance.ProcessingEvent.EventStatus = CardEventStatus.Pending;
                        }

                        CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
                    }
                    else
                    {
                        //当启用硬件识别时，以控制板上传的结果为准
                        entrance.ProcessingEvent.CarPlate = report.CarPlate;
                        entrance.ProcessingEvent.LastCarPlate = report.LastCarPlate;
                    }
                }
                if (entrance.ProcessingEvent.CardPaymentInfo != null && !report.NeedPay)
                {
                    //不需要收费时，将费用设为0，因为收到卡片车牌对比确认事件或事件有效时，控制板肯定会先判断是否已收费的，所以收到该事件时，费用应为0
                    entrance.ProcessingEvent.CardPaymentInfo.Accounts = 0;
                }

                if (report.EventStatus == CardEventStatus.CarPlateFail)
                {
                    //如果收到的刷卡事件状态为对比失败，需要确认或放行

                    if (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)
                    {
                        //上位机确认
                        RaiseCardEventReporting(entrance.ProcessingEvent);
                    }
                    else
                    {
                        //如果处理的事件状态为非对比失败，而收到的刷卡事件状态为对比失败，说明了系统启用了软件识别，并且对比成功了，可放行卡片
                        entrance.CardValid();
                    }
                }
                //这里不上传到上位机，是因为当需要收费时，上位机的收费窗口也可以进行收费处理，但收费窗口是没有卡片的，所以是写不了卡的
                //else 
                //{
                //    RaiseCardEventReporting(entrance.ProcessingEvent);
                //}
            }
        }

        protected override void OnCardPermitted(OfflineCardReadReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            
            //if (entrance.ProcessingEvent != null && entrance.ProcessingEvent.CardID == report.CardID)
            //这里不判读当前卡号事件与脱机有效事件的卡号是否一致，是因为珠海长隆停车场的硬件某个版本上传的卡号可能有错，如果是岗亭读卡收费，发送卡片有效命令，抬闸事件返回的卡号与当前事件的卡号不一致，导致该卡片没有出场纪录
            if (entrance.ProcessingEvent != null)
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
            if (info.ParkID == this.Park.ParkID)
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
            }
            else
            {
                foreach (NETPark subPark in _SubParks)
                {
                    if (info.ParkID == subPark.Park.ParkID)
                    {
                        return subPark.UpdateEntrance(info);
                    }
                }
                return false;
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
            if (this.Park.ParkID == vacantInfo.ParkID)
            {
                ParkVacant = vacantInfo.VacantPort;
                Park.VacantText = vacantInfo.VacantText;
                Park.TotalPosition = vacantInfo.CarPortUpLimit;
                Park.MinPosition = vacantInfo.CarPortDownLimit;
                Park.ParkFullText = vacantInfo.ParkFullText;
                SetParkVacantLed();

                ret = SetParkVacant(vacantInfo.CarPortUpLimit, vacantInfo.VacantPort);

                if (ret)
                {
                    ParkVacantReport report = new ParkVacantReport();
                    report.ParkID = Park.ParkID;
                    report.ParkVacant = Park.Vacant;
                    OnParkVacantReporting(report);
                }
            }
            else
            {
                foreach (NETPark subPark in _SubParks)
                {
                    if (vacantInfo.ParkID == subPark.Park.ParkID)
                    {
                        return subPark.DownloadVacantSetting(vacantInfo);
                    }
                }
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
                    if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
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
        /// <summary>
        /// WaitingCommand服务启用
        /// </summary>
        /// <param name="enable">是否启用</param>
        public override void WaitingCommandServiceEnable(bool enable)
        {
            _WaitingCommandPause = !enable;
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
