using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 贵宾车辆状态
    /// </summary>
    public enum  VipVisitorStatus
    {
        /// <summary>
        /// 目前还没有进出记录
        /// </summary>
        Disactive=0,
        /// <summary>
        /// 目前已在场
        /// </summary>
        InPark=1,
        /// <summary>
        /// 目前已经出场
        /// </summary>
        ExitPark=2
    }
}
