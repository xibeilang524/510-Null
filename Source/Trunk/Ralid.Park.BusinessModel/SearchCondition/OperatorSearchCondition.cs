using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    /// <summary>
    /// 在数据库中查找操作员的条件
    /// </summary>
    public class OperatorSearchCondition:SearchCondition 
    {
        /// <summary>
        /// 操作员的角色
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 操作员编号
        /// </summary>
        public byte? OperatorNum { get; set; }
    }
}
