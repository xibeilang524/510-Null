using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Configuration;

namespace Ralid.Park.WebService
{
    public class AppConifg
    {
        private static AppConifg _Current = null;

        public static AppConifg Current
        {
            get
            {
                if (_Current == null) _Current = new AppConifg();
                return _Current;
            }
        }

        #region 公共属性
        /// <summary>
        /// 获取停车场数据库连接
        /// </summary>
        public string ParkingConnection
        {
            get
            {
                //Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
                //return cfg.ConnectionStrings.ConnectionStrings["ParkingConnection"].ConnectionString;

                string temp = ConfigurationManager.ConnectionStrings["ParkingConnection"].ConnectionString;
                return temp;
            }
        }

        /// <summary>
        /// 获取是否保存日志
        /// </summary>
        public bool Log
        {
            get
            {
                bool log = false;
                //Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
                //string temp = cfg.AppSettings.Settings["Log"].Value;
                string temp = ConfigurationManager.AppSettings["Log"];

                bool.TryParse(temp, out log);
                return log;
            }
        }

        /// <summary>
        /// 获取日志保存路径
        /// </summary>
        public string LogPath
        {
            get
            {
                //Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
                //string path = cfg.AppSettings.Settings["LogPath"].Value;
                string path = ConfigurationManager.AppSettings["LogPath"];
                if (string.IsNullOrEmpty(path))
                {
                    path = @"d:\RalidWebServiceLog";
                }
                return path;
            }
        }
        #endregion
    }
}
