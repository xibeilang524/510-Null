using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class WaitingCommandSearchCondition:SearchCondition 
    {
        /// <summary>
        /// 设置或获取控制板ID
        /// </summary>
        public int EntranceID { get; set; }
    }
}
