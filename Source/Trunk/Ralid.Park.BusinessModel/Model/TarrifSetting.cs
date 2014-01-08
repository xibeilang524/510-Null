using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;
using Ralid.Park.BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示系统当前的收费设置
    /// </summary>
    [DataContract]
    public class TariffSetting
    {
        /// <summary>
        /// 系统当前收费设置
        /// </summary>
        public static TariffSetting Current { get; set; }

        [DataMember]
        private List<TariffBase> _TariffArray;

        #region 构造函数
        public TariffSetting()
        {
            _TariffArray = new List<TariffBase>();
            TariffOption = new TollOptionSetting();
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
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// 获取卡片开始时间当天的计算停车费用的收费费率
        /// </summary>
        /// <param name="cardType">卡片类型</param>
        /// <param name="carType">车型</param>
        /// <param name="parkingStatus">停车状态</param>
        /// <param name="beginning">开始时间</param>
        /// <returns></returns>
        private TariffBase GetCalculateTariff(Byte? cardType, Byte carType, ParkingStatus parkingStatus, DateTime beginning)
        {
            TariffType tt = TariffType.Normal;
            if ((parkingStatus & ParkingStatus.NestedParkMarked) == ParkingStatus.NestedParkMarked)
            {
                if (HolidaySetting.Current.IsHoliday(beginning))
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
                if (HolidaySetting.Current.IsHoliday(beginning))
                {
                    tt = TariffType.Holiday;
                }
            }

            TariffBase tb = GetTariff(cardType, carType, tt);//tt为null时免费
            return tb;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取系统所有的收费标准项
        /// </summary>
        public List<TariffBase> TariffArray
        {
            get
            {
                return _TariffArray;
            }
        }

        /// <summary>
        /// 获取或设置收费选项
        /// </summary>
        [DataMember]
        public TollOptionSetting TariffOption { get; set; }
        #endregion

        #region 公共方法
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

            foreach (TariffBase tariff in _TariffArray)
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
            if (cardType == CardType.Ticket.ID) cardType = CardType.TempCard.ID;
            foreach (TariffBase tariff in _TariffArray)
            {
                if (tariff.CardType == cardType && tariff.CarType == carType)
                {
                    return tariff;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取某一种卡片类型的基本车型的所有费率
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public List<TariffBase> GetBaseCarTypeTariffs(byte cardType)
        {
            List<TariffBase> tariffs = new List<TariffBase>();
            foreach (TariffBase tariff in _TariffArray)
            {
                if (tariff.CardType == cardType && tariff.CarType < 4)
                {
                    tariffs.Add(tariff);
                }
            }
            return tariffs;
        }

        /// <summary>
        /// 计算卡片的应收停车费用,如果之前已经多次收费,则费用会扣除相应的收费金额
        /// </summary>
        /// <param name="card">卡片</param>
        /// <param name="carType">卡片收费车型</param>
        /// <returns>停车收费信息</returns>
        public ParkAccountsInfo CalculateCardParkFee(CardInfo card, Byte carType, DateTime chargeDateTime)
        {
            ParkAccountsInfo parkAccounts = new ParkAccountsInfo();
            parkAccounts.CarType = carType;

            decimal fee = 0;
            DateTime beginning = GetMyDateTime(card.LastDateTime);
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
                if (TariffSetting.Current.IsInFreeTime(card.PaidDateTime.Value, chargeDateTime))
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
            return ts.TotalMinutes <= TariffOption.FreeTimeAfterPay;
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
            freetime = TariffOption.FreeTimeAfterPay - (int)ts.TotalMinutes;
            freetime = freetime > 0 ? freetime : 0;
            return freetime;
        }
        #endregion
    }
}
