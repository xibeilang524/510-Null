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
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.GeneralLibrary.LOG;
using Ralid.Park.PlateRecognition;
using Ralid.Park.BusinessModel.Factory;
using Ralid.Park.BusinessModel.SearchCondition;

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
            CardEventReport cardEvent = CardEventReport.CreateEnterEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, eventDt);
            entrance.ProcessingCard = card;
            entrance.ProcessingEvent = cardEvent;
            entrance.ProcessingEvent.OperatorID = entrance.Operator;
            entrance.ProcessingEvent.StationID = entrance.Station;
        }

        protected void CreateCardExitEvent(CardInfo card, EntranceBase entrance, DateTime eventDt)
        {
            CardEventReport cardEvent = CardEventReport.CreateExitEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, card.CarType, TariffSetting.Current, eventDt);
            entrance.ProcessingEvent = cardEvent;
            entrance.ProcessingCard = card;
        }

        protected void CarPlateHandler(EntranceBase entrance, CardEventReport cardEvent, CardInfo card)
        {
            if (UserSetting.Current.SoftWareCarPlateRecognize)
            {
                PlateRecognitionResult ret = CarPalteRecognize(entrance.ParkID, entrance.EntranceID);
                cardEvent.CarPlate = ret.CarPlate;
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
                if (entrance.ProcessingCard.WithCount && !entrance.EntranceInfo.NoParkingCount)  //如果卡片要进行车位计数或者通道启用车位计数
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
                                if (Park.Vacant > Park.MinPosition) Park.Vacant--;
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
        /// <param name="cardID"></param>
        /// <param name="invalidReason"></param>
        /// <param name="entrance"></param>
        protected void DenyCard(string cardID, EventInvalidType invalidReason, EntranceBase entrance, object param)
        {
            entrance.CardInValid(invalidReason, param);
            CardInvalidEventReport report = new CardInvalidEventReport();
            report.ParkID = _Park.ParkID;
            report.EntranceID = entrance.EntranceID;
            report.SourceName = entrance.EntranceName;
            report.CardID = cardID;
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
                CardBll cbll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                CommandResult ret = null;
                if (!entrance.EntranceInfo.UseAsAcs) //FUCK useasacs
                {
                    if (!Park.IsNested)
                    {
                        ret = cbll.SaveCardAndEvent(entrance.ProcessingCard, report);
                    }
                    else
                    {
                        ret = cbll.SaveCardAndNestedEvent(entrance.ProcessingCard, report);
                    }
                }
                else
                {
                    ret = (new CardEventBll(AppSettings.CurrentSetting.ParkConnect)).Insert(new CardEventRecord(report));
                }
                if (entrance.ProcessingCard.EnableLimitation)
                {
                    entrance.DisplayMsg(string.Format("剩余限时停车时长{0:F1}小时", entrance.ProcessingCard.LimitationRemain), false);
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

            CardBll cbl = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            CardInfo card = cbl.GetCardDetail(report.CardID);
            if (card != null)
            {
                //写卡模式时，桌面读卡时需读取卡片中停车场扇区的数据，并且不是按在线模式处理
                if (Park.IsWriteCardMode && report.Reader == EntranceReader.DeskTopReader && !card.OnlineHandleWhenOfflineMode)
                {
                    CardInfo info = CardDateResolver.Instance.GetCardInfoFromData(report.CardID, report.ParkingData);
                    if (info != null)//已读到卡片数据为准，否则返回
                    {
                        //复制缴费数据
                        CardDateResolver.Instance.CopyPaidDataToCard(card, info);
                    }
                    else
                    {
                        DenyCard(report.CardID, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_Invalid, entrance, null);
                        return;
                    }
                }
                card.RegCarPlate = report.CarPlate;//识别到的车牌
                //脱机模式时，最近一次识别到的车牌按硬件上传的最近一次识别到的车牌为准
                if (WorkMode == ParkWorkMode.OffLine) card.LastCarPlate = report.LastCarPlate;
                if (ValidateCard(card, entrance, report) == false) return;  //如果卡片验证失败,则返回
                ProcessCard(entrance, report.Reader, card, report.EventDateTime);
            }
            else
            {
                DenyCard(report.CardID, Ralid.Park.BusinessModel.Enum.EventInvalidType.INV_UnRegister, entrance, null);
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
                    NestedCardEventHandler(card, eventDateTime, entrance);
                }
            }
            else
            {
                ACSCardEventHandler(card, eventDateTime, entrance);
            }
        }
        //检验卡片有效性
        private bool ValidateCard(CardInfo card, EntranceBase entrance, CardReadReport report)
        {
            if (card.Status == CardStatus.Recycled) //卡片已注销
            {
                DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_Recycled, entrance, null);
                return false;
            }
            if (card.Status == CardStatus.Disabled)  //卡片已锁定
            {
                DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_Lock, entrance, null);
                return false;
            }
            if (card.Status == CardStatus.Loss)   //卡片已挂失
            {
                DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_Loss, entrance, null);
                return false;
            }
            if (card.ActivationDate > DateTime.Now) //卡片未到生效期
            {
                DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_NotActive, entrance, null);
                return false;
            }
            if (card.ValidDate < DateTime.Today && card.CardType != Ralid.Park.BusinessModel.Enum.CardType.TempCard && !card.EnableWhenExpired) // && entrance.EntranceInfo.ForbidWhenCardExpired)//卡片已过期
            {
                DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_OverDate, entrance, card.ValidDate);
                return false;
            }
            if (!entrance.EntranceInfo.IsExitDevice && !card.CanEnterWhenFull) //如果是入场并且卡片不能在满位时入场,则判断车位数
            {
                if (Park.Vacant <= Park.MinPosition) //车位已满位入场
                {
                    DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_ParkFull, entrance, null);
                    return false;
                }
            }
            if (!card.HolidayEnabled && HolidaySetting.Current.IsHoliday(DateTime.Now)) //节假日不允许进出
            {
                DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_HolidayDisabled, entrance, null);
                return false;
            }
            if (!entrance.EntranceInfo.UseAsAcs)  //系统为澳大增加一种是否以门禁模式运行
            {
                if (!Park.IsNested)  //非内嵌车场
                {
                    if (!entrance.EntranceInfo.IsExitDevice && card.IsInPark && !card.CanRepeatIn) //重复入场
                    {
                        DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_HaveIn, entrance, null);
                        return false;
                    }
                    if (entrance.EntranceInfo.IsExitDevice && !card.IsInPark && !card.CanRepeatOut)//重复出场
                    {
                        DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_StillOut, entrance, null);
                        return false;
                    }
                    if (!card.CardType.IsTempCard && entrance.IsTempReader(report.Reader) &&
                        entrance.OptStatus == EntranceOperationStatus.CardTakeingOut)  //入口按了取卡按钮后，临时卡读头上如果读到非临时卡，则表明吐出的卡片无效，直接收回
                    {
                        DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_Type, entrance, null);
                        return false;
                    }
                }
                else
                {
                    if (card.IsInPark) //进出内车场时卡片要先进入外车场
                    {
                        if (!entrance.EntranceInfo.IsExitDevice && card.IsInNestedPark && !card.CanRepeatIn) //重复入场
                        {
                            DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_HaveIn, entrance, null);
                            return false;
                        }
                        if (entrance.EntranceInfo.IsExitDevice && !card.IsInNestedPark && !card.CanRepeatOut)//重复出场
                        {
                            DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_StillOut, entrance, null);
                            return false;
                        }
                    }
                    else
                    {
                        DenyCard(card.CardID, EventInvalidType.INV_StillOut, entrance, null);
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
                        DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_Invalid, entrance, null);
                        return false;
                    }
                }
            }
            return true;
        }
        //处理月卡入场
        private void MonthCardEnteringHander(CardInfo card, EntranceReader reader, DateTime eventDT, EntranceBase entrance)
        {
            CreateCardEnterEvent(card, entrance, eventDT);
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingEvent.CarPlate = entrance.Carplate;
            entrance.Carplate = string.Empty;
            if (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)
            {
                entrance.CardWait();
                RaiseCardEventReporting(entrance.ProcessingEvent);
                return;
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station); //无需确认则写数据库
        }
        //处理储值卡入场
        private void PrepayCardEnteringHandler(CardInfo card, EntranceReader reader, DateTime eventDT, EntranceBase entrance)
        {
            CreateCardEnterEvent(card, entrance, eventDT);
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingEvent.CarPlate = entrance.Carplate;
            entrance.Carplate = string.Empty;
            if (entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)
            {
                entrance.CardWait();
                RaiseCardEventReporting(entrance.ProcessingEvent);
                return;
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station); ; //无需确认则写数据库
        }
        //处理临时卡入场
        private void TempCardEnteringHandler(CardInfo card, EntranceReader reader, DateTime eventDT, EntranceBase entrance)
        {
            CreateCardEnterEvent(card, entrance, eventDT);
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingEvent.CarPlate = entrance.Carplate;
            entrance.Carplate = string.Empty;
            if (UserSetting.Current.EnableCarPlateRecognize) CarPlateHandler(entrance, entrance.ProcessingEvent, entrance.ProcessingCard);
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station); ; //无需确认则写数据库
        }
        //处理月卡出场
        private void MonthCardExitingHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            CreateCardExitEvent(card, entrance, eventDateTime);
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingEvent.CarPlate = entrance.Carplate;
            entrance.Carplate = string.Empty;
            if ((entrance.ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)//车牌对比失败
                || (entrance.ProcessingEvent.CardPaymentInfo != null && entrance.ProcessingEvent.CardPaymentInfo.Accounts > 0) //月卡如果产生了费用
                || (entrance.EntranceInfo.MonthCardWaitWhenOut) //月卡出场需确认
                )
            {
                entrance.CardWait();
                RaiseCardEventReporting(entrance.ProcessingEvent);
                return;
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station);
        }
        //处理储值卡出场
        private void PrepayCardExitingHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            CreateCardExitEvent(card, entrance, eventDateTime);
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingEvent.CarPlate = entrance.Carplate;
            entrance.Carplate = string.Empty;
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
                CardBll cpb = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                if (cpb.PayParkFee(entrance.ProcessingEvent.CardPaymentInfo).Result == ResultCode.Successful)
                {
                    entrance.ProcessingEvent.Balance -= entrance.ProcessingEvent.CardPaymentInfo.Accounts;
                }
                else
                {
                    DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_ReadCard, entrance, null);
                    return;
                }
            }
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station);
        }
        //处理临时卡出场
        private void TempCardExitingHandler(CardInfo card, EntranceReader reader, DateTime eventDateTime, EntranceBase entrance)
        {
            CreateCardExitEvent(card, entrance, eventDateTime);
            entrance.ProcessingEvent.Reader = reader;
            entrance.ProcessingEvent.CarPlate = entrance.Carplate;
            entrance.Carplate = string.Empty;
            if (entrance.ProcessingEvent.CardPaymentInfo.Accounts == 0)
            {
                entrance.CardValid();
                if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station);
            }
            else
            {
                if (entrance.IsTempReader(reader))
                {
                    if (entrance.ProcessingCard.LastPayment == null)
                    {
                        DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.IVN_NotPaid, entrance, null);
                        entrance.TakeOutACard();
                    }
                    else
                    {
                        DenyCard(card.CardID, BusinessModel.Enum.EventInvalidType.INV_OverTime, entrance, null);
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
        private void NestedCardEventHandler(CardInfo card, DateTime eventDateTime, EntranceBase entrance)
        {
            if (entrance.IsExitDevice)
            {
                entrance.ProcessingEvent = CardEventReport.CreateExitEvent(card, entrance.ParkID, entrance.EntranceID,
                    entrance.EntranceName, card.CarType, TariffSetting.Current, eventDateTime);
                entrance.ProcessingEvent.ParkingStatus = card.ParkingStatus | ParkingStatus.NestedParkMarked | ParkingStatus.IndoorIn | ParkingStatus.In;
                entrance.ProcessingEvent.ParkingStatus ^= ParkingStatus.IndoorIn;//出嵌套车场后状态
            }
            else
            {
                entrance.ProcessingEvent = CardEventReport.CreateEnterEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, eventDateTime);
                entrance.ProcessingEvent.ParkingStatus = card.ParkingStatus | ParkingStatus.NestedParkMarked | ParkingStatus.IndoorIn | ParkingStatus.In;
            }
            entrance.ProcessingCard = card;
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station); //无需确认则写数据库
        }
        //处理门禁模式运行时的刷卡事件
        private void ACSCardEventHandler(CardInfo card, DateTime eventDateTime, EntranceBase entrance)
        {
            entrance.ProcessingEvent = CardEventReport.CreateEnterEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName, eventDateTime);
            entrance.ProcessingEvent.IsExitEvent = entrance.EntranceInfo.IsExitDevice;  //生成事件时只用简单的入场事件，不计算停车费用，所以这里要把出口事件改成出口
            entrance.ProcessingCard = card;
            entrance.CardValid();
            if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, entrance.Operator, entrance.Station); //无需确认则写数据库
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
            _Park.Vacant = report.ParkVacant;
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
                    if (!entrance.EntranceInfo.CardValidNeedResponse) PermitCard(entrance, string.Empty, string.Empty);
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
                    DenyCard(notify.CardEvent.CardID, notify.InvalidType, entrance, notify.Balance);
                }
                else if (notify.InvalidType == EventInvalidType.INV_OverDate)
                {
                    DenyCard(notify.CardEvent.CardID, notify.InvalidType, entrance, notify.ExpireDate);
                }
                else
                {
                    DenyCard(notify.CardEvent.CardID, notify.InvalidType, entrance, null);
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
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDetail(entrance.ProcessingEvent.CardID);
                if (card != null)
                {
                    entrance.ProcessingEvent = CardEventReport.CreateExitEvent(card, entrance.ParkID, entrance.EntranceID, entrance.EntranceName,
                        notify.CarType, TariffSetting.Current, entrance.ProcessingEvent.EventDateTime);
                    entrance.CardWait();
                    RaiseCardEventReporting(entrance.ProcessingEvent);
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
                if (string.IsNullOrEmpty(notify.CardID) && !string.IsNullOrEmpty(notify.CarPlate))
                {
                    CardSearchCondition con = new CardSearchCondition();
                    con.CarPlate = notify.CarPlate;
                    List<CardInfo> cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(con).QueryObjects;
                    if (cards != null && cards.Count > 0)
                    {
                        notify.CardID = cards[0].CardID;
                    }
                    else
                    {
                        notify.CardID = notify.CarPlate;
                    }
                }
                CardReadReport report = new CardReadReport();
                report.CardID = notify.CardID;
                report.ParkingData = notify.ParkingData;
                report.ParkID = notify.ParkID;
                report.EntranceID = notify.EntranceID;
                report.EventDateTime = DateTime.Now;
                report.CannotIgnored = true;
                report.Reader = Ralid.Park.BusinessModel.Enum.EntranceReader.DeskTopReader;
                report.LastCarPlate = notify.LastCarPlate;
                entrance.Carplate = notify.CarPlate;
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

        public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType)
        {
            return CardPaymentInfoFactory.CreateCardPaymentRecord(card, TariffSetting.Current, carType, DateTime.Now);
        }

        public CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType, DateTime datetime)
        {
            return CardPaymentInfoFactory.CreateCardPaymentRecord(card, TariffSetting.Current, carType, datetime);
        }
        #endregion

        #endregion
    }
}

