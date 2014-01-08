using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Model
{
    [Serializable]
    public class CardReleaseRecord
    {
        private byte _CardType;

        #region 公共属性
        /// <summary>
        /// 获取或设置卡片发行时间
        /// </summary>
        public DateTime ReleaseDateTime { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
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
        /// 获取或设置卡片类型
        /// </summary>
        public CardType CardType
        {
            get { return CardType.GetSystemCardType(_CardType); }
            set { _CardType = (byte)value; }
        }
        /// <summary>
        /// 获取或设置卡片发行实际收取的费用(包括押金)
        /// </summary>
        public decimal ReleaseMoney { get; set; }
        /// <summary>
        /// 获取或设置收费方式
        /// </summary>
        public PaymentMode PaymentMode { get; set; }
        /// <summary>
        /// 获取或设置卡片发行的初始余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置卡片的生效日期
        /// </summary>
        public DateTime ActivationDate { get; set; }
        /// <summary>
        /// 获取或设置卡片发行后的有效期
        /// </summary>
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 获取或设置卡片节假日是否可进出
        /// </summary>
        public bool HolidayEnabled { get; set; }
        /// <summary>
        /// 获取或设置卡片发行收取的押金
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 获取或设置卡片发行操作员
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置卡片发行操作的工作站
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置卡片发行说明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 获取或设置结算时间,没有进行结算时为空
        /// </summary>
        public DateTime? SettleDateTime { get; set; }
        #endregion

        public CardReleaseRecord Clone()
        {
            return this.MemberwiseClone() as CardReleaseRecord;
        }

    }
}
