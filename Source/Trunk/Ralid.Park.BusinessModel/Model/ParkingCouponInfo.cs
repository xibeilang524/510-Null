using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示停车优惠券信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class ParkingCouponInfo
    {
        #region 构造函数
        public ParkingCouponInfo()
        { 
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 优惠券名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 优惠券面值
        /// </summary>
        [DataMember]
        public decimal ParValue { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 计算优惠券优惠金额
        /// </summary>
        /// <param name="couponCount">优惠券张数</param>
        /// <returns></returns>
        public decimal CalculateDiscount(int couponCount)
        {
            return couponCount * ParValue;
        }
        #endregion
    }
}
