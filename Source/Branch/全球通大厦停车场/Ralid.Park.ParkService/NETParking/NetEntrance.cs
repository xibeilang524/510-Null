using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.Hardware;
using Ralid.Park.BLL;

namespace Ralid.Park.ParkService.NETParking
{
    /// <summary>
    /// 表示停车场的一个通道
    /// </summary>
    public class NetEntrance : EntranceBase
    {
        #region 构造函数
        public NetEntrance(EntranceInfo info, ParkBase parent)
            : base(info, parent)
        {
            this.EntranceInfo = info;
            _CommunicationIP = GlobalVariables.CurrentParkingCommunicationIP;
            _CommunicationPort = GlobalVariables.EventListenerPort;

            _ParkDevice = new ParkDevice(GetLANInfoFrom(info));
            SyncToHardware();  //同步参数到硬件
            _ParkDevice.ListenEvents(_CommunicationIP, _CommunicationPort);
            RegisterEventOf(_ParkDevice); //
        }
        #endregion

        #region 私有变量
        private object _StatusLock = new object();
        private ParkDevice _ParkDevice;
        private AutoResetEvent _TempCardReadEvent = new AutoResetEvent(false);
        private DateTime _LastTakeCard = new DateTime(2011, 1, 1);
        private int _RetryCount = 3;//最多重试次数
        private int _RetryTimeout = 1000;//重试等待时间
        private int _RetryShortTimeout = 50;//较短的重试等待时间
        private System.Net.IPAddress _CommunicationIP;//通信IP
        private int _CommunicationPort;//通信端口
        #endregion

        #region 私有方法
        private Ralid.Park.Hardware.WorkmodeInfo GetWorkModeInfoFrom(EntranceInfo info)
        {
            Ralid.Park.Hardware.WorkmodeInfo wi = new Ralid.Park.Hardware.WorkmodeInfo();
            if (Parent.WorkMode == ParkWorkMode.OffLine) wi.WorkmodeOptions |= WorkmodeOptions.IsOffline; //获取是离线还是在线工作方式
            if (!info.IsMaster) wi.WorkmodeOptions |= WorkmodeOptions.NoneMaster;
            if (!info.IsExitDevice) wi.WorkmodeOptions |= WorkmodeOptions.IsEnterDevice;
            if (!info.UseAsAcs) wi.WorkmodeOptions |= WorkmodeOptions.NoneRoadGateModel;
            if (info.ReadAndTakeCardNeedCarSense) wi.WorkmodeOptions |= WorkmodeOptions.TakeCardNeedCarSense;
            if (info.AllowEjectCardWhithoutRead) wi.WorkmodeOptions |= WorkmodeOptions.AllowEjectCardWhithoutRead;
            if (info.LightEnable) wi.WorkmodeOptions |= WorkmodeOptions.LightEnable;
            if (info.ForbidWhenCardExpired) wi.WorkmodeOptions |= WorkmodeOptions.ForbidExitWhenCardExpired;
            if (info.ExportCharge) wi.WorkmodeOptions |= WorkmodeOptions.ExportCharge;
            if (info.ForbidWhenFull) wi.WorkmodeOptions |= WorkmodeOptions.ForbidEnterWhenFull;
            if (!info.DisableTempCard) wi.WorkmodeOptions |= WorkmodeOptions.EnableTempCard;
            if (info.NoParkingCount) wi.WorkmodeOptions |= WorkmodeOptions.NoParkingCount;
            if (info.Valid) wi.WorkmodeOptions |= WorkmodeOptions.Valid;
            wi.CardReadInterval = info.ReadCardInterval;
            return wi;
        }

        private Ralid.Park.Hardware.DeviceInfo GetDeviceInfoFrom(EntranceInfo info)
        {
            Ralid.Park.Hardware.DeviceInfo di = new DeviceInfo();
            di.ID = info.EntranceID;
            di.StrParkNum = (Parent as NETPark).ParkNum;
            di.Address = EntranceNum;
            return di;
        }

        private Ralid.Park.Hardware.LANInfo GetLANInfoFrom(EntranceInfo info)
        {
            Ralid.Park.Hardware.LANInfo li = new Hardware.LANInfo();
            li.IPAddress = string.IsNullOrEmpty(info.IPAddress) ? "0.0.0.0" : info.IPAddress;
            li.IPMask = string.IsNullOrEmpty(info.IPMask) ? "0.0.0.0" : info.IPMask;
            li.GateWay = string.IsNullOrEmpty(info.Gateway) ? "0.0.0.0" : info.Gateway;
            li.MasterIP = string.IsNullOrEmpty(info.MasterIP) ? "0.0.0.0" : info.MasterIP;
            li.MACAddress = info.MACAddress;
            li.ControlPort = info.ControlPort;
            li.EventPort = info.EventPort;
            //这两个属性要与事件侦听器的一致，设置到硬件时硬件的事件才会发到本机上来
            if (_ParkDevice != null)
            {
                li.EventListenerIP = EventListener.GetSingleton(_CommunicationIP, _CommunicationPort).LocalIP;
                li.EventListenerPort = EventListener.GetSingleton(_CommunicationIP, _CommunicationPort).LocalPort;
            }
            return li;
        }

