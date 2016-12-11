using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.ETC.Response
{
    internal class GetCardInfoResponse : ETCResponse
    {
        #region 卡片属性
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 卡片余额（单位：分）
        /// </summary>
        public long Balance { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CardPlate { get; set; }
        /// <summary>
        /// 车牌颜色
        /// </summary>
        public string CardPlateColor { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public int CardVehClass { get; set; }
        /// <summary>
        /// 车辆用户类型
        /// </summary>
        public int CardVehUserType { get; set; }
        #endregion

        #region 卡片上次进出属性
        /// <summary>
        /// 入/出口小区编码
        /// </summary>
        public string CardAreaNo { get; set; }
        /// <summary>
        /// 入/出口大门编码
        /// </summary>
        public string CardGateNo { get; set; }
        /// <summary>
        /// 入/出口车道编码
        /// </summary>
        public string CardLaneNo { get; set; }
        /// <summary>
        /// 入/出口时间
        /// </summary>
        public string PassTime { get; set; }
        /// <summary>
        /// 入/出口车牌
        /// </summary>
        public string VehPlate { get; set; }
        /// <summary>
        /// 入/出口车型
        /// </summary>
        public int VehType { get; set; }
        /// <summary>
        /// 入/出口车种
        /// </summary>
        public int VehClass { get; set; }
        /// <summary>
        /// 入/出口标识（从卡片001A文件中获取）1：已做出口处理  0：未做出口处理
        /// </summary>
        public int OutFlag { get; set; }
        /// <summary>
        /// 入/出口收费员工号
        /// </summary>
        public string OperatorNo { get; set; }
        #endregion

        #region 广东接口特有属性
        /// <summary>
        /// 小园小门编码
        /// </summary>
        public string LittleGateNo { get; set; }
        /// <summary>
        /// 小园车道编码
        /// </summary>
        public string LittleLaneNo { get; set; }
        /// <summary>
        /// 小园通过日期时间
        /// </summary>
        public string LittlePassTime { get; set; }
        /// <summary>
        /// 小园累计金额
        /// </summary>
        public string LittleCashMoney { get; set; }
        /// <summary>
        /// 小园累计时间
        /// </summary>
        public string LittleTime { get; set; }
        /// <summary>
        /// 优惠类型
        /// </summary>
        public string OfferType { get; set; }
        /// <summary>
        /// 优惠时间
        /// </summary>
        public string OfferTime { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public string BackUp { get; set; }
        /// <summary>
        /// 校验码
        /// </summary>
        public string CheckCode { get; set; }
        #endregion
    }
}
