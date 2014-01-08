using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 车牌对比结果
    /// </summary>
    public enum CarPlateComparisonResult
    {
        /// <summary>
        /// 车牌对比成功
        /// </summary>
        Success = 0x00,

        /// <summary>
        /// 车牌对比失败
        /// </summary>
        Fail = 0x01,

        /// <summary>
        /// 不进行车牌对比
        /// </summary>
        Noncontrastive = 0xFF
    }
}
