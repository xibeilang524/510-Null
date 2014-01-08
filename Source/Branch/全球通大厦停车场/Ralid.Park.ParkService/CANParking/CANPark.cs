using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.ServiceModel;
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
using Ralid.Park.ParkService;

namespace Ralid.Park.ParkService.CANParking
{
    /// <summary>
    /// 表示在线停车场的通信接口
    /// </summary>
    public sealed class CANPark : ParkBase
    {
        #region 构造函数
        public CANPark(ParkInfo park,ParkBase parent)
            : base(park,parent)
        {
            if (park.CommPort > 0)
            {
                this._commComponent = new CommComponent(park.CommPort);
            }
            if (park.SubParks != null && park.SubParks.Count > 0)
            {
                foreach (ParkInfo subPark in park.SubParks)
                {
                    CANPark sub = new CANPark(subPark, _commComponent,this);
                    AddSubPark(sub);
                }
            }
            foreach (EntranceInfo en in park.Entrances)
            {
                AddEntrance(en,false);
            }
            BeginBackGroudWork();
        }

        public CANPark(ParkInfo park, CommComponent commport,ParkBase parent)
            : base(park,parent)
        {
            _commComponent = commport;
            if (park.SubParks != null && park.SubParks.Count > 0)
            {
                foreach (ParkInfo subPark in park.SubParks)
                {
                    CANPark sub = new CANPark(subPark, _commComponent,this);
                    AddSubPark(sub);
                }
            }
            foreach (EntranceInfo en in park.Entrances)
            {
                AddEntrance(en, false);
            }
            BeginBackGroudWork();
        }
        #endregion 构造方法

        #region 成员变量
        private CommComponent _commComponent;
        private CANEntrance _HostEntrance; //主控制板
        #endregion 成员变量

        #region 私有方法
        private void BeginBackGroudWork()
        {
            Thread t = new Thread(SyncTime_Thread);
            t.IsBackground = true;
            t.Start();
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
        #endregion

        #region 在线模式卡片进出处理
        protected override void OnCommandEchoReporting(CommandEchoReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            if (entrance.ProcessingEvent != null)
            {
                base.OnCommandEchoReporting(report);

                //离线模式时，通过远程读卡处理读卡事件采用的是在线模式处理，所以处理完成后要把卡片的信息同步到硬件中
                //if (AppSettings.CurrentSetting.ParkWorkMode == 0 && entrance.ProcessingCard != null)
                if (WorkMode == ParkWorkMode.OffLine && entrance.ProcessingCard != null)
                {
                    SaveCard(entrance.ProcessingCard, ActionType.Upate);
                }
            }
        }
        #endregion

        #region 离线模式卡片进出处理
        protected override void OnCardWaiting(OfflineCardReadReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardDetail(report.CardID);
            if (card != null)
            {
                //离线模式下，如果硬件记录的时间大于系统记录的时间，表明卡片在软件退出时进出过停车场，所以此时以硬件时间为准
                if (report.LastDateTime != null && report.LastDateTime.Value.Ticks > card.LastDateTime.Ticks) card.LastDateTime = report.LastDateTime.Value;

                if (entrance.IsExitDevice)
                {
                    CreateCardExitEvent(card, entrance, report.EventDateTime);
                    if (card.CardType.IsTempCard || card.CardType.IsPrepayCard) entrance.CardWait(); //储值卡和临时卡重新播放费用
                }
                else
                {
                    CreateCardEnterEvent(card, entrance, report.EventDateTime);
                }
                RaiseCardEventReporting(entrance.ProcessingEvent);
            }
            else
            {
                DenyCard(report.CardID, EventInvalidType.INV_UnRegister, entrance,null);
            }
        }

        protected override void OnCardPermitted(OfflineCardReadReport report)
        {
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance == null) return;
            if (entrance.ProcessingEvent != null && entrance.ProcessingEvent.CardID == report.CardID)
            {
                if (entrance.ProcessingEvent.CardType.IsPrepayCard &&
                    entrance.ProcessingEvent.CardPaymentInfo != null &&
                    entrance.ProcessingEvent.CardPaymentInfo.Accounts > 0 &&
                    entrance.ProcessingCard.Balance >= entrance.ProcessingEvent.CardPaymentInfo.Accounts
                    ) //储值卡扣除余额
                {
                    entrance.ProcessingEvent.CardPaymentInfo.Paid = entrance.ProcessingEvent.CardPaymentInfo.Accounts;
                    entrance.ProcessingEvent.CardPaymentInfo.OperatorID = string.Empty;
                    entrance.ProcessingEvent.CardPaymentInfo.StationID = string.Empty;
                    entrance.ProcessingEvent.CardPaymentInfo.PaymentMode = PaymentMode.Prepay;
                    CardBll cpb = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                    cpb.PayParkFee(entrance.ProcessingEvent.CardPaymentInfo);
                    entrance.ProcessingEvent.Balance = entrance.ProcessingEvent.Balance - entrance.ProcessingEvent.CardPaymentInfo.Accounts;
                    entrance.ProcessingCard.Balance = entrance.ProcessingEvent.Balance;
                }

                CardEventReport cardEvent = entrance.ProcessingEvent.Clone();
                cardEvent.EventStatus = CardEventStatus.Valid;
                cardEvent.OperatorID = string.Empty;
                cardEvent.StationID = string.Empty;
                CardBll cbll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                cbll.SaveCardAndEvent(entrance.ProcessingCard, cardEvent);
                RaiseCardEventReporting(cardEvent);
                entrance.ProcessingEvent = null;
            }
        }
        #endregion

