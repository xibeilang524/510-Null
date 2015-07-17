using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 读卡器读卡模式枚举
    /// </summary>
    public enum ReaderReadMode
    {
        /// <summary>
        /// 读Mifare IC卡
        /// </summary>
        MifareIC = 0,
        /// <summary>
        /// 读SAM卡验证的CPU卡
        /// </summary>
        SAM = 1,
        /// <summary>
        /// 读固定密钥验证的CPU卡
        /// </summary>
        FixedKey = 2,
    }
}
