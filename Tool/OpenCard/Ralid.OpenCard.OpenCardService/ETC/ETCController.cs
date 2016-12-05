using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices ;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    public class ETCController
    {
        #region 库函数封装
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pResult"></param>
        /// <param name="pLaneNum"></param>
        /// <param name="pErrMsg"></param>
        /// <returns></returns>
        [DllImport ("EtcController")]
        public static extern int Initialize(StringBuilder pResult, ref int pLaneNum, StringBuilder pErrMsg);

        /// <summary>
        /// 反初始化
        /// </summary>
        /// <returns></returns>
        [DllImport("EtcController")]
        public static extern int  Uninstall();
        #endregion
    }
}
