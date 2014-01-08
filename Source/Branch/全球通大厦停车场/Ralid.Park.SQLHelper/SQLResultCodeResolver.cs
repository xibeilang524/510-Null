using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.SQLHelper
{
    /// <summary>
    /// 表示返回结果的解析类,
    /// </summary>
    public class SQLResultCodeResolver
    {
        /// <summary>
        /// 从数据库异常的编号中解析执行结果
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static SQLResultCode GetFromSqlExceptionNumber(int number)
        {
            switch (number)
            {
                case (2):
                case (53):
                case (4060):
                case (18452):
                case (18456):
                case (10054):
                    return SQLResultCode.CannotConnectServer;
                default:
                    return SQLResultCode.Fail;
            }
        }
    }
}
