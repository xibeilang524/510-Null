using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示一条自助缴费机产生的日志记录
    /// </summary>
    public class APMLog
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置自助缴机日志ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///  获取或设置自助缴机日志流水号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        ///  获取或设置自助缴机日志时间
        /// </summary>
        public DateTime LogDateTime { get; set; }
        /// <summary>
        /// 获取或设置日志类型
        /// </summary>
        public APMLogType LogType { get; set; }
        /// <summary>
        ///  获取或设置自助缴机日志卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        ///  获取或设置自助缴机日志描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///  获取或设置自助缴机编号
        /// </summary>
        public string MID { get; set; }
        /// <summary>
        ///  获取或设置操作员ID
        /// </summary>
        public string OperatorID { get; set; }
        #endregion
    }
}
