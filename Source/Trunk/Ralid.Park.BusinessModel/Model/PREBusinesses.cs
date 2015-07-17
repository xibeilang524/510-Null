using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 商家信息的实体类(优惠录入系统)
    /// </summary>
    public class PREBusinesses
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid BusinessesID { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string BusinessesName { get; set; }
        
        /// <summary>
        /// 商家描述
        /// </summary>
        public string BusinessesDesc { get; set; }
    }
}
