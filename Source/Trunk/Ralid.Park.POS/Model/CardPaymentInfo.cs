using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.POS.Model
{
    /// <summary>
    /// 卡片停车收费记录
    /// </summary>
    [Serializable]
    public class CardPaymentInfo
    {
        #region 公共属性
        /// <summary>
        /// 记录ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置计费出场时间
        /// </summary>
        public DateTime ChargeDateTime { get; set; }
        /// <summary>
        /// 获取或设置计费入场时间
        /// </summary>
        public DateTime? EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        public byte CardType { get; set; }
        /// <summary>
        /// 获取或设置收费车型
        /// </summary>
        public byte CarType { get; set; }
        /// <summary>
        /// 获取或设置收费类型
        /// </summary>
        public TariffType TariffType { get; set; }
        /// <summary>
        /// 获取或设置外车场由进场到缴费时所产生的停车费用
        /// </summary>
        public decimal ParkFee { get; set; }
        /// <summary>
        /// 此次收费之前累计已收
        /// </summary>
        public decimal LastTotalPaid { get; set; }
        /// <summary>
        /// 此次收费之前累计折扣
        /// </summary>
        public decimal LastTotalDiscount { get; set; }
        /// <summary>
        /// 获取或设置应收停车费用
        /// </summary>
        public decimal Accounts { get; set; }
        /// <summary>
        /// 获取或设置本次收取的费用
        /// </summary>
        public decimal Paid { get; set; }
        /// <summary>
        /// 获取或设置本次折扣额
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 获取或设置结算时间,没有进行结算时为空
        /// </summary>
        public DateTime? SettleDateTime { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public PaymentMode PaymentMode { get; set; }
        /// <summary>
        /// 获取或设置操作员编号
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 获取或设置工作站
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置是否是中央收费记录
        /// </summary>
        public bool? IsCenterCharge { get; set; }
        /// <summary>
        /// 获取或设置收费代码
        /// </summary>
        public PaymentCode PaymentCode { get; set; }
        /// <summary>
        /// 获取或设置收费说明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 获取或设置操作员卡编号（收费功能卡）
        /// </summary>
        public string OperatorCardID { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取停车时长
        /// </summary>
        public string TimeInterval
        {
            get
            {
                string ret = string.Empty;
                if (EnterDateTime != null)
                {
                    DatetimeInterval di = new DatetimeInterval(EnterDateTime.Value, ChargeDateTime);
                    return di.ToString();
                }
                return ret;
            }
        }

        /// <summary>
        /// 获取总共已收金额
        /// </summary>
        public decimal TotalPaid
        {
            get { return LastTotalPaid + Paid; }
        }

        /// <summary>
        /// 获取总共折扣金额
        /// </summary>
        public decimal TotalDiscount
        {
            get { return LastTotalDiscount + Discount; }
        }
        #endregion

        #region 公共方法
        public CardPaymentInfo Clone()
        {
            return this.MemberwiseClone() as CardPaymentInfo;
        }
        #endregion

        #region 写卡模式相关
        #region 实体字段
        ///// <summary>
        ///// 获取或设置内车场停车时长，单位分钟
        ///// </summary>
        //

        #endregion
        #endregion
    }
}
