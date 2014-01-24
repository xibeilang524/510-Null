using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Parking.POS.Model
{
    public class AlarmInfo
    {
        /// <summary>
        /// 报警ID
        /// </summary>
        public int AlarmID { get; set; }
        /// <summary>
        /// 获取或设置报警发生时间
        /// </summary>
        public DateTime AlarmDateTime { get; set; }
        /// <summary>
        /// 获取或设置报警发生的控制器
        /// </summary>
        public string AlarmSource { get; set; }
        /// <summary>
        /// 获取或设置报警类型
        /// </summary>
        public AlarmType AlarmType { get; set; }
        /// <summary>
        /// 获取或设置报警说明
        /// </summary>
        public string AlarmDescr { get; set; }
        /// <summary>
        /// 获取或设置报警操作员
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置结算时间,没有进行结算时为空
        /// </summary>
        public DateTime? SettleDateTime { get; set; }

        public AlarmInfo  Clone()
        {
            return this.MemberwiseClone() as AlarmInfo;
        }
    }
}