        #region 重写基类方法及属性
        public override EntranceBase Master
        {
            get { return _HostEntrance; }
        }

        public override bool AddEntrance(EntranceInfo info)
        {
            return AddEntrance(info, true);
        }

        public bool AddEntrance(EntranceInfo info, bool syncHardWare)
        {
            if (info.ParkID == this.Park.ParkID)
            {
                foreach (EntranceBase entrance in _Entrances)
                {
                    if (entrance.EntranceID == info.EntranceID) return false;
                }
                CANEntrance ce = new CANEntrance(_commComponent, info, this);
                ListenEntranceEvents(ce);
                if (syncHardWare) ce.SyncToHardware();
                _Entrances.Add(ce);
                if (info.Address == CanAddress.HostEntrance)
                {
                    _HostEntrance = ce;
                }
                return true;
            }
            else
            {
                foreach (CANPark subPark in _SubParks)
                {
                    if (info.ParkID == subPark.Park.ParkID)
                    {
                        return subPark.AddEntrance(info);
                    }
                }
                return false;
            }
        }

        public override bool UpdateEntrance(EntranceInfo info)
        {
            if (info.ParkID == this.Park.ParkID)
            {
                EntranceBase entrance = GetEntrance(info.EntranceID);
                if (entrance != null)
                {
                    entrance.EntranceInfo = info;
                    (entrance as CANEntrance).SyncToHardware();
                    return true;
                }
            }
            else
            {
                foreach (CANPark subPark in _SubParks)
                {
                    if (info.ParkID == subPark.Park.ParkID)
                    {
                        return subPark.UpdateEntrance(info);
                    }
                }
            }
            return false;
        }

        protected override void UpdateVacant(EntranceBase entrance)
        {
            base.UpdateVacant(entrance);
            //if (AppSettings.CurrentSetting.ParkWorkMode == 0) //离线模式把车位数设置到控制板
            if (WorkMode == ParkWorkMode.OffLine) //离线模式把车位数设置到控制板
            {
                (Master as CANEntrance).SetVacant(Park.Vacant);
            }
        }

        public override bool DownloadVacantSetting(CarPortSetting vacantInfo)
        {
            if (vacantInfo.ParkID == Park.ParkID)
            {
                Park.Vacant = vacantInfo.VacantPort;
                Park.VacantText = vacantInfo.VacantText;
                Park.TotalPosition = vacantInfo.CarPortUpLimit;
                Park.MinPosition = vacantInfo.CarPortDownLimit;
                Park.ParkFullText = vacantInfo.ParkFullText;

                if (WorkMode == ParkWorkMode.OffLine)
                {
                    if (Master != null) Master.ApplyCarPortSetting(vacantInfo);
                }
                else
                {
                    //这里下发车位的数量是由于有时在线模式入口取卡时虽然软件上提示还有车位,但硬件提示"临时车位已满",怀疑是在线模式发卡时也获取了硬件上的
                    //车位信息,所以在线模式设置车位时也应下发到硬件
                    if (Master != null) Master.ApplyCarPortSetting(vacantInfo);
                    foreach (EntranceBase en in _Entrances)
                    {
                        if (!en.IsExitDevice && en.EntranceInfo.EnableParkvacantLed) en.ShowVacant();
                    }
                }
                ParkVacantReport report = new ParkVacantReport();
                report.ParkID = Park.ParkID;
                report.ParkVacant = Park.Vacant;
                RaiseParkVacantReporting(report);
                return true;
            }
            else
            {
                foreach (ParkBase sub in _SubParks)
                {
                    bool ret = sub.DownloadVacantSetting(vacantInfo);
                    if (ret) return ret;
                }
            }
            return false;
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
                    entrance.ProcessingCard.Balance = entrance.ProcessingEvent.Balance;
                }
                entrance.CardValid();
                if (!entrance.EntranceInfo.CardValidNeedResponse) //设置成事件有效指令不需要硬件返回
                {
                    PermitCard(entrance, notify.Operator.OperatorName, notify.Station);
                    //离线模式产生此事件
                    //if (AppSettings.CurrentSetting.ParkWorkMode == 0 && entrance.ProcessingCard != null)
                    if (WorkMode == ParkWorkMode.OffLine && entrance.ProcessingCard != null)
                    {
                        SaveCard(entrance.ProcessingCard, ActionType.Upate);
                    }
                }
            }
            return true;
        }

        protected override void OnDeviceResetReporting(DeviceResetReport report)
        {
            //收到控制器复位事件后把车位数显示到屏上
            EntranceBase entrance = GetEntrance(report.EntranceID);
            if (entrance != null)
            {
                if (report.Address == CanAddress.HostEntrance)
                {
                    foreach (EntranceBase en in _Entrances)
                    {
                        if (!en.IsExitDevice) en.ShowVacant();
                    }
                    if (_SubParks != null && _SubParks.Count > 0)
                    {
                        foreach (ParkBase sub in _SubParks)
                        {
                            CANPark subPark = sub as CANPark;
                            foreach (EntranceBase en in subPark._Entrances)
                            {
                                if (!en.IsExitDevice && en.EntranceInfo.EnableParkvacantLed) en.ShowVacant();
                            }
                        }
                    }
                }
                else if (!entrance.IsExitDevice)
                {
                    if (entrance.EntranceInfo.EnableParkvacantLed) entrance.ShowVacant();
                }
            }
            base.OnDeviceResetReporting(report);
        }
        #endregion
    }
}
