using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService
{
    public class GlobalSettings
    {
        #region 静态变量
        private static GlobalSettings _Instance;

        public static GlobalSettings Current
        {
            get
            {
                if (_Instance == null) _Instance = new GlobalSettings();
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }
        #endregion

        #region 构造函数
        public GlobalSettings()
        {
        }
        #endregion

        private Dictionary<Type, object> _Settings = new Dictionary<Type, object>();

        #region 公共属性及方法
        public T Get<T>() where T : class
        {
            if (_Settings.ContainsKey(typeof(T)))
            {
                object o = _Settings[typeof(T)];
                return o as T;
            }
            return null;
        }

        public void Set<T>(T s) where T : class
        {
            _Settings[typeof(T)] = s;
        }
        #endregion
    }
}
