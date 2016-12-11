using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.ETC.Response
{
    internal class GetCardNoResponse : ETCResponse
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 卡片类型
        /// </summary>
        public int CardType { get; set; }
        /// <summary>
        /// 卡片启用时间
        /// </summary>
        public string CardFirTime { get; set; }
        /// <summary>
        /// 卡片过期时间
        /// </summary>
        public string CardExpiryTime { get; set; }
        /// <summary>
        /// OBU启用日期
        /// </summary>
        public string OBUFirTime { get; set; }
        /// <summary>
        /// OBU到期日期
        /// </summary>
        public string OBUExpiryTime { get; set; }
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
    }
}
