using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 广州分时段收费标准,两个时段,加一个特殊时段
    /// </summary>
    [DataContract]
    [Serializable]
    public class TariffOfGuanZhou : TariffBase
    {
        public TariffOfGuanZhou()
        {

        }

        #region 公共属性
        ///<summary>
        /// 获取或设置过点是否切换时段
        /// </summary>
        [DataMember]
        public bool SwitchWhenTimeZoneOverTime { get; set; }

        /// <summary>
        /// 获取或设置白天收费时段
        /// </summary>
        [DataMember]
        public TariffTimeZone DayTimezone { get; set; }

        /// <summary>
        /// 获取或设置晚上收费时段
        /// </summary>
        [DataMember]
        public TariffTimeZone NightTimezone { get; set; }
        #endregion

        #region 私有方法
        /// <summary>
        /// 在连续时间段内计算各时段交替收费的总费用
        /// </summary>
        /// <param name="beginning"></param>
        /// <param name="ending"></param>
        /// <returns></returns>
        private decimal CalculateFeeAlteration(DateTime beginning, DateTime ending)
        {
            double minDay = 0;
            double minNight = 0;
            decimal fee = 0;
            //交替计算,直到所有时段都计算完
            do
            {
                minDay = DayTimezone.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (minDay > 0)
                {
                    fee += DayTimezone.CalculateFee(minDay);
                    beginning = beginning.AddMinutes(minDay);
                }
                minNight = NightTimezone.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (minNight > 0)
                {
                    fee += NightTimezone.CalculateFee(minNight);
                    beginning = beginning.AddMinutes(minNight);
                }
            }
            while (minDay > 0 || minNight > 0);
            return fee;
        }
        #endregion

        #region 重写基类方法
        public override decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            decimal fee = 0;
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            if (ts.TotalMinutes < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            if (FeeOf24Hour > 0)  //每24小时有限额收费
            {
                int days = (int)Math.Floor(ts.TotalDays);   //收费都是按24小时为一周期,所以先将整24小时的费用收取完
                if (days > 0)
                {
                    fee += FeeOf24Hour * days;
                }

                DateTime begin2 = beginning.AddDays(days);  //计算不足24小时的收费部分
                decimal tail = CalculateFeeAlteration(begin2, ending);
                fee += (tail > FeeOf24Hour) ? FeeOf24Hour : tail;
            }
            else   //无24小时限额收费,则各时间费用累加
            {
                fee = CalculateFeeAlteration(beginning, ending);
            }
            return fee;
        }

        public override string ToString()
        {
            return Resouce.Resource1.Tariff_Guanzhou;
        }
        #endregion


        #region 优惠费率算法相关

        #region 私有方法
        /// <summary>
        /// 计算两个时段内的收费费用，不考虑时段的最高收费
        /// </summary>
        /// <param name="beginning"></param>
        /// <param name="ending"></param>
        /// <returns></returns>
        private TariffTwoZoneCharge CalculateTwoZoneChargeFee(DateTime beginning, DateTime ending)
        {
            TariffTwoZoneCharge chargeFee = new TariffTwoZoneCharge(beginning, ending);
            double minDay = 0;
            double minNight = 0;
            //交替计算,直到所有时段都计算完
            do
            {
                minDay = DayTimezone.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (minDay > 0)
                {
                    chargeFee.FirstCharge += DayTimezone.CalculateFeeWithOutLimiteFee(minDay);
                    beginning = beginning.AddMinutes(minDay);
                }
                minNight = NightTimezone.SliceDuration(beginning, ending, SwitchWhenTimeZoneOverTime);
                if (minNight > 0)
                {
                    chargeFee.SecondCharge += NightTimezone.CalculateFeeWithOutLimiteFee(minNight);
                    beginning = beginning.AddMinutes(minNight);
                }
            }
            while (minDay > 0 || minNight > 0);
            return chargeFee;
        }


        /// <summary>
        /// 计算时段内的优惠金额，计算的时段不超过24小时
        /// </summary>
        /// <param name="beginning">时段的开始时间</param>
        /// <param name="ending">时段的结算时间</param>
        /// <param name="discountHour">可用的优惠时数</param>
        /// <param name="currentHour">输出：当前使用的优惠时数</param>
        /// <returns></returns>
        private decimal CalculateDiscountMoneyTimeZone(DateTime beginning, DateTime ending, int discountHour, out int currentHour)
        {
            decimal discountFee = 0;
            currentHour = 0;
            if (discountHour <= 0) return discountFee;//如果优惠时数小于等于0，优惠金额为0
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            int totalParkingHour = (int)Math.Ceiling(ts.TotalHours);//总共停车时长(整数，不足一小时按一小时计)

            //如果优惠时数大于等于停车时长，优惠金额为时段收取的费用，当前使用的优惠时数为停车时长
            if (discountHour >= totalParkingHour)
            {
                currentHour = totalParkingHour;
                discountFee = CalculateFeeAlteration(beginning, ending);
                return discountFee;
            }

            currentHour = discountHour;//当前使用的优惠时数为可用的优惠时数
            int workHour = currentHour;

            #region 时间段算出每小时收取的费用集合
            DateTime moveBegin = beginning;//定义可变的开始时间
            DateTime moveEnd = ending;     //定义可变的结束时间
            List<TariffTwoZoneCharge> moveAreaMoneyList = new List<TariffTwoZoneCharge>();//定义每过一个小时的时间片段费用的集合
            for (int i = 0; i < totalParkingHour; i++)
            {
                DateTime calMoveEnd = moveBegin.AddHours(1);
                calMoveEnd = calMoveEnd >= moveEnd ? moveEnd : calMoveEnd;
                TariffTwoZoneCharge areaHourMoney = CalculateTwoZoneChargeFee(moveBegin, calMoveEnd);
                moveAreaMoneyList.Add(areaHourMoney);//将此一小时的费用加入费用片段集合
                moveBegin = moveBegin.AddHours(1);
            }
            #endregion

            //decimal chargFee = moveAreaMoneyList.Sum(c => c.ChargeFee);//定义时间段内应收的费用
            //decimal chargFee = CalculateFeeAlteration(beginning, ending);//定义时间段内应收的费用
            TariffTwoZoneCharge chargMoney = CalculateTwoZoneChargeFee(beginning, ending);//定义时间段内应收的费用

            #region 获取待优惠的费用集合
            List<TariffTwoZoneCharge> discountMoneyList = new List<TariffTwoZoneCharge>();//定义用于优惠的时间段费用

            //根据优惠时数循环获取最大的时间段费用作为优惠的费用
            do
            {
                TariffTwoZoneCharge maxAnHourMoney = moveAreaMoneyList.Max();
                discountMoneyList.Add(maxAnHourMoney);

                moveAreaMoneyList.Remove(maxAnHourMoney);
                workHour -= 1;
            }
            while (workHour > 0);
            #endregion


            decimal dayDiscount = discountMoneyList.Sum(c => c.FirstCharge);//定义日间时段优惠的费用
            decimal nightDiscount = discountMoneyList.Sum(c => c.SecondCharge);//定义夜间时段优惠的费用
            //decimal dayFee = moveAreaMoneyList.Sum(c => c.FirstCharge);//定义优惠后日间还需收取的费用
            decimal nightFee = moveAreaMoneyList.Sum(c => c.SecondCharge);//定义优惠后夜间还需收取的费用
            decimal dayFee = chargMoney.FirstCharge - dayDiscount;//定义优惠后日间还需收取的费用，因为日间没有最高收费，所以这里直接使用日间费用-优惠费用
            if (nightFee < chargMoney.SecondCharge - nightDiscount)
            {
                //如果优惠后夜间还需收取的费用小于夜间应收费用-夜间优惠费率
                nightFee = chargMoney.SecondCharge - nightDiscount;//优惠后夜间还需收取的费用为夜间应收费用-夜间优惠费率
            }
            if (dayFee < 0)
            {
                //如果定义优惠后日间还需收取的费用为负数，日间优惠的费用为日间时段的费用
                dayFee = 0;
                dayDiscount = chargMoney.FirstCharge;
            }
            if (nightFee < 0)
            {
                //如果定义优惠后夜间还需收取的费用为负数，夜间优惠的费用为夜间时段的费用
                nightFee = 0;
                nightDiscount = chargMoney.SecondCharge;
            }

            #region 夜间最高限额判断
            //如果夜间有最高限价，不管夜间有没有优惠费用，都需要进行最高限价判断
            if (NightTimezone.LimiteFee != null && NightTimezone.LimiteFee.Value > 0)
            {
                //先判断优惠后夜间收取的费用是否大于等于最高限额
                if (nightFee >= NightTimezone.LimiteFee.Value)
                {
                    //大于等于最高限额，夜间优惠金额设为0，优惠后夜间还需收取的费用为最高限额
                    nightDiscount = 0;
                    nightFee = NightTimezone.LimiteFee.Value;
                }
                else
                {
                    //小于最高限额的，需判断原来的夜间收费是否大于最高限额
                    if (nightFee + nightDiscount > NightTimezone.LimiteFee.Value)
                    {
                        //原来的夜间收费大于最高限额的，夜间优惠金额为最高限额-优惠后夜间收取费用
                        nightDiscount = NightTimezone.LimiteFee.Value - nightFee;
                    }
                }
            }
            #endregion

            //优惠金额为夜间优惠金额+日间优惠金额
            discountFee = nightDiscount + dayDiscount;

            #region 24小时最高收费判断
            if (FeeOf24Hour > 0)
            {
                //先判断优惠后的时段收费是否大于等于24小时最高收费
                //if (chargFee - discountFee >= FeeOf24Hour)
                if (dayFee + nightFee >= FeeOf24Hour)
                {
                    //优惠后的时段收费大于等于24小时最高收费的，优惠金额为0
                    discountFee = 0;
                }
                else
                {
                    //小于24小时最高收费，需判读原来的时段收费是否大于24小时最高收费
                    //if (chargFee > FeeOf24Hour)
                    if (chargMoney.ChargeFee > FeeOf24Hour)
                    {
                        //原来的时段收费大于24小时最高收费的，优惠金额为24小时最高收费-优惠后收取的费用
                        //discountFee = FeeOf24Hour - (chargFee - discountFee);
                        discountFee = FeeOf24Hour - (dayFee + nightFee);
                    }
                }
            }
            #endregion

            //如果出现负数，设优惠金额为0
            if (discountFee < 0)
            {
                discountFee = 0;
            }
            return discountFee;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 计算优惠金额
        /// </summary>
        /// <param name="beginning">开始时间</param>
        /// <param name="ending">结束时间</param>
        /// <param name="discountHour">可用的优惠时数</param>
        /// <param name="currentHour">输出：当前使用的优惠时数</param>
        /// <returns></returns>
        public decimal CalculateDiscountFee(DateTime beginning, DateTime ending, int discountHour, out int currentHour)
        {
            decimal discountFee = 0;
            currentHour = 0;
            if (discountHour <= 0) return discountFee;//如果优惠时数小于等于0，优惠金额为0
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            int totalParkingHour = (int)Math.Ceiling(ts.TotalHours);//总共停车时长(整数，不足一小时按一小时计)

            //如果优惠时数大于等于停车时长，优惠金额为停车费用，当前使用的优惠时数为停车时长
            if (discountHour >= totalParkingHour)
            {
                currentHour = totalParkingHour;
                discountFee = CalculateFee(beginning, ending);
                return discountFee;
            }

            currentHour = discountHour;//优惠时数小于停车时长，当前使用的优惠时数为可用的优惠时数
            int parkingDays = (int)Math.Floor(ts.TotalDays);//总共停满的天数（整数，忽略不够一天的）
            int discountDays = discountHour / 24;//总共优惠的天数（整数，忽略不够一天的）
            int curHour = 0;//用于保存计算优惠金额时输出的使用优惠时数
            DateTime begin = beginning;
            DateTime end = begin;

            //先处理满24小时的优惠时数
            if (discountDays > 0)
            {
                //优惠金额为时间段的停车费用
                if (FeeOf24Hour > 0)
                {
                    //有24小时最高收费的，为最高收费*满24小时的优惠时数
                    discountFee += FeeOf24Hour * discountDays;
                }
                else
                {
                    //累计计算停车费
                    end = begin.AddDays(discountDays);
                    discountFee += CalculateFeeAlteration(beginning, ending);
                }
            }

            //再处理不够24小时的优惠时数
            int workHour = discountHour - (discountDays * 24);
            //从最后停车时长不够一天的时间段开始计算
            begin = beginning.AddDays(parkingDays);
            end = ending;
            while (workHour > 0)
            {
                curHour = 0;
                //计算时间段优惠金额
                discountFee += CalculateDiscountMoneyTimeZone(begin, end, workHour, out curHour);
                workHour -= curHour;
                if (workHour > 0)
                {
                    //还有未处理的优惠时数时，向前推前一天再处理
                    end = begin;
                    begin = begin.AddDays(-1);
                }
            }

            #region 封顶收费判断
            //当有封顶收费时，需判断优惠后费用是否大于等于封顶收费
            if (FeeOfMax > 0)
            {
                decimal chargeFee = CalculateFee(beginning, ending);
                if (chargeFee - discountFee >= FeeOfMax)
                {
                    //大于等于封顶收费时，优惠金额为0
                    discountFee = 0;
                }
                else
                {
                    //小于封顶收费时，需要判断原来的费用是否大于封顶收费
                    if (chargeFee > FeeOfMax)
                    {
                        //如果大于，优惠金额为封顶费用-优惠后的收费
                        discountFee = FeeOfMax - (chargeFee - discountFee);
                    }
                }
            }
            #endregion

            if (discountFee < 0) discountFee = 0;

            return discountFee;
        }
        #endregion

        #endregion
    }
}

