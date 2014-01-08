using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Ralid.Park.SQLHelper
{
    /// <summary>
    /// 实现执行SQL语句的提供者类
    /// </summary>
    public class SQLHelperProvider
    {

        #region 构造函数
        public SQLHelperProvider(string connStr)
        {
            _ConnStr = connStr;
        }
        #endregion

        #region 私有变量
        private string _ConnStr;
        #endregion

        #region 实现ISQLProvider接口
        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public SQLQueryResult<int> SQLExecuteNonQuery(string cmdText)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, con))
                    {
                        con.Open();
                        int count = cmd.ExecuteNonQuery();
                        con.Close();
                        return new SQLQueryResult<int>(SQLResultCode.Successful, count);
                    }
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLQueryResult<int>(SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number), ex.Message, 0);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLQueryResult<int>(SQLResultCode.Fail, ex.Message, 0);
            }
        }

        /// <summary>
        /// 执行SQL语句或存储过程，返回受影响的行数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SQLQueryResult<int> SQLExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnStr))
                {
                    cmd.Connection = con;
                    con.Open();
                    int count = cmd.ExecuteNonQuery();
                    con.Close();
                    return new SQLQueryResult<int>(SQLResultCode.Successful, count);
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLQueryResult<int>(SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number), ex.Message, 0);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLQueryResult<int>(SQLResultCode.Fail, ex.Message, 0);
            }
        }

        /// <summary>
        /// 执行多个SQL语句
        /// </summary>
        /// <param name="cmdTexts">SQL语句</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns></returns>
        public SQLCommandResult SQLExecuteNonQuery(List<string> cmdTexts, bool transaction)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnStr))
                {
                    con.Open();
                    if (transaction)
                    {
                        using (SqlTransaction tran = con.BeginTransaction())
                        {
                            using (SqlCommand cmd = con.CreateCommand())
                            {
                                cmd.Connection = con;
                                cmd.Transaction = tran;
                                foreach (string cmdText in cmdTexts)
                                {
                                    cmd.CommandText = cmdText;
                                    cmd.ExecuteNonQuery();
                                }
                                tran.Commit();
                                con.Close();
                                return new SQLCommandResult(SQLResultCode.Successful);
                            }
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.Connection = con;
                            foreach (string cmdText in cmdTexts)
                            {
                                cmd.CommandText = cmdText;
                                cmd.ExecuteNonQuery();
                            }
                            con.Close();
                            return new SQLCommandResult(SQLResultCode.Successful);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLCommandResult(SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number), ex.Message);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLCommandResult(SQLResultCode.Fail, ex.Message);
            }
        }

        /// <summary>
        /// 执行多个SQL语句或存储过程
        /// </summary>
        /// <param name="cmds">SQL语句或存储过程</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns></returns>
        public SQLCommandResult SQLExecuteNonQuery(List<SqlCommand> cmds, bool transaction)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_ConnStr))
                {
                    con.Open();
                    if (transaction)
                    {
                        using (SqlTransaction tran = con.BeginTransaction())
                        {
                            foreach (SqlCommand cmd in cmds)
                            {
                                cmd.Connection = con;
                                cmd.Transaction = tran;
                                cmd.ExecuteNonQuery();
                            }
                            tran.Commit();
                            con.Close();
                            return new SQLCommandResult(SQLResultCode.Successful);
                        }
                    }
                    else
                    {
                        foreach (SqlCommand cmd in cmds)
                        {
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                        return new SQLCommandResult(SQLResultCode.Successful);
                    }
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLCommandResult(SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number), ex.Message);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new SQLCommandResult(SQLResultCode.Fail, ex.Message);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public SQLQueryResult<object> SQLExecuteScalar(string cmdText)
        {
            SQLQueryResult<object> result = new SQLQueryResult<object>();

            try
            {
                using (SqlConnection con = new SqlConnection(_ConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, con))
                    {
                        con.Open();
                        object o = cmd.ExecuteScalar();
                        con.Close();
                        result.Result = SQLResultCode.Successful;
                        result.Message = string.Empty;
                        result.QueryObject = o;
                    }
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number);
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCode.Fail;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 执行查询或存储过程，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SQLQueryResult<object> SQLExecuteScalar(SqlCommand cmd)
        {
            SQLQueryResult<object> result = new SQLQueryResult<object>();

            try
            {
                using (SqlConnection con = new SqlConnection(_ConnStr))
                {
                    cmd.Connection = con;
                    con.Open();
                    object o = cmd.ExecuteScalar();
                    con.Close();
                    result.Result = SQLResultCode.Successful;
                    result.Message = string.Empty;
                    result.QueryObject = o;
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number);
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCode.Fail;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 执行查询，并返回一个 SqlDataReader 对象，注意使用完 SqlDataReader 对象后必须使用close释放SqlDataReader连接
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public SQLQueryResult<SqlDataReader> SQLExecuteReader(string cmdText)
        {
            SQLQueryResult<SqlDataReader> result = new SQLQueryResult<SqlDataReader>();
            SqlConnection con = new SqlConnection(_ConnStr);
            try
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    con.Open();
                    result.QueryObject = cmd.ExecuteReader();
                    result.Result = SQLResultCode.Successful;
                    result.Message = string.Empty;
                }
            }
            catch (SqlException ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number);
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCode.Fail;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 执行查询或存储过程，并返回一个 SqlDataReader 对象，注意使用完 SqlDataReader 对象后必须使用close释放SqlDataReader连接
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SQLQueryResult<SqlDataReader> SQLExecuteReader(SqlCommand cmd)
        {
            SQLQueryResult<SqlDataReader> result = new SQLQueryResult<SqlDataReader>();
            SqlConnection con = new SqlConnection(_ConnStr);
            try
            {
                cmd.Connection = con;
                con.Open();
                result.QueryObject = cmd.ExecuteReader();
                result.Result = SQLResultCode.Successful;
                result.Message = string.Empty;
            }
            catch (SqlException ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number);
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCode.Fail;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 执行查询，并返回一个 DataTable 对象。
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public SQLQueryResult<DataTable> SQLExecuteTable(string cmdText)
        {
            SQLQueryResult<DataTable> result = new SQLQueryResult<DataTable>();
            SqlConnection con = new SqlConnection(_ConnStr);
            try
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        result.QueryObject = ds.Tables[0];
                        result.Result = SQLResultCode.Successful;
                        result.Message = string.Empty;
                    }
                }
            }
            catch (SqlException ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number);
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCode.Fail;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 执行查询或存储过程，并返回一个 DataTable 对象。
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SQLQueryResult<DataTable> SQLExecuteTable(SqlCommand cmd)
        {
            SQLQueryResult<DataTable> result = new SQLQueryResult<DataTable>();
            SqlConnection con = new SqlConnection(_ConnStr);
            try
            {
                cmd.Connection = con;
                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    result.QueryObject = ds.Tables[0];
                    result.Result = SQLResultCode.Successful;
                    result.Message = string.Empty;
                }
            }
            catch (SqlException ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCodeResolver.GetFromSqlExceptionNumber(ex.Number);
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                con.Close();
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result.Result = SQLResultCode.Fail;
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion
    }
}
