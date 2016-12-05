using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    public class ETCDevice
    {
        #region 构造函数
        public ETCDevice()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置IP地址
        /// </summary>
        public string IPAddr { get; set; }
        /// <summary>
        /// 获取或设置端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 获取或设置超时时间(单位为秒)
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// 获取或设置心跳时间(单位为秒)
        /// </summary>
        public int HeartBeatTime { get; set; }
        /// <summary>
        /// 获取或设置用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 获取或设置省份编号
        /// </summary>
        public string ProvinceNo { get; set; }
        /// <summary>
        /// 获取或设置城市编号
        /// </summary>
        public string CityNo { get; set; }
        /// <summary>
        /// 获取或设置区域编号
        /// </summary>
        public string AreaNo { get; set; }
        /// <summary>
        /// 获取或设置大门编号
        /// </summary>
        public string GateNo { get; set; }
        /// <summary>
        /// 获取或设置车道编号
        /// </summary>
        public int LaneNo { get; set; }
        /// <summary>
        /// 获取或设置天线ID
        /// </summary>
        public int EcRSUID { get; set; }
        /// <summary>
        /// 获取或设置读卡器ID
        /// </summary>
        public int EcReaderID { get; set; }
        #endregion
    }
}
