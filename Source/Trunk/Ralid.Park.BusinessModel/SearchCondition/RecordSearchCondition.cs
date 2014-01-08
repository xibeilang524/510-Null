using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    /// <summary>
    /// 报表查询条件
    /// </summary>
    [Serializable]
    public class RecordSearchCondition : SearchCondition
    {
        /// <summary>
        /// 获取或设置查询条件中的卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置记录发生的时间范围
        /// </summary>
        public DateTimeRange RecordDateTimeRange { get; set; }
        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        public Ralid.Park.BusinessModel.Enum.CardType CardType { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的车型
        /// </summary>
        public byte? CarType { get; set; }
        /// <summary>
        /// 获取或设置收费方式
        /// </summary>
        public Ralid.Park.BusinessModel.Enum.PaymentMode? PaymentMode { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的工作站ID
        /// </summary>
        public string StationID { get; set; }        //工作站
        /// <summary>
        /// 获取或设置查询条件中的操作员
        /// </summary>
        public OperatorInfo Operator { get; set; }       //操作员
        /// <summary>
        /// 获取或设置记录是否已经结算过
        /// </summary>
        public DateTime? SettleDateTime { get; set; }
        /// <summary>
        /// 是否在不指定SettleDateTime时是表示只获取未结算的记录，否则返回结算和未结算的记录
        /// </summary>
        public bool? IsUnSettled { get; set; }
        /// <summary>
        /// 获取或设置查询条件中的事件车牌号
        /// </summary>
        public string CarPlate { get; set; }  //
        /// <summary>
        /// 获取或设置查询条件中的卡片编号
        /// </summary>
        public string CardCertificate { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置查询条件中记录是否已上去到主数据库
        /// </summary>
        public bool? UpdateFlag { get; set; }
    }
}
