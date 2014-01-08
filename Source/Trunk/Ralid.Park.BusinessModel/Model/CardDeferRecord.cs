using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Model
{
    public class CardDeferRecord
    {
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置延期操作发生时间
        /// </summary>
        public DateTime DeferDateTime { get; set; }
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
        /// 获取或设置旧的有效期
        /// </summary>
        public DateTime OriginalDate { get; set; }
        /// <summary>
        /// 获取或设置延期后的有效期
        /// </summary>
        public DateTime CurrentDate { get; set; }
        /// <summary>
        /// 获取或设置收费方式
        /// </summary>
        public PaymentMode PaymentMode { get; set; }
        /// <summary>
        /// 获取或设置延期操作所收金额
        /// </summary>
        public decimal DeferMoney { get; set; }

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

        public CardDeferRecord Clone()
        {
            return this.MemberwiseClone() as CardDeferRecord;
        }
    }
}
