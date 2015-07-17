using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.WebService
{
    /// <summary>
    /// WebService对外提供的卡片停车场收费信息类
    /// 为兼容其他平台调用，类属性基本使用string类型
    /// </summary>
    [Serializable]
    [DataContract]
    public class WSCardPaymentInfo
    {
        public WSCardPaymentInfo()
        {
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置计费时间
        /// </summary>
        [DataMember]
        public string ChargeDateTime { get; set; }
        /// <summary>
        /// 获取或设置入场时间
        /// </summary>
        [DataMember]
        public string EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置入口名称
        /// </summary>
        [DataMember]
        public string EntranceName { get; set; }
        /// <summary>
        /// 获取或设置停车时长
        /// </summary>
        [DataMember]
        public string TimeInterval { get; set; }
        /// <summary>
        /// 获取或设置优惠时长
        /// </summary>
        [DataMember]
        public string CurrDiscountHour { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置应收停车费用
        /// </summary>
        [DataMember]
        public string Accounts { get; set; }
        /// <summary>
        /// 获取卡片本次收费之前累计的总共已收费用(已付金额)
        /// </summary>
        [DataMember]
        public string LastTotalFee { get; set; }
        /// <summary>
        /// 获取或设置本次折扣额
        /// </summary>
        [DataMember]
        public string Discount { get; set; }
        /// <summary>
        /// 获取或设置本次收取的费用
        /// </summary>
        [DataMember]
        public string Paid { get; set; }
        /// <summary>
        /// 获取或设置收费费率
        /// </summary>
        [DataMember]
        public string Tariff { get; set; }
        /// <summary>
        /// 获取或设置收费说明
        /// </summary>
        [DataMember]
        public string Memo { get; set; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 设置停车场收费信息类各属性
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool SetWSCardPaymentInfo(CardPaymentInfo info)
        {
            try
            {
                CardID = info.CardID;
                ChargeDateTime = info.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                EnterDateTime = info.EnterDateTime.HasValue ? info.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                TimeInterval = info.TimeInterval;
                CurrDiscountHour = info.CurrDiscountHour.HasValue ? info.CurrDiscountHour.Value.ToString() : "0";
                CarPlate = info.CarPlate;
                Accounts = info.ParkFee.ToString("N");
                LastTotalFee = info.LastTotalFee.ToString("N");
                Discount = info.Discount.ToString("N");
                Paid = (info.Accounts - info.Discount).ToString("N");
                Tariff = string.Empty;
                Memo = info.Memo;

                return true;
            }
            catch
            {
            }
            return false;
        }
        #endregion
    }
}
