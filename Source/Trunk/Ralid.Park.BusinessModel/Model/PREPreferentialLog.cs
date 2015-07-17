using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 优惠操作记录档(会存储所有优惠，取消优惠的操作信息)
    /// </summary>
    public class PREPreferentialLog
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid PreferentialID { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 本次优惠时数
        /// </summary>
        public int PreferentialHour { get; set; }
        /// <summary>
        /// 商家1
        /// </summary>
        public string BusinessesName1 { get; set; }
        /// <summary>
        /// 消费金额1
        /// </summary>
        public double BusinessesMoney1 { get; set; }
        /// <summary>
        /// 商家2
        /// </summary>
        public string BusinessesName2 { get; set; }
        /// <summary>
        /// 消费金额2
        /// </summary>
        public double BusinessesMoney2 { get; set; }
        /// <summary>
        /// 商家3
        /// </summary>
        public string BusinessesName3 { get; set; }
        /// <summary>
        /// 消费金额3
        /// </summary>
        public double BusinessesMoney3 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 当前工作站ID
        /// </summary>
        public Guid WorkstationID { get; set; }
        /// <summary>
        /// 当前工作站名
        /// </summary>
        public string WorkstationName { get; set; }
        /// <summary>
        /// 操作员登录名
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 优惠操作员
        /// </summary>
        public PREOperatorInfo Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatorTime { get; set; }
        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public DateTime EntranceTime { get; set; }
        /// <summary>
        /// 记录是否为优惠取消(0：优惠，1：取消优惠)
        /// </summary>
        public byte IsCancel { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
    }
}
