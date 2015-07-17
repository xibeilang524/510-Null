using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.ServiceModel;
using System.Reflection;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.GeneralLibrary.LOG;
using Ralid.Park.PlateRecognition;
using Ralid.Park.OutdoorLEDSetting;
using Ralid.Park.BusinessModel.Factory;

namespace Ralid.Park.ParkService
{
    /// <summary>
    /// 表示在线停车场的通信接口
    /// </summary>
    public abstract class ParkBase
    {
        #region 构造函数
        public ParkBase(ParkInfo park, ParkBase parent)
        {
            _Park = park;
            Parent = parent;
        }
        #endregion 构造方法

        #region 成员变量
        private ParkInfo _Park;
        protected List<EntranceBase> _Entrances = new List<EntranceBase>();
        protected List<ParkBase> _SubParks = new List<ParkBase>();
        private object _ParkVacantLocker = new object(); //更新车位数时要加锁,因为停车场每个通道的事件都有一个处理线程,所以这些线程在更新车位数时要同步
        /// <summary>
        /// 当前轮换编号
        /// </summary>
        protected int _CurrentRotationNumber = 1;
        private object _RotationNumberLock = new object();//轮换编号锁
        #endregion 成员变量

        #region 私有方法
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
        #endregion

        #region 处理子车场事件
        private void sub_AlarmReporting(object sender, AlarmReport report)
        {
            RaiseAlarmReporting(report);
        }
        private void sub_DeviceResetReporting(object sender, DeviceResetReport report)
        {
            RaiseDeviceResetReporting(report);
        }

        private void sub_EntranceRemainTempCardReporting(object sender, EntranceRemainTempCardReport report)
        {
            RaiseEntranceRemainTempCardReporting(report);
        }

        private void sub_CardCaptureReporting(object sender, CardCaptureReport report)
        {
            RaiseCardCaptureReporting(report);
        }

        private void sub_ParkVacantReporting(object sender, ParkVacantReport report)
        {
            RaiseParkVacantReporting(report);
        }

        private void sub_CarSenseReporting(object sender, CarSenseReport report)
        {
            RaiseCarSenseReporting(report);
        }

        private void sub_CardInvalidReporting(object sender, CardInvalidEventReport report)
        {
            RaiseCardInvalidReporting(report);
        }

        private void sub_EntranceStatusReporting(object sender, EntranceStatusReport report)
        {
            RaiseEntranceStatusReporting(report);
        }

        private void sub_CardEventReporting(object sender, CardEventReport report)
        {
            RaiseCardEventReporting(report);
        }
        #endregion

        #region 保护方法
        protected void AddSubPark(ParkBase sub)
        {
            _SubParks.Add(sub);
            sub.CardEventReporting += new ReportingHandler<CardEventReport>(sub_CardEventReporting);
            sub.EntranceStatusReporting += new ReportingHandler<EntranceStatusReport>(sub_EntranceStatusReporting);
            sub.CardInvalidReporting += new ReportingHandler<CardInvalidEventReport>(sub_CardInvalidReporting);
            sub.CarSenseReporting += new ReportingHandler<CarSenseReport>(sub_CarSenseReporting);
            sub.ParkVacantReporting += new ReportingHandler<ParkVacantReport>(sub_ParkVacantReporting);
            sub.CardCaptureReporting += new ReportingHandler<CardCaptureReport>(sub_CardCaptureReporting);
            sub.EntranceRemainTempCardReporting += new ReportingHandler<EntranceRemainTempCardReport>(sub_EntranceRemainTempCardReporting);
            sub.DeviceResetReporting += new ReportingHandler<DeviceResetReport>(sub_DeviceResetReporting);
            sub.AlarmReporting += new ReportingHandler<AlarmReport>(sub_AlarmReporting);
        }

        protected EntranceBase GetEntrance(int entranceID)
        {
            EntranceBase entrance = _Entrances.SingleOrDefault(en => en.EntranceID == entranceID);
            if (entrance == null)
            {
                foreach (ParkBase p in _SubParks)
                {
                    entrance = p.GetEntrance(entranceID);
                    if (entrance != null) break;
                }
            }
            return entrance;
        }

        protected void CreateCardEnterEvent(CardInfo card, EntranceBase entrance, DateTime eventDt)
        {
            //事件明细
            CardEventReport cardEvent = CardEventReport.CreateEnterEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, entrance.Park.WorkMode, eventDt);
            entrance.ProcessingCard = card;
            entrance.ProcessingEvent = cardEvent;
        }

