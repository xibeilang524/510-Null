using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.LocalDataBase.Model
{
    public class LDB_CardPaymentRecordSearchCondition : CardPaymentRecordSearchCondition
    {
        /// <summary>
        /// 获取或设置查询记录的数据是否已导出标识
        /// </summary>
        public bool? UpdateFlag { get; set; }
    }
}
