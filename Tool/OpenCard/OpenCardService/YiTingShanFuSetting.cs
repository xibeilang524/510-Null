using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ralid.OpenCard.OpenCardService
{
    /// <summary>
    /// 表示驿停车闪付配置参数
    /// </summary>
    [DataContract]
    public class YiTingShanFuSetting
    {
        #region 构造函数
        public YiTingShanFuSetting()
        {
            Items = new List<YiTingPOS>();
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置驿停服务器IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 获取或设置驿停服务器端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 获取或设置服务器管理的所有POS机
        /// </summary>
        [DataMember]
        public List<YiTingPOS> Items { get; set; }
        /// <summary>
        /// 获取是否存在某个ID的POS机
        /// </summary>
        /// <param name="posID"></param>
        /// <returns></returns>
        public bool HasReader(string posID)
        {
            if (Items != null) return Items.Exists(item => item.ID == posID);
            return false;
        }
        /// <summary>
        /// 获取ID为指定值的POS机
        /// </summary>
        /// <param name="posID"></param>
        /// <returns></returns>
        public YiTingPOS GetReader(string posID)
        {
            if (Items != null) return Items.FirstOrDefault(item => item.ID == posID);
            return null;
        }
        #endregion
    }

    /// <summary>
    /// 表示驿停POS机
    /// </summary>
    [DataContract]
    public class YiTingPOS
    {
        /// <summary>
        /// 获取或设置读卡器IP
        /// </summary>
        [DataMember]
        public string ID { get; set; }
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
