using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.Model
{
    /// <summary>
    /// 表示系统常规设置
    /// </summary>
    public class MySetting
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置系统当前的常规设置
        /// </summary>
        public static MySetting Current { get; set; }

        public static MySetting DefaultSetting
        {
            get
            {
                MySetting ms = new MySetting()
                {
                    WegenType = WegenType.Wengen34,
                    ParkingKey = new byte[] { 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1 },
                    ParkingSection = 2,
                    FreeTimeAfterPay = 15,
                };
                return ms;
            }
        }
        #endregion

        #region 构造函数
        public MySetting()
        {
        }
        #endregion

        #region 常规设置
        /// <summary>
        /// 获取或设置系统使用的Wegen协议
        /// </summary>
        public WegenType  WegenType { get; set; }

        /// <summary>
        /// 密钥值
        /// </summary>
        public byte[] ParkingKey { get; set; }

        /// <summary>
        /// 获取停车场读写的扇区
        /// </summary>
        public byte ParkingSection{get;set;}

        /// <summary>
        /// 获取或设置收费后最多可以在停车场内呆多少分钟而不用收费
        /// </summary>
        public int FreeTimeAfterPay { get; set; }

        /// <summary>
        /// 获取或设置手持机的编号
        /// </summary>
        public string StationID { get; set; }
        #endregion

        #region 费率设置
        //由于费率设置采用继承自Tariffbase的方式，采用XML序列化时不能直接映射到子类，所以每种子类在这里定义一个列表，反序列化后再将所有列表组成一个基类集合
        //系统每增加一种收费子类都需要在这里建一个列表才能序列化和序列化

        #region 公共属性
        public List<TariffOfDixiakongjian> TariffOfDixiakongjians { get; set; }

        public List<TariffOfGuanZhou> TariffOfGuanZhous { get; set; }

        public List<TariffOfLimitation> TariffOfLimitations { get; set; }

        public List<TariffOfTurningLimited > TariffOfTurningLimiteds { get; set; }

        public List<TariffOfTurning> TariffOfTurnings { get; set; }

        public List<TariffPerDay> TariffPerDays { get; set; }

        public List<TariffPerTime> TariffPerTimes { get; set; }

        public List<TariffBase> TariffArray
        {
            get
            {
                List<TariffBase> _TariffArray = new List<TariffBase>();
                if (this.TariffOfDixiakongjians != null && this.TariffOfDixiakongjians.Count > 0)
                {
                    this.TariffOfDixiakongjians.ForEach(tb => _TariffArray.Add(tb));
                }
                if (this.TariffOfGuanZhous != null && this.TariffOfGuanZhous.Count > 0)
                {
                    this.TariffOfGuanZhous.ForEach(tb => _TariffArray.Add(tb));
                }
                if (this.TariffOfLimitations != null && this.TariffOfLimitations.Count > 0)
                {
                    this.TariffOfLimitations.ForEach(tb => _TariffArray.Add(tb));
                }
                if (this.TariffOfTurningLimiteds != null && this.TariffOfTurningLimiteds.Count > 0)
                {
                    this.TariffOfTurningLimiteds.ForEach(tb => _TariffArray.Add(tb));
                }
                if (this.TariffOfTurnings != null && this.TariffOfTurnings.Count > 0)
                {
                    this.TariffOfTurnings.ForEach(tb => _TariffArray.Add(tb));
                }
                if (this.TariffPerDays != null && this.TariffPerDays.Count > 0)
                {
                    this.TariffPerDays.ForEach(tb => _TariffArray.Add(tb));
                }
                if (this.TariffPerTimes != null && this.TariffPerTimes.Count > 0)
                {
                    this.TariffPerTimes.ForEach(tb => _TariffArray.Add(tb));
                }
                return _TariffArray;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取只精确到分钟的日期时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DateTime GetMyDateTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取某卡片某车型beginning时间的当天费率
        /// </summary>
        /// <param name="card"></param>
        /// <param name="carType"></param>
        /// <param name="beginning"></param>
        /// <returns></returns>
        public TariffBase GetIntradayTariff(CardInfo card, Byte carType, DateTime beginning)
        {
            TariffBase tariff = null;
            tariff = MySetting.Current.GetCalculateTariff(card.CardType.ID, carType, card.ParkingStatus, beginning);
            return tariff;
        }


        /// <summary>
        /// 获取收费费率
        /// </summary>
        /// <param name="cardType">卡类型</param>
        /// <param name="carType">车型</param>
        /// <param name="tariffType">收费类型</param>
        /// <returns></returns>
        public TariffBase GetTariff(byte? cardType, int carType, TariffType tariffType)
        {
            byte? _cardType = cardType;
            //系统原型卡类型没有纸票类型，纸票其实就是一种临时卡，所以直接查找临时卡的费率就可以了
            if (_cardType.HasValue && _cardType == CardType.Ticket.ID)
            {
                _cardType = CardType.TempCard.ID;
            }

            foreach (TariffBase tariff in TariffArray)
            {
                if ((_cardType == null || tariff.CardType == _cardType.Value) && tariff.CarType == carType && tariff.TariffType == tariffType)
                {
                    return tariff;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取某一种卡片类型、某一车型的费率
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="carType"></param>
        /// <param name="tariffType"></param>
        /// <returns></returns>
        public TariffBase GetTariff(byte cardType, int carType)
        {
            foreach (TariffBase tariff in TariffArray)
            {
                if (tariff.CardType == cardType && tariff.CarType == carType)
                {
                    return tariff;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取卡片开始时间当天的计算停车费用的收费费率
        /// </summary>
        /// <param name="cardType">卡片类型</param>
        /// <param name="carType">车型</param>
        /// <param name="parkingStatus">停车状态</param>
        /// <param name="beginning">开始时间</param>
        /// <returns></returns>
        public TariffBase GetCalculateTariff(Byte? cardType, Byte carType, ParkingStatus parkingStatus, DateTime beginning)
        {
            TariffType tt = TariffType.Normal;
            if ((parkingStatus & ParkingStatus.NestedParkMarked) == ParkingStatus.NestedParkMarked)
            {
                if (MySetting.Current.IsHoliday(beginning))
                {
                    tt = TariffType.HolidayAndInnerRoom;
                }
                else
                {
                    tt = TariffType.InnerRoom;
                }
            }
            else
            {
                if (MySetting.Current.IsHoliday(beginning))
                {
                    tt = TariffType.Holiday;
                }
            }

            TariffBase tb = GetTariff(cardType, carType, tt);//tt为null时免费
            return tb;
        }

        /// <summary>
        /// 计算卡片的应收停车费用,如果之前已经多次收费,则费用会扣除相应的收费金额
        /// </summary>
        /// <param name="card">卡片</param>
        /// <param name="freeTimeAfterPay">卡片收费后允许免费停留多少分钟</param>
        /// <param name="carType">卡片收费车型</param>
        /// <returns>停车收费信息</returns>
        public ParkAccountsInfo CalculateCardParkFee(CardInfo card, Byte carType, DateTime chargeDateTime)
        {
            ParkAccountsInfo parkAccounts = new ParkAccountsInfo();
            parkAccounts.CarType = carType;

            decimal fee = 0;

            //以卡片的入场时间计算
            DateTime beginning = GetMyDateTime(card.LastDateTime);

            //如果启用了酒店应用
            if (card.EnableHotelApp)
            {
                if (card.IsInFreeTime(chargeDateTime))
                {
                    //处于免费时间点内，费用为0
                    return parkAccounts;
                }
                else if (card.FreeDateTime.HasValue)
                {
                    //过了免费时间点的，以免费时间点为开始时间计算费用
                    beginning = GetMyDateTime(card.FreeDateTime.Value);
                }
            }

            DateTime ending = GetMyDateTime(chargeDateTime);
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            double totalMins = Math.Ceiling(ts.TotalMinutes);
            if (totalMins < 0) return parkAccounts;  //入场时间大于出场时间
            DateTime begin = beginning;
            DateTime end = beginning;
            TariffBase tariff = null;
            while (begin < ending)
            {
                tariff = GetCalculateTariff(card.CardType.ID, carType, card.ParkingStatus, begin);
                while (end.Date < ending.Date)
                {
                    end = end.AddDays(1);
                    TariffBase tb = GetCalculateTariff(card.CardType.ID, carType, card.ParkingStatus, end);
                    if (
                        (tariff == null && tb == null) ||
                        (tariff != null && tariff.Equals(tb))
                        )
                    {
                        //donothing
                    }
                    else
                    {
                        end = end.Date;
                        if (tariff != null)
                        {
                            decimal temp = tariff.CalculateFee(begin, end);
                            fee += tariff.FeeOfMax > 0 && tariff.FeeOfMax < temp ? tariff.FeeOfMax : temp;
                            parkAccounts.TariffType = tariff.TariffType;
                        }
                        tariff = tb;
                        begin = end <= ending ? end : ending;
                        end = begin;
                        break;
                    }
                }
                if (end.Date == ending.Date) break;
            }
            if (tariff != null && begin < ending)
            {
                decimal temp = tariff.CalculateFee(begin, ending);
                fee += tariff.FeeOfMax > 0 && tariff.FeeOfMax < temp ? tariff.FeeOfMax : temp;
            }
            parkAccounts.ParkFee = fee;
            decimal accounts = fee - card.TotalPaidFee;//已缴费用=停车费用-已收费用                    
            if (card.IsCompletedPaid)
            {
                //已完成缴费，判断是否已过收费后免费时间
                if (IsInFreeTime(card.PaidDateTime.Value, chargeDateTime))
                {
                    accounts = 0;
                    //还处于免费时间，车场费用为卡片中的停车费用
                    parkAccounts.ParkFee = card.ParkFee;
                }
            }
            parkAccounts.Accounts = accounts > 0 ? accounts : 0;
            return parkAccounts;
        }

        /// <summary>
        /// 检查时间是否已过免费时间
        /// </summary>
        /// <param name="chargeDateTime">缴费时间</param>
        /// <param name="dateTime">检查的时间</param>
        /// <returns></returns>
        public bool IsInFreeTime(DateTime chargeDateTime, DateTime dateTime)
        {
            TimeSpan ts = new TimeSpan(GetMyDateTime(dateTime).Ticks - GetMyDateTime(chargeDateTime).Ticks);
            return ts.TotalMinutes <= MySetting.Current.FreeTimeAfterPay;
        }

        /// <summary>
        /// 返回缴费后剩余免费时间
        /// </summary>
        /// <param name="chargeDateTime">缴费时间</param>
        /// <param name="dateTime">检查的时间</param>
        /// <returns></returns>
        public int FreeTimeRemaining(DateTime chargeDateTime, DateTime dateTime)
        {
            int freetime = 0;
            TimeSpan ts = new TimeSpan(GetMyDateTime(dateTime).Ticks - GetMyDateTime(chargeDateTime).Ticks);
            freetime = MySetting.Current.FreeTimeAfterPay - (int)ts.TotalMinutes;
            freetime = freetime > 0 ? freetime : 0;
            return freetime;
        }
        #endregion
        #endregion

        #region 节假日设置
        /// <summary>
        ///获取或设置星期六是否为休息日
        /// </summary>
        public bool SaturdayIsRest { get; set; }

        /// <summary>
        /// 获取或设置星期日是否为休息日
        /// </summary>
        public bool SundayIsRest { get; set; }

        /// <summary>
        /// 获取节假日列表
        /// </summary>
        public List<HolidayInfo> Holidays{get;set;}
        /// <summary>
        /// 判断日期是否节假日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool IsHoliday(DateTime dt)
        {
            bool ret = false;
            ////如果周六或者周日是节假日，则在调整的节假日转工作日中查找，否则在节假日列表中找
            if ((dt.DayOfWeek == DayOfWeek.Saturday && SaturdayIsRest) ||
                (dt.DayOfWeek == DayOfWeek.Sunday && SundayIsRest))
            {
                ret = true;
                if (Holidays != null && Holidays.Count > 0)
                {
                    foreach (HolidayInfo holiday in Holidays)
                    {
                        if (holiday.WeekenToWorkDayInterval != null && holiday.WeekenToWorkDayInterval.Count > 0)
                        {
                            foreach (DatetimeInterval di in holiday.WeekenToWorkDayInterval)
                            {
                                if (di.IsInDatetimeInterval(dt))
                                {
                                    ret = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (Holidays != null && Holidays.Count > 0)
                {
                    foreach (HolidayInfo holiday in Holidays)
                    {
                        if (dt.Date >= holiday.StartDate && dt.Date <= holiday.EndDate)
                        {
                            ret = true;
                            break;
                        }
                    }
                }
            }
            return ret;
        }
        #endregion

        #region 停车场系统操作员
        public List<OperatorInfo> Operators { get; set; }
        #endregion
    }
}
