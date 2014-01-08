using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    /// <summary>
    /// 出入控制器的查询条件
    /// </summary>
    public class EntranceSearchCondition : SearchCondition
    {
        /// <summary>
        /// 控制器所属停车场
        /// </summary>
        public int ParkID { get; set; }
        /// <summary>
        /// 控制器物理地址
        /// </summary>
        public int EntranceID { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        public string EntranceName { get; set; }
    }
}