        private Ralid.Park.Hardware.CardType ConvertCardType(Ralid.Park.BusinessModel.Enum.CardType cardType)
        {
            Ralid.Park.BusinessModel.Enum.CardType baseCardType = Ralid.Park.BusinessModel.Enum.CardType.GetBaseCardType(cardType);
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.VipCard)
            {
                return Hardware.CardType.FreeCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.OwnerCard)
            {
                return Hardware.CardType.OwnerCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard)
            {
                return Hardware.CardType.MonthCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.PrePayCard)
            {
                return Hardware.CardType.PrePayCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.TempCard)
            {
                return Hardware.CardType.TempCard;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1)
            {
                return Hardware.CardType.UserDefinedCard1;
            }
            else if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2)
            {
                return Hardware.CardType.UserDefinedCard2;
            }
            else
            {
                return Hardware.CardType.TempCard;
            }
        }

        private Ralid.Park.BusinessModel.Enum.CardType ConvertCardType(Ralid.Park.Hardware.CardType cardType)
        {
            if (cardType == Hardware.CardType.FreeCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.VipCard;
            }
            else if (cardType == Hardware.CardType.OwnerCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.OwnerCard;
            }
            else if (cardType == Hardware.CardType.MonthCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard;
            }
            else if (cardType == Hardware.CardType.PrePayCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.PrePayCard;
            }
            else if (cardType == Hardware.CardType.TempCard)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.TempCard;
            }
            else if (cardType == Hardware.CardType.UserDefinedCard1)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1;
            }
            else if (cardType == Hardware.CardType.UserDefinedCard2)
            {
                return Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2;
            }
            else
            {
                return Ralid.Park.BusinessModel.Enum.CardType.TempCard;
            }
        }

        private Ralid.Park.Hardware.EventInvalidType ConvertInvalidType(Ralid.Park.BusinessModel.Enum.EventInvalidType reason)
        {
            switch (reason)
            {
                case BusinessModel.Enum.EventInvalidType.INV_CarPlateWrong:
                    return Hardware.EventInvalidType.INV_CarPlateWrong;
                case BusinessModel.Enum.EventInvalidType.INV_CarPlateWrongWithPaid:
                    return Hardware.EventInvalidType.INV_CarPlateWrongWithPaid;
                case BusinessModel.Enum.EventInvalidType.INV_DisableNestedPark:
                    return Hardware.EventInvalidType.INV_DisableNestedPark;
                case BusinessModel.Enum.EventInvalidType.INV_ForbidTempCard:
                    return Hardware.EventInvalidType.INV_ForbidTempCard;
                case BusinessModel.Enum.EventInvalidType.INV_HaveIn:
                    return Hardware.EventInvalidType.INV_HaveIn;
                case BusinessModel.Enum.EventInvalidType.INV_Invalid:
                    return Hardware.EventInvalidType.INV_Invalid;
                case BusinessModel.Enum.EventInvalidType.INV_OverDate:
                    return Hardware.EventInvalidType.INV_OverDate;
                case BusinessModel.Enum.EventInvalidType.INV_OverTime:
                    return Hardware.EventInvalidType.INV_OverTime;
                case BusinessModel.Enum.EventInvalidType.INV_ParkFull:
                    return Hardware.EventInvalidType.INV_ParkFull;
                case BusinessModel.Enum.EventInvalidType.INV_StillOut:
                    return Hardware.EventInvalidType.INV_StillOut;
                case BusinessModel.Enum.EventInvalidType.INV_Type:
                    return Hardware.EventInvalidType.INV_Type;
                case BusinessModel.Enum.EventInvalidType.IVN_NotPaid:
                    return Hardware.EventInvalidType.IVN_NotPaid;
                case BusinessModel.Enum.EventInvalidType.INV_HolidayDisabled:
                    return Hardware.EventInvalidType.INV_HolidayDisabled;
                case BusinessModel.Enum.EventInvalidType.INV_Lock:
                    return Hardware.EventInvalidType.INV_Lock;
                case BusinessModel.Enum.EventInvalidType.INV_Loss:
                    return Hardware.EventInvalidType.INV_Loss;
                case BusinessModel.Enum.EventInvalidType.INV_NoAccessRight:
                    return Hardware.EventInvalidType.INV_NoAccessRight;
                case BusinessModel.Enum.EventInvalidType.INV_NotActive:
                    return Hardware.EventInvalidType.INV_NotActive;
                case BusinessModel.Enum.EventInvalidType.INV_UnRegister:
                    return Hardware.EventInvalidType.INV_UnRegister;
                case BusinessModel.Enum.EventInvalidType.INV_WrongInTime:
                    return Hardware.EventInvalidType.INV_WrongInTime;
                case BusinessModel.Enum.EventInvalidType.INV_Balance:
                    return Hardware.EventInvalidType.INV_Balance;
                case BusinessModel.Enum.EventInvalidType.INV_Recycled:
                    return Hardware.EventInvalidType.INV_Recycled;
                case BusinessModel.Enum.EventInvalidType.INV_ParkNumError:
                    return Hardware.EventInvalidType.INV_ParkNumError;
                case BusinessModel.Enum.EventInvalidType.INV_DataError:
                    return Hardware.EventInvalidType.INV_DataError;
                case BusinessModel.Enum.EventInvalidType.INV_VersionError:
                    return Hardware.EventInvalidType.INV_VersionError;
                case BusinessModel.Enum.EventInvalidType.INV_NoCarType:
                    return Hardware.EventInvalidType.INV_NoCarType;
                case BusinessModel.Enum.EventInvalidType.INV_NoTariff:
                    return Hardware.EventInvalidType.INV_NoTariff;
                case BusinessModel.Enum.EventInvalidType.INV_WrongPaidTime:
                    return Hardware.EventInvalidType.INV_WrongPaidTime;
                case BusinessModel.Enum.EventInvalidType.INV_InsertToRecovery:
                    return Hardware.EventInvalidType.INV_InsertToRecovery;
                case BusinessModel.Enum.EventInvalidType.INV_Cancelled:
                    return Hardware.EventInvalidType.INV_Cancelled;
                case BusinessModel.Enum.EventInvalidType.INV_InvalidImg:
                    return Hardware.EventInvalidType.INV_UnKown;
                default:
                    return Hardware.EventInvalidType.INV_Invalid;
            }
        }

        private Ralid.Park.BusinessModel.Enum.EventInvalidType ConvertInvalidType(Ralid.Park.Hardware.EventInvalidType reason)
        {
            switch (reason)
            {
                case Hardware.EventInvalidType.INV_CarPlateWrong:
                    return BusinessModel.Enum.EventInvalidType.INV_CarPlateWrong;
                case Hardware.EventInvalidType.INV_CarPlateWrongWithPaid:
                    return BusinessModel.Enum.EventInvalidType.INV_CarPlateWrongWithPaid;
                case Hardware.EventInvalidType.INV_DisableNestedPark:
                    return BusinessModel.Enum.EventInvalidType.INV_DisableNestedPark;
                case Hardware.EventInvalidType.INV_ForbidTempCard:
                    return BusinessModel.Enum.EventInvalidType.INV_ForbidTempCard;
                case Hardware.EventInvalidType.INV_HaveIn:
                    return BusinessModel.Enum.EventInvalidType.INV_HaveIn;
                case Hardware.EventInvalidType.INV_Invalid:
                    return BusinessModel.Enum.EventInvalidType.INV_Invalid;
                case Hardware.EventInvalidType.INV_OverDate:
                    return BusinessModel.Enum.EventInvalidType.INV_OverDate;
                case Hardware.EventInvalidType.INV_OverTime:
                    return BusinessModel.Enum.EventInvalidType.INV_OverTime;
                case Hardware.EventInvalidType.INV_ParkFull:
                    return BusinessModel.Enum.EventInvalidType.INV_ParkFull;
                case Hardware.EventInvalidType.INV_StillOut:
                    return BusinessModel.Enum.EventInvalidType.INV_StillOut;
                case Hardware.EventInvalidType.INV_Type:
                    return BusinessModel.Enum.EventInvalidType.INV_Type;
                case Hardware.EventInvalidType.IVN_NotPaid:
                    return BusinessModel.Enum.EventInvalidType.IVN_NotPaid;
                case Hardware.EventInvalidType.INV_HolidayDisabled:
                    return BusinessModel.Enum.EventInvalidType.INV_HolidayDisabled;
                case Hardware.EventInvalidType.INV_Lock:
                    return BusinessModel.Enum.EventInvalidType.INV_Lock;
                case Hardware.EventInvalidType.INV_Loss:
                    return BusinessModel.Enum.EventInvalidType.INV_Loss;
                case Hardware.EventInvalidType.INV_NoAccessRight:
                    return BusinessModel.Enum.EventInvalidType.INV_NoAccessRight;
                case Hardware.EventInvalidType.INV_NotActive:
                    return BusinessModel.Enum.EventInvalidType.INV_NotActive;
                case Hardware.EventInvalidType.INV_UnRegister:
                    return BusinessModel.Enum.EventInvalidType.INV_UnRegister;
                case Hardware.EventInvalidType.INV_WrongInTime:
                    return BusinessModel.Enum.EventInvalidType.INV_WrongInTime;
                case Hardware.EventInvalidType.INV_Balance:
                    return BusinessModel.Enum.EventInvalidType.INV_Balance;
                case Hardware.EventInvalidType.INV_Recycled:
                    return BusinessModel.Enum.EventInvalidType.INV_Recycled;
                case Hardware.EventInvalidType.INV_ParkNumError:
                    return BusinessModel.Enum.EventInvalidType.INV_ParkNumError;
                case Hardware.EventInvalidType.INV_DataError:
                    return BusinessModel.Enum.EventInvalidType.INV_DataError;
                case Hardware.EventInvalidType.INV_VersionError:
                    return BusinessModel.Enum.EventInvalidType.INV_VersionError;
                case Hardware.EventInvalidType.INV_NoCarType:
                    return BusinessModel.Enum.EventInvalidType.INV_NoCarType;
                case Hardware.EventInvalidType.INV_NoTariff:
                    return BusinessModel.Enum.EventInvalidType.INV_NoTariff;
                case Hardware.EventInvalidType.INV_WrongPaidTime:
                    return BusinessModel.Enum.EventInvalidType.INV_WrongPaidTime;
                case Hardware.EventInvalidType.INV_InsertToRecovery:
                    return BusinessModel.Enum.EventInvalidType.INV_InsertToRecovery;
                case Hardware.EventInvalidType.INV_Cancelled:
                    return BusinessModel.Enum.EventInvalidType.INV_Cancelled;
                default:
                    return BusinessModel.Enum.EventInvalidType.INV_Invalid;
            }
        }

        private Ralid.Park.Hardware.CardOptions ConvertCardOptions(Ralid.Park.BusinessModel.Enum.CardOptions options)
        {
            Ralid.Park.Hardware.CardOptions ops = 0;
            if ((options & BusinessModel.Enum.CardOptions.OfflineHandleWhenOfflineMode) == BusinessModel.Enum.CardOptions.OfflineHandleWhenOfflineMode) ops |= Hardware.CardOptions.OfflineHandleWhenOfflineMode;
            ops |= (Ralid.Park.Hardware.CardOptions)0x02;
            if ((options & BusinessModel.Enum.CardOptions.ForbidRepeatIn) == BusinessModel.Enum.CardOptions.ForbidRepeatIn) ops |= Hardware.CardOptions.ForbidRepeatIn;
            if ((options & BusinessModel.Enum.CardOptions.ForbidRepeatOut) == BusinessModel.Enum.CardOptions.ForbidRepeatOut) ops |= Hardware.CardOptions.ForbidRepeatOut;
            if ((options & BusinessModel.Enum.CardOptions.WithCount) == BusinessModel.Enum.CardOptions.WithCount) ops |= Hardware.CardOptions.WithCount;
            if ((options & BusinessModel.Enum.CardOptions.ForbidWhenFull) == BusinessModel.Enum.CardOptions.ForbidWhenFull) ops |= Hardware.CardOptions.ForbidWhenFull;
            if ((options & BusinessModel.Enum.CardOptions.HolidayEnable) == BusinessModel.Enum.CardOptions.HolidayEnable) ops |= Hardware.CardOptions.HolidayEnabled;
            if ((options & BusinessModel.Enum.CardOptions.ForbidWhenExpired) == BusinessModel.Enum.CardOptions.ForbidWhenExpired) ops |= Hardware.CardOptions.ForbidWhenExpired;
            return ops;
        }

        private Ralid.Park.Hardware.ParkingStatus ConvertParkingStatus(Ralid.Park.BusinessModel.Enum.ParkingStatus status)
        {
            if (status == BusinessModel.Enum.ParkingStatus.Out || status == BusinessModel.Enum.ParkingStatus.RepeatOut)
            {
                return Hardware.ParkingStatus.None;
            }
            return Hardware.ParkingStatus.InPark;
        }

        private Ralid.Park.Hardware.AccessGroup GetAccessGroupFrom(Ralid.Park.BusinessModel.Model.AccessSetting accessSetting,int entranceID)
        {
            Ralid.Park.Hardware.AccessGroup accessGroup = new AccessGroup();

            List<AccessInfo> accesses = accessSetting.GetAccesses(entranceID);
            accessGroup.Status = AccessGroupStatus.Invalid;
            if (accesses != null && accesses.Count > 0&& accesses.Count <= 8)
            {
                accessGroup.Status = 0;
                for (int i = 0; i < accesses.Count; i++)
                {
                    accessGroup.AccessIDS[i] = accesses[i].ID;
                    foreach (AccessTimeZone zone in accesses[i].AccessTimeZones)
                    {
                        if (zone.AccessEntrances.Any(item => item == entranceID))
                        {
                            H_AccessTimeZone h_zone = new H_AccessTimeZone();
                            h_zone.IncludeHoliday = zone.IncludeHoliday;
                            h_zone.BeginTime = new H_TimeEntity(zone.BeginTime.Hour, zone.BeginTime.Minute);
                            h_zone.EndTime = new H_TimeEntity(zone.EndTime.Hour, zone.EndTime.Minute);
                            accessGroup.AddAccessTimeZones((byte)i, h_zone);
                        }
                    }
                }                
            }
            return accessGroup;
        }

        private Ralid.Park.Hardware.H_WeekProperty GetWeekFrom(Ralid.Park.BusinessModel.Model.HolidaySetting holidaySetting)
        {
            H_WeekProperty week = H_WeekProperty.MondaIsWorkDay
                | H_WeekProperty.TuesdayIsWorkDay
                | H_WeekProperty.WednesdayIsWorkDay
                | H_WeekProperty.ThursdayIsWorkDay
                | H_WeekProperty.FridayIsWorkDay;
            if (!holidaySetting.SaturdayIsRest) week |= H_WeekProperty.SaturdayWorkDay;
            if (!holidaySetting.SundayIsRest) week |= H_WeekProperty.SundayIsWorkDay;
            return week;            
        }

        private List<Ralid.Park.Hardware.H_DateInterval> GetDateIntervalsFrom(Ralid.Park.BusinessModel.Model.HolidaySetting holidaySetting)
        {
            List<H_DateInterval> dateIntervals = new List<H_DateInterval>();
            foreach (HolidayInfo holiday in holidaySetting.Holidays)
            {
                H_DateInterval dateInterval = new H_DateInterval();
                dateInterval.BeginDate = new H_DateEntity(holiday.StartDate);
                dateInterval.EndDate = new H_DateEntity(holiday.EndDate);
                if (dateIntervals.Count < 24)
                {
                    dateIntervals.Add(dateInterval);
                }
                else
                {
                    break;
                }
                foreach (DatetimeInterval di in holiday.WeekenToWorkDayInterval)
                {
                    H_DateInterval h_di = new H_DateInterval();
                    h_di.Status = H_DateIntervalStatus.IsWorkDay;
                    h_di.BeginDate = new H_DateEntity(di.Begin);
                    h_di.EndDate = new H_DateEntity(di.End);
                    if (dateIntervals.Count < 24)
                    {
                        dateIntervals.Add(h_di);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return dateIntervals;

        }

        private Ralid.Park.Hardware.H_TariffType ConvertTariffType(Ralid.Park.BusinessModel.Enum.TariffType tariffType)
        {
            if (tariffType == TariffType.InnerRoom || tariffType == TariffType.HolidayAndInnerRoom)//室内费率
            {
                return H_TariffType.InDoorTariff;
            }
            else
            {
                return H_TariffType.Tariff;
            }

        }

        private Ralid.Park.Hardware.H_Tariff_CardType ConvertTariffCardType(byte cardType)
        {
            byte baseCardType = (byte)(cardType & 0x0F);
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.OwnerCard.ID)
            {
                return H_Tariff_CardType.OwnerCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard.ID)
            {
                return H_Tariff_CardType.MonthCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.PrePayCard.ID)
            {
                return H_Tariff_CardType.PrePayCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.TempCard.ID)
            {
                return H_Tariff_CardType.TempCard;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1.ID)
            {
                return H_Tariff_CardType.UserDefinedCard1;
            }
            if (baseCardType == Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2.ID)
            {
                return H_Tariff_CardType.UserDefinedCard2;
            }

            return H_Tariff_CardType.OwnerCard;
        }

        private Ralid.Park.Hardware.H_Tariff_CarType ConvertTariffCarType(byte carType, TariffType tariffType)
        {

            if (tariffType == TariffType.Holiday || tariffType == TariffType.HolidayAndInnerRoom)//节假日费率
            {

                return (H_Tariff_CarType)(carType + 4);//4~7为车型节假日费率
            }
            else
            {
                return (H_Tariff_CarType)carType;//0~3为车型工作日费率
            }
        }

        private Ralid.Park.Hardware.H_TariffInfo ConvertTariffInfo(short freeTimeAfterPay, Ralid.Park.BusinessModel.Model.TariffBase tariff)
        {
            H_TariffInfo h_Tariff = new H_TariffInfo();
            h_Tariff.TariffType = ConvertTariffType(tariff.TariffType);
            h_Tariff.CardType = ConvertTariffCardType(tariff.CardType);
            h_Tariff.CarType = ConvertTariffCarType(tariff.CarType, tariff.TariffType);
            h_Tariff.T2 = freeTimeAfterPay;

            if (tariff is TariffPerTime)//按次收费
            {
                TariffPerTime t = tariff as TariffPerTime;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode1;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid
                    | H_Tariff_ChargeProperty.DailyLimitInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid
                    | H_Tariff_ChargeProperty.MaximumAmountInvalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M1[0] = (int)(t.FeePerTime * 100);
            }

            if (tariff is TariffPerDay)//按天收费
            {
                TariffPerDay t = tariff as TariffPerDay;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode2;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.DailyLimitInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M1[0] = (int)(t.FeePerDay * 100);

                if (t.OverDay > 0)
                {
                    h_Tariff.T3 = t.OverDay;
                    h_Tariff.M2 = (int)(t.FeePerOverDay * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.OverTimeInvalid;
                }

                if (t.FeeOfMax > 0)
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfTurning)//过点收费
            {
                TariffOfTurning t = tariff as TariffOfTurning;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode1;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.DailyLimitInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M1[0] = (int)(t.FirstFee * 100);
                h_Tariff.T3 = (short)(Ralid.GeneralLibrary.BCDConverter.IntToBCD(t.Turning.Hour) * 0x100 + Ralid.GeneralLibrary.BCDConverter.IntToBCD(t.Turning.Minute));
                h_Tariff.M2 = (int)(t.FeeOfTurning * 100);

                if (t.FeeOfMax > 0)
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfLimitation)//限额收费
            {
                TariffOfLimitation t = tariff as TariffOfLimitation;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode4;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid
                    | H_Tariff_ChargeProperty.IntervalTop2Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval1Invalid
                    | H_Tariff_ChargeProperty.Mode3Interval2Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                if (t.FirstCharge != null)
                {
                    h_Tariff.T4 = t.FirstCharge.Minutes;
                    h_Tariff.T5[0] = t.FirstCharge.Minutes;
                    h_Tariff.M1[0] = (int)(t.FirstCharge.Fee * 100);
                }
                h_Tariff.T6[0] = t.RegularCharge.Minutes;
                h_Tariff.M3[0] = (int)(t.RegularCharge.Fee * 100);
                if (t.FeeOf12Hour > 0)
                {
                    h_Tariff.T7 = 12 * 60;//12小时限额
                    h_Tariff.M5 = (int)(t.FeeOf12Hour * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.IntervalTop1Invalid;
                }
                if (t.FeeOf24Hour > 0)
                {
                    h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.DailyLimitInvalid;
                }
                if (t.FeeOfMax > 0)//封顶收费，最高收费
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfGuanZhou)//日夜差异收费
            {
                TariffOfGuanZhou t = tariff as TariffOfGuanZhou;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode3;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额

                //白天时段
                h_Tariff.TimeInterval[0] = new H_TimeInterval();
                h_Tariff.TimeInterval[0].BeginTime = new H_TimeEntity(t.DayTimezone.Beginning.Hour, t.DayTimezone.Beginning.Minute);
                h_Tariff.TimeInterval[0].EndTime = new H_TimeEntity(t.DayTimezone.Ending.Hour, t.DayTimezone.Ending.Minute);
                h_Tariff.T6[0] = t.DayTimezone.RegularCharge.Minutes;
                h_Tariff.M3[0] = (int)(t.DayTimezone.RegularCharge.Fee * 100);
                if (t.DayTimezone.LimiteFee.HasValue && t.DayTimezone.LimiteFee.Value > 0)//白天时段有最高限额
                {
                    h_Tariff.M4[0] = (int)(t.DayTimezone.LimiteFee.Value * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.IntervalTop1Invalid;
                }

                //夜间时段
                h_Tariff.TimeInterval[1] = new H_TimeInterval();
                h_Tariff.TimeInterval[1].BeginTime = new H_TimeEntity(t.NightTimezone.Beginning.Hour, t.NightTimezone.Beginning.Minute);
                h_Tariff.TimeInterval[1].EndTime = new H_TimeEntity(t.NightTimezone.Ending.Hour, t.NightTimezone.Ending.Minute);
                h_Tariff.T6[1] = t.NightTimezone.RegularCharge.Minutes;
                h_Tariff.M3[1] = (int)(t.NightTimezone.RegularCharge.Fee * 100);
                if (t.NightTimezone.LimiteFee.HasValue && t.NightTimezone.LimiteFee.Value > 0)//夜间有最高限额
                {
                    h_Tariff.M4[1] = (int)(t.NightTimezone.LimiteFee.Value * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.IntervalTop2Invalid;
                }

                if (t.FeeOf24Hour > 0)
                {
                    h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.DailyLimitInvalid;
                }

                if (t.FeeOfMax > 0)//封顶收费，最高收费
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            if (tariff is TariffOfDixiakongjian)//时段限额收费
            {
                TariffOfDixiakongjian t = tariff as TariffOfDixiakongjian;
                h_Tariff.ChargeType = H_Tariff_ChargeType.Mode3;
                h_Tariff.ChargeProperty = H_Tariff_ChargeProperty.OverTimeInvalid
                    | H_Tariff_ChargeProperty.IntervalTop1Invalid;
                h_Tariff.T1 = t.FreeMinutes;
                h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额

                //正常时段
                h_Tariff.TimeInterval[0] = new H_TimeInterval();
                h_Tariff.TimeInterval[0].BeginTime = new H_TimeEntity(t.LimitationTimezone.Ending.Hour, t.LimitationTimezone.Ending.Minute);//以限价时段结束时间为开始时间
                h_Tariff.TimeInterval[0].EndTime = new H_TimeEntity(t.LimitationTimezone.Beginning.Hour, t.LimitationTimezone.Beginning.Minute);//以限价时段开始时间为结束时间
                h_Tariff.T4 = (short)t.FirstMinutes;
                h_Tariff.T5[0] = t.FirstFee.Minutes;
                h_Tariff.M1[0] = (int)(t.FirstFee.Fee * 100);
                h_Tariff.T6[0] = t.RegularFee.Minutes;
                h_Tariff.M3[0] = (int)(t.RegularFee.Fee * 100);

                //限价时段
                h_Tariff.TimeInterval[1] = new H_TimeInterval();
                h_Tariff.TimeInterval[1].BeginTime = new H_TimeEntity(t.LimitationTimezone.Beginning.Hour, t.LimitationTimezone.Beginning.Minute);
                h_Tariff.TimeInterval[1].EndTime = new H_TimeEntity(t.LimitationTimezone.Ending.Hour, t.LimitationTimezone.Ending.Minute);
                h_Tariff.T5[1] = t.LimitationRegularFee.Minutes;
                h_Tariff.M1[1] = (int)(t.LimitationRegularFee.Fee * 100);
                h_Tariff.T6[1] = t.LimitationRegularFee.Minutes;
                h_Tariff.M3[1] = (int)(t.LimitationRegularFee.Fee * 100);
                if (t.Limitation > 0)
                {
                    h_Tariff.M4[1] = (int)(t.Limitation * 100);
                }
                else
                {

                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.IntervalTop2Invalid;
                }

                if (t.FeeOf24Hour > 0)
                {
                    h_Tariff.M6 = (int)(t.FeeOf24Hour * 100);//24小时限额
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.DailyLimitInvalid;
                }
                if (t.FeeOfMax > 0)//封顶收费，最高收费
                {
                    h_Tariff.M7 = (int)(t.FeeOfMax * 100);
                }
                else
                {
                    h_Tariff.ChargeProperty |= H_Tariff_ChargeProperty.MaximumAmountInvalid;
                }
            }

            return h_Tariff;
        }

        private List<Ralid.Park.Hardware.H_TariffSetting> GetCardTypeTariffSettingFrom(Ralid.Park.BusinessModel.Model.TariffSetting tariffSetting, byte cardType)
        {
            List<H_TariffSetting> h_TariffSettings = new List<H_TariffSetting>();
            H_TariffSetting h_TariffSetting1 = new H_TariffSetting();//正常费率
            H_TariffSetting h_TariffSetting2 = new H_TariffSetting();//室内费率
            h_TariffSetting1.CardType = ConvertTariffCardType(cardType);
            h_TariffSetting2.CardType = ConvertTariffCardType(cardType);
            h_TariffSetting1.TariffType = H_TariffType.Tariff;
            h_TariffSetting2.TariffType = H_TariffType.InDoorTariff;

            List<TariffBase> tariffs = tariffSetting.GetBaseCarTypeTariffs(cardType);
            if (tariffs != null && tariffs.Count > 0)
            {
                foreach (TariffBase tariff in tariffs)
                {
                    H_TariffInfo h_Tariff = ConvertTariffInfo((short)tariffSetting.TariffOption.FreeTimeAfterPay, tariff);
                    if (h_Tariff.TariffType == H_TariffType.Tariff)
                    {
                        h_TariffSetting1.AddTariff(h_Tariff.CarType, h_Tariff);
                    }
                    else
                    {
                        h_TariffSetting2.AddTariff(h_Tariff.CarType, h_Tariff);
                    }

                }
            }
            h_TariffSettings.Add(h_TariffSetting1);
            h_TariffSettings.Add(h_TariffSetting2);

            return h_TariffSettings;
        }

        private List<Ralid.Park.Hardware.H_TariffSetting> GetTariffSettingFrom(Ralid.Park.BusinessModel.Model.TariffSetting tariffSetting)
        {
            List<H_TariffSetting> h_TariffSettings = new List<H_TariffSetting>();

            //业主卡
            List<H_TariffSetting> tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.OwnerCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //月租卡
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //储值卡
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.PrePayCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //临时卡
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.TempCard.ID);
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //自定义卡片1
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard1.GetFirstCardTypeFromBase.ID);//只下载第一个的费率
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }

            //自定义卡片2
            tariffs = GetCardTypeTariffSettingFrom(tariffSetting, Ralid.Park.BusinessModel.Enum.CardType.UserDefinedCard2.GetFirstCardTypeFromBase.ID);//只下载第一个的费率
            if (tariffs != null && tariffs.Count > 0)
            {
                h_TariffSettings.AddRange(tariffs);
            }


            return h_TariffSettings;
        }
        #endregion

        #region 控制板底层上传事件处理程序
        private void RegisterEventOf(ParkDevice device)
        {
            device.DeviceReset -= new DeviceEventHandler(ParkDevice_DeviceReset);
            device.DeviceReset += new DeviceEventHandler(ParkDevice_DeviceReset);

            device.CaptureACard -= new DeviceEventHandler(ParkDevice_CaptureACard);
            device.CaptureACard += new DeviceEventHandler(ParkDevice_CaptureACard);

            device.TakeACard -= new DeviceEventHandler(ParkDevice_TakeACard);
            device.TakeACard += new DeviceEventHandler(ParkDevice_TakeACard);

            device.CardIgnored -= new DeviceEventHandler(ParkDevice_CardIgnored);
            device.CardIgnored += new DeviceEventHandler(ParkDevice_CardIgnored);

            device.CardButtonClick -= new CardButtonEventHandler(ParkDevice_CardButtonClick);
            device.CardButtonClick += new CardButtonEventHandler(ParkDevice_CardButtonClick);

            device.CardReading -= new Hardware.CardReadEventHandler(ParkDevice_CardReading);
            device.CardReading += new Hardware.CardReadEventHandler(ParkDevice_CardReading);

            device.CardWaiting -= new Hardware.CardReadEventHandler(ParkDevice_CardWaiting);
            device.CardWaiting += new Hardware.CardReadEventHandler(ParkDevice_CardWaiting);

            device.CardDenied -= new CardDeniedEventHandler(ParkDevice_CardDenied);
            device.CardDenied += new CardDeniedEventHandler(ParkDevice_CardDenied);

            device.CardPermitted -= new Hardware.CardReadEventHandler(ParkDevice_CardPermitted);
            device.CardPermitted += new Hardware.CardReadEventHandler(ParkDevice_CardPermitted);

            device.CarSense -= new CarSenseHandler(ParkDevice_CarSense);
            device.CarSense += new CarSenseHandler(ParkDevice_CarSense);

            device.GateDown -= new DeviceEventHandler(ParkDevice_GateDown);
            device.GateDown += new DeviceEventHandler(ParkDevice_GateDown);

            device.GateUp -= new DeviceEventHandler(ParkDevice_GateUp);
            device.GateUp += new DeviceEventHandler(ParkDevice_GateUp);

            device.HeartBeat -= new HeartBeatEventHandler(NetEntrance_HeartBeat);
            device.HeartBeat += new HeartBeatEventHandler(NetEntrance_HeartBeat);
        }

        private void ParkDevice_DeviceReset(object sender, DeviceEventArgs e)
        {
            DeviceResetReport report = new DeviceResetReport(Park.ParkID, EntranceID, e.EventDateTime, EntranceName);
            AddToReportPool(report);
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CarSense(object sender, CarSenseEventArgs e)
        {
            CarSenseReport report = new CarSenseReport(Park.ParkID, EntranceID, e.EventDateTime,
                EntranceName, (e.CarSense == CarSenseState.CarArrive ? 1 : 0));
            AddToReportPool(report);
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CardButtonClick(object sender, CardButtonEventArgs e)
        {
            ButtonClickedReport report = new ButtonClickedReport()
            {
                ParkID = ParkID,
                EntranceID = EntranceID,
                SourceName = EntranceName,
                Button = e.CardButton == CardButton.Button1 ? 1 : 2,
                EventDateTime = e.EventDateTime
            };
            AddToReportPool(report);
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CardReading(object sender, Hardware.CardReadEventArgs e)
        {
            if (Parent.WorkMode == ParkWorkMode.OffLine)
            {
                //写卡模式时忽略控制器的脱机处理的读卡通知
                if (!e.IsOfflineHandle)
                {
                    //按在线模式处理
                    CardReadReport report = new CardReadReport()
                    {
                        ParkID = ParkID,
                        EntranceID = EntranceID,
                        SourceName = EntranceName,
                        EventDateTime = e.EventDateTime,
                        CardID = e.CardID,
                        CarPlateComparisonResult = (Ralid.Park.BusinessModel.Enum.CarPlateComparisonResult)e.CarPlateComparisonResult,
                        CarPlate = e.CarPlate,
                        LastCarPlate = e.LastCarPlate,
                        Reader = (Ralid.Park.BusinessModel.Enum.EntranceReader)e.Reader,
                    };
                    AddToReportPool(report);
                }
                else if (e.Reason != Hardware.EventInvalidType.INV_Success)
                {
                    CardInvalidEventReport report = new CardInvalidEventReport();
                    report.ParkID = Park.ParkID;
                    report.EntranceID = EntranceID;
                    report.CardID = e.CardID;
                    report.SourceName = EntranceName;
                    report.InvalidType = ConvertInvalidType(e.Reason);
                    report.EventDateTime = e.EventDateTime;
                    AddToReportPool(report);
                }
                else if (!e.IsOpenGateWhenCarPlateComparisonFail
                    && e.CarPlateComparisonResult == Hardware.CarPlateComparisonResult.Fail)
                {
                    //错误代码为成功,不需要缴费，车牌对比失败，失败后不会抬闸时，需要上位机去确认抬闸
                    OfflineCardReadReport report = new OfflineCardReadReport()
                    {
                        ParkID = ParkID,
                        EntranceID = EntranceID,
                        SourceName = EntranceName,
                        EventDateTime = e.EventDateTime,
                        CardID = e.CardID,
                        EventStatus = CardEventStatus.CarPlateFail,
                        CarPlateComparisonResult = (Ralid.Park.BusinessModel.Enum.CarPlateComparisonResult)e.CarPlateComparisonResult,
                        CarPlate = e.CarPlate,
                        LastCarPlate = e.LastCarPlate,
                        Reader = (Ralid.Park.BusinessModel.Enum.EntranceReader)e.Reader,
                        LastDateTime = e.LastDateTime,
                    };
                    AddToReportPool(report);
                }
                else
                {
                    OfflineCardReadReport report = new OfflineCardReadReport()
                    {
                        ParkID = ParkID,
                        EntranceID = EntranceID,
                        SourceName = EntranceName,
                        EventDateTime = e.EventDateTime,
                        CardID = e.CardID,
                        EventStatus = CardEventStatus.Pending,
                        CarPlateComparisonResult = (Ralid.Park.BusinessModel.Enum.CarPlateComparisonResult)e.CarPlateComparisonResult,
                        CarPlate = e.CarPlate,
                        LastCarPlate = e.LastCarPlate,
                        Reader = (Ralid.Park.BusinessModel.Enum.EntranceReader)e.Reader,
                        LastDateTime = e.LastDateTime,
                    };
                    AddToReportPool(report);
                }
            }
            else
            {
                //在线模式时产生读卡事件
                CardReadReport report = new CardReadReport()
                {
                    ParkID = ParkID,
                    EntranceID = EntranceID,
                    SourceName = EntranceName,
                    EventDateTime = e.EventDateTime,
                    CardID = e.CardID,
                    CarPlateComparisonResult = (Ralid.Park.BusinessModel.Enum.CarPlateComparisonResult)e.CarPlateComparisonResult,
                    CarPlate = e.CarPlate,
                    LastCarPlate = e.LastCarPlate,
                    Reader = (Ralid.Park.BusinessModel.Enum.EntranceReader)e.Reader,
                };
                if (IsTempReader(report.Reader))
                {
                    _TempCardReadEvent.Set();
                }
                AddToReportPool(report);
            }
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CardWaiting(object sender, Hardware.CardReadEventArgs e)
        {
            OfflineCardReadReport report = new OfflineCardReadReport()
            {
                ParkID = ParkID,
                EntranceID = EntranceID,
                SourceName = EntranceName,
                EventDateTime = e.EventDateTime,
                CardID = e.CardID,
                EventStatus = CardEventStatus.Pending,
                Reader = (Ralid.Park.BusinessModel.Enum.EntranceReader)e.Reader,
                LastDateTime = e.LastDateTime,
            };
            AddToReportPool(report);
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CardPermitted(object sender, Hardware.CardReadEventArgs e)
        {
            if (Parent.WorkMode == ParkWorkMode.OffLine)
            {
                OfflineCardReadReport report = new OfflineCardReadReport()
                 {
                     ParkID = ParkID,
                     EntranceID = EntranceID,
                     SourceName = EntranceName,
                     EventDateTime = e.EventDateTime,
                     CardID = e.CardID,
                     EventStatus = CardEventStatus.Valid,
                     CarPlateComparisonResult = (Ralid.Park.BusinessModel.Enum.CarPlateComparisonResult)e.CarPlateComparisonResult,
                     CarPlate = e.CarPlate,
                     LastCarPlate = e.LastCarPlate,
                     Reader = (Ralid.Park.BusinessModel.Enum.EntranceReader)e.Reader,
                     LastDateTime = e.LastDateTime,
                 };
                AddToReportPool(report);
            }
            else
            {
                CommandEchoReport report = new CommandEchoReport()
                {
                    ParkID = ParkID,
                    EntranceID = EntranceID,
                    SourceName = EntranceName,
                    EventDateTime = e.EventDateTime,
                };
                AddToReportPool(report);
            }
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CardDenied(object sender, CardDeniedEventArgs e)
        {
            CardInvalidEventReport report = new CardInvalidEventReport();
            report.ParkID = Park.ParkID;
            report.EntranceID = EntranceID;
            report.CardID = e.CardID;
            report.SourceName = EntranceName;
            //report.InvalidType = (Ralid.Park.BusinessModel.Enum.EventInvalidType)e.Reason;
            report.EventDateTime = e.EventDateTime;
            AddToReportPool(report);
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_TakeACard(object sender, DeviceEventArgs e)
        {
            //临时卡数量级减少一张
            if (RemainTempCard > 0) RemainTempCard--;
            if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(this.EntranceName, "收到控制器上传的取走一张卡事件");
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CardIgnored(object sender, DeviceEventArgs e)
        {
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_CaptureACard(object sender, DeviceEventArgs e)
        {
            //CardCaptureReport report = new CardCaptureReport(ParkID, EntranceID, e.EventDateTime, EntranceName);
            //AddToReportPool(report);  //控制板的收卡一张事件由于不能确定是收回一张卡还是吐出一张卡,所以这里就不处理了
            if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(this.EntranceName, "收到控制器上传的收卡一张事件");
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_GateUp(object sender, DeviceEventArgs e)
        {
            EntranceStatusReport report = new EntranceStatusReport(ParkID, EntranceID, e.EventDateTime, EntranceName, EntranceStatus.GateUp);
            AddToReportPool(report);
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void ParkDevice_GateDown(object sender, DeviceEventArgs e)
        {
            EntranceStatusReport report = new EntranceStatusReport(ParkID, EntranceID, e.EventDateTime, EntranceName, EntranceStatus.GateDown);
            AddToReportPool(report);
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
        }

        private void NetEntrance_HeartBeat(object sender, EventArgs e)
        {
            _LastEventDatetime = DateTime.Now;
            Status = EntranceStatus.Ok;
        }

        private void NetEntrance_HeartBeat(object sender, HeartBeatEventArgs e)
        {
            NetEntrance_HeartBeat(sender, EventArgs.Empty);
            if (Parent.WorkMode == ParkWorkMode.Fool)
            {
                e.HeartBeat.Vacant = Park.Vacant;
                e.Response = true;
            }
            else if (EntranceInfo.IsMaster)//脱机模式的主控制器需要回复心跳包
            {
                if (Park.Vacant != e.HeartBeat.Vacant)
                {
                    Park.Vacant = e.HeartBeat.Vacant;
                    ParkVacantReport pReport = new ParkVacantReport(ParkID, EntranceID, DateTime.Now, EntranceName, e.HeartBeat.Vacant);
                    AddToReportPool(pReport);
                }
                e.Response = true;
            }
            else
            {
                e.Response = false;
            }
        }
        #endregion

        #region 重写基类事件处理
        protected override void OnButtonClickedReporting(ButtonClickedReport report)
        {
            EntranceOperationStatus PreStatus = OptStatus;
            if (Parent.WorkMode == ParkWorkMode.OffLine) return; //离线模式不处理
            if (IsExitDevice) return;  //出口读卡器不处理此事件
            if (Park.Vacant <= 0 && EntranceInfo.ForbidWhenFull)  //满位时禁止取卡
            {
                _ParkDevice.SpeakAndShow(DeviceVoiceAndMessage.ePROMPT_CWYM);
                return;
            }

            lock (_StatusLock)  //这里要加锁，防止用户快速按两或三次取卡按钮时出多张卡
            {
                if (OptStatus == EntranceOperationStatus.CardTakeingOut)
                {
                    TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - _LastTakeCard.Ticks);
                    if (ts.TotalSeconds < 6) return;
                }
                if (EntranceInfo.ReadAndTakeCardNeedCarSense && OptStatus != EntranceOperationStatus.CarArrival) return; //如果设置成车压地感取卡，则就先压地感
                OptStatus = EntranceOperationStatus.CardTakeingOut;
                _LastTakeCard = DateTime.Now;
            }
            if (EntranceInfo.TicketPrinterCOMPort > 0) //如果有纸票打印机，则表示临时卡为纸票
            {
                TakeoutATicket();
            }
            else
            {
                int deviceNum = report.Button;
                if (_ParkDevice.EjectCard(deviceNum) == DeviceOperationResult.Success)
                {
                    //如果出卡后超过一定时间临时卡读头没有读到卡片，则收回临时卡并允许车主再取一张
                    if (!EntranceInfo.AllowEjectCardWhithoutRead && !_TempCardReadEvent.WaitOne(1500))
                    {
                        _ParkDevice.CaptureCard(deviceNum);
                        lock (_StatusLock)
                        {
                            OptStatus = PreStatus;
                        }
                    }
                }
            }
            base.OnButtonClickedReporting(report);
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取控制板的临时卡读头
        /// </summary>
        public override bool IsTempReader(EntranceReader reader)
        {
            return reader == EntranceReader.Reader3
                || reader == EntranceReader.Address4Reader
                || reader == EntranceReader.Address5Reader
                || reader == EntranceReader.Address6Reader;
        }
        /// <summary>
        /// 获取或设置硬件通讯组件
        /// </summary>
        public ParkDevice ParkDevice
        {
            get { return _ParkDevice; }
        }
        /// <summary>
        /// 获取控制板的地址编号
        /// </summary>
        public byte EntranceNum
        {
            get
            {
                byte num = 0;
                List<EntranceInfo> entrances = ParkBuffer.Current.GetEntrances();
                entrances = (from en in entrances orderby en.EntranceID ascending select en).ToList();
                int numindex = entrances.FindIndex(en => en.EntranceID == EntranceID);
                numindex = numindex > -1 ? numindex : entrances.Count;
                numindex += 1;
                numindex %= 255;
                numindex = numindex == 0 ? 255 : numindex;
                num = (byte)numindex;
                return num;
            }
        }
        #endregion

        #region 公共重写方法
        /// <summary>
        /// 控制器复位
        /// </summary>
        public override void Reset()
        {
            _ParkDevice.Reset();
        }
        /// <summary>
        /// 同步时间
        /// </summary>
        public override void SyncTime()
        {
            if (Status != EntranceStatus.OffLine)
            {
                _ParkDevice.SetTime(DateTime.Now);
            }
        }
        /// <summary>
        /// 发卡机出卡一张
        /// </summary>
        public override void TakeOutACard()
        {
            if (this.Status != EntranceStatus.OffLine)
            {
                DeviceOperationResult ret = _ParkDevice.EjectCard(1);
                if (ret != DeviceOperationResult.Success)
                {
                    ret = _ParkDevice.EjectCard(2);
                }
            }
        }

        public override bool ApplyUserSetting(UserSetting us)
        {
            this.DisplayMsg(us.CompanyName, true);
            return true;
        }

        public override bool ApplyCarPortSetting(CarPortSetting cps)
        {
            if (Status != EntranceStatus.OffLine)
            {
                Hardware.ParkParams pp = new ParkParams();
                pp.ParkSpace = cps.CarPortUpLimit;
                pp.ParkVacant = cps.VacantPort;
                pp.Index = 0;
                pp.StrParkNum = "010000";
                _ParkDevice.SetParkLots(pp);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 在LED上保存信息
        /// </summary>
        public override void DisplayMsg(string msg, bool permanent)
        {
            if (Status != EntranceStatus.OffLine)
            {
                if (permanent)
                {
                    _ParkDevice.SetCompanyInfo(true, false, true, msg);
                }
                else
                {
                    _ParkDevice.ShowMessageOnLed(msg, LEDAddress.BoardLed);
                }
            }
        }
        /// <summary>
        /// 在车位屏上显示车余数
        /// </summary>
        public override void ShowVacant()
        {
            if (Status != EntranceStatus.OffLine)
            {
                //满位显示屏
                string vmsg = Park.Vacant > 0 ? Park.VacantText + Park.Vacant : Park.ParkFullText;
                _ParkDevice.ShowMessageOnLed(vmsg, LEDAddress.ParkFullLed);
            }
        }
        /// <summary>
        /// 开始收卡
        /// </summary>
        public override void StartCapture()
        {
            //_ParkDevice.SetCardCapturerSate(1);
            if (this.Status != EntranceStatus.OffLine)
            {
                for (int i = 0; i < _RetryCount; i++)
                {
                    Thread.Sleep(_RetryTimeout);

                    if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(this.EntranceName, "发送收卡指令 收卡机1");
                    DeviceOperationResult ret = _ParkDevice.CaptureCard(1);
                    if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(this.EntranceName, "收到收卡机1回复 结果：" + ret.ToString());

                    if (ret == DeviceOperationResult.Success)
                    {
                        //收卡成功后生成一个收卡一张事件,并加入到事件处理队列中.
                        ClearReportPool();
                        AddToReportPool(new CardCaptureReport(Park.ParkID, this.EntranceID, DateTime.Now, this.EntranceName));
                        break;
                    }

                    if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(this.EntranceName, "发送收卡指令 收卡机2");
                    ret = _ParkDevice.CaptureCard(2);
                    if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(this.EntranceName, "收到收卡机2回复 结果：" + ret.ToString());
                    if (ret == DeviceOperationResult.Success)
                    {
                        //收卡成功后生成一个收卡一张事件,并加入到事件处理队列中.
                        ClearReportPool();
                        AddToReportPool(new CardCaptureReport(Park.ParkID, this.EntranceID, DateTime.Now, this.EntranceName));
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 结束收卡
        /// </summary>
        public override void StopCapture()
        {
            _ParkDevice.SetCardCapturerSate(0);
        }
        /// <summary>
        /// 操作道闸
        /// </summary>
        /// <param name="notify"></param>
        public override void OperateGate(GateOperationNotify notify)
        {
            if (this.Status != EntranceStatus.OffLine)
            {
                for (int i = 0; i < _RetryCount; i++)
                {
                    DeviceOperationResult ret = DeviceOperationResult.CommunicationError;
                    if (notify.Action == Ralid.Park.BusinessModel.Enum.GateOperation.Open)
                    {
                        ret = _ParkDevice.GateOperation(Hardware.GateOperation.Open);
                    }
                    else if (notify.Action == Ralid.Park.BusinessModel.Enum.GateOperation.Close)
                    {
                        ret = _ParkDevice.GateOperation(Hardware.GateOperation.Close);
                    }
                    else if (notify.Action == Ralid.Park.BusinessModel.Enum.GateOperation.Stop)
                    {
                        ret = _ParkDevice.GateOperation(Hardware.GateOperation.Stop);
                    }

                    if (ret == DeviceOperationResult.Success)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(_RetryTimeout);
                    }
                }
            }
        }
        /// <summary>
        /// 上传卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCard(CardInfo card, ActionType action)
        {
            return SaveCard(card, action, true);
        }

        /// <summary>
        /// 上传卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCard(CardInfo card, ActionType action, bool savefail)
        {
            bool result = false;
            try
            {
                if (Status != EntranceStatus.OffLine)
                {
                    uint cardID = 0;
                    if (!uint.TryParse(card.CardID, out cardID)) return true; //卡号不能转换的直接跳过
                    if (card.Status == CardStatus.Enabled && action != ActionType.Delete)
                    {
                        Card c = new Card()
                        {
                            CardID = cardID,
                            CardType = ConvertCardType(card.CardType),
                            CarType = (Hardware.CarType)CarTypeSetting.Current.GetHardwareCarType(card.CarType),
                            IsValid = true,
                            ValidDate = card.ValidDate,
                            AccessID = card.AccessID,
                            Options = ConvertCardOptions(card.Options),
                            ActivationDate = card.ActivationDate,
                            CarPlate = card.CarPlate
                        };
                        for (int i = 0; i < _RetryCount; i++)
                        {
                            result = _ParkDevice.SaveCard(c);
                            if (result) break;
                            else Thread.Sleep(_RetryShortTimeout);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _RetryCount; i++)
                        {
                            result = _ParkDevice.DeleteCard(uint.Parse(card.CardID));
                            if (result) break;
                            else Thread.Sleep(_RetryShortTimeout);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            if (savefail && !result)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                WaitingCommandInfo info = new WaitingCommandInfo();
                info.EntranceID = EntranceID;
                info.CardID = card.CardID;
                if (action == ActionType.Add) info.Command = CommandType.AddCard;
                else if (action == ActionType.Upate) info.Command = CommandType.UpateCard;
                else if (action == ActionType.Delete) info.Command = CommandType.DeleteCard;
                wb.Insert(info);
            }
            return result;
        }
        /// <summary>
        /// 上传多张卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCards(List<CardInfo> cards, ActionType action)
        {
            return SaveCards(cards, action, true);
        }
        /// <summary>
        /// 上传多张卡片到控制板
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool SaveCards(List<CardInfo> cards, ActionType action, bool savefail)
        {
            bool result = false;
            try
            {
                if (Status != EntranceStatus.OffLine)
                {
                    if (cards != null && cards.Count > 0 && cards.Count <= 20)
                    {
                        List<Card> cs = new List<Card>();
                        for (int i = 0; i < cards.Count; i++)
                        {
                            CardInfo card = cards[i];
                            uint cardID = 0;
                            if (uint.TryParse(card.CardID, out cardID))
                            {
                                Card c = new Card()
                                {
                                    CardID = cardID,
                                    CardType = ConvertCardType(card.CardType),
                                    CarType = (Hardware.CarType)CarTypeSetting.Current.GetHardwareCarType(card.CarType),
                                    IsValid = card.Status == CardStatus.Enabled && action != ActionType.Delete,
                                    ValidDate = card.ValidDate,
                                    AccessID = card.AccessID,
                                    Options = ConvertCardOptions(card.Options),
                                    ActivationDate = card.ActivationDate,
                                    CarPlate = card.CarPlate
                                };
                            }
                        }

                        for (int i = 0; i < _RetryCount; i++)
                        {
                            result = _ParkDevice.SaveCards(cs.ToArray()) == 0;
                            if (result) break;
                            else Thread.Sleep(_RetryShortTimeout);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            if (savefail && !result)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                foreach (CardInfo card in cards)
                {
                    WaitingCommandInfo info = new WaitingCommandInfo();
                    info.EntranceID = EntranceID;
                    info.CardID = card.CardID;
                    if (action == ActionType.Add) info.Command = CommandType.AddCard;
                    else if (action == ActionType.Upate) info.Command = CommandType.UpateCard;
                    else if (action == ActionType.Delete) info.Command = CommandType.DeleteCard;
                    wb.Insert(info);
                }
            }
            return result;
        }
        /// <summary>
        /// 删除卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool DeleteCard(CardInfo card)
        {
            bool result = false;
            if (Status != EntranceStatus.OffLine)
            {
                uint cardID;
                if (uint.TryParse(card.CardID, out cardID))
                {
                    for (int i = 0; i < _RetryCount; i++)
                    {
                        result = _ParkDevice.DeleteCard(cardID);
                        if (result) break;
                        else Thread.Sleep(_RetryShortTimeout);
                    }
                }
            }
            if (!result)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                WaitingCommandInfo info = new WaitingCommandInfo();
                info.EntranceID = EntranceID;
                info.Command = CommandType.DeleteCard;
                info.CardID = card.CardID;
                wb.Insert(info);
            }
            return result;
        }
        /// <summary>
        /// 清空卡片
        /// </summary>
        /// <returns></returns>
        public override bool ClearCard()
        {
            bool result = false;
            if (Status != EntranceStatus.OffLine)
            {
                result = _ParkDevice.ClearCards();
            }
            if (!result)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                WaitingCommandInfo info = new WaitingCommandInfo();
                info.EntranceID = EntranceID;
                info.Command = CommandType.ClearCard;
                wb.Insert(info);
            }
            return result;
        }
        /// <summary>
        /// 向控制板发送卡片有效
        /// </summary>
        public override void CardValid()
        {
            if (ProcessingEvent != null && Status != EntranceStatus.OffLine)
            {
                CardEventReport report = ProcessingEvent;
                uint cardID;
                uint.TryParse(report.CardID, out cardID);
                DateTime expiredDT = report.ValidDate.Date;
                //发送卡片有效前，需设置对比结果为非失败
                if (report.ComparisonResult == Ralid.Park.BusinessModel.Enum.CarPlateComparisonResult.Fail)
                {
                    report.ComparisonResult = Ralid.Park.BusinessModel.Enum.CarPlateComparisonResult.Success;
                }
                if (AppSettings.CurrentSetting.Debug)
                {
                    Ralid.GeneralLibrary.LOG.FileLog.Log(EntranceName, string.Format("开始发送卡片有效指令 {0}", report.CardID));
                }
                for (int i = 0; i < _RetryCount; i++)
                {
                    DeviceOperationResult ret = _ParkDevice.CardValid((int)cardID, ConvertCardType(report.CardType), expiredDT);
                    if (ret == DeviceOperationResult.Success)
                    {
                        if (AppSettings.CurrentSetting.Debug)
                        {
                            Ralid.GeneralLibrary.LOG.FileLog.Log(EntranceName, string.Format("第 {0} 次发送卡片有效指令成功 {1}", i + 1, report.CardID));
                        }
                        break;
                    }
                    else
                    {
                        if (AppSettings.CurrentSetting.Debug)
                        {
                            Ralid.GeneralLibrary.LOG.FileLog.Log(EntranceName, string.Format("第 {0} 次发送卡片有效指令失败 {1}", i + 1, report.CardID));
                        }
                        Thread.Sleep(_RetryTimeout);
                    }
                }
                if (report.CardType.IsPrepayCard && report.IsOnlineHandleEvent)
                {
                    _ParkDevice.ShowMessageOnLed("卡片余额" + TariffSetting.Current.TariffOption.StrMoney(report.Balance) + "元", LEDAddress.BoardLed);
                }
            }
        }
        /// <summary>
        /// 向控制板发送卡片等待
        /// </summary>
        /// <param name="card"></param>
        public override void CardWait()
        {
            if (ProcessingEvent != null && Status != EntranceStatus.OffLine)
            {
                if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(EntranceName, "发送卡片等待指令 " + ProcessingEvent.CardID);
                if (ProcessingEvent.ChargeAsTempCard)
                {
                    if (ProcessingEvent.CardPaymentInfo.Accounts > 0)
                    {
                        _ParkDevice.SpeakAndPlayPayment((Hardware.CarType)CarTypeSetting.Current.GetHardwareCarType(ProcessingEvent.CarType),
                                TariffSetting.Current.TariffOption.StrMoney(ProcessingEvent.CardPaymentInfo.Accounts));
                    }
                }
                else if (ProcessingEvent.EventStatus == CardEventStatus.CarPlateFail)
                {
                    //_ParkDevice.Speak(DeviceVoice.eAUDIO_CPBF);
                    //_ParkDevice.Speak(DeviceVoice.eAUDIO_ZZQRQSH);
                    _ParkDevice.SpeakAndShow(DeviceVoiceAndMessage.ePROMPT_CPDBSBZZQR);
                }
                else if (ProcessingEvent.CardType.IsTempCard && ProcessingEvent.CardPaymentInfo.Accounts == 0)
                {
                    //_ParkDevice.Speak(DeviceVoice.eAUDIO_QCKHS);
                    _ParkDevice.SpeakAndShow(DeviceVoiceAndMessage.ePROMPT_QCKHS);
                }
                else
                {
                    //_ParkDevice.Speak(DeviceVoice.eAUDIO_ZZQRQSH);
                    _ParkDevice.SpeakAndShow(DeviceVoiceAndMessage.ePROMPT_ZZQRQSH);
                }
            }
        }
        /// <summary>
        /// 向控制板发送卡片无效
        /// </summary>
        /// <param name="cardevent"></param>
        public override void CardInValid(Ralid.Park.BusinessModel.Enum.EventInvalidType invalidType, object param)
        {
            if (Status != EntranceStatus.OffLine)
            {
                if (AppSettings.CurrentSetting.Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(EntranceName,
                     "发送卡片无效指令 " + Ralid.Park.BusinessModel.Resouce.CardInvalidDescripition.GetDescription(invalidType));
                for (int i = 0; i < _RetryCount; i++)
                {
                    DeviceOperationResult ret = _ParkDevice.CardInvalid(1000, Hardware.CardType.TempCard, ConvertInvalidType(invalidType));
                    if (ret == DeviceOperationResult.Success)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(_RetryTimeout);
                    }
                }
                if (!this.IsExitDevice && this.OptStatus == EntranceOperationStatus.CardTakeingOut) //如果此时按下了取卡按钮说明取出的卡无效,此时应允许车主再取一张卡
                {
                    this.OptStatus = EntranceOperationStatus.CarArrival;
                }
            }
        }
        /// <summary>
        /// 与硬件同步信息
        /// </summary>
        public void SyncToHardware()
        {
            //如果有一个命令执行不成功，则说明可能硬件有问题或连接不上，则不要浪费时间执行下面的操作了
            bool ret = false;
            ret = _ParkDevice.SetTime(DateTime.Now);
            if (!ret) return;

            DeviceInfo di = GetDeviceInfoFrom(EntranceInfo);
            ret = _ParkDevice.SetDeviceInfoWithCheck(di);    //设置硬件参数
            if (!ret) return;

            WorkmodeInfo wi = GetWorkModeInfoFrom(EntranceInfo);
            ret = _ParkDevice.SetWorkmodeWithCheck(wi);    //设置工作模式
            if (!ret) return;

            LANInfo li = GetLANInfoFrom(EntranceInfo);
            ret = _ParkDevice.SetLANInfoWithCheck(li);  //设置网络参数
            if (!ret) return;

            ret = _ParkDevice.SetCarPlateNotifyController(EntranceInfo.CarPlateNotifyIPs);//设置车牌识别结果需要发送的控制器IP组
            if (!ret) return;

            ret = _ParkDevice.SetCardTypeProperty(EntranceInfo.CardTypeProperty);//设置卡片类型的属性
            if (!ret) return;
        }
        /// <summary>
        /// 释放占用的资源
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            _ParkDevice.UnListenEvents(_CommunicationIP, _CommunicationPort);
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="accessSetting"></param>
        /// <returns></returns>
        public override bool ApplyAccessSetting(AccessSetting accessSetting)
        {
            bool result = false;
            if (Status != EntranceStatus.OffLine)
            {
                Ralid.Park.Hardware.AccessGroup accessGroup = GetAccessGroupFrom(accessSetting, EntranceID);
                if (accessGroup != null)
                {
                    for (int i = 0; i < _RetryCount; i++)
                    {
                        result = _ParkDevice.SetAccessGroup(accessGroup);
                        if (result) break;
                        else Thread.Sleep(_RetryShortTimeout);
                    }
                    if (result)
                    {
                        foreach (byte index in accessGroup.AccessTimeZones.Keys)
                        {
                            for (int i = 0; i < _RetryCount; i++)
                            {
                                result = _ParkDevice.SetAccessTimeZone(index, accessGroup.AccessTimeZones[index]);
                                if (result) break;
                                else Thread.Sleep(_RetryShortTimeout);
                            }
                            if (!result)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (!result)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                WaitingCommandInfo info = new WaitingCommandInfo();
                info.EntranceID = EntranceID;
                info.Command = CommandType.DownloadAccesses;
                wb.Insert(info);
            }
            return result;
        }

        /// <summary>
        /// 设置节假日
        /// </summary>
        /// <param name="holidaySetting"></param>
        /// <returns></returns>
        public override bool ApplyHolidaySetting(HolidaySetting holidaySetting)
        {
            bool result = false;
            if (Status != EntranceStatus.OffLine)
            {
                Ralid.Park.Hardware.H_WeekProperty week = GetWeekFrom(holidaySetting);
                for (int i = 0; i < _RetryCount; i++)
                {
                    result = _ParkDevice.SetWeekProperty(week);
                    if (result) break;
                    else Thread.Sleep(_RetryShortTimeout);
                }
                if (result)
                {
                    List<Ralid.Park.Hardware.H_DateInterval> dateIntervals = GetDateIntervalsFrom(holidaySetting);
                    for (int i = 0; i < _RetryCount; i++)
                    {
                        result = _ParkDevice.SetDateInterval(dateIntervals);
                        if (result) break;
                        else Thread.Sleep(_RetryShortTimeout);
                    }
                }
            }
            if (!result)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                WaitingCommandInfo info = new WaitingCommandInfo();
                info.EntranceID = EntranceID;
                info.Command = CommandType.DownloadHolidays;
                wb.Insert(info);
            }
            return result;
        }

        /// <summary>
        /// 设置费率
        /// </summary>
        /// <param name="tariffSetting"></param>
        /// <returns></returns>
        public override bool ApplyTariffSetting(TariffSetting tariffSetting)
        {
            bool result = false;
            if (Status != EntranceStatus.OffLine)
            {
                List<H_TariffSetting> h_TariffSettings = GetTariffSettingFrom(tariffSetting);
                if (h_TariffSettings != null && h_TariffSettings.Count > 0)
                {
                    foreach (H_TariffSetting tariff in h_TariffSettings)
                    {
                        for (int i = 0; i < _RetryCount; i++)
                        {
                            result = _ParkDevice.SetTariff(tariff);
                            if (result) break;
                            else Thread.Sleep(_RetryShortTimeout);
                        }
                        if(!result)
                        {
                            break;
                        }
                    }
                }
            }
            if (!result)
            {
                WaitingCommandBLL wb = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                WaitingCommandInfo info = new WaitingCommandInfo();
                info.EntranceID = EntranceID;
                info.Command = CommandType.DownloadTariffs;
                wb.Insert(info);
            }
            return result;
        }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="keySetting"></param>
        /// <returns></returns>
        public override bool ApplyKeySetting(KeySetting keySetting)
        {
            bool result = false;
            if (Status != EntranceStatus.OffLine)
            {
                byte section = keySetting.ParkingSection;
                byte[] key = keySetting.ParkingKey;
                for (int i = 0; i < _RetryCount; i++)
                {
                    result = _ParkDevice.SetReaderKeyA(section, key);
                    if (result) break;
                    else Thread.Sleep(_RetryShortTimeout);
                }
            }
            return result;
        }

        /// <summary>
        /// 向控制板获取识别到的车牌号码
        /// </summary>
        /// <returns></returns>
        public override string GetRecognizedCarPlate()
        {
            string carplate = string.Empty;
            if (Status != EntranceStatus.OffLine)
            {
                bool result = false;
                for (int i = 0; i < _RetryCount; i++)
                {
                    result = _ParkDevice.GetRecognizedCarPlate(out carplate);
                    if (result) break;
                    else Thread.Sleep(_RetryShortTimeout);
                }
            }
            return carplate;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取控制板收费记录存储信息
        /// </summary>
        /// <param name="capacity" >返回控制板上收费记录的容量</param>
        /// <param name="latestIndex">返回最近一条收费记录的流水号</param>
        /// <returns></returns>
        public bool GetPaymentStorageInfo(out int capacity, out int latestIndex)
        {
            if (Status != EntranceStatus.OffLine)
            {
                return _ParkDevice.GetPaymentStorageInfo(out capacity,out latestIndex);
            }
            capacity = latestIndex = 0;
            return false;
        }
         /// <summary>
        /// 从控制器获取收费记录,没有记录或数据错误返回NULL
        /// </summary>
        /// <param name="beginIndex">要获取的第一个收费记录的流水号(控制器中收费记录的流水号从1开始)</param>
        /// <param name="count">要获取的收费记录的数量</param>
        /// <returns></returns>
        public List<DevicePaymentRecord> GetPaymentRecords(int beginIndex, int count)
        {
            if (Status != EntranceStatus.OffLine)
            {
                List<DevicePaymentRecord> deviceRecords = _ParkDevice.GetPaymentRecords(beginIndex, count);
                
                return deviceRecords;
            }
            return null; 
        }
        #endregion
    }
}