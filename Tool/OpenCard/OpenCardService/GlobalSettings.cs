using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService
{
    public class GlobalSettings
    {
        #region 静态变量
        public static GlobalSettings Current { get; set; }
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
