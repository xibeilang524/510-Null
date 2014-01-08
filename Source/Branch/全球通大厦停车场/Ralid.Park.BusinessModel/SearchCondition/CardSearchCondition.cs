using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class CardSearchCondition:SearchCondition 
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 卡片类型
        /// </summary>
        public CardType CardType { get; set; }
        /// <summary>
        /// 卡片状态
        /// </summary>
        public CardStatus? Status { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public Byte? CarType { get; set; }
        /// <summary>
        /// 权限组号
        /// </summary>
        public int? AccessID { get; set; }
        /// <summary>
        /// 车主名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 卡片编号
        /// </summary>
        public string CardCertificate { get; set; }
        /// <summary>
        /// 入场车牌号
        /// </summary>
        public string LastCarPlate { get; set; }
        /// <summary>
        /// 卡片在场状态
        /// </summary>
        public ParkingStatus? ParkingStatus { get; set; }
        /// <summary>
        /// 获取或设置查询条件中卡片的最近一次刷卡时间的时间范围
        /// </summary>
        public DateTimeRange LastDateTime { get; set; }
    }
}
