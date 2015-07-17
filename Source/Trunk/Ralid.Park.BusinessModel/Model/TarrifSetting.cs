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

        #region 私有属性
        /// <summary>
        /// 表示系统所有的费率(收费标准【公共】)
        /// </summary>
        [DataMember]
        private List<TariffBase> _TariffArray;

        ///// <summary>
        ///// 表示系统所有停车场的单独费率的集合(收费标准【私有】)
        ///// </summary>
        [DataMember]
        private Dictionary<int,List<TariffBase>> _parkTariffDictionary;
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

        public Dictionary<int, List<TariffBase>> ParkTariffDictionary
        {
            get
            {
                return _parkTariffDictionary;
            }
            set
            {
                _parkTariffDictionary = value;
            }
        }

        /// <summary>
        /// 获取或设置收费选项
        /// </summary>
        [DataMember]
        public TollOptionSetting TariffOption { get; set; }
        #endregion

        #region 构造函数
        public TariffSetting()
        {
            _TariffArray = new List<TariffBase>();
            _parkTariffDictionary = new Dictionary<int, List<TariffBase>>();
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

        private TariffBase GetCalculateTariff(int? parkID, Byte? cardType, Byte carType, ParkingStatus parkingStatus, DateTime beginning)
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

            TariffBase tb = GetTariff(parkID, cardType, carType, tt);//tt为null时免费
            return tb;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取通用收费费率
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

            if (_TariffArray != null)
            {
                foreach (TariffBase tariff in _TariffArray)
                {
                    if ((_cardType == null || tariff.CardType == _cardType.Value) && tariff.CarType == carType && tariff.TariffType == tariffType)
                    {
                        return tariff;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取停车场单独收费费率
        /// </summary>
        /// <param name="parkID">停车场ID</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="carType">车型</param>
        /// <param name="tariffType">收费类型</param>
        /// <returns></returns>
        public TariffBase GetAloneTariff(int parkID, byte? cardType, int carType, TariffType tariffType)
        {
            byte? _cardType = cardType;
            //系统原型卡类型没有纸票类型，纸票其实就是一种临时卡，所以直接查找临时卡的费率就可以了
            if (_cardType.HasValue && _cardType == CardType.Ticket.ID)
            {
                _cardType = CardType.TempCard.ID;
            }

            if (_parkTariffDictionary != null
                && _parkTariffDictionary.ContainsKey(parkID)
                && _parkTariffDictionary[parkID] != null)
            {
                foreach (TariffBase tariff in _parkTariffDictionary[parkID])
                {
                    if ((_cardType == null || tariff.CardType == _cardType.Value) && tariff.CarType == carType && tariff.TariffType == tariffType)
                    {
                        return tariff;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取收费费率，有设置单独费率的，返回单独费率，否则返回通用费率
        /// </summary>
        /// <param name="parkID">停车场ID</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="carType">车型</param>
        /// <param name="tariffType">收费类型</param>
        /// <returns></returns>
        public TariffBase GetTariff(int? parkID, byte? cardType, int carType, TariffType tariffType)
        {
            byte? _cardType = cardType;
            //系统原型卡类型没有纸票类型，纸票其实就是一种临时卡，所以直接查找临时卡的费率就可以了
            if (_cardType.HasValue && _cardType == CardType.Ticket.ID)
            {
                _cardType = CardType.TempCard.ID;
            }

            List<TariffBase> workTariff = _TariffArray;

            //当有设置单独费率并且设置费率的个数大于0时，才返回单独费率
            if (_parkTariffDictionary != null
                && parkID.HasValue
                && _parkTariffDictionary.ContainsKey(parkID.Value)
                && _parkTariffDictionary[parkID.Value] != null
                && _parkTariffDictionary[parkID.Value].Count > 0)
            {
                workTariff = _parkTariffDictionary[parkID.Value];
            }

            if (workTariff != null)
            {
                foreach (TariffBase tariff in workTariff)
                {
                    if ((_cardType == null || tariff.CardType == _cardType.Value) && tariff.CarType == carType && tariff.TariffType == tariffType)
                    {
                        return tariff;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取某一种卡片类型、某一车型的费率（通用费率）
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="carType"></param>
        /// <param name="tariffType"></param>
        /// <returns></returns>
        public TariffBase GetTariff(byte cardType, int carType)
        {
            if (cardType == CardType.Ticket.ID) cardType = CardType.TempCard.ID;
            if (_TariffArray != null)
            {
                foreach (TariffBase tariff in _TariffArray)
                {
                    if (tariff.CardType == cardType && tariff.CarType == carType)
                    {
                        return tariff;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取某停车场，某一种卡片类型、某一车型的费率
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="cardType"></param>
        /// <param name="carType"></param>
        /// <returns></returns>
        public TariffBase GetTariff(int? parkID, byte cardType, int carType)
        {
            if (cardType == CardType.Ticket.ID) cardType = CardType.TempCard.ID;
            //先查找当独费率
            if (_parkTariffDictionary != null
                && parkID.HasValue
                && _parkTariffDictionary.ContainsKey(parkID.Value)
                && _parkTariffDictionary[parkID.Value] != null)
            {
                foreach (TariffBase tariff in _parkTariffDictionary[parkID.Value])
                {
                    if (tariff.CardType == cardType && tariff.CarType == carType)
                    {
                        return tariff;
                    }
                }
            }

            //单独费率没找到的，再查找通用费率
            return GetTariff(cardType, carType);
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
        /// 获取某一种卡片类型的基本车型的所有费率(有单独费率的返回单独费率，否则返回通用费率)
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public List<TariffBase> GetBaseCarTypeTariffs(int? parkID, byte cardType)
        {
            List<TariffBase> tariffs = new List<TariffBase>();

            List<TariffBase> workTariff = null;
            //当有设置单独费率并且设置费率的个数大于0时，才返回单独费率
            if (_parkTariffDictionary != null
                && parkID.HasValue
                && _parkTariffDictionary.ContainsKey(parkID.Value)
                && _parkTariffDictionary[parkID.Value] != null
                && _parkTariffDictionary[parkID.Value].Count > 0)
            {
                workTariff = _parkTariffDictionary[parkID.Value];
            }
            else
            {
                workTariff = _TariffArray;
            }

            if (workTariff != null)
            {
                foreach (TariffBase tariff in workTariff)
                {
                    if (tariff.CardType == cardType && tariff.CarType < 4)
                    {
                        tariffs.Add(tariff);
                    }
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
                else if (card.FreeDateTime.HasValue && card.FreeDateTime.Value > beginning)
                {
                    //过了免费时间点的，如果免费时间点大于开始时间，以免费时间点为开始时间计算费用
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

        public ParkAccountsInfo CalculateCardParkFee(int? parkID, CardInfo card, Byte carType, DateTime chargeDateTime)
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
                else if (card.FreeDateTime.HasValue && card.FreeDateTime.Value > beginning)
                {
                    //过了免费时间点的，如果免费时间点大于开始时间，以免费时间点为开始时间计算费用
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
                tariff = GetCalculateTariff(parkID,card.CardType.ID, carType, card.ParkingStatus, begin);
                while (end.Date < ending.Date)
                {
                    end = end.AddDays(1);
                    TariffBase tb = GetCalculateTariff(parkID,card.CardType.ID, carType, card.ParkingStatus, end);
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
            //if (tariff is TariffOfGuanZhou) //2015-4-29 修改为计算费用时，判断该日夜差异收费费率是否允许一次收费多次进出
            //{
            //    TariffOfGuanZhou tariffguangzhou = tariff as TariffOfGuanZhou;
            //    if (tariffguangzhou != null)
            //    {
            //        if (tariffguangzhou.DayTimezone.ZSLH1 == true) //白天时段一次交费多次进出
            //        {
            //            if (tariffguangzhou.DayTimezone.IsIn(chargeDateTime)) //出场时间是否在免费时间之内
            //            {
            //                parkAccounts.FeeEndDateTime = Convert.ToDateTime(DateTime.Now.ToLongDateString() + tariffguangzhou.DayTimezone.Ending.Hour.ToString() + ":" + tariffguangzhou.DayTimezone.Ending.Minute.ToString());
            //            }
            //        }
            //        if (tariffguangzhou.NightTimezone.ZSLH2 == true) //夜晚时段一次交费多次进出
            //        {
            //            if (tariffguangzhou.NightTimezone.IsIn(chargeDateTime)) //出场时间是否在免费时间之内
            //            {
            //                parkAccounts.FeeEndDateTime = Convert.ToDateTime(DateTime.Now.ToLongDateString() + tariffguangzhou.NightTimezone.Ending.Hour.ToString() + ":" + tariffguangzhou.DayTimezone.Ending.Minute.ToString());
            //            }
            //        }
            //    }
            //}
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
        /// 计算卡片的优惠金额
        /// </summary>
        /// <param name="parkID">停车场ID</param>
        /// <param name="card">卡片实体类</param>
        /// <param name="carType">卡片类型</param>
        /// <param name="chargeDateTime">计费时间</param>
        /// <param name="usedHour">已使用优惠时数</param>
        /// <param name="currentHour">输出：当前使用的优惠时数</param>
        /// <returns></returns>
        public decimal CalculateCardDiscountMoney(int? parkID, CardInfo card, Byte carType, DateTime chargeDateTime,int usedHour, out int currentHour)
        {
            currentHour = 0;
            int discountHour = card.DiscountHour - usedHour;
            if (discountHour <= 0) return 0;
            //优惠时数有效期为24小时
            if (card.PreferentialTime != null && chargeDateTime.AddHours(-24) > card.PreferentialTime.Value) return 0;

            decimal discountMoney = 0;

            #region 以卡片的入场时间、出场时间获取费率费率
            //以卡片的入场时间计算
            DateTime beginning = GetMyDateTime(card.LastDateTime);
            //如果启用了酒店应用
            if (card.EnableHotelApp)
            {
                if (card.FreeDateTime.HasValue && card.FreeDateTime.Value > beginning)
                {//过了免费时间点的，如果免费时间点大于开始时间，以免费时间点为开始时间计算费用
                    beginning = GetMyDateTime(card.FreeDateTime.Value);
                }
            }
            DateTime ending = GetMyDateTime(chargeDateTime);
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            double totalMins = Math.Ceiling(ts.TotalMinutes);
            TariffBase tariff = null;
            //由于优惠算法是太古汇使用的，太古汇现场没有节假日不同费率收费的情况，所以这里不考虑工作日与节假日交替计算优惠的情况
            tariff = GetCalculateTariff(parkID, card.CardType.ID, carType, card.ParkingStatus, beginning);

            //DateTime begin = beginning;
            //DateTime end = beginning;
            //while (begin < ending)
            //{
            //    tariff = GetCalculateTariff(parkID, card.CardType.ID, carType, card.ParkingStatus, begin);
            //    while (end.Date < ending.Date)
            //    {
            //        end = end.AddDays(1);
            //        TariffBase tb = GetCalculateTariff(parkID, card.CardType.ID, carType, card.ParkingStatus, end);
            //        if (
            //            (tariff == null && tb == null) ||
            //            (tariff != null && tariff.Equals(tb))
            //            )
            //        {
            //            //donothing
            //        }
            //        else
            //        {
            //            end = end.Date;
            //            tariff = tb;
            //            begin = end <= ending ? end : ending;
            //            end = begin;
            //            break;
            //        }
            //    }
            //    if (end.Date == ending.Date) break;
            //}
            #endregion

            if (tariff != null)
            {
                if (tariff is TariffOfGuanZhou)//日夜差异收费
                {
                    TariffOfGuanZhou gzTariff = tariff as TariffOfGuanZhou;
                    discountMoney = gzTariff.CalculateDiscountFee(beginning, ending, discountHour, out currentHour);
                }
            }
            return discountMoney;
        }

        //2014-10-15 Jan 注销 ，算法计算出的优惠金额部分情况与客户要求不符
        ///// <summary>
        ///// 计算卡片的优惠金额
        ///// </summary>
        ///// <param name="parkID"></param>
        ///// <param name="card"></param>
        ///// <param name="carType"></param>
        ///// <param name="chargeDateTime"></param>
        ///// <returns></returns>
        //public decimal CalculateCardDiscountMoney(int? parkID,CardInfo card, Byte carType, DateTime chargeDateTime,ref int currentHour)
        //{
        //    if (card.DiscountHour == 0) return 0;
        //    if (card.PreferentialTime != null && chargeDateTime.AddHours(-24) > card.PreferentialTime.Value) return 0;

        //    decimal discountMoney = 0;

        //    #region 求解卡片的入场时间、出场时间、费率(算法同CalculateCardParkFee)
        //    //以卡片的入场时间计算
        //    DateTime beginning = GetMyDateTime(card.LastDateTime);
        //    //如果启用了酒店应用
        //    if (card.EnableHotelApp)
        //    {
        //        if (card.FreeDateTime.HasValue && card.FreeDateTime.Value > beginning)
        //        {//过了免费时间点的，如果免费时间点大于开始时间，以免费时间点为开始时间计算费用
        //            beginning = GetMyDateTime(card.FreeDateTime.Value);
        //        }
        //    }
        //    DateTime ending = GetMyDateTime(chargeDateTime);
        //    TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
        //    double totalMins = Math.Ceiling(ts.TotalMinutes);

        //    DateTime begin = beginning;
        //    DateTime end = beginning;
        //    TariffBase tariff = null;
        //    while (begin < ending)
        //    {
        //        tariff = GetCalculateTariff(parkID, card.CardType.ID, carType, card.ParkingStatus, begin);
        //        while (end.Date < ending.Date)
        //        {
        //            end = end.AddDays(1);
        //            TariffBase tb = GetCalculateTariff(parkID, card.CardType.ID, carType, card.ParkingStatus, end);
        //            if (
        //                (tariff == null && tb == null) ||
        //                (tariff != null && tariff.Equals(tb))
        //                )
        //            {
        //                //donothing
        //            }
        //            else
        //            {
        //                end = end.Date;
        //                tariff = tb;
        //                begin = end <= ending ? end : ending;
        //                end = begin;
        //                break;
        //            }
        //        }
        //        if (end.Date == ending.Date) break;
        //    }
        //    #endregion

        //    if (tariff != null && begin < ending)
        //    {
        //        if (tariff is TariffOfGuanZhou)//日夜差异收费
        //        {
        //            TariffOfGuanZhou gzTariff = tariff as TariffOfGuanZhou;
        //            discountMoney = CalculateDiscountMoney(gzTariff, card, chargeDateTime, ref currentHour);
        //        }
        //    }
        //    return discountMoney;
        //}

        //2014-10-15 Jan 注销 ，算法计算出的优惠金额部分情况与客户要求不符
        ///// <summary>
        ///// 计算从入场时间到收费时间段内的最大优惠时数的优惠费用
        ///// PREPreferential表(PreferentialHour)和Card表(DiscountHour)将存储录入的允许优惠的时数，且出场前这个值不会改变(出场后清零)；
        ///// CardPaymentRecord表(DiscountHour)将存储已优惠的时数，且这个值是个累加值。
        ///// </summary>
        ///// <param name="tariff"></param>
        ///// <param name="card"></param>
        ///// <param name="chargeDateTime"></param>
        ///// <param name="currentHour">输出：本次优惠时数</param>
        ///// <returns></returns>
        //public decimal CalculateDiscountMoney(TariffOfGuanZhou tariff, CardInfo card, DateTime chargeDateTime,ref int currentHour)
        //{
        //    decimal result = 0;
        //    int maxDiscountHour = card.DiscountHour;//当前卡片的优惠时长(小时)
        //    DateTime beginning = GetMyDateTime(card.LastDateTime);//入场时间
        //    DateTime ending = GetMyDateTime(chargeDateTime);//出场时间(收费时间)
        //    TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
        //    int totalParkingHour = (int)Math.Ceiling(ts.TotalHours);//总共停车时长(整数，不足一小时按一小时计)
        //    int workHour = maxDiscountHour >= totalParkingHour ? totalParkingHour : maxDiscountHour;//本次应计算的优惠时数
        //    currentHour = workHour;

        //    DateTime moveBegin = beginning;//定义可变的开始时间
        //    DateTime moveEnd = ending;     //定义可变的结束时间
        //    List<decimal> moveAreaMoneyList = new List<decimal>();//定义每过一个小时的时间片段费用的集合
        //    for (int i = 0; i < totalParkingHour; i++)
        //    {
        //        DateTime calMoveEnd = moveBegin.AddHours(1);
        //        calMoveEnd = calMoveEnd >= moveEnd ? moveEnd : calMoveEnd;
        //        decimal areaHourMoney = tariff.CalculateFee(moveBegin, calMoveEnd);
        //        moveAreaMoneyList.Add(areaHourMoney);//将此一小时的费用加入费用片段集合
        //        moveBegin = moveBegin.AddHours(1);
        //    }

        //    do
        //    {
        //        decimal maxAnHourMoney = moveAreaMoneyList.Max();
        //        result += maxAnHourMoney;

        //        moveAreaMoneyList.Remove(maxAnHourMoney);
        //        workHour -= 1;
        //    }
        //    while (workHour > 0);
        //    decimal parkFee = tariff.CalculateFee(beginning, ending);//应收费用
        //    decimal HasDiscountFee = tariff.CalculateFee(beginning.AddHours(currentHour), ending);//优惠后应收费用
        //    if (parkFee == HasDiscountFee)
        //        result = 0;
        //    return result;
        //}

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
