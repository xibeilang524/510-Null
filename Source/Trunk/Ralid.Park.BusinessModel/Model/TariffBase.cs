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
    [KnownType(typeof(TariffOfTurning))]
    [KnownType(typeof(TariffOfTurningLimited))]
    [KnownType(typeof(TariffOfLimitation))]
    [KnownType(typeof(TariffOfGuanZhou))]
    [KnownType(typeof(TariffOfDixiakongjian))]
    [KnownType(typeof(TariffOfThreeTimeZone))]
    public abstract class TariffBase
    {
        #region 公共属性
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
        #endregion

        #region 虚拟方法
        /// <summary>
        /// 计算停车费用
        /// </summary>
        /// <param name="enter">入场时间</param>
        /// <param name="exit">出场时间</param>
        /// <returns></returns>
        public virtual decimal CalculateFee(DateTime beginning, DateTime ending)
        {
            throw new NotImplementedException("子类没有重写基类的CalculateFee方法");
        }

        ///// <summary>
        ///// 计算一个计费周期的停车费用
        ///// </summary>
        ///// <param name="minutes">已计费的分钟数</param>
        ///// <param name="beginning">计费周期的开始时间</param>
        ///// <param name="ending">计费周期的结束时间</param>
        ///// <returns></returns>
        //public virtual decimal CalcalateCycleFee(double calMins, DateTime beginning, DateTime ending)
        //{
        //    throw new NotImplementedException("子类没有重写基类的CalcalateCycleFee方法");
        //}

        ///// <summary>
        ///// 计算一天内的停车费用
        ///// </summary>
        ///// <param name="calMins">已计费的分钟数</param>
        ///// <param name="beginning">开始时间</param>
        ///// <param name="ending">结束时间</param>
        ///// <returns></returns>
        //public virtual decimal CalcalateIntradayFee(double calMins, DateTime beginning, DateTime ending)
        //{
        //    throw new NotImplementedException("子类没有重写基类的CalcalateIntradayFee方法");
        //}

        ///// <summary>
        ///// 计算一个收费单元的费用
        ///// </summary>
        ///// <param name="beginning">开始时间</param>
        ///// <returns></returns>
        //public virtual decimal GetChargeUnitFee(DateTime beginning)
        //{
        //    throw new NotImplementedException("子类没有重写基类的GetChargeUnitFee方法");
        //}
        #endregion

        #region 重写基类方法
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is TariffBase)) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            else
            {
                TariffBase tb = obj as TariffBase;
                return this.CalculateFee(new DateTime(2011, 1, 1), new DateTime(2011, 1, 2)) == tb.CalculateFee(new DateTime(2011, 1, 1), new DateTime(2011, 1, 2));
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
