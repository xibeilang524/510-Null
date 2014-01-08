using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class CardChargeRecord
    {
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public String CardID { get; set; }
        /// <summary>
        /// 获取或设置充值日期
        /// </summary>
        public DateTime ChargeDateTime{get;set;}
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
        ///获取或设置充值金额
        /// </summary>
        public decimal ChargeAmount { get; set; }
        /// <summary>
        /// 获取或设置实收金额
        /// </summary>
        public decimal Payment { get; set; }
        /// <summary>
        /// 获取或设置充值后的卡片余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置卡片充值后的有效期截止日期
        /// </summary>
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 获取或设置收费方式
        /// </summary>
        public Ralid.Park.BusinessModel .Enum. PaymentMode PaymentMode{get;set;}
        /// <summary>
        /// 获取或设置收费操作员
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

        public CardChargeRecord Clone()
        {
            return this.MemberwiseClone() as CardChargeRecord;
        }
    }
}
