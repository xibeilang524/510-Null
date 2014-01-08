using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.BusinessModel.Model
{
    //add by Jan 2012-3-29
    /// <summary>
    /// 停车折扣计费方式
    /// </summary>
    [DataContract]
    public class DiscountCalculateSetting
    {
        #region 静态方法
        private static DiscountCalculateSetting _Current;

        /// <summary>
        /// 获取或设置当前停车折扣计费方式
        /// </summary>
        public static DiscountCalculateSetting Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new DiscountCalculateSetting();
                }
                return _Current;
            }
            set
            {
                _Current = value;
            }
        }
        #endregion

        #region 构造函数
        public DiscountCalculateSetting()
        {
            ApplyDefault();
        }
        #endregion

        #region 公共方法
        public void ApplyDefault()
        {
            this.calculateType = DiscountCalculateType.Hour;
            this.couponValue = 0;
        }
        #endregion

        /// <summary>
        /// 计费方式
        /// </summary>
        [DataMember]
        public DiscountCalculateType calculateType { get; set; }

        /// <summary>
        /// 优惠劵金额
        /// </summary>
        [DataMember]
        public int couponValue { get; set; }


    }

    //end
}
