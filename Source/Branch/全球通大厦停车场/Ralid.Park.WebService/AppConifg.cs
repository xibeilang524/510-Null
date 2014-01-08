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
        public string ParkingConnection
        {
            get
            {
                Configuration cfg = WebConfigurationManager.OpenWebConfiguration("~");
                return cfg.ConnectionStrings.ConnectionStrings["ParkingConnection"].ConnectionString;
            }
        }
        #endregion
    }
}
