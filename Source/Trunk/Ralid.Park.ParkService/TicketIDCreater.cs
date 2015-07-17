using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.ParkService
{
    public class TicketIDCreater
    {
        #region 私有变量
        private static object _Locker7 = new object();
        private static int _LastID7; //生成的最近一个ID
        #endregion

        /// <summary>
        /// 生成7位的卡号，
        /// </summary>
        /// <returns></returns>
        public static string Create7CharCardID()
        {
            lock (_Locker7)
            {
                DateTime dt = DateTime.Now;
                int id = dt.Minute * 100000 + dt.Second * 1000 + dt.Millisecond;
                if (id < 1000000)
                {
                    id += 7000000;
                }
                if (_LastID7 == id)
                {
                    id += 1;
                }
                _LastID7 = id;
                return id.ToString();
            }
        }
    }
}
