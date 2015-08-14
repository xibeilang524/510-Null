using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.OpenCard.OpenCardService
{
    [DataContract]
    public class ZSTSetting
    {
        public readonly static string CardType = "中山通";
        #region 构造函数
        public ZSTSetting()
        {
            Items = new List<ZSTItem>();
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置中山通与停车场通道键值对,以中山通读卡器IP为键，通道ID为值
        /// </summary>
        [DataMember]
        public List<ZSTItem> Items { get; set; }

        /// <summary>
        /// 获取或设置设置中是否存在IP地址为指定IP的读卡器
        /// </summary>
        /// <param name="readerIP"></param>
        /// <returns></returns>
        public bool HasReader(string readerIP)
        {
            if (Items != null) return Items.Exists(item => item.ReaderIP == readerIP);
            return false;
        }
        /// <summary>
        /// 获取IP为指定值的中山通读卡器信息
        /// </summary>
        /// <param name="readerIP"></param>
        /// <returns></returns>
        public ZSTItem GetReader(string readerIP)
        {
            if (Items != null) return Items.FirstOrDefault(item => item.ReaderIP == readerIP);
            return null;
        }
        #endregion
    }

    /// <summary>
    /// 表示一个中山通读卡器
    /// </summary>
    [DataContract]
    public class ZSTItem
    {
        /// <summary>
        /// 获取或设置读卡器IP
        /// </summary>
        [DataMember]
        public string ReaderIP { get; set; }
        /// <summary>
        /// 获取或设置通道ID，如果没有指定的话为0
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }
        /// <summary>
        /// 获取或设置说明信息
        /// </summary>
        [DataMember]
        public string Memo { get; set; }
    }
}
