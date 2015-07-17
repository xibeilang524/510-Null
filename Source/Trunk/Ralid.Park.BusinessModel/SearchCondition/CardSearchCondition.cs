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
        /// 部门
        /// </summary>
        public string Department { get; set; }
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
        /// <summary>
        /// 获取或设置查询条件中的是否已上传到主数据库标识
        /// </summary>
        public bool? UpdateFlag { get; set; }
        /// <summary>
        /// 获取或设置车卡是否在场
        /// </summary>
        public bool? IsIn { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的车牌号码或入场车牌号
        /// </summary>
        public string CarPlateOrLast { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的名单类型
        /// </summary>
        public CardListType? ListType { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的名单车牌，必须完全一致
        /// </summary>
        public string ListCarPlate { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的分页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的分页页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 符合条件的记录总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 所有卡片记录总数
        /// </summary>
        public int TotalCountEx { get; set; }
        /// <summary>
        /// 脱机时在线处理的卡片
        /// </summary>
        public bool? OnlineHandleWhenOfflineMode { get; set; }
    }
}
