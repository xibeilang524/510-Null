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
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }

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
            //if (GlobalVariables.IsNETParkAndOffLie)
            //{
            //    //网络型写卡模式获取费用
            //    tariff = TariffSetting.Current.GetCalculateTariff(card.CardType.ID, carType, card.ParkingStatus, beginning);
            //}
            //else
            //{
            //    //其他模式获取费率
            //    if (card.CardType.IsPrimaryCardType && (card.CardType.IsTempCard || card.CardType.IsPrepayCard))
            //    {
            //        tariff = TariffSetting.Current.GetCalculateTariff(null, carType, card.ParkingStatus, beginning);
            //    }
            //    else
            //    {
            //        tariff = card.CardType.GetTariff();
            //    }
            //}
            tariff = TariffSetting.Current.GetCalculateTariff(card.CardType.ID, carType, card.ParkingStatus, beginning);
            return tariff;
        }

        ///// <summary>
        ///// 计算停车费用
        ///// </summary>
        ///// <param name="card">卡片</param>
        ///// <param name="carType">车型</param>
        ///// <param name="chargeDateTime">计费时间</param>
        ///// <returns></returns>
        //private decimal CalculateFee(CardInfo card, Byte carType, DateTime chargeDateTime)
        //{
        //    decimal ParkFee = 0;
        //    TariffBase tariff = null;
        //    TimeSpan ts = new TimeSpan(chargeDateTime.Ticks - card.LastDateTime.Ticks);
        //    if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间

        //    DateTime begin = card.LastDateTime;

        //    do
        //    {
        //        tariff = GetIntradayTariff(card, carType, begin);
        //        if (tariff == null)
        //        {
        //            //获取费率为空时，可认为当天的费用为0，开始时间修改为下一天的0:00
        //            begin = begin.AddDays(1);
        //            begin = new DateTime(begin.Year, begin.Month, begin.Day, 0, 0, 0);
        //            if (begin > chargeDateTime) return 0;
        //        }
        //    } while (tariff == null);

        //    if (tariff != null)
        //    {

        //        //先判断是否免费停车时间
        //        if (tariff.FreeMinutes > 0 && ts.TotalMinutes <= tariff.FreeMinutes) return 0;//小于免费停车时间
        //        if (tariff.FreeMinutes == 0 && ts.TotalMinutes == 0) return tariff.GetChargeUnitFee(begin);//返回收费单元的费用

        //        DateTime end;
        //        //收费都是按24小时为一周期
        //        do
        //        {
        //            end = begin.AddDays(1);
        //            end = end < chargeDateTime ? end : chargeDateTime;


        //            ParkFee += tariff.CalculateFee(GetMyDateTime(begin), GetMyDateTime(end));
        //            begin = end;

        //            while (begin < chargeDateTime)
        //            {
        //                //开始一个收费周期
        //                tariff = GetIntradayTariff(card, carType, begin);//获取下一个收费周期的费率
        //                if (tariff != null) break;
        //                begin = begin.AddDays(1);
        //            }
        //        } while (begin < chargeDateTime);
        //    }

        //    return ParkFee;

        //}
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

        /// <summary>
        /// 获取收费费率
        /// </summary>
        /// <param name="carType">车型</param>
        /// <param name="tariffType">收费类型</param>
        /// <returns></returns>
        public TariffBase GetTariff(int carType, TariffType tariffType)
        {
            foreach (TariffBase tariff in _TariffArray)
            {
                if (tariff.CarType == carType && tariff.TariffType == tariffType)
                {
                    return tariff;
                }
            }
            return null;
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
        /// 获取某一种卡片类型的所有费率
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public List<TariffBase> GetTariffs(byte cardType)
        {
            List<TariffBase> tariffs = new List<TariffBase>();
            foreach (TariffBase tariff in _TariffArray)
            {
                if (tariff.CardType == cardType)
                {
                    tariffs.Add(tariff);
                }
            }
            return tariffs;
        }

        ///// <summary>
        ///// 获取卡片某段时间的计算停车费用的收费费率
        ///// </summary>
        ///// <param name="carType">车型</param>
        ///// <param name="parkingStatus">停车状态</param>
        ///// <param name="beginning">入场时间</param>
        ///// <param name="ending">出场时间</param>
        ///// <returns></returns>
        //public TariffBase GetCalculateTariff(Byte carType, ParkingStatus parkingStatus, DateTime beginning, DateTime ending)
        //{
        //    TariffType tt = TariffType.Normal;
        //    if ((parkingStatus & ParkingStatus.NestedParkMarked) == ParkingStatus.NestedParkMarked)
        //    {
        //        if (HolidaySetting.Current.IsInHoliday(beginning, ending))
        //        {
        //            tt = TariffType.HolidayAndInnerRoom;
        //        }
        //        else
        //        {
        //            tt = TariffType.InnerRoom;
        //        }
        //    }
        //    else
        //    {
        //        if (HolidaySetting.Current.IsInHoliday(beginning, ending))
        //        {
        //            tt = TariffType.Holiday;
        //        }
        //    }

        //    TariffBase tb = GetTariff(carType, tt);
        //    if (tb == null)  //如果没有找到相应的就用默认的收费费率
        //    {
        //        tb = GetTariff(CarTypeSetting.DefaultCarType, TariffType.Normal);
        //    }
        //    return tb;
        //}

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

            TariffBase tb = GetTariff(cardType,carType, tt);//tt为null时免费
            return tb;
        }

        ///// <summary>
        ///// 获取卡片开始时间当天的计算停车费用的收费费率
        ///// </summary>
        ///// <param name="cardType">卡片类型</param>
        ///// <param name="carType">车型</param>
        ///// <param name="parkingStatus">停车状态</param>
        ///// <param name="beginning">开始时间</param>
        ///// <returns></returns>
        //public TariffBase GetCalculateTariff(Byte? cardType, Byte carType, ParkingStatus parkingStatus, DateTime beginning,DateTime ending)
        //{
        //    TariffType tt = TariffType.Normal;
        //    if ((parkingStatus & ParkingStatus.NestedParkMarked) == ParkingStatus.NestedParkMarked)
        //    {
        //        if (HolidaySetting.Current.IsHoliday(beginning))
        //        {
        //            tt = TariffType.HolidayAndInnerRoom;
        //        }
        //        else
        //        {
        //            tt = TariffType.InnerRoom;
        //        }
        //    }
        //    else
        //    {
        //        if (HolidaySetting.Current.IsHoliday(beginning))
        //        {
        //            tt = TariffType.Holiday;
        //        }
        //    }

        //    TariffBase tb = GetTariff(cardType, carType, tt);//tt为null时免费
        //    return tb;
        //}


        ///// <summary>
        ///// 获取卡片某段时间的计算停车费用的收费费率
        ///// </summary>
        ///// <param name="carType">卡片类型</param>
        ///// <param name="isNested">是否内套车场</param>
        ///// <param name="beginning">入场时间</param>
        ///// <param name="ending">出场时间</param>
        ///// <returns></returns>
        //public TariffBase GetCalculateTariff(Byte carType, bool isNested, DateTime beginning, DateTime ending)
        //{
        //    TariffType tt = TariffType.Normal;
        //    if (isNested)
        //    {
        //        if (HolidaySetting.Current.IsInHoliday(beginning, ending))
        //        {
        //            tt = TariffType.HolidayAndInnerRoom;
        //        }
        //        else
        //        {
        //            tt = TariffType.InnerRoom;
        //        }
        //    }
        //    else
        //    {
        //        if (HolidaySetting.Current.IsInHoliday(beginning, ending))
        //        {
        //            tt = TariffType.Holiday;
        //        }
        //    }

        //    TariffBase tb = GetTariff(carType, tt);
        //    if (tb == null)  //如果没有找到相应的就用默认的收费费率
        //    {
        //        tb = GetTariff(CarTypeSetting.DefaultCarType, TariffType.Normal);
        //    }
        //    return tb;
        //}


        ///// <summary>
        ///// 计算卡片目前应收停车费用(包括外车场和内车场),如果之前已经多次收费,则费用会扣除相应的收费金额
        ///// </summary>
        ///// <param name="card">卡片</param>
        ///// <param name="freeTimeAfterPay">卡片收费后允许免费停留多少分钟</param>
        ///// <param name="carType">卡片收费车型</param>
        ///// <returns>停车收费信息</returns>
        //public ParkAccountsInfo CalculateCardParkFee(CardInfo card, Byte carType, DateTime chargeDateTime)
        //{
        //    ParkAccountsInfo parkAccounts = new ParkAccountsInfo();
        //    TariffBase tariff = null;
        //    if (card.CardType.IsPrimaryCardType && (card.CardType.IsTempCard || card.CardType.IsPrepayCard))
        //    {
        //        tariff = TariffSetting.Current.GetCalculateTariff(carType, false, card.LastDateTime, chargeDateTime);
        //    }
        //    else
        //    {
        //        tariff = card.CardType.GetTariff();
        //    }
        //    if (tariff != null)
        //    {
        //        parkAccounts.Accounts = tariff.CalculateFee(GetMyDateTime(card.LastDateTime), GetMyDateTime(chargeDateTime));
        //        parkAccounts.TariffType = tariff.TariffType;
        //        parkAccounts.CarType = carType;
        //        if (card.LastPayment != null)
        //        {
        //            //再重新以上次收费计费时间计算一次费用，如果计算结果比上次累计实收和累计折扣大，则表明之前有收费计算时与当前车型不一致，这部分
        //            //费用要重新补交。（防止下面这种情况出现：中央收费后设置允许15分钟内可以免费出场，则有些车主在入场后每隔15分钟去刷一次卡交费，
        //            //这样出场时就不会产生费用）
        //            decimal lastTotal = tariff.CalculateFee(GetMyDateTime(card.LastDateTime), GetMyDateTime(card.LastPayment.ChargeDateTime));
        //            if (lastTotal > card.LastPayment.TotalDiscount + card.LastPayment.TotalPaid) //之前还没有把费用全部收齐
        //            {
        //                parkAccounts.Accounts -= (card.LastPayment.TotalPaid + card.LastPayment.TotalDiscount);
        //            }
        //            else
        //            {
        //                TimeSpan ts = new TimeSpan(GetMyDateTime(chargeDateTime).Ticks - GetMyDateTime(card.LastPayment.ChargeDateTime).Ticks);
        //                if (ts.TotalMinutes <= TariffOption.FreeTimeAfterPay)
        //                {
        //                    parkAccounts.Accounts = 0;
        //                }
        //                else
        //                {
        //                    parkAccounts.Accounts -= (card.LastPayment.TotalPaid + card.LastPayment.TotalDiscount);
        //                    if (parkAccounts.Accounts < 0) parkAccounts.Accounts = 0;
        //                }
        //            }
        //        }
        //    }
        //    return parkAccounts;
        //}

        ///// <summary>
        ///// 计算卡片的应收停车费用,如果之前已经多次收费,则费用会扣除相应的收费金额
        ///// </summary>
        ///// <param name="card">卡片</param>
        ///// <param name="freeTimeAfterPay">卡片收费后允许免费停留多少分钟</param>
        ///// <param name="carType">卡片收费车型</param>
        ///// <returns>停车收费信息</returns>
        //public ParkAccountsInfo CalculateCardParkFee(CardInfo card, Byte carType, DateTime chargeDateTime)
        //{
        //    ParkAccountsInfo parkAccounts = new ParkAccountsInfo();
        //    TariffBase tariff = null;
        //    if (GlobalVariables.IsNETParkAndOffLie)
        //    {
        //        //网络型写卡模式获取费用
        //        tariff = TariffSetting.Current.GetCalculateTariff(card.CardType.ID, carType, card.ParkingStatus, card.LastDateTime, chargeDateTime);
        //    }
        //    else
        //    {
        //        //其他模式获取费率
        //        if (card.CardType.IsPrimaryCardType && (card.CardType.IsTempCard || card.CardType.IsPrepayCard))
        //        {
        //            tariff = TariffSetting.Current.GetCalculateTariff(null, carType, card.ParkingStatus, card.LastDateTime, chargeDateTime);
        //        }
        //        else
        //        {
        //            tariff = card.CardType.GetTariff();
        //        }
        //    }
        //    if (tariff != null)
        //    {
        //        //先计算车场费用
        //        decimal ParkFee = tariff.CalculateFee(GetMyDateTime(card.LastDateTime), GetMyDateTime(chargeDateTime));
        //        parkAccounts.ParkFee = ParkFee;
        //        parkAccounts.TariffType = tariff.TariffType;
        //        parkAccounts.CarType = carType;

        //        decimal accounts = ParkFee - card.TotalPaidFee;//已缴费用=停车费用-已收费用

        //        ////是否外车场已缴费
        //        //if (card.IsPaid)
        //        //{
        //        //    //判断上次缴费是否已收取全部费用，累计停车费用为0
        //        //    if (card.TotalFee > 0)
        //        //    {
        //        //        //上次收费有费用未收取
        //        //        if (ParkFee > card.ParkFee)
        //        //        {
        //        //            //费用为多出的费用
        //        //            accounts = ParkFee - card.ParkFee;
        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        //已完成收费，判断是否已过收费后免费时间
        //        //        if (!TariffSetting.Current.IsInFreeTime(card.PaidDateTime.Value, chargeDateTime))
        //        //        {
        //        //            //已过免费时间
        //        //            if (ParkFee > card.ParkFee)
        //        //            {
        //        //                //费用为多出的费用
        //        //                accounts = ParkFee - card.ParkFee;
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            //还处于免费时间，车场费用为卡片中的停车费用
        //        //            parkAccounts.ParkFee = card.ParkFee;
        //        //        }
        //        //    }

        //        //}
        //        //else
        //        //{
        //        //    //未缴费，费用为外车场费用
        //        //    accounts = ParkFee;
        //        //}

        //        //parkAccounts.Accounts = accounts + card.TotalFee;      

        //        if (card.IsCompletedPaid)
        //        {
        //            //已完成缴费，判断是否已过收费后免费时间
        //            if (TariffSetting.Current.IsInFreeTime(card.PaidDateTime.Value, chargeDateTime))
        //            {

        //                accounts = 0;
        //                //还处于免费时间，车场费用为卡片中的停车费用
        //                parkAccounts.ParkFee = card.ParkFee;
        //            }
        //        }
        //        parkAccounts.Accounts = accounts > 0 ? accounts : 0;
        //    }
        //    return parkAccounts;
        //}

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
            TariffBase tariff = null;
            DateTime begin = GetMyDateTime(card.LastDateTime);
            DateTime end = GetMyDateTime(chargeDateTime);

            do
            {
                tariff = GetIntradayTariff(card, carType, begin);
                if (tariff == null)
                {
                    //获取费率为空时，可认为当天的费用为0，开始时间修改为下一天的0:00
                    begin = begin.AddDays(1);
                    begin = new DateTime(begin.Year, begin.Month, begin.Day, 0, 0, 0);
                    if (begin > end) break;
                }
            } 
            while (tariff == null);

            if (tariff != null)
            {
                tariff.Card = card;
                //先计算车场费用
                decimal ParkFee = tariff.CalculateFee(begin, end);
                parkAccounts.ParkFee = ParkFee;
                parkAccounts.TariffType = tariff.TariffType;
                parkAccounts.CarType = carType;

                decimal accounts = ParkFee - card.TotalPaidFee;//已缴费用=停车费用-已收费用                    

                if (card.IsCompletedPaid)
                {
                    //已完成缴费，判断是否已过收费后免费时间
                    if (TariffSetting.Current.IsInFreeTime(card.PaidDateTime.Value, end))
                    {

                        accounts = 0;
                        //还处于免费时间，车场费用为卡片中的停车费用
                        parkAccounts.ParkFee = card.ParkFee;
                    }
                }
                parkAccounts.Accounts = accounts > 0 ? accounts : 0;

                tariff.Card = null;
            }
            return parkAccounts;
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
    }
}
