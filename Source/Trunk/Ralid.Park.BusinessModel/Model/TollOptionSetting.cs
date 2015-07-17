using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 收费选项设置
    /// </summary>
    [DataContract]
    public class TollOptionSetting
    {
        private byte _PointCount;

        #region 构造函数
        public TollOptionSetting()
        {
            PointCount = 0;      //小数点0位
            FreeTimeAfterPay = 15;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 小数点数量
        /// </summary>
        [DataMember]
        public byte PointCount
        {
            get { return _PointCount; }
            set
            {
                if (value > 2 || value < 0)
                {
                    throw new InvalidOperationException(Resouce.Resource1.InvalidPointCount);
                }
                else
                {
                    _PointCount = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置收费后最多可以在停车场内呆多少分钟而不用收费
        /// </summary>
        [DataMember]
        public int FreeTimeAfterPay { get; set; }

        /// <summary>
        /// 获取或设置自定义的金额单位名称
        /// </summary>
        [DataMember]
        public string MoneyUnit { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 把系统最小金额单位的整形转换成单位为元表示的金额
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public decimal ToYuan(int money)
        {
            return ((decimal)money) / ((decimal)System.Math.Pow(10, PointCount));
        }

        /// <summary>
        /// 把以元表示的金额数转换成以系统最小金额单位的整形数
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public int FromYuan(decimal money)
        {
            return (int)(money * (decimal)System.Math.Pow(10, PointCount));
        }
        /// <summary>
        /// 把以元为单位的金额转化成字符表示的金额
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public string StrMoney(decimal money)
        {
            if (_PointCount == 0)
                return money.ToString("F0");
            else
                return money.ToString("F1");
        }
        /// <summary>
        /// 获取系统金额单位名单
        /// </summary>
        /// <returns></returns>
        public string GetMoneyUnit()
        {
            if (string.IsNullOrEmpty(MoneyUnit))
            {
                return Resouce.Resource1.Yuan;
            }
            else
            {
                return MoneyUnit;
            }
        }
        #endregion
    }
}