        protected void CreateCardExitEvent(CardInfo card, EntranceBase entrance, DateTime eventDt)
        {
            CardEventReport cardEvent = CardEventReport.CreateExitEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, entrance.Park.WorkMode, card.CarType, TariffSetting.Current, eventDt);
            entrance.ProcessingEvent = cardEvent;
            entrance.ProcessingCard = card;
        }

        protected void CarPlateHandler(EntranceBase entrance, CardEventReport cardEvent, CardInfo card)
        {
            if (UserSetting.Current.SoftWareCarPlateRecognize)
            {
                PlateRecognitionResult ret = CarPalteRecognize(entrance.ParkID, entrance.EntranceID);
                cardEvent.CarPlate = ret.CarPlate;
                card.RegCarPlate = ret.CarPlate;
            }
            else if (UserSetting.Current.HardWareCarPlateRecognize)
            {
                if (cardEvent.Reader == EntranceReader.DeskTopReader)//硬件识别时，如果在桌面发卡机读卡，向控制板获取识别到的车牌号码
                {
                    string carplate = entrance.GetRecognizedCarPlate();
                    cardEvent.CarPlate = carplate;
                    card.RegCarPlate = carplate;
                }
            }
            if (cardEvent.IsExitEvent)  //出场事件
            {
                if (card.IsTempCard == false)
                {
                    if (UserSetting.Current.FixCardExitWaitWhenCarPlateFail)
                    {
                        if (!CarPlateComparer.CarPlateComparison(cardEvent.CarPlate, cardEvent.LastCarPlate, UserSetting.Current.MaxCarPlateErrorChar))
                            cardEvent.EventStatus = CardEventStatus.CarPlateFail;
                    }
                    else if (UserSetting.Current.FixCardEnterAndExitWaitWhenCarPlateFail)
                    {
                        if (!CarPlateComparer.CarPlateComparison(cardEvent.CarPlate, card.CarPlate, UserSetting.Current.MaxCarPlateErrorChar))
                            cardEvent.EventStatus = CardEventStatus.CarPlateFail;
                    }
                }
                else
                {
                    if (UserSetting.Current.TempCardExitWaitWhenCarPlateFail && !CarPlateComparer.CarPlateComparison(cardEvent.CarPlate, cardEvent.LastCarPlate, UserSetting.Current.MaxCarPlateErrorChar))
                        cardEvent.EventStatus = CardEventStatus.CarPlateFail;
                }
            }
            else  //入场事件
            {
                if (card.IsTempCard == false)
                {
                    if (UserSetting.Current.FixCardEnterAndExitWaitWhenCarPlateFail)
                    {
                        if (!CarPlateComparer.CarPlateComparison(cardEvent.CarPlate, card.CarPlate, UserSetting.Current.MaxCarPlateErrorChar))
                            cardEvent.EventStatus = CardEventStatus.CarPlateFail;
                    }
                }
            }
            if (cardEvent.EventStatus == CardEventStatus.CarPlateFail)
                cardEvent.ComparisonResult = CarPlateComparisonResult.Fail;
        }

        protected virtual void UpdateVacant(EntranceBase entrance)
        {
            if (entrance.ProcessingCard != null)
            {
                if (entrance.ProcessingCard.WithCount || !entrance.EntranceInfo.NoParkingCount)  //如果卡片要进行车位计数或者通道启用车位计数
                {
                    if (entrance.ProcessingEvent != null)
                    {
                        lock (_ParkVacantLocker)
                        {
                            if (entrance.ProcessingEvent.IsExitEvent)//出口控制器
                            {
                                if (Park.Vacant < Park.TotalPosition) Park.Vacant++;
                            }
                            else
                            {
                                //modify by Jan 允许空车位数小于最小车位数，如果不允许，当车位满时允许入场后再出场，车位数就会不对
                                Park.Vacant--;
                            }
                        }
                        if (Park.DeviceType == EntranceDeviceType.CANEntrance)
                        {
                            //网络型的车余数已通过心跳包发送到主控制板，主控制板会自动更新硬件的车余数，不需要发送显示车余数命令
                            foreach (EntranceBase en in _Entrances)
                            {
                                if (!en.IsExitDevice && en.EntranceInfo.EnableParkvacantLed) en.ShowVacant();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 拒绝卡片出入场
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="carPlate">车牌号</param>
        /// <param name="cardType">卡类型（为空时使用硬件卡类型）</param>
        /// <param name="hcardType">硬件卡类型</param>
        /// <param name="reader">读头</param>
        /// <param name="invalidReason">拒绝原因</param>
        /// <param name="entrance">控制版</param>
        /// <param name="param">无效参数</param>
        protected void DenyCard(string cardID, string carPlate, CardType cardType, byte hcardType, EntranceReader reader, EventInvalidType invalidReason, EntranceBase entrance, object param)
        {
            entrance.CardInValid(cardID, cardType, hcardType, reader, invalidReason, param);
            CardInvalidEventReport report = new CardInvalidEventReport();
            report.ParkID = _Park.ParkID;
            report.EntranceID = entrance.EntranceID;
            report.SourceName = entrance.EntranceName;
            report.CardID = cardID;
            report.CarPlate = carPlate;
            report.InvalidType = invalidReason;
            report.EventDateTime = DateTime.Now;
            //更新通道状态参数
            entrance.ProcessingEvent = null;
            entrance.Operator = string.Empty;
            entrance.Station = string.Empty;

            OnCardInvalidReporting(report);
        }
        /// <summary>
        /// 允许卡片进出
        /// </summary>
        /// <param name="entrance"></param>
        /// <param name="operatorID"></param>
        /// <param name="stationID"></param>
        protected void PermitCard(EntranceBase entrance, string operatorID, string stationID)
        {
            if (entrance.ProcessingEvent != null)
            {
                CardEventReport report = entrance.ProcessingEvent.Clone(); //克隆一分事件
                report.EventStatus = CardEventStatus.Valid;
                report.OperatorID = operatorID;
                report.StationID = stationID;
                report.UpdateFlag = true;//先标识为已上传

                string master = DataBaseConnectionsManager.Current.MasterConnected ? AppSettings.CurrentSetting.CurrentMasterConnect : string.Empty;
                string standby = DataBaseConnectionsManager.Current.StandbyConnected ? AppSettings.CurrentSetting.CurrentStandbyConnect : string.Empty;

                CardBll cbll = new CardBll(master) ;
                CardBll standbycbll = new CardBll(standby); ;
                CommandResult ret = new CommandResult(ResultCode.Fail, string.Empty);
                if (!entrance.EntranceInfo.UseAsAcs) //FUCK useasacs
                {
                    if (!Park.IsNested)
                    {
                        if (!string.IsNullOrEmpty(master)) ret = cbll.SaveCardAndEvent(entrance.ProcessingCard, report);
                        if (ret.Result != ResultCode.Successful) report.UpdateFlag = false;//标识为未上传
                        if (!string.IsNullOrEmpty(standby))
                        {
                            CommandResult standbyret = standbycbll.SaveCardAndEvent(entrance.ProcessingCard, report);//写入备份数据库
                            if (ret.Result != ResultCode.Successful) ret = standbyret;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(master)) ret = cbll.SaveCardAndNestedEvent(entrance.ProcessingCard, report);
                        if (ret.Result != ResultCode.Successful) report.UpdateFlag = false;//标识为未上传
                        if (!string.IsNullOrEmpty(standby))
                        {
                            CommandResult standbyret = standbycbll.SaveCardAndNestedEvent(entrance.ProcessingCard, report);//写入备份数据库
                            if (ret.Result != ResultCode.Successful) ret = standbyret;
                        }
                    }
                }
                else
                {
                    ret = (new CardEventBll(master)).Insert(new CardEventRecord(report));
                    if (!string.IsNullOrEmpty(standby))
                    {
                        CommandResult standbyret = (new CardEventBll(standby)).Insert(new CardEventRecord(report));
                        if (ret.Result != ResultCode.Successful) ret = standbyret;
                    }
                }
                if (ret.Result == ResultCode.Successful)
                {
                    RaiseCardEventReporting(report);
                    entrance.Operator = null;
                    entrance.Station = null;
                    //更新车位信息
                    UpdateVacant(entrance);
                    ParkVacantReport pReport = new ParkVacantReport(entrance.ParkID, entrance.EntranceID, DateTime.Now, entrance.EntranceName, _Park.Vacant);
                    OnParkVacantReporting(pReport);
                }

                if (UserSetting.Current.EnableOutdoorLed && entrance.ProcessingCard.WithCount) //如果启用了澳大户外屏
                {
                    ParkOutDoorLedManager pdm = ParkOutDoorLedSettingsStorage.Get(report.ParkID);
                    if (pdm != null && pdm.ProcessCardEvent(report))
                    {
                        ParkOutDoorLedSettingsStorage.Save(pdm);
                    }
                }
                entrance.ProcessingEvent = null;
            }
        }
        #endregion

        #region 处理控制板事件
        protected void ListenEntranceEvents(EntranceBase entrance)
        {
            entrance.AlarmReporting -= new ReportingHandler<AlarmReport>(entrance_AlarmReporting);
            entrance.AlarmReporting += new ReportingHandler<AlarmReport>(entrance_AlarmReporting);

            entrance.CaptureACardReporting -= new ReportingHandler<CardCaptureReport>(entrance_CardCaptureReporting);
            entrance.CaptureACardReporting += new ReportingHandler<CardCaptureReport>(entrance_CardCaptureReporting);

            entrance.CardReadingReporting -= new ReportingHandler<CardReadReport>(entrance_CardReadingReporting);
            entrance.CardReadingReporting += new ReportingHandler<CardReadReport>(entrance_CardReadingReporting);

            entrance.CarSenseReporting -= new ReportingHandler<CarSenseReport>(entrance_CarSenseReporting);
            entrance.CarSenseReporting += new ReportingHandler<CarSenseReport>(entrance_CarSenseReporting);

            entrance.DeviceResetReporting -= new ReportingHandler<DeviceResetReport>(entrance_DeviceResetReporting);
            entrance.DeviceResetReporting += new ReportingHandler<DeviceResetReport>(entrance_DeviceResetReporting);

            entrance.EntranceRemainTempCardReporting -= new ReportingHandler<EntranceRemainTempCardReport>(entrance_EntranceRemainTempCardReporting);
            entrance.EntranceRemainTempCardReporting += new ReportingHandler<EntranceRemainTempCardReport>(entrance_EntranceRemainTempCardReporting);

            entrance.StatusChangedReporting -= new ReportingHandler<EntranceStatusReport>(entrance_EntranceStatusReporting);
            entrance.StatusChangedReporting += new ReportingHandler<EntranceStatusReport>(entrance_EntranceStatusReporting);

            entrance.CardPermittedReporting -= new ReportingHandler<OfflineCardReadReport>(entrance_CardPermittedReporting);
            entrance.CardPermittedReporting += new ReportingHandler<OfflineCardReadReport>(entrance_CardPermittedReporting);

            entrance.CardWaitReporting -= new ReportingHandler<OfflineCardReadReport>(entrance_CardWaitReporting);
            entrance.CardWaitReporting += new ReportingHandler<OfflineCardReadReport>(entrance_CardWaitReporting);

            entrance.CardDeniedReporting -= new ReportingHandler<CardInvalidEventReport>(entrance_CardInvalidReporting);
            entrance.CardDeniedReporting += new ReportingHandler<CardInvalidEventReport>(entrance_CardInvalidReporting);

            entrance.ParkVacantReporting -= new ReportingHandler<ParkVacantReport>(entrance_ParkVacantReporting);
            entrance.ParkVacantReporting += new ReportingHandler<ParkVacantReport>(entrance_ParkVacantReporting);

            entrance.CommandEchoReporting -= new ReportingHandler<CommandEchoReport>(entrance_CommandEchoReporting);
            entrance.CommandEchoReporting += new ReportingHandler<CommandEchoReport>(entrance_CommandEchoReporting);
        }

        private void entrance_ParkVacantReporting(object sender, ParkVacantReport report)
        {
            OnParkVacantReporting(report);
        }

        private void entrance_EntranceStatusReporting(object sender, EntranceStatusReport report)
        {
            OnEntranceStatusReporting(report);
        }

        private void entrance_EntranceRemainTempCardReporting(object sender, EntranceRemainTempCardReport report)
        {
            OnEntranceRemainTempCardReporting(report);
        }

        private void entrance_DeviceResetReporting(object sender, DeviceResetReport report)
        {
            OnDeviceResetReporting(report);
        }

        private void entrance_CarSenseReporting(object sender, CarSenseReport report)
        {
            OnCarSenseReporting(report);
        }

        private void entrance_CardInvalidReporting(object sender, CardInvalidEventReport report)
        {
            OnCardInvalidReporting(report);
        }

        private void entrance_CardCaptureReporting(object sender, CardCaptureReport report)
        {
            OnCardCaptureReporting(report);
        }

        private void entrance_CardReadingReporting(object sender, CardReadReport report)
        {
            OnCardReading(report);
        }

        private void entrance_CardWaitReporting(object sender, OfflineCardReadReport report)
        {
            OnCardWaiting(report);
        }

        private void entrance_CardPermittedReporting(object sender, OfflineCardReadReport report)
        {
            OnCardPermitted(report);
        }

        private void entrance_AlarmReporting(object sender, AlarmReport report)
        {
            OnAlarmReporting(report);
        }

        private void entrance_CommandEchoReporting(object sender, CommandEchoReport report)
        {
            OnCommandEchoReporting(report);
        }
        #endregion

        #region 在线模式卡片进出处理
        /// <summary>
        /// 处理读卡
        /// </summary>
        /// <param name="report"></param>
        private void ProcessCardRead(CardReadReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;

            CardBll cbl = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);;
            CardInfo card = null;
            CardInfo info = CardDateResolver.Instance.GetCardInfoFromData(report.CardID, report.ParkingData);
            
            if (DataBaseConnectionsManager.Current.MasterConnected || DataBaseConnectionsManager.Current.StandbyConnected)
            {
                //根据主数据库和备用数据库的连接状态选择数据库
                card = cbl.GetCardDetail(report.CardID, AppSettings.CurrentSetting.CurrentStandbyConnect);
            }
            else if (info != null && !info.OnlineHandleWhenOfflineMode)//与主数据库和备用数据库断开时，如果是脱机处理的卡片，卡片信息从卡片的停车场数据中获取
            {
                card = info.Clone();
            }

            if (card != null)
            {
                //写卡模式时，并且不是按在线模式处理
                if (Park.IsWriteCardMode && !card.OnlineHandleWhenOfflineMode)
                {
                    //桌面读卡时需读取卡片中停车场扇区的数据
                    if (report.Reader == EntranceReader.DeskTopReader)
                    {
                        //卡片按序列号方式处理，卡片有效即可放行
                        if (IsSerialNumberHandle(card, entrance, report)) return;

                        //如果是卡片名单，需要判断是否读到时间
                        if (card.IsCardList)
                        {
                            if (info != null)//已读到卡片数据为准，否则返回
                            {
                                //复制缴费数据
                                CardDateResolver.Instance.CopyPaidDataToCard(card, info);
                            }
                            else
                            {
                                DenyCard(report.CardID, report.CarPlate, null, report.CardType, report.Reader, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_Invalid, entrance, null);
                                return;
                            }
                        }
                    }
                    else
                    {
                        #region 票箱读到脱机处理的卡片的处理
                        //如果是票箱读到的，先判断卡片是否可用
                        //因为注销、禁用、挂失后，控制板会没有卡片的名单，控制板会将读到的卡片上传上位机处理
                        //如果不判断卡片是否可用，不能使用的卡片刷卡后会一直提示请重新读卡
                        if (card.Status == CardStatus.Recycled) //卡片已注销
                        {
                            DenyCard(card.CardID, card.CarPlate, card.CardType, 0, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Recycled, entrance, null);
                            return;
                        }
                        if (card.Status == CardStatus.Disabled)  //卡片已锁定
                        {
                            DenyCard(card.CardID, card.CarPlate, card.CardType, 0, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Lock, entrance, null);
                            return ;
                        }
                        if (card.Status == CardStatus.Loss)   //卡片已挂失
                        {
                            DenyCard(card.CardID, card.CarPlate, card.CardType, 0, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Loss, entrance, null);
                            return ;
                        }

                        //如果是卡片名单，卡片可用时，再提示重新读卡
                        //由于车牌名单不能写卡，所以即使车牌名单是脱机处理的，车辆也运行向下处理
                        if (card.IsCardList)
                        {
                            //卡片可用时，再提示重新读卡
                            DenyCard(report.CardID, report.CarPlate, card.CardType, 0, report.Reader, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_ReadCard, entrance, null);
                            return;
                        }
                        #endregion
                    }
                }
                card.RegCarPlate = report.CarPlate;//识别到的车牌
                //硬件上传的读卡事件中，脱机模式时，脱机处理的卡片，最近一次识别到的车牌按硬件上传的最近一次识别到的车牌为准
                if (report.Reader != EntranceReader.DeskTopReader
                    && WorkMode == ParkWorkMode.OffLine
                    && !card.OnlineHandleWhenOfflineMode)
                {
                    card.LastCarPlate = report.LastCarPlate;
                }
                if (ValidateCard(card, entrance, report) == false) return;  //如果卡片验证失败,则返回
                ProcessCard(entrance, report.Reader, card, report.EventDateTime);
            }
            else
            {
                if (!report.IsCarPlateEventHandle)
                {
                    DenyCard(report.CardID, report.CarPlate, null, report.CardType, report.Reader, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_UnRegister, entrance, null);
                }
                else
                {
                    DenyCard(report.CardID, report.CarPlate, null, report.CardType, report.Reader, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_NotOnTheList, entrance, null);
                }
            }
        }
        //服务器处理卡片
        protected void ProcessCard(EntranceBase entrance, EntranceReader reader, CardInfo card, DateTime eventDateTime)
        {
            if (!entrance.EntranceInfo.UseAsAcs)
            {
                if (!Park.IsNested)
                {
                    if (entrance.IsExitDevice == false && card.CardType.IsMonthCard)
                    {
                        MonthCardEnteringHander(card, reader, eventDateTime, entrance);
                    }
                    else if (entrance.IsExitDevice == false && card.CardType.IsPrepayCard)
                    {
                        PrepayCardEnteringHandler(card, reader, eventDateTime, entrance);
                    }
                    else if (entrance.IsExitDevice == false && card.CardType.IsTempCard)
                    {
                        TempCardEnteringHandler(card, reader, eventDateTime, entrance);
                    }
                    else if (entrance.IsExitDevice && card.CardType.IsMonthCard)
                    {
                        MonthCardExitingHandler(card, reader, eventDateTime, entrance);
                    }
                    else if (entrance.IsExitDevice && card.CardType.IsPrepayCard)
                    {
                        PrepayCardExitingHandler(card, reader, eventDateTime, entrance);
                    }
                    else if (entrance.IsExitDevice && card.CardType.IsTempCard)
                    {
                        TempCardExitingHandler(card, reader, eventDateTime, entrance);
                    }
                }
                else
                {
                    NestedCardEventHandler(card, reader, eventDateTime, entrance);
                }
            }
            else
            {
                ACSCardEventHandler(card, reader, eventDateTime, entrance);
            }
        }
        /// <summary>
        /// 是否按序列号方式处理，相当于在韦根读卡器刷卡，不判断进出场状态，直接放行
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="entranceInfo"></param>
        /// <returns></returns>
        private bool IsSerialNumberHandle(CardInfo card, EntranceBase entrance, CardReadReport report)
        {
            EntranceCardTypeProperty property = entrance.EntranceInfo.GetCardTypeProperty(card.CardType);
            if ((property & EntranceCardTypeProperty.WriteCardHandle) == 0)
            {
                if ((property & EntranceCardTypeProperty.EnabledWiegandReader) == EntranceCardTypeProperty.EnabledWiegandReader)
                {
                    CardInfo serialCard = card.Clone();
                    //由于在韦根读卡器刷卡，不判断进出场状态，相当于可以重复经出场，并且不需要进行写卡了
                    //所以这里使用卡片的复制体，并将重复进出场设为有效，进行有效验证，
                    serialCard.CanRepeatIn = true;
                    serialCard.CanRepeatOut = true;
                    if (ValidateCard(serialCard, entrance, report))
                    {
                        if (entrance.IsExitDevice) CreateCardExitEvent(card, entrance, report.EventDateTime);
                        else CreateCardEnterEvent(card, entrance, report.EventDateTime);
                        entrance.ProcessingEvent.Reader = report.Reader;
                        //由于不需要进行写卡了，所以将读卡事件设置为按脱机模式时按在线模式处理
                        entrance.ProcessingEvent.OnlineHandleWhenOfflineMode = true;
                        if (entrance.EntranceInfo.MonthCardWaitWhenOut) //月卡出场需确认
                        {
                            entrance.CardWait();
                            RaiseCardEventReporting(entrance.ProcessingEvent);
                            return true;
                        }

                        //卡片有效即可放行
                        entrance.CardValid();
                        if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
                    }
                }
                else
                {
                    DenyCard(report.CardID, report.CarPlate, card.CardType, report.CardType, report.Reader, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_Type, entrance, null);
                }

                return true;
            }
            return false;
        }
        //检验卡片有效性
        private bool ValidateCard(CardInfo card, EntranceBase entrance, CardReadReport report)
        {
            if (card.Status == CardStatus.Recycled) //卡片已注销
            {
                DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Recycled, entrance, null);
                return false;
            }
            if (card.Status == CardStatus.Disabled)  //卡片已锁定
            {
                DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Lock, entrance, null);
                return false;
            }
            if (card.Status == CardStatus.Loss)   //卡片已挂失
            {
                DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Loss, entrance, null);
                return false;
            }
            if (card.ActivationDate > DateTime.Now) //卡片未到生效期
            {
                DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_NotActive, entrance, null);
                return false;
            }
            if (card.ValidDate < DateTime.Today && card.CardType != Ralid.Park.BusinessModel.Enum.CardType.TempCard && !card.EnableWhenExpired && entrance.EntranceInfo.ForbidWhenCardExpired)//卡片已过期
            {
                DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, card.IsCardList ? BusinessModel.Enum.EventInvalidType.INV_OverDate : BusinessModel.Enum.EventInvalidType.INV_Expired, entrance, card.ValidDate);
                return false;
            }
            if (!entrance.EntranceInfo.IsExitDevice && !card.CanEnterWhenFull && entrance.EntranceInfo.ForbidWhenFull) //如果是入场并且卡片不能在满位时入场,则判断车位数
            {
                if (UserSetting.Current.EnableOutdoorLed)
                {
                    ParkOutDoorLedManager pm = ParkOutDoorLedSettingsStorage.Get(_Park.ParkID);
                    if (pm != null)
                    {
                        int? vacant = pm.GetVacant(card.CardType, entrance.EntranceID);
                        if (vacant != null && vacant.Value == 0)
                        {
                            DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_ParkFull, entrance, null);
                            return false;
                        }
                    }
                    else if (Park.Vacant <= Park.MinPosition) //车位已满位入场
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_ParkFull, entrance, null);
                        return false;
                    }
                }
                else if (Park.Vacant <= Park.MinPosition) //车位已满位入场
                {
                    DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_ParkFull, entrance, null);
                    return false;
                }
            }
            if (!card.HolidayEnabled && !entrance.IsExitDevice && HolidaySetting.Current.IsHoliday(DateTime.Now)) //节假日不允许进入
            {
                DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_HolidayDisabled, entrance, null);
                return false;
            }
            if (!entrance.EntranceInfo.UseAsAcs)  //系统为澳大增加一种是否以门禁模式运行
            {
                if (!Park.IsNested)  //非内嵌车场
                {
                    if (!entrance.EntranceInfo.IsExitDevice && card.IsInPark && !card.CanRepeatIn) //重复入场
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, card.IsCardList ? BusinessModel.Enum.EventInvalidType.INV_HaveIn : BusinessModel.Enum.EventInvalidType.INV_CarIsIn, entrance, null);
                        return false;
                    }
                    if (entrance.EntranceInfo.IsExitDevice && !card.IsInPark && !card.CanRepeatOut)//重复出场
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, card.IsCardList ? BusinessModel.Enum.EventInvalidType.INV_StillOut : BusinessModel.Enum.EventInvalidType.INV_CarIsOut, entrance, null);
                        return false;
                    }
                    if (!card.CardType.IsTempCard&& entrance.IsTempReader(report.Reader) && (this is NETParking.NETPark))  //网络型停车场临时卡读头上如果读到非临时卡，则表明卡片无效，直接收回
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Type, entrance, null);
                        return false;
                    }
                    //if (card.EnableHotelApp && !card.HotelCheckOut && card.IsInFreeTime(report.EventDateTime) && entrance.IsExitDevice && entrance.IsTempReader(report.Reader) && (this is NETParking.NETPark))
                    //{
                    //    //临时卡时，如果启用了酒店应用并且未退房的，并且处于免费时间内的，不允许在网络型停车场的出口控制板临时卡读头上读卡
                    //    DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_Type, entrance, null);
                    //    return false;
                    //}
                }
                else
                {
                    if (card.IsInPark) //进出内车场时卡片要先进入外车场
                    {
                        if (!entrance.EntranceInfo.IsExitDevice && card.IsInNestedPark && !card.CanRepeatIn) //重复入场
                        {
                            DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, card.IsCardList ? BusinessModel.Enum.EventInvalidType.INV_HaveIn : BusinessModel.Enum.EventInvalidType.INV_CarIsIn, entrance, null);
                            return false;
                        }
                        if (entrance.EntranceInfo.IsExitDevice && !card.IsInNestedPark && !card.CanRepeatOut)//重复出场
                        {
                            DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, card.IsCardList ? BusinessModel.Enum.EventInvalidType.INV_StillOut : BusinessModel.Enum.EventInvalidType.INV_CarIsOut, entrance, null);
                            return false;
                        }
                    }
                    else
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, EventInvalidType.INV_StillOut, entrance, null);
                        return false;
                    }
                }
            }
            if (card.AccessID > 0)  //判断是否有权限进入
            {
                if (AccessSetting.Current != null && AccessSetting.Current.Accesses != null)
                {
                    AccessInfo access = AccessSetting.Current.Accesses.FirstOrDefault(item => item.ID == card.AccessID);
                    if (access != null && !access.CanAccess(entrance.EntranceInfo.EntranceID, DateTime.Now))
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, card.IsCardList ? BusinessModel.Enum.EventInvalidType.INV_NoAccessRight : BusinessModel.Enum.EventInvalidType.INV_NoAccess, entrance, null);
                        return false;
                    }
                }
            }
            //判断是否有超速违章记录
            if ((UserSetting.Current.ForbiddenEnterWhenSpeeding && !entrance.IsExitDevice)
                || (UserSetting.Current.ForbiddenExitWhenSpeeding && entrance.IsExitDevice))
            {
                SpeedingRecordBll bll = new SpeedingRecordBll(AppSettings.CurrentSetting.ParkConnect);
                List<SpeedingRecord> records = bll.GetRecordsByCarPlate(card.CarPlate).QueryObjects;
                if (records != null && records.Count > 0)
                {
                    DenyCard(card.CardID, card.CarPlate, card.CardType, report.CardType, report.Reader, BusinessModel.Enum.EventInvalidType.INV_Speeding, entrance, null);
                    return false;
                }
            }

            return true;
        }
        //处理月卡入场
        private void MonthCardEnteringHander(CardInfo card, EntranceReader reader, DateTime eventDT, EntranceBase entrance)
        {
            CreateCardEnterEvent(card, entrance, eventDT);
            entrance.ProcessingEvent.Reader = reader;
            if (UserSetting.Current.EnableCarPlateRecognize)
            {
                CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
            }
            else
            {
                entrance.ProcessingEvent.CarPlate = card.CarPlate;
            }
            if (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail && card.CardType != CardType.VipCard) //太古汇临时增加vip卡车牌对比失败时也放行的功能 2014-5-14
            {
                entrance.CardWait();
                RaiseCardEventReporting(entrance.ProcessingEvent);
                return;
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty); //无需确认则写数据库
        }
        //处理储值卡入场
        private void PrepayCardEnteringHandler(CardInfo card, EntranceReader reader, DateTime eventDT, EntranceBase entrance)
        {
            CreateCardEnterEvent(card, entrance, eventDT);
            entrance.ProcessingEvent.Reader = reader;
            if (UserSetting.Current.EnableCarPlateRecognize)
            {
                CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
            }
            else
            {
                entrance.ProcessingEvent.CarPlate = card.CarPlate;
            }
            if (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)
            {
                entrance.CardWait();
                RaiseCardEventReporting(entrance.ProcessingEvent);
                return;
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty); //无需确认则写数据库
        }
        //处理临时卡入场
        private void TempCardEnteringHandler(CardInfo card, EntranceReader reader, DateTime eventDT, EntranceBase entrance)
        {
            CreateCardEnterEvent(card, entrance, eventDT);
            entrance.ProcessingEvent.Reader = reader;
            if (entrance.IsTempReader(reader))
            {
                //如果是临时读头读到的，可认为临时卡是从发卡机发卡的，需要清空免费授权信息
                entrance.ProcessingEvent.ClearFreeAuthorization();
            }

            if (UserSetting.Current.EnableCarPlateRecognize) CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty); //无需确认则写数据库
        }
        //处理月卡出场
        private void MonthCardExitingHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            CreateCardExitEvent(card, entrance, eventDateTime);
            entrance.ProcessingEvent.Reader = reader;
            if (UserSetting.Current.EnableCarPlateRecognize)
            {
                CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
            }
            else
            {
                entrance.ProcessingEvent.CarPlate = card.CarPlate;
            }
            if ((entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail && card.CardType != CardType.VipCard) //太古汇临时增加vip卡车牌对比失败时也放行的功能 2014-5-14  //车牌对比失败
                || (entrance.ProcessingEvent.CardPaymentInfo != null && entrance.ProcessingEvent.CardPaymentInfo.Accounts > 0) //月卡如果产生了费用
                || (entrance.EntranceInfo.MonthCardWaitWhenOut) //月卡出场需确认
                )
            {
                entrance.CardWait();
                RaiseCardEventReporting(entrance.ProcessingEvent);
                return;
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
        }
        //处理储值卡出场
        private void PrepayCardExitingHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            CreateCardExitEvent(card, entrance, eventDateTime);
            entrance.ProcessingEvent.Reader = reader;
            if (UserSetting.Current.EnableCarPlateRecognize)
            {
                CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
            }
            else
            {
                entrance.ProcessingEvent.CarPlate = card.CarPlate;
            }
            if ((entrance.ProcessingEvent.ChargeAsTempCard)   //余额不足
                 || (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)  //车牌对比失败
                 || (entrance.EntranceInfo.PrepayCardWaitWhenOut) //储值卡出场需确认
                     )
            {
                entrance.CardWait();
                RaiseCardEventReporting(entrance.ProcessingEvent);
                return;
            }
            if (entrance.ProcessingEvent.CardPaymentInfo.Accounts > 0)
            {
                entrance.ProcessingEvent.CardPaymentInfo.Paid = entrance.ProcessingEvent.CardPaymentInfo.Accounts;
                entrance.ProcessingEvent.CardPaymentInfo.OperatorID = string.Empty;
                entrance.ProcessingEvent.CardPaymentInfo.StationID = string.Empty;
                entrance.ProcessingEvent.CardPaymentInfo.PaymentMode = PaymentMode.Prepay;
                CardBll cpb = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
                CommandResult result = cpb.PayParkFeeWithStandby(entrance.ProcessingCard, entrance.ProcessingEvent.CardPaymentInfo, AppSettings.CurrentSetting.CurrentStandbyConnect);
                if (result.Result == ResultCode.Successful)
                {
                    entrance.ProcessingEvent.Balance -= entrance.ProcessingEvent.CardPaymentInfo.Accounts;
                }
                else if (!entrance.ProcessingEvent.OnlineHandleWhenOfflineMode
                    && entrance.ProcessingEvent.Reader == EntranceReader.DeskTopReader)//脱机处理时，返回由上位机处理
                {
                    entrance.CardWait();
                    RaiseCardEventReporting(entrance.ProcessingEvent);
                    return;
                }
                else
                {
                    DenyCard(card.CardID, card.CarPlate, card.CardType, 0, reader, BusinessModel.Enum.EventInvalidType.INV_ReadCard, entrance, null);
                    return;
                }
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
        }
        //处理临时卡出场
        private void TempCardExitingHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            CreateCardExitEvent(card, entrance, eventDateTime);
            entrance.ProcessingEvent.Reader = reader;
            if (entrance.ProcessingEvent.CardPaymentInfo.Accounts == 0)
            {
                if (entrance.IsTempReader(reader))
                {
                    if (card.EnableHotelApp && !card.HotelCheckOut && card.IsInFreeTime(eventDateTime))
                    {
                        //临时卡时，如果启用了酒店应用并且未退房的，并且处于免费时间内的，不允许在网络型停车场的出口控制板临时卡读头上读卡
                        DenyCard(card.CardID, card.CarPlate, card.CardType, 0, reader, BusinessModel.Enum.EventInvalidType.INV_Type, entrance, null);
                        return;
                    }

                    //如果收卡机内没有读卡器要发出“请插卡回收"语音
                    if (entrance.EntranceInfo.NoReaderOnCardCaptuer) entrance.CardWait();
                    entrance.StartCapture();
                }
                else
                {
                    if (reader == EntranceReader.DeskTopReader && UserSetting.Current.EnableCarPlateRecognize) //桌面发卡机刷卡
                    {
                        CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
                        //在线模式远程读卡时，如果启用了车牌对比功能，车牌对比成功后直接放行
                        if (!Park.IsWriteCardMode && UserSetting .Current .TempCardExitWaitWhenCarPlateFail && entrance.ProcessingEvent.EventStatus != CardEventStatus.CarPlateFail)
                        {
                            entrance.CardValid();
                            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
                            return;
                        }
                    }

                    //在线模式在票箱上刷卡，临时卡时，如果启用了酒店应用并且未退房的，并且处于免费时间内的，车牌对比成功后直接放行
                    if (!Park.IsWriteCardMode && reader != EntranceReader.DeskTopReader && card.EnableHotelApp && !card.HotelCheckOut && card.IsInFreeTime(eventDateTime))
                    {
                        if (UserSetting.Current.EnableCarPlateRecognize) CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
                        //如果启用车牌识别,且出入口车牌对比成功
                        if (entrance.ProcessingEvent.EventStatus != CardEventStatus.CarPlateFail)
                        {
                            entrance.CardValid();
                            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
                            return;
                        }
                    }

                    entrance.CardWait();
                    RaiseCardEventReporting(entrance.ProcessingEvent);
                }
            }
            else
            {
                if (entrance.IsTempReader(reader))
                {
                    if (entrance.ProcessingCard.PaidDateTime == null)
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, 0, reader, BusinessModel.Enum.EventInvalidType.IVN_NotPaid, entrance, null);
                        entrance.TakeOutACard();
                    }
                    else
                    {
                        DenyCard(card.CardID, card.CarPlate, card.CardType, 0, reader, BusinessModel.Enum.EventInvalidType.INV_OverTime, entrance, null);
                        entrance.TakeOutACard();
                    }
                }
                else if (reader == EntranceReader.DeskTopReader) //桌面发卡机刷卡
                {
                    if (UserSetting.Current.EnableCarPlateRecognize) CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
                    entrance.CardWait();
                    RaiseCardEventReporting(entrance.ProcessingEvent);
                }
                else
                {
                    entrance.CardWait();
                    RaiseCardEventReporting(entrance.ProcessingEvent);
                }
            }
        }
        //处理嵌套车场事件
        private void NestedCardEventHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            if (entrance.IsExitDevice)
            {
                entrance.ProcessingEvent = CardEventReport.CreateExitEvent(card, entrance.ParkID, entrance.EntranceID,
                    entrance.EntranceName, entrance.Park.WorkMode, card.CarType, TariffSetting.Current, eventDateTime);
                entrance.ProcessingEvent.ParkingStatus = card.ParkingStatus | ParkingStatus.NestedParkMarked | ParkingStatus.IndoorIn | ParkingStatus.In;
                entrance.ProcessingEvent.ParkingStatus ^= ParkingStatus.IndoorIn;//出嵌套车场后状态
            }
            else
            {
                entrance.ProcessingEvent = CardEventReport.CreateEnterEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, entrance.Park.WorkMode, eventDateTime);
                entrance.ProcessingEvent.ParkingStatus = card.ParkingStatus | ParkingStatus.NestedParkMarked | ParkingStatus.IndoorIn | ParkingStatus.In;
            }
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingCard = card;
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty); //无需确认则写数据库
        }
        //处理门禁模式运行时的刷卡事件
        private void ACSCardEventHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            entrance.ProcessingEvent = CardEventReport.CreateEnterEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, entrance.Park.WorkMode, eventDateTime);
            entrance.ProcessingEvent.IsExitEvent = entrance.EntranceInfo.IsExitDevice;  //生成事件时只用简单的入场事件，不计算停车费用，所以这里要把出口事件改成出口
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingCard = card;
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty); //无需确认则写数据库
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置停车场的父停车场
        /// </summary>
        public ParkBase Parent { get; set; }
        /// <summary>
        /// 获取停车场的信息
        /// </summary>
        public ParkInfo Park
        {
            get { return _Park; }
        }
        /// <summary>
        /// 获取停车场所有通道
        /// </summary>
        public List<EntranceBase> Entrances
        {
            get { return _Entrances; }
        }

        /// <summary>
        /// 获取停车场的主控制通道
        /// </summary>
        public abstract EntranceBase Master { get; }

        /// <summary>
        /// 获取停车场的工作模式
        /// </summary>
        public ParkWorkMode WorkMode
        {
            //递归获取最上层停车场的工作模式
            get
            {
                if (Parent == null)
                {
                    return Park.WorkMode;
                }
                else
                {
                    return Parent.WorkMode;
                }
            }
        }
        /// <summary>
        /// 获取停车场的进出凭证名单模式
        /// </summary>
        public ParkListMode ListMode
        {
            //递归获取最上层停车场的进出凭证名单模式
            get
            {
                if (Parent == null)
                {
                    return Park.ListMode;
                }
                else
                {
                    return Parent.ListMode;
                }
            }
        }
        /// <summary>
        /// 获取或设置停车场空车位数
        /// </summary>
        public short ParkVacant
        {
            get
            {
                return Park.Vacant;
            }
            set
            {
                lock (_ParkVacantLocker)
                {
                    Park.Vacant = value;
                }
            }
        }
        #endregion

        #region 公共事件
        public event ReportingHandler<EntranceStatusReport> EntranceStatusReporting;

        public event ReportingHandler<CardEventReport> CardEventReporting;

        public event ReportingHandler<CardInvalidEventReport> CardInvalidReporting;

        public event ReportingHandler<CarSenseReport> CarSenseReporting;

        public event ReportingHandler<ParkVacantReport> ParkVacantReporting;

        public event ReportingHandler<CardCaptureReport> CardCaptureReporting;

        public event ReportingHandler<EntranceRemainTempCardReport> EntranceRemainTempCardReporting;

        public event ReportingHandler<DeviceResetReport> DeviceResetReporting;

        public event ReportingHandler<AlarmReport> AlarmReporting;

        protected void RaiseEntranceStatusReporting(EntranceStatusReport report)
        {
            if (this.EntranceStatusReporting != null) this.EntranceStatusReporting(this, report);
        }

        protected void RaiseCardEventReporting(CardEventReport report)
        {
            if (this.CardEventReporting != null) this.CardEventReporting(this, report);
        }

        protected void RaiseCardInvalidReporting(CardInvalidEventReport report)
        {
            if (this.CardInvalidReporting != null) this.CardInvalidReporting(this, report);
        }

        protected void RaiseCarSenseReporting(CarSenseReport report)
        {
            if (this.CarSenseReporting != null) this.CarSenseReporting(this, report);
        }

        protected void RaiseParkVacantReporting(ParkVacantReport report)
        {
            if (this.ParkVacantReporting != null) this.ParkVacantReporting(this, report);
        }

        protected void RaiseCardCaptureReporting(CardCaptureReport report)
        {
            if (this.CardCaptureReporting != null) CardCaptureReporting(this, report);
        }

        protected void RaiseEntranceRemainTempCardReporting(EntranceRemainTempCardReport report)
        {
            if (this.EntranceRemainTempCardReporting != null) this.EntranceRemainTempCardReporting(this, report);
        }

        protected void RaiseDeviceResetReporting(DeviceResetReport report)
        {
            if (this.DeviceResetReporting != null) this.DeviceResetReporting(this, report);
        }

        protected void RaiseAlarmReporting(AlarmReport report)
        {
            if (this.AlarmReporting != null) this.AlarmReporting(this, report);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取下一个轮换通道序号
        /// </summary>
        /// <param name="currentNumber">当前轮换通道序号</param>
        /// <returns></returns>
        private int GetNextRotationNumber(int currentNumber)
        {
            Dictionary<int, List<RotationInfo>> rotations = UserSetting.Current.Rotations;

            if (rotations != null && rotations.Count > 0)
            {
                int nextNumber = rotations.Keys.FirstOrDefault(k => k > currentNumber);
                if (nextNumber == 0)
                {
                    //没有的下一个的，获取第一个 
                    nextNumber = rotations.Keys.FirstOrDefault(k => k > 0);
                }
                return nextNumber;
            }
            return 0;
        }

        /// <summary>
        /// 该entranceID通道是否能取卡
        /// </summary>
        /// <returns></returns>
        public bool CanTakeCard(int entranceID)
        {
            //没有启用轮换功能的，可以取卡
            if (UserSetting.Current == null && !UserSetting.Current.EnableRotation)
            {
                return true;
            }

            //当启用轮换功能时，车位数大于等于进入轮换状态的数值的，允许取卡
            if (this.ParkVacant >= UserSetting.Current.RotationVacant)
            {
                return true;
            }

            //小于时，进入轮换状态
            if (UserSetting.Current.Rotations == null
                || UserSetting.Current.Rotations.Count == 0)
            {
                //没有设置轮换通道，该通道不允许取卡
                return false;
            }

            
            lock (_RotationNumberLock)
            {
                //如果没有当前序号，查找下一个轮换序号
                if (!UserSetting.Current.Rotations.ContainsKey(_CurrentRotationNumber))
                {
                    _CurrentRotationNumber = GetNextRotationNumber(_CurrentRotationNumber);
                    if (_CurrentRotationNumber == 0)
                    {
                        //没找到
                        return false;
                    }
                }

                int currentNumber = _CurrentRotationNumber;//当前序号

                do
                {
                    //entranceID通道是否当前轮换通道
                    List<RotationInfo> rotations = UserSetting.Current.Rotations[currentNumber];
                    if (rotations != null && rotations.Count > 0)
                    {
                        if (rotations.Any(r => r.EntranceID == entranceID))
                        {
                            //如果是当前通道，返回可以取卡
                            if (currentNumber != _CurrentRotationNumber)
                            {
                                _CurrentRotationNumber = currentNumber;
                            }
                            return true;
                        }

                        //如果不是当前轮换通道，判断当前轮换车道上是否有车
                        foreach (RotationInfo info in rotations)
                        {
                            EntranceBase entrance = GetEntrance(info.EntranceID);
                            if (entrance != null
                                && entrance.Status != EntranceStatus.OffLine)
                            {
                                //如果该控制板在线，判断是否有车
                                if (entrance.OptStatus == EntranceOperationStatus.CarArrival
                                || entrance.OptStatus == EntranceOperationStatus.CardTakeingOut
                                || entrance.OptStatus == EntranceOperationStatus.FullAndWait)
                                {
                                    //该通道上有车，不允许取卡
                                    return false;
                                }
                            }
                        }
                    }

                    //如果当前轮换通道上没有车辆，查找下一个轮换序号
                    currentNumber = GetNextRotationNumber(currentNumber);
                    if (currentNumber == 0)
                    {
                        //没找到
                        _CurrentRotationNumber = 0;
                        return false;
                    }
                }
                while (currentNumber != _CurrentRotationNumber);//当相等时，表示已经查找完一轮了
            }


            //已经查找完一轮了，该通道不在轮换列表的，不允许取卡
            return false;
        }

        /// <summary>
        /// 轮换到下一个序号取卡
        /// </summary>
        /// <param name="entranceID"></param>
        public void NextRotation(int entranceID)
        {
            if (UserSetting.Current != null && UserSetting.Current.EnableRotation)
            {
                //当启用轮换功能时，车位数小于进入轮换状态的数值的，轮换到下一个序号
                if (this.ParkVacant < UserSetting.Current.RotationVacant)
                {
                    if (UserSetting.Current.Rotations != null)
                    {
                        if (UserSetting.Current.Rotations.ContainsKey(_CurrentRotationNumber))
                        {
                            lock (_RotationNumberLock)
                            {
                                List<RotationInfo> rotations = UserSetting.Current.Rotations[_CurrentRotationNumber];
                                //如果通道是当前轮换的通道
                                if (rotations.Any(r => r.EntranceID == entranceID))
                                {
                                    //轮换到下一个通道序号
                                    _CurrentRotationNumber = GetNextRotationNumber(_CurrentRotationNumber);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 基类可重写的保护方法，产生相关事件时的处理函数
        protected virtual void OnEntranceStatusReporting(EntranceStatusReport report)
        {
            RaiseEntranceStatusReporting(report);
        }

        protected virtual void OnCardInvalidReporting(CardInvalidEventReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            entrance.ProcessingEvent = null;
            entrance.Station = null;
            entrance.Operator = null;
            RaiseCardInvalidReporting(report);
        }

        protected virtual void OnCarSenseReporting(CarSenseReport report)
        {
            if (report.InOrOutFlag == 1 && UserSetting.Current.SnapshotWhenCarArrive)
            {
                AlarmInfo alarm = new AlarmInfo()
                {
                    AlarmDateTime = report.EventDateTime,
                    AlarmSource = report.SourceName,
                    AlarmType = AlarmType.CarArrive,
                };
                (new AlarmBll(AppSettings.CurrentSetting.ParkConnect)).Insert(alarm);
            }
            RaiseCarSenseReporting(report);
        }

        protected virtual void OnParkVacantReporting(ParkVacantReport report)
        {
            lock (_ParkVacantLocker)
            {
                _Park.Vacant = report.ParkVacant;
            }
            (new ParkBll(AppSettings.CurrentSetting.ParkConnect)).UpdateVacant(_Park.ParkID, _Park.Vacant);
            RaiseParkVacantReporting(report);
        }

        protected virtual void OnCardCaptureReporting(CardCaptureReport report)
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
                    RaiseCardEventReporting(entrance.ProcessingEvent);
                }
                else
                {
                    entrance.CardValid();
                    //add by Jan 2014-06-22
                    //由于临时卡已回收到票箱了，当抬闸失败时，也不能再次刷卡出场了，所以这里也不再需要等待返回抬闸事件确认了，直接标识卡片已出场
                    //if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
                    PermitCard(entrance, string.Empty, string.Empty);
                }
            }
        }

        protected virtual void OnEntranceRemainTempCardReporting(EntranceRemainTempCardReport report)
        {
            RaiseEntranceRemainTempCardReporting(report);
        }

        protected virtual void OnDeviceResetReporting(DeviceResetReport report)
        {
            RaiseDeviceResetReporting(report);
        }

        protected virtual void OnAlarmReporting(AlarmReport report)
        {
            RaiseAlarmReporting(report);
        }

        protected virtual void OnCardReading(CardReadReport report)
        {
            ProcessCardRead(report);
        }

        protected virtual void OnCardWaiting(OfflineCardReadReport report)
        {

        }

        protected virtual void OnCardPermitted(OfflineCardReadReport report)
        {

        }

        protected virtual void OnCommandEchoReporting(CommandEchoReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            ////Modify by Jan 2014-07-24 如果抬闸事件返回卡号，需判断与当前处理事件卡号相同才处理
            //if (entrance.ProcessingEvent != null && (string.IsNullOrEmpty(report.CardID) || entrance.ProcessingEvent.CardID == report.CardID))
            //Modify by Jan 2014-09-23 这里不判断与当前处理事件卡号是否相同，是为了兼容旧版本的硬件，因为旧版本的硬件程序抬闸事件上传的卡号有问题，
            //（已发现太古汇现场硬件（软件程序版本1.47）有问题，如需要判断与当前处理事件卡号相同才处理，需确认现场硬件程序版本是否支持）
            if (entrance.ProcessingEvent != null)
            {
                if (entrance.ProcessingEvent.ComparisonResult == CarPlateComparisonResult.Fail)
                {
                    entrance.ProcessingEvent.ComparisonResult = CarPlateComparisonResult.Noncontrastive;
                }
                PermitCard(entrance, entrance.Operator, entrance.Station);
            }
        }
        #endregion

        #region IPark 接口方法
        public virtual bool DownloadVacantSetting(CarPortSetting vacantInfo)
        {
            throw new NotImplementedException("sub class not implement DownloadVacantSetting");
        }

        public virtual bool ModifiedVacant(short vacant)
        {
            lock (_ParkVacantLocker)
            {
                short newVacant = (short)(Park.Vacant + vacant);
                if (newVacant < 0)
                {
                    Park.Vacant = 0;
                }
                else if (newVacant > Park.TotalPosition)
                {
                    Park.Vacant = Park.TotalPosition;
                }
                else
                {
                    Park.Vacant = newVacant;
                }
            }
            if (Park.DeviceType == EntranceDeviceType.CANEntrance)
            {
                //网络型的车余数已通过心跳包发送到主控制板，主控制板会自动更新硬件的车余数，不需要发送显示车余数命令
                foreach (EntranceBase en in _Entrances)
                {
                    if (!en.IsExitDevice && en.EntranceInfo.EnableParkvacantLed) en.ShowVacant();
                }
            }
            else if (Park.WorkMode == ParkWorkMode.OffLine)
            {
                //网络型的脱机模式需要发送下载车位信息命令更新车位余数
                CarPortSetting vacantInfo = new CarPortSetting(Park);

                return DownloadVacantSetting(vacantInfo);
            }

            
            ParkVacantReport pReport = new ParkVacantReport();
            pReport.ParkID = Park.ParkID;
            pReport.ParkVacant = Park.Vacant;
            OnParkVacantReporting(pReport);

            return true;
        }

        public virtual bool DownloadAccessSetting(AccessSetting ascLevel)
        {
            AccessSetting.Current = ascLevel;
            bool ret = true;
            //if (_Entrances != null)
            //{
            //    foreach (EntranceBase entrance in _Entrances)
            //    {
            //        if (!entrance.ApplyAccessSetting(ascLevel)) ret = false;
            //    }
            //}
            //if (_SubParks != null)
            //{
            //    foreach (ParkBase p in _SubParks)
            //    {
            //        if (!p.DownloadAccessSetting(ascLevel)) ret = false;
            //    }
            //}
            return ret;
        }

        public virtual bool DownloadAccessSettingToEntrance(int entranceID, AccessSetting ascLevel)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.ApplyAccessSetting(ascLevel);
            }
            return false;
        }

        public virtual bool DownLoadUserSetting(UserSetting us)
        {
            UserSetting.Current = us;
            bool ret = true;
            if (_Entrances != null)
            {
                foreach (EntranceBase entrance in _Entrances)
                {
                    if (!entrance.ApplyUserSetting(us)) ret = false;
                }
            }
            if (_SubParks != null)
            {
                foreach (ParkBase p in _SubParks)
                {
                    if (!p.DownLoadUserSetting(us)) ret = false;
                }
            }
            return ret;
        }

        public virtual bool DownloadTariffSetting(TariffSetting tariffSetting)
        {
            bool ret = true;
            TariffSetting.Current = tariffSetting;
            if (_Entrances != null)
            {
                foreach (EntranceBase entrance in _Entrances)
                {
                    if (!entrance.ApplyTariffSetting(tariffSetting)) ret = false;
                }
            }
            if (_SubParks != null)
            {
                foreach (ParkBase p in _SubParks)
                {
                    if (!p.DownloadTariffSetting(tariffSetting)) ret = false;
                }
            }
            return ret;
        }

        public virtual bool DownloadTariffSettingToEntrance(int entranceID, TariffSetting tariffSetting)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.ApplyTariffSetting(tariffSetting);
            }
            return false;
        }

        public virtual bool DownloadKeySetting(KeySetting keySetting)
        {
            KeySetting.Current = keySetting;
            return true;
        }

        public virtual bool DownloadKeySettingToEntrance(int entranceID, KeySetting keySetting)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.ApplyKeySetting(keySetting);
            }
            return false;
        }

        public virtual bool DownloadHolidaySetting(HolidaySetting holidaySetting)
        {
            HolidaySetting.Current = holidaySetting;
            bool ret = true;
            //if (_Entrances != null)
            //{
            //    foreach (EntranceBase entrance in _Entrances)
            //    {
            //        if (!entrance.ApplyHolidaySetting(holidaySetting)) ret = false;
            //    }
            //}
            //if (_SubParks != null)
            //{
            //    foreach (ParkBase p in _SubParks)
            //    {
            //        if (!p.DownloadHolidaySetting(holidaySetting)) ret = false;
            //    }
            //}
            return ret;
        }

        public virtual bool DownloadHolidaySettingToEntrance(int entranceID, HolidaySetting holidaySetting)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.ApplyHolidaySetting(holidaySetting);
            }
            return false;
        }

        public virtual bool DownloadCarTypeSetting(CarTypeSetting carTypeSetting)
        {
            CarTypeSetting.Current = carTypeSetting;
            return true;
        }

        //add by Jan 2012-3-24
        public virtual bool DownloadDiscountCalculateSetting(DiscountCalculateSetting discountcalculateSetting)
        {
            DiscountCalculateSetting.Current = discountcalculateSetting;
            return true;
        }
        //end
        public virtual bool EventValid(EventValidNotify notify)
        {
            return false;
        }

        public virtual bool EventInvalid(EventInvalidNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.CardEvent.EntranceID);
            if (entrance != null)
            {
                if (notify.InvalidType == EventInvalidType.INV_Balance)
                {
                    DenyCard(notify.CardEvent.CardID, notify.CardEvent.CarPlate, notify.CardEvent.CardType, 0, notify.CardEvent.Reader, notify.InvalidType, entrance, notify.Balance);
                }
                else if (notify.InvalidType == EventInvalidType.INV_OverDate)
                {
                    DenyCard(notify.CardEvent.CardID, notify.CardEvent.CarPlate, notify.CardEvent.CardType, 0, notify.CardEvent.Reader, notify.InvalidType, entrance, notify.ExpireDate);
                }
                else
                {
                    DenyCard(notify.CardEvent.CardID, notify.CardEvent.CarPlate, notify.CardEvent.CardType, 0, notify.CardEvent.Reader, notify.InvalidType, entrance, null);
                }
            }
            return true;
        }

        public virtual bool ResetDevice(DeviceReSetNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.EntranceID);
            if (entrance != null) entrance.Reset();
            return true;
        }

        public virtual void LedDisplay(SetLedDisplayNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.EntranceID);
            entrance.DisplayMsg(notify.DisplayMsg, notify.IsPermanent);
        }

        public virtual bool GateOperate(GateOperationNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.EntranceID);
            if (entrance != null) entrance.OperateGate(notify);

            AlarmInfo alarm = new AlarmInfo();
            alarm.AlarmDateTime = DateTime.Now;
            alarm.AlarmSource = entrance.EntranceName;
            alarm.AlarmType = notify.Action == GateOperation.Open ? AlarmType.Opendoor : AlarmType.Closedoor;
            alarm.OperatorID = notify.OperatorID;
            (new AlarmBll(AppSettings.CurrentSetting.ParkConnect)).Insert(alarm);

            AlarmReport report = new AlarmReport(
                _Park.ParkID, entrance.EntranceID, alarm.AlarmDateTime,
                entrance.EntranceName, alarm.AlarmType,
                alarm.AlarmDescr, alarm.OperatorID);
            if (this.AlarmReporting != null) this.AlarmReporting(this, report);
            return true;
        }

        /// <summary>
        /// 更改车辆类型
        /// </summary>
        /// <param name="carType"></param>
        public virtual void SwitchCarType(CarTypeSwitchNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.EntranceID);
            if (entrance == null) return;
            if (entrance.ProcessingEvent != null && entrance.ProcessingEvent.IsExitEvent)
            {
                //CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDetail(entrance.ProcessingEvent.CardID);
                CardInfo card = entrance.ProcessingCard;
                if (card != null)
                {
                    entrance.ProcessingEvent = CardEventReport.CreateExitEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, entrance.Park.WorkMode,
                        notify.CarType, TariffSetting.Current, entrance.ProcessingEvent.EventDateTime);
                    entrance.CardWait();
                    RaiseCardEventReporting(entrance.ProcessingEvent);
                }
            }
        }

        /// <summary>
        /// 更改通道
        /// </summary>
        /// <param name="carType"></param>
        public virtual void SwitchEntrance(EntranceSwitchNotify notify)
        {
            EntranceBase currentEntrance = GetEntrance(notify.CurrentEntranceID);
            EntranceBase newEntrance = GetEntrance(notify.NewEntranceID);
            if (currentEntrance == null || newEntrance == null) return;
            if (currentEntrance.ProcessingEvent != null && currentEntrance.ProcessingEvent.IsExitEvent)
            {
                CardInfo card = currentEntrance.ProcessingCard;
                EntranceReader reader = currentEntrance.ProcessingEvent.Reader;
                if (card != null)
                {
                    //新通道生成处理事件
                    newEntrance.ProcessingEvent = CardEventReport.CreateExitEvent(card, newEntrance.ParkID, newEntrance.EntranceID, newEntrance.EntranceName, newEntrance.Park.WorkMode,
                        currentEntrance.ProcessingEvent.CarType, TariffSetting.Current, currentEntrance.ProcessingEvent.EventDateTime);
                    newEntrance.ProcessingEvent.Reader = reader;
                    newEntrance.ProcessingCard = card;

                    if (notify.CurrentEntranceID != notify.NewEntranceID)
                    {
                        //如不是同一个通道，将当前通道的参数清空
                        currentEntrance.ProcessingEvent = null;
                        currentEntrance.Operator = string.Empty;
                        currentEntrance.Station = string.Empty;
                    }

                    newEntrance.CardWait();
                    RaiseCardEventReporting(newEntrance.ProcessingEvent);
                }
            }
        }

        public virtual bool SetEntranceRemainTempCard(EntranceRemainTempCardNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.EntranceID);
            if (entrance != null)
            {
                entrance.RemainTempCard = notify.RemainTempCard;
            }
            return true;
        }

        public virtual bool RemoteReadCard(RemoteReadCardNotify notify)
        {
            EntranceBase entrance = GetEntrance(notify.EntranceID);
            if (entrance != null)
            {
                CardReadReport report = new CardReadReport();
                report.CardID = notify.CardID;
                report.CarPlate = notify.CarPlate;
                report.ParkingData = notify.ParkingData;
                report.ParkID = notify.ParkID;
                report.EntranceID = notify.EntranceID;
                report.EventDateTime = DateTime.Now;
                report.CannotIgnored = true;
                report.Reader = Ralid.Park.BusinessModel.Enum.EntranceReader.DeskTopReader;
                report.LastCarPlate = notify.LastCarPlate;
                report.IsCarPlateEventHandle = string.IsNullOrEmpty(notify.CardID);
                report.IsCarNotPlate = string.IsNullOrEmpty(notify.CarPlate);
                entrance.Operator = notify.OperatorID;
                entrance.Station = notify.Station;
                entrance.AddToReportPool(report);
            }
            return true;
        }

        public abstract bool AddEntrance(EntranceInfo info);

        public abstract bool UpdateEntrance(EntranceInfo info);

        public virtual bool DeleteEntrance(EntranceInfo info)
        {
            EntranceBase entrance = GetEntrance(info.EntranceID);
            if (entrance != null)
            {
                entrance.Dispose();
                _Entrances.Remove(entrance);
            }
            return true;
        }

        public virtual bool SaveCard(CardInfo card, ActionType action)
        {
            if (Master != null)
            {
                return Master.SaveCard(card, action);
            }
            return false;
        }

        public virtual bool SaveCardToEntrance(int entranceID, CardInfo card, ActionType action)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.SaveCard(card, action);
            }
            return false;
        }

        public virtual bool SaveCardsToEntrance(int entranceID, List<CardInfo> cards, ActionType action)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.SaveCards(cards, action);
            }
            return false;
        }

        public virtual bool DeleteCard(CardInfo card)
        {
            if (Master != null)
            {
                return Master.DeleteCard(card);
            }
            return false;
        }

        public virtual bool DeleteCardToEntrance(int entranceID, CardInfo card)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.DeleteCard(card);
            }
            return false;
        }

        public virtual bool ClearCard()
        {
            if (Master != null)
            {
                return Master.ClearCard();
            }
            return false;
        }

        public virtual bool ClearCardToEntrance(int entranceID)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.ClearCard();
            }
            return false;
        }

        public virtual void WaitingCommandServiceEnable(bool enable)
        { 
        }

        #region 写卡模式新增

        public EntranceStatus GetParkStatus()
        {
            return _Park.Status;
        }

        public EntranceStatus GetEntranceStatus(int entranceID)
        {
            EntranceBase entrance = GetEntrance(entranceID);
            if (entrance != null)
            {
                return entrance.Status;
            }
            return EntranceStatus.UnKnown;
        }

        //public byte GetServerWorkMode()
        //{
        //    return AppSettings.CurrentSetting.ParkWorkMode;
        //}

        public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType)
        {
            return CardPaymentInfoFactory.CreateCardPaymentRecord(this.Park.ParkID, card, TariffSetting.Current, carType, DateTime.Now);
        }

        public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType, DateTime datetime)
        {
            return CardPaymentInfoFactory.CreateCardPaymentRecord(this.Park.ParkID, card, TariffSetting.Current, carType, datetime);
        }
        #endregion

        #endregion
    }
}

