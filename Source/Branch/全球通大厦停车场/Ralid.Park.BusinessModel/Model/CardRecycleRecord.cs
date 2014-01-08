using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Model
{
    public class CardRecycleRecord
    {
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置回收日期
        /// </summary>
        public DateTime RecycleDateTime { get; set; }
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

        private byte _CardType;
        /// <summary>
        ///获取或设置卡片类型 
        /// </summary>
        public CardType CardType
        {
            get { return (CardType)_CardType; }
            set { _CardType = (byte)value; }
        }
        /// <summary>
        /// 获取或设置卡片原有余额
        /// </summary>
        public Decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置卡片原有效期
        /// </summary>
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 获取或设置卡片原有押金
        /// </summary>
        public Decimal Deposit { get; set; }
        /// <summary>
        /// 获取或设置退回金额
        /// </summary>
        public decimal RecycleMoney { get; set; }
        /// <summary>
        /// 获取或设置操作员
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置工作站
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

        public CardRecycleRecord Clone()
        {
            return this.MemberwiseClone() as CardRecycleRecord;
        }
    }
}
