using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 工作站信息的实体类(优惠录入系统)
    /// </summary>
    public class PREWorkstation
    {
        public Guid WorkstationID { get; set; }
        /// <summary>
        /// 工作站名称
        /// </summary>
        public string WorkstationName { get; set; }
        /// <summary>
        /// 工作站描述
        /// </summary>
        public string WorkstationDesc { get; set; }
    }
}
