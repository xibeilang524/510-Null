using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

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
        [DllImport("EtcController")]
        private static extern int Initialize(StringBuilder pResult, ref int pLaneNum, StringBuilder pErrMsg);
        /// <summary>
        /// 向指定通道发送心跳包
        /// </summary>
        /// <param name="iLaneNo"></param>
        /// <returns></returns>
        [DllImport("EtcController")]
        private static extern int HeartBeat(int iLaneNo); 
        /// <summary>
        /// 反初始化
        /// </summary>
        /// <returns></returns>
        [DllImport("EtcController")]
        private static extern int Uninstall();
        #endregion

        public ETCDevice[] ETCDevices { get; set; }

        #region 公共方法
        public void Init()
        {
            try
            {
                ETCDevices = null;
                StringBuilder pRet = new StringBuilder(100 * 1000);
                StringBuilder err = new StringBuilder(1000);
                int count = 0;
                Initialize(pRet, ref count, err);
                if (count > 0)
                {
                    var str = pRet.ToString().Trim();
                    ETCDevices = JsonConvert.DeserializeObject<ETCDevice[]>(str);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        public int HeartBeatEx(int laneNo)
        {
            return HeartBeat(laneNo);
        }
        public void UnInit()
        {
            ETCDevices = null;
            Uninstall();
        }
        #endregion
    }
}
