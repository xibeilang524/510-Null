using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    [DataContract]
    public class YCTSetting
    {
        [DataMember]
        public string ServiceCode { get; set; }
        /// <summary>
        /// 获取或设置服务器管理的所有POS机
        /// </summary>
        [DataMember]
        public List<YCTItem> Items { get; set; }
    }

    /// <summary>
    /// 表示羊城通读头
    /// </summary>
    [DataContract]
    public class YCTItem
    {
        /// <summary>
        /// 获取或设置串口
        /// </summary>
        [DataMember]
        public int Comport { get; set; }
        /// <summary>
        /// 获取或设置通道ID，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public int? EntranceID { get; set; }
        /// <summary>
        /// 获取或设置说明信息
        /// </summary>
        [DataMember]
        public string Memo { get; set; }
    }
}
