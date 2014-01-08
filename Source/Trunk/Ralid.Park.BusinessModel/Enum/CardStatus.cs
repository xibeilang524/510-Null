using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    public enum CardStatus
    {
        /// <summary>
        /// 此卡不未登记(在系统中不存在)
        /// </summary>
        UnRegister = 0,
        /// <summary>
        /// 已发行
        /// </summary>
        Enabled = 1,
        /// <summary>
        /// 8-已删除
        /// </summary>
        Deleted = 2,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled = 3,
        /// <summary>
        /// 挂失
        /// </summary>
        Loss = 4,
        /// <summary>
        /// 待发行
        /// </summary>
        Recycled = 5,
    }
}
