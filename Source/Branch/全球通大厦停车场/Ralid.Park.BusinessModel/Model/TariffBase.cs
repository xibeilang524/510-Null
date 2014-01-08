using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 停车费率的基类
    /// </summary>
    [DataContract]
    [Serializable]
    [KnownType(typeof(TariffPerTime))]
    [KnownType(typeof(TariffPerDay))]
    [KnownType(typeof(TariffOfMidNight))]
    [KnownType(typeof(TariffOfTurning))]
    [KnownType(typeof(TariffOfLimitation))]
    [KnownType(typeof(TariffOfGuanZhou))]
    [KnownType(typeof(TariffOfMultTimezone))]
    [KnownType (typeof(TariffOfDixiakongjian))]
    public abstract class TariffBase
    {
        #region 虚拟方法
        /// <summary>
        /// 计算停车费用
        /// </summary>
        /// <param name="enter">入场时间</param>
        /// <param name="exit">出场时间</param>
        /// <returns></returns>
        public virtual decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            //throw new NotImplementedException("子类没有重写基类的CalculateFee方法");
            decimal fee = 0;
            decimal daysfee = 0;
            TimeSpan ts = new TimeSpan(ending.Ticks - beginning.Ticks);
            double totalMins = Math.Floor(ts.TotalMinutes);
            double calMins = 0; //已经计费的分钟数
            if (totalMins < 0) return 0;  //入场时间大于出场时间
            if (FreeMinutes > 0 && ts.TotalMinutes <= FreeMinutes) return 0;//小于免费停车时间
            if (FreeMinutes == 0 && ts.TotalMinutes == 0) return GetChargeUnitFee(beginning);//返回收费单元的费用

            DateTime begin = beginning;
            DateTime end = begin.AddDays(1);

            TariffBase tariff = this;

            //收费都是按24小时为一周期
            while (end <= ending)
            {
                daysfee += tariff.CalcalateCycleFee(calMins, begin, end);
                begin = end;
                end = begin.AddDays(1);
                calMins += 1440;

                while (Card != null && begin < ending)
                {
                    //开始一个收费周期
                    tariff = TariffSetting.Current.GetIntradayTariff(Card, CarType, begin);//获取下一个收费周期的费率
                    if (tariff != null || end >= ending) break;
                    begin = end;
                    end = begin.AddDays(1);
                    calMins += 1440;
                }
            }

            if (tariff != null)
            {
                fee = tariff.CalcalateIntradayFee(calMins, begin, ending);
            }

            fee = fee + daysfee;


            if (FeeOfMax > 0 && fee > FeeOfMax) fee = FeeOfMax;//有封顶费用

            return fee;
        }

        /// <summary>
        /// 计算一个计费周期的停车费用
        /// </summary>
        /// <param name="minutes">已计费的分钟数</param>
        /// <param name="beginning">计费周期的开始时间</param>
        /// <param name="ending">计费周期的结束时间</param>
        /// <returns></returns>
        public virtual decimal CalcalateCycleFee(double calMins, DateTime beginning, DateTime ending)
        {
            throw new NotImplementedException("子类没有重写基类的CalculateFee方法");
        }

        /// <summary>
        /// 计算一天内的停车费用
        /// </summary>
        /// <param name="calMins">已计费的分钟数</param>
        /// <param name="beginning">开始时间</param>
        /// <param name="ending">结束时间</param>
        /// <returns></returns>
        public virtual decimal CalcalateIntradayFee(double calMins, DateTime beginning, DateTime ending)
        {
            throw new NotImplementedException("子类没有重写基类的CalculateFee方法");
        }

        /// <summary>
        /// 计算一个收费单元的费用
        /// </summary>
        /// <param name="beginning">开始时间</param>
        /// <returns></returns>
        public virtual decimal GetChargeUnitFee(DateTime beginning)
        {
            throw new NotImplementedException("子类没有重写基类的CalculateFee方法");
        }
        #endregion

        


        /// <summary>
        /// 收费卡类型
        /// </summary>
        [DataMember]
        public Byte CardType { get; set; }

        /// <summary>
        /// 收费车型
        /// </summary>
        [DataMember]
        public Byte CarType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>
        [DataMember]
        public TariffType TariffType { get; set; }

        /// <summary>
        /// 获取或设置免费时间(分钟)
        /// </summary>
        [DataMember]
        public byte FreeMinutes { get; set; }

        /// <summary>
        /// 获取或设置无入场记录按次收费
        /// </summary>
        [DataMember]
        public bool ChargePerTimeWithoutEnter { get; set; }

        /// <summary>
        ///获取或设置无入场记录按次收费的收费金额
        /// </summary>
        [DataMember]
        public decimal FeeWithoutEnter { get; set; }

        /// <summary>
        /// 获取或设置每24小时最高收费,等于0时表示没有设置每24小时最高收费
        /// </summary>
        [DataMember]
        public decimal FeeOf24Hour { get; set; }

        /// <summary>
        /// 获取或设置封顶费用，即收费的最大费用，等于0时表示没有设置封顶费用
        /// </summary>
        [DataMember]
        public decimal FeeOfMax { get; set; }

        /// <summary>
        /// 获取或设置当前缴费的卡片
        /// </summary>
        public CardInfo Card { get; set; }
    }
}
