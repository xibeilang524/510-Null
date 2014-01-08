using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.WebService
{
    /// <summary>
    /// 接口返回代码
    /// </summary>
    public enum InterfaceReturnCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0x00,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParameterError = 0x01,
        /// <summary>
        /// 失败
        /// </summary>    
        DatabaseError = 0x02,
        /// <summary>
        /// 没有找到记录
        /// </summary>
        DatabaseNoRecord = 0x03,
        /// <summary>
        /// 数据写入数据库时失败
        /// </summary>
        DatabaseSaveDataError = 0x04,
        /// <summary>
        /// 连接数据库失败
        /// </summary>
        CannotConnectDatabase = 0x05,
        /// <summary>
        /// 没有找到停车地点权限
        /// </summary>
        NoPlaceAccess = 0x06,

        /// <summary>
        /// 接口异常错误
        /// </summary>
        InterfaceException=0xFF
    }
}
