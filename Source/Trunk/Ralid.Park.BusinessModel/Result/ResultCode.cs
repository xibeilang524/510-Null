using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Result
{
    /// <summary>
    /// 查询结果
    /// </summary>
    [Serializable]
    public enum ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Successful = 0,

        /// <summary>
        /// 参数错误
        /// </summary>
        ParameterError = 0x01,

        /// <summary>
        ///失败
        /// </summary>
        Fail = 0x02,

        /// <summary>
        /// 没有找到记录
        /// </summary>
        NoRecord = 0x03,

        /// <summary>
        /// 数据写入数据库时失败
        /// </summary>
        SaveDataError = 0x04,

        /// <summary>
        /// 连接服务器（数据库)失败
        /// </summary>
        CannotConnectServer = 0x05,

        /// <summary>
        /// 接口异常错误
        /// </summary>
        InterfaceException = 0xFF
    }
}
