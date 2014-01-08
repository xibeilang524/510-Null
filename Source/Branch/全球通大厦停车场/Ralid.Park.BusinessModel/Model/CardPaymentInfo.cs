using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;
using Ralid.Park.BusinessModel.Enum;
using Ralid.GeneralLibrary.Printer;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 卡片停车收费记录
    /// </summary>
    [Serializable]
    [DataContract]
    public class CardPaymentInfo
    {
        #region 私有变量
        [DataMember]
        private byte _CardType;
        #endregion

        #region 公共属性
        /// <summary>
        /// 记录ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置卡片的编号
        /// </summary>
        [DataMember]
        public string CardCertificate { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        [DataMember]
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置计费出场时间
        /// </summary>
        [DataMember]
        public DateTime ChargeDateTime { get; set; }
        /// <summary>
        /// 获取或设置计费入场时间
        /// </summary>
        [DataMember]
        public DateTime? EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        [DataMember]
        public CardType CardType
        {
            get
            {
                return CardType.GetSystemCardType(_CardType);
            }
            set
            {
                _CardType = (byte)value;
            }
        }
        /// <summary>
        /// 获取或设置收费车型
        /// </summary>
        [DataMember]
        public Byte CarType { get; set; }
        /// <summary>
        /// 获取或设置收费类型
        /// </summary>
        [DataMember]
        public TariffType TariffType { get; set; }
        /// <summary>
        /// 获取或设置卡片本次收费之前累计的实收金额
        /// </summary>
        [DataMember]
        public decimal LastTotalPaid { get; set; }
        /// <summary>
        /// 获取或设置卡片本次收费之前累计的折扣
        /// </summary>
        [DataMember]
        public decimal LastTotalDiscount { get; set; }
        /// <summary>
        /// 获取或设置应收停车费用
        /// </summary>
        [DataMember]
        public decimal Accounts { get; set; }
        /// <summary>
        /// 获取或设置本次收取的费用
        /// </summary>
        [DataMember]
        public decimal Paid { get; set; }
        /// <summary>
        /// 获取或设置本次折扣额
        /// </summary>
        [DataMember]
        public decimal Discount { get; set; }
        /// <summary>
        /// 获取或设置结算时间,没有进行结算时为空
        /// </summary>
        public DateTime? SettleDateTime { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        [DataMember]
        public PaymentMode PaymentMode { get; set; }
        /// <summary>
        /// 获取或设置卡片未用的优惠时数
        /// </summary>
        [DataMember]
        public int DiscountHour { get; set; }
        /// <summary>
        /// 获取或设置操作员编号
        /// </summary>
        [DataMember]
        public string  OperatorID { get; set; }
        /// <summary>
        /// 获取或设置工作站
        /// </summary>
        [DataMember]
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置上一次收费的工作站
        /// </summary>
        [DataMember]
        public string LastStationID { get; set; }
        /// <summary>
        /// 获取或设置是否是中央收费记录
        /// </summary>
        [DataMember]
        public bool? IsCenterCharge { get; set; }
        /// <summary>
        /// 获取或设置收费代码
        /// </summary>
        [DataMember]
        public PaymentCode PaymentCode { get; set; }
        /// <summary>
        /// 获取或设置收费说明
        /// </summary>
        [DataMember]
        public string Memo { get; set; }
        /// <summary>
        /// 获取或设置操作员卡编号（收费功能卡）
        /// </summary>
        [DataMember]
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

        /// <summary>
        /// 获取卡片本次收费之前累计的总共已收费用
        /// </summary>
        public decimal LastTotalFee
        {
            get { return LastTotalPaid + LastTotalDiscount; }
        }

        /// <summary>
        /// 获取卡片的总共应收费用（由进场到缴费时所产生的停车费用）
        /// </summary>
        public decimal TotalFee
        {
            get { return LastTotalFee + Accounts; }
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
        //[DataMember]
        //public int IndoorTimeInterval { get; set; }
        /// <summary>
        /// 获取或设置外车场由进场到缴费时所产生的停车费用
        /// </summary>
        [DataMember]
        public decimal ParkFee { get; set; }
        #endregion
        #endregion
    }
}
