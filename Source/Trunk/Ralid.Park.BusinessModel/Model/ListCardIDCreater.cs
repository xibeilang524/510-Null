using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 名单ID卡号创建者
    /// </summary>
    public class ListCardIDCreater
    {
        #region 私有变量
        private static object _IDLocker = new object();
        private static int _LastID; //生成的最近一个ID
        #endregion

        /// <summary>
        /// 生成名单的卡号
        /// </summary>
        /// <returns></returns>
        public static string CreateListCardID()
        {
            lock (_IDLocker)
            {
                DateTime dt = DateTime.Now;
                int id = dt.Day * 10000000 + dt.Minute * 100000 + dt.Second * 1000 + dt.Millisecond;
                if (_LastID == id)
                {
                    id += 1;
                }
                _LastID = id;
                return id.ToString();
            }
        }
    }
}
