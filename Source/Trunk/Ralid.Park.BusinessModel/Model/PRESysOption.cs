using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    public class PRESysOption
    {
        /// <summary>
        /// 当前优惠工作站ID
        /// </summary>
        public Guid CurrentWorkstationID { get; set; }
        /// <summary>
        /// 当前优惠工作站
        /// </summary>
        public string CurrentWorkstation { get; set; }
        /// <summary>
        /// 允许手动输入优惠时数(1：是 ， 0：否)
        /// </summary>
        public byte IsAllowDefineHour { get; set; }
        /// <summary>
        /// 最大优惠时数
        /// </summary>
        public int MaxHour { get; set; }
    }
}
