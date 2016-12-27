using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    [DataContract]
    public class ETCDeviceInfo
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置IP地址
        /// </summary>
        [DataMember]
        public string IPAddr { get; set; }
        /// <summary>
        /// 获取或设置端口号
        /// </summary>
        [DataMember]
        public string Port { get; set; }
        /// <summary>
        /// 获取或设置超时时间(单位为秒)
        /// </summary>
        [DataMember]
        public string TimeOut { get; set; }
        /// <summary>
        /// 获取或设置心跳时间(单位为秒)
        /// </summary>
        [DataMember]
        public string HeartBeatTime { get; set; }
        /// <summary>
        /// 获取或设置用户名称
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// 获取或设置省份编号
        /// </summary>
        [DataMember]
        public string ProvinceNo { get; set; }
        /// <summary>
        /// 获取或设置城市编号
        /// </summary>
        [DataMember]
        public string CityNo { get; set; }
        /// <summary>
        /// 获取或设置区域编号
        /// </summary>
        [DataMember]
        public string AreaNo { get; set; }
        /// <summary>
        /// 获取或设置大门编号
        /// </summary>
        [DataMember]
        public string GateNo { get; set; }
        /// <summary>
        /// 获取或设置车道编号
        /// </summary>
        [DataMember]
        public string LaneNo { get; set; }
        /// <summary>
        /// 获取或设置天线ID
        /// </summary>
        [DataMember]
        public string EcRSUID { get; set; }
        /// <summary>
        /// 获取或设置读卡器ID
        /// </summary>
        [DataMember]
        public string EcReaderID { get; set; }
        /// <summary>
        /// 获取或设置所属的停车场通道ID,没有对应的通道时为零
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }
        #endregion
    }
}
