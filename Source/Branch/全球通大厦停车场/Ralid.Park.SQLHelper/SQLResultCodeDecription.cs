using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.SQLHelper
{
    public class SQLResultCodeDecription
    {
        /// <summary>
        /// 获取命令返回结果的文字描述
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetDescription(SQLResultCode result)
        {
            switch (result)
            {
                case SQLResultCode.CannotConnectServer:
                    return "连接服务器（数据库)失败";
                case SQLResultCode.Fail:
                    return "失败";
                case SQLResultCode.NoRecord:
                    return "没有找到记录";
                case SQLResultCode.SaveDataError:
                    return "数据写入数据库时失败";
                case SQLResultCode.Successful:
                    return "成功";
                default:
                    return "失败";
            }
        }
    }
}
