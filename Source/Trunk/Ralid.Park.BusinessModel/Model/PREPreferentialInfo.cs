using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 优惠信息实体类(存储卡片的优惠信息，但如果执行一笔优惠取消，则会删除一笔优惠信息)
    /// </summary>
    public class PREPreferentialInfo
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
        /// 本次优惠时间
        /// </summary>
        public DateTime PreferentialTime { get; set; }

        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public DateTime EntranceTime { get; set; }

        public PREPreferentialLog CreateLog()
        {
            PREPreferentialLog log = new PREPreferentialLog();
            log.PreferentialID = Guid.NewGuid();
            log.CardID = this.CardID;
            log.PreferentialHour = this.PreferentialHour;
            log.BusinessesName1 = this.BusinessesName1;
            log.BusinessesName2 = this.BusinessesName2;
            log.BusinessesName3 = this.BusinessesName3;
            log.BusinessesMoney1 = this.BusinessesMoney1;
            log.BusinessesMoney2 = this.BusinessesMoney2;
            log.BusinessesMoney3 = this.BusinessesMoney3;
            log.Notes = this.Notes;
            log.WorkstationID = this.WorkstationID;
            log.WorkstationName = this.WorkstationName;
            log.OperatorID = this.OperatorID;
            log.Operator = this.Operator;
            log.EntranceTime = this.EntranceTime;
            return log;
        }
    }
}
