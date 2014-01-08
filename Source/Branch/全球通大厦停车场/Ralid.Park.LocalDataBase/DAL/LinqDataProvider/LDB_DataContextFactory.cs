using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data;

namespace Ralid.Park.LocalDataBase.DAL.LinqDataProvider
{
    public class LDB_DataContextFactory
    {
        //本地数据库
        public static LDB_DataContext CreateLDB(string connStr)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(connStr), "没有找到有效的数据库连接!");
            IDbConnection conn = new SQLiteConnection(connStr);
            LDB_DataContext ldb = new LDB_DataContext(conn);
            return ldb;
        }
    }
}
