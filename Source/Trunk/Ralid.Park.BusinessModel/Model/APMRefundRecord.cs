using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 缴费机退款记录
    /// </summary>
    public class APMRefundRecord
    {
        #region 构造函数
        public APMRefundRecord()
        { 
        }
        public APMRefundRecord(CardInfo card)
        {
            CardID = card.CardID;
            OwnerName = card.OwnerName;
            CardCertificate = card.CardCertificate;
            CarPlate = card.CarPlate;
            _CardType = card.CardType.ID;
            EnterDateTime = card.LastDateTime;
            PaidDateTime = card.PaidDateTime;
            ParkFee=card.ParkFee;
            TotalPaidFee = card.TotalPaidFee;
        }
        #endregion

        #region 私有变量
        private byte _CardType;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置退款卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置退款时间
        /// </summary>
        public DateTime RefundDateTime { get; set; }
        /// <summary>
        /// 获取或设置缴费机编号
        /// </summary>
        public string MID { get; set; }
        /// <summary>
        /// 获取或设置缴费流水号
        /// </summary>
        public string PaymentSerialNumber { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置卡片编号
        /// </summary>
        public string CardCertificate { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 卡片类型
        /// </summary>
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
        /// 获取或设置入场时间
        /// </summary>
        public DateTime EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置缴费时间
        /// </summary>
        public DateTime? PaidDateTime { get; set; }
        /// <summary>
        /// 获取或设置停车费用
        /// </summary>
        public decimal ParkFee { get; set; }
        /// <summary>
        /// 获取或设置累计已缴的费用（包括支付的费用和折扣费用）
        /// </summary>
        public decimal TotalPaidFee { get; set; }
        /// <summary>
        /// 获取或设置退款金额
        /// </summary>
        public decimal RefundMoney { get; set; }
        /// <summary>
        /// 获取或设置操作员
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置工作站名
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置说明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 获取或设置结算时间,没有进行结算时为空
        /// </summary>
        public DateTime? SettleDateTime { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取一个复制体
        /// </summary>
        /// <returns></returns>
        public APMRefundRecord Clone()
        {
            return this.MemberwiseClone() as APMRefundRecord;
        }
        #endregion
    }
}
