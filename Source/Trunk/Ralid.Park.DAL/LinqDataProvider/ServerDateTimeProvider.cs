using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.LinqDataProvider
{
    /// <summary>
    /// 获取服务器时间的提供者类
    /// </summary>
    public class ServerDateTimeProvider : IServerDatetimeProvider
    {
        #region 构造函数
        public ServerDateTimeProvider(string connStr)
        {
            _ConnStr = connStr;
        }
        #endregion

        #region 私有变量
        private string _ConnStr;
        #endregion


        #region 实现IServerDateTimeProvider 接口
        /// <summary>
        /// 获取服务器时间，如果执行失败，serverDT 返回null 
        /// </summary>
        /// <param name="serverDT"></param>
        /// <returns></returns>
        public CommandResult GetServerDateTime(out DateTime? serverDT)
        {
            try
            {
                if (!string.IsNullOrEmpty(_ConnStr))
                {
                    using (SqlConnection con = new SqlConnection(_ConnStr))
                    {
                        using (SqlCommand cmd = new SqlCommand("select getdate() ", con))
                        {
                            con.Open();
                            object o = cmd.ExecuteScalar();
                            serverDT = Convert.ToDateTime(o);
                            return new CommandResult(ResultCode.Successful, string.Empty);
                        }
                    }
                }
                else
                {
                    serverDT = null;
                    return new CommandResult(ResultCode.Fail);
                }
            }
            catch (SqlException ex)
            {
                serverDT = null;
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCodeResolver.GetFromSqlExceptionNumber(ex.Number), ex.Message);
            }
            catch (Exception ex)
            {
                serverDT = null;
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }
        #endregion
    }
}
