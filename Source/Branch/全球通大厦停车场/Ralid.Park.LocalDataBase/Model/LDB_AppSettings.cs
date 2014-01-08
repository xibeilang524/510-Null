using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.Park.LocalDataBase.Model
{
    public class LDB_AppSettings
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置当前设置
        /// </summary>
        public static LDB_AppSettings Current
        {
            get
            {
                if (_instance == null) _instance = new LDB_AppSettings();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        #endregion

        #region 私有变量
        private static LDB_AppSettings _instance = null;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取本地数据库连接字符串
        /// </summary>
        public string LDBConnect
        {
            get
            {
                if (!System.IO.Directory.Exists(System.IO.Path.Combine(Application.StartupPath, "LDB")))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Application.StartupPath, "LDB"));
                }
                if (!System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "LDB\\RalidParking.db")))
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "RalidParking_New.db")))
                    {
                        System.IO.File.Copy(System.IO.Path.Combine(Application.StartupPath, "RalidParking_New.db"), System.IO.Path.Combine(Application.StartupPath, "LDB\\RalidParking.db"));
                    }
                }

                if (System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "LDB\\RalidParking.db")))
                {
                    System.Data.SQLite.SQLiteConnectionStringBuilder builder = new System.Data.SQLite.SQLiteConnectionStringBuilder();
                    builder.DataSource = System.IO.Path.Combine(Application.StartupPath, "LDB\\RalidParking.db");
                    builder.Password = "ralid888";

                    return builder.ConnectionString; ;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion
    }
}
